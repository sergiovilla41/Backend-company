using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Application.Interfaces.BackGroundTaskService
{
    
    public interface IBackgroundTask
    {
        Task StartAsync(CancellationToken cancellationToken);
    }
}
