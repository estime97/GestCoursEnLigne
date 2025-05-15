using BlazorHero.CleanArchitecture.Application.Requests;

namespace BlazorHero.CleanArchitecture.Client.Infrastructure.Routes
{
    public static class SuiviRequisitionEndpoints
    {
        public const string Export = "api/v1/suivirequisition/export";
        public const string Import = "api/v1/suivirequisition/import";
        public const string Update = "api/v1/suivirequisition/update";
        public static string GetAll(PaginateRequest pageInfos) => $"api/v1/suivirequisition/?page={pageInfos.PageNumber}&size={pageInfos.PageSize}&search={pageInfos.SearchString}";
    }
}
