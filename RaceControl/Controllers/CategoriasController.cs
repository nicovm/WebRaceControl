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
            ViewBag.torneo = db.Torneo.Find(idTorneo);
            return PartialView("CreateEdit");
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
                return RedirectToAction("GetOne", "Torneo", new { id = categoria.idTorneo }); 
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
            ViewBag.idTorneo = new SelectList(db.Torneo, "idTorneo", "nombre");
            ViewBag.torneo = db.Torneo.Find(categoria.idTorneo);
            return View("CreateEdit",categoria);
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
                return RedirectToAction("GetOne", "Torneo", new { id = categoria.idTorneo });
            }
           // ViewBag.idTorneo = new SelectList(db.Torneo, "idTorneo", "nombre", categoria.idTorneo);
            return RedirectToAction("GetOne", "Torneo", new { id = categoria.idTorneo });
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

        public ActionResult AgregarPiloto(int idCategoria )
        {
            ViewBag.Categoria = db.Categoria.Find(idCategoria);
            ViewBag.Pilotos = db.Piloto.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AgregarPiloto(int idCategoria , int dni)
        {
            //Verifico que el piloto no este agreagado ya a la categoria
            Categoria_Piloto CatPiloto;

            CatPiloto = db.Categoria_Piloto.Where(cp => cp.dniPiloto == dni && cp.idCategoria == idCategoria).FirstOrDefault();
            
            if (CatPiloto == null)
            {
                //El piloto no esta agregado
                CatPiloto = new Categoria_Piloto();
                CatPiloto.idCategoria = idCategoria;
                CatPiloto.dniPiloto = dni;

                db.Categoria_Piloto.Add(CatPiloto);
                Categoria categoria = db.Categoria.Find(idCategoria);

                db.SaveChanges();
                return RedirectToAction("GetOne", "Torneo", new { id = categoria.Torneo.idTorneo });
            }
            else
            {
                //El piloto ya esta agregado 
                Piloto piloto = db.Piloto.Find(dni);
                Categoria categoria = db.Categoria.Find(idCategoria);
                string msj = string.Format("El Piloto {0} {1} ya se encuentra agregado a la categoría {2} ", piloto.nombre, piloto.apellido, categoria.nombre);
                TempData["alert"] = Constante.alertDanger(msj);
              
                return RedirectToAction("AgregarPiloto", new { idCategoria = categoria.idTorneo});
            }

          
        }

        public ActionResult Buscar(string buscar, int idCategoria)
        {
            List<Piloto> listPilotos;
            ViewBag.Categoria = db.Categoria.Find(idCategoria);
           
            if (Constante.IsNumeric(buscar)) // si es numerico es porque ingreso el dni del piloto
            {
                int dni = int.Parse(buscar);
                listPilotos = db.Piloto.Where(p => p.nombre.Contains(buscar) || 
                p.apellido.Contains(buscar) || p.dni == dni).ToList();
            }
            else if (string.IsNullOrEmpty(buscar)) // no infreso ninguno datos devuelvo todolos los pilotos
            {
                listPilotos = db.Piloto.ToList();
            }
            else // ingreso un texto puede ser el nombre o apellido
            {
                listPilotos = db.Piloto.Where(p => p.nombre.Contains(buscar) ||
               p.apellido.Contains(buscar) ).ToList();
            }
            ViewBag.Pilotos = listPilotos;
            return View("AgregarPiloto");
          
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
