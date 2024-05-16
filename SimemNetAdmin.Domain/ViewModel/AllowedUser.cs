using SimemNetAdmin.Domain.Models.SeguridadTercero;
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class AllowedUser
    {
        public List<Empresa> Empresas { get; set; } = [];
        public string Permisos { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
