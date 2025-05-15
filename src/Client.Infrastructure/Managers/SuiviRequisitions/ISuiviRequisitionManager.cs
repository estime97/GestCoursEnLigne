using BlazorHero.CleanArchitecture.Application.Features.Requisitions;
using BlazorHero.CleanArchitecture.Application.Features.Requisitions.Commands;
using BlazorHero.CleanArchitecture.Application.Requests;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.SuiviRequisitions
{
    public interface ISuiviRequisitionManager : IManager
    {
        Task<IResult<int>> ImportRequisitions(ImportRequisitions.Command command);
        Task<IResult<string>> UpdateRequisition(UpdateRequisition.Command command);
        Task<PaginatedResult<RequisitionResponse>> GetAllRequisitions(PaginateRequest pageInfos);
        Task<string> ExportToExcelAsync();
    }
}
