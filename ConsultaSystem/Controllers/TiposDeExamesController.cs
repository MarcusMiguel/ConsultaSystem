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
    public class TiposDeExamesController : Controller
    {
        private ConsultaSystemContext db = new ConsultaSystemContext();

        // GET: TiposDeExames
        public ActionResult Index()
        {
            return View(db.TiposDeExames.ToList());
        }

        // GET: TiposDeExames/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: TiposDeExames/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Descricao")] TipoDeExame tipoDeExame)
        {
            if (ModelState.IsValid)
            {
                db.TiposDeExames.Add(tipoDeExame);
                db.SaveChanges();
                TempData["Message"] = "Tipo de exame criado com sucesso!";
                return View("Create");
            }

            return View(tipoDeExame);
        }

        // GET: TiposDeExames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDeExame tipoDeExame = db.TiposDeExames.Find(id);
            if (tipoDeExame == null)
            {
                return HttpNotFound();
            }
            return View(tipoDeExame);
        }

        // POST: TiposDeExames/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Descricao")] TipoDeExame tipoDeExame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDeExame).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Tipo de exame editado com sucesso!";
                return View("Edit");
            }
            TempData["InvalidModelState"] = "ModelState inválido";
            return View(tipoDeExame);
        }

        // GET: TipoDeExames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDeExame tipoDeExame = db.TiposDeExames.Find(id);
            if (tipoDeExame == null)
            {
                return HttpNotFound();
            }
            return View(tipoDeExame);
        }

        // POST: TiposDeExames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoDeExame tipoDeExame = db.TiposDeExames.Find(id);
            db.TiposDeExames.Remove(tipoDeExame);
            db.SaveChanges();
            TempData["Message"] = "Tipo de Exame deletado com sucesso!";
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
