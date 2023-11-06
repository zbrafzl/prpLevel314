using Prp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hims.API.Controllers
{
    [RoutePrefix("prod")]
    public class APICandidateInfoController : PRPAPIBaseV1Controller
    {

        [HttpGet]
        [Route("applicant/get")]
        public ApplicantInfoAPI ApplicantGetInfo(string applicantNo)
        {
            ApplicantInfoAPI obj = new ApplicantInfoAPI();
            if (DateTime.Now < new DateTime(2023, 11, 30, 00, 00, 00) && (applicantNo.Substring(0, 3) == "921" || applicantNo.Substring(0, 3) == "922" || applicantNo.Substring(0, 3) == "923"))
            {
                try
                {
                    string strurltest = String.Format("http://172.16.38.9/api/phf/applicant/get?applicantNo=" + applicantNo + "");
                    //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    WebRequest requestObjGet = WebRequest.Create(strurltest);
                    requestObjGet.Method = "GET";
                    HttpWebResponse responseObjGet = null;
                    responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();

                    string strresulttest = null;
                    using (Stream stream = responseObjGet.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(stream);
                        strresulttest = sr.ReadToEnd();
                        sr.Close();
                    }
                    string[] packets = strresulttest.Split(':');
                    int applicantId = packets[1].TooString().Replace("\"", "").Split(',')[0].TooInt();
                    string applicantNos = packets[2].TooString().Replace("\"", "").Split(',')[0].TooString();
                    string name = packets[3].TooString().Replace("\"", "").Split(',')[0].TooString();
                    string pmdc = packets[4].TooString().Replace("\"", "").Split(',')[0].TooString();
                    string cnic = packets[5].TooString().Replace("\"", "").Split(',')[0].TooString();
                    int amount = packets[6].TooString().Replace("\"", "").Split(',')[0].TooInt();
                    DateTime dueDate = DateTime.Now;
                    try
                    {
                        dueDate = Convert.ToDateTime(packets[7].TooString().Replace("\"", "").Split(' ')[0]);
                    }
                    catch(Exception ex)
                    {

                    }
                    string msg = packets[10].TooString().Replace("\"", "").Split(',')[0].TooString();
                    bool status =  false;
                    try
                    {
                        status = packets[11].TooString().Replace("}", "").TooString().TooBoolean();
                    }
                    catch (Exception ex)
                    {

                    }
                    obj.applicantId = applicantId;
                    obj.applicantNo = applicantNos;
                    obj.name = name;
                    obj.pmdcNo = pmdc;
                    obj.cnicNo = cnic;
                    obj.amount = amount;
                    obj.dueDate = dueDate;
                    obj.message = msg;
                    obj.status = status;
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                obj = new CommonDAL().ApplicantGetInfo(applicantNo, 2);
            }
            
            //ApplicantInfoAPI obj = new CommonDAL().ApplicantGetInfo(applicantNo, 2);

            //if (obj != null && obj.applicantId > 0)
            //{
            //    obj.message = "Applicant Exists";
            //    obj.status = true;
            //}
            //else
            //{
            //    obj.message = "Applicant Not Exists";
            //    obj.status = false;

            //}
            return obj;
        }


        [HttpPost]
        [Route("applicant/voucher-add")]
        public MessageAPI ApplicantVoucherInfoAdd(ApplicantVoucherAPIInPut obj)
        {
            MessageAPI msg = new MessageAPI();
            if (DateTime.Now < new DateTime(2023, 11, 30, 00, 00, 00) && (obj.applicantNo.Substring(0, 3) == "921" || obj.applicantNo.Substring(0, 3) == "922" || obj.applicantNo.Substring(0, 3) == "923"))
            { 
                try
                {
                    //http://172.16.38.8/api/phf/applicant/voucher-add
                    string strurltest = String.Format("http://172.16.38.9/api/phf/applicant/voucher-add");
                    //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    WebRequest requestObjGet = WebRequest.Create(strurltest);
                    requestObjGet.Method = "POST";
                    requestObjGet.ContentType = "application/json";
                    string postData = "{\"applicantNo\":\""+obj.applicantNo+ "\",\"amount\":\"" + obj.amount.TooString()+ "\",\"transactionIdBank\":\"" + obj.transactionIdBank+ "\"}";
                    using (var streamWriter = new StreamWriter(requestObjGet.GetRequestStream()))
                    {
                        streamWriter.Write(postData);
                        streamWriter.Flush();
                        streamWriter.Close();

                        var httpResponse = (HttpWebResponse)requestObjGet.GetResponse();

                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result2 = streamReader.ReadToEnd();
                            string[] packets = result2.TooString().Split(':');
                            msg.message = packets[1].TooString().Replace("\"", "").Split(',')[0].TooString();
                            msg.status = packets[2].TooString().Replace("\"", "").Split('}')[0].TooBoolean();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
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
                    item.transactionType = 2;

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
                item.transactionType = 2;


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
            if (DateTime.Now < new DateTime(2023, 11, 30, 00, 00, 00) && (applicantNo.Substring(0, 3) == "921" || applicantNo.Substring(0, 3) == "922" || applicantNo.Substring(0, 3) == "923"))
            {
                try
                {
                    string strurltest = String.Format("http://172.16.38.9/api/phf/voucher/info-get?applicantNo=" + applicantNo + "");
                    //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    WebRequest requestObjGet = WebRequest.Create(strurltest);
                    requestObjGet.Method = "GET";
                    HttpWebResponse responseObjGet = null;
                    responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();
                    string strresulttest = null;
                    using (Stream stream = responseObjGet.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(stream);
                        strresulttest = sr.ReadToEnd();
                        sr.Close();
                    }
                    string[] packets = strresulttest.Split(':');
                    //{"applicantId":"16019","applicantNo":"92316019","name":"user","pmdcNo":"","cnicNo":"11111-1111111-1","amount":300,"transactionStatus":"PAID","transactionIdBank":"0000001"}
                    objRturn.applicantId = packets[1].TooString().Replace("\"", "").Split(',')[0].TooInt();
                    objRturn.applicantNo = packets[2].TooString().Replace("\"", "").Split(',')[0].TooString();
                    objRturn.cnicNo = packets[5].TooString().Replace("\"", "").Split(',')[0].TooString();
                    objRturn.name = packets[3].TooString().Replace("\"", "").Split(',')[0].TooString();
                    objRturn.pmdcNo = packets[4].TooString().Replace("\"", "").Split(',')[0].TooString();
                    objRturn.transactionIdBank = packets[8].TooString().Replace("\"", "").Split('}')[0].TooString();
                    string status = packets[7].TooString().Replace("\"", "").Split('}')[0].TooString();
                    objRturn.transactionStatus = packets[7].TooString().Replace("\"", "").Split(',')[0].TooString();
                }
                catch (Exception ex)
                {

                }
                
            }
            else
            {
                try
                {
                    ApplicantVoucherBank obj = new ApplicantDAL().ApplicantVoucherGetByApplicantNo(applicantNo, 2);

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
            }
            return objRturn;
        }



    }
}
