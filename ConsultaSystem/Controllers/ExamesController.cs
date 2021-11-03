using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ConsultaSystem.Data;
using ConsultaSystem.Entities;
using AutoMapper;
using ConsultaSystem.ViewModels;
using System.Collections.Generic;

namespace ConsultaSystem.Controllers
{
    public class ExamesController : Controller
    {
        private ConsultaSystemContext db = new ConsultaSystemContext();

        private IMapper _exameDomainToViewModel; 

        private IMapper _exameViewModelToDomain;
        
        private IMapper _tipoDeExameDomainToViewModel;

        public ExamesController()
        {
            _exameViewModelToDomain = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<ExameViewModel, Exame>()));
            _exameDomainToViewModel = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Exame, ExameViewModel>()));
            _tipoDeExameDomainToViewModel = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TipoDeExame, TipoDeExameViewModel>()));

        }
        public ActionResult Index()
        {
            IEnumerable<ExameViewModel> exames = _exameDomainToViewModel.Map<IEnumerable<Exame>, IEnumerable<ExameViewModel>>(db.Exames.ToList());
            return View(exames);
        }

        public JsonResult GetByTipo(int TipoId)
        {
            var exames = db.Exames.ToList().Where(o => o.IDTipoDeExame == TipoId);
            IEnumerable<ExameViewModel> newExames = _exameDomainToViewModel.Map<IEnumerable<Exame>, IEnumerable<ExameViewModel>>(exames);

            return Json(newExames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            IEnumerable<TipoDeExameViewModel> tiposDeExames = _tipoDeExameDomainToViewModel.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(db.TiposDeExames.ToList());
            ViewData["IDTipoDeExame"] = new SelectList(tiposDeExames, "ID", "Nome");

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, Nome, Observacoes, IDTipoDeExame")] ExameViewModel exame)
        {
            IEnumerable<TipoDeExameViewModel> newTiposDeExames = _tipoDeExameDomainToViewModel.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(db.TiposDeExames.ToList());
            ViewData["IDTipoDeExame"] = new SelectList(newTiposDeExames, "ID", "Nome", exame.IDTipoDeExame);

            if (ModelState.IsValid)
            {
                Exame newExame = _exameViewModelToDomain.Map<Exame>(exame);
                db.Exames.Add(newExame);
                db.SaveChanges();
                TempData["Message"] = "Exame criado com sucesso!";
                return View("Create");
            }
            return View(exame);
        }

        public ActionResult Edit(int? id)
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
            IEnumerable<TipoDeExameViewModel> newTiposDeExames = _tipoDeExameDomainToViewModel.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(db.TiposDeExames.ToList());
            ViewData["IDTipoDeExame"] = new SelectList(newTiposDeExames, "ID", "Nome", exame.IDTipoDeExame);
            ExameViewModel newExame = _exameDomainToViewModel.Map<ExameViewModel>(exame);
            return PartialView(newExame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, Nome, Observacoes, IDTipoDeExame")] ExameViewModel exame)
        {
            IEnumerable<TipoDeExameViewModel> newTiposDeExames = _tipoDeExameDomainToViewModel.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(db.TiposDeExames.ToList());
            ViewData["IDTipoDeExame"] = new SelectList(newTiposDeExames, "ID", "Nome", exame.IDTipoDeExame);

            if (ModelState.IsValid)
            {
                Exame newExame = _exameViewModelToDomain.Map<Exame>(exame);
                db.Entry(newExame).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Exame editado com sucesso!";
                return View("Edit");
            }

            return View(exame);
        }

        public ActionResult Delete(int? id)
        {
            Exame exame = db.Exames.Find(id);
            db.Exames.Remove(exame);
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
