using Prp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prp.Sln
{
    public class Params
    {
    }

    public class ApplicantInfoParam
    {

        public int applicantDetailId { get; set; }
        public int applicantId { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public int typeId { get; set; }
        public string fatherName { get; set; }
        public int genderId { get; set; }
        public int disableId { get; set; }
        public string dob { get; set; }
        public string pmdcExpiryDate { get; set; }
        public string mbbsPassingDate { get; set; }
        public string fcpsPassingDate { get; set; }
        public int countryId { get; set; }
        public int countryIdDegreePassing { get; set; }
        public int provinceId { get; set; }
        public int districtId { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string districtName { get; set; }
        public int domicileProvinceId { get; set; }
        public int domicileDistrictId { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string address { get; set; }
        public string cnicNo { get; set; }
        public string cnicExpiryDate { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string cnicPicFront { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string cnicPicBack { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string domicilePicFront { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string domicilePicBack { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public string disableImage { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string pic { get; set; }

        public int dualNationalityType { get; set; }
    }

    public class ApplicantDegreeSpecialityParam
    {
        public int specialityId { get; set; }
        public int applicantId { get; set; }
        public int specialityTypeId { get; set; }
        public string degreeDate { get; set; }
        public int totalMarks { get; set; }
        public int obtainMarks { get; set; }
        public string imageCertificate { get; set; }
    }


    public class ApplicantEducationParam
    {
        public ApplicantDegree applicantDegree { get; set; }
        public List<ApplicantDegreeMark> listDegreeMarks { get; set; }

        public List<ApplicantCertificate> listCertificate { get; set; }


        public ApplicantEducationParam()
        {
            applicantDegree = new ApplicantDegree();
            listDegreeMarks = new List<ApplicantDegreeMark>();
            listCertificate = new List<ApplicantCertificate>();
        }
    }


    public class ApplicantCertificateParam
    {

        public int applicantCertificateTypeId { get; set; }
        public int applicantId { get; set; }
        public int typeId { get; set; }
        public int obtainMarks { get; set; }
        public int totalMarks { get; set; }
        public string passingDate { get; set; }
        public string imageCertificate { get; set; }


    }

   
    public class ApplicantExperienceParam
    {
        public int applicantExperienceId { get; set; }
        public int applicantId { get; set; }
        public int levelId { get; set; }
        public int levelTypeId { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string instituteName { get; set; }
        public bool isCurrent { get; set; }
        public int instituteId { get; set; }
        public int provinceId { get; set; }
        public int typeId { get; set; }
        public string startDated { get; set; }
        public string endDate { get; set; }
        public string imageExperience { get; set; }
        public string dated { get; set; }
    }

    public class ApplicantDistinctionParam
    {
        public int applicantDistinctionId { get; set; }
        public int applicantId { get; set; }
        public string subject { get; set; }
        public int year { get; set; }
        public string imageDistinction { get; set; }
        public int position { get; set; }
        public string instituteId { get; set; }
        public string dated { get; set; }
    }

    public class ApplicantResearchPaperParam
    {
        public int applicantResearchId { get; set; }
        public int applicantId { get; set; }
        public string name { get; set; }
        public int authorId { get; set; }
        public int publishStatusId { get; set; }
        public string link { get; set; }
        public string imageLetter { get; set; }
        public int researchJournalId { get; set; }
        public string webLink { get; set; }
        public bool isActive { get; set; }

    }

    public class ApplicantVoucherParam : ApplicantVoucher
    {
        public string dateSubmitted { get; set; }

    }

    public class ApplicantEducation
    {
        public ApplicantDegree applicantDegree { get; set; }
        public List<ApplicantDegreeMark> listDegreeMarks { get; set; }
        public List<ApplicantCertificate> listCertificate { get; set; }

        public ApplicantEducation()
        {
            applicantDegree = new ApplicantDegree();
            listDegreeMarks = new List<ApplicantDegreeMark>();
            listCertificate = new List<ApplicantCertificate>();

        }

    }
}