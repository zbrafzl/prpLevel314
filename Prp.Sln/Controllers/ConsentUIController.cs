using Newtonsoft.Json;
using Prp.Data;
using Prp.Sln;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
    public class ConsentUIController : BaseController
    {
        public ConsentUIController()
        {
        }

        public ActionResult ApplicantConsent()
        {
            ConsentModel consentModel = new ConsentModel();
            int num = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
            consentModel.roundId = num;
            consentModel.applicant = (new ApplicantDAL()).GetApplicant(ProjConstant.inductionId, base.loggedInUser.applicantId);
            consentModel.consentId = Request.QueryString["consentId"].TooInt();
            //int result = 0;
            //string query = "select isnull((select top(1) applicantId from tblMeritApplicantFinal where inductionId = 14 and roundNo = 5 and applicantId = " + consentModel.applicant.applicantId + "),0)";
            //SqlConnection connection = new SqlConnection(PrpDbConnectADO.Conn);
            //SqlCommand cmd = new SqlCommand(query, connection);
            //connection.Open();
            //result = Convert.ToInt32(cmd.ExecuteScalar());
            //if (result > 0)
            //{
            //	consentModel.roundId = 5;
            //	num = 5;
            //}
            //else {
            //	consentModel.roundId = 3;
            //	num = 3;
            //}
            //connection.Close();

            DDLConstants dDLConstant = new DDLConstants()
            {
                condition = "ByType",
                typeId = ProjConstant.Constant.consentType
            };
            consentModel.listConsentStatus = (
                from x in (new ConstantDAL()).GetConstantDDL(dDLConstant)
                orderby x.id
                select x).ToList<EntityDDL>();
            consentModel.validStatus = 1;
            if (consentModel.validStatus == 1)
            {
                if (consentModel.consentId > 0)
                {
                    consentModel.consent = (new ConsentDAL()).GetById(consentModel.consentId);
                    if (consentModel.consent.applicantId != base.loggedInUser.applicantId)
                    {
                        consentModel.validStatus = 0;
                    }
                }
                if (consentModel.validStatus == 1)
                {
                    consentModel.listType = (new ConsentDAL()).GetTypeApplicantHasMerit(base.loggedInUser.applicantId, num);
                    consentModel.listConsent = (new ConsentDAL()).GetAllByApplicant(base.loggedInUser.applicantId);
                }
            }
            return View(consentModel);
        }

        [HttpPost]
        public ActionResult ApplicantContents()
        {
            ConsentModel consentModel = new ConsentModel()
            {
                listConsent = (new ConsentDAL()).GetAllByApplicant(base.loggedInUser.applicantId)
            };
            string str = JsonConvert.SerializeObject(consentModel.listConsent);
            return base.Content(str, "application/json");
        }

        public ActionResult ApplicantJoining()
        {
            ApplicantJoiningModel applicantJoiningModel = new ApplicantJoiningModel()
            {
                applicantFinal = (new JoiningDAL()).GetByApplicantDetailById(base.loggedInUser.applicantId),
                info = (new ApplicantDAL()).GetApplicantInfo(ProjConstant.inductionId, ProjConstant.phaseId, base.loggedInUser.applicantId)
            };
            return View(applicantJoiningModel);
        }

        [HttpPost]
        public ActionResult ApplicantMerits()
        {
            int num = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
            ConsentModel consentModel = new ConsentModel()
            {
                roundId = num,
                listType = (new ConsentDAL()).GetTypeApplicantHasMerit(base.loggedInUser.applicantId, num)
            };
            string str = JsonConvert.SerializeObject(consentModel.listType);
            return base.Content(str, "application/json");
        }

        [HttpPost]
        public JsonResult ConsentSave(ConsentPushed consentsFinal)
        {
            Message message = new Message();
            int num = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
            int num1 = ProjConstant.inductionId;
            int num2 = ProjConstant.phaseId;
            for (int i = 0; i < consentsFinal.consentsFinal.Count; i++)
            {
                Consent consent = new Consent()
                {
                    applicantId = base.loggedInUser.applicantId,
                    typeId = consentsFinal.consentsFinal[i].typeId.TooInt(),
                    consentTypeId = consentsFinal.consentsFinal[i].consentTypeId.TooInt(),
                    roundId = num,
                    inductionId = num1,
                    phaseId = num2
                };
                if (base.loggedInUser.adminId == 0)
                {
                }
            }
            return base.Json(message, 0);
        }
        public string GetContactNumber(int applicantId)
        {
            string result = string.Empty;
            string query = "select isnull((select top(1) contactNumber from tblApplicant where applicantId = " + applicantId + "),0)";
            SqlConnection connection = new SqlConnection(PrpDbConnectADO.Conn);
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string value = reader.GetString(0); // assuming the column type is string
                if (!string.IsNullOrEmpty(value))
                {
                    result = value;
                }
                // do something with the value
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public int CheckOtp(string mobileNumber, int otp)
        {
            int result;
            string query = "select isnull((select applicantId from tblOtps where mobilenumber = '" + mobileNumber + "' and otpCode = " + otp + " and isUsed = 0),0)";
            SqlConnection connection = new SqlConnection(PrpDbConnectADO.Conn);
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            int ret = cmd.ExecuteScalar().TooInt();
            connection.Close();
            //check from db where mobile number and otp matches and not used before
            //if exists, update isUsed to 1 AND add verification status 53,1
            if (ret > 0)
            {
                Session["aoolicantId"] = ret;
                result = 1;
                query = "update tblOtps set isUsed = 1 where mobilenumber = '" + mobileNumber + "' and otpCode = " + otp + " and isUsed = 0";
                connection = new SqlConnection(PrpDbConnectADO.Conn);
                cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.ExecuteScalar().TooInt();
                connection.Close();
            }
            else
            {
                result = 0;
            }
            return result;
        }

       

        public string GenerateOtpCode(int applicantId)
        {
            Message msg = new Message();
            int randomNumber = 0;
            string result = string.Empty;
            string tpCode = string.Empty;
            string sRandomOTP = string.Empty;
            int contactNumber = 0;
            string Number = GetContactNumber(applicantId);
            //Number = "03006647709";
            string query2 = "select top(1) Cast(otpCode as varchar(250)) from tblOtps where applicantId = " + applicantId + " AND IsUsed = 0 ";
            SqlConnection connections = new SqlConnection(PrpDbConnectADO.Conn);
            SqlCommand cmds = new SqlCommand(query2, connections);
            connections.Open();
            SqlDataReader readers = cmds.ExecuteReader();
            if (readers.HasRows)
            {

                if (readers.Read())
                {

                    string value = readers.GetString(0); // assuming the column type is string
                    if (!string.IsNullOrEmpty(value))
                    {
                        string query = "update tblOtps set isUsed = 1 where applicantId = " + applicantId + " and isUsed = 0";
                        SqlConnection connection = new SqlConnection(PrpDbConnectADO.Conn);
                        SqlCommand cmd = new SqlCommand(query, connection);
                        connection.Open();
                        cmd.ExecuteScalar().TooInt();
                        connection.Close();
                        result = value;
                    }
                    // do something with the value
                }
            }
            readers.Close();
            connections.Close();
            if (string.IsNullOrEmpty(result))
            {
                if (!string.IsNullOrEmpty(Number))
                {
                    contactNumber = Number.TooInt();
                }
                try
                {
                    string smsBody = "";
                    int smsId = 0;
                    try
                    {
                        SMS sms = new SMSDAL().GetByTypeForApplicant(applicantId, ProjConstant.SMSType.registration);
                        //smsBody = sms.detail;
                        smsId = sms.smsId;
                    }
                    catch (Exception)
                    {
                        smsBody = "";
                    }
                    if (String.IsNullOrWhiteSpace(smsBody))
                    {
                        smsId = 0;
                        sRandomOTP = FunctionUI.GenerateRandomOTP(6);
                        randomNumber = Convert.ToInt32(sRandomOTP);
                        smsBody = "Dear Candidate, Your OTP is : " + randomNumber + ".";

                    }

                    Message msgSms = FunctionUI.SendSms(Convert.ToString(Number), smsBody);
                    Applicant obj = loggedInUser;

                    try
                    {
                        obj.emailId.SendEmail("OTP for Conset", "Consent", smsBody);
                    }
                    catch (Exception exception)
                    {
                    }
                    try
                    {
                        SmsProcess objProcess = msgSms.status.SmsProcessMakeDefaultObject(applicantId, smsId);
                        //new SMSDAL().AddUpdateSmsProcess(objProcess);
                        string query = "insert into tblOtps values (" + applicantId + ",'" + Number + "'," + randomNumber + ",getdate(),0)";
                        SqlConnection connection = new SqlConnection(PrpDbConnectADO.Conn);
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        result = sRandomOTP;
                    }
                    catch (Exception)
                    {
                    }
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

     

        [ValidateInput(false)]
        public ActionResult SaveConsentData(ConsentModel ModelSave, IEnumerable<HttpPostedFileBase> flImages)
        {
            Consent modelSave = ModelSave.consent;
            modelSave.inductionId = ProjConstant.inductionId;
            modelSave.phaseId = ProjConstant.phaseId;
            modelSave.consentId = modelSave.consentId.TooInt();
            modelSave.applicantId = base.loggedInUser.applicantId;
            modelSave.typeId = modelSave.typeId.TooInt();
            modelSave.consentTypeId = modelSave.consentTypeId.TooInt();
            modelSave.roundId = ProjConstant.consentRound;


            string imageName = "";
            try
            {
                if ((flImages == null || flImages.Count<HttpPostedFileBase>() <= 0 ? false : flImages.ToList<HttpPostedFileBase>()[0] != null))
                {
                    imageName = Files.SaveFile(flImages.ToList<HttpPostedFileBase>()[0], "/images/Applicant/" + modelSave.applicantId + "/", 0, "undertaking_consent_round_" + modelSave.roundId + "_" + (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds + "" + System.IO.Path.GetExtension(flImages.ToList<HttpPostedFileBase>()[0].FileName) + "");
                }
            }
            catch (Exception ex)
            {
                imageName = "";
            }

            modelSave.mobileNumber = ModelSave.mobileNumber.TooString();
            modelSave.img = imageName.TooString();
            modelSave.otp = ModelSave.otpCode.TooString();

            (new ConsentDAL()).AddUpdate(modelSave);
            ActionResult actionResult = this.Redirect(string.Concat("/", ProjConstant.preUrl, "/applicant/consent"));
            return actionResult;
        }
    }
}