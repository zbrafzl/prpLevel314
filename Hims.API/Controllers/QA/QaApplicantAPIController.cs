using Prp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hims.API.Controllers.QA
{
    [RoutePrefix("qa")]
    public class QaApplicantAPIController : PrpQaBasedController
    {
        [HttpGet]
        [Route("applicant/get")]
        public ApplicantInfoAPI ApplicantGetInfo(string applicantNo)
        {
            ApplicantInfoAPI obj = new ApplicantInfoAPI();
            try
            {

                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantGetByApplicantNo]"
                };
                cmd.Parameters.AddWithValue("@applicantNo", applicantNo);
                cmd.Parameters.AddWithValue("@transactionType", 1);
                DataTable dt = PrpDbADO.FillDataTable(cmd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    obj.applicantId = dr["applicantId"].TooInt();
                    obj.applicantNo = dr["applicantNo"].TooString();
                    obj.name = dr["name"].TooString();
                    obj.pmdcNo = dr["pmdcNo"].TooString();
                    obj.cnicNo = dr["cnicNo"].TooString();
                    obj.amount = dr["amount"].TooInt();
                    obj.message = dr["message"].TooString();
                    obj.status = dr["status"].TooBoolean();
                    obj.dueDate = Convert.ToDateTime(dr["dueDate"]);
                }


            }
            catch (Exception ex)
            {

                obj = new ApplicantInfoAPI();
            }

            return obj;
        }


        [HttpPost]
        [Route("applicant/voucher-add")]
        public MessageAPI ApplicantVoucherInfoAdd(ApplicantVoucherAPIInPut obj)
        {
            MessageAPI msg = new MessageAPI();
            try
            {
                ApplicantVoucherBank item = new ApplicantVoucherBank();
                item.applicantNo = obj.applicantNo;
                item.applicantId = obj.applicantNo.TooInt();
                item.amount = obj.amount.TooInt();
                if (item.amount == 0)
                    item.amount = 1000;
                item.transactionIdBank = obj.transactionIdBank;
                item.statusBank = 1;
                item.transactionType = 1;
                msg = new ApplicantDAL().ApplicantVoucherBankAddUpdate(item);
            }
            catch (Exception)
            {
                msg.message = "System Error";
                msg.status = false;
            }
            return msg;
        }


        [HttpPost]
        [Route("applicant/voucher-cancel")]
        public MessageAPI ApplicantVoucherInfoCancel(ApplicantVoucherAPIInPut obj)
        {
            MessageAPI msg = new MessageAPI();
            try
            {
                ApplicantVoucherBank item = new ApplicantVoucherBank();
                item.applicantNo = obj.applicantNo;
                item.applicantId = obj.applicantNo.TooInt();
                item.amount = obj.amount.TooInt();
                if (item.amount == 0)
                    item.amount = 1000;
                item.transactionIdBank = obj.transactionIdBank;
                item.statusBank = 2;
                item.transactionType = 1;


                msg = new ApplicantDAL().ApplicantVoucherBankAddUpdate(item);

                //if (msgg.status)
                //{
                //    msg.status = msgg.status;
                //    msg.message = "Transaction saved successfully";
                //}
                //else
                //{
                //    msg.status = false;
                //    msg.message = msg.message;

                //}


            }
            catch (Exception)
            {
                msg.message = "System Error";
                msg.status = false;
            }


            return msg;




        }

        [HttpGet]
        [Route("voucher/info-get")]
        public ApplicantVoucherAPIOutPut VoucherInfoGetByApplicantNo(string applicantNo)
        {
            ApplicantVoucherAPIOutPut objRturn = new ApplicantVoucherAPIOutPut();

            try
            {
                ApplicantVoucherBank obj = new ApplicantDAL().ApplicantVoucherGetByApplicantNo(applicantNo, 1);

                //ApplicantVoucherAPIOutPut
                if (obj != null && obj.applicantId > 0)
                {
                    objRturn.applicantId = obj.applicantId;
                    objRturn.applicantNo = obj.applicantNo;
                    objRturn.name = obj.name;
                    objRturn.pmdcNo = obj.pmdcNo;
                    objRturn.cnicNo = obj.cnicNo;
                    objRturn.transactionStatus = obj.status;
                    objRturn.transactionIdBank = obj.transactionIdBank;
                }
                else
                {
                    objRturn = new ApplicantVoucherAPIOutPut();
                    objRturn.transactionStatus = "Applicant Not Exists";
                }
            }
            catch (Exception)
            {
                objRturn = new ApplicantVoucherAPIOutPut();
                objRturn.transactionStatus = "System Error.";
            }

            return objRturn;
        }
    }
}
