using Org.BouncyCastle.Asn1.Cmp;
using Prp.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using static Prp.Sln.ProjConstant;
using System.Web.Services.Description;
using Org.BouncyCastle.Asn1.Crmf;
using System.Text;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Web.UI.WebControls;

namespace Prp.Sln
{

    public static class FunctionUI
    {

        public static string GenerateRandomOTP(int iOTPLength = 6)
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();
            for (int i = 0; i < iOTPLength; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }
            return sOTP;
        }

        public static Prp.Data.Message SendActivationEmail(int applicantId)
        {
            Prp.Data.Message msg = new Prp.Data.Message();
            try
            {
                EmailResp objEm = new EmailResp();
                EmailProcess objs = new EmailProcess();
                objs.applicantId = applicantId;
                objs.typeId = EmailTemplateType.registration;
                string key = Convert.ToString(objs.applicantId + 10011);
                objs.reffIds1 = key.Encrypt();
                objEm = new EmailSMSDAL().EmailProcessGetInfoByType(objs);

                msg = ProcessEmail(objEm);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = msg.msg + "Some Error : Occured " + ex.Message;
            }
            return msg;
        }

        public static Prp.Data.Message ProcessEmail(EmailResp objEm)
        {
            Prp.Data.Message msg = new Prp.Data.Message();
            try
            {
                if (objEm.status)
                {
                    msg.msg = "1. Email Fetched from Db.";
                    Prp.Data.Message msgEm = objEm.SendEmail();
                    objEm.resp = msgEm.msg;
                    objEm.isProcess = 1;
                    objEm.isSent = 0;
                    if (msgEm.status == true)
                    {
                        objEm.isSent = 1;
                        msg.msg = msg.msg + "2. Email sent to applicant";
                    }
                    else
                    {
                        msg.msg = msg.msg + "2. Email not sent to applicant";
                    }
                    Prp.Data.Message msgStatus = new EmailSMSDAL().EmailProcessAddUpdate(objEm);
                    if (msgStatus.status)
                    {
                        msg.msg = msg.msg + "3. Email sent stauts updated";
                    }
                    else
                    {
                        msg.msg = msg.msg + "3. Email sent stauts Error occured";
                    }
                }
                else
                {
                    msg.msg = objEm.body;
                }
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = msg.msg + "Some Error : Occured " + ex.Message;
            }
            return msg;
        }

        public static SmsProcess SmsProcessMakeDefaultObject(this bool isSent, int applicantId, int smsId)
        {
            SmsProcess obj = new SmsProcess();
            try
            {
                obj.applicantId = applicantId;
                obj.isSent = 0;
                if (isSent)
                    obj.isSent = 1;
                obj.isProcess = 1;
                obj.smsId = smsId;
            }
            catch (Exception)
            {
                obj = new SmsProcess();
            }
            return obj;

        }

        public static SMSResp SmsProcessMakeDefaultObjectResp(this bool isSent, int applicantId, int smsId = 1)
        {
            SMSResp obj = new SMSResp();
            try
            {
                obj.applicantId = applicantId;
                obj.isSent = 0;
                if (isSent)
                    obj.isSent = 1;
                obj.isProcess = 1;
                obj.smsId = smsId;
            }
            catch (Exception)
            {
                obj = new SMSResp();
            }
            return obj;

        }

        public static SmsAPI GetSmsUrl(string number, string message)
        {
            SmsAPI obj = new SmsAPI();
            try
            {

                obj.link = ConfigurationManager.AppSettings["SmsApiLink"];
                string password = "";
                try
                {
                    password = ConfigurationManager.AppSettings["SmsPassword"];
                }
                catch (Exception)
                {
                    password = "";
                }

                if (String.IsNullOrWhiteSpace(password))
                {
                    try
                    {
                        string path = ProjConstant.SMSPath.Path.smsPassword;
                        string filePath = Path.Combine(HttpContext.Current.Server.MapPath(path));
                        password = filePath.ReadFile();
                        password = password.Trim();
                    }
                    catch (Exception)
                    {
                        password = "";
                    }
                }
                if (String.IsNullOrWhiteSpace(password))
                {
                    password = "AHL%2fcJw8rwobY9hd2XefAq84EdiM8lf4GtDI08ob%2f2SciwVUqiYHKgN%2fNoFgo65deg%3d%3d";
                }
                string urlParameters = "?user=phf&pwd=#password#&sender=PHF&reciever=#number#&msg-data=#message#&response=string";
                number = "92" + number.Substring(1, number.Length - 1);
                urlParameters = urlParameters.Replace("#password#", password);
                urlParameters = urlParameters.Replace("#number#", number);
                urlParameters = urlParameters.Replace("#message#", message);

                obj.url = urlParameters;
                obj.fullUrl = obj.link + obj.url;
            }
            catch (Exception)
            {
                obj.fullUrl = "";
            }
            return obj;
        }

        public static Prp.Data.Message SendSms(string number, string message, int typeId = 1)
        {

            Prp.Data.Message msg = new Prp.Data.Message();
            if (typeId == 1)
                msg = SendSms_M1(number, message);
            else if (typeId == 2)
                msg = SendSms_M2(number, message);
           

            return msg;
        }

        public static Prp.Data.Message SendSms_M2(string number, string message)
        {

            Prp.Data.Message msg = new Prp.Data.Message();
            string msgBody = "";
            SmsAPI objApi = GetSmsUrl(number, message);
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(objApi.link);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(objApi.url).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    msgBody = responseContent.ReadAsStringAsync().GetAwaiter().GetResult().ToLower();

                    if (msgBody.Contains("successfully"))
                    {
                        msg.status = true;
                        msg.msg = "Sent";
                    }
                    else
                    {
                        msg.status = false;
                        msg.msg = "1.Error!";
                    }
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    msg.status = false;
                    msg.msg = "2.Error!!";
                }
                //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                client.Dispose();
                msg.message = objApi.fullUrl;
            }
            catch (Exception ex)
            {
                msg.msg = "3.Error!!! " + ex.Message;
            }

            msg.message = msg.msg + msgBody + "    :     " + objApi.fullUrl;
            return msg;
        }


        public static Prp.Data.Message SendSms_M1(string number, string message)
        {
            Prp.Data.Message msg = new Prp.Data.Message();
            string msgBody = "";
            try
            {

                var url = ConfigurationManager.AppSettings["SmsApiLink2"].TooString();
                var password = ConfigurationManager.AppSettings["SmsPassword2"].TooString();
             
                MsgBodyHisdu objBody = new MsgBodyHisdu();
                objBody.message = message;
                objBody.mobileNumber = number;
                // Add an Accept header for JSON format.
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(objBody);

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("XApiKey", password);
                //var content = new StringContent("{\r\n \"id\": 0,\r\n \"mobileNumber\": \""+ number + "\",\r\n \"message\": \""+message+"\",\r\n  \"hospitalId\": \"PMF\",\r\n\"hospitalName\": \"Promotion\",\r\n\"projectId\": 1,\r\n \"masking\": \"HISDU\"\r\n}\r\n", null, "application/json");
                var content = new StringContent(json, null, "application/json");

                request.Content = content;
                var resp = client.SendAsync(request).Result;


                if (resp.IsSuccessStatusCode == true)
                {
                    msg.status = true;
                    msg.msg = "Sent";
                }
                else
                {
                    msg.status = false;
                    msg.msg = "1.Error!";
                }
                client.Dispose();
            }
            catch (Exception ex)
            {
                msg.msg = "3.Error!!!";
                msg.msg = "3.Error!!!  " + ex.Message;
            }

            msg.message = msg.msg + msgBody;
            return msg;
        }
       

    }
    public static class EmailFunctions
    {
        public static Prp.Data.Message SendEmail(this EmailResp obj)
        {
            //, string subject, string emailTitle, string body
            Prp.Data.Message msg = new Prp.Data.Message();
            try
            {
                string mailServer = WebConfigurationManager.AppSettings["MailServer"].ToString();
                int mailPort = WebConfigurationManager.AppSettings["MailPort"].TooInt();
                bool mailSSL = WebConfigurationManager.AppSettings["MailSSl"].TooBoolean();
                bool useDefaultCredentials = WebConfigurationManager.AppSettings["UseDefaultCredentials"].TooBoolean();

                var fromAddress = new MailAddress(obj.emailFrom, obj.title);
                var toAddress = new MailAddress(obj.emailId);

                var smtp = new SmtpClient
                {
                    Host = mailServer,
                    Port = mailPort,
                    EnableSsl = mailSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = useDefaultCredentials,
                    Credentials = new NetworkCredential(fromAddress.Address, obj.emailPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = obj.subject,
                    Body = obj.body
                })
                {
                    smtp.Send(message);
                }
                msg.status = true;
                msg.message = "Sent";

            }
            catch (Exception ex)
            {
                msg.status = false;

                msg.message = ex.Message;
            }

            return msg;

        }

        public static Prp.Data.Message SendEmailQuestion(Contact contact)
        {
            Prp.Data.Message msg = new Prp.Data.Message();
            try
            {
                string path = ProjConstant.Email.Path.question;
                string filePath = path.GetServerPathFolder();
                string body = filePath.ReadFile();

                if (String.IsNullOrWhiteSpace(contact.name))
                {
                    contact.name = "Applicant";
                }

                string emailBody = body.Replace("{#question#}", contact.question)
                    .Replace("{#name#}", contact.name)
                    .Replace("{#pmdcNo#}", contact.pmdcNo)
                                 .Replace("{#emailId#}", contact.emailId);
                msg = contact.emailId.SendEmail(ProjConstant.Email.Subject.contactUs, "PRP", emailBody);
            }
            catch (Exception ex)
            {

                msg.status = false;
                msg.message = ex.Message;
            }

            return msg;

        }


        public static Prp.Data.Message SendEmail(this EmailProcess obj)
        {
            //, string subject, string emailTitle, string body
            Prp.Data.Message msg = new Prp.Data.Message();
            try
            {

                string emailIdFrom = WebConfigurationManager.AppSettings["MailEmailId"].ToString();
                string password = WebConfigurationManager.AppSettings["MailPassword"].ToString();
                string mailServer = WebConfigurationManager.AppSettings["MailServer"].ToString();
                int mailPort = WebConfigurationManager.AppSettings["MailPort"].TooInt();
                bool mailSSL = WebConfigurationManager.AppSettings["MailSSl"].TooBoolean();
                bool useDefaultCredentials = WebConfigurationManager.AppSettings["UseDefaultCredentials"].TooBoolean();

                var fromAddress = new MailAddress(emailIdFrom, obj.title);
                var toAddress = new MailAddress(obj.emailId);


                var smtp = new SmtpClient
                {
                    Host = mailServer,
                    Port = mailPort,
                    EnableSsl = mailSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = useDefaultCredentials,
                    Credentials = new NetworkCredential(fromAddress.Address, password)
                };


                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = obj.subject,
                    Body = obj.body
                })
                {
                    smtp.Send(message);
                }
                msg.status = true;
                msg.message = "Sent";

            }
            catch (Exception ex)
            {
                msg.status = false;

                msg.message = ex.Message;
            }

            return msg;

        }

        public static Prp.Data.Message SendEmail(this string emailTo, string subject, string emailTitle, string body)
        {
            Prp.Data.Message msg = new Prp.Data.Message();
            try
            {

                string emailIdFrom = WebConfigurationManager.AppSettings["MailEmailId"].ToString();
                string password = WebConfigurationManager.AppSettings["MailPassword"].ToString();


                string mailServer = WebConfigurationManager.AppSettings["MailServer"].ToString();
                int mailPort = WebConfigurationManager.AppSettings["MailPort"].TooInt();
                bool mailSSL = WebConfigurationManager.AppSettings["MailSSl"].TooBoolean();
                bool useDefaultCredentials = WebConfigurationManager.AppSettings["UseDefaultCredentials"].TooBoolean();

                var fromAddress = new MailAddress(emailIdFrom, emailTitle);
                var toAddress = new MailAddress(emailTo);

                //MailAddress addressBCC = new MailAddress("phf.it.waheedahmad@gmail.com");

                var smtp = new SmtpClient
                {
                    Host = mailServer,
                    Port = mailPort,
                    EnableSsl = mailSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = useDefaultCredentials,
                    Credentials = new NetworkCredential(fromAddress.Address, password)
                };


                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                msg.status = true;
                msg.message = "Sent";

            }
            catch (Exception ex)
            {
                msg.status = false;

                msg.message = ex.Message;
            }

            return msg;

        }

        public static Prp.Data.Message SendEmail0(this string emailTo, string subject, string emailTitle, string body)
        {
            Prp.Data.Message msg = new Prp.Data.Message();
            try
            {

                string emailIdFrom = WebConfigurationManager.AppSettings["MailEmailId0"].ToString();
                string password = WebConfigurationManager.AppSettings["MailPassword0"].ToString();
                string mailServer = WebConfigurationManager.AppSettings["MailServer"].ToString();
                int mailPort = WebConfigurationManager.AppSettings["MailPort"].TooInt();
                bool mailSSL = WebConfigurationManager.AppSettings["MailSSl"].TooBoolean();
                bool useDefaultCredentials = WebConfigurationManager.AppSettings["UseDefaultCredentials"].TooBoolean();

                var fromAddress = new MailAddress(emailIdFrom, emailTitle);
                var toAddress = new MailAddress(emailTo);

                //MailAddress addressBCC = new MailAddress("phf.it.waheedahmad@gmail.com");

                var smtp = new SmtpClient
                {
                    Host = mailServer,
                    Port = mailPort,
                    EnableSsl = mailSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = useDefaultCredentials,
                    Credentials = new NetworkCredential(fromAddress.Address, password)
                };


                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                msg.status = true;
                msg.message = "Sent";

            }
            catch (Exception ex)
            {
                msg.status = false;

                msg.message = ex.Message;
            }

            return msg;

        }

        public static Prp.Data.Message SendEmail1(this string emailTo, string subject, string emailTitle, string body)
        {
            Prp.Data.Message msg = new Prp.Data.Message();
            try
            {

                string emailIdFrom = WebConfigurationManager.AppSettings["MailEmailId1"].ToString();
                string password = WebConfigurationManager.AppSettings["MailPassword1"].ToString();
                string mailServer = WebConfigurationManager.AppSettings["MailServer"].ToString();
                int mailPort = WebConfigurationManager.AppSettings["MailPort"].TooInt();
                bool mailSSL = WebConfigurationManager.AppSettings["MailSSl"].TooBoolean();
                bool useDefaultCredentials = WebConfigurationManager.AppSettings["UseDefaultCredentials"].TooBoolean();

                var fromAddress = new MailAddress(emailIdFrom, emailTitle);
                var toAddress = new MailAddress(emailTo);

                //MailAddress addressBCC = new MailAddress("phf.it.waheedahmad@gmail.com");

                var smtp = new SmtpClient
                {
                    Host = mailServer,
                    Port = mailPort,
                    EnableSsl = mailSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = useDefaultCredentials,
                    Credentials = new NetworkCredential(fromAddress.Address, password)
                };


                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                msg.status = true;
                msg.message = "Sent";

            }
            catch (Exception ex)
            {
                msg.status = false;

                msg.message = ex.Message;
            }

            return msg;

        }

    }


}