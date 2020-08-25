using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using Microsoft.AspNet.Identity;
using BugTracker.Helpers;
using System.Web.Configuration;
using System.IO;

namespace BugTracker.Controllers
{
    public class TicketAttachmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketAttachments
        public ActionResult Index()
        {
            var ticketAttachments = db.TicketAttachments.Include(t => t.Ticket).Include(t => t.User);
            return View(ticketAttachments.ToList());
        }

        // GET: TicketAttachments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            if (ticketAttachment == null)
            {
                return HttpNotFound();
            }
            return View(ticketAttachment);
        }


        // POST: TicketAttachments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketId,FileName")] TicketAttachment ticketAttachment, string attachmentDescription, HttpPostedFileBase attachment)
        {
            if (attachment == null)
            {
                TempData["Error"] = "You must supply a file";
                return RedirectToAction("Dashboard", "Tickets", new { id = ticketAttachment.TicketId });
            }

            if (ModelState.IsValid)
            {
                // step 1: run the file through a validator (size, extension)
                if (FileUploadValidator.IsWebFriendlyImage(attachment) || FileUploadValidator.IsWebFriendlyFile(attachment))
                {
                    ticketAttachment.Description = attachmentDescription;
                    ticketAttachment.Created = DateTime.Now;
                    ticketAttachment.UserId = User.Identity.GetUserId();
                    // step 2: isolate, slug and stamp the file name using FileStamp in Helpers
                    var fileName = FileStamp.MakeUnique(attachment.FileName); // FileStamp the attachment file

                    // step 3: assign the FilePath property and save the physical file
                    var serverFolder = WebConfigurationManager.AppSettings["DefaultAttachmentFodler"];
                    attachment.SaveAs(Path.Combine(Server.MapPath(serverFolder), fileName));
                    ticketAttachment.FilePath = $"{serverFolder}{fileName}";
                    db.TicketAttachments.Add(ticketAttachment);
                    db.SaveChanges();
                }

                RedirectToAction("Dashboard", "Tickets", new { id = ticketAttachment.TicketId });
            }

            TempData["Error"] = "The model was invalid!";
            return RedirectToAction("Dashboard", "Tickets", new { id = ticketAttachment.TicketId });
        }



        // GET: TicketAttachments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            if (ticketAttachment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "SubmitterId", ticketAttachment.TicketId);

            return View(ticketAttachment);
        }

        // POST: TicketAttachments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketId,UserId,FilePath,Description,FileName,Created")] TicketAttachment ticketAttachment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketAttachment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticketAttachment);
        }

        // GET: TicketAttachments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            if (ticketAttachment == null)
            {
                return HttpNotFound();
            }
            return View(ticketAttachment);
        }

        // POST: TicketAttachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            db.TicketAttachments.Remove(ticketAttachment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
