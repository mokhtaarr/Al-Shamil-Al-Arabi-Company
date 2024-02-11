using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Service.Controllers;
using Website.Controllers;
using App.Dal.Model;
using AutoMapper;
using App.VM;
using App.Static.Enums;
using App.Static.Files;
using System.IO;
using System.Configuration;
using Website.Areas.Admin.Data;
using System.Net;
using System.Net.Mail;

namespace Website.Areas.Admin.Controllers
{
    public class ContactUsController : BaseController
    {
        public ContactUsService Service = new ContactUsService();
        int Language { get; set; }
        // GET: Admin/ContactUs
        [CustomAuthenticationFilter]
        public ActionResult Index()
        {
            Language = GetLanguage();
            return View();
        }
        
        public ActionResult GettAll()
        {
            var GetContactUs = Service.GetContactUs();
            List<ContactUsVM> ContactUsVM = new List<ContactUsVM>();
            ContactUsVM.Add(GetContactUs);
            return Json(new { data = ContactUsVM }, JsonRequestBehavior.AllowGet);
        }
        
        // GET: Admin/ContactUs/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Language = GetLanguage();
                ContactUs ContactUs = Service.GetContactUsById(id);
                return PartialView("_Create", ContactUs);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/ContactUs/Create
        public ActionResult Create()
        {
            Language = GetLanguage();
            return PartialView("_Create");
        }

        // POST: Admin/ContactUs/Create
        [HttpPost]
        public ActionResult Create(ContactUs ContactUs)
        {
            try
            {
                bool result = SaveImages(ContactUs);
                if (ContactUs.Id == 0)
                {
                    // Save
                    Service.Post(ContactUs);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Edit
                    Service.Put(ContactUs);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/ContactUs/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Service.Delete(id);
                return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public bool SaveImages(ContactUs Model)
        {
            Model.Image = string.Empty;
            HttpFileCollectionBase files = Request.Files;
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    ContactUs Item = new ContactUs();
                    if (file != null && file.ContentLength > 0)
                    {
                        string fname;
                        fname = file.FileName;
                        // Get the complete folder path and store the file inside it.    
                        string fileName = Path.GetFileName(fname);
                        string _name = FileHelper.GetFileNewNamewithoutfolder(fileName, Path.GetExtension(fileName));
                        Model.Image = _name;
                        HttpCookie Logo = new HttpCookie("Logo", _name);
                        Logo.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(Logo);
                        string path = Path.Combine(Server.MapPath("~/"+ ConfigurationManager.AppSettings["LocalPath"] + "/"), _name);

                        file.SaveAs(path);
                        if (Model.Id != 0)
                        {
                            Item = Service.GetContactUsById(Model.Id);
                            List<string> images = new List<string>();
                            if (Item.Image != null)
                            {
                                images.AddRange(Item.Image.Split(',').ToList());
                                foreach (var item in images)
                                {
                                    string str = Path.Combine(Server.MapPath("~/" + ConfigurationManager.AppSettings["LocalPath"] + "/"), item);
                                    if (System.IO.File.Exists(str))
                                    {
                                        System.IO.File.Delete(str);
                                    }
                                }
                            }
                        }
                    }
                    else if (Model.Id != 0)
                    {
                        Item = Service.GetContactUsById(Model.Id);
                        Model.Image = Item.Image;
                    }
                }
            }
            else if (Model.Id != 0 && (Model.Image == "" || Model.Image == null))
            {
                var Item = Service.GetContactUsById(Model.Id);
                Model.Image = Item.Image;
            }
            return true;
        }

        [HttpPost]
        public ActionResult SendMail(Mail _objModelMail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactUsVM MailDB = Service.GetContactUs();
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(MailDB.Email);
                    //mail.From = new MailAddress(_objModelMail.Email);
                    mail.To.Add(MailDB.Email);
                    mail.Subject = _objModelMail.Subject;
                    string Body = "The sender's name : " + _objModelMail.Name + "<br/>" +
                                      "From : " + _objModelMail.Email + "<br/>" +
                                      "Subject : " + _objModelMail.Subject + "<br/>" +
                                      "Message : " + _objModelMail.Body + "<br/>";
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(MailDB.Email, MailDB.EmailPassword); // Enter seders User name and password       
                    //smtp.Host = "mail.titaniumcybersolutions.com";
                    smtp.Host = MailDB.Host;
                    smtp.Port = MailDB.Port;
                    //smtp.Port = 8889;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = MailDB.EnableSsl;
                    //smtp.EnableSsl = false;
                    smtp.Send(mail);
                    return Json(new { Status = true, Data = "Valid" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Status = false, Data = "NotValid" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Data = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Subscribe(string email)
        {
            try
            {
                bool status = Service.Subscribe(email);
                return Json(new { Status = status }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CheckIfEmailExisting(string EmailSubscribed)
        {
            try
            {
                bool status = Service.CheckIfEmailExisting(EmailSubscribed);
                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
