using AutoMapper;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Services;
using ConsultaSystem.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ConsultaSystem.MVC.Controllers
{
    public class ConsultasController : Controller
    {
        private IMapper _consultaViewModelToDomain = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<ConsultaViewModel, Consulta>()));
        private IMapper _consultaDomainToViewModel = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Consulta, ConsultaViewModel>()));
        private IMapper _pacienteDomainToViewModel = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Paciente, PacienteViewModel>()));
        private IMapper _tipoDeExameDomainToViewModel = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TipoDeExame, TipoDeExameViewModel>()));
        private IConsultaService _consultaService;
        private IPacienteService _pacienteService;
        private ITipoDeExameService _tipoDeExameService;

        public ConsultasController(IConsultaService consultaService,
                                    IPacienteService pacienteService,
                                    ITipoDeExameService tipoDeExameService)
        {
            _consultaService = consultaService;
            _pacienteService = pacienteService;
            _tipoDeExameService = tipoDeExameService;
        }
        public ActionResult Index()
        {
            var consultas = _consultaService.GetAll();
            IEnumerable<ConsultaViewModel> newConsultas = _consultaDomainToViewModel.Map<IEnumerable<Consulta> , IEnumerable<ConsultaViewModel>>(consultas);
            return View(newConsultas);
        }
        
        public ActionResult Create()
        {
            IEnumerable<PacienteViewModel> pacientes = _pacienteDomainToViewModel.Map<IEnumerable<Paciente>, IEnumerable<PacienteViewModel>>(_pacienteService.GetAll());
            ViewData["IDPaciente"] = new SelectList(pacientes, "ID", "Nome");

            IEnumerable<TipoDeExameViewModel> tiposDeExames = _tipoDeExameDomainToViewModel.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(_tipoDeExameService.GetAll());
            ViewData["IDTipoDeExame"] = new SelectList(tiposDeExames, "ID", "Nome", 0);

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
                    if (_consultaService.HorarioIsAvaliable(consulta.Horario))
                    {
                        Consulta newConsulta = _consultaViewModelToDomain.Map<Consulta>(consulta);
                        newConsulta.Protocolo = DateTime.Now.Ticks.ToString();
                        _consultaService.Add(newConsulta);
                        TempData["Message"] = "Consulta criada com sucesso!";

                        ViewData["IDPaciente"] = new SelectList(_tipoDeExameService.GetAll(), "ID", "Nome");
                        ViewData["IDTipoDeExame"] = new SelectList(_tipoDeExameService.GetAll(), "ID", "Nome");
                        ViewData["IDExame"] = new SelectList(Enumerable.Empty<SelectListItem>());

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

            var pacientes = _pacienteService.GetAll();
            if (pacientes.Count() > 0)
            {
                ViewData["IDPaciente"] = new SelectList(pacientes, "ID", "Nome");
            }

            ViewData["IDTipoDeExame"] = new SelectList(_tipoDeExameService.GetAll(), "ID", "Nome");
            ViewData["IDExame"] = new SelectList(Enumerable.Empty<SelectListItem>());

            return View(consulta);
        }

        public ActionResult Edit(int id)
        {
            Consulta consulta = _consultaService.GetById(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            ConsultaViewModel newConsulta = _consultaDomainToViewModel.Map<ConsultaViewModel>(consulta);
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
                    if (_consultaService.HorarioIsAvaliable(consulta.Horario))
                    {
                        Consulta consultaAlterada = _consultaService.GetById(consulta.ID);
                        consultaAlterada.Protocolo = DateTime.Now.Ticks.ToString();
                        consultaAlterada.Horario = consulta.Horario;
                        _consultaService.Update(consultaAlterada);
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
            Consulta consulta2 = _consultaService.GetById(consulta.ID);
            ConsultaViewModel newconsulta = _consultaDomainToViewModel.Map<ConsultaViewModel>(consulta2);
            return View(newconsulta);
        }

        public ActionResult Delete(int id)
        {
            Consulta consulta = _consultaService.GetById(id);
            _consultaService.Remove(consulta);
            return RedirectToAction("Index");
        }
    }
}
