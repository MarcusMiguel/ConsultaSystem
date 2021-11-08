using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using ConsultaSystem.Application.UseCases;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.MVC.ViewModels;
using MediatR;

namespace ConsultaSystem.MVC.Controllers
{
    public class PacientesController : Controller
    {
        private IMediator _mediator;
        private IMapper _mapper;
        public PacientesController( IMediator mediator, 
                                    IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            var result = _mediator.Send(new GetAllPacientes());
            IEnumerable<PacienteViewModel> pacientes = _mapper.Map<IEnumerable<Paciente>, IEnumerable<PacienteViewModel>>(result.Result);
           
            return View(pacientes);
        }
        public ActionResult RedirectToCreate()
        {
            // Will open the create modal
            TempData["ShowModal"] = "ShowModal"; 
            
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, Nome, CPF, DataNascimento, Sexo, Telefone, Email")] PacienteViewModel paciente)
        {
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(new ExistsCPF(paciente.CPF)).Result;
                if (!result)
                {
                    Paciente newPaciente = _mapper.Map<Paciente>(paciente);
                    _mediator.Send(new AddPaciente(newPaciente));
                    TempData["Message"] = "Paciente criado com sucesso!";
                    return View("Create");
                }
                ModelState.AddModelError(string.Empty, "O CPF já está em uso.");
            }
            return View(paciente);
        }

        public ActionResult Edit(int id)
        {
            Paciente paciente = _mediator.Send(new GetPacienteById(id)).Result;
            if (paciente == null)
            {
                return HttpNotFound();
            }
            PacienteViewModel newPaciente = _mapper.Map<PacienteViewModel>(paciente);
            return View(newPaciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,CPF,DataNascimento,Sexo,Telefone,Email")] PacienteViewModel paciente)
        {
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(new ExistsCPF(paciente.CPF)).Result;
                if (!result)
                {
                    Paciente newPaciente = _mapper.Map<Paciente>(paciente);
                    _mediator.Send(new UpdatePaciente(newPaciente));
                    TempData["Message"] = "Paciente criado com sucesso!";
                    return View("Create");
                }
                ModelState.AddModelError(string.Empty, "O CPF já está em uso.");
            }
            return View(paciente);
        }

        public ActionResult Delete(int id)
        {
            _mediator.Send(new RemovePaciente(id));
            return RedirectToAction("Index");
        }

    }
}

