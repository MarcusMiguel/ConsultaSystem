using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConsultaSystem.Data;
using ConsultaSystem.Entities;

namespace ConsultaSystem.Controllers
{
    public class ConsultasController : Controller
    {
        private ConsultaSystemContext db = new ConsultaSystemContext();

        // GET: Consultas
        public ActionResult Index()
        {
            return View(db.Consultas.ToList());
        }

        // GET: Consultas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = db.Consultas.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // GET: Consultas/Create
        public ActionResult Create()
        {
            var pacientes = db.Pacientes.ToList();
            if (pacientes.Count() > 0)
            {
                ViewData["IDPaciente"] = new SelectList(pacientes, "ID", "Nome");
            }
            
            var tipoDeExames = db.TiposDeExames.ToList();
            ViewData["IDTipoDeExame"] = new SelectList(tipoDeExames, "ID", "Nome");

            ViewData["IDExame"] = new SelectList(Enumerable.Empty<SelectListItem>());

            return View();
        }

        // POST: Consultas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, Horario, IDPaciente, IDTipoDeExame, IDExame")] Consulta consulta)
        {
            ModelState.Remove("Protocolo");

            if (ModelState.IsValid && (consulta.IDTipoDeExame != 0))
            {
                if (consulta.Horario > DateTime.Now)
                {
                    var conflict = db.Consultas.ToList().Where(o => o.Horario == consulta.Horario);
                    if (conflict.Count() == 0)
                    {
                        consulta.Protocolo = DateTime.Now.Ticks.ToString();
                        db.Consultas.Add(consulta);
                        db.SaveChanges();
                        TempData["Message"] = "Consulta criada com sucesso!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "O horário nao está disponivel.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Escolha uma data no futuro.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Alguns campos são inválidos.");
            }

            var pacientes = db.Pacientes.ToList();
            if (pacientes.Count() > 0)
            {
                ViewData["IDPaciente"] = new SelectList(pacientes, "ID", "Nome");
            }

            var tipoDeExames = db.TiposDeExames.ToList();
            ViewData["IDTipoDeExame"] = new SelectList(tipoDeExames, "ID", "Nome");

            ViewData["IDExame"] = new SelectList(Enumerable.Empty<SelectListItem>());

            return View(consulta);
        }

        // GET: Consultas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = db.Consultas.Find(id);
                       
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, Horario, IDPaciente, IDTipoDeExame, IDExame, Protocolo")] Consulta consulta)
        {
            ModelState.Remove("Horario");
            if (ModelState.IsValid)
            {
                if (consulta.Horario > DateTime.Now)
                {
                    var conflict = db.Consultas.ToList().Where(o => (o.Horario == consulta.Horario) && (o.ID != consulta.ID));
                    if (conflict.Count() == 0)
                    {
                        Consulta consultaAlterada = db.Consultas.Find(consulta.ID);
                        consultaAlterada.Protocolo = DateTime.Now.Ticks.ToString();
                        consultaAlterada.Horario = consulta.Horario;
                        db.SaveChanges();
                        TempData["Message"] = "Consulta editada com sucesso!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "O horário nao está disponivel.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Escolha uma data no futuro.");
                }
            }
            consulta = db.Consultas.Find(consulta.ID);
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = db.Consultas.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consulta consulta = db.Consultas.Find(id);
            db.Consultas.Remove(consulta);
            db.SaveChanges();
            TempData["Message"] = "Consulta deletada com sucesso!";
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
