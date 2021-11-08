using System.Data;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using System.Collections.Generic;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.MVC.ViewModels;
using MediatR;
using ConsultaSystem.Application.UseCases;

namespace ConsultaSystem.MVC.Controllers
{
    public class ExamesController : Controller
    {
        private IMediator _mediator;
        private IMapper _mapper;
        public ExamesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            IEnumerable<ExameViewModel> exames = _mapper.Map<IEnumerable<Exame>, IEnumerable<ExameViewModel>>(_mediator.Send(new GetAllExames()).Result);
            return View(exames);
        }

        public JsonResult GetByTipo(int TipoId)
        {
            var exames = _mediator.Send(new GetAllExames()).Result.Where(o => o.IDTipoDeExame == TipoId);
            IEnumerable<ExameViewModel> newExames = _mapper.Map<IEnumerable<Exame>, IEnumerable<ExameViewModel>>(exames);

            return Json(newExames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            IEnumerable<TipoDeExameViewModel> tiposDeExames = _mapper.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(_mediator.Send(new GetAllTiposDeExames()).Result);
            ViewData["IDTipoDeExame"] = new SelectList(tiposDeExames, "ID", "Nome");

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, Nome, Observacoes, IDTipoDeExame")] ExameViewModel exame)
        {
            IEnumerable<TipoDeExameViewModel> newTiposDeExames = _mapper.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(_mediator.Send(new GetAllTiposDeExames()).Result);
            ViewData["IDTipoDeExame"] = new SelectList(newTiposDeExames, "ID", "Nome", exame.IDTipoDeExame);

            if (ModelState.IsValid)
            {
                Exame newExame = _mapper.Map<Exame>(exame);
                _mediator.Send(new AddExame(newExame));
                TempData["Message"] = "Exame criado com sucesso!";
                return View("Create");
            }
            return View(exame);
        }

        public ActionResult Edit(int id)
        {
            Exame exame = _mediator.Send(new GetExameById(id)).Result;
            if (exame == null)
            {
                return HttpNotFound();
            }
            IEnumerable<TipoDeExameViewModel> newTiposDeExames = _mapper.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(_mediator.Send(new GetAllTiposDeExames()).Result);
            ViewData["IDTipoDeExame"] = new SelectList(newTiposDeExames, "ID", "Nome", exame.IDTipoDeExame);
            ExameViewModel newExame = _mapper.Map<ExameViewModel>(exame);
            return PartialView(newExame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, Nome, Observacoes, IDTipoDeExame")] ExameViewModel exame)
        {
            IEnumerable<TipoDeExameViewModel> newTiposDeExames = _mapper.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(_mediator.Send(new GetAllTiposDeExames()).Result);
            ViewData["IDTipoDeExame"] = new SelectList(newTiposDeExames, "ID", "Nome", exame.IDTipoDeExame);

            if (ModelState.IsValid)
            {
                Exame newExame = _mapper.Map<Exame>(exame);
                _mediator.Send(new UpdateExame(newExame));
                TempData["Message"] = "Exame editado com sucesso!";
                return View("Edit");
            }

            return View(exame);
        }
        
        public ActionResult Delete(int id)
        {
            _mediator.Send(new RemoveExame(id));
            return RedirectToAction("Index");
        }

    }
}


