using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Prp.Data;

namespace Prp.Sln.Controllers
{
    public class CommonController : Controller
    {

        [HttpGet]
        public JsonResult GetCurrentDateTime()
        {
            DateTime date = DateTime.UtcNow;
            //https://stackoverflow.com/questions/46120116/json-date-and-datetime-serialisation-in-c-sharp-newtonsoft
            string sdsd = JsonConvert.SerializeObject(date);
            return Json(date, JsonRequestBehavior.AllowGet);
        }

        public FileResult GetCaptchaImage()
        {
            Message msg = CaptchaGenerate();
            string text = msg.msg;

            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            Font font = new Font("Arial", 15);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width + 40, (int)textSize.Height + 20);
            drawing = Graphics.FromImage(img);

            Color backColor = Color.SeaShell;
            Color textColor = Color.Red;
            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 20, 10);

            drawing.Save();

            font.Dispose();
            textBrush.Dispose();
            drawing.Dispose();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();

            return File(ms.ToArray(), "image/png");
        }


        public JsonResult GenerateCaptcha()
        {
            Message msg = CaptchaGenerate();
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public Message CaptchaGenerate()
        {
            Message msg = new Message();

            try
            {
                msg.msg = ProjFunctions.GetCaptchaText();
                Session["CAPTCHA"] = msg.msg;

            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
                msg.msg = ex.Message;
            }

            return msg;
        }

        public JsonResult CompareCaptcha(string captcha)
        {
            Message msg = new Message();
            try
            {

                string serverCaptcha = Session["CAPTCHA"].ToString();

                if (!captcha.Equals(serverCaptcha))
                {
                    msg.status = false;
                }
                else
                {
                    msg.status = true;
                }
            }
            catch (Exception ex)
            {
                msg.status = false;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }



        #region Commons

        [HttpGet]
        public JsonResult RegionGetByCondition(int typeId, int parentId, string condition)
        {
            List<Prp.Data.Region> list = new RegionDAL().RegionGetByCondition(typeId, parentId, condition);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult InstitueGetByType(int typeId)
        {
            List<Institute> list = new InstitueDAL().GetAll(typeId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ConstantGetByType(int typeId)
        {
            List<Constant> list = new ConstantDAL().GetAll(typeId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ConstantGetForDDL(DDLConstants obj)
        {
            List<EntityDDL> list = new ConstantDAL().GetConstantDDL(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SpecialityGetForDDL(DDLSpecialitys obj)
        {
            List<EntityDDL> list = new SpecialityDAL().GetSpecialityDDL(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult JournalGetForDDL(DDLJournal obj)
        {
            List<EntityDDL> list = new MasterSetupDAL().GetJournalForDDL(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //

        [HttpPost]
        public JsonResult DDLGetInstitute(DDLSearch obj)
        {
            List<EntityDDL> list = DDLInstitute.GetAll(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InstituteGetForDDL(DDLInstitutes obj)
        {
            List<EntityDDL> list = new InstitueDAL().GetInstituteDDL(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DDLGetRegion(DDLSearch obj)
        {
            List<EntityDDL> list = DDLRegion.GetAll(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DDLSpecialityGet(DDLSearch obj)
        {
            List<EntityDDL> list = DDLSpeciality.GetAll(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult HospitalGetForDDL(DDLHospitals obj)
        {

            obj.typeId = obj.typeId.TooInt();
            obj.typeId = obj.typeId.TooInt();
            obj.userId = obj.userId.TooInt();
            obj.reffIds = obj.reffIds.TooString();

            List<EntityDDL> list = new HospitalDAL().GetHospitalDDL(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DDLGetDepartment(DDLSearch obj)
        {
            List<EntityDDL> list = DDLDepartment.GetAll(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DDLGetUnit(DDLSearch obj)
        {
            List<EntityDDL> list = DDLUnit.GetAll(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DDLGetDiscipline(DDLSearch obj)
        {
            List<EntityDDL> list = DDLDiscipline.GetAll(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DDLGetSpeciality(DDLSearch obj)
        {
            List<EntityDDL> list = DDLSpeciality.GetAll(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Image

        [HttpPost]
        public ActionResult UploadImage()
        {
            Message msg = new Message();
            string imageName = "";
            string folderPath = "/Images/Applicant/";

            try
            {

                string imageType = Request.Form["imageType"].TooString();
                int applicantId = Request.Form["applicantId"].TooInt();
                int inductionId = ProjConstant.inductionId;

                folderPath = folderPath + "/" + applicantId;

                CreateDirectory(folderPath);

                // Checking no of files injected in Request object  
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        //  Get all files from Request object  
                        HttpFileCollectionBase files = Request.Files;
                        for (int i = 0; i < files.Count; i++)
                        {
                            //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                            //string filename = Path.GetFileName(Request.Files[i].FileName);  

                            HttpPostedFileBase file = files[i];
                            string fname;

                            // Checking for Internet Explorer  
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }

                            imageName = imageType + "_" + inductionId + Path.GetExtension(fname);


                            //imageName = fname;

                            //int number = 1;
                            //imageName = MakeUniqueFileName(folderPath, imageName, number);
                            //if (String.IsNullOrWhiteSpace(imageName))
                            //    imageName = fname;

                            string imagePath = Path.Combine(Server.MapPath(folderPath), imageName);
                            if (System.IO.File.Exists(imagePath))
                            {
                                imageName = imageType + "_" + inductionId + "_1" + Path.GetExtension(fname);
                                imagePath = Path.Combine(Server.MapPath(folderPath), imageName);

                                if (System.IO.File.Exists(imagePath))
                                {
                                    imageName = imageType + "_" + inductionId + "_2" + Path.GetExtension(fname);
                                    imagePath = Path.Combine(Server.MapPath(folderPath), imageName);

                                    if (System.IO.File.Exists(imagePath))
                                    {
                                        imageName = imageType + "_" + inductionId + "_3" + Path.GetExtension(fname);
                                        imagePath = Path.Combine(Server.MapPath(folderPath), imageName);

                                        if (System.IO.File.Exists(imagePath))
                                        {
                                            imageName = imageType + "_" + inductionId + "_4" + Path.GetExtension(fname);
                                            imagePath = Path.Combine(Server.MapPath(folderPath), imageName);

                                            if (System.IO.File.Exists(imagePath))
                                            {
                                                imageName = imageType + "_" + inductionId + "_5" + Path.GetExtension(fname);
                                                imagePath = Path.Combine(Server.MapPath(folderPath), imageName);
                                            }
                                        }
                                    }
                                }
                            }

                            // Get the complete folder path and store the file inside it.  
                            //imagePath = Path.Combine(Server.MapPath(folderPath), imageName);

                            file.SaveAs(imagePath);
                        }

                        msg.id = 1;
                        msg.msg = imageName;
                    }
                    catch (Exception ex)
                    {
                        msg.id = 0;
                        msg.msg = ex.Message;
                    }
                }
                else
                {
                    msg.id = 0;
                    msg.msg = "No image selected";
                }
            }
            catch (Exception ex)
            {

                msg.id = 0;
                msg.msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        private string MakeUniqueFileName(string folderPath, string imageName, int number)
        {
            string imageFileName = imageName;
            try
            {
                string imagePath = Path.Combine(Server.MapPath(folderPath), imageName);
                if (System.IO.File.Exists(imagePath))
                {
                    imageName = Path.GetFileNameWithoutExtension(imageName) + number + Path.GetExtension(imageName);
                    imageFileName = MakeUniqueFileName(folderPath, imageName, number);
                }
            }
            catch (Exception)
            {
            }
            return imageFileName;
        }

        public void CreateDirectory(string subPath)
        {
            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

        }


        #endregion
    }
}