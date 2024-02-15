using Newtonsoft.Json;
using prp.fn;
using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class ApplicantActionController : BaseAdminController
    {
        public ApplicantActionController()
        {
        }

        public ActionResult ActionLisiting()
        {
            ApplicantActionAdminModel applicantActionAdminModel = new ApplicantActionAdminModel()
            {
                listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant)
            };
            string absolutePath = Request.Url.AbsolutePath;
            if (absolutePath.Contains("reg-term"))
            {
                applicantActionAdminModel.typeId = 0;
            }
            else if (absolutePath.Contains("freez"))
            {
                applicantActionAdminModel.typeId = ProjConstant.Constant.ActionStatusType.freez;
                applicantActionAdminModel.heading = "Freez";
            }
            return View(applicantActionAdminModel);
        }

        [HttpPost]
        public ActionResult ActionSearch(SearchReport obj)
        {
            obj.adminId = base.loggedInUser.userId;
            obj.search = obj.search.TooString("");
            DataTable dataTable = (new ReportDAL()).ActionSearch(obj);
            string str = JsonConvert.SerializeObject(dataTable);
            return base.Content(str, "application/json");
        }

        public ActionResult ActionSetup()
        {
            ApplicantActionAdminModel applicantActionAdminModel = new ApplicantActionAdminModel()
            {
                listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant)
            };
            string lower = Request.Url.AbsolutePath.ToLower();
            if (lower.Contains("reg-term"))
            {
                applicantActionAdminModel.typeId = 0;
                applicantActionAdminModel.heading = "Resignation/Termination";
            }
            else if (lower.Contains("freez"))
            {
                applicantActionAdminModel.typeId = ProjConstant.Constant.ActionStatusType.freez;
                applicantActionAdminModel.heading = "Freez";
            }
            applicantActionAdminModel.applicantId = Request.QueryString["applicantId"].TooInt();
            if (applicantActionAdminModel.applicantId > 0)
            {
                applicantActionAdminModel.joinApplicant = (new JoiningDAL()).GetJoinedApplicantDetailById(applicantActionAdminModel.applicantId);
                if (applicantActionAdminModel.joinApplicant.applicantId > 0)
                {
                    applicantActionAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, applicantActionAdminModel.applicantId);
                    applicantActionAdminModel.applicantInfo = (new ApplicantDAL()).GetApplicantInfo(0, 0, applicantActionAdminModel.applicantId);
                    applicantActionAdminModel.action = (new ActionDAL()).GetById(applicantActionAdminModel.applicantId, applicantActionAdminModel.typeId);
                }
            }
            return View(applicantActionAdminModel);
        }

        [HttpPost]
        public JsonResult ActionSetupSave(ApplicantAction ac)
        {
            ac.startDate = DateTime.Now;
            ac.endDate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(ac.dateStart))
            {
                ac.startDate = ac.dateStart.TooDate();
            }
            if (!string.IsNullOrWhiteSpace(ac.dateEnd))
            {
                ac.endDate = ac.dateEnd.TooDate();
            }
            ac.adminId = base.loggedInUser.userId;
            Message message = (new ActionDAL()).AddUpdate(ac);
            return base.Json(message, 0);
        }

        [HttpPost]
        public JsonResult ActionUpdateStatus(ApplicantAction ac)
        {
            ac.image = ac.image.TooString("");
            ac.adminId = base.loggedInUser.userId;
            Message message = (new ActionDAL()).AddUpdateStatus(ac);
            return base.Json(message, 0);
        }

        public ActionResult ExtensionApprovalLisiting()
        {
            ApplicantActionAdminModel applicantActionAdminModel = new ApplicantActionAdminModel()
            {
                listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant)
            };
            string absolutePath = Request.Url.AbsolutePath;
            applicantActionAdminModel.typeId = 0;
            applicantActionAdminModel.instituteId = base.loggedInUser.reffId;
            return View(applicantActionAdminModel);
        }

        [HttpPost]
        public ActionResult ExtensionApprovalSearch(SearchReport obj)
        {
            obj.adminId = base.loggedInUser.userId;
            obj.search = obj.search.TooString("");
            DataTable dataTable = (new ReportDAL()).ExtensionApprovalSearch(obj);
            string str = JsonConvert.SerializeObject(dataTable);
            return base.Content(str, "application/json");
        }

        public ActionResult ExtensionApprovalSetup()
        {
            ApplicantActionAdminModel applicantActionAdminModel = new ApplicantActionAdminModel()
            {
                listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant)
            };
            Request.Url.AbsolutePath.ToLower();
            applicantActionAdminModel.typeId = 0;
            applicantActionAdminModel.heading = "Extension Approval";
            applicantActionAdminModel.applicantId = Request.QueryString["applicantId"].TooInt();
            applicantActionAdminModel.applicantLeaveId = Request.QueryString["applicantLeaveId"].TooInt();
            if (applicantActionAdminModel.applicantId > 0)
            {
                applicantActionAdminModel.joinApplicant = (new JoiningDAL()).GetJoinedApplicantDetailById(applicantActionAdminModel.applicantId);
                if (applicantActionAdminModel.applicantId > 0)
                {
                    applicantActionAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, applicantActionAdminModel.applicantId);
                    applicantActionAdminModel.applicantInfo = (new ApplicantDAL()).GetApplicantInfo(0, 0, applicantActionAdminModel.applicantId);
                    applicantActionAdminModel.action = (new ActionDAL()).GetById(applicantActionAdminModel.applicantId, applicantActionAdminModel.typeId);
                    ApplicantExtensionAction extensionData = (new ActionDAL()).getExtensionData(applicantActionAdminModel.applicantId, applicantActionAdminModel.applicantLeaveId);
                    applicantActionAdminModel.extensionDataList = (new ActionDAL()).getExtensionDataList(applicantActionAdminModel.applicantId);
                    applicantActionAdminModel.extensionData = extensionData;
                }
            }
            applicantActionAdminModel.typeId = 0;
            if ((applicantActionAdminModel.applicantId <= 0 ? false : applicantActionAdminModel.joinApplicant.applicantId == 0))
            {
                applicantActionAdminModel.joinApplicant.applicantId = applicantActionAdminModel.applicantId;
                applicantActionAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, applicantActionAdminModel.applicantId);
            }
            if (applicantActionAdminModel.joinApplicant.joiningDate < DateTime.Now.AddYears(-10))
            {
                int num = applicantActionAdminModel.applicantId;
                string str = string.Concat("select joiningDate, speciality, hospital, program, name,pmdcNo,emailId,contactNumber from tblTraineeInfo ti inner join tblApplicant a on ti.applicantId = a.applicantId where ti.applicantId = ", num.ToString());
                SqlConnection sqlConnection = new SqlConnection();
                DataTable dataTable = new DataTable();
                Message message = new Message();
                SqlCommand sqlCommand = new SqlCommand(str);
                try
                {
                    try
                    {
                        sqlConnection = new SqlConnection(PrpDbConnectADO.Conn);
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        (new SqlDataAdapter(sqlCommand)).Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            applicantActionAdminModel.joinApplicant.joiningDate = Convert.ToDateTime(dataTable.Rows[0][0]);
                            applicantActionAdminModel.joinApplicant.specialityName = dataTable.Rows[0][1].TooString("");
                            applicantActionAdminModel.joinApplicant.hospitalName = dataTable.Rows[0][2].TooString("");
                            applicantActionAdminModel.joinApplicant.typeName = dataTable.Rows[0][3].TooString("");
                            applicantActionAdminModel.joinApplicant.name = dataTable.Rows[0][4].TooString("");
                            applicantActionAdminModel.applicant.name = dataTable.Rows[0][4].TooString("");
                            applicantActionAdminModel.joinApplicant.pmdcNo = dataTable.Rows[0][5].TooString("");
                            applicantActionAdminModel.applicant.pmdcNo = dataTable.Rows[0][5].TooString("");
                            applicantActionAdminModel.joinApplicant.emailId = dataTable.Rows[0][6].TooString("");
                            applicantActionAdminModel.applicant.emailId = dataTable.Rows[0][6].TooString("");
                            applicantActionAdminModel.joinApplicant.contactNumber = dataTable.Rows[0][7].TooString("");
                            applicantActionAdminModel.applicant.contactNumber = dataTable.Rows[0][7].TooString("");
                        }
                        message.status = true;
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        message.status = false;
                        message.msg = exception.Message;
                    }
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            ViewData["LoggedInUserId"] = loggedInUser.userId;
            return View(applicantActionAdminModel);
        }

        [HttpPost]
        public JsonResult ExtensionApprovalSetupSave(ApplicantExtensionAction ac)
        {
            int num;
            DateTime now;
            Message message = new Message();
            if (ac.actionId <= 1)
            {
                if (base.loggedInUser.userId == 48)
                {
                    string str = ac.remarks.TooString("");
                    num = ac.applicantExtensionId;
                    string str1 = string.Concat("update tblApplicantExtension set  remarksByDS = '", str, "'  where applicantExtensionId = ", num.ToString());
                    string str2 = string.Concat("", "insert into tblApplicantExtensionCorrespondence (");
                    str2 = string.Concat(str2, "  applicantExtensionId, remarks");
                    str2 = string.Concat(str2, ", applicantId, actionPerformed, adminId, dated)");
                    str2 = string.Concat(str2, " values (");
                    string[] strArrays = new string[] { str2, null, null, null, null };
                    num = ac.applicantExtensionId;
                    strArrays[1] = num.ToString();
                    strArrays[2] = ", '";
                    strArrays[3] = ac.remarks;
                    strArrays[4] = "'";
                    str2 = string.Concat(strArrays);
                    string[] strArrays1 = new string[] { str2, " , ", null, null, null, null, null, null, null, null };
                    num = ac.applicantId;
                    strArrays1[2] = num.ToString();
                    strArrays1[3] = ", ";
                    num = ac.actionId;
                    strArrays1[4] = num.ToString();
                    strArrays1[5] = ", ";
                    num = base.loggedInUser.userId;
                    strArrays1[6] = num.ToString();
                    strArrays1[7] = ", '";
                    now = DateTime.Now;
                    strArrays1[8] = now.ToString();
                    strArrays1[9] = "' ";
                    str2 = string.Concat(string.Concat(strArrays1), " )");
                    SqlConnection sqlConnection = new SqlConnection();
                    DataTable dataTable = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand(str1);
                    SqlCommand sqlCommand1 = new SqlCommand(str2);
                    try
                    {
                        try
                        {
                            sqlConnection = new SqlConnection(PrpDbConnectADO.Conn);
                            sqlConnection.Open();
                            sqlCommand.Connection = sqlConnection;
                            sqlCommand.ExecuteNonQuery();
                            sqlCommand1.Connection = sqlConnection;
                            sqlCommand1.ExecuteNonQuery();
                            message.status = true;
                        }
                        catch (Exception exception1)
                        {
                            Exception exception = exception1;
                            message.status = false;
                            message.msg = exception.Message;
                        }
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
                else if (base.loggedInUser.userId == 151)
                {
                    string str3 = ac.remarks.TooString("");
                    num = ac.applicantExtensionId;
                    string str4 = string.Concat("update tblApplicantExtension set  remarksByAST = '", str3, "' where applicantExtensionId = ", num.ToString());
                    string str5 = string.Concat("", "insert into tblApplicantExtensionCorrespondence (");
                    str5 = string.Concat(str5, "  applicantExtensionId, remarks");
                    str5 = string.Concat(str5, ", applicantId, actionPerformed, adminId, dated)");
                    str5 = string.Concat(str5, " values (");
                    string[] strArrays2 = new string[] { str5, null, null, null, null };
                    num = ac.applicantExtensionId;
                    strArrays2[1] = num.ToString();
                    strArrays2[2] = ", '";
                    strArrays2[3] = ac.remarks;
                    strArrays2[4] = "'";
                    str5 = string.Concat(strArrays2);
                    string[] strArrays3 = new string[] { str5, " , ", null, null, null, null, null, null, null, null };
                    num = ac.applicantId;
                    strArrays3[2] = num.ToString();
                    strArrays3[3] = ", ";
                    num = ac.actionId;
                    strArrays3[4] = num.ToString();
                    strArrays3[5] = ", ";
                    num = base.loggedInUser.userId;
                    strArrays3[6] = num.ToString();
                    strArrays3[7] = ", '";
                    now = DateTime.Now;
                    strArrays3[8] = now.ToString();
                    strArrays3[9] = "' ";
                    str5 = string.Concat(string.Concat(strArrays3), " )");
                    SqlConnection sqlConnection1 = new SqlConnection();
                    DataTable dataTable1 = new DataTable();
                    SqlCommand sqlCommand2 = new SqlCommand(str4);
                    SqlCommand sqlCommand3 = new SqlCommand(str5);
                    try
                    {
                        try
                        {
                            sqlConnection1 = new SqlConnection(PrpDbConnectADO.Conn);
                            sqlConnection1.Open();
                            sqlCommand2.Connection = sqlConnection1;
                            sqlCommand2.ExecuteNonQuery();
                            sqlCommand3.Connection = sqlConnection1;
                            sqlCommand3.ExecuteNonQuery();
                            message.status = true;
                        }
                        catch (Exception exception3)
                        {
                            Exception exception2 = exception3;
                            message.status = false;
                            message.msg = exception2.Message;
                        }
                    }
                    finally
                    {
                        sqlConnection1.Close();
                    }
                }
                else if (base.loggedInUser.userId == 152)
                {
                    string str6 = ac.remarks.TooString("");
                    num = ac.applicantExtensionId;
                    string str7 = string.Concat("update tblApplicantExtension set  remarksBySec = '", str6, "'  where applicantExtensionId = ", num.ToString());
                    string str8 = string.Concat("", "insert into tblApplicantExtensionCorrespondence (");
                    str8 = string.Concat(str8, "  applicantExtensionId, remarks");
                    str8 = string.Concat(str8, ", applicantId, actionPerformed, adminId, dated)");
                    str8 = string.Concat(str8, " values (");
                    string[] strArrays4 = new string[] { str8, null, null, null, null };
                    num = ac.applicantExtensionId;
                    strArrays4[1] = num.ToString();
                    strArrays4[2] = ", '";
                    strArrays4[3] = ac.remarks;
                    strArrays4[4] = "'";
                    str8 = string.Concat(strArrays4);
                    string[] strArrays5 = new string[] { str8, " , ", null, null, null, null, null, null, null, null };
                    num = ac.applicantId;
                    strArrays5[2] = num.ToString();
                    strArrays5[3] = ", ";
                    num = ac.actionId;
                    strArrays5[4] = num.ToString();
                    strArrays5[5] = ", ";
                    num = base.loggedInUser.userId;
                    strArrays5[6] = num.ToString();
                    strArrays5[7] = ", '";
                    now = DateTime.Now;
                    strArrays5[8] = now.ToString();
                    strArrays5[9] = "' ";
                    str8 = string.Concat(string.Concat(strArrays5), " )");
                    SqlConnection sqlConnection2 = new SqlConnection();
                    DataTable dataTable2 = new DataTable();
                    SqlCommand sqlCommand4 = new SqlCommand(str7);
                    SqlCommand sqlCommand5 = new SqlCommand(str8);
                    try
                    {
                        try
                        {
                            sqlConnection2 = new SqlConnection(PrpDbConnectADO.Conn);
                            sqlConnection2.Open();
                            sqlCommand4.Connection = sqlConnection2;
                            sqlCommand4.ExecuteNonQuery();
                            sqlCommand5.Connection = sqlConnection2;
                            sqlCommand5.ExecuteNonQuery();
                            message.status = true;
                        }
                        catch (Exception exception5)
                        {
                            Exception exception4 = exception5;
                            message.status = false;
                            message.msg = exception4.Message;
                        }
                    }
                    finally
                    {
                        sqlConnection2.Close();
                    }
                }
                ac.adminId = base.loggedInUser.userId;
                message = (new ActionDAL()).AddUpdateExtensionApproval(ac);
            }
            else if (base.loggedInUser.userId == 150)
            {
                string[] strArrays6 = new string[] { "update tblApplicantExtension set imageApplicationValidity = ", null, null, null, null };
                num = ac.imageApplicationValidity.TooInt();
                strArrays6[1] = num.ToString();
                strArrays6[2] = ", imageApplicationRemarks = '";
                strArrays6[3] = ac.imageApplicationRemarks.TooString("");
                strArrays6[4] = "' ";
                string str9 = string.Concat(strArrays6);
                string[] strArrays7 = new string[] { str9, " ,imagePERValidity = ", null, null, null, null };
                num = ac.imagePERValidity.TooInt();
                strArrays7[2] = num.ToString();
                strArrays7[3] = ", imagePERRemarks = '";
                strArrays7[4] = ac.imagePERRemarks.TooString("");
                strArrays7[5] = "' ";
                str9 = string.Concat(strArrays7);
                string[] strArrays8 = new string[] { str9, " ,imageNOCValidity = ", null, null, null, null };
                num = ac.imageNOCValidity.TooInt();
                strArrays8[2] = num.ToString();
                strArrays8[3] = ", imageNOCRemarks = '";
                strArrays8[4] = ac.imageNOCRemarks.TooString("");
                strArrays8[5] = "' ";
                str9 = string.Concat(strArrays8);
                string[] strArrays9 = new string[] { str9, " ,imagePMDCValidity = ", null, null, null, null };
                num = ac.imagePMDCValidity.TooInt();
                strArrays9[2] = num.ToString();
                strArrays9[3] = ", imagePMDCRemarks = '";
                strArrays9[4] = ac.imagePMDCRemarks.TooString("");
                strArrays9[5] = "' ";
                str9 = string.Concat(strArrays9);
                string[] str10 = new string[] { str9, " ,imageExtensionOrderValidity = ", null, null, null, null };
                num = ac.imageExtensionOrderValidity.TooInt();
                str10[2] = num.ToString();
                str10[3] = ", imageExtensionOrderRemarks = '";
                str10[4] = ac.imageExtensionOrderRemarks.TooString("");
                str10[5] = "' ";
                str9 = string.Concat(str10);
                string[] strArrays10 = new string[] { str9, " ,imageJoiningOrderValidity = ", null, null, null, null };
                num = ac.imageJoiningOrderValidity.TooInt();
                strArrays10[2] = num.ToString();
                strArrays10[3] = ", imageJoiningOrderRemarks = '";
                strArrays10[4] = ac.imageJoiningOrderRemarks.TooString("");
                strArrays10[5] = "' ";
                str9 = string.Concat(strArrays10);
                string[] str11 = new string[] { str9, " ,imageDoc1Validity = ", null, null, null, null };
                num = ac.imageDoc1Validity.TooInt();
                str11[2] = num.ToString();
                str11[3] = ", imageDoc1Remarks = '";
                str11[4] = ac.imageDoc1Remarks.TooString("");
                str11[5] = "' ";
                str9 = string.Concat(str11);
                string[] strArrays11 = new string[] { str9, " ,imageDoc2Validity = ", null, null, null, null };
                num = ac.imageDoc2Validity.TooInt();
                strArrays11[2] = num.ToString();
                strArrays11[3] = ", imageDoc2Remarks = '";
                strArrays11[4] = ac.imageDoc2Remarks.TooString("");
                strArrays11[5] = "' ";
                str9 = string.Concat(strArrays11);
                string[] str12 = new string[] { str9, " ,imageInductionOrderValidity = ", null, null, null, null };
                num = ac.imageInductionOrderValidity.TooInt();
                str12[2] = num.ToString();
                str12[3] = ", imageInductionOrderRemarks = '";
                str12[4] = ac.imageInductionOrderRemarks.TooString("");
                str12[5] = "' ";
                str9 = string.Concat(str12);
                string[] strArrays12 = new string[] { str9, " ,imageTothcValidity = ", null, null, null, null };
                num = ac.imageTothcValidity.TooInt();
                strArrays12[2] = num.ToString();
                strArrays12[3] = ", imageTothcRemarks = '";
                strArrays12[4] = ac.imageTothcRemarks.TooString("");
                strArrays12[5] = "' ";
                str9 = string.Concat(strArrays12);
                string[] str13 = new string[] { str9, " ,imageJoatValidity = ", null, null, null, null };
                num = ac.imageJoatValidity.TooInt();
                str13[2] = num.ToString();
                str13[3] = ", imageJoatRemarks = '";
                str13[4] = ac.imageJoatRemarks.TooString("");
                str13[5] = "' ";
                str9 = string.Concat(str13);
                str9 = string.Concat(str9, " ,remarksBySO = '", ac.remarks.TooString(""), "' ");
                num = ac.applicantExtensionId;
                str9 = string.Concat(str9, ", forwardedTo = 48 where applicantExtensionId = ", num.ToString());
                string str14 = string.Concat("", "insert into tblApplicantExtensionCorrespondence (");
                str14 = string.Concat(str14, "  applicantExtensionId, remarks");
                str14 = string.Concat(str14, ", imageApplicationValidity, imageDoc1Validity, imageDoc2Validity, imagePERValidity");
                str14 = string.Concat(str14, ", imageNOCValidity, imagePMDCValidity, imageExtensionOrderValidity, imageJoiningOrderValidity, imageInductionOrderValidity");
                str14 = string.Concat(str14, ", rtmcUhsNoValidity, imageTothcValidity, imageJoatValidity");
                str14 = string.Concat(str14, ", imageApplicationRemarks, imageDoc1Remarks, imageDoc2Remarks, imagePERRemarks");
                str14 = string.Concat(str14, ", imageNOCRemarks, imagePMDCRemarks, imageExtensionOrderRemarks, imageJoiningOrderRemarks, imageInductionOrderRemarks");
                str14 = string.Concat(str14, ", rtmcUhsNoRemarks, imageTothcRemarks, imageJoatRemarks");
                str14 = string.Concat(str14, ", applicantId, actionPerformed, adminId, dated)");
                str14 = string.Concat(str14, " values (");
                string[] strArrays13 = new string[] { str14, null, null, null, null };
                num = ac.applicantExtensionId;
                strArrays13[1] = num.ToString();
                strArrays13[2] = ", '";
                strArrays13[3] = ac.remarks;
                strArrays13[4] = "'";
                str14 = string.Concat(strArrays13);
                string[] strArrays14 = new string[] { str14, ", ", null, null, null, null, null, null, null };
                num = ac.imageApplicationValidity.TooInt();
                strArrays14[2] = num.ToString();
                strArrays14[3] = ", ";
                num = ac.imageDoc1Validity.TooInt();
                strArrays14[4] = num.ToString();
                strArrays14[5] = ", ";
                num = ac.imageDoc2Validity.TooInt();
                strArrays14[6] = num.ToString();
                strArrays14[7] = ", ";
                num = ac.imagePERValidity.TooInt();
                strArrays14[8] = num.ToString();
                str14 = string.Concat(strArrays14);
                string[] str15 = new string[] { str14, ", ", null, null, null, null, null, null, null, null, null };
                num = ac.imageNOCValidity.TooInt();
                str15[2] = num.ToString();
                str15[3] = ", ";
                num = ac.imagePMDCValidity.TooInt();
                str15[4] = num.ToString();
                str15[5] = ", ";
                num = ac.imageExtensionOrderValidity.TooInt();
                str15[6] = num.ToString();
                str15[7] = ", ";
                num = ac.imageJoiningOrderValidity.TooInt();
                str15[8] = num.ToString();
                str15[9] = ", ";
                num = ac.imageInductionOrderValidity.TooInt();
                str15[10] = num.ToString();
                str14 = string.Concat(str15);
                string[] strArrays15 = new string[] { str14, ", ", null, null, null, null, null };
                num = ac.rtmcUhsNoValidity.TooInt();
                strArrays15[2] = num.ToString();
                strArrays15[3] = ", ";
                num = ac.imageTothcValidity.TooInt();
                strArrays15[4] = num.ToString();
                strArrays15[5] = ", ";
                num = ac.imageJoatValidity.TooInt();
                strArrays15[6] = num.ToString();
                str14 = string.Concat(strArrays15);
                str14 = string.Concat(new string[] { str14, ", '", ac.imageApplicationRemarks.TooString(""), "', '", ac.imageDoc1Remarks.TooString(""), "', '", ac.imageDoc2Remarks.TooString(""), "', '", ac.imagePERRemarks.TooString(""), "'" });
                str14 = string.Concat(new string[] { str14, ", '", ac.imageNOCRemarks.TooString(""), "', '", ac.imagePMDCRemarks.TooString(""), "', '", ac.imageExtensionOrderRemarks.TooString(""), "', '", ac.imageJoiningOrderRemarks.TooString(""), "', '", ac.imageInductionOrderRemarks.TooString(""), "'" });
                str14 = string.Concat(new string[] { str14, ", '", ac.rtmcUhsNoRemarks.TooString(""), "', '", ac.imageTothcRemarks.TooString(""), "', '", ac.imageJoatRemarks.TooString(""), "'" });
                string[] str16 = new string[] { str14, " , ", null, null, null, null, null, null, null, null };
                num = ac.applicantId;
                str16[2] = num.ToString();
                str16[3] = ", ";
                num = ac.actionId;
                str16[4] = num.ToString();
                str16[5] = ", ";
                num = base.loggedInUser.userId;
                str16[6] = num.ToString();
                str16[7] = ", '";
                now = DateTime.Now;
                str16[8] = now.ToString();
                str16[9] = "' ";
                str14 = string.Concat(string.Concat(str16), " )");
                SqlConnection sqlConnection3 = new SqlConnection();
                DataTable dataTable3 = new DataTable();
                SqlCommand sqlCommand6 = new SqlCommand(str9);
                SqlCommand sqlCommand7 = new SqlCommand(str14);
                try
                {
                    try
                    {
                        sqlConnection3 = new SqlConnection(PrpDbConnectADO.Conn);
                        sqlConnection3.Open();
                        sqlCommand6.Connection = sqlConnection3;
                        sqlCommand6.ExecuteNonQuery();
                        sqlCommand7.Connection = sqlConnection3;
                        sqlCommand7.ExecuteNonQuery();
                        message.status = true;
                    }
                    catch (Exception exception7)
                    {
                        Exception exception6 = exception7;
                        message.status = false;
                        message.msg = exception6.Message;
                    }
                }
                finally
                {
                    sqlConnection3.Close();
                }
            }
            else if (base.loggedInUser.userId == 48)
            {
                string str17 = string.Concat("update tblApplicantExtension set  remarksByDS = '", ac.remarks.TooString(""), "' ");
                string[] strArrays16 = new string[] { str17, ", forwardedTo = ", null, null, null };
                num = ac.actionId;
                strArrays16[2] = num.ToString();
                strArrays16[3] = " where applicantExtensionId = ";
                num = ac.applicantExtensionId;
                strArrays16[4] = num.ToString();
                str17 = string.Concat(strArrays16);
                string str18 = string.Concat("", "insert into tblApplicantExtensionCorrespondence (");
                str18 = string.Concat(str18, "  applicantExtensionId, remarks");
                str18 = string.Concat(str18, ", applicantId, actionPerformed, adminId, dated)");
                str18 = string.Concat(str18, " values (");
                string[] strArrays17 = new string[] { str18, null, null, null, null };
                num = ac.applicantExtensionId;
                strArrays17[1] = num.ToString();
                strArrays17[2] = ", '";
                strArrays17[3] = ac.remarks;
                strArrays17[4] = "'";
                str18 = string.Concat(strArrays17);
                string[] strArrays18 = new string[] { str18, " , ", null, null, null, null, null, null, null, null };
                num = ac.applicantId;
                strArrays18[2] = num.ToString();
                strArrays18[3] = ", ";
                num = ac.actionId;
                strArrays18[4] = num.ToString();
                strArrays18[5] = ", ";
                num = base.loggedInUser.userId;
                strArrays18[6] = num.ToString();
                strArrays18[7] = ", '";
                now = DateTime.Now;
                strArrays18[8] = now.ToString();
                strArrays18[9] = "' ";
                str18 = string.Concat(string.Concat(strArrays18), " )");
                SqlConnection sqlConnection4 = new SqlConnection();
                DataTable dataTable4 = new DataTable();
                SqlCommand sqlCommand8 = new SqlCommand(str17);
                SqlCommand sqlCommand9 = new SqlCommand(str18);
                try
                {
                    try
                    {
                        sqlConnection4 = new SqlConnection(PrpDbConnectADO.Conn);
                        sqlConnection4.Open();
                        sqlCommand8.Connection = sqlConnection4;
                        sqlCommand8.ExecuteNonQuery();
                        sqlCommand9.Connection = sqlConnection4;
                        sqlCommand9.ExecuteNonQuery();
                        message.status = true;
                    }
                    catch (Exception exception9)
                    {
                        Exception exception8 = exception9;
                        message.status = false;
                        message.msg = exception8.Message;
                    }
                }
                finally
                {
                    sqlConnection4.Close();
                }
            }
            else if (base.loggedInUser.userId == 151)
            {
                string str19 = string.Concat("update tblApplicantExtension set  remarksByAST = '", ac.remarks.TooString(""), "' ");
                string[] strArrays19 = new string[] { str19, ", forwardedTo = ", null, null, null };
                num = ac.actionId;
                strArrays19[2] = num.ToString();
                strArrays19[3] = " where applicantExtensionId = ";
                num = ac.applicantExtensionId;
                strArrays19[4] = num.ToString();
                str19 = string.Concat(strArrays19);
                string str20 = string.Concat("", "insert into tblApplicantExtensionCorrespondence (");
                str20 = string.Concat(str20, "  applicantExtensionId, remarks");
                str20 = string.Concat(str20, ", applicantId, actionPerformed, adminId, dated)");
                str20 = string.Concat(str20, " values (");
                string[] strArrays20 = new string[] { str20, null, null, null, null };
                num = ac.applicantExtensionId;
                strArrays20[1] = num.ToString();
                strArrays20[2] = ", '";
                strArrays20[3] = ac.remarks;
                strArrays20[4] = "'";
                str20 = string.Concat(strArrays20);
                string[] str21 = new string[] { str20, " , ", null, null, null, null, null, null, null, null };
                num = ac.applicantId;
                str21[2] = num.ToString();
                str21[3] = ", ";
                num = ac.actionId;
                str21[4] = num.ToString();
                str21[5] = ", ";
                num = base.loggedInUser.userId;
                str21[6] = num.ToString();
                str21[7] = ", '";
                now = DateTime.Now;
                str21[8] = now.ToString();
                str21[9] = "' ";
                str20 = string.Concat(string.Concat(str21), " )");
                SqlConnection sqlConnection5 = new SqlConnection();
                DataTable dataTable5 = new DataTable();
                SqlCommand sqlCommand10 = new SqlCommand(str19);
                SqlCommand sqlCommand11 = new SqlCommand(str20);
                try
                {
                    try
                    {
                        sqlConnection5 = new SqlConnection(PrpDbConnectADO.Conn);
                        sqlConnection5.Open();
                        sqlCommand10.Connection = sqlConnection5;
                        sqlCommand10.ExecuteNonQuery();
                        sqlCommand11.Connection = sqlConnection5;
                        sqlCommand11.ExecuteNonQuery();
                        message.status = true;
                    }
                    catch (Exception exception11)
                    {
                        Exception exception10 = exception11;
                        message.status = false;
                        message.msg = exception10.Message;
                    }
                }
                finally
                {
                    sqlConnection5.Close();
                }
            }
            else if (base.loggedInUser.userId == 152)
            {
                string str22 = string.Concat("update tblApplicantExtension set  remarksBySec = '", ac.remarks.TooString(""), "' ");
                string[] strArrays21 = new string[] { str22, ", forwardedTo = ", null, null, null };
                num = ac.actionId;
                strArrays21[2] = num.ToString();
                strArrays21[3] = " where applicantExtensionId = ";
                num = ac.applicantExtensionId;
                strArrays21[4] = num.ToString();
                str22 = string.Concat(strArrays21);
                string str23 = string.Concat("", "insert into tblApplicantExtensionCorrespondence (");
                str23 = string.Concat(str23, "  applicantExtensionId, remarks");
                str23 = string.Concat(str23, ", applicantId, actionPerformed, adminId, dated)");
                str23 = string.Concat(str23, " values (");
                string[] strArrays22 = new string[] { str23, null, null, null, null };
                num = ac.applicantExtensionId;
                strArrays22[1] = num.ToString();
                strArrays22[2] = ", '";
                strArrays22[3] = ac.remarks;
                strArrays22[4] = "'";
                str23 = string.Concat(strArrays22);
                string[] strArrays23 = new string[] { str23, " , ", null, null, null, null, null, null, null, null };
                num = ac.applicantId;
                strArrays23[2] = num.ToString();
                strArrays23[3] = ", ";
                num = ac.actionId;
                strArrays23[4] = num.ToString();
                strArrays23[5] = ", ";
                num = base.loggedInUser.userId;
                strArrays23[6] = num.ToString();
                strArrays23[7] = ", '";
                now = DateTime.Now;
                strArrays23[8] = now.ToString();
                strArrays23[9] = "' ";
                str23 = string.Concat(string.Concat(strArrays23), " )");
                SqlConnection sqlConnection6 = new SqlConnection();
                DataTable dataTable6 = new DataTable();
                SqlCommand sqlCommand12 = new SqlCommand(str22);
                SqlCommand sqlCommand13 = new SqlCommand(str23);
                try
                {
                    try
                    {
                        sqlConnection6 = new SqlConnection(PrpDbConnectADO.Conn);
                        sqlConnection6.Open();
                        sqlCommand12.Connection = sqlConnection6;
                        sqlCommand12.ExecuteNonQuery();
                        sqlCommand13.Connection = sqlConnection6;
                        sqlCommand13.ExecuteNonQuery();
                        message.status = true;
                    }
                    catch (Exception exception13)
                    {
                        Exception exception12 = exception13;
                        message.status = false;
                        message.msg = exception12.Message;
                    }
                }
                finally
                {
                    sqlConnection6.Close();
                }
            }
            return base.Json(message, 0);
        }

        [HttpPost]
        public JsonResult IssueOrder(ApplicantExtensionAction ac)
        {
            Message message = new Message();
            string sqlQuery = "update tblApplicantExtension set noOfDays = 1 where applicantExtensionId = " + ac.applicantExtensionId + "";
            SqlConnection con = new SqlConnection();
            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                message.status = true;
            }
            catch (Exception ex)
            {
                message.status = false;
            }
            finally
            {
                con.Close();
            }
            return base.Json(message, 0);
        }

        public ActionResult ExtensionSetup()
        {
            ApplicantActionAdminModel applicantActionAdminModel = new ApplicantActionAdminModel()
            {
                listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant)
            };
            Request.Url.AbsolutePath.ToLower();
            applicantActionAdminModel.typeId = 0;
            applicantActionAdminModel.heading = "Extension Application";
            applicantActionAdminModel.applicantId = Request.QueryString["applicantId"].TooInt();
            if (applicantActionAdminModel.applicantId > 0)
            {
                applicantActionAdminModel.joinApplicant = (new JoiningDAL()).GetJoinedApplicantDetailById(applicantActionAdminModel.applicantId);
                applicantActionAdminModel.extensionDataList = (new ActionDAL()).getExtensionDataList(applicantActionAdminModel.applicantId);
                if (applicantActionAdminModel.joinApplicant.applicantId > 0)
                {
                    applicantActionAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, applicantActionAdminModel.applicantId);
                    applicantActionAdminModel.applicantInfo = (new ApplicantDAL()).GetApplicantInfo(0, 0, applicantActionAdminModel.applicantId);
                    applicantActionAdminModel.action = (new ActionDAL()).GetById(applicantActionAdminModel.applicantId, applicantActionAdminModel.typeId);
                }
            }
            applicantActionAdminModel.typeId = 0;
            if ((applicantActionAdminModel.applicantId <= 0 ? false : applicantActionAdminModel.joinApplicant.applicantId == 0))
            {
                applicantActionAdminModel.joinApplicant.applicantId = applicantActionAdminModel.applicantId;
                applicantActionAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, applicantActionAdminModel.applicantId);
            }
            if (applicantActionAdminModel.joinApplicant.joiningDate < DateTime.Now.AddYears(-10))
            {
                int num = applicantActionAdminModel.applicantId;
                string str = string.Concat("select joiningDate, speciality, hospital, program, name,pmdcNo,emailId,contactNumber from tblTraineeInfo ti inner join tblApplicant a on ti.applicantId = a.applicantId where ti.applicantId = ", num.ToString());
                SqlConnection sqlConnection = new SqlConnection();
                DataTable dataTable = new DataTable();
                Message message = new Message();
                SqlCommand sqlCommand = new SqlCommand(str);
                try
                {
                    try
                    {
                        sqlConnection = new SqlConnection(PrpDbConnectADO.Conn);
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        (new SqlDataAdapter(sqlCommand)).Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            applicantActionAdminModel.joinApplicant.joiningDate = Convert.ToDateTime(dataTable.Rows[0][0]);
                            applicantActionAdminModel.joinApplicant.specialityName = dataTable.Rows[0][1].TooString("");
                            applicantActionAdminModel.joinApplicant.hospitalName = dataTable.Rows[0][2].TooString("");
                            applicantActionAdminModel.joinApplicant.typeName = dataTable.Rows[0][3].TooString("");
                            applicantActionAdminModel.joinApplicant.name = dataTable.Rows[0][4].TooString("");
                            applicantActionAdminModel.applicant.name = dataTable.Rows[0][4].TooString("");
                            applicantActionAdminModel.joinApplicant.pmdcNo = dataTable.Rows[0][5].TooString("");
                            applicantActionAdminModel.applicant.pmdcNo = dataTable.Rows[0][5].TooString("");
                            applicantActionAdminModel.joinApplicant.emailId = dataTable.Rows[0][6].TooString("");
                            applicantActionAdminModel.applicant.emailId = dataTable.Rows[0][6].TooString("");
                            applicantActionAdminModel.joinApplicant.contactNumber = dataTable.Rows[0][7].TooString("");
                            applicantActionAdminModel.applicant.contactNumber = dataTable.Rows[0][7].TooString("");
                        }
                        message.status = true;
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        message.status = false;
                        message.msg = exception.Message;
                    }
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            applicantActionAdminModel.extensionData = new ApplicantExtensionAction();
            return View(applicantActionAdminModel);
        }

        [HttpPost]
        public JsonResult ExtensionSetupSave(ApplicantExtensionAction ac)
        {
            ac.startDate = DateTime.Now;
            ac.endDate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(ac.dateStart))
            {
                ac.startDate = ac.dateStart.TooDate();
            }
            if (!string.IsNullOrWhiteSpace(ac.dateEnd))
            {
                ac.endDate = ac.dateEnd.TooDate();
            }
            ac.adminId = base.loggedInUser.userId;
            Message message = (new ActionDAL()).AddUpdateExtension(ac);
            return base.Json(message, 0);
        }

        public ActionResult ExtenstionLisiting()
        {
            ApplicantActionAdminModel applicantActionAdminModel = new ApplicantActionAdminModel()
            {
                listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant)
            };
            string absolutePath = Request.Url.AbsolutePath;
            applicantActionAdminModel.typeId = 0;
            applicantActionAdminModel.instituteId = base.loggedInUser.reffId;
            return View(applicantActionAdminModel);
        }

        [HttpGet]
        public JsonResult GetApplicantExtensionData(int applicantLeaveId)
        {
            List<ApplicantExtensionAction> applicantExtensionActions = new List<ApplicantExtensionAction>();
            ApplicantExtensionAction extensionData = (new ActionDAL()).getExtensionData(0, applicantLeaveId);
            applicantExtensionActions.Add(extensionData);
            return base.Json(extensionData, 0);
        }

        [HttpGet]
        public JsonResult GetApplicantLeaveData(int applicantLeaveId)
        {
            List<ApplicantLeaveAction> applicantLeaveActions = new List<ApplicantLeaveAction>();
            ApplicantLeaveAction leaveData = (new ActionDAL()).getLeaveData(0, applicantLeaveId);
            applicantLeaveActions.Add(leaveData);
            return base.Json(leaveData, 0);
        }

        [HttpPost]
        public ActionResult LeaveApprovalListSearch(SearchReport obj)
        {
            obj.adminId = base.loggedInUser.userId;
            obj.search = obj.search.TooString("");
            DataTable dataTable = (new ReportDAL()).LeavesApprovalListSearch(obj);
            string str = JsonConvert.SerializeObject(dataTable);
            return base.Content(str, "application/json");
        }

        [HttpPost]
        public ActionResult LeaveApprovalSearch(SearchReport obj)
        {
            obj.adminId = base.loggedInUser.userId;
            obj.search = obj.search.TooString("");
            DataTable dataTable = (new ReportDAL()).LeavesApprovalSearch(obj);
            string str = JsonConvert.SerializeObject(dataTable);
            return base.Content(str, "application/json");
        }

        public Message IssueOrder(int leaveId)
        {
            Message msg = new Message();
            string str1 = "insert into tblLeaveSanctioned values (" + leaveId + ",1,getdate())";
            SqlConnection sqlConnection = new SqlConnection();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand(str1);
            try
            {
                try
                {
                    sqlConnection = new SqlConnection(PrpDbConnectADO.Conn);
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.ExecuteNonQuery();
                    msg.status = true;
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    msg.status = false;
                    msg.msg = exception.Message;
                }
            }
            finally
            {
                sqlConnection.Close();
            }
            return msg;
        }

        #region Leave 

        public ActionResult LeaveApplicationSetup()
        {
            EmptyModelAdmin model = new EmptyModelAdmin();
            return View(model);
        }

        [HttpPost]
        public ActionResult LeaveDocsGetByParam(Leave obj)
        {
            obj.adminId = loggedInUser.userId;
            DataSet result = new ActionDAL().LeaveDocsGetByParam(obj);
            string str = JsonConvert.SerializeObject(result);
            return base.Content(str, "application/json");
        }

        [HttpPost]
        public ActionResult ApplicantLeaveInfoByParam(Leave obj)
        {
            obj.adminId = loggedInUser.userId;
            DataSet result = new ActionDAL().ApplicantLeaveInfoByParam(obj);
            string str = JsonConvert.SerializeObject(result);
            return base.Content(str, "application/json");
        }

        //

        [HttpPost]
        public JsonResult LeaveValidate(Leave ac)
        {
            if (!string.IsNullOrWhiteSpace(ac.dateStart))
            {
                ac.startDate = ac.dateStart.TooDate();
            }
            if (!string.IsNullOrWhiteSpace(ac.dateEnd))
            {
                ac.endDate = ac.dateEnd.TooDate();
            }
            if (ac.typeId != 6113)
            {
                ac.eddDate = DateTime.Now;
            }
            else if (!string.IsNullOrWhiteSpace(ac.dateEdd))
            {
                ac.eddDate = ac.dateEdd.TooDate();
            }
            ac.adminId = base.loggedInUser.userId;
            Message message = (new ActionDAL()).LeaveValidate(ac);
            return Json(message, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult LeaveAddUpdate(Leave ac)
        {
            if (!string.IsNullOrWhiteSpace(ac.dateStart))
            {
                ac.startDate = ac.dateStart.TooDate();
            }
            if (!string.IsNullOrWhiteSpace(ac.dateEnd))
            {
                ac.endDate = ac.dateEnd.TooDate();
            }
            if (ac.typeId != 6113)
            {
                ac.eddDate = DateTime.Now;
            }
            else if (!string.IsNullOrWhiteSpace(ac.dateEdd))
            {
                ac.eddDate = ac.dateEdd.TooDate();  
            }
            ac.adminId = base.loggedInUser.userId;

            ac.dataTable = MyFunctions.ConvertDataTable(ac.listDataTb);

            Message message = (new ActionDAL()).LeaveAddUpdate(ac);

            return Json(message, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult LeaveSetupSave(ApplicantLeaveAction ac)
        {
            ac.startDate = DateTime.Now;
            ac.endDate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(ac.dateStart))
            {
                ac.startDate = ac.dateStart.TooDate();
            }
            if (!string.IsNullOrWhiteSpace(ac.dateEnd))
            {
                ac.endDate = ac.dateEnd.TooDate();
            }
            if (ac.typeId != 6113)
            {
                ac.edd = null;
            }
            else if (!string.IsNullOrWhiteSpace(ac.eddString))
            {
                ac.edd = new DateTime?(ac.eddString.TooDate());
            }
            ac.adminId = base.loggedInUser.userId;
            Message message = (new ActionDAL()).AddUpdateLeave(ac);
            return base.Json(message, 0);
        }

        #endregion

        public ActionResult LeaveApprovalSetup()
        {
            ApplicantActionAdminModel applicantActionAdminModel = new ApplicantActionAdminModel()
            {
                listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant)
            };
            Request.Url.AbsolutePath.ToLower();
            applicantActionAdminModel.typeId = 0;
            applicantActionAdminModel.heading = "Leave Approval";
            applicantActionAdminModel.applicantId = Request.QueryString["applicantId"].TooInt();
            applicantActionAdminModel.applicantLeaveId = Request.QueryString["applicantLeaveId"].TooInt();
            if (applicantActionAdminModel.applicantId > 0)
            {
                applicantActionAdminModel.joinApplicant = (new JoiningDAL()).GetJoinedApplicantDetailById(applicantActionAdminModel.applicantId);
                if (applicantActionAdminModel.applicantId > 0)
                {
                    applicantActionAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, applicantActionAdminModel.applicantId);
                    applicantActionAdminModel.applicantInfo = (new ApplicantDAL()).GetApplicantInfo(0, 0, applicantActionAdminModel.applicantId);
                    applicantActionAdminModel.action = (new ActionDAL()).GetById(applicantActionAdminModel.applicantId, applicantActionAdminModel.typeId);
                    ApplicantLeaveAction leaveData = (new ActionDAL()).getLeaveData(applicantActionAdminModel.applicantId, applicantActionAdminModel.applicantLeaveId);
                    applicantActionAdminModel.leaveDataList = (new ActionDAL()).getLeaveDataList(applicantActionAdminModel.applicantId);
                    applicantActionAdminModel.leaveData = leaveData;
                }
            }
            applicantActionAdminModel.typeId = 0;
            if ((applicantActionAdminModel.applicantId <= 0 ? false : applicantActionAdminModel.joinApplicant.applicantId == 0))
            {
                applicantActionAdminModel.joinApplicant.applicantId = applicantActionAdminModel.applicantId;
                applicantActionAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, applicantActionAdminModel.applicantId);
            }
            if (applicantActionAdminModel.joinApplicant.joiningDate < DateTime.Now.AddYears(-10))
            {
                int num = applicantActionAdminModel.applicantId;
                string str = string.Concat("select joiningDate, speciality, hospital, program, name,pmdcNo,emailId,contactNumber from tblTraineeInfo ti inner join tblApplicant a on ti.applicantId = a.applicantId where ti.applicantId = ", num.ToString());
                SqlConnection sqlConnection = new SqlConnection();
                DataTable dataTable = new DataTable();
                Message message = new Message();
                SqlCommand sqlCommand = new SqlCommand(str);
                try
                {
                    try
                    {
                        sqlConnection = new SqlConnection(PrpDbConnectADO.Conn);
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        (new SqlDataAdapter(sqlCommand)).Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            applicantActionAdminModel.joinApplicant.joiningDate = Convert.ToDateTime(dataTable.Rows[0][0]);
                            applicantActionAdminModel.joinApplicant.specialityName = dataTable.Rows[0][1].TooString("");
                            applicantActionAdminModel.joinApplicant.hospitalName = dataTable.Rows[0][2].TooString("");
                            applicantActionAdminModel.joinApplicant.typeName = dataTable.Rows[0][3].TooString("");
                            applicantActionAdminModel.joinApplicant.name = dataTable.Rows[0][4].TooString("");
                            applicantActionAdminModel.applicant.name = dataTable.Rows[0][4].TooString("");
                            applicantActionAdminModel.joinApplicant.pmdcNo = dataTable.Rows[0][5].TooString("");
                            applicantActionAdminModel.applicant.pmdcNo = dataTable.Rows[0][5].TooString("");
                            applicantActionAdminModel.joinApplicant.emailId = dataTable.Rows[0][6].TooString("");
                            applicantActionAdminModel.applicant.emailId = dataTable.Rows[0][6].TooString("");
                            applicantActionAdminModel.joinApplicant.contactNumber = dataTable.Rows[0][7].TooString("");
                            applicantActionAdminModel.applicant.contactNumber = dataTable.Rows[0][7].TooString("");
                        }
                        message.status = true;
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        message.status = false;
                        message.msg = exception.Message;
                    }
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            ViewData["LoggedInUserId"] = loggedInUser.userId;
            return View(applicantActionAdminModel);
        }

        [HttpPost]
        public JsonResult LeaveApprovalSetupSave(ApplicantLeaveAction ac)
        {
            int num;
            DateTime now;
            Message message = new Message();
            if (ac.actionId <= 1)
            {
                if (base.loggedInUser.userId == 48)
                {
                    string str = ac.remarks.TooString("");
                    num = ac.applicantLeaveId;
                    string str1 = string.Concat("update tblApplicantLeave set  remarksByDS = '", str, "'  where applicantLeaveId = ", num.ToString());
                    string str2 = string.Concat("", "insert into tblApplicantLeaveCorrespondence(applicantId, applicantLeaveId, leaveTypeId ");
                    str2 = string.Concat(str2, ", remarks, dated, adminId, actionPerformed) ");
                    string[] strArrays = new string[] { str2, "values (", null, null, null, null, null, null };
                    num = ac.applicantId;
                    strArrays[2] = num.ToString();
                    strArrays[3] = ", ";
                    num = ac.applicantLeaveId;
                    strArrays[4] = num.ToString();
                    strArrays[5] = ", ";
                    num = ac.typeId;
                    strArrays[6] = num.ToString();
                    strArrays[7] = " ";
                    str2 = string.Concat(strArrays);
                    string[] strArrays1 = new string[] { str2, ", '", ac.remarks.TooString(""), "', '", null, null, null, null, null, null };
                    now = DateTime.Now;
                    strArrays1[4] = now.ToString();
                    strArrays1[5] = "', ";
                    num = base.loggedInUser.userId;
                    strArrays1[6] = num.ToString();
                    strArrays1[7] = ", ";
                    num = ac.actionId;
                    strArrays1[8] = num.ToString();
                    strArrays1[9] = ") ";
                    str2 = string.Concat(strArrays1);
                    SqlConnection sqlConnection = new SqlConnection();
                    DataTable dataTable = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand(str1);
                    SqlCommand sqlCommand1 = new SqlCommand(str2);
                    try
                    {
                        try
                        {
                            sqlConnection = new SqlConnection(PrpDbConnectADO.Conn);
                            sqlConnection.Open();
                            sqlCommand.Connection = sqlConnection;
                            sqlCommand.ExecuteNonQuery();
                            sqlCommand1.Connection = sqlConnection;
                            sqlCommand1.ExecuteNonQuery();
                            message.status = true;
                        }
                        catch (Exception exception1)
                        {
                            Exception exception = exception1;
                            message.status = false;
                            message.msg = exception.Message;
                        }
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
                else if (base.loggedInUser.userId == 151)
                {
                    string str3 = ac.remarks.TooString("");
                    num = ac.applicantLeaveId;
                    string str4 = string.Concat("update tblApplicantLeave set  remarksByAST = '", str3, "' where applicantLeaveId = ", num.ToString());
                    string str5 = string.Concat("", "insert into tblApplicantLeaveCorrespondence(applicantId, applicantLeaveId, leaveTypeId ");
                    str5 = string.Concat(str5, ", remarks, dated, adminId, actionPerformed) ");
                    string[] strArrays2 = new string[] { str5, "values (", null, null, null, null, null, null };
                    num = ac.applicantId;
                    strArrays2[2] = num.ToString();
                    strArrays2[3] = ", ";
                    num = ac.applicantLeaveId;
                    strArrays2[4] = num.ToString();
                    strArrays2[5] = ", ";
                    num = ac.typeId;
                    strArrays2[6] = num.ToString();
                    strArrays2[7] = " ";
                    str5 = string.Concat(strArrays2);
                    string[] strArrays3 = new string[] { str5, ", '", ac.remarks.TooString(""), "', '", null, null, null, null, null, null };
                    now = DateTime.Now;
                    strArrays3[4] = now.ToString();
                    strArrays3[5] = "', ";
                    num = base.loggedInUser.userId;
                    strArrays3[6] = num.ToString();
                    strArrays3[7] = ", ";
                    num = ac.actionId;
                    strArrays3[8] = num.ToString();
                    strArrays3[9] = ") ";
                    str5 = string.Concat(strArrays3);
                    SqlConnection sqlConnection1 = new SqlConnection();
                    DataTable dataTable1 = new DataTable();
                    SqlCommand sqlCommand2 = new SqlCommand(str4);
                    SqlCommand sqlCommand3 = new SqlCommand(str5);
                    try
                    {
                        try
                        {
                            sqlConnection1 = new SqlConnection(PrpDbConnectADO.Conn);
                            sqlConnection1.Open();
                            sqlCommand2.Connection = sqlConnection1;
                            sqlCommand2.ExecuteNonQuery();
                            sqlCommand3.Connection = sqlConnection1;
                            sqlCommand3.ExecuteNonQuery();
                            message.status = true;
                        }
                        catch (Exception exception3)
                        {
                            Exception exception2 = exception3;
                            message.status = false;
                            message.msg = exception2.Message;
                        }
                    }
                    finally
                    {
                        sqlConnection1.Close();
                    }
                }
                else if (base.loggedInUser.userId == 152)
                {
                    string str6 = ac.remarks.TooString("");
                    num = ac.applicantLeaveId;
                    string str7 = string.Concat("update tblApplicantLeave set  remarksBySS = '", str6, "'  where applicantLeaveId = ", num.ToString());
                    string str8 = string.Concat("", "insert into tblApplicantLeaveCorrespondence(applicantId, applicantLeaveId, leaveTypeId ");
                    str8 = string.Concat(str8, ", remarks, dated, adminId, actionPerformed) ");
                    string[] strArrays4 = new string[] { str8, "values (", null, null, null, null, null, null };
                    num = ac.applicantId;
                    strArrays4[2] = num.ToString();
                    strArrays4[3] = ", ";
                    num = ac.applicantLeaveId;
                    strArrays4[4] = num.ToString();
                    strArrays4[5] = ", ";
                    num = ac.typeId;
                    strArrays4[6] = num.ToString();
                    strArrays4[7] = " ";
                    str8 = string.Concat(strArrays4);
                    string[] strArrays5 = new string[] { str8, ", '", ac.remarks.TooString(""), "', '", null, null, null, null, null, null };
                    now = DateTime.Now;
                    strArrays5[4] = now.ToString();
                    strArrays5[5] = "', ";
                    num = base.loggedInUser.userId;
                    strArrays5[6] = num.ToString();
                    strArrays5[7] = ", ";
                    num = ac.actionId;
                    strArrays5[8] = num.ToString();
                    strArrays5[9] = ") ";
                    str8 = string.Concat(strArrays5);
                    SqlConnection sqlConnection2 = new SqlConnection();
                    DataTable dataTable2 = new DataTable();
                    SqlCommand sqlCommand4 = new SqlCommand(str7);
                    SqlCommand sqlCommand5 = new SqlCommand(str8);
                    try
                    {
                        try
                        {
                            sqlConnection2 = new SqlConnection(PrpDbConnectADO.Conn);
                            sqlConnection2.Open();
                            sqlCommand4.Connection = sqlConnection2;
                            sqlCommand4.ExecuteNonQuery();
                            sqlCommand5.Connection = sqlConnection2;
                            sqlCommand5.ExecuteNonQuery();
                            message.status = true;
                        }
                        catch (Exception exception5)
                        {
                            Exception exception4 = exception5;
                            message.status = false;
                            message.msg = exception4.Message;
                        }
                    }
                    finally
                    {
                        sqlConnection2.Close();
                    }
                }
                ac.adminId = base.loggedInUser.userId;
                message = (new ActionDAL()).AddUpdateLeaveApproval(ac);
            }
            else if (base.loggedInUser.userId == 150)
            {
                string[] strArrays6 = new string[] { "update tblApplicantLeave set applicationValidity = ", null, null, null, null };
                num = ac.applicationValidity.TooInt();
                strArrays6[1] = num.ToString();
                strArrays6[2] = ", applicationRemarks = '";
                strArrays6[3] = ac.applicationRemarks.TooString("");
                strArrays6[4] = "' ";
                string str9 = string.Concat(strArrays6);
                string[] strArrays7 = new string[] { str9, " ,affidavitValidity = ", null, null, null, null };
                num = ac.affidavitValidity.TooInt();
                strArrays7[2] = num.ToString();
                strArrays7[3] = ", affidavitRemarks = '";
                strArrays7[4] = ac.affidavitRemarks.TooString("");
                strArrays7[5] = "' ";
                str9 = string.Concat(strArrays7);
                string[] strArrays8 = new string[] { str9, " ,medicalValidity = ", null, null, null, null };
                num = ac.medicalValidity.TooInt();
                strArrays8[2] = num.ToString();
                strArrays8[3] = ", medicalRemarks = '";
                strArrays8[4] = ac.medicalRemarks.TooString("");
                strArrays8[5] = "' ";
                str9 = string.Concat(strArrays8);
                string[] strArrays9 = new string[] { str9, " ,matenrityValidity = ", null, null, null, null };
                num = ac.matenrityValidity.TooInt();
                strArrays9[2] = num.ToString();
                strArrays9[3] = ", matenrityRemarks = '";
                strArrays9[4] = ac.matenrityRemarks.TooString("");
                strArrays9[5] = "' ";
                str9 = string.Concat(strArrays9);
                string[] str10 = new string[] { str9, " ,forwardingValidity = ", null, null, null, null };
                num = ac.forwardingValidity.TooInt();
                str10[2] = num.ToString();
                str10[3] = ", forwardingRemarks = '";
                str10[4] = ac.forwardingRemarks.TooString("");
                str10[5] = "' ";
                str9 = string.Concat(str10);
                string[] strArrays10 = new string[] { str9, " ,ipgacValidity = ", null, null, null, null };
                num = ac.ipgacValidity.TooInt();
                strArrays10[2] = num.ToString();
                strArrays10[3] = ", ipgacRemarks = '";
                strArrays10[4] = ac.ipgacRemarks.TooString("");
                strArrays10[5] = "' ";
                str9 = string.Concat(strArrays10);
                string[] str11 = new string[] { str9, " ,suretyValidity = ", null, null, null, null };
                num = ac.suretyValidity.TooInt();
                str11[2] = num.ToString();
                str11[3] = ", suretyRemarks = '";
                str11[4] = ac.suretyRemarks.TooString("");
                str11[5] = "' ";
                str9 = string.Concat(str11);
                string[] strArrays11 = new string[] { str9, " ,attorneyValidity = ", null, null, null, null };
                num = ac.attorneyValidity.TooInt();
                strArrays11[2] = num.ToString();
                strArrays11[3] = ", attorneyRemarks = '";
                strArrays11[4] = ac.attorneyRemarks.TooString("");
                strArrays11[5] = "' ";
                str9 = string.Concat(strArrays11);
                string[] str12 = new string[] { str9, " ,purposeValidity = ", null, null, null, null };
                num = ac.purposeValidity.TooInt();
                str12[2] = num.ToString();
                str12[3] = ", purposeRemarks = '";
                str12[4] = ac.purposeRemarks.TooString("");
                str12[5] = "' ";
                str9 = string.Concat(str12);
                string[] strArrays12 = new string[] { str9, " ,visaValidity = ", null, null, null, null };
                num = ac.visaValidity.TooInt();
                strArrays12[2] = num.ToString();
                strArrays12[3] = ", visaRemarks = '";
                strArrays12[4] = ac.visaRemarks.TooString("");
                strArrays12[5] = "' ";
                str9 = string.Concat(strArrays12);
                string[] str13 = new string[] { str9, " ,RTMCValidity = ", null, null, null, null };
                num = ac.RTMCValidity.TooInt();
                str13[2] = num.ToString();
                str13[3] = ", RTMCRemarks = '";
                str13[4] = ac.RTMCRemarks.TooString("");
                str13[5] = "' ";
                str9 = string.Concat(str13);

                str13 = new string[] { str9, " ,imagePreviousLeaveReportValidy = ", null, null, null, null };
                num = ac.imagePreviousLeaveReportValidity.TooInt();
                str13[2] = num.ToString();
                str13[3] = ", imagePreviousLeaveReportRemarks = '";
                str13[4] = ac.imagePreviousLeaveReportRemarks.TooString("");
                str13[5] = "' ";
                str9 = string.Concat(str13);

                str9 = string.Concat(str9, " ,remarksBySO = '", ac.remarks.TooString(""), "' ");
                num = ac.applicantLeaveId;
                str9 = string.Concat(str9, ", forwardedTo = 48 where applicantLeaveId = ", num.ToString());
                string str14 = string.Concat("", "insert into tblApplicantLeaveCorrespondence(applicantId, applicantLeaveId, leaveTypeId ");
                str14 = string.Concat(str14, ", applicationValidity, applicationRemarks ");
                str14 = string.Concat(str14, ", affidavitValidity, affidavitRemarks ");
                str14 = string.Concat(str14, ", medicalValidity, medicalRemarks ");
                str14 = string.Concat(str14, ", matenrityValidity, matenrityRemarks ");
                str14 = string.Concat(str14, ", forwardingValidity, forwardingRemarks ");
                str14 = string.Concat(str14, ", ipgacValidity, ipgacRemarks ");
                str14 = string.Concat(str14, ", suretyValidity, suretyRemarks ");
                str14 = string.Concat(str14, ", attorneyValidity, attorneyRemarks ");
                str14 = string.Concat(str14, ", purposeValidity, purposeRemarks ");
                str14 = string.Concat(str14, ", RTMCValidity, RTMCRemarks ");
                str14 = string.Concat(str14, ", imagePreviousLeaveReportValidy, imagePreviousLeaveReportRemarks ");
                str14 = string.Concat(str14, ", remarks, dated, adminId, actionPerformed) ");
                string[] strArrays13 = new string[] { str14, "values (", null, null, null, null, null, null };
                num = ac.applicantId;
                strArrays13[2] = num.ToString();
                strArrays13[3] = ", ";
                num = ac.applicantLeaveId;
                strArrays13[4] = num.ToString();
                strArrays13[5] = ", ";
                num = ac.typeId;
                strArrays13[6] = num.ToString();
                strArrays13[7] = " ";
                str14 = string.Concat(strArrays13);
                string[] strArrays14 = new string[] { str14, ", ", null, null, null, null };
                num = ac.applicationValidity.TooInt();
                strArrays14[2] = num.ToString();
                strArrays14[3] = ", '";
                strArrays14[4] = ac.applicationRemarks.TooString("");
                strArrays14[5] = "' ";
                str14 = string.Concat(strArrays14);
                string[] str15 = new string[] { str14, ", ", null, null, null, null };
                num = ac.affidavitValidity.TooInt();
                str15[2] = num.ToString();
                str15[3] = ", '";
                str15[4] = ac.affidavitRemarks.TooString("");
                str15[5] = "' ";
                str14 = string.Concat(str15);
                string[] strArrays15 = new string[] { str14, ", ", null, null, null, null };
                num = ac.medicalValidity.TooInt();
                strArrays15[2] = num.ToString();
                strArrays15[3] = ", '";
                strArrays15[4] = ac.medicalRemarks.TooString("");
                strArrays15[5] = "' ";
                str14 = string.Concat(strArrays15);
                string[] str16 = new string[] { str14, ", ", null, null, null, null };
                num = ac.matenrityValidity.TooInt();
                str16[2] = num.ToString();
                str16[3] = ", '";
                str16[4] = ac.matenrityRemarks.TooString("");
                str16[5] = "' ";
                str14 = string.Concat(str16);
                string[] strArrays16 = new string[] { str14, ", ", null, null, null, null };
                num = ac.forwardingValidity.TooInt();
                strArrays16[2] = num.ToString();
                strArrays16[3] = ", '";
                strArrays16[4] = ac.forwardingRemarks.TooString("");
                strArrays16[5] = "' ";
                str14 = string.Concat(strArrays16);
                string[] str17 = new string[] { str14, ", ", null, null, null, null };
                num = ac.ipgacValidity.TooInt();
                str17[2] = num.ToString();
                str17[3] = ", '";
                str17[4] = ac.ipgacRemarks.TooString("");
                str17[5] = "' ";
                str14 = string.Concat(str17);
                string[] strArrays17 = new string[] { str14, ", ", null, null, null, null };
                num = ac.suretyValidity.TooInt();
                strArrays17[2] = num.ToString();
                strArrays17[3] = ", '";
                strArrays17[4] = ac.suretyRemarks.TooString("");
                strArrays17[5] = "' ";
                str14 = string.Concat(strArrays17);
                string[] str18 = new string[] { str14, ", ", null, null, null, null };
                num = ac.attorneyValidity.TooInt();
                str18[2] = num.ToString();
                str18[3] = ", '";
                str18[4] = ac.attorneyRemarks.TooString("");
                str18[5] = "' ";
                str14 = string.Concat(str18);
                string[] strArrays18 = new string[] { str14, ", ", null, null, null, null };
                num = ac.purposeValidity.TooInt();
                strArrays18[2] = num.ToString();
                strArrays18[3] = ", '";
                strArrays18[4] = ac.purposeRemarks.TooString("");
                strArrays18[5] = "' ";
                str14 = string.Concat(strArrays18);

                string[] str19 = new string[] { str14, ", ", null, null, null, null };
                num = ac.RTMCValidity.TooInt();
                str19[2] = num.ToString();
                str19[3] = ", '";
                str19[4] = ac.RTMCRemarks.TooString("");
                str19[5] = "' ";
                str14 = string.Concat(str19);

                string[] str20 = new string[] { str14, ", ", null, null, null, null };
                num = ac.imagePreviousLeaveReportValidity.TooInt();
                str20[2] = num.ToString();
                str20[3] = ", '";
                str20[4] = ac.imagePreviousLeaveReportRemarks.TooString("");
                str20[5] = "' ";
                str14 = string.Concat(str20);

                string[] strArrays19 = new string[] { str14, ", '", ac.remarks.TooString(""), "', '", null, null, null, null, null, null };
                now = DateTime.Now;
                strArrays19[4] = now.ToString();
                strArrays19[5] = "', ";
                num = base.loggedInUser.userId;
                strArrays19[6] = num.ToString();
                strArrays19[7] = ", ";
                num = ac.actionId;
                strArrays19[8] = num.ToString();
                strArrays19[9] = ") ";
                str14 = string.Concat(strArrays19);
                SqlConnection sqlConnection3 = new SqlConnection();
                DataTable dataTable3 = new DataTable();
                SqlCommand sqlCommand6 = new SqlCommand(str9);
                SqlCommand sqlCommand7 = new SqlCommand(str14);
                try
                {
                    try
                    {
                        sqlConnection3 = new SqlConnection(PrpDbConnectADO.Conn);
                        sqlConnection3.Open();
                        sqlCommand6.Connection = sqlConnection3;
                        sqlCommand6.ExecuteNonQuery();
                        sqlCommand7.Connection = sqlConnection3;
                        sqlCommand7.ExecuteNonQuery();
                        message.status = true;
                    }
                    catch (Exception exception7)
                    {
                        Exception exception6 = exception7;
                        message.status = false;
                        message.msg = exception6.Message;
                    }
                }
                finally
                {
                    sqlConnection3.Close();
                }
            }
            else if (base.loggedInUser.userId == 48)
            {
                string str20 = string.Concat("update tblApplicantLeave set  remarksByDS = '", ac.remarks.TooString(""), "' ");
                string[] strArrays20 = new string[] { str20, ", forwardedTo = ", null, null, null };
                num = ac.actionId;
                strArrays20[2] = num.ToString();
                strArrays20[3] = " where applicantLeaveId = ";
                num = ac.applicantLeaveId;
                strArrays20[4] = num.ToString();
                str20 = string.Concat(strArrays20);
                string str21 = string.Concat("", "insert into tblApplicantLeaveCorrespondence(applicantId, applicantLeaveId, leaveTypeId ");
                str21 = string.Concat(str21, ", remarks, dated, adminId, actionPerformed) ");
                string[] strArrays21 = new string[] { str21, "values (", null, null, null, null, null, null };
                num = ac.applicantId;
                strArrays21[2] = num.ToString();
                strArrays21[3] = ", ";
                num = ac.applicantLeaveId;
                strArrays21[4] = num.ToString();
                strArrays21[5] = ", ";
                num = ac.typeId;
                strArrays21[6] = num.ToString();
                strArrays21[7] = " ";
                str21 = string.Concat(strArrays21);
                string[] str22 = new string[] { str21, ", '", ac.remarks.TooString(""), "', '", null, null, null, null, null, null };
                now = DateTime.Now;
                str22[4] = now.ToString();
                str22[5] = "', ";
                num = base.loggedInUser.userId;
                str22[6] = num.ToString();
                str22[7] = ", ";
                num = ac.actionId;
                str22[8] = num.ToString();
                str22[9] = ") ";
                str21 = string.Concat(str22);
                SqlConnection sqlConnection4 = new SqlConnection();
                DataTable dataTable4 = new DataTable();
                SqlCommand sqlCommand8 = new SqlCommand(str20);
                SqlCommand sqlCommand9 = new SqlCommand(str21);
                try
                {
                    try
                    {
                        sqlConnection4 = new SqlConnection(PrpDbConnectADO.Conn);
                        sqlConnection4.Open();
                        sqlCommand8.Connection = sqlConnection4;
                        sqlCommand8.ExecuteNonQuery();
                        sqlCommand9.Connection = sqlConnection4;
                        sqlCommand9.ExecuteNonQuery();
                        message.status = true;
                    }
                    catch (Exception exception9)
                    {
                        Exception exception8 = exception9;
                        message.status = false;
                        message.msg = exception8.Message;
                    }
                }
                finally
                {
                    sqlConnection4.Close();
                }
            }
            else if (base.loggedInUser.userId == 151)
            {
                string str23 = string.Concat("update tblApplicantLeave set  remarksByAST = '", ac.remarks.TooString(""), "' ");
                string[] strArrays22 = new string[] { str23, ", forwardedTo = ", null, null, null };
                num = ac.actionId;
                strArrays22[2] = num.ToString();
                strArrays22[3] = " where applicantLeaveId = ";
                num = ac.applicantLeaveId;
                strArrays22[4] = num.ToString();
                str23 = string.Concat(strArrays22);
                string str24 = string.Concat("", "insert into tblApplicantLeaveCorrespondence(applicantId, applicantLeaveId, leaveTypeId ");
                str24 = string.Concat(str24, ", remarks, dated, adminId, actionPerformed) ");
                string[] strArrays23 = new string[] { str24, "values (", null, null, null, null, null, null };
                num = ac.applicantId;
                strArrays23[2] = num.ToString();
                strArrays23[3] = ", ";
                num = ac.applicantLeaveId;
                strArrays23[4] = num.ToString();
                strArrays23[5] = ", ";
                num = ac.typeId;
                strArrays23[6] = num.ToString();
                strArrays23[7] = " ";
                str24 = string.Concat(strArrays23);
                string[] strArrays24 = new string[] { str24, ", '", ac.remarks.TooString(""), "', '", null, null, null, null, null, null };
                now = DateTime.Now;
                strArrays24[4] = now.ToString();
                strArrays24[5] = "', ";
                num = base.loggedInUser.userId;
                strArrays24[6] = num.ToString();
                strArrays24[7] = ", ";
                num = ac.actionId;
                strArrays24[8] = num.ToString();
                strArrays24[9] = ") ";
                str24 = string.Concat(strArrays24);
                SqlConnection sqlConnection5 = new SqlConnection();
                DataTable dataTable5 = new DataTable();
                SqlCommand sqlCommand10 = new SqlCommand(str23);
                SqlCommand sqlCommand11 = new SqlCommand(str24);
                try
                {
                    try
                    {
                        sqlConnection5 = new SqlConnection(PrpDbConnectADO.Conn);
                        sqlConnection5.Open();
                        sqlCommand10.Connection = sqlConnection5;
                        sqlCommand10.ExecuteNonQuery();
                        sqlCommand11.Connection = sqlConnection5;
                        sqlCommand11.ExecuteNonQuery();
                        message.status = true;
                    }
                    catch (Exception exception11)
                    {
                        Exception exception10 = exception11;
                        message.status = false;
                        message.msg = exception10.Message;
                    }
                }
                finally
                {
                    sqlConnection5.Close();
                }
            }
            else if (base.loggedInUser.userId == 152)
            {
                string str25 = string.Concat("update tblApplicantLeave set  remarksBySS = '", ac.remarks.TooString(""), "' ");
                string[] strArrays25 = new string[] { str25, ", forwardedTo = ", null, null, null };
                num = ac.actionId;
                strArrays25[2] = num.ToString();
                strArrays25[3] = " where applicantLeaveId = ";
                num = ac.applicantLeaveId;
                strArrays25[4] = num.ToString();
                str25 = string.Concat(strArrays25);
                string str26 = string.Concat("", "insert into tblApplicantLeaveCorrespondence(applicantId, applicantLeaveId, leaveTypeId ");
                str26 = string.Concat(str26, ", remarks, dated, adminId, actionPerformed) ");
                string[] strArrays26 = new string[] { str26, "values (", null, null, null, null, null, null };
                num = ac.applicantId;
                strArrays26[2] = num.ToString();
                strArrays26[3] = ", ";
                num = ac.applicantLeaveId;
                strArrays26[4] = num.ToString();
                strArrays26[5] = ", ";
                num = ac.typeId;
                strArrays26[6] = num.ToString();
                strArrays26[7] = " ";
                str26 = string.Concat(strArrays26);
                string[] str27 = new string[] { str26, ", '", ac.remarks.TooString(""), "', '", null, null, null, null, null, null };
                now = DateTime.Now;
                str27[4] = now.ToString();
                str27[5] = "', ";
                num = base.loggedInUser.userId;
                str27[6] = num.ToString();
                str27[7] = ", ";
                num = ac.actionId;
                str27[8] = num.ToString();
                str27[9] = ") ";
                str26 = string.Concat(str27);
                SqlConnection sqlConnection6 = new SqlConnection();
                DataTable dataTable6 = new DataTable();
                SqlCommand sqlCommand12 = new SqlCommand(str25);
                SqlCommand sqlCommand13 = new SqlCommand(str26);
                try
                {
                    try
                    {
                        sqlConnection6 = new SqlConnection(PrpDbConnectADO.Conn);
                        sqlConnection6.Open();
                        sqlCommand12.Connection = sqlConnection6;
                        sqlCommand12.ExecuteNonQuery();
                        sqlCommand13.Connection = sqlConnection6;
                        sqlCommand13.ExecuteNonQuery();
                        message.status = true;
                    }
                    catch (Exception exception13)
                    {
                        Exception exception12 = exception13;
                        message.status = false;
                        message.msg = exception12.Message;
                    }
                }
                finally
                {
                    sqlConnection6.Close();
                }
            }
            return base.Json(message, 0);
        }

        public ActionResult LeavePrintSetup()
        {
            ApplicantActionAdminModel applicantActionAdminModel = new ApplicantActionAdminModel()
            {
                listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant)
            };
            Request.Url.AbsolutePath.ToLower();
            applicantActionAdminModel.typeId = 0;
            applicantActionAdminModel.heading = "Leave Approval";
            applicantActionAdminModel.applicantId = Request.QueryString["applicantId"].TooInt();
            applicantActionAdminModel.applicantLeaveId = Request.QueryString["applicantLeaveId"].TooInt();
            if (applicantActionAdminModel.applicantId > 0)
            {
                applicantActionAdminModel.joinApplicant = (new JoiningDAL()).GetJoinedApplicantDetailById(applicantActionAdminModel.applicantId);
                if (applicantActionAdminModel.applicantId > 0)
                {
                    applicantActionAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, applicantActionAdminModel.applicantId);
                    applicantActionAdminModel.applicantInfo = (new ApplicantDAL()).GetApplicantInfo(0, 0, applicantActionAdminModel.applicantId);
                    applicantActionAdminModel.action = (new ActionDAL()).GetById(applicantActionAdminModel.applicantId, applicantActionAdminModel.typeId);
                    ApplicantLeaveAction leaveData = (new ActionDAL()).getLeaveData(applicantActionAdminModel.applicantId, applicantActionAdminModel.applicantLeaveId);
                    applicantActionAdminModel.leaveDataList = (new ActionDAL()).getLeaveDataList(applicantActionAdminModel.applicantId);
                    applicantActionAdminModel.leaveData = leaveData;
                }
            }
            applicantActionAdminModel.typeId = 0;
            return View(applicantActionAdminModel);
        }

        public ActionResult LeavesApprovalLisiting()
        {
            ApplicantActionAdminModel applicantActionAdminModel = new ApplicantActionAdminModel()
            {
                listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant)
            };
            string absolutePath = Request.Url.AbsolutePath;
            applicantActionAdminModel.typeId = 0;
            applicantActionAdminModel.instituteId = base.loggedInUser.reffId;
            return View(applicantActionAdminModel);
        }

        [HttpPost]
        public ActionResult LeaveSearch(SearchReport obj)
        {
            obj.adminId = base.loggedInUser.userId;
            obj.search = obj.search.TooString("");
            DataTable dataTable = (new ReportDAL()).LeavesSearch(obj);
            string str = JsonConvert.SerializeObject(dataTable);
            return base.Content(str, "application/json");
        }

        public ActionResult LeaveSetup()
        {
            DateTime dateTime;
            ApplicantActionAdminModel applicantActionAdminModel = new ApplicantActionAdminModel()
            {
                listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant)
            };
            Request.Url.AbsolutePath.ToLower();
            applicantActionAdminModel.typeId = 0;
            applicantActionAdminModel.heading = "Leave Application";
            applicantActionAdminModel.applicantId = Request.QueryString["applicantId"].TooInt();
            if (applicantActionAdminModel.applicantId > 0)
            {
                applicantActionAdminModel.joinApplicant = (new JoiningDAL()).GetJoinedApplicantDetailById(applicantActionAdminModel.applicantId);
                applicantActionAdminModel.leaveDataList = (new ActionDAL()).getLeaveDataList(applicantActionAdminModel.applicantId);
                if (applicantActionAdminModel.joinApplicant.applicantId > 0)
                {
                    applicantActionAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, applicantActionAdminModel.applicantId);
                    applicantActionAdminModel.applicantInfo = (new ApplicantDAL()).GetApplicantInfo(0, 0, applicantActionAdminModel.applicantId);
                    applicantActionAdminModel.action = (new ActionDAL()).GetById(applicantActionAdminModel.applicantId, applicantActionAdminModel.typeId);
                }
            }
            applicantActionAdminModel.typeId = 0;
            if ((applicantActionAdminModel.applicantId <= 0 ? false : applicantActionAdminModel.joinApplicant.applicantId == 0))
            {
                applicantActionAdminModel.joinApplicant.applicantId = applicantActionAdminModel.applicantId;
                applicantActionAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, applicantActionAdminModel.applicantId);
            }
            if (applicantActionAdminModel.joinApplicant.joiningDate < DateTime.Now.AddYears(-10))
            {
                int num = applicantActionAdminModel.applicantId;
                string str = string.Concat("select joiningDate, speciality, hospital, program, name,pmdcNo,emailId,contactNumber from tblTraineeInfo ti inner join tblApplicant a on ti.applicantId = a.applicantId where ti.applicantId = ", num.ToString());
                SqlConnection sqlConnection = new SqlConnection();
                DataTable dataTable = new DataTable();
                Message message = new Message();
                SqlCommand sqlCommand = new SqlCommand(str);
                try
                {
                    try
                    {
                        sqlConnection = new SqlConnection(PrpDbConnectADO.Conn);
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        (new SqlDataAdapter(sqlCommand)).Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            applicantActionAdminModel.joinApplicant.joiningDate = Convert.ToDateTime(dataTable.Rows[0][0]);
                            applicantActionAdminModel.joinApplicant.specialityName = dataTable.Rows[0][1].TooString("");
                            applicantActionAdminModel.joinApplicant.hospitalName = dataTable.Rows[0][2].TooString("");
                            applicantActionAdminModel.joinApplicant.typeName = dataTable.Rows[0][3].TooString("");
                            applicantActionAdminModel.joinApplicant.name = dataTable.Rows[0][4].TooString("");
                            applicantActionAdminModel.applicant.name = dataTable.Rows[0][4].TooString("");
                            applicantActionAdminModel.joinApplicant.pmdcNo = dataTable.Rows[0][5].TooString("");
                            applicantActionAdminModel.applicant.pmdcNo = dataTable.Rows[0][5].TooString("");
                            applicantActionAdminModel.joinApplicant.emailId = dataTable.Rows[0][6].TooString("");
                            applicantActionAdminModel.applicant.emailId = dataTable.Rows[0][6].TooString("");
                            applicantActionAdminModel.joinApplicant.contactNumber = dataTable.Rows[0][7].TooString("");
                            applicantActionAdminModel.applicant.contactNumber = dataTable.Rows[0][7].TooString("");
                        }
                        message.status = true;
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        message.status = false;
                        message.msg = exception.Message;
                    }
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            DateTime dateTime1 = applicantActionAdminModel.joinApplicant.joiningDate;
            int num1 = Convert.ToInt32(dateTime1.ToString("yyyy"));
            if (applicantActionAdminModel.joinApplicant.joiningDate <= DateTime.Now.AddYears(-1))
            {
                int num2 = DateTime.Now.Year - num1;
                dateTime1 = applicantActionAdminModel.joinApplicant.joiningDate;
                dateTime1 = dateTime1.AddYears(num2 + 1);
                dateTime = dateTime1.AddDays(-1);
            }
            else
            {
                dateTime1 = applicantActionAdminModel.joinApplicant.joiningDate;
                dateTime1 = dateTime1.AddYears(1);
                dateTime = dateTime1.AddDays(-1);
            }
            ViewData["lastDay"] = dateTime.Date;
            return View(applicantActionAdminModel);
        }

        public ActionResult LeavesLisiting()
        {
            ApplicantActionAdminModel applicantActionAdminModel = new ApplicantActionAdminModel()
            {
                listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant)
            };
            string absolutePath = Request.Url.AbsolutePath;
            applicantActionAdminModel.typeId = 0;
            applicantActionAdminModel.instituteId = base.loggedInUser.reffId;
            return View(applicantActionAdminModel);
        }

        public ActionResult LeavesListLisiting()
        {
            ApplicantActionAdminModel applicantActionAdminModel = new ApplicantActionAdminModel()
            {
                listInstitute = DDLHospital.GetAll(25, "HasEmployee")
            };
            string absolutePath = Request.Url.AbsolutePath;
            applicantActionAdminModel.typeId = 0;
            applicantActionAdminModel.instituteId = base.loggedInUser.reffId;
            return View(applicantActionAdminModel);
        }
    }
}