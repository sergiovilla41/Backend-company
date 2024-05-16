using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.Common
{
    [ExcludeFromCodeCoverage]
    public class DataSendgrid
    {
        public object? personalizations { get; set; }
        public string? templateId { get; set; }
        public List<EmailData>? toEmails { get; set; }
        public List<EmailData>? ccEmails { get; set; }
        public TemplateData? templateData { get; set; }
    }

    public class EmailData
    {
        public string? name { get; set; }
        public string? email { get; set; }
    }

    public class TemplateData { 
        public dynamic? notificaciones { get; set; }
    }
}
