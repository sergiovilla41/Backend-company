using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.Models
{
    [ExcludeFromCodeCoverage]
    [Table("Dominio", Schema = "SeguridadTercero")]
    public class Dominio
    {
        [Key()]
        public Guid IdDominio { get; set; }
        [Column("Dominio")]
        public string Domain { get; set; } = "";
    }
}
