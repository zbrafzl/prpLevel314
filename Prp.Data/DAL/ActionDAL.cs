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
					DataRow item = dataTable.Rows[0];
					applicantLeaveAction.applicantLeaveId = Convert.ToInt32(item[0]);
					applicantLeaveAction.applicantId = Convert.ToInt32(item[1]);
					applicantLeaveAction.typeId = Convert.ToInt32(item[2]);
					applicantLeaveAction.startDate = Convert.ToDateTime(item[3]);
					applicantLeaveAction.endDate = Convert.ToDateTime(item[4]);
					applicantLeaveAction.image = item[5].TooString("");
					applicantLeaveAction.imageAffidavit = item[6].TooString("");
					applicantLeaveAction.dateRequested = Convert.ToDateTime(item[7]);
					applicantLeaveAction.remarksRequested = item[8].TooString("");
					applicantLeaveAction.dateApproved = item[9].TooString("").TooDate();
					applicantLeaveAction.approvalRemarks = item[10].TooString("");
					applicantLeaveAction.noOfDays = item[11].TooInt();
					applicantLeaveAction.approvedBy = item[12].TooInt();
					applicantLeaveAction.requestedBy = item[13].TooInt();
					applicantLeaveAction.approvalStatus = item[14].TooInt();
					applicantLeaveAction.imageMedical = item[15].TooString("");
					applicantLeaveAction.imageMaternity = item[16].TooString("");
					applicantLeaveAction.imagePGAC = item[17].TooString("");
					applicantLeaveAction.ddlDoxTaken = item[18].TooInt();
					applicantLeaveAction.imageForwarding = item[19].TooString("");
					applicantLeaveAction.imageSurety = item[20].TooString("");
					applicantLeaveAction.imageAttorney = item[21].TooString("");
					applicantLeaveAction.imageVisa = item[22].TooString("");
					applicantLeaveAction.imagePurpose = item[23].TooString("");
					try
					{
						DateTime dateTime = Convert.ToDateTime(item[24]);
						applicantLeaveAction.edd = new DateTime?(dateTime.ToString("dd/MM/yyyy").TooDate());
					}
					catch (Exception exception)
					{
						applicantLeaveAction.edd = null;
					}
					applicantLeaveAction.approvalBySO = item[25].TooInt();
					applicantLeaveAction.approvalByDS = item[26].TooInt();
					applicantLeaveAction.approvalByAST = item[27].TooInt();
					applicantLeaveAction.approvalBySS = item[28].TooInt();
					applicantLeaveAction.approvalBySec = item[29].TooInt();
					applicantLeaveAction.remarksBySO = item[30].TooString("");
					applicantLeaveAction.remarksByDS = item[31].TooString("");
					applicantLeaveAction.remarksByAST = item[32].TooString("");
					applicantLeaveAction.cnicFrontImage = item[33].TooString("");
					applicantLeaveAction.cnicBackImage = item[34].TooString("");
					applicantLeaveAction.cnicFrontValidity = item[35].TooBoolean(false);
					applicantLeaveAction.cnicBackValidity = item[36].TooBoolean(false);
					applicantLeaveAction.cnicValidity = item[37].TooBoolean(false);
					applicantLeaveAction.applicationValidity = item[38].TooBoolean(false);
					applicantLeaveAction.affidavitValidity = item[39].TooBoolean(false);
					applicantLeaveAction.medicalValidity = item[40].TooBoolean(false);
					applicantLeaveAction.matenrityValidity = item[41].TooBoolean(false);
					applicantLeaveAction.forwardingValidity = item[42].TooBoolean(false);
					applicantLeaveAction.ipgacValidity = item[43].TooBoolean(false);
					applicantLeaveAction.suretyValidity = item[44].TooBoolean(false);
					applicantLeaveAction.attorneyValidity = item[45].TooBoolean(false);
					applicantLeaveAction.purposeValidity = item[46].TooBoolean(false);
					applicantLeaveAction.cnicFrontRemarks = item[47].TooString("");
					applicantLeaveAction.cnicBackRemarks = item[48].TooString("");
					applicantLeaveAction.applicationRemarks = item[49].TooString("");
					applicantLeaveAction.affidavitRemarks = item[50].TooString("");
					applicantLeaveAction.medicalRemarks = item[51].TooString("");
					applicantLeaveAction.matenrityRemarks = item[52].TooString("");
					applicantLeaveAction.forwardingRemarks = item[53].TooString("");
					applicantLeaveAction.ipgacRemarks = item[54].TooString("");
					applicantLeaveAction.suretyRemarks = item[55].TooString("");
					applicantLeaveAction.attorneyRemarks = item[56].TooString("");
					applicantLeaveAction.purposeRemarks = item[57].TooString("");
					applicantLeaveAction.forwardedTo = item[58].TooInt();
					applicantLeaveAction.visaValidity = item[59].TooBoolean(false);
					applicantLeaveAction.visaRemarks = item[60].TooString("");
					applicantLeaveAction.remarksBySS = item[61].TooString("");
					applicantLeaveAction.imageRTMC = item[63].TooString("");
					applicantLeaveAction.RTMCValidity = item[64].TooBoolean(false);
					applicantLeaveAction.RTMCRemarks = item[65].TooString("");
					applicantLeaveAction.requestedByName = item["userName"].TooString("");
					applicantLeaveAction.typeName = item["typeName"].TooString("");
					applicantLeaveAction.approver = item["approver"].TooString("");
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
							applicantLeaveId = Convert.ToInt32(row[0]),
							applicantId = Convert.ToInt32(row[1]),
							typeId = Convert.ToInt32(row[2]),
							startDate = Convert.ToDateTime(row[3]),
							endDate = Convert.ToDateTime(row[4]),
							image = row[5].TooString(""),
							imageAffidavit = row[6].TooString(""),
							dateRequested = Convert.ToDateTime(row[7]),
							remarksRequested = row[8].TooString(""),
							dateApproved = row[9].TooString("").TooDate(),
							approvalRemarks = row[10].TooString(""),
							noOfDays = row[11].TooInt(),
							approvedBy = row[12].TooInt(),
							requestedBy = row[13].TooInt(),
							approvalStatus = row[14].TooInt(),
							imageMedical = row[15].TooString(""),
							imageMaternity = row[16].TooString(""),
							imagePGAC = row[17].TooString(""),
							ddlDoxTaken = row[18].TooInt(),
							imageForwarding = row[19].TooString(""),
							imageSurety = row[20].TooString(""),
							imageAttorney = row[21].TooString(""),
							imageVisa = row[22].TooString(""),
							imagePurpose = row[23].TooString("")
						};
						try
						{
							string thedString = row[24].TooString();
							DateTime dd = thedString.TooDate();
							applicantLeaveAction.edd = new DateTime?(Convert.ToDateTime(row[24]).ToString("dd/MM/yyyy").TooDate());
						}
						catch (Exception exception)
						{
							applicantLeaveAction.edd = null;
						}
						applicantLeaveAction.approvalBySO = row[25].TooInt();
						applicantLeaveAction.approvalByDS = row[26].TooInt();
						applicantLeaveAction.approvalByAST = row[27].TooInt();
						applicantLeaveAction.approvalBySS = row[28].TooInt();
						applicantLeaveAction.approvalBySec = row[29].TooInt();
						applicantLeaveAction.remarksBySO = row[30].TooString("");
						applicantLeaveAction.remarksByDS = row[31].TooString("");
						applicantLeaveAction.remarksByAST = row[32].TooString("");
						applicantLeaveAction.cnicFrontImage = row[33].TooString("");
						applicantLeaveAction.cnicBackImage = row[34].TooString("");
						applicantLeaveAction.cnicFrontValidity = row[35].TooBoolean(false);
						applicantLeaveAction.cnicBackValidity = row[36].TooBoolean(false);
						applicantLeaveAction.cnicValidity = row[37].TooBoolean(false);
						applicantLeaveAction.applicationValidity = row[38].TooBoolean(false);
						applicantLeaveAction.affidavitValidity = row[39].TooBoolean(false);
						applicantLeaveAction.medicalValidity = row[40].TooBoolean(false);
						applicantLeaveAction.matenrityValidity = row[41].TooBoolean(false);
						applicantLeaveAction.forwardingValidity = row[42].TooBoolean(false);
						applicantLeaveAction.ipgacValidity = row[43].TooBoolean(false);
						applicantLeaveAction.suretyValidity = row[44].TooBoolean(false);
						applicantLeaveAction.attorneyValidity = row[45].TooBoolean(false);
						applicantLeaveAction.purposeValidity = row[46].TooBoolean(false);
						applicantLeaveAction.cnicFrontRemarks = row[47].TooString("");
						applicantLeaveAction.cnicBackRemarks = row[48].TooString("");
						applicantLeaveAction.applicationRemarks = row[49].TooString("");
						applicantLeaveAction.affidavitRemarks = row[50].TooString("");
						applicantLeaveAction.medicalRemarks = row[51].TooString("");
						applicantLeaveAction.matenrityRemarks = row[52].TooString("");
						applicantLeaveAction.forwardingRemarks = row[53].TooString("");
						applicantLeaveAction.ipgacRemarks = row[54].TooString("");
						applicantLeaveAction.suretyRemarks = row[55].TooString("");
						applicantLeaveAction.attorneyRemarks = row[56].TooString("");
						applicantLeaveAction.purposeRemarks = row[57].TooString("");
						applicantLeaveAction.forwardedTo = row[58].TooInt();
						applicantLeaveAction.visaValidity = row[59].TooBoolean(false);
						applicantLeaveAction.visaRemarks = row[60].TooString("");
						applicantLeaveAction.remarksBySS = row[61].TooString("");
						applicantLeaveAction.imageRTMC = row[63].TooString("");
						applicantLeaveAction.RTMCValidity = row[64].TooBoolean(false);
						applicantLeaveAction.RTMCRemarks = row[65].TooString("");
						applicantLeaveAction.requestedByName = row["userName"].TooString("");
						applicantLeaveAction.typeName = row["typeName"].TooString("");
						applicantLeaveAction.approver = row["approver"].TooString("");
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