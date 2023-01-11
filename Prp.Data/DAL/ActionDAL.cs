using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Prp.Data
{
    public class ActionDAL : PrpDBConnect
    {
        public ApplicantAction GetById(int applicantId, int typeId)
        {
            ApplicantAction obj = new ApplicantAction();
            try
            {
                var objt = db.spApplicantActionGetByApplicantId(applicantId, typeId).FirstOrDefault();
                obj = MapResignation.ToEntity(objt);
            }
            catch (Exception)
            {
                obj = new ApplicantAction();
            }
            return obj;
        }
        public Message AddUpdateLeave(ApplicantLeaveAction obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spLeaveAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@actionId", obj.actionId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@image", obj.image);
            cmd.Parameters.AddWithValue("@imageAffidavit", obj.imageAffidavit);
            cmd.Parameters.AddWithValue("@imageMedical", obj.imageMedical);
            cmd.Parameters.AddWithValue("@imageMaternity", obj.imageMaternity);
            cmd.Parameters.AddWithValue("@imagePGAC", obj.imagePGAC);
            cmd.Parameters.AddWithValue("@imageForwarding", obj.imageForwarding);
            cmd.Parameters.AddWithValue("@imageSurety", obj.imageSurety);
            cmd.Parameters.AddWithValue("@ddlDoxTaken", obj.ddlDoxTaken);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@categoryId", obj.categoryId);
            cmd.Parameters.AddWithValue("@startDate", obj.startDate);   
            cmd.Parameters.AddWithValue("@endDate", obj.endDate);
            cmd.Parameters.AddWithValue("@isDocsCollected", obj.isDocsCollected);
            cmd.Parameters.AddWithValue("@remarks", obj.remarks);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            cmd.Parameters.AddWithValue("@imageAttorney", obj.imageAttorney);
            cmd.Parameters.AddWithValue("@imageVisa", obj.imageVisa);
            cmd.Parameters.AddWithValue("@imagePurpose", obj.imagePurpose);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public Message AddUpdateExtension(ApplicantExtensionAction obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spExtensionAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@imageApplicantion", obj.imageApplication);
            cmd.Parameters.AddWithValue("@imagePER", obj.imagePER);
            cmd.Parameters.AddWithValue("@imageNOC", obj.imageNOC);
            cmd.Parameters.AddWithValue("@imagePMDC", obj.imagePMDC);
            cmd.Parameters.AddWithValue("@imageExtensionOrder", obj.imageExtensionOrder);
            cmd.Parameters.AddWithValue("@imageJoiningOrder", obj.imageJoiningOrder);
            cmd.Parameters.AddWithValue("@imageDoc1", obj.imageDoc1);
            cmd.Parameters.AddWithValue("@imageDoc2", obj.imageDoc2);
            cmd.Parameters.AddWithValue("@approvalBySupervisor", obj.approvalBySupervisor);
            cmd.Parameters.AddWithValue("@startDate", obj.startDate);
            cmd.Parameters.AddWithValue("@endDate", obj.endDate);
            cmd.Parameters.AddWithValue("@remarks", "");
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            cmd.Parameters.AddWithValue("@imageInductionOrder", obj.imageInductionOrder);
            cmd.Parameters.AddWithValue("@rtmcUhsNo", obj.rtmcUhsNo);
            cmd.Parameters.AddWithValue("@imageTothc", obj.imageTothc);
            cmd.Parameters.AddWithValue("@imageJoat", obj.imageJoat);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public Message AddUpdateExtensionApproval(ApplicantLeaveAction obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spExtensionApprovalAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@actionId", obj.actionId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@applicantLeaveId", obj.applicantLeaveId);
            cmd.Parameters.AddWithValue("@remarks", obj.remarks);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public Message AddUpdateLeaveApproval(ApplicantLeaveAction obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spLeaveApprovalAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@actionId", obj.actionId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@applicantLeaveId", obj.applicantLeaveId);
            cmd.Parameters.AddWithValue("@remarks", obj.remarks);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }
        public Message AddUpdate(ApplicantAction obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spActionAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@actionId", obj.actionId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@specialityJobId", obj.specialityJobId);
            cmd.Parameters.AddWithValue("@image", obj.image);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@categoryId", obj.categoryId);
            cmd.Parameters.AddWithValue("@startDate", obj.startDate);
            cmd.Parameters.AddWithValue("@endDate", obj.endDate);
            cmd.Parameters.AddWithValue("@isDocsCollected", obj.isDocsCollected);
            cmd.Parameters.AddWithValue("@remarks", obj.remarks);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public ApplicantLeaveAction getLeaveData(int applicantId, int applicantLeaveId)
        {
            ApplicantLeaveAction leaveData = new ApplicantLeaveAction();
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGetLeaveData]"
            };
            SqlConnection con = new SqlConnection();
            con = new SqlConnection(PrpDbConnectADO.Conn);
            cmd.Parameters.AddWithValue("@applicantId", applicantId);
            cmd.Parameters.AddWithValue("@applicantLeaveId", applicantLeaveId);
            DataTable dt = new DataTable();
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            try {
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    leaveData.applicantId = Convert.ToInt32(dr[1]);
                    leaveData.typeId = Convert.ToInt32(dr[2]);
                    leaveData.startDate = Convert.ToDateTime(dr[3]);
                    leaveData.endDate = Convert.ToDateTime(dr[4]);
                    leaveData.image = dr[5].TooString();
                    leaveData.imageAffidavit = dr[6].TooString();
                    leaveData.dateRequested = Convert.ToDateTime(dr[7]);
                    leaveData.remarksRequested = dr[8].TooString();
                    leaveData.dateApproved = dr[9].TooString().TooDate();
                    leaveData.approvalRemarks = dr[10].TooString();
                    leaveData.noOfDays = dr[11].TooInt();
                    leaveData.approvedBy = dr[12].TooInt();
                    leaveData.requestedBy = dr[13].TooInt();
                    leaveData.approvalStatus = dr[14].TooInt();
                    leaveData.imageMedical = dr[15].TooString();
                    leaveData.imageMaternity = dr[16].TooString();
                    leaveData.imagePGAC = dr[17].TooString();
                    leaveData.ddlDoxTaken = dr[18].TooInt();
                    leaveData.imageForwarding = dr[19].TooString();
                    leaveData.imageSurety = dr[20].TooString();
                    leaveData.imageAttorney = dr[21].TooString();
                    leaveData.imageVisa = dr[22].TooString();
                    leaveData.imagePurpose = dr[23].TooString();
                    leaveData.requestedByName = dr[24].TooString();
                    leaveData.typeName = dr[25].TooString();
                    leaveData.approver = dr[26].TooString();

                }
            }
            catch (Exception ex)
            { 
            
            }
            
            return leaveData;
        }

        public List<ApplicantLeaveAction> getLeaveDataList(int applicantId)
        {
            List<ApplicantLeaveAction> leavesList = new List<ApplicantLeaveAction>();
            
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGetLeaveData]"
            };
            SqlConnection con = new SqlConnection();
            con = new SqlConnection(PrpDbConnectADO.Conn);
            cmd.Parameters.AddWithValue("@applicantId", applicantId);
            //cmd.Parameters.AddWithValue("@applicantLeaveId", 0);
            DataTable dt = new DataTable();
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            try
            {
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ApplicantLeaveAction leaveData = new ApplicantLeaveAction();
                        leaveData.applicantId = Convert.ToInt32(dr[1]);
                        leaveData.typeId = Convert.ToInt32(dr[2]);
                        leaveData.startDate = Convert.ToDateTime(dr[3]);
                        leaveData.endDate = Convert.ToDateTime(dr[4]);
                        leaveData.image = dr[5].TooString();
                        leaveData.imageAffidavit = dr[6].TooString();
                        leaveData.dateRequested = Convert.ToDateTime(dr[7]);
                        leaveData.remarksRequested = dr[8].TooString();
                        leaveData.dateApproved = dr[9].TooString().TooDate();
                        leaveData.approvalRemarks = dr[10].TooString();
                        leaveData.noOfDays = dr[11].TooInt();
                        leaveData.approvedBy = dr[12].TooInt();
                        leaveData.requestedBy = dr[13].TooInt();
                        leaveData.approvalStatus = dr[14].TooInt();
                        leaveData.imageMedical = dr[15].TooString();
                        leaveData.imageMaternity = dr[16].TooString();
                        leaveData.imagePGAC = dr[17].TooString();
                        leaveData.ddlDoxTaken = dr[18].TooInt();
                        leaveData.imageForwarding = dr[19].TooString();
                        leaveData.imageSurety = dr[20].TooString();
                        leaveData.imageAttorney = dr[21].TooString();
                        leaveData.imageVisa = dr[22].TooString();
                        leaveData.imagePurpose = dr[23].TooString();
                        leaveData.requestedByName = dr[24].TooString();
                        leaveData.typeName = dr[25].TooString();
                        leaveData.approver = dr[26].TooString();
                        leavesList.Add(leaveData);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return leavesList;
        }
        public ApplicantExtensionAction getExtensionData(int applicantId, int applicantLeaveId)
        {
            ApplicantExtensionAction leaveData = new ApplicantExtensionAction();
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGetExtensionData]"
            };
            SqlConnection con = new SqlConnection();
            con = new SqlConnection(PrpDbConnectADO.Conn);
            cmd.Parameters.AddWithValue("@applicantId", applicantId);
            cmd.Parameters.AddWithValue("@applicantLeaveId", applicantLeaveId);
            DataTable dt = new DataTable();
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            try
            {
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    leaveData.applicantId = Convert.ToInt32(dr[1]);
                    leaveData.startDate = Convert.ToDateTime(dr[2]);
                    leaveData.endDate = Convert.ToDateTime(dr[3]);
                    leaveData.noOfDays = dr[4].TooInt();
                    leaveData.noOfMonths = dr[5].TooInt();
                    leaveData.approvalBySupervisor = dr[6].TooInt();
                    leaveData.imageApplication = dr[7].TooString();
                    leaveData.imagePER = dr[8].TooString();
                    leaveData.imageNOC = dr[9].TooString();
                    leaveData.imagePMDC = dr[10].TooString();
                    leaveData.imageExtensionOrder = dr[11].TooString();
                    leaveData.imageJoiningOrder = dr[12].TooString();
                    leaveData.imageDoc1 = dr[13].TooString();
                    leaveData.imageDoc2 = dr[14].TooString();
                    leaveData.requestedBy = dr[15].TooInt();
                    leaveData.dateRequested = Convert.ToDateTime(dr[16]);
                    leaveData.remarksRequested = dr[17].TooString();
                    leaveData.approvalStatus = dr[18].TooInt();
                    leaveData.approvedBy = dr[19].TooInt();
                    leaveData.dateApproved = dr[20].TooString().TooDate();
                    leaveData.approvalRemarks = dr[21].TooString();
                    leaveData.imageInductionOrder = dr[22].TooString();
                    leaveData.rtmcUhsNo = dr[23].TooString();
                    leaveData.imageTothc = dr[24].TooString();
                    leaveData.imageJoat = dr[25].TooString();
                    leaveData.requestedByName = dr[26].TooString();
                    leaveData.approver = dr[27].TooString();
                }
            }
            catch (Exception ex)
            {

            }

            return leaveData;
        }
        public List<ApplicantExtensionAction> getExtensionDataList(int applicantId)
        {
            List<ApplicantExtensionAction> leavesList = new List<ApplicantExtensionAction>();

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGetExtensionData]"
            };
            SqlConnection con = new SqlConnection();
            con = new SqlConnection(PrpDbConnectADO.Conn);
            cmd.Parameters.AddWithValue("@applicantId", applicantId);
            //cmd.Parameters.AddWithValue("@applicantLeaveId", 0);
            DataTable dt = new DataTable();
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            try
            {
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ApplicantExtensionAction leaveData = new ApplicantExtensionAction();
                        leaveData.applicantId = Convert.ToInt32(dr[1]);
                        leaveData.startDate = Convert.ToDateTime(dr[2]);
                        leaveData.endDate = Convert.ToDateTime(dr[3]);
                        leaveData.noOfDays = dr[4].TooInt();
                        leaveData.dateStart = leaveData.startDate.ToString("dd-MM-yyyy");
                        leaveData.dateEnd = leaveData.endDate.ToString("dd-MM-yyyy");
                        leaveData.noOfMonths = dr[5].TooInt();
                        leaveData.approvalBySupervisor = dr[6].TooInt();
                        leaveData.imageApplication = dr[7].TooString();
                        leaveData.imagePER = dr[8].TooString();
                        leaveData.imageNOC = dr[9].TooString();
                        leaveData.imagePMDC = dr[10].TooString();
                        leaveData.imageExtensionOrder = dr[11].TooString();
                        leaveData.imageJoiningOrder = dr[12].TooString();
                        leaveData.imageDoc1 = dr[13].TooString();
                        leaveData.imageDoc2 = dr[14].TooString();
                        leaveData.requestedBy = dr[15].TooInt();
                        leaveData.dateRequested = Convert.ToDateTime(dr[16]);
                        leaveData.remarksRequested = dr[17].TooString();
                        leaveData.approvalStatus = dr[18].TooInt();
                        leaveData.approvedBy = dr[19].TooInt();
                        leaveData.dateApproved = dr[20].TooString().TooDate();
                        leaveData.approvalRemarks = dr[21].TooString();
                        leaveData.imageInductionOrder = dr[22].TooString();
                        leaveData.rtmcUhsNo = dr[23].TooString();
                        leaveData.imageTothc = dr[24].TooString();
                        leaveData.imageJoat = dr[25].TooString();
                        leaveData.requestedByName = dr[26].TooString();
                        leaveData.approver = dr[27].TooString();
                        leavesList.Add(leaveData);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return leavesList;
        }
        public Message AddUpdateStatus(ApplicantAction obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spActionStatusAdd]"
            };
            cmd.Parameters.AddWithValue("@actionId", obj.actionId);
            cmd.Parameters.AddWithValue("@image", obj.image);
            cmd.Parameters.AddWithValue("@remarks", obj.remarks);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }
    }


}
