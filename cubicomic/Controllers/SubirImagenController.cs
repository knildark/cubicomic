﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using cubicomic.Models;

namespace cubicomic.Controllers
{
    public class SubirImagenController : Controller
    {
        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        // GET: SubirImagen
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PDF()
        {
            return View();
        }
        
        public ActionResult Comic()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Subir(IEnumerable<HttpPostedFileBase> file, String categoria)
        {
            if (file == null)
            {
                TempData["notice"] = "Ocurrio un error al subir la imagen, intente nuevamente";
                return RedirectToAction("Index", "SubirImagen");
            }
            if(user.Id == null)
            {
                TempData["notice"] = "Ocurrio un error al subir la imagen, Porfavor inicie una cuenta valida";
                return RedirectToAction("Index", "SubirImagen");
            }
            foreach (var files in file)
            {
                if (files != null && files.ContentLength > 0)
                {
                    string archivo = (user.Id + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") +"-"+ categoria + "-"+ "image" + files.FileName).ToLower();
                    files.SaveAs(Server.MapPath("~/Uploads/" + archivo));
                    
                }

            }
            TempData["notice"] = "Archivos subidos con exito";
            return RedirectToAction("Index", "SubirImagen");
        } 

        [HttpPost]
        public ActionResult SubirPDF(HttpPostedFileBase[] file, String categoria, string nombre)
        {
            if (file[0] != null && file[1]!= null)
            {
                string fileExt = Path.GetExtension(file[0].FileName).ToLower();
                string fileName = Path.GetFileName(file[0].FileName);
                if (file == null)
                {
                    TempData["notice"] = "Ocurrio un error al subir el PDF, intente nuevamente";
                    return RedirectToAction("Index", "SubirImagen");
                }
                if (file[0] != null && (fileExt == ".png" || fileExt == ".jpg"))
                {

                    string fileExt2 = Path.GetExtension(file[1].FileName).ToLower();
                    string fileName2 = Path.GetFileName(file[1].FileName);
                    string archivo = (user.Id + "-" + categoria + "-" + "pdf" + nombre + fileExt).ToLower();

                    if (file[1] != null && fileExt2 == ".pdf")
                    {
                        string archivo2 = (user.Id + "-" + categoria + "-" + "pdf" + nombre + fileExt2).ToLower();
                        String nuevo = null;
                        nuevo = archivo2.Replace(".jpg", ".pdf").Replace(".png", ".pdf");
                        file[0].SaveAs(Server.MapPath("~/Uploads/" + archivo));
                        file[1].SaveAs(Server.MapPath("~/UploadsPDF/" + nuevo));
                        TempData["notice"] = "Subido con exito";
                        return RedirectToAction("Index", "SubirImagen");
                    }
                }
            }
            TempData["notice"] = "Ocurrio un error al subir el PDF, intente nuevamente";
            return RedirectToAction("Index", "SubirImagen");
        }

        public ActionResult SubirComic(IEnumerable<HttpPostedFileBase> comic, HttpPostedFileBase[] portada, String categoria, string nombre)
        {
            if (user.Id == null)
            {
                TempData["notice"] = "Ocurrio un error al subir la imagen, Porfavor inicie una cuenta valida";
                return RedirectToAction("Index", "SubirImagen");
            }
            if (portada[0] != null && comic.LongCount() > 0 )
            {
                string fileExt = Path.GetExtension(portada[0].FileName).ToLower();
                string fileName = Path.GetFileName(portada[0].FileName);
                if (portada != null && (fileExt == ".png" || fileExt == ".jpg"))
                {
                    string archivoP;
                    if (nombre.LongCount() > 0)
                    {
                        archivoP = (user.Id + "-" + categoria + "-" + "-comic-" + nombre + fileExt).ToLower();
                    }
                    else
                    {
                        archivoP = (user.Id + "-" + categoria + "-" + "-comic-" + fileName).ToLower();
                    }

                    Directory.CreateDirectory(Server.MapPath("~/UploadsComic/" + archivoP));
                    if (comic != null)
                    {
                        foreach (var files in comic)
                        {
                            string fileExt2 = Path.GetExtension(files.FileName).ToLower();
                            string fileName2 = Path.GetFileName(files.FileName);
                            if (files != null && files.ContentLength > 0 && (fileExt2 == ".png" || fileExt2 == ".jpg"))
                            {
                                string archivo = (files.FileName).ToLower();
                                files.SaveAs(Server.MapPath("~/UploadsComic/" + archivoP + "/" + archivo));
                            }
                        }
                        portada[0].SaveAs(Server.MapPath("~/Uploads/" + archivoP));
                        TempData["notice"] = "Archivos subidos con exito";
                        return RedirectToAction("Index", "SubirImagen");
                    }
                }
            }
            TempData["notice"] = "Ocurrio un error al subir el comic, intente nuevamente";
            return RedirectToAction("Index", "SubirImagen");
        }

        public ActionResult SubirManga(IEnumerable<HttpPostedFileBase> manga, HttpPostedFileBase[] portada, string categoria, string nombre)
        {
            if (user.Id == null)
            {
                TempData["notice"] = "Ocurrio un error al subir la imagen, Porfavor inicie una cuenta valida";
                return RedirectToAction("Index", "SubirImagen");
            }
            if (portada[0] != null && manga.LongCount() > 0)
            {
                string fileExt = Path.GetExtension(portada[0].FileName).ToLower();
                string fileName = Path.GetFileName(portada[0].FileName);
                if (portada != null && (fileExt == ".png" || fileExt == ".jpg"))
                {
                    string archivoP;
                    if (nombre.LongCount() > 0)
                    {
                        archivoP = (user.Id + "-" + categoria + "-" + "-manga-" + nombre + fileExt).ToLower();
                    }
                    else
                    {
                        archivoP = (user.Id + "-" + categoria + "-" + "-manga-" + nombre + fileName).ToLower();
                    }

                    Directory.CreateDirectory(Server.MapPath("~/UploadsComic/" + archivoP));
                    if (manga != null)
                    {
                        foreach (var files in manga)
                        {
                            string fileExt2 = Path.GetExtension(files.FileName).ToLower();
                            string fileName2 = Path.GetFileName(files.FileName);
                            if (files != null && files.ContentLength > 0 && (fileExt2 == ".png" || fileExt2 == ".jpg"))
                            {
                                string archivo = (files.FileName).ToLower();
                                files.SaveAs(Server.MapPath("~/UploadsComic/" + archivoP + "/" + archivo));
                            }
                        }
                        portada[0].SaveAs(Server.MapPath("~/Uploads/" + archivoP));
                        TempData["notice"] = "Archivos subidos con exito";
                        return RedirectToAction("Index", "SubirImagen");
                    }
                }
            }
            TempData["notice"] = "Ocurrio un error al subir el manga, intente nuevamente";
            return RedirectToAction("Index", "SubirImagen");
        }
    }
}