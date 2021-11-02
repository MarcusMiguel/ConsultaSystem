using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using ConsultaSystem.Data;
using ConsultaSystem.Entities;
using ConsultaSystem.ViewModels;

namespace ConsultaSystem.Controllers
{
    public class TiposDeExamesController : Controller
    {
        private ConsultaSystemContext db = new ConsultaSystemContext();

        private IMapper _tiposDeExamesDomainToViewModel;

        private IMapper _tiposDeExamesViewModelToDomain;

        public TiposDeExamesController()
        {
            _tiposDeExamesDomainToViewModel = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TipoDeExame, TipoDeExameViewModel>()));
            _tiposDeExamesViewModelToDomain = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TipoDeExameViewModel, TipoDeExame>()));
        }
        public ActionResult Index()
        {
            IEnumerable<TipoDeExameViewModel> tiposDeExames = _tiposDeExamesDomainToViewModel.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(db.TiposDeExames.ToList());
            return View(tiposDeExames);
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, Nome, Descricao")] TipoDeExameViewModel tipoDeExame)
        {
            if (ModelState.IsValid)
            {
                TipoDeExame newTipoDeExames = _tiposDeExamesViewModelToDomain.Map<TipoDeExame>(tipoDeExame);
                db.TiposDeExames.Add(newTipoDeExames);
                db.SaveChanges();
                TempData["Message"] = "Tipo de exame criado com sucesso!";
                return View("Create");
            }

            return View(tipoDeExame);
        }

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
            TipoDeExameViewModel newTipoDeExame = _tiposDeExamesDomainToViewModel.Map<TipoDeExameViewModel>(tipoDeExame);
            return View(newTipoDeExame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, Nome, Descricao")] TipoDeExameViewModel tipoDeExame)
        {
            if (ModelState.IsValid)
            {
                TipoDeExame newTipoDeExame = _tiposDeExamesViewModelToDomain.Map<TipoDeExame>(tipoDeExame);
                db.Entry(newTipoDeExame).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Tipo de exame editado com sucesso!";
                return View("Edit");
            }
            TempData["InvalidModelState"] = "ModelState inválido";
            return View(tipoDeExame);
        }

        public ActionResult Delete(int? id)
        {
            TipoDeExame tipoDeExame = db.TiposDeExames.Find(id);
            db.TiposDeExames.Remove(tipoDeExame);
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
