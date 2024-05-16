using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.Models.SeguridadTercero
{
    [ExcludeFromCodeCoverage]
    public class CompanyByDomainGetUsers
    {
        public Guid IdEmpresa { get; set; }
        public Guid IdDominio { get; set; }
        public string? NombreDominio { get; set; }
        public string? NombreEmpresa { get; set; }
    }
}
