using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Services;
using ConsultaSystem.MVC.ViewModels;

namespace ConsultaSystem.MVC.Controllers
{
    public class PacientesController : Controller
    {
        private IMapper _pacienteDomainToViewModel =  new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Paciente, PacienteViewModel>()));
        private IMapper _pacienteViewModelToDomain = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<PacienteViewModel, Paciente>()));
        private IPacienteService _pacienteService;
        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }
        public ActionResult Index()
        {
            IEnumerable<PacienteViewModel> pacientes = _pacienteDomainToViewModel.Map<IEnumerable<Paciente>, IEnumerable<PacienteViewModel>>(_pacienteService.GetAll());
            return View(pacientes);
        }
        public ActionResult RedirectToCreate()
        {
            TempData["ShowModal"] = "ShowModal";
            IEnumerable<PacienteViewModel> pacientes = _pacienteDomainToViewModel.Map<IEnumerable<Paciente>, IEnumerable<PacienteViewModel>>(_pacienteService.GetAll());
            return RedirectToAction("Index", pacientes);
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
                if (!_pacienteService.ExistsCPF(paciente.CPF))
                {
                    Paciente newPaciente = _pacienteViewModelToDomain.Map<Paciente>(paciente);
                    _pacienteService.Add(newPaciente);
                    TempData["Message"] = "Paciente criado com sucesso!";
                    return View("Create");
                }
                ModelState.AddModelError(string.Empty, "O CPF já está em uso.");
            }
            return View(paciente);
        }

        public ActionResult Edit(int id)
        {
            Paciente paciente = _pacienteService.GetById(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            PacienteViewModel newPaciente = _pacienteDomainToViewModel.Map<PacienteViewModel>(paciente);
            return View(newPaciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,CPF,DataNascimento,Sexo,Telefone,Email")] PacienteViewModel paciente)
        {
            if (ModelState.IsValid)
            {
                Paciente newPaciente = _pacienteViewModelToDomain.Map<Paciente>(paciente);
                _pacienteService.Update(newPaciente);
                TempData["Message"] = "Paciente editado com sucesso!";
                return View("Edit");
            }
            return View(paciente);
        }

        public ActionResult Delete(int id)
        {
            Paciente paciente = _pacienteService.GetById(id);
            _pacienteService.Remove(paciente);
            return RedirectToAction("Index");
        }

    }
}

