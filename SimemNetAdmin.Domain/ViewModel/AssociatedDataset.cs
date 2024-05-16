using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class AssociatedDataset
    {
        public Guid? Id { get; set; }
        public string? Titulo { get; set; }
        public bool Estado { get; set; }
        public string? ConjuntoDeDatosAsociados { get; set; }
    }
}
