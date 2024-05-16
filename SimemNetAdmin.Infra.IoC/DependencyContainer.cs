using Microsoft.Extensions.DependencyInjection;
using SimemNetAdmin.Application.Interfaces;
using SimemNetAdmin.Application.Interfaces.ColumnasOrigenService;
using SimemNetAdmin.Application.Interfaces.ConfiguracionEjecucionService;
using SimemNetAdmin.Application.Interfaces.Dcatservice;
using SimemNetAdmin.Application.Interfaces.ExecutionService;
using SimemNetAdmin.Application.Interfaces.GeneracionArchivos;
using SimemNetAdmin.Application.Interfaces.MenuServices;
using SimemNetAdmin.Application.Interfaces.NotificationService;
using SimemNetAdmin.Application.Services;
using SimemNetAdmin.Application.Services.ColumnasOrigenService;
using SimemNetAdmin.Application.Services.ConfiguracionEjecucionService;
using SimemNetAdmin.Application.Services.DcatService;
using SimemNetAdmin.Application.Services.ExecutionService;
using SimemNetAdmin.Application.Services.ExtractionService;
using SimemNetAdmin.Application.Services.GeneracionArchivos;
using SimemNetAdmin.Application.Services.MenuService;
using SimemNetAdmin.Application.Services.NotificationService;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Interfaces.GeneracionArchivos;
using SimemNetAdmin.Domain.Interfaces.NotificationDomain;
using SimemNetAdmin.Infra.Data.Repository;
using SimemNetAdmin.Infra.Data.Repository.ColumnasOrigenRepository;
using SimemNetAdmin.Infra.Data.Repository.ConfiguracionEjecucion;
using SimemNetAdmin.Infra.Data.Repository.Dcat;
using SimemNetAdmin.Infra.Data.Repository.Execution;
using SimemNetAdmin.Infra.Data.Repository.Extraction;
using SimemNetAdmin.Infra.Data.Repository.GeneracionArchivos;
using SimemNetAdmin.Infra.Data.Repository.Menu;
using SimemNetAdmin.Infra.Data.Repository.NotificationRepository;
using SimemNetAdmin.Transversal.Interfaces;
using SimemNetAdmin.Transversal.SendNotifications;
using System.Diagnostics.CodeAnalysis;
using SimemNetAdmin.Infra.Data.Repository.ClasificacionRegulatoria;
using SimemNetAdmin.Application.Interfaces.PublicationService;
using SimemNetAdmin.Application.Services.PublicationService;
using SimemNetAdmin.Infra.Data.Repository.Publication;
using SimemNetAdmin.Application.Interfaces.Columns;
using SimemNetAdmin.Application.Services.Columns;
using SimemNetAdmin.Domain.Interfaces.Columns;
using SimemNetAdmin.Infra.Data.Repository.Column;

namespace SimemNetAdmin.Infra.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
             
            services.AddScoped<IMenuService, MenuServices>();
            services.AddScoped<IParameterNotificationRepository, ParameterNotificationRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationServices>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IDcatService, Dcatservice>();
            services.AddScoped<IDcatRepository, DcatRepository>();
            services.AddScoped<IGeneracionArchivoService, GeneracionArchivoService>();
            services.AddScoped<IGeneracionArchivoRepository, GeneracionArchivoRepository>();
            services.AddScoped<IOriginColumnsService, OriginColumnsService>();
            services.AddScoped<IOriginColumnsRepository, OriginColumnsRepository>();
            services.AddScoped<IExecutionConfigurationService, ExecutionConfigurationService>();
            services.AddScoped<IExecutionConfigurationRepository, ExecutionConfigurationRepository>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<IUserManagementRepository, UserManagementRepository>();
            services.AddScoped<IExtractionService, ExtractionService>();
            services.AddScoped<IExtractionRepository, ExtractionRepository>();
            services.AddScoped<IExecutionService, ExecutionService>();
            services.AddScoped<IExecutionRepository, ExecutionRepository>();
            services.AddScoped<IRegulatoryClassifyServiceInterface, RegulatoryClassifyService>();
            services.AddScoped<IRegulatoryClassifyRepositoryInterface, RegulatoryClassifyRepository>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<ILabelsService, LabelsService>();
            services.AddScoped<ILabelsRepository, LabelsRepository>();
            services.AddScoped<IAssociatedDataSetService, AssociatedDataSetService>();
            services.AddScoped<IAssociatedDataSetRepository, AssociatedDataSetRepository>();
            services.AddScoped<IPublicationService, PublicationService>();
            services.AddScoped<IPublicationRepository, PublicationRepository>();
            services.AddScoped<IExecutionLogService, ExecutionLogService>();
            services.AddScoped<IExecutionLogRepository, ExecutionLogRepository>();
            services.AddScoped<IConfiguracionColumnasDestinoService, ConfiguracionColumnasDestinoService>();
            services.AddScoped<IConfiguracionColumnasDestinoRepository,ConfiguracionColumnasDestinoRepository>();
        }
    }
}
