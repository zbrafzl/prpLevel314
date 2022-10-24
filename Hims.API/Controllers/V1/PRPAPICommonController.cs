﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Prp.Data;

namespace Hims.API.Controllers
{
    [RoutePrefix("prod")]
    public class PRPAPICommonController : PRPAPIBaseV1Controller
    {

        
        [HttpGet]
        [Route("check-credentials")]
        public MessageAPI UserLogin()
        {
            MessageAPI obj = new MessageAPI();
            obj.message = "Valid User";
            obj.status = true;
            return obj;
        }


      
        
    }
}
