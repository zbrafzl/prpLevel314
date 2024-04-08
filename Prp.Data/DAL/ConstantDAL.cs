using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Prp.Data
{
	public class ConstantDAL : PrpDBConnect
	{
		public ConstantDAL()
		{
		}

		public Message AddUpdate(Constant obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spConstantAddUpdate]"
			};
			sqlCommand.Parameters.AddWithValue("@constantId", obj.constantId);
			sqlCommand.Parameters.AddWithValue("@id", obj.id);
			sqlCommand.Parameters.AddWithValue("@name", obj.name);
			sqlCommand.Parameters.AddWithValue("@code", obj.code);
			sqlCommand.Parameters.AddWithValue("@value", obj.@value);
			sqlCommand.Parameters.AddWithValue("@nameDisplay", obj.nameDisplay);
			sqlCommand.Parameters.AddWithValue("@shortDesc", obj.shortDesc);
			sqlCommand.Parameters.AddWithValue("@detail", obj.detail);
			sqlCommand.Parameters.AddWithValue("@isActive", obj.isActive);
			sqlCommand.Parameters.AddWithValue("@isDeleted", obj.isDeleted);
			sqlCommand.Parameters.AddWithValue("@parentId", obj.parentId);
            sqlCommand.Parameters.AddWithValue("@sortOrder", obj.sortOrder);
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

        public DataSet ConstantSearch(Constant obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spConstantSearch]"
            };
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
            sqlCommand.Parameters.AddWithValue("@parentId", obj.parentId);
            return PrpDbADO.FillDataSet(sqlCommand);
        }

        public DataTable ConstantGetByParam(Constant obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spConstantGetByParam]"
            };
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
            sqlCommand.Parameters.AddWithValue("@parentId", obj.parentId);
            return PrpDbADO.FillDataTable(sqlCommand);
        }

        public List<Constant> GetAll()
		{
			List<Constant> constants = new List<Constant>();
			try
			{
				List<tvwConstant> list = this.db.tvwConstants.ToList<tvwConstant>();
				constants = MapConstant.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return constants;
		}

		public List<Constant> GetAll(int typeId)
		{
			List<Constant> constants = new List<Constant>();
			try
			{
				List<tvwConstant> list = (
					from x in this.db.tvwConstants
					where x.typeId == typeId
					select x).ToList<tvwConstant>();
				constants = MapConstant.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return constants;
		}

		public Constant GetById(int constantId)
		{
			Constant constant = new Constant();
			try
			{
				tblConstant _tblConstant = this.db.tblConstants.FirstOrDefault<tblConstant>((tblConstant x) => x.constantId == constantId);
				constant = MapConstant.ToEntity(_tblConstant);
			}
			catch (Exception exception)
			{
			}
			return constant;
		}

		public List<EntityDDL> GetConstantDDL(DDLConstants obj)
		{
			List<EntityDDL> entityDDLs = new List<EntityDDL>();
			try
			{
				List<spConstantForDDL_Result> list = this.db.spConstantForDDL(new int?(obj.typeId), new int?(obj.parentId), new int?(obj.reffId), new int?(obj.userId), obj.reffIds, obj.condition).ToList<spConstantForDDL_Result>();
				entityDDLs = MapConstant.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return entityDDLs;
		}

		public List<Constant> GetTypes()
		{
			List<Constant> constants = new List<Constant>();
			try
			{
				List<tblConstant> list = (
					from x in this.db.tblConstants
					where x.typeId == 0
					select x).ToList<tblConstant>();
				constants = MapConstant.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return constants;
		}
	}
}