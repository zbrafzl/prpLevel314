﻿using Prp.Data;
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
                if (DateTime.Now < new DateTime(2023, 10, 31, 00, 00, 00) && (applicantNo.Substring(0, 3) == "921" || applicantNo.Substring(0, 3) == "922" || applicantNo.Substring(0, 3) == "923"))
                {
                    try
                    {
                        string strurltest = String.Format("http://172.16.38.8/phf_nursing/public/api/phf/applicant/get?applicantNo=" + applicantNo + "");
                        WebRequest requestObjGet = WebRequest.Create(strurltest);
                        requestObjGet.Method = "GET";
                        WebResponse responseObjGet = null;
                        responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();
                        string strresulttest = null;
                        StreamReader reader = new StreamReader(responseObjGet.GetResponseStream());
                        string jsons = reader.ReadToEnd();
                        //{"$id":"1","applicantId":0,"applicationNo":0,"name":null,"pmdcNo":null,"cnicNo":null,"amount":0,"dueDate":"0001-01-01T00:00:00","message":"Applicant Not Exists","status":false}
                        //{"applicantId":"16019","applicantNo":"92316019","name":"user","pmdcNo":"","cnicNo":"11111-1111111-1","amount":300,"dueDate":"2023-10-31 00:00:00","message":"Applicant Exists","success":true}
                        string[] packets = jsons.Split(':');
                        string iddd = packets[2].Split(',')[0].Trim();
                        int nnnn = 0;
                        string ssss = null;
                        bool success = int.TryParse(new string(iddd
                             .SkipWhile(x => !char.IsDigit(x))
                             .TakeWhile(x => char.IsDigit(x))
                             .ToArray()), out nnnn);
                        if (success)
                        {
                            obj.applicantId = Convert.ToInt32(nnnn);
                        }

                        ssss = null;
                        nnnn = 0;
                        ssss = packets[3].Split(',')[0].Trim();
                        if (success)
                        {
                            obj.applicantNo = ssss;
                        }


                        ssss = null;
                        nnnn = 0;
                        iddd = packets[4].Split(',')[0].Trim();
                        iddd = iddd.Split(',')[0];
                        iddd = iddd.Split('"')[1];
                        iddd = iddd.Split('\\')[0];
                        if (success)
                        {
                            obj.name = iddd;
                        }

                        iddd = packets[5].Split(',')[0].Trim();
                        if (success)
                        {
                            try
                            {
                                iddd = iddd.Split(',')[0];
                                iddd = iddd.Split('"')[1];
                                iddd = iddd.Split('\\')[0];
                                obj.pmdcNo = iddd;
                            }
                            catch (Exception ex)
                            {
                                obj.pmdcNo = iddd;
                            }
                        }

                        iddd = packets[6].Split(',')[0].Trim();
                        iddd = iddd.Split(',')[0];
                        iddd = iddd.Split('"')[1];
                        iddd = iddd.Split('\\')[0];
                        if (success)
                        {
                            obj.cnicNo = iddd;
                        }

                        ssss = null;
                        nnnn = 0;
                        iddd = packets[7].Split(',')[0].Trim();
                        success = int.TryParse(new string(iddd
                             .SkipWhile(x => !char.IsDigit(x))
                             .TakeWhile(x => char.IsDigit(x))
                             .ToArray()), out nnnn);
                        if (success)
                        {
                            obj.amount = Convert.ToInt32(nnnn);
                        }

                        iddd = packets[8].Split(',')[0].Trim();
                        iddd += ":";
                        iddd += packets[9].Trim();
                        iddd += ":";
                        iddd += packets[10].Trim();
                        iddd = iddd.Split(',')[0];
                        iddd = iddd.Split('"')[1];
                        iddd = iddd.Split('\\')[0];
                        if (success)
                        {
                            try
                            {
                                obj.dueDate = DateTime.Parse(iddd.Split(',')[0]);
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                        iddd = packets[11].Split(',')[0].Trim();
                        iddd = iddd.Split(',')[0];
                        iddd = iddd.Split('"')[1];
                        iddd = iddd.Split('\\')[0];
                        if (success)
                        {
                            obj.message = iddd;
                        }

                        iddd = packets[12].Split(',')[0].Trim();
                        if (success)
                        {
                            string nw = iddd.Split('}')[0];
                            //nw = nw.Split('"')[1];
                            obj.status = Convert.ToBoolean(nw);
                        }


                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
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

            }
            catch (Exception ex)
            {

                obj = new ApplicantInfoAPI();
            }
            /*
            ApplicantInfoAPI obj = new CommonDAL().ApplicantGetInfo(applicantNo, 1);
            //if (obj != null && obj.applicantId > 0)
            //{
            //    //obj.message = "Applicant Exists";
            //    //obj.status = true;
            //}
            //else
            //{
            //    obj.message = "Applicant Not Exists";
            //    obj.status = false;
            //}
            return obj;
            */
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
