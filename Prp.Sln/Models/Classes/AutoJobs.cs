using Prp.Data;
using Prp.Model;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Interop;

namespace Prp.Sln
{
    public class AutoJobs
    {
    }

    public class JobServiceSMS : IJob
    {
        public static readonly string JobServiceStatusSMS = ConfigurationManager.AppSettings["JobServiceStatusSMS"];
        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() =>
            {
                if (JobServiceStatusSMS.Equals("ON"))
                {
                    try
                    {
                        List<spSMSProcessGetRemaning_Result> listProcess = new SMSDAL().SMSProcessGetRemaning();
                        if (listProcess != null && listProcess.Count > 0)
                        {
                            foreach (var sms in listProcess)
                            {
                                Message msgSms = FunctionUI.SendSms(sms.contactNumber, sms.body);
                                try
                                {
                                    SMSResp objResp = msgSms.status.SmsProcessMakeDefaultObjectResp(sms.applicantId.TooInt(), sms.smsId.TooInt());
                                    objResp.detailId = sms.smsProcessId.TooInt();
                                    new SMSDAL().SMSProcessAddUpdate(objResp);
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }

                    }
                    catch (Exception)
                    {
                    }

                }
            });
            return task;
        }
    }

    public class JobServiceEmail : IJob
    {
        public static readonly string JobServiceStatusEmail = ConfigurationManager.AppSettings["JobServiceStatusEmail"];
        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() =>
            {
                if (JobServiceStatusEmail.Equals("ON"))
                {
                    try
                    {
                        string emailProcessIds = "";
                        List<EmailResp> listProcess = new EmailDAL().EmailProcessGetAllRemainging().ToList();
                        if (listProcess != null && listProcess.Count > 0)
                        {
                            foreach (var objEm in listProcess)
                            {
                                try
                                {
                                    Message msgSms = FunctionUI.ProcessEmail(objEm);
                                    if (msgSms.status)
                                    {
                                        emailProcessIds = emailProcessIds + objEm.detailId + ",";
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            emailProcessIds = emailProcessIds.TrimEnd(',');
                            new EmailDAL().EmailStatusUpdateByProcessIds(emailProcessIds);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            });
            return task;
        }
    }



    public class ExecuteTaskServiceCallScheduler
    {
        //private static readonly string ScheduleCronExpression = ConfigurationManager.AppSettings["ExecuteTaskScheduleCronExpression"];
        public static async System.Threading.Tasks.Task StartAsync()
        {
            try
            {

                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                if (!scheduler.IsStarted)
                {
                    await scheduler.Start();
                }


                var jobSMS = JobBuilder.Create<JobServiceSMS>()
                    .WithIdentity("JobSMS", "GroupJobSMS")
                    .Build();

                var triggerSMS = TriggerBuilder.Create()
                    .WithIdentity("TriggerSMS", "GroupTriggerSMS")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                    .Build();

                await scheduler.ScheduleJob(jobSMS, triggerSMS);



                var jobEmail = JobBuilder.Create<JobServiceEmail>()
                   .WithIdentity("JobEmail", "GroupJobEmail")
                   .Build();

                var triggerEmail = TriggerBuilder.Create()
                    .WithIdentity("TriggerEmail", "GroupTriggerEmail")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(ProjConstant.emailInterval)
                    .RepeatForever())
                    .Build();

                await scheduler.ScheduleJob(jobEmail, triggerEmail);

            }
            catch (Exception ex)
            {

            }
        }
    }
}