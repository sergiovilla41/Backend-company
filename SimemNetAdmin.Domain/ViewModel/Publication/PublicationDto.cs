using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.ViewModel.Publication
{
    [ExcludeFromCodeCoverage]
    public class PublicationDto
    {
        public string? IdPublicacionRegulatoria { get; set; }
        public string? IdConfiguracionGeneracionArchivos { get; set; }
        public Int16? Dia { get; set; }
        public Int16? Mes { get; set; }
        public Int16? DiaSemana { get; set; }
        public bool IndDiaHabil { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
