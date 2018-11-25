using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PointOpeOsaaminen.Models;
using PointOpeOsaaminen.ViewModel;

namespace PointOpeOsaaminen.Controllers
{
    public class OsaamisenController : Controller
    {
        private OpeOsaamisKantaEntities db = new OpeOsaamisKantaEntities();

        // GET: Osaamisen
        [HttpGet]
        public ActionResult Index()
        {
            var osaamiset = db.Osaamiset.Include(o => o.Opettaja);
            return View(osaamiset.ToList());
        }
        [HttpPost]
        public ActionResult Index(string Osaaminen, Osaamiset osa)
        {
            var osaaminen = db.Osaamiset.ToList().Where(o => o.Osaaminen.StartsWith(Osaaminen));
            return View(osaaminen);
        }
        // GET: Osaamisen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osaamiset osaamiset = db.Osaamiset.Find(id);
            if (osaamiset == null)
            {
                return HttpNotFound();
            }
            return View(osaamiset);
        }

        // GET: Osaamisen/Create
        public ActionResult Create()
        {
            ViewBag.OpettajaID = new SelectList(db.Opettaja, "OpettajaID", "Etunimi");
            return View();
        }

        // POST: Osaamisen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OsaamisID,Osaaminen,Kuvaus")] Osaamiset osaamiset)
        {
            if (ModelState.IsValid)
            {
                db.Osaamiset.Add(osaamiset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OpettajaID = new SelectList(db.Opettaja, "OpettajaID", "Etunimi", osaamiset.OpettajaID);
            return View(osaamiset);
        }

        // GET: Osaamisen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osaamiset osaamiset = db.Osaamiset.Find(id);
            if (osaamiset == null)
            {
                return HttpNotFound();
            }
            ViewBag.OpettajaID = new SelectList(db.Opettaja, "OpettajaID", "Etunimi", osaamiset.OpettajaID);
            return View(osaamiset);
        }

        // POST: Osaamisen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OsaamisID,Osaaminen,OpettamisenHalukkuus,Kuvaus,OpettajaID")] Osaamiset osaamiset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(osaamiset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OpettajaID = new SelectList(db.Opettaja, "OpettajaID", "Etunimi", osaamiset.OpettajaID);
            return View(osaamiset);
        }

        // GET: Osaamisen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osaamiset osaamiset = db.Osaamiset.Find(id);
            if (osaamiset == null)
            {
                return HttpNotFound();
            }
            return View(osaamiset);
        }

        // POST: Osaamisen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Osaamiset osaamiset = db.Osaamiset.Find(id);
            db.Osaamiset.Remove(osaamiset);
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

        public ActionResult OsaamisListaus()
        {
            List<OpettajaOsaaminen> model = new List<OpettajaOsaaminen>();
            OpeOsaamisKantaEntities entities = new OpeOsaamisKantaEntities();
            try
            {
                List<Osaamiset> osaamiset = entities.Osaamiset.ToList();
                foreach (Osaamiset osaaminen in osaamiset)
                {
                    OpettajaOsaaminen view = new OpettajaOsaaminen();
                    view.OsaamisID = osaaminen.OsaamisID;
                    view.Osaaminen = osaaminen.Osaaminen;
                    view.OpettamisenHalukkuus = osaaminen.OpettamisenHalukkuus;
                    view.Kuvaus = osaaminen.Kuvaus;
                    view.OpettajaID = osaaminen.Opettaja.OpettajaID;
                    view.Etunimi = osaaminen.Opettaja.Etunimi;
                    view.Sukunimi = osaaminen.Opettaja.Sukunimi;
                    view.Sähköposti = osaaminen.Opettaja.Sähköposti;
                    view.Henkilönumero = osaaminen.Opettaja.Henkilönumero;
                    view.Yksikkö = osaaminen.Opettaja.Yksikkö;
                    view.Toimenkuva = osaaminen.Opettaja.Toimenkuva;
                    view.Nimi = osaaminen.Opettaja.Etunimi + " " + osaaminen.Opettaja.Sukunimi;
                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);
        }
    }
}
