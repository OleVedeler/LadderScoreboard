using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DotNetAppSqlDb.Models;

namespace DotNetAppSqlDb.Controllers
{
    public class LadderEntryController : Controller
    {
        private MyDatabaseContext db = new MyDatabaseContext();

        // GET: Todos
        public ActionResult Index()
        {
            Trace.WriteLine("GET /LadderEntry/Index");

            var ladderEntries = db.LadderEntries.ToList();

            var sortedLadderEntries = ladderEntries.OrderBy(o => o.Rank).ToList();
            return View(sortedLadderEntries);
        }

        // GET: Todos
        public ActionResult Admin()
        {
            Trace.WriteLine("GET /LadderEntry/Admin");

            var ladderEntries = db.LadderEntries.ToList();

            var sortedLadderEntries = ladderEntries.OrderBy(o => o.Rank).ToList();
            return View(sortedLadderEntries);
        }

        // GET: Todos/Details/5
        public ActionResult Details(int? id)
        {
            Trace.WriteLine("GET /LadderEntry/Details/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LadderEntry ladderEntry = db.LadderEntries.Find(id);
            if (ladderEntry == null)
            {
                return HttpNotFound();
            }
            return View(ladderEntry);
        }

        // GET: Todos/Create
        public ActionResult Create()
        {
            Trace.WriteLine("GET /LadderEntry/Create");
            return View(new LadderEntry());
        }

        // POST: Todos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArmyList,Army,PlayerName, Rank")] LadderEntry ladderEntry)
        {
            Trace.WriteLine("POST /LadderEntry/Create");


            if (ModelState.IsValid)
            {
                var count = db.LadderEntries.Count() + 1;
                ladderEntry.Rank = count.ToString();
                db.LadderEntries.Add(ladderEntry);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }

            return View(ladderEntry);
        }

        // GET: Todos/Edit/5
        public ActionResult Edit(int? id)
        {
            Trace.WriteLine("GET /LadderEntry/Edit/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LadderEntry ladderEntry = db.LadderEntries.Find(id);
            if (ladderEntry == null)
            {
                return HttpNotFound();
            }
            return View(ladderEntry);
        }

        // POST: Todos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Rank,ArmyList,Army,PlayerName")] LadderEntry ladderEntry)
        {
            Trace.WriteLine("POST /LadderEntry/Edit/" + ladderEntry.ID);
            if (ModelState.IsValid)
            {
                var previousEntryData = db.LadderEntries.Find(ladderEntry.ID)?.Rank;
                
                var swapEntry = db.LadderEntries.FirstOrDefault(x => x.Rank == ladderEntry.Rank && x.ID != ladderEntry.ID);

                if (swapEntry != null && previousEntryData != null)
                {
                    swapEntry.Rank = previousEntryData;
                    db.Entry(swapEntry).State = EntityState.Modified;
                }

                db.Entry(ladderEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(ladderEntry);
        }

        // GET: Todos/Delete/5
        public ActionResult Delete(int? id)
        {
            Trace.WriteLine("GET /LadderEntry/Delete/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LadderEntry ladderEntry = db.LadderEntries.Find(id);
            if (ladderEntry == null)
            {
                return HttpNotFound();
            }
            return View(ladderEntry);
        }

        // POST: Todos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trace.WriteLine("POST /LadderEntry/Delete/" + id);
            LadderEntry ladderEntry = db.LadderEntries.Find(id);
            db.LadderEntries.Remove(ladderEntry);
            db.SaveChanges();
            return RedirectToAction("Admin");
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