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
    public class ConstantDAL : PrpDBConnect
    {
        public Constant GetById(int constantId)
        {
            Constant obj = new Constant();
            try
            {
                tblConstant objt = db.tblConstants.FirstOrDefault(x => x.constantId == constantId);
                obj = MapConstant.ToEntity(objt);

            }
            catch (Exception)
            {
            }
            return obj;
        }

        public List<Constant> GetAll()
        {
            List<Constant> list = new List<Constant>();
            try
            {
                List<tvwConstant> listt = db.tvwConstants.ToList();
                list = MapConstant.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<Constant> GetAll(int typeId)
        {
            List<Constant> list = new List<Constant>();
            try
            {
                List<tvwConstant> listt = db.tvwConstants.Where(x => x.typeId == typeId).ToList();
                list = MapConstant.ToEntityList(listt);

            }
            catch (Exception)
            {
            }
            return list;
        }

        public List<Constant> GetTypes()
        {
            List<Constant> list = new List<Constant>();
            try
            {
                List<tblConstant> listt = db.tblConstants.Where(x => x.typeId == 0).ToList();
                list = MapConstant.ToEntityList(listt);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public List<EntityDDL> GetConstantDDL(DDLConstants obj)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                var listt = db.spConstantForDDL(obj.typeId, obj.parentId, obj.reffId, obj.userId, obj.reffIds, obj.condition).ToList();
                list = MapConstant.ToEntityList(listt);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public Message AddUpdate(Constant obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spConstantAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@constantId", obj.constantId);
            cmd.Parameters.AddWithValue("@id", obj.id);
            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@code", obj.code);
            cmd.Parameters.AddWithValue("@value", obj.value);
            cmd.Parameters.AddWithValue("@nameDisplay", obj.nameDisplay);
            cmd.Parameters.AddWithValue("@shortDesc", obj.shortDesc);
            cmd.Parameters.AddWithValue("@detail", obj.detail);
            cmd.Parameters.AddWithValue("@isActive", obj.isActive);
            cmd.Parameters.AddWithValue("@isDeleted", obj.isDeleted);
            cmd.Parameters.AddWithValue("@parentId", obj.parentId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);

            return PrpDbADO.FillDataTableMessage(cmd);
        }

    }
}
