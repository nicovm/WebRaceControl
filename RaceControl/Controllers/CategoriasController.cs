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
    public class CategoriasController : Controller
    {
        private EntidadesRaceControl db = new EntidadesRaceControl();

        // GET: Categorias
        public ActionResult Index()
        {
            var categoria = db.Categoria.Include(c => c.Torneo);
            return View(categoria.ToList());
        }

        // GET: Categorias/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create(int idTorneo)
        {
            ViewBag.idTorneo = new SelectList(db.Torneo, "idTorneo", "nombre");
            ViewBag.torneo = db.Torneo.Find(3);
            return PartialView("_Categoria");
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCategoria,nombre,descripcion,idTorneo")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Categoria.Add(categoria);
                db.SaveChanges();
                return RedirectToAction("GetOne", "Torneo", new { id = categoria.idTorneo }); ;
            }

            ViewBag.idTorneo = new SelectList(db.Torneo, "idTorneo", "nombre", categoria.idTorneo);
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTorneo = new SelectList(db.Torneo, "idTorneo", "nombre", categoria.idTorneo);
            return RedirectToAction("GetOne","Torneo");
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCategoria,nombre,descripcion,idTorneo")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTorneo = new SelectList(db.Torneo, "idTorneo", "nombre", categoria.idTorneo);
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Categoria categoria = db.Categoria.Find(id);
            db.Categoria.Remove(categoria);
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
