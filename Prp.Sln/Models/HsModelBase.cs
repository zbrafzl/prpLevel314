﻿using Prp.Data;
using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Prp.Sln
{
    public class HsModelBased
    {
        public Applicant loggedInUser { get; set; }
        public string redirectUrl { get; set; }
        public int hsId { get; set; }
        public string dateTime { get; set; }
        public tblH  objHs { get; set; }
        public List<Ticker> listTicker { get; set; }
        public HsModelBased() {

            var dated = DateTime.Now;
            dateTime = DateTime.Now.ToString("MMM") + " " + dated.Day.TooString() + ", " + dated.Year.TooString()
                + " " + dated.Hour.TooString() + ":" + dated.Minute.TooString() + ":" + dated.Second.TooString();
            objHs = new HsDAL().GetHsCurrent();
            hsId = objHs.hsId;

            if (hsId == 0)
            {
                try
                {
                    ProjFunctions.RemoveCookies(ProjConstant.Cookies.loggedInApplicantHs);
                }
                catch (Exception exception)
                {
                }
            }

            loggedInUser = ProjFunctions.CookieApplicantGetHs();

            try
            {
                listTicker = new MasterSetupDAL().TickerGetByInduction(6, hsId);
            }
            catch (Exception)
            {
            }
        }
    }

   
   

    
}