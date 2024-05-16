using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.Models.Archivo;
using SimemNetAdmin.Domain.Models.Categorias;
using SimemNetAdmin.Domain.Models.Columnas;
using SimemNetAdmin.Domain.Models.DataSet;
using SimemNetAdmin.Domain.Models.DuracionISO;
using SimemNetAdmin.Domain.Models.Etiqueta;
using SimemNetAdmin.Domain.Models.GeneracionArchivos;
using SimemNetAdmin.Domain.Models.Granularidad;
using SimemNetAdmin.Domain.Models.Menu;
using SimemNetAdmin.Domain.Models.Notification;
using SimemNetAdmin.Domain.Models.Periodicidad;
using SimemNetAdmin.Domain.Models.TipoVista;
using SimemNetAdmin.Domain.Models.SeguridadTercero;
using SimemNetAdmin.Transversal.KeyVault;
using System.Diagnostics.CodeAnalysis;
using SimemNetAdmin.Domain.Models;
using SimemNetAdmin.Domain.Models.Execution;
using SimemNetAdmin.Domain.Models.Extraction;
using SimemNetAdmin.Domain.Models.Publication;

namespace SimemNetAdmin.Infra.Data.Context
{
    public partial class SimemNetAdminDbContext : DbContext
    {
        public SimemNetAdminDbContext() { }

        [ExcludeFromCodeCoverage]
        public SimemNetAdminDbContext(DbContextOptions<SimemNetAdminDbContext> options) : base(options)
        {
            DictionaryErrorModel = Set<DictionaryErrorModel>();
            ExecutionLogModel = Set<ExecutionLogModel>();
            FileGenerationModel = Set<FileGenerationModel>();
            LogSendNotificacionModel = Set<LogSendNotificationModel>();
            MenuModel = Set<MenuModel>();
            MenuJsonModel = Set<MenuJsonModel>();
            NotificationEmailModel = Set<NotificationEmailModel>();
            NotificacionTypeModel = Set<NotificacionTypeModel>();
            ParameterNotificationModel = Set<ParameterNotificationModel>();
            NotificacionDataSet = Set<NotificacionDataSetRegulatorioModel>();
            Etiquetas = Set<Etiqueta>();
            GeneracionArchivoEtiquetas = Set<GeneracionArchivoEtiqueta>();
            ExecutionModel = Set<ExecutionModel>();
            ExtractionsModel = Set<ExtractionsModel>();
            ClasificacionRegulatoriaModel = Set<ClasificacionRegulatoriaModel>();
            GeneracionArchivosJson = Set<GeneracionArchivoJson>();
            Granularidad = Set<Granularidad>();
            ConfiguracionPeriodicidad = Set<ConfiguracionPeriodicidad>();
            ConfiguracionDuracionISO = Set<ConfiguracionDuracionIso>();
            DatosBasicosJson = Set<DatosBasicosJson>();
            Categorias = Set<Categoria>();
            Archivo = Set<Archivo>();
            GestionUsuarios = Set<GestionUsuarios>();
            TiposVista = Set<TipoVista>();
            InformacionDeltaRegulatorio = Set<InformacionDeltaRegulatorio>();            
            EmpresaDominio = Set<EmpresaDominio>();
            PublicationModel = Set<PublicationModel>();
            ColumnasDestino = Set<ConfiguracionColumnasDestino>();
        }
        public virtual DbSet<InformacionDeltaRegulatorio> InformacionDeltaRegulatorio { get; set; }
        public DbSet<Archivo> Archivo { get; set; }
        public virtual DbSet<DictionaryErrorModel> DictionaryErrorModel { get; set; }
        public virtual DbSet<ExecutionLogModel> ExecutionLogModel { get; set; }
        public virtual DbSet<FileGenerationModel> FileGenerationModel { get; set; }
        public virtual DbSet<LogSendNotificationModel> LogSendNotificacionModel { get; set; }
        public virtual DbSet<MenuModel> MenuModel { get; set; }
        public virtual DbSet<MenuJsonModel> MenuJsonModel { get; set; }
        public virtual DbSet<NotificationEmailModel> NotificationEmailModel { get; set; }
        public virtual DbSet<NotificacionTypeModel> NotificacionTypeModel { get; set; }
        public virtual DbSet<ParameterNotificationModel> ParameterNotificationModel { get; set; }
        public virtual DbSet<NotificacionDataSetRegulatorioModel> NotificacionDataSet { get; set; }
        public virtual DbSet<Etiqueta> Etiquetas { get; set; }
        public virtual DbSet<GeneracionArchivoEtiqueta> GeneracionArchivoEtiquetas { get; set; }
        public virtual DbSet<ExecutionModel> ExecutionModel { get; set; }
        public virtual DbSet<ExtractionsModel> ExtractionsModel { get; set; }
        public virtual DbSet<ClasificacionRegulatoriaModel> ClasificacionRegulatoriaModel { get; set; }
        public virtual DbSet<GeneracionArchivoJson> GeneracionArchivosJson { get; set; }
        public virtual DbSet<Granularidad> Granularidad { get; set; }
        public virtual DbSet<ConfiguracionPeriodicidad> ConfiguracionPeriodicidad { get; set; }
        public virtual DbSet<ConfiguracionDuracionIso> ConfiguracionDuracionISO { get; set; }
        public virtual DbSet<DatosBasicosJson> DatosBasicosJson { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<ConfiguracionColumnasDestino> ColumnasDestino { get; set; }
        public virtual DbSet<ConfiguracionColumnasOrigen> ColumnasOrigen { get; set; }
        public virtual DbSet<GestionUsuarios> GestionUsuarios { get; set; }
        public virtual DbSet<SeguridadDominio> SeguridadDominio { get; set; }
        public virtual DbSet<EmpresaDominio> EmpresaDominio { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<TipoVista> TiposVista { get; set; }
        public virtual DbSet<PublicationModel> PublicationModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string simmenConnectionValue = KeyVaultManager.GetSecretValue(KeyVaultTypes.SimemConnection);
                optionsBuilder.UseSqlServer(simmenConnectionValue);
            }
        }

        [ExcludeFromCodeCoverage]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuJsonModel>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<ConfiguracionColumnasDestino>(entity =>
            {
                entity.HasKey(e => e.IdColumnaDestino);
                entity.Property(e => e.IdColumnaDestino).HasColumnType("uniqueidentifier");

                
            });
            modelBuilder.Entity<NotificacionDataSetRegulatorioModel>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<GeneracionArchivoJson>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<DatosBasicosJson>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<Etiqueta>()
        .HasMany(e => e.GeneracionArchivos)
        .WithMany(e => e.Etiquetas)
        .UsingEntity<GeneracionArchivoEtiqueta>();
        }
    }
}