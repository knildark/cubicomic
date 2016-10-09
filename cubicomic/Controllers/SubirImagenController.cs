﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace cubicomic.Controllers
{
    public class SubirImagenController : Controller
    {
        // GET: SubirImagen
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void Subir(HttpPostedFileBase file)
        {
            
            string targetPath = @"~/Uploads/";
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }
            if (file == null) return;
            

            string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName).ToLower();

            file.SaveAs(Server.MapPath("~/Uploads/" + archivo));

            
        }
    }


}