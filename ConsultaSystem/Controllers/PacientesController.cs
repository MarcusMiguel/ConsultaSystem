using System.Collections.Generic;
using System.Data;
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
    public class PacientesController : Controller
    {
        private ConsultaSystemContext db = new ConsultaSystemContext();

        private IMapper _pacienteDomainToViewModel;

        private IMapper _pacienteViewModelToDomain;

        public PacientesController()
        {
            _pacienteDomainToViewModel = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Paciente, PacienteViewModel>()));
            _pacienteViewModelToDomain = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<PacienteViewModel, Paciente>()));
        }
        public ActionResult Index()
        {
            IEnumerable<PacienteViewModel> pacientes = _pacienteDomainToViewModel.Map<IEnumerable<Paciente>, IEnumerable<PacienteViewModel>>(db.Pacientes.ToList());
            return View(pacientes);
        }
        public ActionResult RedirectToCreate()
        {
            TempData["ShowModal"] = "ShowModal";
            IEnumerable<PacienteViewModel> pacientes = _pacienteDomainToViewModel.Map<IEnumerable<Paciente>, IEnumerable<PacienteViewModel>>(db.Pacientes.ToList());
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
                var cpfIgual = db.Pacientes.ToList().Where(o => o.CPF == paciente.CPF);
                if (cpfIgual.Count() == 0 )
                {
                    Paciente newPaciente = _pacienteViewModelToDomain.Map<Paciente>(paciente);
                    db.Pacientes.Add(newPaciente);
                    db.SaveChanges();
                    TempData["Message"] = "Paciente criado com sucesso!";
                    return View("Create");
                }
                ModelState.AddModelError(string.Empty, "O CPF já está em uso.");
            }
            return View(paciente);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Pacientes.Find(id);
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
                db.Entry(newPaciente).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Paciente editado com sucesso!";
                return View("Edit");
            }
            return View(paciente);
        }

        public ActionResult Delete(int? id)
        {
            Paciente paciente = db.Pacientes.Find(id);
            db.Pacientes.Remove(paciente);
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
