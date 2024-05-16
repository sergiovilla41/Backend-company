using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.Models.TipoVista
{
    [ExcludeFromCodeCoverage]
    [Table("TipoVista", Schema = "dato")]
    public class TipoVista
    {
        [Key()]
        public Guid IdTipoVista { get; set; }
        public string Titulo {  get; set; } = string.Empty;
        public bool Estado {  get; set; }
    }
}
