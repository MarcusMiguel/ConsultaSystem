using System.Data;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using System.Collections.Generic;
using ConsultaSystem.Domain.Interfaces.Services;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.MVC.ViewModels;

namespace ConsultaSystem.MVC.Controllers
{
    public class ExamesController : Controller
    {
        private IMapper _exameViewModelToDomain = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<ExameViewModel, Exame>()));
        private IMapper _exameDomainToViewModel = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Exame, ExameViewModel>()));
        private IMapper _tipoDeExameDomainToViewModel = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TipoDeExame, TipoDeExameViewModel>()));
        private IExameService _exameService;
        private ITipoDeExameService _tipoDeExameService;

        public ExamesController(IExameService exameService,
                                ITipoDeExameService tipoDeExameService)
        {
           _exameService = exameService;
           _tipoDeExameService = tipoDeExameService;
        }

        public ActionResult Index()
        {
            IEnumerable<ExameViewModel> exames = _exameDomainToViewModel.Map<IEnumerable<Exame>, IEnumerable<ExameViewModel>>(_exameService.GetAll());
            return View(exames);
        }

        public JsonResult GetByTipo(int TipoId)
        {
            var exames = _exameService.GetAll().Where(o => o.IDTipoDeExame == TipoId);
            IEnumerable<ExameViewModel> newExames = _exameDomainToViewModel.Map<IEnumerable<Exame>, IEnumerable<ExameViewModel>>(exames);

            return Json(newExames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            IEnumerable<TipoDeExameViewModel> tiposDeExames = _tipoDeExameDomainToViewModel.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(_tipoDeExameService.GetAll());
            ViewData["IDTipoDeExame"] = new SelectList(tiposDeExames, "ID", "Nome");

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, Nome, Observacoes, IDTipoDeExame")] ExameViewModel exame)
        {
            IEnumerable<TipoDeExameViewModel> newTiposDeExames = _tipoDeExameDomainToViewModel.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(_tipoDeExameService.GetAll());
            ViewData["IDTipoDeExame"] = new SelectList(newTiposDeExames, "ID", "Nome", exame.IDTipoDeExame);

            if (ModelState.IsValid)
            {
                Exame newExame = _exameViewModelToDomain.Map<Exame>(exame);
                _exameService.Add(newExame);
                TempData["Message"] = "Exame criado com sucesso!";
                return View("Create");
            }
            return View(exame);
        }

        public ActionResult Edit(int id)
        {
            Exame exame = _exameService.GetById(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            IEnumerable<TipoDeExameViewModel> newTiposDeExames = _tipoDeExameDomainToViewModel.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(_tipoDeExameService.GetAll());
            ViewData["IDTipoDeExame"] = new SelectList(newTiposDeExames, "ID", "Nome", exame.IDTipoDeExame);
            ExameViewModel newExame = _exameDomainToViewModel.Map<ExameViewModel>(exame);
            return PartialView(newExame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, Nome, Observacoes, IDTipoDeExame")] ExameViewModel exame)
        {
            IEnumerable<TipoDeExameViewModel> newTiposDeExames = _tipoDeExameDomainToViewModel.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(_tipoDeExameService.GetAll());
            ViewData["IDTipoDeExame"] = new SelectList(newTiposDeExames, "ID", "Nome", exame.IDTipoDeExame);

            if (ModelState.IsValid)
            {
                Exame newExame = _exameViewModelToDomain.Map<Exame>(exame);
                _exameService.Update(newExame);
                TempData["Message"] = "Exame editado com sucesso!";
                return View("Edit");
            }

            return View(exame);
        }

        public ActionResult Delete(int id)
        {
            Exame exame = _exameService.GetById(id);
            _exameService.Remove(exame);
            return RedirectToAction("Index");
        }

    }
}


