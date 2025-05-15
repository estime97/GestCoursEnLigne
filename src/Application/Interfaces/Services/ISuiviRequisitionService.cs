using BlazorHero.CleanArchitecture.Application.Interfaces.Common;

using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Infrastructure.Services
{
    public interface ISuiviRequisitionService : IService
    {
        Task<string> ExportToExcelAsync();
    }
}
