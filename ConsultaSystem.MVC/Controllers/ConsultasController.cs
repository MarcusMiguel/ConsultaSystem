using AutoMapper;
using ConsultaSystem.Application.UseCases;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.MVC.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ConsultaSystem.MVC.Controllers
{
    public class ConsultasController : Controller
    {
        private IMediator _mediator;
        private IMapper _mapper;
        public ConsultasController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            var consultas = _mediator.Send(new GetAllConsultas()).Result;
            IEnumerable<ConsultaViewModel> newConsultas = _mapper.Map<IEnumerable<Consulta> , IEnumerable<ConsultaViewModel>>(consultas);
            return View(newConsultas);
        }
        
        public ActionResult Create()
        {
            var pacientes = _mediator.Send(new GetAllPacientes()).Result;
            var newpacientes = _mapper.Map<IEnumerable<Paciente>, IEnumerable<PacienteViewModel>>(pacientes);
            ViewData["IDPaciente"] = new SelectList(newpacientes, "ID", "Nome");

            var tiposDeExames = _mediator.Send(new GetAllTiposDeExames()).Result;
            var newTiposDeExames = _mapper.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(tiposDeExames);
            ViewData["IDTipoDeExame"] = new SelectList(newTiposDeExames, "ID", "Nome", 0);

            ViewData["IDExame"] = new SelectList(Enumerable.Empty<SelectListItem>());

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, Horario, IDPaciente, IDTipoDeExame, IDExame")] ConsultaViewModel consulta)
        {
            if (ModelState.IsValid && (consulta.IDTipoDeExame != 0) && (consulta.IDExame != 0))
            {
                if (consulta.Horario > DateTime.Now)
                {
                    var result = _mediator.Send(new HorarioIsAvailable(consulta.Horario)).Result;
                    if (result)
                    {
                        Consulta newConsulta = _mapper.Map<Consulta>(consulta);
                        newConsulta.Protocolo = DateTime.Now.Ticks.ToString();
                        _mediator.Send(new AddConsulta(newConsulta));
                        TempData["Message"] = "Consulta criada com sucesso!";

                        return View("Create");
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

            var pacientes = _mediator.Send(new GetAllPacientes()).Result;
            var newpacientes = _mapper.Map<IEnumerable<Paciente>, IEnumerable<PacienteViewModel>>(pacientes);
            ViewData["IDPaciente"] = new SelectList(newpacientes, "ID", "Nome");

            var tiposDeExames = _mediator.Send(new GetAllTiposDeExames()).Result;
            var newTiposDeExames = _mapper.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(tiposDeExames);
            ViewData["IDTipoDeExame"] = new SelectList(newTiposDeExames, "ID", "Nome", 0);

            ViewData["IDExame"] = new SelectList(Enumerable.Empty<SelectListItem>());

            return View(consulta);
        }

        public ActionResult Edit(int id)
        {
            Consulta consulta = _mediator.Send(new GetConsultaById(id)).Result;
            if (consulta == null)
            {
                return HttpNotFound();
            }
            ConsultaViewModel newConsulta = _mapper.Map<ConsultaViewModel>(consulta);
            return View(newConsulta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, Horario, IDPaciente, IDTipoDeExame, IDExame, Protocolo")] ConsultaViewModel consulta)
        {
            if (ModelState.IsValid)
            {
                if (consulta.Horario > DateTime.Now)
                {
                    var result = _mediator.Send(new HorarioIsAvailable(consulta.Horario)).Result;
                    if (!result) 
                    {
                        Consulta consultaAlterada = _mediator.Send(new GetConsultaById(consulta.ID)).Result;
                        consultaAlterada.Protocolo = DateTime.Now.Ticks.ToString();
                        consultaAlterada.Horario = consulta.Horario;
                        _mediator.Send(new UpdateConsulta(consultaAlterada));
                        TempData["Message"] = "Consulta editada com sucesso!";
                        return View("Edit");
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
            Consulta consulta2 = _mediator.Send(new GetConsultaById(consulta.ID)).Result;
            ConsultaViewModel newconsulta = _mapper.Map<ConsultaViewModel>(consulta2);
            return View(newconsulta);
        }

        public ActionResult Delete(int id)
        {
            _mediator.Send(new RemoveConsulta(id));
            return RedirectToAction("Index");
        }
    }
}
