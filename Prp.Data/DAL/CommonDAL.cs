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
    public class CommonDAL : PrpDBConnect
    {
        public List<EntityDDL> SearchDDL(DDLSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spDDL]"
            };

            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@parentId", obj.parentId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@reffId", obj.reffId);
            cmd.Parameters.AddWithValue("@reffIds", obj.reffIds);
            cmd.Parameters.AddWithValue("@search", obj.search);
            cmd.Parameters.AddWithValue("@section", obj.section);
            return PrpDbADO.FillDataTableEntityDDL(cmd);

        }

        public List<EntityCount> GetDashboardCount(int inductionId, int phaseId)
        {
            List<EntityCount> list = new List<EntityCount>();
            try
            {
                var listt = db.spApplicantStatusGetAll(inductionId, phaseId).ToList();
                list = MapCommon.ToEntityList(listt);
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<EntityCount> GetDashboardCountInstituteHospital(int instituteId, int hospitalId, string condition)
        {
            List<EntityCount> list = new List<EntityCount>();
            try
            {
                var listt = db.spInstituteHospitalGetDashBoardData(instituteId, hospitalId, condition).ToList();
                list = MapCommon.ToEntityList(listt);
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<EntityCount> GetCountJobs(CountJobsEntity obj)
        {
            List<EntityCount> list = new List<EntityCount>();
            try
            {
                var listt = db.spJobGetCount(obj.typeId, obj.hospitalId, obj.specialityId, obj.condition).ToList();
                list = MapCommon.ToEntityList(listt);
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<EntityCount> GetCountApplicantApprovalStatus(CountApplicantStatusEntity obj)
        {
            List<EntityCount> list = new List<EntityCount>();
            try
            {
                //var listt = db.spApplicantApprovalStatusCount(obj.reffId, obj.condition).ToList();
                //list = MapCommon.ToEntityList(listt);
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        #region DisciplineDAL


        public List<Discipline> DisciplineGetAll()
        {
            List<Discipline> list = new List<Discipline>();
            try
            {
                var listt = db.tblDisciplines.ToList();
                list = MapDiscipline.ToEntityList(listt);

            }
            catch (Exception ex)
            {
                list = new List<Discipline>();
            }
            return list;
        }

        #endregion

        #region Department

        public Department DepartmentGetById(int departmentId)
        {
            Department obj = new Department();
            try
            {
                tblDepartment objt = db.tblDepartments.FirstOrDefault(x => x.departmentId == departmentId);
                obj = MapDepartment.ToEntity(objt);

            }
            catch (Exception)
            {
            }
            return obj;
        }

        public List<Department> DepartmentGetAll(int typeId)
        {
            List<Department> list = new List<Department>();
            try
            {
                List<tvwDepartment> listt = db.tvwDepartments.Where(x => x.typeId == typeId).ToList();
                list = MapDepartment.ToEntityList(listt);

            }
            catch (Exception)
            {
            }
            return list;
        }

        public Message DepartmentAddUpdate(Department obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spDepartmentAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@departmentId", obj.departmentId);
            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@code", obj.code);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@isActive", obj.isActive);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public DataTable GetDepartmentForHospital(int hospitalId)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spDepartmentForHospital]"
            };
            cmd.Parameters.AddWithValue("@hospitalId", hospitalId);
            return PrpDbADO.FillDataTable(cmd);
        }

        public Message DepartmentHospitalAddUpdate(int hospitalId, string ids, int adminId)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spDepartmentHospitalAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@hospitalId", hospitalId);
            cmd.Parameters.AddWithValue("@ids", ids);
            cmd.Parameters.AddWithValue("@adminId", adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }



        #endregion

        #region Unit

        public Message UnitAddUpdate(Unit obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spUnitAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@unitId", obj.unitId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@departmentId", obj.departmentId);
            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@code", obj.code);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@isActive", obj.isActive);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }
        #endregion

        #region Beds

        public Message BedAddUpdate(Bed obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spBedsAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@bedId", obj.bedId);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@departmentId", obj.departmentId);
            cmd.Parameters.AddWithValue("@unitId", obj.unitId);
            cmd.Parameters.AddWithValue("@disciplineId", obj.disciplineId);
            cmd.Parameters.AddWithValue("@specialityId", obj.specialityId);
            cmd.Parameters.AddWithValue("@bedsER", obj.bedsER);
            cmd.Parameters.AddWithValue("@bedsICU", obj.bedsICU);
            cmd.Parameters.AddWithValue("@bedsWards", obj.bedsWards);
            cmd.Parameters.AddWithValue("@bedsOther", obj.bedsOther);
            cmd.Parameters.AddWithValue("@remarksN", obj.remarksN);
            cmd.Parameters.AddWithValue("@imageN", obj.imageN);
            cmd.Parameters.AddWithValue("@bedsDep", obj.bedsDep);
            cmd.Parameters.AddWithValue("@remarksDep", obj.remarksDep);
            cmd.Parameters.AddWithValue("@imageDep", obj.imageDep);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public Message BedDelete(Bed obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spBedsDelete]"
            };
            cmd.Parameters.AddWithValue("@bedId", obj.bedId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public DataTable BedsSearch(Bed obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[BedsSearch]"
            };
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@departmentId", obj.departmentId);
            cmd.Parameters.AddWithValue("@unitId", obj.unitId);
            cmd.Parameters.AddWithValue("@disciplineId", obj.disciplineId);

            return PrpDbADO.FillDataTable(cmd);
        }

        #endregion


        #region API DAL

        public ApplicantInfoAPI ApplicantGetInfo(string applicantNo, int transactionType)
        {
            ApplicantInfoAPI obj = new ApplicantInfoAPI();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantGetByApplicantNo]"
                };
                cmd.Parameters.AddWithValue("@applicantNo", applicantNo);
                cmd.Parameters.AddWithValue("@transactionType", transactionType);
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
            catch (Exception)
            {

                obj = new ApplicantInfoAPI();
            }

            return obj;
        }


        //public ApplicantInfoAPI APiApplicantInfo(string applicantNo)
        //{
        //    ApplicantInfoAPI obj = new ApplicantInfoAPI();
        //    try
        //    {
        //        var objt = db.spApplicantGetByApplicantNo(applicantNo).FirstOrDefault();
        //        if (objt != null && objt.applicantId > 0)
        //        {
        //            obj = MapCommon.ToEntity(objt);
        //        }
        //        else
        //        {
        //            obj = new ApplicantInfoAPI();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        obj = new ApplicantInfoAPI();
        //    }
        //    return obj;
        //}



        #endregion

    }
}
