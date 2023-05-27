using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Prp.Data
{
	public class FeedBackDAL : PrpDBConnect
	{
		public FeedBackDAL()
		{
		}

		public Message AddUpdate(FeedBack obj)
		{
			Message message = new Message();
			try
			{
				tblFeedBack _tblFeedBack = new tblFeedBack();
				if (obj.feedbackId != 0)
				{
					_tblFeedBack = this.db.tblFeedBacks.FirstOrDefault<tblFeedBack>((tblFeedBack x) => x.feedbackId == obj.feedbackId);
					_tblFeedBack.applicantIdBy = obj.applicantIdBy;
					_tblFeedBack.pmdcNo = obj.pmdcNo;
					_tblFeedBack.comments = obj.comments;
					_tblFeedBack.dated = obj.dated;
				}
				else
				{
					_tblFeedBack = MapFeedBack.ToTable(obj);
					this.db.tblFeedBacks.Add(_tblFeedBack);
				}
				this.db.SaveChanges();
				message.id = _tblFeedBack.feedbackId;
			}
			catch (Exception exception)
			{
				message.msg = exception.Message;
				message.id = 0;
			}
			return message;
		}

		public List<FeedBack> GetByApplicant(int applicantId)
		{
			List<FeedBack> feedBacks = new List<FeedBack>();
			try
			{
				List<tvwFeedback> list = (
					from x in this.db.tvwFeedbacks
					where x.feedBackById == applicantId
					select x).ToList<tvwFeedback>();
				feedBacks = MapFeedBack.ToEntityList(list);
			}
			catch (Exception exception)
			{
				feedBacks = new List<FeedBack>();
			}
			return feedBacks;
		}

		public FeedBack GetById(int feedbackId)
		{
			FeedBack feedBack = new FeedBack();
			try
			{
				tblFeedBack _tblFeedBack = this.db.tblFeedBacks.FirstOrDefault<tblFeedBack>((tblFeedBack x) => x.feedbackId == feedbackId);
				feedBack = MapFeedBack.ToEntity(_tblFeedBack);
			}
			catch (Exception exception)
			{
				feedBack = new FeedBack();
			}
			return feedBack;
		}

		public List<FeedBack> GetBySearch(FeedbackSearch obj)
		{
			List<FeedBack> feedBacks = new List<FeedBack>();
			try
			{
				List<spFeedbackSearch_Result> list = this.db.spFeedbackSearch(new int?(obj.top), new int?(obj.pageNum), obj.search, new DateTime?(obj.startDate), new DateTime?(obj.endDate)).ToList<spFeedbackSearch_Result>();
				feedBacks = MapFeedBack.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return feedBacks;
		}

		public Message UpdateFeedback(FeedBack obj)
		{
			Message message = new Message();
			try
			{
				tblFeedBack _tblFeedBack = new tblFeedBack();
				_tblFeedBack = this.db.tblFeedBacks.FirstOrDefault<tblFeedBack>((tblFeedBack x) => x.feedbackId == obj.feedbackId);
				_tblFeedBack.adminId = obj.adminId;
				_tblFeedBack.reply = obj.reply;
				_tblFeedBack.datedReply = obj.datedReply;
				this.db.SaveChanges();
				message.id = _tblFeedBack.feedbackId;
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