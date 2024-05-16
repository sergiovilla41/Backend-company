using SimemNetAdmin.Domain.ViewModel.FileGenerationModel;

namespace SimemNetAdmin.Domain.ViewModel.Labels
{
    public class LabelDataSetDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public List<FileGenerationModelDto>? GeneracionArchivos { get; set; }
    }
}
