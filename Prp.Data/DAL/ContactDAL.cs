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
	public class ContactDAL : PrpDBConnect
	{
		public ContactDAL()
		{
		}

        public Message AddUpdate(Contact obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spContactQuestion]"
            };
			if (obj.projId == 0) obj.projId = 1;
            sqlCommand.Parameters.AddWithValue("@projId", obj.projId);
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@name", obj.name);
            sqlCommand.Parameters.AddWithValue("@emailId", obj.emailId);
            sqlCommand.Parameters.AddWithValue("@pmdcNo", obj.pmdcNo.TooString());
            sqlCommand.Parameters.AddWithValue("@info", obj.info.TooString());
            sqlCommand.Parameters.AddWithValue("@title", obj.title);
            sqlCommand.Parameters.AddWithValue("@question", obj.question);
            return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
        }

  //      public Message AddUpdate(Contact obj)
		//{
		//	Message message = new Message();
		//	try
		//	{
		//		spContactQuestion_Result spContactQuestionResult = this.db.spContactQuestion(new int?(obj.typeId), obj.name, new int?(obj.applicantId), obj.pmdcNo, obj.emailId, obj.title, obj.question).FirstOrDefault<spContactQuestion_Result>();
		//		message = MapContact.ToEntity(spContactQuestionResult);
		//	}
		//	catch (Exception exception)
		//	{
		//		message.msg = exception.Message;
		//		message.id = 0;
		//	}
		//	return message;
		//}

		public Message AddUpdateDocs(int contactId, string images, int typeId)
		{
			Message message = new Message();
			try
			{
				spContactDocsAdd_Result spContactDocsAddResult = this.db.spContactDocsAdd(new int?(typeId), new int?(contactId), images).FirstOrDefault<spContactDocsAdd_Result>();
				message = MapContact.ToEntity(spContactDocsAddResult);
			}
			catch (Exception exception)
			{
				message.msg = exception.Message;
				message.id = 0;
			}
			return message;
		}

		public Message AddUpdateStatus(ContactStatus obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spContactStatusAddUpdate]"
			};
			sqlCommand.Parameters.AddWithValue("@id", obj.id);
			sqlCommand.Parameters.AddWithValue("@contactId", obj.contactId);
			sqlCommand.Parameters.AddWithValue("@comments", obj.comments);
			sqlCommand.Parameters.AddWithValue("@statusId", obj.statusId);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public Message AttendenceAddUpdate(ContactAttendence obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spContactAttendence]"
			};
			sqlCommand.Parameters.AddWithValue("@contactIdAttendenceId", obj.contactIdAttendenceId);
			sqlCommand.Parameters.AddWithValue("@contactId", obj.contactId);
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
			sqlCommand.Parameters.AddWithValue("@isSelf", obj.isSelf);
			sqlCommand.Parameters.AddWithValue("@relationId", obj.relationId);
			sqlCommand.Parameters.AddWithValue("@attendenceNo", obj.attendenceNo);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public DataTable ContactAttendencePrint(ContactSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spContactAttendencePrint]"
			};
			sqlCommand.Parameters.AddWithValue("@start", obj.start);
			sqlCommand.Parameters.AddWithValue("@end", obj.end);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable ContactAttendenceSearch(ContactSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spContactAttendenceSearch]"
			};
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable ContactAttendenceSearchCurrent(ContactSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spContactAttendenceCurrentForAttendnce]"
			};
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable ContactAttendenceStatusPrint(ContactSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spContactAttendenceStatusPrint]"
			};
			sqlCommand.Parameters.AddWithValue("@start", obj.start);
			sqlCommand.Parameters.AddWithValue("@end", obj.end);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@dayNo", obj.dayNo);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable ContactAttendenceStatusSearchCurrent(ContactSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spContactAttendenceCurrentForStatus]"
			};
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public int ContactQuestionGetNoReplied(int adminId)
		{
			int count = 0;
			try
			{
				SqlCommand sqlCommand = new SqlCommand()
				{
					CommandType = CommandType.StoredProcedure,
					CommandText = "[dbo].[spContactQuestionGetNoReplied]"
				};
				sqlCommand.Parameters.AddWithValue("@adminId", adminId);
				DataTable dataTable = PrpDbADO.FillDataTable(sqlCommand, "");
				if (dataTable != null)
				{
					count = dataTable.Rows.Count;
				}
			}
			catch (Exception exception)
			{
				count = 0;
			}
			return count;
		}

		public DataTable ContactQuestionSearch(Contact obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spContactQuestionSearch]"
			};
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public Message ContactReplyAnswer(ContactReply obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spContactAnswer]"
			};
			sqlCommand.Parameters.AddWithValue("@contactId", obj.contactId);
			sqlCommand.Parameters.AddWithValue("@answer", obj.answer);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public List<ContactDoc> GetAllDocsByTypeAndContact(int typeId, int contactId)
		{
			List<ContactDoc> contactDocs = new List<ContactDoc>();
			try
			{
				List<tblContactDoc> list = (
					from x in this.db.tblContactDocs
					where x.typeId == typeId && x.contactId == contactId
					select x).ToList<tblContactDoc>();
				contactDocs = MapContact.ToEntityList(list);
			}
			catch (Exception exception)
			{
				contactDocs = new List<ContactDoc>();
			}
			return contactDocs;
		}

		public List<ContactReply> GetAnswerListByApplicant(int contactId, int applicantId)
		{
			List<ContactReply> contactReplies = new List<ContactReply>();
			try
			{
				List<tblContactReply> list = (
					from x in this.db.tblContactReplies
					where x.contactId == contactId
					select x).ToList<tblContactReply>();
				contactReplies = MapContact.ToEntityList(list);
			}
			catch (Exception exception)
			{
				contactReplies = new List<ContactReply>();
			}
			return contactReplies;
		}

		public List<ContactReply> GetAnswerListByContactId(int contactId, int adminId)
		{
			List<ContactReply> contactReplies = new List<ContactReply>();
			try
			{
				List<spContactAnswerListGetById_Result> list = this.db.spContactAnswerListGetById(new int?(contactId), new int?(adminId)).ToList<spContactAnswerListGetById_Result>();
				contactReplies = MapContact.ToEntityList(list);
			}
			catch (Exception exception)
			{
				contactReplies = new List<ContactReply>();
			}
			return contactReplies;
		}

		public Contact GetByApplicantId(int inductionId, int contactId, int applicantId)
		{
			Contact contact = new Contact();
			try
			{
				tvwContact _tvwContact = (
					from x in this.db.tvwContacts
					where x.inductionId == inductionId && x.contactId == contactId && x.applicantId == applicantId
					select x).FirstOrDefault<tvwContact>();
				if ((_tvwContact == null ? false : _tvwContact.contactId > 0))
				{
					contact = MapContact.ToEntity(_tvwContact);
				}
			}
			catch (Exception exception)
			{
				contact = new Contact();
			}
			return contact;
		}

		public Contact GetById(int contactId)
		{
			Contact contact = new Contact();
			try
			{
				tvwContact _tvwContact = this.db.tvwContacts.FirstOrDefault<tvwContact>((tvwContact x) => x.contactId == contactId);
				contact = MapContact.ToEntity(_tvwContact);
			}
			catch (Exception exception)
			{
				contact = new Contact();
			}
			return contact;
		}

		public Contact GetById(int contactId, int adminId)
		{
			Contact contact = new Contact();
			try
			{
				spContactQuestionGetById_Result spContactQuestionGetByIdResult = this.db.spContactQuestionGetById(new int?(contactId), new int?(adminId)).FirstOrDefault<spContactQuestionGetById_Result>();
				if ((spContactQuestionGetByIdResult == null ? false : spContactQuestionGetByIdResult.contactId > 0))
				{
					contact = MapContact.ToEntity(spContactQuestionGetByIdResult);
				}
			}
			catch (Exception exception)
			{
				contact = new Contact();
			}
			return contact;
		}

		public List<Contact> GetByTypeId(int typeId, int inductionId)
		{
			List<Contact> contacts = new List<Contact>();
			try
			{
				List<tvwContact> list = (
					from x in this.db.tvwContacts
					where x.typeId == typeId && x.inductionId == inductionId
					select x).ToList<tvwContact>();
				contacts = MapContact.ToEntityList(list);
			}
			catch (Exception exception)
			{
				contacts = new List<Contact>();
			}
			return contacts;
		}

		public DataTable GetContactStatus(int inductionId, int typeId)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spContactStatus]"
			};
			sqlCommand.Parameters.AddWithValue("@inductionId", inductionId);
			sqlCommand.Parameters.AddWithValue("@typeId", typeId);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public ContactStatus GetContactStatusById(int contactId)
		{
			ContactStatus contactStatu = new ContactStatus();
			try
			{
				tblContactStatu _tblContactStatu = (
					from x in this.db.tblContactStatus
					where x.contactId == contactId
					orderby x.id descending
					select x).FirstOrDefault<tblContactStatu>();
				contactStatu = MapContact.ToEntity(_tblContactStatu);
			}
			catch (Exception exception)
			{
				contactStatu = new ContactStatus();
			}
			return contactStatu;
		}

		public List<Contact> GetQuestionByApplicant(int typeId, int inductionId, int applicantId)
		{
			List<Contact> contacts = new List<Contact>();
			try
			{
				List<tvwContact> list = (
					from x in this.db.tvwContacts
					where x.typeId == typeId && x.inductionId == inductionId && x.applicantId == applicantId
					select x).ToList<tvwContact>();
				contacts = MapContact.ToEntityList(list);
			}
			catch (Exception exception)
			{
				contacts = new List<Contact>();
			}
			return contacts;
		}
	}
}