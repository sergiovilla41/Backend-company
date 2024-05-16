using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Domain.Models.Notification
{
    [ExcludeFromCodeCoverage]
    [Table("DiccionarioError", Schema = "Configuracion")]
    public partial class DictionaryErrorModel
    {
        [Key()]
        public Guid IdDiccionarioError { get; set; }
        public string? Error { get; set; }
        public string? DescripcionError { get; set; }
        public string? Origen { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
