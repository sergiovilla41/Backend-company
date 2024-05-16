
using System.Diagnostics.CodeAnalysis;

namespace SimemNetAdmin.Domain.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class Paginator
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
