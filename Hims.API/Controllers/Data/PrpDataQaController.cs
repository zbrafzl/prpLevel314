using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Prp.Data;


namespace Hims.API.Controllers
{
    //[RoutePrefix("data-qa")]
    public class PrpDataQaController : PrpDataQaBaseController
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

        [HttpGet]
        [Route("induction/getall")]
        public HttpResponseMessage GetInductionList(int projId)
        {
            DataTable dt = new APIDataDAL().APIInductionGetAll(projId);
            try
            {
                var Result = this.Request.CreateResponse(HttpStatusCode.OK, dt, new JsonMediaTypeFormatter());
                return Result;
            }
            catch (Exception ex)
            {
                HttpError Error = new HttpError("Some thing went Wrong") { { "IsSuccess", false } };
                return this.Request.CreateErrorResponse(HttpStatusCode.OK, Error);
            }       
        }

        [HttpGet]
        [Route("institute/getall")]
        public HttpResponseMessage GetInstituteList(int projId)
        {
            DataTable dt = new APIDataDAL().APIInstituteGetAll(projId);
            try
            {
                var Result = this.Request.CreateResponse(HttpStatusCode.OK, dt, new JsonMediaTypeFormatter());
                return Result;
            }
            catch (Exception ex)
            {
                HttpError Error = new HttpError("Some thing went Wrong") { { "IsSuccess", false } };
                return this.Request.CreateErrorResponse(HttpStatusCode.OK, Error);
            }
        }

        [HttpGet]
        [Route("hospital/getall")]
        public HttpResponseMessage GetHospitalList(int projId)
        {
            DataTable dt = new APIDataDAL().APIHospitalGetAll(projId);
            try
            {
                var Result = this.Request.CreateResponse(HttpStatusCode.OK, dt, new JsonMediaTypeFormatter());
                return Result;
            }
            catch (Exception ex)
            {
                HttpError Error = new HttpError("Some thing went Wrong") { { "IsSuccess", false } };
                return this.Request.CreateErrorResponse(HttpStatusCode.OK, Error);
            }
        }

        [HttpPost]
        [Route("hospital/byinstitute")]
        public HttpResponseMessage GetHospitalGetByInstitute(HospitalReq obj)
        {
            DataTable dt = new APIDataDAL().APIHospitalGetByInstitute(obj);
            try
            {
                var Result = this.Request.CreateResponse(HttpStatusCode.OK, dt, new JsonMediaTypeFormatter());
                return Result;
            }
            catch (Exception ex)
            {
                HttpError Error = new HttpError("Some thing went Wrong") { { "IsSuccess", false } };
                return this.Request.CreateErrorResponse(HttpStatusCode.OK, Error);
            }
        }


    }
}
