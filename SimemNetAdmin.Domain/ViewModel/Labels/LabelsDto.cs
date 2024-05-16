using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.ViewModel.Labels
{
    [ExcludeFromCodeCoverage]
    public class LabelsDto
    {
        public Guid? Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
