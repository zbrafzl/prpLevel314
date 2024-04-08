using Prp.Data;
using Prp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prp.Sln
{
    public class ModelBaseHs
    {
        public int hsId { get; set; }
        public Applicant loggedInUser { get; set; }
        public string redirectUrl { get; set; }
        public List<Ticker> listTicker { get; set; }
        public string dateTime { get; set; }

        public tblH objHs { get; set; }
        public List<HsStepCalendar> listHsStepCalendar { get; set; }

        public ModelBaseHs()
        {
            var dated = DateTime.Now;
            dateTime = DateTime.Now.ToString("MMM") + " " + dated.Day.TooString() + ", " + dated.Year.TooString()
                + " " + dated.Hour.TooString() + ":" + dated.Minute.TooString() + ":" + dated.Second.TooString();

            loggedInUser = ProjFunctions.CookieApplicantGetHs();
            objHs = new HsDAL().GetHsCurrent();
            hsId = objHs.hsId;
            try
            {
                listTicker = new MasterSetupDAL().TickerGetByInduction(6, hsId);
            }
            catch (Exception)
            {
            }
        }
    }

    public class LoginHsModel : ModelBaseHs
    {
        public int applicationId { get; set; }
        public bool isAlreadyConfirmed { get; set; }
        public Message msg { get; set; }
        public LoginHsModel()
        {
            msg = new Message();
        }
    }

    public class HomeHsModel : ModelBaseHs
    {
    }

    public class EmptyHsModel : ModelBaseHs
    {
    }
}