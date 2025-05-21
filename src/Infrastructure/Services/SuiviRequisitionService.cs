using BlazorHero.CleanArchitecture.Application.Features.Requisitions;
using BlazorHero.CleanArchitecture.Application.Interfaces.Services;

using Microsoft.Extensions.Localization;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Infrastructure.Services
{
    public class SuiviRequisitionService : ISuiviRequisitionService
    {
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<SuiviRequisitionService> _localizer;
        public SuiviRequisitionService(IExcelService excelService, IStringLocalizer<SuiviRequisitionService> localizer)
        {
            _excelService = excelService;
            _localizer = localizer;
        }
        public async Task<string> ExportToExcelAsync()
        {
            var emptyData = new List<RequisitionResponse>();

            var result = await _excelService.ExportAsync(emptyData, sheetName: _localizer["Requisitions"],
                mappers: new Dictionary<string, Func<RequisitionResponse, object>>
                {
                    { _localizer["NumeroRequisition"], item => item.NumeroRequisition },
                    { _localizer["NumeroTitreFoncier"], item => item.NumeroTitreFoncier },
                    { _localizer["DateRequisition"], item => item.DateRequisition },
                    { _localizer["NomRequerant"], item => item.NomRequerant },
                    { _localizer["PrenomRequerant"], item => item.PrenomRequerant },
                    { _localizer["Bureau"], item => item.Bureau },
                    { _localizer["Region"], item => item.Region },
                    { _localizer["Statut"], item => item.Statut },
                    { _localizer["MotifRejet"], item => item.MotifRejet },
                    { _localizer["PiecesManquantes"], item => item.PiecesManquantes },
                    { _localizer["DateCreation"], item => item.DateCreation }
                });

            return result;
        }
    }
}