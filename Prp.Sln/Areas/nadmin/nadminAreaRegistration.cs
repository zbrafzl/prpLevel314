using System;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin
{
	public class nadminAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "nadmin";
			}
		}

		public nadminAreaRegistration()
		{
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute("LoginAdmin01", "admin", new { controller = "LoggedInAdmin", action = "Login", id = UrlParameter.Optional });
			context.MapRoute("LoginAdmin02", "admin/login", new { controller = "LoggedInAdmin", action = "Login", id = UrlParameter.Optional });
			context.MapRoute("AdminLogout", "admin/logout", new { controller = "LoggedInAdmin", action = "Logout", id = UrlParameter.Optional });
			context.MapRoute("DashboardAdmin", "admin/index", new { controller = "HomeAdmin", action = "HomePageAdmin", id = UrlParameter.Optional });
			context.MapRoute("DashboardStatusInductionWiseAdmin", "admin/dashboard-status", new { controller = "HomeAdmin", action = "DashboardInductionWise", id = UrlParameter.Optional });
			context.MapRoute("DsbAd_JoiningInstituteWise", "admin/dsb/joining-institute", new { controller = "HomeAdmin", action = "DsbJoiningInstitute", id = UrlParameter.Optional });
			context.MapRoute("Ad_JoiningInstituteWise", "admin/joining-institute", new { controller = "HomeAdmin", action = "DsbJoiningInstitute", id = UrlParameter.Optional });
			context.MapRoute("DsbAd_JoiningHospitalWise", "admin/dsb/joining-hospital", new { controller = "HomeAdmin", action = "DsbJoiningHospital", id = UrlParameter.Optional });
			context.MapRoute("AdminConstantSetup", "admin/constant-setup", new { controller = "CommonAdmin", action = "ConstantSetup", id = UrlParameter.Optional });
			context.MapRoute("AdminConstantManage", "admin/constant-manage", new { controller = "CommonAdmin", action = "ConstantManage", id = UrlParameter.Optional });
			context.MapRoute("AdminDepartmentSetup", "admin/department-setup", new { controller = "CommonAdmin", action = "DepartmentSetup", id = UrlParameter.Optional });
			context.MapRoute("AdminDepartmentManage", "admin/department-manage", new { controller = "CommonAdmin", action = "DepartmentManage", id = UrlParameter.Optional });
			context.MapRoute("AdminDepartmentAssociation", "admin/department-association", new { controller = "Hospitals", action = "DepartmentAssociation", id = UrlParameter.Optional });
			context.MapRoute("MenusAdminSetup", "admin/menu-setup", new { controller = "MenusAdmin", action = "Setup", id = UrlParameter.Optional });
			context.MapRoute("MenusAdminManage", "admin/menu-manage", new { controller = "MenusAdmin", action = "Manage", id = UrlParameter.Optional });
			context.MapRoute("MenusAdminAccessUserType", "admin/menu-access-type", new { controller = "MenusAdmin", action = "AccessUserType", id = UrlParameter.Optional });
			context.MapRoute("MenusAdminAccessUser", "admin/menu-access-user", new { controller = "MenusAdmin", action = "AccessUser", id = UrlParameter.Optional });
			context.MapRoute("UsersAdminSetup", "admin/user-setup", new { controller = "UsersAdmin", action = "Setup", id = UrlParameter.Optional });
			context.MapRoute("ChangePasswordAdmin", "admin/change-password", new { controller = "UsersAdmin", action = "ChangePassword", id = UrlParameter.Optional });
			context.MapRoute("UsersAdminManage", "admin/user-manage", new { controller = "UsersAdmin", action = "Manage", id = UrlParameter.Optional });
			context.MapRoute("AdmininstituteSetup", "admin/institute-setup", new { controller = "InstitueAdmin", action = "InstituteSetup", id = UrlParameter.Optional });
			context.MapRoute("AdmininstituteManage", "admin/institute-manage", new { controller = "InstitueAdmin", action = "InstituteManage", id = UrlParameter.Optional });
			context.MapRoute("AdminiHospitalsSetup", "admin/hospital-setup", new { controller = "Hospitals", action = "HospitalSetup", id = UrlParameter.Optional });
			context.MapRoute("AdminHospitalsManage", "admin/hospital-manage", new { controller = "Hospitals", action = "HospitalManage", id = UrlParameter.Optional });
			context.MapRoute("AdminHospitalBedsManagement", "admin/hospital-bed-management", new { controller = "Hospitals", action = "BedsManagement", id = UrlParameter.Optional });
			context.MapRoute("AdminHospitalDiscipline", "admin/hospital-discipline", new { controller = "Hospitals", action = "DisciplineAssociation", id = UrlParameter.Optional });
			context.MapRoute("AdminispecialitySetup", "admin/speciality-setup", new { controller = "SubjectAdmin", action = "SubjectSetup", id = UrlParameter.Optional });
			context.MapRoute("AdminspecialityManage", "admin/speciality-manage", new { controller = "SubjectAdmin", action = "SubjectManage", id = UrlParameter.Optional });
			context.MapRoute("DisciplineManage", "admin/discipline-manage", new { controller = "CommonAdmin", action = "DisciplineManage", id = UrlParameter.Optional });
			context.MapRoute("SpecialityJobSetup", "admin/speciality-job", new { controller = "SpecialityAdmin", action = "SpecialityJobSetup", id = UrlParameter.Optional });
			context.MapRoute("JobInstituteWise", "admin/institute-job", new { controller = "SpecialityAdmin", action = "JobInstituteWise", id = UrlParameter.Optional });
			context.MapRoute("EmployeeAdminSetup", "admin/employee-setup", new { controller = "EmployeeAdmin", action = "Setup", id = UrlParameter.Optional });
			context.MapRoute("EmployeeAdminManage", "admin/employee-manage", new { controller = "EmployeeAdmin", action = "Manage", id = UrlParameter.Optional });
			context.MapRoute("EmployeeManageExperience", "admin/employee-experience-manage", new { controller = "EmployeeAdmin", action = "ManageExperience", id = UrlParameter.Optional });
			context.MapRoute("EmployeeApplicantAssign", "admin/employee-applicant-association", new { controller = "EmployeeAdmin", action = "ApplicantAssign", id = UrlParameter.Optional });
			context.MapRoute("EmployeeSpecialization", "admin/employee-specialization", new { controller = "EmployeeAdmin", action = "ManageSpecialization", id = UrlParameter.Optional });
			context.MapRoute("AdminJobListing", "admin/job-list", new { controller = "SpecialityAdmin", action = "JobListing", id = UrlParameter.Optional });
			context.MapRoute("AdminJobListingApplication", "admin/job-list-application", new { controller = "SpecialityAdmin", action = "JobApplicationSearch", id = UrlParameter.Optional });
			context.MapRoute("AdminSMSSetup", "admin/sms-setup", new { controller = "CommonAdmin", action = "SMSSetup", id = UrlParameter.Optional });
			context.MapRoute("AdminSMSManage", "admin/sms-manage", new { controller = "CommonAdmin", action = "SMSManage", id = UrlParameter.Optional });
			context.MapRoute("ApplicantAddUpdate", "admin/applicant-setup", new { controller = "ApplicantAdmin", action = "ApplicantAddition", id = UrlParameter.Optional });
			context.MapRoute("ApplicantActionStatus", "admin/applicant-status-update", new { controller = "ApplicantAdmin", action = "ApplicantActionStatus", id = UrlParameter.Optional });
			context.MapRoute("ApplicantStatusComplete", "admin/applicant-status", new { controller = "ApplicantAdmin", action = "ApplicantStatus", id = UrlParameter.Optional });
			context.MapRoute("ApplicantAccountStatus", "admin/applicant-account-status", new { controller = "ApplicantAdmin", action = "ApplicantAccountStatus", id = UrlParameter.Optional });
			context.MapRoute("ApplicantApplicationStatus", "admin/applicant-application-status", new { controller = "ApplicantAdmin", action = "ApplicantApplicationStatus", id = UrlParameter.Optional });
			context.MapRoute("ApplicantAdminSearch", "admin/applicant-search", new { controller = "ApplicantAdmin", action = "ApplicantSearch", id = UrlParameter.Optional });
			context.MapRoute("ApplicantAdminSearchInducion", "admin/applicant-search-induction-{induction}", new { controller = "ApplicantAdmin", action = "ApplicantSearch", id = UrlParameter.Optional });
			context.MapRoute("ApplicantAdminPic", "admin/applicant-pic", new { controller = "ApplicantAdmin", action = "ApplicantPic", id = UrlParameter.Optional });
			context.MapRoute("ApplicationReviewAdmin", "admin/application-review", new { controller = "ApplicationReviewAdmin", action = "ApplicantList", id = UrlParameter.Optional });
			context.MapRoute("ApplicantViewDetail", "admin/application-view-detail", new { controller = "ApplicationReviewAdmin", action = "ApplicantViewDetail", id = UrlParameter.Optional });
			context.MapRoute("ApplicantViewDetailView", "admin/application-detail", new { controller = "ApplicationReviewAdmin", action = "ApplicantViewDetailView", id = UrlParameter.Optional });
			context.MapRoute("ApplicantViewOnlyView", "admin/application-view", new { controller = "ApplicationReviewAdmin", action = "ApplicantView", id = UrlParameter.Optional });
			context.MapRoute("ApplicantDatailViewCallCenter", "admin/applicant-detail", new { controller = "ApplicationReviewAdmin", action = "ApplicantDetail", id = UrlParameter.Optional });
			context.MapRoute("ApplicantDatailViewDebar", "admin/applicant-debar", new { controller = "ApplicationReviewAdmin", action = "ApplicantDebar", id = UrlParameter.Optional });
			context.MapRoute("ApplicantDatailViewCallCenterInduction", "admin/applicant-detail-induction-{induction}", new { controller = "ApplicationReviewAdmin", action = "ApplicantDetail", id = UrlParameter.Optional });
			context.MapRoute("VerificationTeam", "admin/applicant/verify-ke", new { controller = "ApplicationReviewAdmin", action = "ApplicantDetail", id = UrlParameter.Optional });
			context.MapRoute("SupportTeamQuery", "admin/query/addnew", new { controller = "ApplicantAdmin", action = "AddQuery", id = UrlParameter.Optional });
			context.MapRoute("SupportTeamAddQuery", "admin/query/add", new { controller = "ApplicantAdmin", action = "AddApplicantQuery", id = UrlParameter.Optional });
			context.MapRoute("VerificationTeamPhf", "admin/applicant/verify", new { controller = "ApplicationReviewAdmin", action = "ApplicantDetail", id = UrlParameter.Optional });
			context.MapRoute("AmmendmentProcess", "admin/applicant/ammendment", new { controller = "ApplicationReviewAdmin", action = "ApplicantDetail", id = UrlParameter.Optional });
			context.MapRoute("VerificationViewSingle", "admin/applicant/verify-ke-view", new { controller = "VerficationProcess", action = "VerificationView", id = UrlParameter.Optional });
			context.MapRoute("ApplicantListForVerificationWithDownload", "admin/applicant/verify-list-view", new { controller = "VerficationProcess", action = "ApplicantListForVerificationWithDownload", id = UrlParameter.Optional });
			context.MapRoute("VoucherList", "admin/voucher-list", new { controller = "VoucherAdmin", action = "VoucherList", id = UrlParameter.Optional });
			context.MapRoute("VoucherBankList", "admin/voucher-list-bank", new { controller = "VoucherAdmin", action = "VoucherBankList", id = UrlParameter.Optional });
			context.MapRoute("VoucherListAll", "admin/voucher-list-all", new { controller = "VoucherAdmin", action = "VoucherListAll", id = UrlParameter.Optional });
			context.MapRoute("VoucherBankVarifacation", "admin/voucher-bank-verification", new { controller = "VoucherAdmin", action = "VoucherListNotVerified", id = UrlParameter.Optional });
			context.MapRoute("VoucherDetail", "admin/voucher-detail", new { controller = "VoucherAdmin", action = "VoucherDetail", id = UrlParameter.Optional });
			context.MapRoute("EmailTempalteSetup", "admin/email-template-setup", new { controller = "EmailAndSmsAmin", action = "TemplateSetupEmail", id = UrlParameter.Optional });
			context.MapRoute("EmailTempalteManage", "admin/email-template-manage", new { controller = "EmailAndSmsAmin", action = "TemplateManageEmail", id = UrlParameter.Optional });
			context.MapRoute("SMSProcessQueryRun", "admin/sms-run-query", new { controller = "EmailAndSmsAmin", action = "SMSProcessQuery", id = UrlParameter.Optional });
			context.MapRoute("SMSProcessViewAdmin", "admin/sms-send-type", new { controller = "EmailAndSmsAmin", action = "SMSProcessView", id = UrlParameter.Optional });
			context.MapRoute("SMSProcessAdmin", "admin/applicant-send-sms", new { controller = "EmailAndSmsAmin", action = "SMSProcess", id = UrlParameter.Optional });
			context.MapRoute("SMSProcessSingleAdmin", "admin/applicant-send-sms-single", new { controller = "EmailAndSmsAmin", action = "SMSProcessSingle", id = UrlParameter.Optional });
			context.MapRoute("SMSProcessSingleRejectAdmin", "admin/applicant-send-sms-reject", new { controller = "EmailAndSmsAmin", action = "SMSProcessSingle", id = UrlParameter.Optional });
			context.MapRoute("EmailProcessAdmin", "admin/applicant-send-email", new { controller = "EmailAndSmsAmin", action = "EmailProcess", id = UrlParameter.Optional });
			context.MapRoute("EmailProcessAdminStart", "admin/email-send-start", new { controller = "EmailAndSmsAmin", action = "EmailProcessStart", id = UrlParameter.Optional });
			context.MapRoute("EmailProcessAdminByType", "admin/email-send-type", new { controller = "EmailAndSmsAmin", action = "EmailProcessView", id = UrlParameter.Optional });
			context.MapRoute("EmailProcessSingleAdmin", "admin/applicant-send-email-single", new { controller = "EmailAndSmsAmin", action = "EmailProcessSingle", id = UrlParameter.Optional });
			context.MapRoute("EmailProcessSingleAdminCustom", "admin/applicant-send-email-single-custom", new { controller = "EmailAndSmsAmin", action = "EmailProcessCustomeSingle", id = UrlParameter.Optional });
			context.MapRoute("EmailSMSAmendmentAlertSend", "admin/send-amendment-email", new { controller = "EmailAndSmsAmin", action = "EmailSMSAmendmentAlert", id = UrlParameter.Optional });
			context.MapRoute("EmailSMSGazetteMarksAlertSend", "admin/send-gazette-email", new { controller = "EmailAndSmsAmin", action = "EmailSMSGazetteMarksAlert", id = UrlParameter.Optional });
			context.MapRoute("EmailSMSMeritMarksAlertSend", "admin/send-merit-consent-email", new { controller = "EmailAndSmsAmin", action = "EmailSMSMeritMarksAlert", id = UrlParameter.Optional });
			context.MapRoute("EmailJoning", "admin/send-email-joining", new { controller = "EmailAndSmsAmin", action = "EmailSMSJoiningAlert", id = UrlParameter.Optional });
			context.MapRoute("EmailSMSSelectedCongratulationAlert", "admin/send-email-congratulation", new { controller = "EmailAndSmsAmin", action = "EmailSMSSelectedCongratulationAlert", id = UrlParameter.Optional });
			context.MapRoute("EmailSMSBulkSendingToRemaningOld", "admin/send-email-remaning-old", new { controller = "EmailAndSmsAmin", action = "EmailSMSBulkSendingToRemaning", id = UrlParameter.Optional });
			context.MapRoute("ContactStatus", "admin/contact-status", new { controller = "ContactUsAdmin", action = "ContactStatus", id = UrlParameter.Optional });
			context.MapRoute("ContactList", "admin/contact/question-list", new { controller = "ContactUsAdmin", action = "ContactListing", id = UrlParameter.Optional });
			context.MapRoute("ContactReply", "admin/contact/reply", new { controller = "ContactUsAdmin", action = "ContactAnswer", id = UrlParameter.Optional });
			context.MapRoute("EditExperienceAdmin", "admin/application-edit-experience", new { controller = "ApplicationUpdate", action = "ExperienceAdmin", id = UrlParameter.Optional });
			context.MapRoute("AdminApplicantMeritView", "admin/applicant-merit-check", new { controller = "MeritAdmin", action = "ApplicantPreference", id = UrlParameter.Optional });
			context.MapRoute("AdminApplicantMeritViewRound02", "admin/applicant-merit-check-round-02", new { controller = "MeritAdmin", action = "ApplicantMeritRound2", id = UrlParameter.Optional });
			context.MapRoute("AdminApplicantMeritViewRound03", "admin/applicant-merit-check-round-03", new { controller = "MeritAdmin", action = "ApplicantMeritRound3", id = UrlParameter.Optional });
			context.MapRoute("AdminApplicantMeritViewRound04", "admin/applicant-merit-check-round-04", new { controller = "MeritAdmin", action = "ApplicantMeritRound4", id = UrlParameter.Optional });
			context.MapRoute("AdminApplicantMeritViewRound05", "admin/applicant-merit-check-round-05", new { controller = "MeritAdmin", action = "ApplicantMeritRound5", id = UrlParameter.Optional });
			context.MapRoute("AdminInductionApplicantMeritViewRound02", "admin/applicant-merit-check-round-02-induction{induction}", new { controller = "MeritAdmin", action = "ApplicantMeritRound2", id = UrlParameter.Optional });
			context.MapRoute("AdminInductionApplicantMeritViewRound03", "admin/applicant-merit-check-round-03-induction-{induction}", new { controller = "MeritAdmin", action = "ApplicantMeritRound3", id = UrlParameter.Optional });
			context.MapRoute("AdminFeedbackList", "admin/feedback-list", new { controller = "FeedbackAdmin", action = "FeebBackList", id = UrlParameter.Optional });
			context.MapRoute("ContactAttendenceVerificationCurrent", "admin/attendence-verification-current", new { controller = "ContactUsAdmin", action = "ContactAttendenceCurrent", id = UrlParameter.Optional });
			context.MapRoute("ContactAttendenceGazetteCurrent", "admin/attendence-gazette-current", new { controller = "ContactUsAdmin", action = "ContactAttendenceCurrent", id = UrlParameter.Optional });
			context.MapRoute("ContactAttendenceverification", "admin/attendence-verification", new { controller = "ContactUsAdmin", action = "ContactAttendence", id = UrlParameter.Optional });
			context.MapRoute("ContactAttendencegazette", "admin/attendence-gazette", new { controller = "ContactUsAdmin", action = "ContactAttendence", id = UrlParameter.Optional });
			context.MapRoute("PrintAttendenceVerification", "admin/print-attendence-verification", new { controller = "ContactUsAdmin", action = "ContactAttendenceView", id = UrlParameter.Optional });
			context.MapRoute("PrintAttendenceGazette", "admin/print-attendence-gazette", new { controller = "ContactUsAdmin", action = "ContactAttendenceView", id = UrlParameter.Optional });
			context.MapRoute("AdminAttendenceMark", "admin/attendence-mark", new { controller = "ContactUsAdmin", action = "ContactAttendenceMark", id = UrlParameter.Optional });
			context.MapRoute("ContactStatusListCurrentVerification", "admin/attendence-list-verification", new { controller = "ContactUsAdmin", action = "ContactStatusListCurrent", id = UrlParameter.Optional });
			context.MapRoute("ContactStatusListCurrentGazette", "admin/attendence-list-gazette", new { controller = "ContactUsAdmin", action = "ContactStatusListCurrent", id = UrlParameter.Optional });
			context.MapRoute("ContactStatusListCurrentVerificationAll", "admin/attendence-list-all-verification", new { controller = "ContactUsAdmin", action = "ContactStatusListAll", id = UrlParameter.Optional });
			context.MapRoute("ContactStatusListCurrentGazetteAll", "admin/attendence-list-all-gazette", new { controller = "ContactUsAdmin", action = "ContactStatusListAll", id = UrlParameter.Optional });
			context.MapRoute("AdminGrievanceComments", "admin/contact-comments", new { controller = "ContactUsAdmin", action = "ContactAttendenceComment", id = UrlParameter.Optional });
			context.MapRoute("ContactAttendencePrintverification", "admin/print-status-verification", new { controller = "ContactUsAdmin", action = "ContactStatusPrintList", id = UrlParameter.Optional });
			context.MapRoute("ContactAttendencePrintgazette", "admin/print-status-gazette", new { controller = "ContactUsAdmin", action = "ContactStatusPrintList", id = UrlParameter.Optional });
			context.MapRoute("GrievanceAdminGazzette", "admin/grievance-gazzette", new { controller = "GrievanceAdmin", action = "GrievanceManage", id = UrlParameter.Optional });
			context.MapRoute("GrievanceAdminAttendenceRemarks", "admin/grievance-attendence-remarks", new { controller = "GrievanceAdmin", action = "GrievanceAttendenceRemarks", id = UrlParameter.Optional });
			context.MapRoute("GrievanceAdminMarksList", "admin/grievance-marks-list", new { controller = "GrievanceAdmin", action = "GrievanceGazetteMarksList", id = UrlParameter.Optional });
			context.MapRoute("GrievanceAdminMarksDetail", "admin/grievance-marks-detail", new { controller = "GrievanceAdmin", action = "GrievanceGazetteMarksDetail", id = UrlParameter.Optional });
			context.MapRoute("GrievanceAdminPrint", "admin/grievance-print", new { controller = "GrievanceAdmin", action = "GrievancePrint", id = UrlParameter.Optional });
			context.MapRoute("ApplicantJoinedInduction", "admin/applicant-join-list", new { controller = "JoiningAdmin", action = "ApplicantJoined", id = UrlParameter.Optional });
			context.MapRoute("ApplicantListFinalJoinList", "admin/applicant-join", new { controller = "JoiningAdmin", action = "ApplicantListFinal", id = UrlParameter.Optional });
			context.MapRoute("ApplicantJoiningFullList", "admin/applicant-all-join", new { controller = "JoiningAdmin", action = "ApplicantJoiningFullList", id = UrlParameter.Optional });
			context.MapRoute("ApplicantListFinalJoining", "admin/applicant-join-add", new { controller = "JoiningAdmin", action = "ApplicantTakeJoining", id = UrlParameter.Optional });
			context.MapRoute("ApplicantJoiningListByInstitute", "admin/joining-list-institute", new { controller = "JoiningAdmin", action = "ApplicantJoiningListHospInst", id = UrlParameter.Optional });
			context.MapRoute("ApplicantJoiningListByHospital", "admin/joining-list-hospital", new { controller = "JoiningAdmin", action = "ApplicantJoiningListHospInst", id = UrlParameter.Optional });
			context.MapRoute("ActionSetupResignation", "admin/reg-term-listing", new { controller = "ApplicantAction", action = "ActionLisiting", id = UrlParameter.Optional });
			context.MapRoute("ActionLisitingResignation", "admin/reg-term-setup", new { controller = "ApplicantAction", action = "ActionSetup", id = UrlParameter.Optional });
			context.MapRoute("ActionSetupFreez", "admin/freez-listing", new { controller = "ApplicantAction", action = "ActionLisiting", id = UrlParameter.Optional });
			context.MapRoute("ActionLisitingFreez", "admin/freez-setup", new { controller = "ApplicantAction", action = "ActionSetup", id = UrlParameter.Optional });
			context.MapRoute("ActionExtensionRequest", "admin/extension-setup", new { controller = "ApplicantAction", action = "ExtensionSetup", id = UrlParameter.Optional });
			context.MapRoute("ActionSetupExtension", "admin/extension-applicant-listing", new { controller = "ApplicantAction", action = "ExtenstionLisiting", id = UrlParameter.Optional });
			context.MapRoute("ActionExtensionApproval", "admin/extension-approval-setup", new { controller = "ApplicantAction", action = "ExtensionApprovalSetup", id = UrlParameter.Optional });
			context.MapRoute("ActionSetupExtensionApproval", "admin/extension-approval-listing", new { controller = "ApplicantAction", action = "ExtensionApprovalLisiting", id = UrlParameter.Optional });
			context.MapRoute("ActionLeaveRequest", "admin/leave-setup", new { controller = "ApplicantAction", action = "LeaveSetup", id = UrlParameter.Optional });
			context.MapRoute("ActionLeaveApproval", "admin/leave-approval-setup", new { controller = "ApplicantAction", action = "LeaveApprovalSetup", id = UrlParameter.Optional });
			context.MapRoute("ActionLeaveList", "admin/leaves-list", new { controller = "ApplicantAction", action = "LeavesListLisiting", id = UrlParameter.Optional });
			context.MapRoute("ActionLeavePrint", "admin/leave-print-setup", new { controller = "ApplicantAction", action = "LeavePrintSetup", id = UrlParameter.Optional });
			context.MapRoute("ActionSetupLeaves", "admin/leave-applicant-listing", new { controller = "ApplicantAction", action = "LeavesLisiting", id = UrlParameter.Optional });
			context.MapRoute("ActionSetupLeavesApproval", "admin/leave-approval-listing", new { controller = "ApplicantAction", action = "LeavesApprovalLisiting", id = UrlParameter.Optional });
			context.MapRoute("ApplicantDetail", "admin/hardship-applicant-status", new { controller = "Hardship", action = "ApplicantDetail", id = UrlParameter.Optional });
			context.MapRoute("SMSAdminSearch", "admin/sms-search", new { controller = "SMSAdmin", action = "SearchSms", id = UrlParameter.Optional });
			context.MapRoute("AdminRegionManage", "admin/region-manage", new { controller = "MasterSetups", action = "RegionManage", id = UrlParameter.Optional });
			context.MapRoute("AdminRegionSetup", "admin/region-setup", new { controller = "MasterSetups", action = "RegionSetup", id = UrlParameter.Optional });
			context.MapRoute("AdminResearchJournalApplicantList", "admin/research-journal-applicants", new { controller = "JournalAdmin", action = "JournalApplicants", id = UrlParameter.Optional });
			context.MapRoute("AdminResearchJournalManage", "admin/research-journal-manage", new { controller = "JournalAdmin", action = "JournalManage", id = UrlParameter.Optional });
			context.MapRoute("AdminResearchJournalSetup", "admin/research-journal-setup", new { controller = "JournalAdmin", action = "JournalSetup", id = UrlParameter.Optional });
			context.MapRoute("AdminTickerSetup", "admin/ticker-setup", new { controller = "CommonAdmin", action = "TickerSetup", id = UrlParameter.Optional });
			context.MapRoute("AdminTickerManage", "admin/ticker-manage", new { controller = "CommonAdmin", action = "TickerManage", id = UrlParameter.Optional });
			context.MapRoute("PrintApplicantListJoiningInstitute", "admin/print-applicant-join-institute", new { controller = "PrintAdmin", action = "PrintJoiningApplicantInstitute", id = UrlParameter.Optional });
			context.MapRoute("PrintApplicantAttachedHospital", "admin/print-applicant-attach-hospital", new { controller = "PrintAdmin", action = "PrintApplicantAttachedHospital", id = UrlParameter.Optional });
			context.MapRoute("ReportRptSeatsStatus", "admin/report-seat-status", new { controller = "Reportss", action = "SeatsReport", id = UrlParameter.Optional });
			context.MapRoute("ReportJoinedApplicantHospital", "admin/report-applicant-hospital", new { controller = "Reportss", action = "JoinedApplicantHospital", id = UrlParameter.Optional });
			context.MapRoute("ReportJoinedApplicantHospitalStatus", "admin/report-applicant-hospital-status", new { controller = "Reportss", action = "JoinedApplicantHospitalStatus", id = UrlParameter.Optional });
			context.MapRoute("TraineeAttachReportHospitalWise", "admin/hospital-trainee-status", new { controller = "Reportss", action = "TraineeAttachReportHospitalWise", id = UrlParameter.Optional });
			context.MapRoute("nadmin_default", "admin/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });
		}
	}
}