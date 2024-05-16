
using SimemNetAdmin.Domain.ViewModel.NotificationViewModel;
using SimemNetAdmin.Infra.Data.Context;
using SimemNetAdmin.Domain.Interfaces.NotificationDomain;
using SimemNetAdmin.Domain.Models.Notification;
using System.Globalization;
using SimemNetAdmin.Domain.Models.DataSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SimemNetAdmin.Domain.ViewModel;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using SimemNetAdmin.Domain.Models.Execution;

namespace SimemNetAdmin.Infra.Data.Repository.NotificationRepository
{
    public class NotificationRepository : INotificationRepository
    {
        public NotificationRepository() { }

        public async Task<List<NotificationResult>?> GetLogExecution()
        {
            List<NotificationResult> LogExecutionLogResult = [];
            try
            {

                using var context = new SimemNetAdminDbContext();
                LogExecutionLogResult = await Task.Run(() =>
                new List<NotificationResult>(
                        from execLog in context.Set<ExecutionLogModel>()
                        join dictioError in context.Set<DictionaryErrorModel>()
                        on execLog.IdDiccionarioError equals dictioError.IdDiccionarioError
                        join fileGeneration in context.Set<FileGenerationModel>()
                        on execLog.IdConfiguracionGeneracionArchivos equals fileGeneration.IdConfiguracionGeneracionArchivos
                        where execLog.Estado == "Error" && !execLog.NotificacionEnviada

                        select new NotificationResult
                        {
                            NombreArchivoDestino = fileGeneration.NombreArchivoDestino,
                            IdLogsEjecuciones = execLog.IdLogsEjecuciones,
                            IdExtraccion = execLog.IdConfiguracionGeneracionArchivos,
                            FechaEjecucionInicio =HandleNulleableDate(execLog.FechaInicioEjecucion, "yyyy-MM-dd H:mms"), 
                            FechaEjecucionFin = HandleNulleableDate(execLog.FechaFinEjecucion, "yyyy-MM-dd H:mms"),
                            DeltaInicial = execLog.ValorDeltaInicial.ToString("yyyy-MM-dd"),
                            DeltaFinal = HandleNulleableDate(execLog.ValorDeltaFinal, "yyyy-MM-dd"), 
                            ProximaEjecucionProgramada = HandleNulleableDate(execLog.FechaProximaEjecucionProgramada, "yyyy-MM-dd"),
                            DescripcionError = $"Error: {dictioError.Error},  Origen:  {dictioError.Origen},  Descripción: {dictioError.DescripcionError}:",
                        })

                        .ToList());

                return LogExecutionLogResult;
            }
            catch (Exception)
            {
                return LogExecutionLogResult;
            }
        }

        public static string HandleNulleableDate(DateTime? date,string format)
        {
            return date.HasValue ? DateTime.Parse(date.Value.ToString()).ToString(format) : "";
        }

        public async Task<List<ExecutionAndErrorMonitoringViewModel>> GetExecutionAndErrorMonitoring()
        {
            List<ExecutionAndErrorMonitoringViewModel> executionAndErrorMonitoringResult = [];
            try
            {
                using var context = new SimemNetAdminDbContext();
                executionAndErrorMonitoringResult = await Task.Run(() =>
               new List<ExecutionAndErrorMonitoringViewModel>(
                      from execLog in context.ExecutionLogModel
                      join dictioError in context.DictionaryErrorModel
                      on execLog.IdDiccionarioError equals dictioError.IdDiccionarioError into dictioErrorJoin
                      from dictioError in dictioErrorJoin.DefaultIfEmpty()
                      join fileGeneration in context.FileGenerationModel
                      on execLog.IdConfiguracionGeneracionArchivos equals fileGeneration.IdConfiguracionGeneracionArchivos
                      join clasificacionRegulatoria in context.ClasificacionRegulatoriaModel
                      on fileGeneration.IdConfiguracionClasificacionRegulatoria equals clasificacionRegulatoria.IdConfiguracionClasificacionRegulatoria into clasificacionRegulatoriaJoin
                      from clasificacionRegulatoria in clasificacionRegulatoriaJoin.DefaultIfEmpty()

                      select new ExecutionAndErrorMonitoringViewModel
                       {
                          IdConfiguracionGeneracionArchivos = fileGeneration.IdConfiguracionGeneracionArchivos,
                          NombreConjuntoDeDatos = fileGeneration.Titulo,
                          NombreArchivoDestino =fileGeneration.NombreArchivoDestino,
                          Estado = execLog.Estado,
                          FechaInicioEjecucion = execLog.FechaInicioEjecucion.ToString("yyyy-MM-dd HH:mm"),
                          FechaFinEjecucion = HandleNulleableDate(execLog.FechaFinEjecucion, "yyyy-MM-dd HH:mm"),
                          EsRegulatorio = fileGeneration.IndRegulatorio,
                          FechaProximaEjecucion = execLog.FechaProximaEjecucionProgramada.HasValue ? Convert.ToDateTime(execLog.FechaProximaEjecucionProgramada.ToString()).ToString("yyyy-MM-dd HH:mm") : "",
                          LanzadoPor = "Ejecución automática",
                          IdEjecucion = execLog.IdLogsEjecuciones,
                          PipelineRunId = execLog.PipelineRunId,
                          ValorDeltaInicial = execLog.ValorDeltaInicial.ToString("yyyy-MM-dd"),
                          ValorDeltaFinal = HandleNulleableDate(execLog.ValorDeltaFinal, "yyyy-MM-dd"),
                          ClasificacionDeltas = clasificacionRegulatoria != null ? clasificacionRegulatoria.Descripcion : "",
                          Observaciones = dictioError != null ? $"Error: {dictioError.Error},  Origen:  {dictioError.Origen},  Descripción: {dictioError.DescripcionError}" : "",
                          Extracciones = GetExtractions(execLog.IdConfiguracionGeneracionArchivos),
                          Ejecuciones = GetExecution(execLog.IdConfiguracionGeneracionArchivos)
                      })

                       .ToList());
              
                return executionAndErrorMonitoringResult;
            }
            catch (Exception)
            {
                return executionAndErrorMonitoringResult;
            }
        }

        public static List<ExtractionsDto> GetExtractions(Guid IdConfiguracionGeneracionArchivos)
        {
            try
            {
                using var context = new SimemNetAdminDbContext();
                var extractions = context.ExtractionsModel.Where(x => x.IdConfiguracionGeneracionArchivos == IdConfiguracionGeneracionArchivos).ToList();

                var extractionsDto = new List<ExtractionsDto>();
                foreach (var extraction in extractions)
                {
                    extractionsDto.Add(
                        new ExtractionsDto { 
                            NombreExtraccion = extraction.NombreExtraccion,
                            FechaDeltaInicial = HandleNulleableDate(extraction.FechaDeltaInicial, "yyyy-MM-dd"),
                            FechaDeltaFinal = HandleNulleableDate(extraction.FechaDeltaFinal, "yyyy-MM-dd")
                        });
                }
                return extractionsDto;
            }
            catch (Exception)
            {
                return new List<ExtractionsDto>();
            }
        }

        public static List<ExecutionModel> GetExecution(Guid IdConfiguracionGeneracionArchivos)
        {
            try
            {
                using var context = new SimemNetAdminDbContext();
                return context.ExecutionModel.Where(x => x.IdConfiguracionGeneracionArchivos == IdConfiguracionGeneracionArchivos).ToList();
            }
            catch (Exception)
            {
                return new List<ExecutionModel>();
            }
        }

        public bool UpdateSubmittedErrors(List<NotificationResult> notificationResult)
        {
            try
            {
                using var context = new SimemNetAdminDbContext();
                notificationResult.ForEach((notificacion) =>
                {
                    var dbEntity = context.ExecutionLogModel.FirstOrDefault(x => x.IdLogsEjecuciones.Equals(notificacion.IdLogsEjecuciones));
                    if (dbEntity != null)
                    {
                        dbEntity.NotificacionEnviada = true;

                    }
                });
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<NotificacionDataSetRegulatorioViewModel>> NotificacionDataSetRegulatorio(string? idEmpresa = null)
        {
            List<NotificacionDataSetRegulatorioViewModel> notificacionDataSetRegulatorio = [];
            try
            {
                using var context = new SimemNetAdminDbContext();
                var param = new SqlParameter("@idEmpresa", idEmpresa ?? (object)DBNull.Value);
                List<NotificacionDataSetRegulatorioModel> notificacionDataSetRegulatorioResult = await context.NotificacionDataSet.FromSqlInterpolated($" EXEC [dato].[proc_NotificacionDataSetRegulatorio] @idEmpresa={param}").ToListAsync();
                if (notificacionDataSetRegulatorioResult.Count == 0)
                    return [];

                notificacionDataSetRegulatorio = await Task.Run(() =>
                new List<NotificacionDataSetRegulatorioViewModel>(
                     from dataSetRegulatorio in notificacionDataSetRegulatorioResult
                     select new NotificacionDataSetRegulatorioViewModel
                     {
                         Id = dataSetRegulatorio.IdConfiguracionGeneracionArchivos,
                         Nombre = dataSetRegulatorio.NombreArchivoDestino ?? "",
                         MaximaFechaRegulatoria = dataSetRegulatorio.MaximaFechaRegulatoria!,
                         FechaProximaEjecucion = dataSetRegulatorio.FechaProximaEjecucionProgramada.ToString()!,
                         DeltaInicialEjecutar = dataSetRegulatorio.DeltaInicial,
                         DeltaFinalEjecutar = dataSetRegulatorio.DeltaFinal,
                         DiasHabilesFaltantes = dataSetRegulatorio.DiasFaltantes,
                         Estado = dataSetRegulatorio.Estado,
                         Tema = dataSetRegulatorio.Tema ?? ""
                     }).OrderBy(x => x.DiasHabilesFaltantes).ToList());
                return notificacionDataSetRegulatorio;
            }
            catch (Exception)
            {
                return notificacionDataSetRegulatorio;
            }
        }

        public bool SaveLog(LogSendNotificationViewModel logSend)
        {
            try
            {
                using var context = new SimemNetAdminDbContext();
                LogSendNotificationModel consultaDatoLog = new()
                {

                    IdTipoNotificacion = logSend.IdTipoNotificacion,
                    IdParametroNotificacion = logSend.IdParametroNotificacion,
                    IdCorreoNotificacion = logSend.IdCorreoNotificacion,
                    DescripcionError = logSend.DescripcionError,
                    MetodoError = logSend.MetodoError,
                    EstadoEnvio = logSend.EstadoEnvio,
                    NumeroRegistros = logSend.NumeroRegistros,
                    FechaEjecucion = DateTime.Now,
                };
                context.LogSendNotificacionModel.Add(consultaDatoLog);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<NotificacionDataSetRegulatorioViewModel>> GenerateSendRegulatoryDataSet(bool cumple = true, string? idEmpresa = null)
        {
            return await GetNotificacionDataSetRegulatorio(cumple);
        }

        public async Task<List<NotificacionDataSetRegulatorioViewModel>> GetNotificacionDataSetRegulatorio(bool cumple, string? idEmpresa = null)
        {
            List<NotificacionDataSetRegulatorioViewModel> recordsNotification = [];
            try
            {
                List<NotificacionDataSetRegulatorioViewModel> notificationResults = await NotificacionDataSetRegulatorio(idEmpresa)!;
                recordsNotification.AddRange(notificationResults);

                if (!cumple && recordsNotification.Count != 0)
                {
                    recordsNotification.RemoveAll(r => r.Estado.Equals("Cumplido", StringComparison.CurrentCultureIgnoreCase));
                    recordsNotification.RemoveAll(r => r.Estado.Equals("Cargado Incumplido", StringComparison.CurrentCultureIgnoreCase));
                }

                return recordsNotification;
            }
            catch (Exception)
            {
                return recordsNotification = [];
            }
        }
    }
}

