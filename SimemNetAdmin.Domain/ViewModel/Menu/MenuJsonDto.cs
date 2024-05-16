using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.ViewModel.Menu
{
    [ExcludeFromCodeCoverage]
    public class MenuJsonDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public Guid IdPadre { get; set; }
        public int Secuencia { get; set; }
        public string Enlace { get; set; } = string.Empty;
        public string? Icono { get; set; }
        public bool Activo { get; set; } = true;
        
    }
}
