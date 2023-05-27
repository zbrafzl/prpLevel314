using Prp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Prp.Data
{
	public class UserDAL : PrpDBConnect
	{
		public UserDAL()
		{
		}

		public Message AddUpdate(User obj)
		{
			Message message = new Message();
			try
			{
				spUserAddUpdate_Result spUserAddUpdateResult = this.db.spUserAddUpdate(new int?(obj.userId), obj.firstName, obj.lastName, obj.emailId, obj.password, new int?(obj.typeId), obj.parentId, new int?(obj.departmentId), new int?(obj.designationId), new bool?(obj.isActive), new int?(obj.adminId)).FirstOrDefault<spUserAddUpdate_Result>();
				message = MapUser.ToEntity(spUserAddUpdateResult);
			}
			catch (Exception exception)
			{
				message.msg = exception.Message;
				message.id = 0;
			}
			return message;
		}

		public List<User> GetAll()
		{
			List<User> users = new List<User>();
			try
			{
				List<tvwUser> list = this.db.tvwUsers.ToList<tvwUser>();
				users = MapUser.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return users;
		}

		public User GetById(int userId)
		{
			User user = new User();
			try
			{
				tvwUser _tvwUser = this.db.tvwUsers.FirstOrDefault<tvwUser>((tvwUser x) => x.userId == userId);
				user = MapUser.ToEntity(_tvwUser);
			}
			catch (Exception exception)
			{
				user = new User();
			}
			return user;
		}

		public List<User> GetByParentId(int parentId)
		{
			List<User> users = new List<User>();
			try
			{
				List<tvwUser> list = (
					from x in this.db.tvwUsers
					where x.parentId == (int?)parentId
					select x).ToList<tvwUser>();
				users = MapUser.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return users;
		}

		public List<User> GetByType(int typeId)
		{
			List<User> users = new List<User>();
			try
			{
				List<tvwUser> list = (
					from x in this.db.tvwUsers
					where x.typeId == typeId
					select x).ToList<tvwUser>();
				users = MapUser.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return users;
		}

		public List<User> GetByTypeId(int typeId)
		{
			List<User> users = new List<User>();
			try
			{
				List<spUserGetByType_Result> list = this.db.spUserGetByType(new int?(typeId)).ToList<spUserGetByType_Result>();
				users = MapUser.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return users;
		}

		public User Login(string userName, string password)
		{
			User user = new User();
			try
			{
				spAdminLogin_Result spAdminLoginResult = this.db.spAdminLogin(userName, password).FirstOrDefault<spAdminLogin_Result>();
				user = MapUser.ToEntity(spAdminLoginResult);
			}
			catch (Exception exception)
			{
				user = new User();
			}
			return user;
		}

		public Message UpdatePassword(User obj)
		{
			Message message = new Message();
			try
			{
				spUserChangePassword_Result spUserChangePasswordResult = this.db.spUserChangePassword(new int?(obj.userId), obj.password, obj.passwordNew).FirstOrDefault<spUserChangePassword_Result>();
				message = MapUser.ToEntity(spUserChangePasswordResult);
			}
			catch (Exception exception)
			{
				message.msg = exception.Message;
				message.id = 0;
			}
			return message;
		}
	}
}