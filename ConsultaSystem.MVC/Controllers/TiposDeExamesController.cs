using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using ConsultaSystem.Application.UseCases;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.MVC.ViewModels;
using MediatR;

namespace ConsultaSystem.MVC.Controllers
{
    public class TiposDeExamesController : Controller
    {

        private IMediator _mediator;
        private IMapper _mapper;
        public TiposDeExamesController(IMediator mediator,
                                    IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var tiposDeExames = _mapper.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(_mediator.Send(new GetAllTiposDeExames()).Result);
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
                TipoDeExame newTipoDeExames = _mapper.Map<TipoDeExame>(tipoDeExame);
                _mediator.Send(new AddTipoDeExame(newTipoDeExames));
                TempData["Message"] = "Tipo de exame criado com sucesso!";
                return View("Create");
            }

            return View(tipoDeExame);
        }

        public ActionResult Edit(int id)
        {
            TipoDeExame tipoDeExame = _mediator.Send(new GetTipoDeExameById(id)).Result;
            if (tipoDeExame == null)
            {
                return HttpNotFound();
            }
            TipoDeExameViewModel newTipoDeExame = _mapper.Map<TipoDeExameViewModel>(tipoDeExame);
            return View(newTipoDeExame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, Nome, Descricao")] TipoDeExameViewModel tipoDeExame)
        {
            if (ModelState.IsValid)
            {
                TipoDeExame newTipoDeExame = _mapper.Map<TipoDeExame>(tipoDeExame);
                _mediator.Send(new UpdateTipoDeExame(newTipoDeExame));
                TempData["Message"] = "Tipo de exame editado com sucesso!";
                return View("Edit");
            }
            return View(tipoDeExame);
        }

        public ActionResult Delete(int id)
        {
            _mediator.Send(new RemoveTipoDeExame(id));
            return RedirectToAction("Index");
        }

     
    }
}


