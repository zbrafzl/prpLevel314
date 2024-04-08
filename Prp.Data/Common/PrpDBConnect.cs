﻿using prp.fn;
using Prp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prp.Data
{
    public class PrpDBConnect
    {
        public DbPrpEntities db = null;

        public PrpDBConnect()
        {
            db = new DbPrpEntities();
        }
    }

    public static class PrpDbConnectADO
    {
        public static string Conn = "";

        static PrpDbConnectADO()
        {
            Conn = ConfigurationManager.ConnectionStrings["DbPrpConn"].ConnectionString;
        }

    }

    public static class PrpDbADO
    {
        public static Message ExecuteNonQuery(SqlCommand cmd, int timeOut = 0)
        {
            SqlConnection con = new SqlConnection();
            Message msg = new Message();

            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return msg;
        }

        public static System.Data.DataTable FillDataTable(SqlCommand cmd, string dataTableName = "")
        {
            SqlConnection con = new SqlConnection();
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                dataTableName = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public static DataSet FillDataSet(SqlCommand cmd)
        {
            SqlConnection con = new SqlConnection();
            DataSet ds = new DataSet();
            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                try
                {
                    DataTable dt = new DataTable();
                    List<Message> listMsg = new List<Message>();
                    Message msg = new Message();
                    msg.status = false;
                    msg.id = 0;
                    msg.msg = ex.Message;
                    listMsg.Add(msg);
                    dt = MyFunctions.ConvertDataTable(listMsg);
                    ds.Tables.Add(dt);
                }
                catch (Exception)
                {
                }
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public static Message FillDataTableMessage(SqlCommand cmd, int timeOut = 0)
        {
            Message msg = new Message();
            SqlConnection con = new SqlConnection();
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];

                msg = dt.TooMessage();
            }
            catch (Exception ex)
            {
                msg = new Message();
            }
            finally
            {
                con.Close();
            }
            return msg;
        }

        public static Message TooMessage(this DataTable dt)
        {
            Message msg = new Message();
            try
            {
                DataRow dr = dt.Rows[0];
                msg.id = dr["id"].TooInt();
                msg.status = dr["status"].TooBoolean();
                try
                {
                    msg.msg = dr["message"].TooString();
                }
                catch (Exception)
                {
                }
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public static List<EntityDDL> FillDataTableEntityDDL(SqlCommand cmd, int timeOut = 0)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            SqlConnection con = new SqlConnection();
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];

                list = dt.TooEntityDDL();
            }
            catch (Exception ex)
            {
                list = new List<EntityDDL>();
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public static List<EntityDDL> TooEntityDDL(this DataTable dt)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EntityDDL obj = new EntityDDL();
                    obj.key = dr["key"].TooString();
                    obj.value = dr["value"].TooString();
                    obj.typeId = dr["typeId"].TooInt();
                    list.Add(obj);
                }
               
                    
               
            }
            catch (Exception ex)
            {
                list = new List<EntityDDL>();
            }

            return list;
        }

        public static Message CheckConnection()
        {
            SqlConnection con = new SqlConnection();
            Message msg = new Message();

            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                msg.status = true;
                msg.msg = "Connection Opened";
            }
            catch (Exception ex)
            {
                msg.status = true;
                msg.msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return msg;
        }

    }

    public static class CommonFunctionDAL
    {
        public static Message ConvertToEnitityMessage(this System.Data.DataTable dt)
        {
            Message msg = new Message();
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    msg.id = dr["id"].TooInt();
                    msg.status = dr["status"].TooBoolean();
                    msg.message = dr["message"].TooString();
                    msg.msg = dr["message"].TooString();
                }

            }
            catch (Exception ex)
            {
                msg = new Message();
            }
            finally
            {

            }
            return msg;
        }
    }
}
