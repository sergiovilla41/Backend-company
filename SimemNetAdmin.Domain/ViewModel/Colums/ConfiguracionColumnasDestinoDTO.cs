using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.ViewModel.Colums
{
    public class ConfiguracionColumnasDestinoDTO
    {
        public Guid IdColumnaDestino { get; set; }
        public string? NombreColumnaDestino { get; set; }
        public string? TipoDato { get; set; }
        public string? AtributoVariable { get; set; }
        public Guid? VariableId { get; set; }
        public bool Estado { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
