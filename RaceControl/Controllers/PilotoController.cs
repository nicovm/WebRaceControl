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
    public class PilotoController : Controller
    {
        const string quote = "\"";
        private EntidadesRaceControl db = new EntidadesRaceControl();

        // GET: Piloto
       
        public ActionResult Index()
        {
           
            return View(db.Piloto.ToList());
        }

       
        public ActionResult Index1(string alert)
        {
            ViewBag.alert = alert;
            return View("index",db.Piloto.ToList());
        }


        // GET: Piloto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piloto piloto = db.Piloto.Find(id);
            if (piloto == null)
            {
                return HttpNotFound();
            }
            return View(piloto);
        }

        // GET: Piloto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Piloto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre,apellido,dni,email")] Piloto piloto)
        {
            if (ModelState.IsValid)
            {
                db.Piloto.Add(piloto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView(piloto);
        }

        // GET: Piloto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piloto piloto = db.Piloto.Find(id);
            if (piloto == null)
            {
                return HttpNotFound();
            }
            return View(piloto);
        }

        // POST: Piloto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nombre,apellido,dni,email")] Piloto piloto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(piloto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(piloto);
        }

        // GET: Piloto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piloto piloto = db.Piloto.Find(id);
            if (piloto == null)
            {
                return HttpNotFound();
            }
            return View(piloto);
        }

        // POST: Piloto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Piloto piloto = db.Piloto.Find(id);
            string msj = string.Format("El piloto  {0} {1} con DNI: {2} a sido eliminado ", piloto.nombre, piloto.apellido, piloto.dni);
            db.Piloto.Remove(piloto);
            db.SaveChanges();

            string alert = "<div class=" + quote + "alert alert-danger" + quote + "><a class=" + quote +
                  "close" + quote + " data-dismiss=" + quote + "alert" + quote + ">×</a><span> "+ msj +" </span></div>";

            TempData["alert"] = alert;

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
