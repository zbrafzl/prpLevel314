using Newtonsoft.Json;
using Prp.Data;
using Prp.Sln;
using System;
using System.Data;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
    public class MeritGazatController : Controller
    {
        public MeritGazatController()
        {
        }

        public ActionResult SeatsList()
        {
            MeritGazatModel model = new MeritGazatModel();
            string param = Request.RequestContext.RouteData.Values["param"].TooString();
            string key = param.Decrypt();
            string[] arrStr = key.Split(',');
            model.cType = arrStr[0];
            model.inductionId = arrStr[1].TooInt();
            model.typeId = arrStr[2].TooInt();
            model.urlPram = param;

            model.isValid = true;
            if (model.cType != "s")
            {
                model.isValid = false;
            }
            if (model.inductionId != ProjConstant.inductionId)
            {
                model.isValid = false;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult SpecialityJobLisitngGetByParam(GazatMerit obj)
        {
            DataSet ds = new DataSet();
            try
            {
                string key = obj.urlPram.Decrypt();
                string[] arrStr = key.Split(',');
                int inductionId = arrStr[1].TooInt();
                obj.search = obj.search.TooString("");
                obj.adminId = 0;
                obj.inductionId = ProjConstant.inductionId;
                obj.typeId = arrStr[2].TooInt();

                if (inductionId == ProjConstant.inductionId)
                {
                    ds = (new MeritDAL()).SpecialityJobLisitngGetByParam(obj);
                }
            }
            catch (Exception)
            {
                ds = new DataSet();
            }
            string str = JsonConvert.SerializeObject(ds);
            return base.Content(str, "application/json");
        }


        public ActionResult Gazatte()
        {
            MeritGazatModel model = new MeritGazatModel();
            //string param = "", pageType = "";
            //int typeId = 0, inductionId = 0;
            //try
            //{
            //    param = Request.RequestContext.RouteData.Values["param"].TooString();
            //    var result = param.DecryptSimple();
            //    var arr = result.Split(',');
            //    pageType = arr[0].TooString();
            //    inductionId = arr[1].TooInt();
            //    typeId = arr[2].TooInt();
            //}
            //catch (Exception)
            //{
            //    param = "";
            //}
            model.urlPram = Request.RequestContext.RouteData.Values["param"].TooString();
            model.typeId = Request.RequestContext.RouteData.Values["typeId"].TooInt();
            return View(model);
        }
        [HttpPost]
        public ActionResult GazatteGetAllByTypeView(GazatMerit obj)
        {
            DataSet ds = new DataSet();
            try
            {
                //            string key = obj.urlPram.Decrypt();
                //            var value = key.TooInt();
                //            obj.typeId = value - ProjConstant.inductionId;
                //            int inductionId = value - obj.typeId;
                //            obj.search = obj.search.TooString("");
                //            obj.inductionId = ProjConstant.inductionId;
                //            if (inductionId == ProjConstant.inductionId)
                //            {
                //                ds = (new MeritDAL()).GazatGetAllByTypeView(obj);
                //            }

                obj.search = obj.search.TooString("");
                obj.inductionId = ProjConstant.inductionId;
                ds = (new MeritDAL()).GazatGetAllByTypeView(obj);
            }
            catch (Exception)
            {
                ds = new DataSet();
            }
            string str = JsonConvert.SerializeObject(ds);
            return base.Content(str, "application/json");
        }

        //////////////////////////



        [ValidateInput(false)]
        public ActionResult ExportDataToExcelAndDownload(MeritGazatModel ModelSave)
        {
            Message message = new Message();
            GazatMerit gazatMerit = new GazatMerit()
            {
                typeId = ModelSave.gazatMerit.typeId
            };
            try
            {
                string str = string.Concat(ModelSave.exportExcel.fileName, ".xlsx");
                string str1 = str.GenerateFilePath("/ExcelFiles/Gazat/");
                if (string.IsNullOrWhiteSpace(str1))
                {
                    message.status = false;
                    message.msg = "Error : File path and name creating.";
                }
                else
                {
                    DataTable dataTable = new DataTable();
                    dataTable = (new MeritDAL()).GazatGetAllByTypeExport(gazatMerit);
                    if ((dataTable == null ? true : dataTable.Rows.Count <= 0))
                    {
                        message.status = false;
                        message.msg = "";
                    }
                    else
                    {
                        message = str1.ExcelFileWrite(dataTable, "Sheet1", "A1");
                        str1.FileDownload();
                    }
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.msg = string.Concat("Error in exported : ", exception.Message);
            }
            if (string.IsNullOrWhiteSpace(ModelSave.exportExcel.url))
            {
                ModelSave.exportExcel.url = "/";
            }
            return this.Redirect(ModelSave.exportExcel.url);
        }


       


        [HttpGet]
        [ValidateInput(false)]
        public ActionResult ExportDataToExcelAndDownloadGazzat(GazatMerit obj)
        {
            Message message = new Message();
            obj.search = obj.search.TooString("");
            try
            {
                string str = "Gazzet-January-2023.xlsx".GenerateFilePath("/ExcelFiles/Gazat/");
                if (string.IsNullOrWhiteSpace(str))
                {
                    message.status = false;
                    message.msg = "Error : File path and name creating.";
                }
                else
                {
                    DataTable dataTable = new DataTable();
                    dataTable = (new MeritDAL()).GazatGetAllByTypeViewExport(obj);
                    if ((dataTable == null ? true : dataTable.Rows.Count <= 0))
                    {
                        message.status = false;
                        message.msg = "";
                    }
                    else
                    {
                        message = str.ExcelFileWrite(dataTable, "Sheet1", "A1");
                        str.FileDownload();
                    }
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.msg = string.Concat("Error in exported : ", exception.Message);
            }
            return this.Redirect("/ExcelFiles/Gazat/Gazzet-January-2023.xlsx");
        }





        [HttpPost]
        public ActionResult GazatGetAllByTypeViewExport(GazatMerit obj)
        {
            obj.search = obj.search.TooString("");
            DataTable dataTable = (new MeritDAL()).GazatGetAllByTypeViewExport(obj);
            string str = JsonConvert.SerializeObject(dataTable);
            return base.Content(str, "application/json");
        }

        public ActionResult GazatMD()
        {
            return View(new MeritGazatModel());
        }

        public ActionResult GazatMDS()
        {
            return View(new MeritGazatModel());
        }

        public ActionResult GazatMS()
        {
            return View(new MeritGazatModel());
        }

        public ActionResult JobListing()
        {
            return View(new MeritGazatModel());
        }

        public ActionResult MeritList()
        {
            MeritGazatModel model = new MeritGazatModel();
            model.urlPram = Request.RequestContext.RouteData.Values["param"].TooString();
            model.typeId = Request.RequestContext.RouteData.Values["typeId"].TooInt();
            return View(model);
        }

        [HttpPost]
        public ActionResult MeritGetAllByTypeView(GazatMerit obj)
        {
            DataSet ds = new DataSet();
            try
            {
                obj.search = obj.search.TooString("");
                obj.inductionId = ProjConstant.inductionId;
                obj.roundNo = ProjConstant.consentRound;
                ds = (new MeritDAL()).MeritGetAllByTypeView(obj);
            }
            catch (Exception)
            {
                ds = new DataSet();
            }
            string str = JsonConvert.SerializeObject(ds);
            return base.Content(str, "application/json");
        }

        public ActionResult MeritFCPS()
        {
            return View(new MeritGazatModel());
        }

        public ActionResult MeritFCPSD()
        {
            return View(new MeritGazatModel());
        }



        public ActionResult MeritMD()
        {
            return View(new MeritGazatModel());
        }

        public ActionResult MeritMDS()
        {
            return View(new MeritGazatModel());
        }

        public ActionResult MeritMS()
        {
            return View(new MeritGazatModel());
        }
    }
}