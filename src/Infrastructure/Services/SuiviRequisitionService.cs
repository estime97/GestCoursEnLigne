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
                    { _localizer["NomRequerant"], item => item.NomRequerant },
                    { _localizer["PrenomRequerant"], item => item.PrenomRequerant },
                    { _localizer["Localite"], item => item.Localite },
                    { _localizer["DateRequisition"], item => item.DateRequisition },
                    { _localizer["BureauRequisition"], item => item.BureauRequisition },
                    { _localizer["Geometre"], item => item.Geometre },
                    { _localizer["DateBornage"], item => item.DateBornage },
                    { _localizer["EquipeBornage"], item => item.EquipeBornage },
                    { _localizer["DateTransmission"], item => item.DateTransmission },
                    { _localizer["TypePrestation"], item => item.TypePrestation },
                    { _localizer["NumeroJORT"], item => item.NumeroJORT },
                    { _localizer["DateInsertionJORT"], item => item.DateInsertionJORT },
                    { _localizer["DateAffichage"], item => item.DateAffichage },
                    { _localizer["DatePublication"], item => item.DatePublication },
                    { _localizer["NumeroTitre"], item => item.NumeroTitre },
                    { _localizer["DateSigned"], item => item.DateSigned },
                    { _localizer["DateRetrait"], item => item.DateRetrait },
                    { _localizer["StatutRequisition"], item => item.StatutRequisition },
                    { _localizer["Region"], item => item.Region },
                    { _localizer["MotifRejet"], item => item.MotifRejet },
                });

            return result;
        }
    }
}