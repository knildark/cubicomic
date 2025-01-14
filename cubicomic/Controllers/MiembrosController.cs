﻿using System.Web.Mvc;
using cubicomic.Models;
using cubicomic.DAL;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using cubicomic.ViewModels;
using System.IO;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace cubicomic.Controllers
{
    public class MiembrosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Miembros
        public ActionResult Perfil(string id)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id);
            //Datos del usuario
            PerfilViewModel model = new PerfilViewModel();
            model.Id = user.Id;
            model.UserName = user.UserName;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.CompleteName = user.FirstName + " " + user.LastName;
            model.Email = user.Email;
            model.Avatar = user.Avatar;
            if(user.emailDonacion != null)
            {
                ViewBag.donacion = user.emailDonacion;

            }

            //Archivos

            List<string> listaRutaImagenes = new List<string>();
            var carpeta = Server.MapPath("~") + @"Uploads";
            Debug.WriteLine(carpeta);
            //Necesitas: using System.IO; para realizar esto
            DirectoryInfo d = new DirectoryInfo(carpeta);
            //Obtenemos todos los .jpg
            FileInfo[] Files = d.GetFiles("*"+user.Id+"*");
            //Recorremos la carpeta
            foreach (FileInfo file in Files)
            {
                listaRutaImagenes.Add(file.Name);
            }
            ViewBag.lista = listaRutaImagenes;

            return View(model);
        }

        // GET: Image
        public ActionResult ShowAvatar(string id)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id);
            //var imageId = db.Files.Find(id);
            //File image = db.Files.Include(s => s.File).SingleOrDefault(s => s.ID == id);
            try
            {
                var imageData = user.Avatar.Content;
                var imageType = user.Avatar.ContentType;
                return File(imageData, imageType);
            }
            catch(Exception e)
            {
                return null;
            }



        }

        public ActionResult Galeria()
        {
            return View();
        }

        public ActionResult ConfigCuenta2(string id)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id);
            //Datos del usuario
            PerfilViewModel model = new PerfilViewModel();
            model.Id = user.Id;
            model.UserName = user.UserName;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.CompleteName = user.FirstName + " " + user.LastName;
            model.Email = user.Email;
            model.Avatar = user.Avatar;
            if ( user.emailDonacion != null )
            {
                ViewBag.donacion2 = user.emailDonacion;

            }

            //Archivos

            List<string> listaRutaImagenes = new List<string>();
            var carpeta = Server.MapPath("~") + @"Uploads";
            Debug.WriteLine(carpeta);
            //Necesitas: using System.IO; para realizar esto
            DirectoryInfo d = new DirectoryInfo(carpeta);
            //Obtenemos todos los .jpg
            FileInfo[] Files = d.GetFiles("*" + user.Id + "*");
            //Recorremos la carpeta
            foreach ( FileInfo file in Files )
            {
                listaRutaImagenes.Add(file.Name);
            }
            ViewBag.lista = listaRutaImagenes;

            return View(model);
        }

        // GET: Miembros/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Miembros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Miembros/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationUser miembro = db.Include(s => s.Files).SingleOrDefault(s => s.ID == id);
        //    if (miembro == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(miembro);
        //}

        // POST: miembro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        //public ActionResult EditPost(int? id, HttpPostedFileBase upload)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var miembroToUpdate = db.miembros.Find(id);
        //    if (TryUpdateModel(miembroToUpdate, "",
        //       new string[] { "LastName", "FirstMidName", "EnrollmentDate" }))
        //    {
        //        try
        //        {
        //            if (upload != null && upload.ContentLength > 0)
        //            {
        //                if (miembroToUpdate.Files.Any(f => f.FileType == FileType.Avatar))
        //                {
        //                    db.Files.Remove(miembroToUpdate.Files.First(f => f.FileType == FileType.Avatar));
        //                }
        //                var avatar = new File
        //                {
        //                    FileName = System.IO.Path.GetFileName(upload.FileName),
        //                    FileType = FileType.Avatar,
        //                    ContentType = upload.ContentType
        //                };
        //                using (var reader = new System.IO.BinaryReader(upload.InputStream))
        //                {
        //                    avatar.Content = reader.ReadBytes(upload.ContentLength);
        //                }
        //                miembroToUpdate.Files = new List<File> { avatar };
        //            }
        //            db.Entry(miembroToUpdate).State = EntityState.Modified;
        //            db.SaveChanges();

        //            return RedirectToAction("Index");
        //        }
        //        catch (RetryLimitExceededException /* dex */)
        //        {
        //            //Log the error (uncomment dex variable name and add a line here to write a log.
        //            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
        //        }
        //    }
        //    return View(miembroToUpdate);
        //}

        // GET: Miembros/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Miembros/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult _donacion(string email)
        {
            return View();
        }
        [HttpPost]
        public ActionResult getDonacion(string email)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var original = db.Users.Find(user.Id);
            if ( email == "")
            {
                TempData["notice"] = "por favor rellena el campo email";
                return RedirectToAction("Perfil", "Miembros", new { id = user.Id });
            }

            var sql = @"Update [AspNetUsers] SET emailDonacion = {0} WHERE Id = {1}";
          db.Database.ExecuteSqlCommand(sql, email, user.Id);
          db.SaveChanges();
            return RedirectToAction("Perfil", "Miembros", new { id = user.Id });
        }
        public ActionResult _editar(string nombre, string apellido, string email)
        {
            return View();
        }
        [HttpPost]
        public ActionResult setEditar(string nombre, string apellido, string email)
        {
         
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var original = db.Users.Find(user.Id);
            if (nombre == "" && apellido == "" && email =="")
            {
                TempData["notice"] = "por favor rellena algun campo a modificar";
                return RedirectToAction("Perfil", "Miembros", new { id = user.Id });
            }
            if(nombre == "")
            {
                nombre = user.FirstName;
            }
            if(apellido == "")
            {
                apellido = user.LastName;
            }
            if(email =="")
            {
                email = user.Email;
            }
            var sql = @"Update [AspNetUsers] SET FirstName = {0}, LastName = {1}, Email ={2} WHERE Id = {3}";
            db.Database.ExecuteSqlCommand(sql, nombre,apellido, email, user.Id);
            db.SaveChanges();
            return RedirectToAction("Perfil", "Miembros", new { id = user.Id });
        }
    }
}