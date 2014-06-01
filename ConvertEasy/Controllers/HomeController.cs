using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Aspose.Words;
using Aspose.Words.Saving;
using ConvertApi;

namespace ConvertEasy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Using Aspose .Pdf Library
        /// </summary>
        /// <param name="pdfFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PdfToDoc(HttpPostedFileBase pdfFile)
        {
            if (pdfFile != null)
            {
                var outputStream = new MemoryStream();
                var fileName = Path.GetFileNameWithoutExtension(pdfFile.FileName);
                var fileExt = Path.GetExtension(pdfFile.FileName);
                if (fileExt == ".pdf")
                {
                    string uploadedPath;
                    if (SaveFile(pdfFile, out uploadedPath))
                    {
                        var document = new Aspose.Pdf.Document(uploadedPath);
                        var saveOptions = new Aspose.Pdf.DocSaveOptions();
                        document.Save(outputStream, saveOptions);

                        outputStream.Position = 0;
                        return File(outputStream, "application/msword", fileName + ".doc");
                    }
                    else
                    {
                        ModelState.AddModelError("", "File saving error : X001");
                        return RedirectToAction("Index");
                    }    
                }
            }
            else
            {
                ModelState.AddModelError("", "File format error : X002");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Using Aspose
        /// </summary>
        /// <param name="docFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DocToPdf(HttpPostedFileBase docFile)
        {
            if (docFile != null)
            {
                var outputStream = new MemoryStream();
                var fileName = Path.GetFileNameWithoutExtension(docFile.FileName);
                var fileExt = Path.GetExtension(docFile.FileName);
                if (fileExt == ".doc")
                {
                    string uploadedPath;
                    if (SaveFile(docFile, out uploadedPath))
                    {
                        var document = new Aspose.Words.Document(uploadedPath);
                        document.Save(outputStream, SaveFormat.Pdf);

                        outputStream.Position = 0;
                        return File(outputStream, "application/pdf", fileName + ".pdf");
                    }
                    else
                    {
                        ModelState.AddModelError("", "File saving error : X001");
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "File format error : X002");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        private bool SaveFile(HttpPostedFileBase file, out string uploadPath)
        {
            bool result = false;
            string filename = Helpers.StringExtensions.ToFriendlyUrl(file.FileName);

            if (!string.IsNullOrEmpty(filename))
            {
                uploadPath = Server.MapPath("/Upload/Temp/" + Path.GetRandomFileName());
                Directory.CreateDirectory(uploadPath);
                uploadPath += "\\" + filename;

                file.SaveAs(uploadPath);
                result = true;
            }
            else
                uploadPath = String.Empty;

            return result;
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}