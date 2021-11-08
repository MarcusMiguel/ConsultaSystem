[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ConsultaSystem.MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ConsultaSystem.MVC.App_Start.NinjectWebCommon), "Stop")]

namespace ConsultaSystem.MVC.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using AutoMapper;
    using ConsultaSystem.Application.UseCases;
    using ConsultaSystem.Domain.Entities;
    using ConsultaSystem.Domain.Interfaces.Repositories;
    using ConsultaSystem.Infra.Repositories;
    using ConsultaSystem.MVC.ViewModels;
    using MediatR;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));
            kernel.Bind<IPacienteRepository>().To<PacienteRepository>();
            kernel.Bind<IExameRepository>().To<ExameRepository>();
            kernel.Bind<ITipoDeExameRepository>().To<TipoDeExameRepository>();
            kernel.Bind<IConsultaRepository>().To<ConsultaRepository>();

            kernel.Bind<IMediator>().To<Mediator>();
            kernel.Bind<IRequestHandler<ExistsCPF, bool>>().To<ExistsCPFHandler>();
            kernel.Bind<IRequestHandler<AddPaciente, Paciente>>().To<AddPacienteHandler>();
            kernel.Bind<IRequestHandler<GetAllPacientes, IEnumerable<Paciente>>>().To<GetAllPacientesHandler>();
            kernel.Bind<IRequestHandler<GetPacienteById, Paciente>>().To<GetPacienteByIdHandler>();
            kernel.Bind<IRequestHandler<RemovePaciente, int>>().To<RemovePacienteHandler>();
            kernel.Bind<IRequestHandler<UpdatePaciente, Paciente>>().To<UpdatePacienteHandler>();

            kernel.Bind<IRequestHandler<HorarioIsAvailable, bool>>().To<HorarioIsAvailableHandler>();
            kernel.Bind<IRequestHandler<AddConsulta, Consulta>>().To<AddConsultaHandler>();
            kernel.Bind<IRequestHandler<GetAllConsultas, IEnumerable<Consulta>>>().To<GetAllConsultasHandler>();
            kernel.Bind<IRequestHandler<GetConsultaById, Consulta>>().To<GetConsultaByIdHandler>();
            kernel.Bind<IRequestHandler<RemoveConsulta, int>>().To<RemoveConsultaHandler>();
            kernel.Bind<IRequestHandler<UpdateConsulta, Consulta>>().To<UpdateConsultaHandler>();

            kernel.Bind<IRequestHandler<AddTipoDeExame, TipoDeExame>>().To<AddTipoDeExameHandler>();
            kernel.Bind<IRequestHandler<GetAllTiposDeExames, IEnumerable<TipoDeExame>>>().To<GetAllTiposDeExamesHandler>();
            kernel.Bind<IRequestHandler<GetTipoDeExameById, TipoDeExame>>().To<GetTipoDeExameByIdHandler>();
            kernel.Bind<IRequestHandler<RemoveTipoDeExame, int>>().To<RemoveTipoDeExameHandler>();
            kernel.Bind<IRequestHandler<UpdateTipoDeExame, TipoDeExame>>().To<UpdateTipoDeExameHandler>();

            kernel.Bind<IRequestHandler<AddExame, Exame>>().To<AddExameHandler>();
            kernel.Bind<IRequestHandler<GetAllExames, IEnumerable<Exame>>>().To<GetAllExamesHandler>();
            kernel.Bind<IRequestHandler<GetExameById, Exame>>().To<GetExameByIdHandler>();
            kernel.Bind<IRequestHandler<RemoveExame, int>>().To<RemoveExameHandler>();
            kernel.Bind<IRequestHandler<UpdateExame, Exame>>().To<UpdateExameHandler>();

            kernel.Bind<ServiceFactory>().ToMethod(ctx => t => ctx.Kernel.TryGet(t));

            var mapperConfiguration = CreateConfiguration();
            kernel.Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            kernel.Bind<IMapper>().ToMethod(ctx =>
                 new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private static MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Paciente, PacienteViewModel>();
                cfg.CreateMap<PacienteViewModel, Paciente>();
                cfg.CreateMap<Consulta, ConsultaViewModel>();
                cfg.CreateMap<ConsultaViewModel, Consulta>();
                cfg.CreateMap<Exame, ExameViewModel>();
                cfg.CreateMap<ExameViewModel, Exame>();
                cfg.CreateMap<TipoDeExame, TipoDeExameViewModel>();
                cfg.CreateMap<TipoDeExameViewModel, TipoDeExame>();
            });

            return config;
        }
    }
}