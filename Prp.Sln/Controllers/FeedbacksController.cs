using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
	public class FeedbacksController : BaseController
	{
		public FeedbacksController()
		{
		}

		public ActionResult ConactUs()
		{
			ContactModel contactModel = new ContactModel();
			contactModel.contact.typeId = 1;
			contactModel.contact.name = base.loggedInUser.name;
			contactModel.contact.emailId = base.loggedInUser.emailId;
			contactModel.contact.pmdcNo = base.loggedInUser.pmdcNo;
			contactModel.listQuestion = (new ContactDAL()).GetQuestionByApplicant(1, ProjConstant.inductionId, base.loggedInUser.applicantId);
			ViewBag.message = TempData["Message"];
			return View(contactModel);
		}

		public ActionResult ConactUsDetail()
		{
			ContactModel contactModel = new ContactModel();
			int num = Request.QueryString["id"].TooInt();
			contactModel.contact = (new ContactDAL()).GetByApplicantId(ProjConstant.inductionId, num, base.loggedInUser.applicantId);
			if (contactModel.contact.contactId > 0)
			{
				contactModel.listAnswer = (new ContactDAL()).GetAnswerListByApplicant(num, base.loggedInUser.applicantId);
				contactModel.listDocs = (new ContactDAL()).GetAllDocsByTypeAndContact(1, num);
			}
			return View(contactModel);
		}

		[HttpPost]
		public ActionResult ContactUsSave(ContactModel model, IEnumerable<HttpPostedFileBase> flImages)
		{
			string absoluteUri = Request.Url.AbsoluteUri;
			Message message = new Message();
			Message message1 = new Message();
			int applicantId = 0;
			try
			{
                applicantId = base.loggedInUser.applicantId;
			}
			catch (Exception exception)
			{
                applicantId = 0;
			}
			Contact contact = model.contact;
			contact.applicantId = applicantId;
			if (applicantId > 0)
			{
				contact.emailId = base.loggedInUser.emailId;
				contact.name = base.loggedInUser.name;
				contact.pmdcNo = base.loggedInUser.pmdcNo;
				contact.applicantId = applicantId;

            }
			try
			{
				message = (new ContactDAL()).AddUpdate(contact);
				if (contact.typeId == 1)
				{
					if (message.id > 0)
					{
						EmailFunctions.SendEmailQuestion(contact);
					}
				}
				string str = "";
				try
				{
					if ((flImages == null || flImages.Count<HttpPostedFileBase>() <= 0 ? false : flImages.ToList<HttpPostedFileBase>()[0] != null))
					{
						str = Files.SaveMultipleFiles(flImages, "/images/contact/");
					}
				}
				catch (Exception exception1)
				{
					str = "";
				}
				str = str.TrimEnd(new char[] { ',' });
				if (!string.IsNullOrWhiteSpace(str))
				{
					message1 = (new ContactDAL()).AddUpdateDocs(message.id, str, 1);
				}
			}
			catch (Exception exception3)
			{
				Exception exception2 = exception3;
				message.id = 0;
				message.status = false;
				message.msg = exception2.Message;
			}
			TempData["Message"] = message;
			return this.Redirect(model.redirectUrl);
		}

		public ActionResult FeedBackByApplicant()
		{
			FeedBackModel feedBackModel = new FeedBackModel()
			{
				listFeedBack = (new FeedBackDAL()).GetByApplicant(base.loggedInUser.applicantId)
			};
			return View(feedBackModel);
		}

		public ActionResult GrievanceApplication()
		{
			ContactModel contactModel = new ContactModel()
			{
				isChangeAble = false
			};
			string absolutePath = HttpContext.Request.Url.AbsolutePath;
			contactModel.redirectUrl = absolutePath;
			absolutePath = absolutePath.TrimStart(new char[] { '/' });
			contactModel.typeId = 1;
			if (absolutePath == string.Concat(ProjConstant.preUrl, "/grievance-verification-application"))
			{
				contactModel.typeId = 11;
			}
			else if (absolutePath == string.Concat(ProjConstant.preUrl, "/grievance-gazette-application"))
			{
				contactModel.typeId = 21;
			}
            else if (absolutePath == "/hardship-apply")
            {
                contactModel.typeId = 31;
            }
            contactModel.listQuestion = (new ContactDAL()).GetQuestionByApplicant(contactModel.typeId, ProjConstant.inductionId, base.loggedInUser.applicantId);
			if ((contactModel.listQuestion == null ? true : contactModel.listQuestion.Count <= 0))
			{
				contactModel.contact.contactId = 0;
				contactModel.contact.typeId = contactModel.typeId;
				contactModel.contact.name = base.loggedInUser.name;
				contactModel.contact.pmdcNo = base.loggedInUser.pmdcNo;
				contactModel.contact.emailId = base.loggedInUser.emailId;
				contactModel.isChangeAble = true;
				contactModel.contact.typeId = contactModel.typeId;
			}
			else
			{
				Contact contact = (
					from x in contactModel.listQuestion
					where (x.typeId != contactModel.typeId ? false : x.totalReply == 0)
					select x).FirstOrDefault<Contact>();
				if ((contact == null ? false : contact.contactId > 0))
				{
					contactModel.contact.contactId = contact.contactId;
					contactModel.contact.typeId = contact.typeId;
					contactModel.contact.applicantId = contact.applicantId;
					contactModel.contact.name = base.loggedInUser.name;
					contactModel.contact.emailId = contact.emailId;
					contactModel.contact.pmdcNo = contact.pmdcNo;
					contactModel.contact.title = contact.title;
					contactModel.contact.question = contact.question;
					contactModel.isChangeAble = false;
				}
			}
			ViewBag.message = TempData["Message"];
			return View(contactModel);
		}

		[ValidateInput(false)]
		public ActionResult SaveFeedbackData(FeedBackModel ModelSave, HttpPostedFileBase files)
		{
			FeedBack modelSave = ModelSave.feedBack;
			modelSave.applicantIdBy = base.loggedInUser.applicantId.TooInt();
			modelSave.pmdcNo = modelSave.pmdcNo.TooString("");
			modelSave.comments = modelSave.comments.TooString("");
			modelSave.dated = DateTime.Now;
			(new FeedBackDAL()).AddUpdate(modelSave);
			return this.Redirect("/applicant/feedback");
		}

		[ValidateInput(false)]
		public ActionResult SaveGrievanceData(GrievanceModel ModelSave, HttpPostedFileBase files)
		{
			Grievance modelSave = ModelSave.grievance;
			modelSave.grievanceId = modelSave.grievanceId.TooInt();
			modelSave.typeId = 6;
			modelSave.typeId.TooInt();
			modelSave.verficationTypeId = modelSave.verficationTypeId.TooInt();
			modelSave.checkListIds = modelSave.checkListIds.TooString("");
			modelSave.applicantId = base.loggedInUser.applicantId.TooInt();
			modelSave.detail = modelSave.detail.TooString("");
			modelSave.title = modelSave.title.TooString("");
			(new GrievanceDAL()).AddUpdate(modelSave);
			return this.Redirect("/grievance-gazzette");
		}
	}
}