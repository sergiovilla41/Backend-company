using Microsoft.Extensions.Hosting;
using SimemNetAdmin.Application.Services.NotificationService;
using SimemNetAdmin.Domain.ViewModel.NotificationViewModel;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Application.Services.BackgroundTaskService
{
    [ExcludeFromCodeCoverage]
    public class BackgroundTaskService: IHostedService
    {
        private readonly NotificationServices notificationServices;   
        private List<ParameterNotificationViewModel> parameterTask;
        private Timer? _timer;

        public BackgroundTaskService() {
            notificationServices = new();
            parameterTask = [];
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            parameterTask = await GetParameterTask();
            SetTimer();
            await Task.CompletedTask;
        }

        private  Task<List<ParameterNotificationViewModel>> GetParameterTask() {
            return notificationServices.GetParameterNotification();
        }

        private void SetTimer()
        {
            var now = DateTime.Now;
            var nextExecutionTime = GetNextExecutionTime(now);
            
            _timer = new Timer(DoWork, null, nextExecutionTime - now, TimeSpan.FromMinutes(60)); 
        }

        private DateTime GetNextExecutionTime(DateTime now)
        {
            var nextExecutionTime = now;

            foreach (var parameter in parameterTask)
            {
                if (parameter.ExecutionTime >= now.Hour && parameter.ExecutionMinute > now.Minute && parameter.ExecutionTime + 5 <= 24)
                {
                    var scheduledTime = new DateTime(now.Year, now.Month, now.Day, parameter.ExecutionTime + 5, parameter.ExecutionMinute, 0);

                    if (scheduledTime > now)
                    {
                        nextExecutionTime = scheduledTime;
                        break;
                    }
                }
            }

            if (nextExecutionTime <= now)
            {
                nextExecutionTime = new DateTime(now.Year, now.Month, now.Day, parameterTask[0].ExecutionTime , parameterTask[0].ExecutionMinute, 0).AddDays(1);
            }
            return nextExecutionTime;
        }

        private void DoWork(object? state)
        {
            var now = DateTime.Now;
            foreach (var parameter in parameterTask)
            {
                if (now.Hour == parameter.ExecutionTime && now.Minute == parameter.ExecutionMinute) {
                    notificationServices.GenerateSendNotification(parameter);
                }
            }
            SetTimer();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
