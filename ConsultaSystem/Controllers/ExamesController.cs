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
    public class ExamesController : Controller
    {
        private ConsultaSystemContext db = new ConsultaSystemContext();

        // GET: Exames
        public ActionResult Index()
        {
            return View(db.Exames.ToList());
        }

        // GET: Exames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exame exame = db.Exames.Find(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        public JsonResult GetByTipo(int TipoId)
        {
            var exames = db.Exames.ToList().Where(o => o.IDTipoDeExame == TipoId);
            Console.WriteLine(exames);
            return Json(exames, JsonRequestBehavior.AllowGet);
        }

        // GET: Exames/Create
        public ActionResult Create()
        {
            var tipoDeExames = db.TiposDeExames.ToList();
            ViewData["IDTipoDeExame"] = new SelectList(tipoDeExames, "ID", "Nome");

            return View();
        }

        // POST: Exames/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Observacoes, IDTipoDeExame")] Exame exame)
        {

            var tipoDeExames = db.TiposDeExames.ToList();
            ViewData["IDTipoDeExame"] = new SelectList(tipoDeExames, "ID", "Nome", exame.IDTipoDeExame);

            if (ModelState.IsValid)
            {
                db.Exames.Add(exame);
                db.SaveChanges();
                TempData["Message"] = "Exame criado com sucesso!";
                return RedirectToAction("Index");
            }

            return View(exame);
        }

        // GET: Exames/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Exame exame = db.Exames.Find(id);

            var tipoDeExames = db.TiposDeExames.ToList();
            ViewData["IDTipoDeExame"] = new SelectList(tipoDeExames, "ID", "Nome", exame.IDTipoDeExame);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        // POST: Exames/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Observacoes,IDTipoDeExame")] Exame exame)
        {


            var tipoDeExames = db.TiposDeExames.ToList();
            ViewData["IDTipoDeExame"] = new SelectList(tipoDeExames, "ID", "Nome", exame.IDTipoDeExame);

            if (ModelState.IsValid)
            {

                db.Entry(exame).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Exame editado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(exame);
        }

        // GET: Exames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exame exame = db.Exames.Find(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        // POST: Exames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exame exame = db.Exames.Find(id);
            db.Exames.Remove(exame);
            db.SaveChanges();
            TempData["Message"] = "Exame deletado com sucesso!";
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
