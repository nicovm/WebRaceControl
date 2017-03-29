using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RaceControl.Models;

namespace RaceControl.Controllers
{
    public class CarreraController : Controller
    {
        private EntidadesRaceControl db = new EntidadesRaceControl();

        // GET: Carrera
        public ActionResult Index()
        {
            var carrera = db.Carrera.Include(c => c.Torneo);
            return View(carrera.ToList());
        }

        // GET: Carrera/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrera carrera = db.Carrera.Find(id);
            if (carrera == null)
            {
                return HttpNotFound();
            }
            return View(carrera);
        }

        // GET: Carrera/Create
        public ActionResult Create(int idTorneo)
        {
           ViewBag.listAutodromo = db.Autodromo.ToList();



            //ViewBag.idAutodromo = new SelectList(db.Autodromo, "idAutodromo", "nombre");

            ViewData["ListAutodromo"] = new SelectList(db.Autodromo, "idAutodromo", "nombre");

            ViewBag.Torneo = db.Torneo.Find(idTorneo);

            return View("CreateEdit");
        }

        // POST: Carrera/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCarrera,nombre,fecha,idTorneo,idAutodromo")] Carrera carrera)
        {
            if (ModelState.IsValid)
            {
                db.Carrera.Add(carrera);
                db.SaveChanges();
                return RedirectToAction("GetOne","Torneo", new { id = carrera.idTorneo});
            }

            ViewBag.idTorneo = new SelectList(db.Torneo, "idTorneo", "nombre", carrera.idTorneo);
            return RedirectToAction("GetOne", "Torneo", new { id = carrera.idTorneo });
        }

        // GET: Carrera/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrera carrera = db.Carrera.Find(id);
            if (carrera == null)
            {
                return HttpNotFound();
            }
            //ViewBag.idAutodromo = new SelectList(db.Autodromo, "idAutodromo", "nombre");
            ViewData["ListAutodromo"] = new SelectList(db.Autodromo, "idAutodromo", "nombre");
            ViewBag.torneo = db.Torneo.Find(carrera.idTorneo);
            return View("CreateEdit",carrera);
        }

        // POST: Carrera/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCarrera,nombre,fecha,idTorneo")] Carrera carrera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carrera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetOne", "Torneo", new { id = carrera.idTorneo });
            }
            ViewBag.idTorneo = new SelectList(db.Torneo, "idTorneo", "nombre", carrera.idTorneo);
            return View(carrera);
        }

        // GET: Carrera/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrera carrera = db.Carrera.Find(id);
            if (carrera == null)
            {
                return HttpNotFound();
            }
            return View(carrera);
        }

        // POST: Carrera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Carrera carrera = db.Carrera.Find(id);
            db.Carrera.Remove(carrera);
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
