using Prp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Prp.Data
{
    public class EmailDAL : PrpDBConnect
    {
        public EmailDAL()
        {
        }

        public Message EmailProcessAdd(EmailProcess obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmailProcessAdd]"
            };
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
            sqlCommand.Parameters.AddWithValue("@keyword", obj.keyword);
            sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.ExecuteNonQuery(sqlCommand, 0);
        }

        //public List<EmailProcess> EmailProcessGetAllRemaninig()
        //{
        //    List<EmailProcess> emailProcesses = new List<EmailProcess>();
        //    try
        //    {
        //        List<spEmailProcessGetAllRemaninig_Result> list = this.db.spEmailProcessGetAllRemaninig().ToList<spEmailProcessGetAllRemaninig_Result>();
        //        emailProcesses = MapEmail.ToEntityList(list);
        //    }
        //    catch (Exception exception)
        //    {
        //        emailProcesses = new List<EmailProcess>();
        //    }
        //    return emailProcesses;
        //}

        public List<EmailResp> EmailProcessGetAllRemainging()
        {
            List<EmailResp> listEmail = new List<EmailResp>();
            try
            {
                List<spEmailProcessGetAllRemaninig_Result> list = this.db.spEmailProcessGetAllRemaninig().ToList<spEmailProcessGetAllRemaninig_Result>();
                listEmail = MapEmail.ToEntityList(list);
            }
            catch (Exception exception)
            {
            }
            return listEmail;
        }

        public EmailProcess EmailProcessGetByApplicantAndType(int applicantId, int typeId)
        {
            EmailProcess emailProcess = new EmailProcess();
            try
            {
                SqlCommand sqlCommand = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spEmailProcessGetByApplicantAndType]"
                };
                sqlCommand.Parameters.AddWithValue("@applicantId", applicantId);
                sqlCommand.Parameters.AddWithValue("@typeId", typeId);
                foreach (DataRow row in PrpDbADO.FillDataTable(sqlCommand, "").Rows)
                {
                    emailProcess.emailProcessId = row["emailProcessId"].TooInt();
                    emailProcess.applicantId = row["applicantId"].TooInt();
                    emailProcess.title = row["title"].TooString("");
                    emailProcess.subject = row["subject"].TooString("");
                    emailProcess.body = row["body"].TooString("");
                }
            }
            catch (Exception exception)
            {
                emailProcess = new EmailProcess();
            }
            return emailProcess;
        }

        public List<EmailProcess> EmailProcessGetByType(int typeId)
        {
            List<EmailProcess> emailProcesses = new List<EmailProcess>();
            try
            {
                //List<spEmailProcessGetByType_Result> list = this.db.spEmailProcessGetByType(new int?(typeId)).ToList<spEmailProcessGetByType_Result>();
                //emailProcesses = MapEmail.ToEntityList(list);
            }
            catch (Exception exception)
            {
                emailProcesses = new List<EmailProcess>();
            }
            return emailProcesses;
        }

        public Message EmailStatusAddUpdate(StatusEmail obj)
        {
            Message message = new Message();
            try
            {
                spEmailStatusAddUpdate_Result spEmailStatusAddUpdateResult = this.db.spEmailStatusAddUpdate(new int?(obj.emailStatusId), new int?(obj.applicantId), new int?(obj.typeId), new int?(obj.statusId), obj.statusMessage, obj.body, new int?(obj.adminId), new DateTime?(obj.sechduleDate)).FirstOrDefault<spEmailStatusAddUpdate_Result>();
                message = MapStatusEmail.ToEntity(spEmailStatusAddUpdateResult);
            }
            catch (Exception exception)
            {
                message.msg = exception.Message;
                message.id = 0;
            }
            return message;
        }

        public Message EmailStatusAddUpdate(EmailProcess obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmailProcessUpdateStatus]"
            };
            sqlCommand.Parameters.AddWithValue("@emailProcessId", obj.emailProcessId);
            sqlCommand.Parameters.AddWithValue("@isProcess", obj.isProcess);
            sqlCommand.Parameters.AddWithValue("@isSent", obj.isSent);
            sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.ExecuteNonQuery(sqlCommand, 0);
        }

        public Message EmailStatusUpdateByProcessIds(string emailProcessIds)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmailProcessUpdateStatusByProcessIds]"
            };
            sqlCommand.Parameters.AddWithValue("@emailProcessIds", emailProcessIds);
            return PrpDbADO.ExecuteNonQuery(sqlCommand, 0);
        }

        public Message EmailTemplateAddUpdate(EmailTemplate template)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmailTemplateAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@tempId", template.tempId);
            sqlCommand.Parameters.AddWithValue("@typeId", template.typeId);
            sqlCommand.Parameters.AddWithValue("@inductionId", template.inductionId);
            sqlCommand.Parameters.AddWithValue("@title", template.title);
            sqlCommand.Parameters.AddWithValue("@subject", template.subject);
            sqlCommand.Parameters.AddWithValue("@body", template.body);
            sqlCommand.Parameters.AddWithValue("@isActive", template.isActive);
            sqlCommand.Parameters.AddWithValue("@adminId", template.adminId);


            return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
        }

        public EmailTemplate EmailTemplateByTypeId(int typeId, int inductionId = 0)
        {
            EmailTemplate emailTemplate = new EmailTemplate();
            try
            {
                //spEmailTemplateByTypeId_Result spEmailTemplateByTypeIdResult = this.db.spEmailTemplateByTypeId(new int?(typeId), new int?(inductionId)).FirstOrDefault<spEmailTemplateByTypeId_Result>();
                //emailTemplate = MapEmail.ToEntity(spEmailTemplateByTypeIdResult);
            }
            catch (Exception exception)
            {
                emailTemplate = new EmailTemplate();
            }
            return emailTemplate;
        }

        public DataSet EmailTemplateSearch(EmailTemplate obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmailTemplateSearch]"
            };
            sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
            sqlCommand.Parameters.AddWithValue("@search", obj.search.TooString());
            return PrpDbADO.FillDataSet(sqlCommand);
        }

        public DataSet EmailTemplateGetInfoById(EmailTemplate obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmailTemplateGetInfoById]"
            };
            sqlCommand.Parameters.AddWithValue("@tempId", obj.tempId);
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);

            return PrpDbADO.FillDataSet(sqlCommand);
        }

    }
}