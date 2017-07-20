using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RaceControl.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;


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
                return RedirectToAction("GetOne", "Torneo", new { id = carrera.idTorneo });
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
            return View("CreateEdit", carrera);
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


        public ActionResult Tecnica(int idCarrera, int? idCategoria, int? dniPiloto, int? tabDefault)
        {
            TecnicaTmp tecnicaTmp = new TecnicaTmp();

            Carrera carrera = db.Carrera.Find(idCarrera);

            tecnicaTmp.carrera = carrera;

            if (idCategoria != null) // selecciono una categoria
            {
                tecnicaTmp.categoria = db.Categoria.Find(idCategoria);

            }
            else
            {
                //Busco las categoria del torneo
                ViewBag.Categorias = db.Categoria.Where(c => c.idTorneo == carrera.idTorneo).ToList();
                //retorno la vista con las categorias del torneo a seleccionar
                return View("SelectTecnica", tecnicaTmp);
            }


            if (dniPiloto != null) // selecciono un piloto
            {

                tecnicaTmp.piloto = db.Piloto.Find(dniPiloto);
                //direccinar a realizar la revision
            }
            else
            {
                //Buscar todos los pilotos que pertenecen a la categoria seleccionada
                ViewBag.CatPiloto = db.Categoria_Piloto.Where(cp => cp.idCategoria == tecnicaTmp.categoria.idCategoria).ToList();
                //retorno la vista con las categorias del torneo a seleccionar
                return View("SelectTecnica", tecnicaTmp);
            }


            if (tecnicaTmp.categoria != null && tecnicaTmp.piloto != null) // selecciono la categoria y el piloto
            {
                //Busco la tecnica
                Tecnica tecnica = db.Tecnica.Where(t => t.idCarrera == tecnicaTmp.carrera.idCarrera
                && t.idCategoria == tecnicaTmp.categoria.idCategoria && t.dniPiloto == tecnicaTmp.piloto.dni).FirstOrDefault();

                if (tecnica != null) tecnicaTmp.tecnica = tecnica; // ya tiene una tecnica creada
                else tecnicaTmp.tecnica = nuevaTecnica(tecnicaTmp.carrera.idCarrera,
                    tecnicaTmp.categoria.idCategoria, tecnicaTmp.piloto.dni);// nueva tecnica

            }

            if (tabDefault == null) ViewBag.tabDefault = Constante.defaultTabTecnica.TAB_REVISION;
            else ViewBag.tabDefault = tabDefault;
            return View(tecnicaTmp);
        }

        //GET
        public ActionResult Revision(int idRevision, int tabDefault)
        {
            Revision revision = db.Revision.Find(idRevision);
            ViewBag.tabDefault = tabDefault;

            return View(revision);

        }


        //GET
        public ActionResult CreateObs(int idRevision)
        {
            ViewBag.idRevision = idRevision;
            return View("CreateEditObs");
        }

        //POST
        [HttpPost]
        public ActionResult CreateObs(Observacion Observacion)
        {
            Observacion.fecha = DateTime.Now;
            Observacion.ok = false;
            string currentUserId = User.Identity.GetUserId();
            Observacion.Usuario = db.Usuario.Where(u => u.idAspNetUsers == currentUserId).FirstOrDefault();
            Observacion.fecha = DateTime.Now;

            db.Observacion.Add(Observacion);
            db.SaveChanges();
            //Crear logue de usuario

            return RedirectToAction("Revision", new { idRevision = Observacion.idRevision, tabDefault = Constante.defaultTabRevision.TAB_OBSERVACION });
        }

        //GET
        public ActionResult EditObs(int idObservacion)
        {
            Observacion obs = db.Observacion.Find(idObservacion);
            ViewBag.idRevision = obs.idRevision;
            return View("CreateEditObs", obs);
        }

        //POST
        [HttpPost]
        public ActionResult EditObs(Observacion Observacion)
        {
            Observacion editar = db.Observacion.Find(Observacion.idObservacion);
            editar.descripcion = Observacion.descripcion;
            db.SaveChanges();
            //Crear logue de usuario

            return RedirectToAction("Revision", new { idRevision = Observacion.idRevision, tabDefault = Constante.defaultTabRevision.TAB_OBSERVACION });
        }

        public ActionResult ConfirmarObs(int idObservacion, int idRevisionOK)
        {
            Observacion observacion = db.Observacion.Find(idObservacion);
            observacion.fechaOk = DateTime.Now;
            observacion.idRevisionOk = idRevisionOK;
            string currentUserId = User.Identity.GetUserId();
            Usuario usuarioOK = db.Usuario.Where(u => u.idAspNetUsers == currentUserId).FirstOrDefault();
            observacion.idUsuarioOk = usuarioOK.idUsuario;
            observacion.ok = true;

            db.SaveChanges();

            return RedirectToAction("Revision", new { idRevision = observacion.idRevision, tabDefault = Constante.defaultTabRevision.TAB_OBSERVACION });
        }

        public ActionResult CreatePrecinto(int idRevision)
        {
            ViewBag.idRevision = idRevision;
            return View("CreateEditPrecinto");

        }

        //POST
        [HttpPost]
        public ActionResult CreatePrecinto(Precinto precinto)
        {
            precinto.fecha = DateTime.Now;

            string currentUserId = User.Identity.GetUserId();
            precinto.Usuario = db.Usuario.Where(u => u.idAspNetUsers == currentUserId).FirstOrDefault();


            db.Precinto.Add(precinto);
            db.SaveChanges();
            //Crear logue de usuario

            return RedirectToAction("Revision", new { idRevision = precinto.idRevision, tabDefault = Constante.defaultTabRevision.TAB_PRECINTO });
        }

        //GET
        public ActionResult EditPrecinto(int idPrecinto)
        {
            Precinto oPrecinto = db.Precinto.Find(idPrecinto);


            return View("CreateEditPrecinto", oPrecinto);
        }
        //POST
        [HttpPost]
        public ActionResult EditPrecinto(Precinto EditPrecinto)
        {

            Precinto precinto = db.Precinto.Find(EditPrecinto.idPrecinto);

            precinto.precinto1 = EditPrecinto.precinto1;
            precinto.fecha = DateTime.Now;

            string currentUserId = User.Identity.GetUserId();
            precinto.Usuario = db.Usuario.Where(u => u.idAspNetUsers == currentUserId).FirstOrDefault();

            db.SaveChanges();

            return RedirectToAction("Revision", new { idRevision = precinto.idRevision, tabDefault = Constante.defaultTabRevision.TAB_PRECINTO });
        }

        //GET
        public ActionResult EliminarPrecinto(int idPrecinto)
        {
            Precinto precinto = db.Precinto.Find(idPrecinto);
            db.Precinto.Remove(precinto);
            db.SaveChanges();

            return RedirectToAction("Revision", new { idRevision = idPrecinto, tabDefault = Constante.defaultTabRevision.TAB_PRECINTO });
        }

        public ActionResult CreateNeumatico(int idTecnica)
        {
            ViewBag.idTecnica = idTecnica;

            return View("CreateEditNeumatico");
        }

        //POST
        [HttpPost]
        public ActionResult CreateNeumatico(Neumatico neumatico)
        {

            string currentUserId = User.Identity.GetUserId();
            neumatico.Usuario = db.Usuario.Where(u => u.idAspNetUsers == currentUserId).FirstOrDefault();
            neumatico.fecha = DateTime.Now;
            db.Neumatico.Add(neumatico);
            db.SaveChanges();

            Tecnica tecnica = db.Tecnica.Find(neumatico.idTecnica);

            // //int idCarrera, int? idCategoria, int? dniPiloto, int? tabDefault
            return RedirectToAction("Tecnica", new
            {
                idCarrera = tecnica.idCarrera,
                idCategoria = tecnica.idCategoria,
                dniPiloto = tecnica.dniPiloto,
                tabDefault = Constante.defaultTabTecnica.TAB_NEUMAITCO
            });
        }

        public Boolean ExisteNeumatico(int numNeumatico)
        {
            Neumatico neumatico = db.Neumatico.Find(numNeumatico);

            //Existe el neumatico si se encuentra un neumatico con el mismo numero
            return neumatico != null;
        }

        public ActionResult EditNeumatico(int numNeumatico)
        {
            Neumatico neumatico = db.Neumatico.Find(numNeumatico);
            ViewBag.idTecnica = neumatico.idTecnica;

            return View("CreateEditNeumatico", neumatico);
        }

        [HttpPost]
        public ActionResult EditNeumatico(Neumatico editNeumatico)
        {
            Neumatico neumatico = db.Neumatico.Find(editNeumatico.numNeumatico);
            neumatico.numNeumatico = editNeumatico.numNeumatico;
            neumatico.nuevo = editNeumatico.nuevo;
            neumatico.fecha = DateTime.Now;
            string currentUserId = User.Identity.GetUserId();
            neumatico.Usuario = db.Usuario.Where(u => u.idAspNetUsers == currentUserId).FirstOrDefault();
            db.SaveChanges();

            Tecnica tecnica = db.Tecnica.Find(neumatico.idTecnica);
            return RedirectToAction("Tecnica", new
            {
                idCarrera = tecnica.idCarrera,
                idCategoria = tecnica.idCategoria,
                dniPiloto = tecnica.dniPiloto,
                tabDefault = Constante.defaultTabTecnica.TAB_NEUMAITCO
            });
        }

        public ActionResult EliminarNeumatico(Int64 numNeumatico)
        {
            Neumatico neumatico = db.Neumatico.Find(numNeumatico);
            Tecnica tecnica = db.Tecnica.Find(neumatico.idTecnica);

            db.Neumatico.Remove(neumatico);
            db.SaveChanges();


            return RedirectToAction("Tecnica", new
            {
                idCarrera = tecnica.idCarrera,
                idCategoria = tecnica.idCategoria,
                dniPiloto = tecnica.dniPiloto,
                tabDefault = Constante.defaultTabTecnica.TAB_NEUMAITCO
            });
        }

        public ActionResult BuscarPiloto(int idCarrera, int idCategoria, string buscar)
        {
            ViewBag.idCarrera = idCarrera;
            if (Constante.IsNumeric(buscar)) // si es numerico es porque ingreso el dni del piloto
            {
                int dni = int.Parse(buscar);
                List<Categoria_Piloto> catPiloto = db.Categoria_Piloto.Where(ct => ct.idCategoria == idCategoria &&
                (ct.dniPiloto == dni || ct.Piloto.nombre.Contains(buscar) || ct.Piloto.apellido.Contains(buscar))).ToList();

                ViewBag.CatPiloto = catPiloto;
            }
            else if (string.IsNullOrEmpty(buscar)) // no ingreso ningun datos devuelvo todo los los pilotos
            {
                List<Categoria_Piloto> catPiloto = db.Categoria_Piloto.Where(ct => ct.idCategoria == idCategoria).ToList();

                ViewBag.CatPiloto = catPiloto;
            }
            else // ingreso un texto puede ser el nombre o apellido
            {
                List<Categoria_Piloto> catPiloto = db.Categoria_Piloto.Where(ct => ct.idCategoria == idCategoria &&
                (ct.Piloto.nombre.Contains(buscar) || ct.Piloto.apellido.Contains(buscar))).ToList();

                ViewBag.CatPiloto = catPiloto;
            }

            return View("ListPilotoTecnica");
        }

        private Tecnica nuevaTecnica(long idCarrera, long idCategoria, int dniPiloto)
        {
            Tecnica newTecnica = new Models.Tecnica();
            newTecnica.idCarrera = idCarrera;
            newTecnica.idCategoria = idCategoria;
            newTecnica.dniPiloto = dniPiloto;
            newTecnica.fecha = DateTime.Now;

            db.Tecnica.Add(newTecnica);

            db.SaveChanges();
            // Tambien asocio los elementos de revision con la tecnica
            asociarElemRevisionTenica(newTecnica.idTecnica);

            return newTecnica;

        }

        /// <summary>
        /// Permte asociar los elemento de revision con la idTenica 
        /// </summary>
        /// <param name="idTecnica"> tenica que se va asociar los elementos de revision</param>
        private void asociarElemRevisionTenica(long idTecnica)
        {
            List<Elem_Revision> listElem = db.Elem_Revision.ToList();

            foreach (Elem_Revision item in listElem)
            {
                Revision newTecRev = new Revision();
                newTecRev.idElemRevision = item.idElemRevision;
                newTecRev.idTecnica = idTecnica;

                db.Revision.Add(newTecRev);

            }

            db.SaveChanges();
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
