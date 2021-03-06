﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitFriend.Models;
using Microsoft.AspNet.Identity;

namespace FitFriend.Controllers
{
    public class ExercisesController : Controller
    {
        private FitDBContext db = new FitDBContext();
        [Authorize]
        // GET: Exercises
        public ActionResult Index()
        {
            var currentUser = User.Identity.GetUserId();
            var exercise = db.Exercises.Where(x => x.ApplicationUserID == currentUser);
            return View(exercise);           
        }
        [Authorize]
        // GET: Exercises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercises exercises = db.Exercises.Find(id);
            if (exercises == null)
            {
                return HttpNotFound();
            }
            return View(exercises);
        }
        [Authorize]
        // GET: Exercises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,CaloriesBurned")] Exercises exercises)
        {
            if (ModelState.IsValid)
            {
                exercises.ApplicationUserID = User.Identity.GetUserId();
                db.Exercises.Add(exercises);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(exercises);
        }
        [Authorize]
        // GET: Exercises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercises exercises = db.Exercises.Find(id);
            if (exercises == null)
            {
                return HttpNotFound();
            }
            return View(exercises);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,CaloriesBurned")] Exercises exercises)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exercises).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exercises);
        }
        [Authorize]
        // GET: Exercises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercises exercises = db.Exercises.Find(id);
            if (exercises == null)
            {
                return HttpNotFound();
            }
            return View(exercises);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exercises exercises = db.Exercises.Find(id);
            db.Exercises.Remove(exercises);
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
