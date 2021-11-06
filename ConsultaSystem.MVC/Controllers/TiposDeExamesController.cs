using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Services;
using ConsultaSystem.MVC.ViewModels;

namespace ConsultaSystem.MVC.Controllers
{
    public class TiposDeExamesController : Controller
    {
        private IMapper _tiposDeExamesDomainToViewModel = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TipoDeExame, TipoDeExameViewModel>()));
        private IMapper _tiposDeExamesViewModelToDomain = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TipoDeExameViewModel, TipoDeExame>()));

        private ITipoDeExameService _tipoDeExameService;
        public TiposDeExamesController(ITipoDeExameService tipoDeExameService )
        {
            _tipoDeExameService = tipoDeExameService;
        }
        public ActionResult Index()
        {
            IEnumerable<TipoDeExameViewModel> tiposDeExames = _tiposDeExamesDomainToViewModel.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(_tipoDeExameService.GetAll());
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
                _tipoDeExameService.Add(newTipoDeExames);
                TempData["Message"] = "Tipo de exame criado com sucesso!";
                return View("Create");
            }

            return View(tipoDeExame);
        }

        public ActionResult Edit(int id)
        {
            TipoDeExame tipoDeExame = _tipoDeExameService.GetById(id);
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
                _tipoDeExameService.Update(newTipoDeExame);
                TempData["Message"] = "Tipo de exame editado com sucesso!";
                return View("Edit");
            }
            return View(tipoDeExame);
        }

        public ActionResult Delete(int id)
        {
            TipoDeExame tipoDeExame = _tipoDeExameService.GetById(id);
            _tipoDeExameService.Remove(tipoDeExame);
            return RedirectToAction("Index");
        }

     
    }
}


