using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prp.Model;

namespace Prp.Data
{
    public class ModelEntities
    {
    }

    public class User : tvwUser
    {

        public string passwordNew { get; set; }
        public string passwordNewRetype { get; set; }

        public string displayName { get; set; }
        public int reffId { get; set; }



    }

    public class Menu : tvwMenu
    {
        public bool hasRight { get; set; }

    }

    public class CalenderLevel : tblCalenderLevel
    {
        public int inductionId { get; set; }


    }
    public class InductionCalendar : tblInductionCalendar
    {
        public string dateStart { get; set; }
        public string dateEnd { get; set; }

    }

    public class EmailTemplate : tvwEmailTemplate
    {
        public int tempId { get; set; }
        public string search { get; set; }
    }

    public class Ticker : tvwTicker
    {
        public string detailShort { get; set; }
        public string detailLong { get; set; }
    }



    public class SMS : tblSM
    {
        public string typeName { get; set; }
        public int applicantId { get; set; }
        public string search { get; set; }


    }

    public class SmsCampaign : tblSmsCampaign
    {

        public bool isSchedule { get; set; }
        
        public string timeStart { get; set; }
        public int applicantId { get; set; }
        public string search { get; set; }


    }


    public class SmsProcess : tvwSmsProcess
    {
        public string contactNumber { get; set; }

        public string search { get; set; }
        public string reffIds1 { get; set; }
        public string reffIds2 { get; set; }
        public string reffIds3 { get; set; }
        public string reffIds4 { get; set; }
        public string reffIds5 { get; set; }
        public string resp { get; set; }
    }

    public class EmailProcess : tblEmailProcess
    {
        public string title { get; set; }
        public string subject { get; set; }
        public string name { get; set; }
        public string pmdcNo { get; set; }
        public string emailId { get; set; }
        public string contactNumber { get; set; }
        public string keyword { get; set; }

        public string search { get; set; }
        public string reffIds1 { get; set; }
        public string reffIds2 { get; set; }
        public string reffIds3 { get; set; }
        public string reffIds4 { get; set; }
        public string reffIds5 { get; set; }
        public string resp { get; set; }

        public EmailProcess()
        {
            keyword = "";
        }

    }

    public class Applicant
    {
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int applicantId { get; set; }
        public string name { get; set; }
        public string pmdcNo { get; set; }
        public string emailId { get; set; }
        public string password { get; set; }
        public string contactNumber { get; set; }
        public int network { get; set; }
        public int levelId { get; set; }
        public int facultyId { get; set; }
        public string pic { get; set; }
        public System.DateTime dated { get; set; }
        public string passwordDecrypt { get; set; }
        public string date { get; set; }
        public string levelName { get; set; }
        public string facultyName { get; set; }
        public string accountStatus { get; set; }
        public int accountStatusId { get; set; }
        public int applicationStatusId { get; set; }
        public string applicationStatus { get; set; }
        public int isValid { get; set; }
        public string pathProfilePic { get; set; }
        public int adminId { get; set; }

        public int statusId { get; set; }
        public string status { get; set; }
        public int specialityJobId { get; set; }

    }



    public class ApplicationStatus : tblApplicationStatu
    {
        public string statusType { get; set; }
        public string status { get; set; }

        public int stepStatusId { get; set; }
    }

    public class ApplicantInfo
    {
        public int applicantDetailId { get; set; }
        public int applicantId { get; set; }
        public string fatherName { get; set; }
        public int genderId { get; set; }
        public int disableId { get; set; }



        public System.DateTime dob { get; set; }
        public string bod { get; set; }
        public System.DateTime pmdcExpiryDate { get; set; }
        public System.DateTime mbbsPassingDate { get; set; }

        public int countryIdDegreePassing { get; set; }


        public int countryId { get; set; }
        public int districtId { get; set; }
        public string districtName { get; set; }

        public int domicileProvinceId { get; set; }
        public int domicileDistrictId { get; set; }

        public string address { get; set; }
        public string cnicNo { get; set; }
        public System.DateTime cnicExpiryDate { get; set; }
        public string cnicPicFront { get; set; }
        public string cnicPicBack { get; set; }
        public string domicilePicFront { get; set; }
        public string domicilePicBack { get; set; }

        public string pic { get; set; }
        public string disableImage { get; set; }


        // View Properties

        public string gender { get; set; }
        public string disable { get; set; }
        public string countryDegreePassing { get; set; }
        public string domicileProvince { get; set; }
        public string domicileDistrict { get; set; }
        public string countryName { get; set; }
        public string provinceName { get; set; }

        public int adminId { get; set; }


        public int inductionId { get; set; }
        public int phaseId { get; set; }

        public int dualNationalityType { get; set; }
        public string dualNationality { get; set; }

    }


    public class ApplicantInfoDualNational : tvwApplicantInfoDualNational
    {

    }
    public class ApplicantDegree
    {
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int applicantDegreeDetailId { get; set; }
        public int applicantId { get; set; }
        public int graduateTypeId { get; set; }
        public int degreeTypeId { get; set; }
        public int degreeYear { get; set; }
        public int provinceId { get; set; }
        public int instituteTypeId { get; set; }
        public int instituteId { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string instituteName { get; set; }
        public decimal totalMarks { get; set; }
        public decimal obtainMarks { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string imageDegree { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string imageDegreeForeignFront { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string imageDegreeForeignBack { get; set; }
        public string imageDegreeMatric { get; set; }
        public string imageCertificate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string certificatePMDC { get; set; }
        public System.DateTime dated { get; set; }
        public string typeIds { get; set; }
        // View properties
        public string graduateType { get; set; }
        public string degree { get; set; }
        public string instituteType { get; set; }
        public string province { get; set; }
        public int adminId { get; set; }
        public bool fcpsExemptionStatus { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string fcpsExemptionCertificate { get; set; }

    }

    public class ApplicantDegreeMark : tblApplicantDegreeMark
    {
        public int adminId { get; set; }
        public int position { get; set; }
        public string imgPosition { get; set; }
    }

    public class ApplicantCertificate : tblApplicantCertificate
    {
        public string datePassing { get; set; }
        public string typeName { get; set; }

        public string disciplineName { get; set; }

        public string certificateType { get; set; }

        public int adminId { get; set; }
    }

    public class ApplicantHouseJob : tvwApplicantHouseJob
    {
        public int countryId { get; set; } 
        public string startDateStr { get; set; }
        public string endDateStr { get; set; }
        public int phaseId { get; set; }


    }

    public class ApplicantExperience : tblApplicantExperience
    {

        public string levelName { get; set; }
        public string levelTypeName { get; set; }
        public string duration { get; set; }
        public string instituteNameThis { get; set; }

        public string provinceName { get; set; }
        public string typeName { get; set; }

        public int adminId { get; set; }

    }

    public class ApplicantDistinction : tblApplicantDistinction
    {
        public int adminId { get; set; }
    }

    public class ApplicantResearchPaper : tvwApplicantResearchPaper
    {
        public int adminId { get; set; }
    }

    public class ApplicantSpecility : tblApplicantSpecility
    {
        public string typeName { get; set; }
        public string specialityName { get; set; }
        public string hospitalName { get; set; }
        public int marks { get; set; }

        public decimal marksType { get; set; }

        public int adminId { get; set; }





    }

    public class ProfileSearch
    {
        public int applicantId { get; set; }
        public int adminId { get; set; }
        public int stepId { get; set; }
        public int inductionId { get; set; }
    }

    public class Application
    {
        public Applicant applicant { get; set; }
        public ApplicantInfo applicantInfo { get; set; }
        public ApplicantDegree applicantDegree { get; set; }
        public List<ApplicantDegreeMark> listDegreeMarks { get; set; }

        public Application()
        {
            applicantDegree = new ApplicantDegree();
            listDegreeMarks = new List<ApplicantDegreeMark>();
        }
    }

    public class Constant : tblConstant
    {
        public string typeName { get; set; }
    }
    public class Content : tblContent
    {
    }

    public class Department : tblDepartment
    {
        public string admin { get; set; }
        public string typeName { get; set; }
    }

    public class Induction : tblInduction
    {

    }

    public class Region : tblRegion
    {
        public string parentName { get; set; }
        public string regionType { get; set; }
    }

    #region Employee
    public class Employee : tblEmployee
    {
        public string gender { get; set; }
        public string relation { get; set; }
        public string martialStatus { get; set; }
        public string district { get; set; }
        public string designation { get; set; }
        public string degree { get; set; }
        public string hospital { get; set; }
        public string dateJoining { get; set; }

    }


    public class EmployeeSpeciality : tblEmployeeSpeciality
    {
        public  string typeName { get; set; }
        public string discipline { get; set; }
        public string speciality { get; set; }

    }

    public class EmployeeDoc : tblEmployeeDoc
    {

    }

    public class EmployeeExperience : tblEmployeeExperience
    {
        public string typeName { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int executionType { get; set; }
    }






    #endregion

    #region Trainee

    public class TraineeInfo : tblTraineeInfo
    {
        public string typeId { get; set; }
        public string specialityId { get; set; }

        public string dateJoining { get; set; }

        public int statusId { get; set; }
        public int hospitalIdTo { get; set; }

    }

    public class EmployeeTrainee : tblEmployeeTrainee
    {
        public int hospitalId { get; set; }
    }

    #endregion

    public class Unit : tblUnit
    {

    }

    public class Bed : tblBed
    {
        public int top { get; set; }
        public int pageNum { get; set; }
        public string search { get; set; }
    }


    public class Institute : tblInstitute
    {
        public string instituteType { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string hospitalIds { get; set; }

        public string hospitalNames { get; set; }
        public int hospitalCount { get; set; }
    }

    public class Speciality : tblSpeciality
    {
        public int inductionId { get; set; }
        public int totalJobs { get; set; }

    }

    public class SpecialityJob : tblSpecialityJob
    {
        public string specialityName { get; set; }
        public string hospitalName { get; set; }
        public string instituteName { get; set; }
        public string typeName { get; set; }
        public string quotaName { get; set; }

        public int totalSeats { get; set; }
        public int meritSeats { get; set; }
        public int consentSeats { get; set; }
        public int joinSeats { get; set; }
        public int vacantSeats { get; set; }

        public decimal marks { get; set; }
        public int seats { get; set; }

        public int seatJoin { get; set; }
        public int seatsRemaniing { get; set; }


        public int reffId { get; set; }
        public int baseId { get; set; }
        public string search { get; set; }
        public List<SqlTypeTb> listDataTb { get; set; }
        public DataTable dataTable { get; set; }     
    }

    public class Hospital : tvwHospital
    {
        public string instituteIds { get; set; }
        public int hasRight { get; set; }

        public int userId { get; set; }
        public string userName { get; set; }

    }

    public class HospitalDiscipline : tvwHospitalDiscipline
    {

        public string endDate { get; set; }
        public string startDate { get; set; }

        public string certificateImage { get; set; }

        public string search { get; set; }
        public HospitalDiscipline()
        {
            isApproved = true;
           
        }
    }


    public class VerficationCheckList : tvwVerficationCheckList
    {
        public string adminName { get; set; }
    }
    public class Marks : tblMark
    {
        public decimal marks { get; set; }

    }

    public class ApplicantVoucher : tblApplicantVoucher
    {
        public string name { get; set; }
        public string pmdcNo { get; set; }
        public string cnicNo { get; set; }
        public string status { get; set; }
        public int adminId { get; set; }

    }

    public class ApplicantVoucherBank : tblApplicantVoucherBank
    {
        public string status { get; set; }
        public string name { get; set; }
        public string pmdcNo { get; set; }
        public string cnicNo { get; set; }


    }

    public class VoucherInfo : tvwVoucherInfo
    {


    }


    public class StatusEmail : tblStatusEmail
    {


    }


    public class ApplicantApprovalStatus
    {

        public int applicantId { get; set; }
        public int approvalStatusTypeId { get; set; }
        public string approvalStatusType { get; set; }
        public int approvalStatusId { get; set; }
        public string comments { get; set; }
        public string approvalStatus { get; set; }
        public string statusMessage { get; set; }

        public string dateMessage { get; set; }
    }

    public class ApplicationVerificationStatus
    {

        public int applicantId { get; set; }
        public string name { get; set; }
        public string pmdcNo { get; set; }
        public string contactNumber { get; set; }
        public int countryId { get; set; }
        public string countryName { get; set; }
        public int countryTypeId { get; set; }
        public int countryIdDegreePassing { get; set; }
        public string countryDegreePassing { get; set; }
        public int degreeCountryTypeId { get; set; }
        public string status { get; set; }
        public int statusId { get; set; }
        public string comments { get; set; }
        public Nullable<System.DateTime> dated { get; set; }
        public string adminName { get; set; }

    }


    public class ApplicantSpecilityMerit
    {
        public int typeId { get; set; }
        public int applicantId { get; set; }
        public int specialityJobId { get; set; }
        public string specialityName { get; set; }
        public string hospitalName { get; set; }
        public int preferenceNo { get; set; }
        public decimal marks { get; set; }
        public decimal minMerit { get; set; }
        public Nullable<decimal> differnceMarks { get; set; }
        public int specialityJobIdMerit { get; set; }

        public int preferenceNoMerit { get; set; }
        public int isFilled { get; set; }
    }

    public class GazatMerit
    {
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int quotaId { get; set; }
        public string quotaName { get; set; }
        public int typeId { get; set; }
        public string typeName { get; set; }
        public string name { get; set; }
        public string fatherName { get; set; }
        public string pmdcNo { get; set; }
        public string emailId { get; set; }
        public decimal marks { get; set; }
        public int adminId { get; set; }
        public int roundNo { get; set; }
        public int top { get; set; }

        public int pageNum { get; set; }
        public string search { get; set; }
        public string urlPram { get; set; } 
    }

    public class Contact : tvwContact
    {

        public int phaseId { get; set; }

        public int top { get; set; }

        public int pageNum { get; set; }
        public string search { get; set; }
        public int adminId { get; set; }

        public int projId { get; set; }
        public string info { get; set; }


    }

    public class ContactSearch
    {
        public int phaseId { get; set; }
        public int top { get; set; }
        public int typeId { get; set; }
        public int pageNum { get; set; }
        public string search { get; set; }
        public int adminId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int statusAttendence { get; set; }
        public int statusGC { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int dayNo { get; set; }
    }

    public class ContactAttendence : tblContactAttendence
    {
    }

    public class ContactStatus : tblContactStatu
    {
    }

    public class ContactReply : tblContactReply
    {
        public int phaseId { get; set; }
        public int top { get; set; }
        public int pageNum { get; set; }
        public string search { get; set; }
    }

    public class ContactDoc : tblContactDoc
    {
        public int phaseId { get; set; }
        public int top { get; set; }
        public int pageNum { get; set; }
        public string search { get; set; }
    }


    public class FeedBack
    {
        public int feedbackId { get; set; }
        public int applicantIdBy { get; set; }
        public string pmdcNo { get; set; }
        public string comments { get; set; }
        public DateTime dated { get; set; }


        public int feedBackById { get; set; }
        public string feedBackByPmdcNo { get; set; }
        public string feedBackByEmailId { get; set; }
        public string feedBackByContactNumber { get; set; }

        public int feedBackTypeId { get; set; }
        public string feedBackTypeName { get; set; }
        public string name { get; set; }

        public string emailId { get; set; }
        public string contactNumber { get; set; }

        public int sr { get; set; }
        public int totalCountSet { get; set; }

        public int emailStatusId { get; set; }
        public string emailStatus { get; set; }

        public int adminId { get; set; }
        public string reply { get; set; }
        public System.DateTime datedReply { get; set; }

        public string adminName { get; set; }

    }

    public class Grievance : tblGrievance
    {
        public int grievanceTypeId { get; set; }
        public string subject { get; set; }
        public string application { get; set; }
        public string grievanceType { get; set; }
        public int appearanceId { get; set; }
        public int appearanceTypeId { get; set; }
        public int attendanceNo { get; set; }
        public int guardianId { get; set; }
        public string guardianName { get; set; }
        public string guardianContactNumber { get; set; }
        public System.DateTime datedAppearance { get; set; }
        public string status { get; set; }
        public int statusId { get; set; }
        public System.DateTime datedStatus { get; set; }

        public string actionComments { get; set; }
        public int adminIdAppearance { get; set; }
        public string adminAppearance { get; set; }
        public int adminIdStatus { get; set; }
        public string adminName { get; set; }

        public Nullable<long> sr { get; set; }
        public Nullable<long> rowNum { get; set; }
        public Nullable<int> totalCountSet { get; set; }
        public string pmdcNo { get; set; }
        public string name { get; set; }
        public string emailId { get; set; }
        public string contactNumber { get; set; }
        public string appearanceDate { get; set; }
        public string appearanceType { get; set; }
        public int actionTypeId { get; set; }
        public string actionType { get; set; }

        public int top { get; set; }
        public int pageNum { get; set; }
        public string search { get; set; }

        public string datedStr { get; set; }

    }

    public class GrievanceAction : tblGrievanceAction
    {


    }

    public class GrievanceRemark : tblGrievanceRemark
    {


    }

    public class Consent
    {

        public int consentId { get; set; }
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int roundId { get; set; }
        public int applicantId { get; set; }
        public int typeId { get; set; }
        public int consentTypeId { get; set; }
        public System.DateTime dated { get; set; }

        public string consentType { get; set; }
        public string typeName { get; set; }
        public string consentImage { get; set; }

        public string img { get; set; }
        public string otp { get; set; }
        public string mobileNumber { get; set; }
    }

   

    public class Discipline : tblDiscipline
    {
        public string typeIds { get; set; }
        public string search { get; set; }
    }

    public class ApplicantSelected : tvwApplicantSelected
    {

    }


    public class ApplicantJoiningDsb
    {
        public int instituteId { get; set; }
        public string instituteName { get; set; }
        public int totalCount { get; set; }
        public int totalJoin { get; set; }
        public int totalNotJoin { get; set; }
        public string hospitalNames { get; set; }
        public int hospitalCount { get; set; }

        public int hospitalId { get; set; }
        public string hospitalName { get; set; }

    }



    public class ApplicantJoined : tblApplicantJoined
    {
        public string specialityName { get; set; }
        public string instituteName { get; set; }
        public string hospitalName { get; set; }
        public string typeName { get; set; }
        public string quotaName { get; set; }
        public int jobs { get; set; }
        public string name { get; set; }
        public string pmdcNo { get; set; }
        public string emailId { get; set; }
        public string pic { get; set; }
        public string dateJoining { get; set; }

        public string contactNumber { get; set; }

        public string fatherName { get; set; }



        public int joined { get; set; }
        public int notJoined { get; set; }
        public long rowNum { get; set; }
        public int totalCountSet { get; set; }
        public int totalCount { get; set; }
        public Nullable<int> joinStatus { get; set; }
    }

  

    public class ApplicantAction : tblApplicantAction
    {
        public string typeName { get; set; }
        public string categoryName { get; set; }
        public string dateStart { get; set; }
        public string dateEnd { get; set; }
        public string status { get; set; }
        public string admin { get; set; }
    }

    public class ApplicantExtensionAction : tblApplicantAction
    {
        public int applicantExtensionId { get; set; }
        public string dateStart { get; set; }
        public string dateEnd { get; set; }
        public DateTime dateRequested { get; set; }
        public DateTime dateApproved { get; set; }
        public int approvalBySupervisor { get; set; }
        public int noOfDays { get; set; }
        public int noOfMonths { get; set; }
        public string status { get; set; }
        public string admin { get; set; }
        public string imageApplication { get; set; }
        public string imagePER { get; set; }
        public string imageNOC { get; set; }
        public string imagePMDC { get; set; }
        public string imageExtensionOrder { get; set; }
        public string imageJoiningOrder { get; set; }
        public string imageDoc1 { get; set; }
        public string imageDoc2 { get; set; }
        public int requestedBy { get; set; }
        public string requestedByName { get; set; }
        public string remarksRequested { get; set; }
        public int approvedBy { get; set; }
        public string approver { get; set; }
        public int approvalStatus { get; set; }
        public string approvalRemarks { get; set; }
        public string rtmcUhsNo { get; set; }
        public string imageInductionOrder { get; set; }
        public string imageTothc { get; set; }
        public string imageJoat { get; set; }

        public int forwardedTo { get; set; }
        public bool imageApplicationValidity { get; set; }
        public string imageApplicationRemarks { get; set; }

        public bool imageDoc1Validity { get; set; }
        public string imageDoc1Remarks { get; set; }
        public bool imageDoc2Validity { get; set; }
        public string imageDoc2Remarks { get; set; }

        public bool imagePERValidity { get; set; }
        public string imagePERRemarks { get; set; }
        public bool imageExtensionOrderValidity { get; set; }
        public string imageExtensionOrderRemarks { get; set; }
        public bool imageInductionOrderValidity { get; set; }
        public string imageInductionOrderRemarks { get; set; }
        public bool imagePMDCValidity { get; set; }
        public string imagePMDCRemarks { get; set; }
        public bool imageJoiningOrderValidity { get; set; }
        public string imageJoiningOrderRemarks { get; set; }

        public bool imageNOCValidity { get; set; }
        public string imageNOCRemarks { get; set; }
        public bool imageTothcValidity { get; set; }
        public string imageTothcRemarks { get; set; }
        public bool rtmcUhsNoValidity { get; set; }
        public string rtmcUhsNoRemarks { get; set; }
        public bool imageJoatValidity { get; set; }
        public string imageJoatRemarks { get; set; }

        public int approvalByNawaz { get; set; }
        public int approvalBySO { get; set; }
        public int approvalByDS { get; set; }

        public int approvalByAST { get; set; }
        public int approvalBySS { get; set; }
        public int approvalBySec { get; set; }
        public string remarksByNawaz { get; set; }

        public string remarksBySO { get; set; }

        public string remarksByDS { get; set; }

        public string remarksByAST { get; set; }
        public string remarksBySec { get; set; }
    }


    public class Leave : tblLeave
    {
        public int leaveId { get; set; }
        public int assignTo { get; set; }
        public string remarks { get; set; }
        public int search { get; set; }
        public List<SqlTypeTb> listDataTb { get; set; }
        public DataTable dataTable { get; set; }
        public string dateStart { get; set; }
        public string dateEnd { get; set; }
        public string dateEdd { get; set; }
        public DateTime eddDate { get; set; }

    }

    public class ApplicantLeaveAction : tblApplicantAction
    {
        public int forwardedTo { get; set; }
        public int approvalBySO { get; set; }
        public int approvalByDS { get; set; }
        public int approvalByAST { get; set; }
        public int approvalBySS { get; set; }
        public int approvalBySec { get; set; }
        public string remarksBySO { get; set; }
        public string remarksByDS { get; set; }
        public string remarksByAST { get; set; }
        public string cnicFrontImage { get; set; }
        public string cnicBackImage { get; set; }
        public bool cnicFrontValidity { get; set; }
        public bool cnicBackValidity { get; set; }
        public string cnicFrontRemarks { get; set; }
        public string cnicBackRemarks { get; set; }
        public string remarksBySS { get; set; }
        public string imageRTMC { get; set; }

        public string imagePreviousLeaveReport { get; set; }
        public bool cnicValidity { get; set; }

        public int applicantLeaveId { get; set; }
        public string typeName { get; set; }
        public string categoryName { get; set; }
        public string dateStart { get; set; }
        public string dateEnd { get; set; }
        public string eddString { get; set; }
        public DateTime dateRequested { get; set; }
        public DateTime dateApproved { get; set; }
        public int noOfDays { get; set; }
        public string status { get; set; }
        public string admin { get; set; }
        public string imageAffidavit { get; set; }
        public string imageMedical { get; set; }
        public string imageMaternity { get; set; }
        public string imagePGAC { get; set; }
        public string imageForwarding { get; set; }
        public string imageSurety { get; set; }
        public int ddlDoxTaken { get; set; }
        public int requestedBy { get; set; }
        public string requestedByName { get; set; }
        public string remarksRequested { get; set; }
        public int approvedBy { get; set; }
        public string approver { get; set; }
        public int approvalStatus { get; set; }
        public string approvalRemarks { get; set; }
        public string imageAttorney { get; set; }
        public string imageVisa { get; set; }
        public string imagePurpose { get; set; }
        public DateTime? edd { get; set; }
        public bool applicationValidity { get; set; }
        public string applicationRemarks { get; set; }
        public bool affidavitValidity { get; set; }

        public bool medicalValidity { get; set; }
        public bool matenrityValidity { get; set; }
        public bool forwardingValidity { get; set; }
        public bool ipgacValidity { get; set; }
        public bool suretyValidity { get; set; }
        public bool attorneyValidity { get; set; }
        public bool purposeValidity { get; set; }
        public bool visaValidity { get; set; }
        public bool RTMCValidity { get; set; }
        public bool imagePreviousLeaveReportValidity { get; set; }

        public string affidavitRemarks { get; set; }
        public string medicalRemarks { get; set; }
        public string matenrityRemarks { get; set; }
        public string forwardingRemarks { get; set; }
        public string ipgacRemarks { get; set; }
        public string suretyRemarks { get; set; }
        public string attorneyRemarks { get; set; }
        public string purposeRemarks { get; set; }
        public string visaRemarks { get; set; }
        public string RTMCRemarks { get; set; }
        public string imagePreviousLeaveReportRemarks { get; set; }
        public int issuedOrderStatus { get; set; }

    }

    public class ResearchJournal : tvwResearchJournal
    {

    }

    public class ApplicantDebarData
    { 
        public int applicantId { get; set; }
        public int typeId { get; set; }
        public string image { get; set; }
    }

    public class OtpCode
    {
        public int applicantId { get; set; }
        public string mobileNumber { get; set; }
        public int otpCode { get; set; }
        public int typeId { get; set; }
    }

}
