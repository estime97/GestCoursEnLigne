using BlazorHero.CleanArchitecture.Application.Features.Requisitions;
using BlazorHero.CleanArchitecture.Application.Features.Requisitions.Commands;
using BlazorHero.CleanArchitecture.Application.Requests;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Extensions;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Routes;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.SuiviRequisitions
{
    public class SuiviRequisitionManager : ISuiviRequisitionManager
    {
        private readonly HttpClient _httpClient;
        public SuiviRequisitionManager(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<string> ExportToExcelAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(SuiviRequisitionEndpoints.Export);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<PaginatedResult<RequisitionResponse>> GetAllRequisitions(PaginateRequest pageInfos)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(SuiviRequisitionEndpoints.GetAll(pageInfos));
            return await response.ToPaginatedResult<RequisitionResponse>();
        }

        public async Task<IResult<int>> ImportRequisitions(ImportRequisitions.Command command)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(SuiviRequisitionEndpoints.Import, command);
            return await response.ToResult<int>();
        }

        public async Task<IResult<string>> UpdateRequisition(UpdateRequisition.Command command)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(SuiviRequisitionEndpoints.Update, command);
            return await response.ToResult<string>();
        }
    }
}
