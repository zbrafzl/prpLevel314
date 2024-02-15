using Prp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Prp.Data
{
    public class ActionDAL : PrpDBConnect
    {
        public ActionDAL()
        {
        }

        public Message AddUpdate(ApplicantAction obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spActionAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@actionId", obj.actionId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@specialityJobId", obj.specialityJobId);
            sqlCommand.Parameters.AddWithValue("@image", obj.image);
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
            sqlCommand.Parameters.AddWithValue("@categoryId", obj.categoryId);
            sqlCommand.Parameters.AddWithValue("@startDate", obj.startDate);
            sqlCommand.Parameters.AddWithValue("@endDate", obj.endDate);
            sqlCommand.Parameters.AddWithValue("@isDocsCollected", obj.isDocsCollected);
            sqlCommand.Parameters.AddWithValue("@remarks", obj.remarks);
            sqlCommand.Parameters.AddWithValue("@statusId", obj.statusId);
            sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
        }

        public Message AddUpdateExtension(ApplicantExtensionAction obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spExtensionAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@applicantExtensionId", obj.applicantExtensionId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@imageApplicantion", obj.imageApplication);
            sqlCommand.Parameters.AddWithValue("@imagePER", obj.imagePER);
            sqlCommand.Parameters.AddWithValue("@imageNOC", obj.imageNOC);
            sqlCommand.Parameters.AddWithValue("@imagePMDC", obj.imagePMDC);
            sqlCommand.Parameters.AddWithValue("@imageExtensionOrder", obj.imageExtensionOrder);
            sqlCommand.Parameters.AddWithValue("@imageJoiningOrder", obj.imageJoiningOrder);
            sqlCommand.Parameters.AddWithValue("@imageDoc1", obj.imageDoc1);
            sqlCommand.Parameters.AddWithValue("@imageDoc2", obj.imageDoc2);
            sqlCommand.Parameters.AddWithValue("@approvalBySupervisor", obj.approvalBySupervisor);
            sqlCommand.Parameters.AddWithValue("@startDate", obj.startDate);
            sqlCommand.Parameters.AddWithValue("@endDate", obj.endDate);
            sqlCommand.Parameters.AddWithValue("@remarks", obj.remarksRequested);
            sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
            sqlCommand.Parameters.AddWithValue("@noMonths", obj.noOfMonths);
            sqlCommand.Parameters.AddWithValue("@imageInductionOrder", obj.imageInductionOrder);
            sqlCommand.Parameters.AddWithValue("@rtmcUhsNo", obj.rtmcUhsNo);
            sqlCommand.Parameters.AddWithValue("@imageTothc", obj.imageTothc);
            sqlCommand.Parameters.AddWithValue("@imageJoat", obj.imageJoat);
            return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
        }

        public Message AddUpdateExtensionApproval(ApplicantExtensionAction obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spExtensionApprovalAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@actionId", obj.actionId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@applicantLeaveId", obj.applicantExtensionId);
            sqlCommand.Parameters.AddWithValue("@remarks", obj.remarks);
            sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
        }

        public Message LeaveValidate(Leave obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spLeaveValidate]"
            };
            sqlCommand.Parameters.AddWithValue("@leaveId", obj.leaveId);
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@startDate", obj.startDate);
            sqlCommand.Parameters.AddWithValue("@endDate", obj.endDate);
            sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(sqlCommand);
        }

        public Message LeaveAddUpdate(Leave obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spLeaveAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@leaveId", obj.leaveId);
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@startDate", obj.startDate);
            sqlCommand.Parameters.AddWithValue("@endDate", obj.endDate);

            
            sqlCommand.Parameters.AddWithValue("@assignTo", obj.assignTo);
            sqlCommand.Parameters.AddWithValue("@remarks", obj.remarks);
            sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);

            SqlParameter param = new SqlParameter("@tbList", SqlDbType.Structured)
            {
                TypeName = SqlDataTypes.tbType,
                Value = obj.dataTable
            };
            sqlCommand.Parameters.Add(param);
            return PrpDbADO.FillDataTableMessage(sqlCommand);
        }
        public Message AddUpdateLeave(ApplicantLeaveAction obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spLeaveAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@applicantLeaveId", obj.applicantLeaveId);
            sqlCommand.Parameters.AddWithValue("@actionId", obj.actionId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@image", obj.image);
            sqlCommand.Parameters.AddWithValue("@imageAffidavit", obj.imageAffidavit);
            sqlCommand.Parameters.AddWithValue("@imageMedical", obj.imageMedical);
            sqlCommand.Parameters.AddWithValue("@imageMaternity", obj.imageMaternity);
            sqlCommand.Parameters.AddWithValue("@imagePGAC", obj.imagePGAC);
            sqlCommand.Parameters.AddWithValue("@imageForwarding", obj.imageForwarding);
            sqlCommand.Parameters.AddWithValue("@imageSurety", obj.imageSurety);
            sqlCommand.Parameters.AddWithValue("@ddlDoxTaken", obj.ddlDoxTaken);
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
            sqlCommand.Parameters.AddWithValue("@categoryId", obj.categoryId);
            sqlCommand.Parameters.AddWithValue("@startDate", obj.startDate);
            sqlCommand.Parameters.AddWithValue("@endDate", obj.endDate);
            sqlCommand.Parameters.AddWithValue("@isDocsCollected", obj.isDocsCollected);
            sqlCommand.Parameters.AddWithValue("@remarks", obj.remarks);
            sqlCommand.Parameters.AddWithValue("@statusId", obj.statusId);
            sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
            sqlCommand.Parameters.AddWithValue("@imageAttorney", obj.imageAttorney);
            sqlCommand.Parameters.AddWithValue("@imageVisa", obj.imageVisa);
            sqlCommand.Parameters.AddWithValue("@imagePurpose", obj.imagePurpose);
            sqlCommand.Parameters.AddWithValue("@imageRTMC", obj.imageRTMC);
            sqlCommand.Parameters.AddWithValue("@imagePreviousLeaveReport", obj.imagePreviousLeaveReport);
            sqlCommand.Parameters.AddWithValue("@edd", obj.edd);
            return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
        }

        public Message AddUpdateLeaveApproval(ApplicantLeaveAction obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spLeaveApprovalAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@actionId", obj.actionId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@applicantLeaveId", obj.applicantLeaveId);
            sqlCommand.Parameters.AddWithValue("@remarks", obj.remarks);
            sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
        }

        public Message AddUpdateStatus(ApplicantAction obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spActionStatusAdd]"
            };
            sqlCommand.Parameters.AddWithValue("@actionId", obj.actionId);
            sqlCommand.Parameters.AddWithValue("@image", obj.image);
            sqlCommand.Parameters.AddWithValue("@remarks", obj.remarks);
            sqlCommand.Parameters.AddWithValue("@statusId", obj.statusId);
            sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
        }

        public ApplicantAction GetById(int applicantId, int typeId)
        {
            ApplicantAction applicantAction = new ApplicantAction();
            try
            {
                spApplicantActionGetByApplicantId_Result spApplicantActionGetByApplicantIdResult = this.db.spApplicantActionGetByApplicantId(new int?(applicantId), new int?(typeId)).FirstOrDefault<spApplicantActionGetByApplicantId_Result>();
                applicantAction = MapResignation.ToEntity(spApplicantActionGetByApplicantIdResult);
            }
            catch (Exception exception)
            {
                applicantAction = new ApplicantAction();
            }
            return applicantAction;
        }

        public ApplicantExtensionAction getExtensionData(int applicantId, int applicantLeaveId)
        {
            ApplicantExtensionAction applicantExtensionAction = new ApplicantExtensionAction();
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGetExtensionData]"
            };
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = new SqlConnection(PrpDbConnectADO.Conn);
            sqlCommand.Parameters.AddWithValue("@applicantId", applicantId);
            sqlCommand.Parameters.AddWithValue("@applicantLeaveId", applicantLeaveId);
            DataTable dataTable = new DataTable();
            sqlCommand.Connection = sqlConnection;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            try
            {
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow item = dataTable.Rows[0];
                    applicantExtensionAction.applicantExtensionId = Convert.ToInt32(item[0]);
                    applicantExtensionAction.applicantId = Convert.ToInt32(item[1]);
                    applicantExtensionAction.startDate = Convert.ToDateTime(item[2]);
                    applicantExtensionAction.endDate = Convert.ToDateTime(item[3]);
                    applicantExtensionAction.noOfDays = item[4].TooInt();
                    applicantExtensionAction.noOfMonths = item[5].TooInt();
                    applicantExtensionAction.approvalBySupervisor = item[6].TooInt();
                    applicantExtensionAction.imageApplication = item[7].TooString("");
                    applicantExtensionAction.imagePER = item[8].TooString("");
                    applicantExtensionAction.imageNOC = item[9].TooString("");
                    applicantExtensionAction.imagePMDC = item[10].TooString("");
                    applicantExtensionAction.imageExtensionOrder = item[11].TooString("");
                    applicantExtensionAction.imageJoiningOrder = item[12].TooString("");
                    applicantExtensionAction.imageDoc1 = item[13].TooString("");
                    applicantExtensionAction.imageDoc2 = item[14].TooString("");
                    applicantExtensionAction.requestedBy = item[15].TooInt();
                    applicantExtensionAction.dateRequested = Convert.ToDateTime(item[16]);
                    applicantExtensionAction.remarksRequested = item[17].TooString("");
                    applicantExtensionAction.approvalStatus = item[18].TooInt();
                    applicantExtensionAction.approvedBy = item[19].TooInt();
                    try
                    {
                        DateTime dateTime = Convert.ToDateTime(item[20]);
                        applicantExtensionAction.dateApproved = dateTime.ToString("dd/MM/yyyy").TooDate();
                    }
                    catch (Exception exception)
                    {
                        applicantExtensionAction.dateApproved = item[20].TooString("").TooDate();
                    }
                    applicantExtensionAction.approvalRemarks = item[21].TooString("");
                    applicantExtensionAction.imageInductionOrder = item[22].TooString("");
                    applicantExtensionAction.rtmcUhsNo = item[23].TooString("");
                    applicantExtensionAction.imageTothc = item[24].TooString("");
                    applicantExtensionAction.imageJoat = item[25].TooString("");
                    applicantExtensionAction.approvalByNawaz = item[26].TooInt();
                    applicantExtensionAction.approvalBySO = item[27].TooInt();
                    applicantExtensionAction.approvalByDS = item[28].TooInt();
                    applicantExtensionAction.approvalByAST = item[29].TooInt();
                    applicantExtensionAction.approvalBySS = item[30].TooInt();
                    applicantExtensionAction.approvalBySec = item[31].TooInt();
                    applicantExtensionAction.remarksByNawaz = item[32].TooString("");
                    applicantExtensionAction.remarksBySO = item[33].TooString("");
                    applicantExtensionAction.remarksByDS = item[34].TooString("");
                    applicantExtensionAction.remarksByAST = item[35].TooString("");
                    applicantExtensionAction.remarksBySec = item[36].TooString("");
                    applicantExtensionAction.imageApplicationRemarks = item[37].TooString("");
                    applicantExtensionAction.imagePERRemarks = item[38].TooString("");
                    applicantExtensionAction.imageNOCRemarks = item[39].TooString("");
                    applicantExtensionAction.imagePMDCRemarks = item[40].TooString("");
                    applicantExtensionAction.imageExtensionOrderRemarks = item[41].TooString("");
                    applicantExtensionAction.imageJoiningOrderRemarks = item[42].TooString("");
                    applicantExtensionAction.imageDoc1Remarks = item[43].TooString("");
                    applicantExtensionAction.imageDoc2Remarks = item[44].TooString("");
                    applicantExtensionAction.imageInductionOrderRemarks = item[45].TooString("");
                    applicantExtensionAction.rtmcUhsNoRemarks = item[46].TooString("");
                    applicantExtensionAction.imageTothcRemarks = item[47].TooString("");
                    applicantExtensionAction.imageJoatRemarks = item[48].TooString("");
                    applicantExtensionAction.imageApplicationValidity = item[49].TooBoolean(false);
                    applicantExtensionAction.imagePERValidity = item[50].TooBoolean(false);
                    applicantExtensionAction.imageNOCValidity = item[51].TooBoolean(false);
                    applicantExtensionAction.imagePMDCValidity = item[52].TooBoolean(false);
                    applicantExtensionAction.imageExtensionOrderValidity = item[53].TooBoolean(false);
                    applicantExtensionAction.imageJoiningOrderValidity = item[54].TooBoolean(false);
                    applicantExtensionAction.imageDoc1Validity = item[55].TooBoolean(false);
                    applicantExtensionAction.imageDoc2Validity = item[56].TooBoolean(false);
                    applicantExtensionAction.imageInductionOrderValidity = item[57].TooBoolean(false);
                    applicantExtensionAction.rtmcUhsNoValidity = item[58].TooBoolean(false);
                    applicantExtensionAction.imageTothcValidity = item[59].TooBoolean(false);
                    applicantExtensionAction.imageJoatValidity = item[60].TooBoolean(false);
                    applicantExtensionAction.forwardedTo = item[61].TooInt();
                    applicantExtensionAction.requestedByName = item["userName"].TooString("");
                    applicantExtensionAction.approver = item["approver"].TooString("");
                }
            }
            catch (Exception exception1)
            {
            }
            return applicantExtensionAction;
        }

        public List<ApplicantExtensionAction> getExtensionDataList(int applicantId)
        {
            List<ApplicantExtensionAction> applicantExtensionActions = new List<ApplicantExtensionAction>();
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGetExtensionData]"
            };
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = new SqlConnection(PrpDbConnectADO.Conn);
            sqlCommand.Parameters.AddWithValue("@applicantId", applicantId);
            DataTable dataTable = new DataTable();
            sqlCommand.Connection = sqlConnection;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            try
            {
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ApplicantExtensionAction applicantExtensionAction = new ApplicantExtensionAction()
                        {
                            applicantExtensionId = Convert.ToInt32(row[0]),
                            applicantId = Convert.ToInt32(row[1]),
                            startDate = Convert.ToDateTime(row[2]),
                            endDate = Convert.ToDateTime(row[3]),
                            noOfDays = row[4].TooInt()
                        };
                        DateTime dateTime = applicantExtensionAction.startDate;
                        applicantExtensionAction.dateStart = dateTime.ToString("dd-MM-yyyy");
                        dateTime = applicantExtensionAction.endDate;
                        applicantExtensionAction.dateEnd = dateTime.ToString("dd-MM-yyyy");
                        applicantExtensionAction.noOfMonths = row[5].TooInt();
                        applicantExtensionAction.approvalBySupervisor = row[6].TooInt();
                        applicantExtensionAction.imageApplication = row[7].TooString("");
                        applicantExtensionAction.imagePER = row[8].TooString("");
                        applicantExtensionAction.imageNOC = row[9].TooString("");
                        applicantExtensionAction.imagePMDC = row[10].TooString("");
                        applicantExtensionAction.imageExtensionOrder = row[11].TooString("");
                        applicantExtensionAction.imageJoiningOrder = row[12].TooString("");
                        applicantExtensionAction.imageDoc1 = row[13].TooString("");
                        applicantExtensionAction.imageDoc2 = row[14].TooString("");
                        applicantExtensionAction.requestedBy = row[15].TooInt();
                        applicantExtensionAction.dateRequested = Convert.ToDateTime(row[16]);
                        applicantExtensionAction.remarksRequested = row[17].TooString("");
                        applicantExtensionAction.approvalStatus = row[18].TooInt();
                        applicantExtensionAction.approvedBy = row[19].TooInt();
                        try
                        {
                            dateTime = Convert.ToDateTime(row[20]);
                            applicantExtensionAction.dateApproved = dateTime.ToString("dd/MM/yyyy").TooDate();
                        }
                        catch (Exception exception)
                        {
                            applicantExtensionAction.dateApproved = row[20].TooString("").TooDate();
                        }
                        applicantExtensionAction.approvalRemarks = row[21].TooString("");
                        applicantExtensionAction.imageInductionOrder = row[22].TooString("");
                        applicantExtensionAction.rtmcUhsNo = row[23].TooString("");
                        applicantExtensionAction.imageTothc = row[24].TooString("");
                        applicantExtensionAction.imageJoat = row[25].TooString("");
                        applicantExtensionAction.approvalByNawaz = row[26].TooInt();
                        applicantExtensionAction.approvalBySO = row[27].TooInt();
                        applicantExtensionAction.approvalByDS = row[28].TooInt();
                        applicantExtensionAction.approvalByAST = row[29].TooInt();
                        applicantExtensionAction.approvalBySS = row[30].TooInt();
                        applicantExtensionAction.approvalBySec = row[31].TooInt();
                        applicantExtensionAction.remarksByNawaz = row[32].TooString("");
                        applicantExtensionAction.remarksBySO = row[33].TooString("");
                        applicantExtensionAction.remarksByDS = row[34].TooString("");
                        applicantExtensionAction.remarksByAST = row[35].TooString("");
                        applicantExtensionAction.remarksBySec = row[36].TooString("");
                        applicantExtensionAction.imageApplicationRemarks = row[37].TooString("");
                        applicantExtensionAction.imagePERRemarks = row[38].TooString("");
                        applicantExtensionAction.imageNOCRemarks = row[39].TooString("");
                        applicantExtensionAction.imagePMDCRemarks = row[40].TooString("");
                        applicantExtensionAction.imageExtensionOrderRemarks = row[41].TooString("");
                        applicantExtensionAction.imageJoiningOrderRemarks = row[42].TooString("");
                        applicantExtensionAction.imageDoc1Remarks = row[43].TooString("");
                        applicantExtensionAction.imageDoc2Remarks = row[44].TooString("");
                        applicantExtensionAction.imageInductionOrderRemarks = row[45].TooString("");
                        applicantExtensionAction.rtmcUhsNoRemarks = row[46].TooString("");
                        applicantExtensionAction.imageTothcRemarks = row[47].TooString("");
                        applicantExtensionAction.imageJoatRemarks = row[48].TooString("");
                        applicantExtensionAction.imageApplicationValidity = row[49].TooBoolean(false);
                        applicantExtensionAction.imagePERValidity = row[50].TooBoolean(false);
                        applicantExtensionAction.imageNOCValidity = row[51].TooBoolean(false);
                        applicantExtensionAction.imagePMDCValidity = row[52].TooBoolean(false);
                        applicantExtensionAction.imageExtensionOrderValidity = row[53].TooBoolean(false);
                        applicantExtensionAction.imageJoiningOrderValidity = row[54].TooBoolean(false);
                        applicantExtensionAction.imageDoc1Validity = row[55].TooBoolean(false);
                        applicantExtensionAction.imageDoc2Validity = row[56].TooBoolean(false);
                        applicantExtensionAction.imageInductionOrderValidity = row[57].TooBoolean(false);
                        applicantExtensionAction.rtmcUhsNoValidity = row[58].TooBoolean(false);
                        applicantExtensionAction.imageTothcValidity = row[59].TooBoolean(false);
                        applicantExtensionAction.imageJoatValidity = row[60].TooBoolean(false);
                        applicantExtensionAction.forwardedTo = row[61].TooInt();
                        applicantExtensionAction.requestedByName = row["userName"].TooString("");
                        applicantExtensionAction.approver = row["approver"].TooString("");
                        applicantExtensionActions.Add(applicantExtensionAction);
                    }
                }
            }
            catch (Exception exception1)
            {
            }
            return applicantExtensionActions;
        }
        #region Leave

        public DataSet LeaveDocsGetByParam(Leave obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spLeaveDocsGetByParam]"
            };
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
            sqlCommand.Parameters.AddWithValue("@leaveId", obj.leaveId);
            sqlCommand.Parameters.AddWithValue("@search", obj.search.TooString());
            return PrpDbADO.FillDataSet(sqlCommand);
        }

        public DataSet ApplicantLeaveInfoByParam(Leave obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantLeaveInfoByParam]"
            };
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@leaveId", obj.leaveId);
            sqlCommand.Parameters.AddWithValue("@search", obj.search.TooString());
            sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);

            return PrpDbADO.FillDataSet(sqlCommand);
        }



        #endregion





        public ApplicantLeaveAction getLeaveData(int applicantId, int applicantLeaveId)
        {
            ApplicantLeaveAction applicantLeaveAction = new ApplicantLeaveAction();
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGetLeaveData]"
            };
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = new SqlConnection(PrpDbConnectADO.Conn);
            sqlCommand.Parameters.AddWithValue("@applicantId", applicantId);
            sqlCommand.Parameters.AddWithValue("@applicantLeaveId", applicantLeaveId);
            DataTable dataTable = new DataTable();
            sqlCommand.Connection = sqlConnection;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            try
            {
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;
                    string dictionaryString = "{";
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dataTable.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                            dictionaryString += col.ColumnName + " : " + dr[col] + ", ";
                        }
                        rows.Add(row);
                    }
                    dictionaryString = dictionaryString.TrimEnd(',', ' ') + "}";
                    DataRow item = dataTable.Rows[0];

                    applicantLeaveAction.applicantLeaveId = Convert.ToInt32(item["applicantLeaveId"]);
                    applicantLeaveAction.applicantId = Convert.ToInt32(item["applicantId"]);
                    applicantLeaveAction.typeId = Convert.ToInt32(item["leaveTypeId"]);
                    applicantLeaveAction.startDate = Convert.ToDateTime(item["startDated"]);
                    applicantLeaveAction.endDate = Convert.ToDateTime(item["endDate"]);
                    applicantLeaveAction.image = item["imageLeaveForm"].TooString("");
                    applicantLeaveAction.imageAffidavit = item["imageAffidavit"].TooString("");
                    applicantLeaveAction.dateRequested = Convert.ToDateTime(item["datedRequested"]);
                    applicantLeaveAction.remarksRequested = item["remarksRequest"].TooString("");
                    applicantLeaveAction.dateApproved = item["datedApproval"].TooString("").TooDate();
                    applicantLeaveAction.approvalRemarks = item["remarksApproval"].TooString("");
                    applicantLeaveAction.noOfDays = item["noOfDays"].TooInt();
                    applicantLeaveAction.approvedBy = item["approvedBy"].TooInt();
                    applicantLeaveAction.requestedBy = item["requestedBy"].TooInt();
                    applicantLeaveAction.approvalStatus = item["approvalStatus"].TooInt();
                    applicantLeaveAction.imageMedical = item["imageMedical"].TooString("");
                    applicantLeaveAction.imageMaternity = item["imageMaternity"].TooString("");
                    applicantLeaveAction.imagePGAC = item["imagePGAC"].TooString("");
                    applicantLeaveAction.ddlDoxTaken = item["ddlDoxTaken"].TooInt();
                    applicantLeaveAction.imageForwarding = item["imageForwarding"].TooString("");
                    applicantLeaveAction.imageSurety = item["imageSurety"].TooString("");
                    applicantLeaveAction.imageAttorney = item["imageAttorney"].TooString("");
                    applicantLeaveAction.imageVisa = item["imageVisa"].TooString("");
                    applicantLeaveAction.imagePurpose = item["imagePurpose"].TooString("");
                    try
                    {
                        DateTime dateTime = Convert.ToDateTime(item["edd"]);
                        applicantLeaveAction.edd = new DateTime?(dateTime.ToString("dd/MM/yyyy").TooDate());
                    }
                    catch (Exception exception)
                    {
                        applicantLeaveAction.edd = null;
                    }
                    applicantLeaveAction.approvalBySO = item["approvalBySO"].TooInt();
                    applicantLeaveAction.approvalByDS = item["approvalByDS"].TooInt();
                    applicantLeaveAction.approvalByAST = item["approvalByAST"].TooInt();
                    applicantLeaveAction.approvalBySS = item["approvalBySS"].TooInt();
                    applicantLeaveAction.approvalBySec = item["approvalBySec"].TooInt();
                    applicantLeaveAction.remarksBySO = item["remarksBySO"].TooString("");
                    applicantLeaveAction.remarksByDS = item["remarksByDS"].TooString("");
                    applicantLeaveAction.remarksByAST = item["remarksByAST"].TooString("");
                    applicantLeaveAction.cnicFrontImage = item["cnicFrontImage"].TooString("");
                    applicantLeaveAction.cnicBackImage = item["cnicBackImage"].TooString("");
                    applicantLeaveAction.cnicFrontValidity = item["cnicFrontValidity"].TooBoolean(false);
                    applicantLeaveAction.cnicBackValidity = item["cnicBackValidity"].TooBoolean(false);
                    applicantLeaveAction.cnicValidity = item["cnicValidity"].TooBoolean(false);
                    applicantLeaveAction.applicationValidity = item["applicationValidity"].TooBoolean(false);
                    applicantLeaveAction.affidavitValidity = item["affidavitValidity"].TooBoolean(false);
                    applicantLeaveAction.medicalValidity = item["medicalValidity"].TooBoolean(false);
                    applicantLeaveAction.matenrityValidity = item["matenrityValidity"].TooBoolean(false);
                    applicantLeaveAction.forwardingValidity = item["forwardingValidity"].TooBoolean(false);
                    applicantLeaveAction.ipgacValidity = item["ipgacValidity"].TooBoolean(false);
                    applicantLeaveAction.suretyValidity = item["suretyValidity"].TooBoolean(false);
                    applicantLeaveAction.attorneyValidity = item["attorneyValidity"].TooBoolean(false);
                    applicantLeaveAction.purposeValidity = item["purposeValidity"].TooBoolean(false);
                    applicantLeaveAction.cnicFrontRemarks = item["cnicFrontRemarks"].TooString("");
                    applicantLeaveAction.cnicBackRemarks = item["cnicBackRemarks"].TooString("");
                    applicantLeaveAction.applicationRemarks = item["applicationRemarks"].TooString("");
                    applicantLeaveAction.affidavitRemarks = item["affidavitRemarks"].TooString("");
                    applicantLeaveAction.medicalRemarks = item["medicalRemarks"].TooString("");
                    applicantLeaveAction.matenrityRemarks = item["matenrityRemarks"].TooString("");
                    applicantLeaveAction.forwardingRemarks = item["forwardingRemarks"].TooString("");
                    applicantLeaveAction.ipgacRemarks = item["ipgacRemarks"].TooString("");
                    applicantLeaveAction.suretyRemarks = item["suretyRemarks"].TooString("");
                    applicantLeaveAction.attorneyRemarks = item["attorneyRemarks"].TooString("");
                    applicantLeaveAction.purposeRemarks = item["purposeRemarks"].TooString("");
                    applicantLeaveAction.forwardedTo = item["forwardedTo"].TooInt();
                    applicantLeaveAction.visaValidity = item["visaValidity"].TooBoolean(false);
                    applicantLeaveAction.visaRemarks = item["visaRemarks"].TooString("");
                    applicantLeaveAction.remarksBySS = item["remarksBySS"].TooString("");
                    applicantLeaveAction.imageRTMC = item["imageRTMC"].TooString("");
                    applicantLeaveAction.RTMCValidity = item["RTMCValidity"].TooBoolean(false);
                    applicantLeaveAction.RTMCRemarks = item["RTMCRemarks"].TooString("");

                    applicantLeaveAction.imagePreviousLeaveReport = item["imagePreviousLeaveReport"].TooString("");
                    applicantLeaveAction.imagePreviousLeaveReportValidity = item["imagePreviousLeaveReportValidy"].TooBoolean(false);
                    applicantLeaveAction.imagePreviousLeaveReportRemarks = item["imagePreviousLeaveReportRemarks"].TooString("");

                    applicantLeaveAction.requestedByName = item["userName"].TooString("");
                    applicantLeaveAction.typeName = item["typeName"].TooString("");
                    applicantLeaveAction.approver = item["approver"].TooString("");
                    applicantLeaveAction.issuedOrderStatus = item["issuedOrderStatus"].TooInt();
                }
            }
            catch (Exception exception1)
            {
            }
            return applicantLeaveAction;
        }

        public List<ApplicantLeaveAction> getLeaveDataList(int applicantId)
        {
            List<ApplicantLeaveAction> applicantLeaveActions = new List<ApplicantLeaveAction>();
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGetLeaveData]"
            };
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = new SqlConnection(PrpDbConnectADO.Conn);
            sqlCommand.Parameters.AddWithValue("@applicantId", applicantId);
            DataTable dataTable = new DataTable();
            sqlCommand.Connection = sqlConnection;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            try
            {
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ApplicantLeaveAction applicantLeaveAction = new ApplicantLeaveAction()
                        {
                            applicantLeaveId = Convert.ToInt32(row["applicantLeaveId"]),
                            applicantId = Convert.ToInt32(row["applicantId"]),
                            typeId = Convert.ToInt32(row["leaveTypeId"]),
                            startDate = Convert.ToDateTime(row["startDated"]),
                            endDate = Convert.ToDateTime(row["endDate"]),
                            image = row["imageLeaveForm"].TooString(""),
                            imageAffidavit = row["imageAffidavit"].TooString(""),
                            dateRequested = Convert.ToDateTime(row["datedRequested"]),
                            remarksRequested = row["remarksRequest"].TooString(""),
                            dateApproved = row["datedApproval"].TooString("").TooDate(),
                            approvalRemarks = row["remarksApproval"].TooString(""),
                            noOfDays = row["noOfDays"].TooInt(),
                            approvedBy = row["approvedBy"].TooInt(),
                            requestedBy = row["requestedBy"].TooInt(),
                            approvalStatus = row["approvalStatus"].TooInt(),
                            imageMedical = row["imageMedical"].TooString(""),
                            imageMaternity = row["imageMaternity"].TooString(""),
                            imagePGAC = row["imagePGAC"].TooString(""),
                            ddlDoxTaken = row["ddlDoxTaken"].TooInt(),
                            imageForwarding = row["imageForwarding"].TooString(""),
                            imageSurety = row["imageSurety"].TooString(""),
                            imageAttorney = row["imageAttorney"].TooString(""),
                            imageVisa = row["imageVisa"].TooString(""),
                            imagePurpose = row["imagePurpose"].TooString("")
                        };
                        try
                        {
                            string thedString = row["edd"].TooString();
                            DateTime dd = thedString.TooDate();
                            applicantLeaveAction.edd = new DateTime?(Convert.ToDateTime(row[24]).ToString("dd/MM/yyyy").TooDate());
                        }
                        catch (Exception exception)
                        {
                            applicantLeaveAction.edd = null;
                        }
                        applicantLeaveAction.approvalBySO = row["approvalBySO"].TooInt();
                        applicantLeaveAction.approvalByDS = row["approvalByDS"].TooInt();
                        applicantLeaveAction.approvalByAST = row["approvalByAST"].TooInt();
                        applicantLeaveAction.approvalBySS = row["approvalBySS"].TooInt();
                        applicantLeaveAction.approvalBySec = row["approvalBySec"].TooInt();
                        applicantLeaveAction.remarksBySO = row["remarksBySO"].TooString("");
                        applicantLeaveAction.remarksByDS = row["remarksByDS"].TooString("");
                        applicantLeaveAction.remarksByAST = row["remarksByAST"].TooString("");
                        applicantLeaveAction.cnicFrontImage = row["cnicFrontImage"].TooString("");
                        applicantLeaveAction.cnicBackImage = row["cnicBackImage"].TooString("");
                        applicantLeaveAction.cnicFrontValidity = row["cnicFrontValidity"].TooBoolean(false);
                        applicantLeaveAction.cnicBackValidity = row["cnicBackValidity"].TooBoolean(false);
                        applicantLeaveAction.cnicValidity = row["cnicValidity"].TooBoolean(false);
                        applicantLeaveAction.applicationValidity = row["applicationValidity"].TooBoolean(false);
                        applicantLeaveAction.affidavitValidity = row["affidavitValidity"].TooBoolean(false);
                        applicantLeaveAction.medicalValidity = row["medicalValidity"].TooBoolean(false);
                        applicantLeaveAction.matenrityValidity = row["matenrityValidity"].TooBoolean(false);
                        applicantLeaveAction.forwardingValidity = row["forwardingValidity"].TooBoolean(false);
                        applicantLeaveAction.ipgacValidity = row["ipgacValidity"].TooBoolean(false);
                        applicantLeaveAction.suretyValidity = row["suretyValidity"].TooBoolean(false);
                        applicantLeaveAction.attorneyValidity = row["attorneyValidity"].TooBoolean(false);
                        applicantLeaveAction.purposeValidity = row["purposeValidity"].TooBoolean(false);
                        applicantLeaveAction.cnicFrontRemarks = row["cnicFrontRemarks"].TooString("");
                        applicantLeaveAction.cnicBackRemarks = row["cnicBackRemarks"].TooString("");
                        applicantLeaveAction.applicationRemarks = row["applicationRemarks"].TooString("");
                        applicantLeaveAction.affidavitRemarks = row["affidavitRemarks"].TooString("");
                        applicantLeaveAction.medicalRemarks = row["medicalRemarks"].TooString("");
                        applicantLeaveAction.matenrityRemarks = row["matenrityRemarks"].TooString("");
                        applicantLeaveAction.forwardingRemarks = row["forwardingRemarks"].TooString("");
                        applicantLeaveAction.ipgacRemarks = row["ipgacRemarks"].TooString("");
                        applicantLeaveAction.suretyRemarks = row["suretyRemarks"].TooString("");
                        applicantLeaveAction.attorneyRemarks = row["attorneyRemarks"].TooString("");
                        applicantLeaveAction.purposeRemarks = row["purposeRemarks"].TooString("");
                        applicantLeaveAction.forwardedTo = row["forwardedTo"].TooInt();
                        applicantLeaveAction.visaValidity = row["visaValidity"].TooBoolean(false);
                        applicantLeaveAction.visaRemarks = row["visaRemarks"].TooString("");
                        applicantLeaveAction.remarksBySS = row["remarksBySS"].TooString("");
                        applicantLeaveAction.imageRTMC = row["imageRTMC"].TooString("");
                        applicantLeaveAction.RTMCValidity = row["RTMCValidity"].TooBoolean(false);
                        applicantLeaveAction.RTMCRemarks = row["RTMCRemarks"].TooString("");
                        applicantLeaveAction.imagePreviousLeaveReport = row["imagePreviousLeaveReport"].TooString("");
                        applicantLeaveAction.imagePreviousLeaveReportValidity = row["imagePreviousLeaveReportValidy"].TooBoolean(false);
                        applicantLeaveAction.imagePreviousLeaveReportRemarks = row["imagePreviousLeaveReportRemarks"].TooString("");
                        applicantLeaveAction.requestedByName = row["userName"].TooString("");
                        applicantLeaveAction.typeName = row["typeName"].TooString("");
                        applicantLeaveAction.approver = row["approver"].TooString("");
                        applicantLeaveAction.issuedOrderStatus = row["issuedOrderStatus"].TooInt();
                        applicantLeaveActions.Add(applicantLeaveAction);
                    }
                }
            }
            catch (Exception exception1)
            {
            }
            return applicantLeaveActions;
        }
    }
}