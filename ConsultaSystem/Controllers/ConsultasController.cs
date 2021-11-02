using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ConsultaSystem.Data;
using ConsultaSystem.Entities;
using ConsultaSystem.ViewModels;
using AutoMapper;
using System.Collections.Generic;

namespace ConsultaSystem.Controllers
{
    public class ConsultasController : Controller
    {
        private ConsultaSystemContext db = new ConsultaSystemContext();

        private IMapper _consultaViewModelToDomain;

        private IMapper _consultaDomainToViewModel;

        private IMapper _pacienteDomainToViewModel;

        private IMapper _tipoDeExameDomainToViewModel;

        public ConsultasController()
        {
            _consultaViewModelToDomain = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<ConsultaViewModel, Consulta>()));
            _consultaDomainToViewModel = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Consulta, ConsultaViewModel>()));
            _pacienteDomainToViewModel = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Paciente, PacienteViewModel>()));
            _tipoDeExameDomainToViewModel = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TipoDeExame, TipoDeExameViewModel>()));
        }

        public ActionResult Index()
        {
            var consultas = db.Consultas.ToList();
            IEnumerable<ConsultaViewModel> newConsultas = _consultaDomainToViewModel.Map< IEnumerable <Consulta> , IEnumerable <ConsultaViewModel>>(consultas);

            return View(newConsultas);
        }
        
        public ActionResult Create()
        {
            IEnumerable<PacienteViewModel> pacientes = _pacienteDomainToViewModel.Map<IEnumerable<Paciente>, IEnumerable<PacienteViewModel>>(db.Pacientes.ToList());
            IEnumerable<TipoDeExameViewModel> tiposDeExames = _tipoDeExameDomainToViewModel.Map<IEnumerable<TipoDeExame>, IEnumerable<TipoDeExameViewModel>>(db.TiposDeExames.ToList());
            ViewData["IDPaciente"] = new SelectList(pacientes, "ID", "Nome");
           var newTiposDeExames = new SelectList(tiposDeExames, "ID", "Nome", 0);
            ViewData["IDTipoDeExame"] = newTiposDeExames;
            ViewData["IDExame"] = new SelectList(Enumerable.Empty<SelectListItem>());

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, Horario, IDPaciente, IDTipoDeExame, IDExame")] ConsultaViewModel consulta)
        {
            if (ModelState.IsValid && (consulta.IDTipoDeExame != 0))
            {
                if (consulta.Horario > DateTime.Now)
                {
                    var conflict = db.Consultas.ToList().Where(o => o.Horario == consulta.Horario);
                    if (conflict.Count() == 0)
                    {
                        Consulta newConsulta = _consultaViewModelToDomain.Map<Consulta>(consulta);
                        newConsulta.Protocolo = DateTime.Now.Ticks.ToString();
                        db.Consultas.Add(newConsulta);
                        db.SaveChanges();
                        TempData["Message"] = "Consulta criada com sucesso!";

                        var pacientes2 = db.Pacientes.ToList();
                        if (pacientes2.Count() > 0)
                        {
                            ViewData["IDPaciente"] = new SelectList(pacientes2, "ID", "Nome");
                        }
                        var tipoDeExames2 = db.TiposDeExames.ToList();
                        ViewData["IDTipoDeExame"] = new SelectList(tipoDeExames2, "ID", "Nome");
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

            var pacientes = db.Pacientes.ToList();
            if (pacientes.Count() > 0)
            {
                ViewData["IDPaciente"] = new SelectList(pacientes, "ID", "Nome");
            }

            ViewData["IDTipoDeExame"] = new SelectList(db.TiposDeExames.ToList(), "ID", "Nome");
            ViewData["IDExame"] = new SelectList(Enumerable.Empty<SelectListItem>());
            TempData["InvalidModelState"] = "ModelState inválido";

            return View(consulta);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Consulta consulta = db.Consultas.Find(id);
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
                    var conflict = db.Consultas.ToList().Where(o => (o.Horario == consulta.Horario) && (o.ID != consulta.ID));
                    if (conflict.Count() == 0)
                    {
                        Consulta consultaAlterada = _consultaViewModelToDomain.Map<Consulta>(consulta);
                        consultaAlterada.Protocolo = DateTime.Now.Ticks.ToString();
                        consultaAlterada.Horario = consulta.Horario;
                        db.SaveChanges();
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
            TempData["InvalidModelState"] = "ModelState inválido";
            return View(consulta);
        }

        public ActionResult Delete(int? id)
        {
            Consulta consulta = db.Consultas.Find(id);
            db.Consultas.Remove(consulta);
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
