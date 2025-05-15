using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Application.Interfaces.Services;
using BlazorHero.CleanArchitecture.Application.Requests;
using BlazorHero.CleanArchitecture.Domain.Entities.SuiviRequisition;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using MediatR;

using Microsoft.Extensions.Localization;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Requisitions.Commands
{
    public static class ImportRequisitions
    {
        public record Command : IRequest<Result<int>>
        {
            public UploadRequest UploadRequest { get; set; }
        }
        internal class Handler : IRequestHandler<Command, Result<int>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;
            private readonly IExcelService _excelService;
            private readonly IStringLocalizer<Command> _localizer;
            public Handler(IUnitOfWork<int> unitOfWork, IExcelService excelService, IStringLocalizer<Command> localizer)
            {
                _unitOfWork = unitOfWork;
                _excelService = excelService;
                _localizer = localizer;
            }
            public async Task<Result<int>> Handle(Command command, CancellationToken cancellationToken)
            {
                var stream = new MemoryStream(command.UploadRequest.Data);

                var ExcelDataRequest = await _excelService.ImportAsync(stream,
                    mappers: new Dictionary<string, Func<DataRow, RequisitionResponse, object>>
                    {
                        {
                            _localizer["NumeroRequisition"],
                            (row, item) => item.NumeroRequisition = row[_localizer["NumeroRequisition"]].ToString()
                        },
                        {
                            _localizer["NomRequerant"],
                            (row, item) => item.NomRequerant = row[_localizer["NomRequerant"]].ToString()
                        },
                        {
                            _localizer["PrenomRequerant"],
                            (row, item) => item.PrenomRequerant = row[_localizer["PrenomRequerant"]].ToString()
                        },
                        {
                            _localizer["Localite"],
                            (row, item) => item.Localite = row[_localizer["Localite"]].ToString()
                        },
                        {
                            _localizer["DateRequisition"],
                            (row, item) => item.DateRequisition = row[_localizer["DateRequisition"]].ToString()
                        },
                        {
                            _localizer["BureauRequisition"],
                            (row, item) => item.BureauRequisition = row[_localizer["BureauRequisition"]].ToString()
                        },
                        {
                            _localizer["Geometre"],
                            (row, item) => item.Geometre = row[_localizer["Geometre"]].ToString()
                        },
                        {
                            _localizer["DateBornage"],
                            (row, item) => item.DateBornage = row[_localizer["DateBornage"]].ToString()
                        },
                        {
                            _localizer["EquipeBornage"],
                            (row, item) => item.EquipeBornage = row[_localizer["EquipeBornage"]].ToString()
                        },
                        {
                            _localizer["DateTransmission"],
                            (row, item) => item.DateTransmission = row[_localizer["DateTransmission"]].ToString()
                        },
                        {
                            _localizer["TypePrestation"],
                            (row, item) => item.TypePrestation = row[_localizer["TypePrestation"]].ToString()
                        },
                        {
                            _localizer["NumeroJORT"],
                            (row, item) => item.NumeroJORT = row[_localizer["NumeroJORT"]].ToString()
                        },
                        {
                            _localizer["DateInsertionJORT"],
                            (row, item) => item.DateInsertionJORT = row[_localizer["DateInsertionJORT"]].ToString()
                        },
                        {
                            _localizer["DateAffichage"],
                            (row, item) => item.DateAffichage = row[_localizer["DateAffichage"]].ToString()
                        },
                        {
                            _localizer["DatePublication"],
                            (row, item) => item.DatePublication = row[_localizer["DatePublication"]].ToString()
                        },
                        {
                            _localizer["NumeroTitre"],
                            (row, item) => item.NumeroTitre = row[_localizer["NumeroTitre"]].ToString()
                        },
                        {
                            _localizer["DateSigned"],
                            (row, item) => item.DateSigned = row[_localizer["DateSigned"]].ToString()
                        },
                        {
                            _localizer["DateRetrait"],
                            (row, item) => item.DateRetrait = row[_localizer["DateRetrait"]].ToString()
                        },
                        {
                            _localizer["StatutRequisition"],
                            (row, item) => item.StatutRequisition = row[_localizer["StatutRequisition"]].ToString()
                        },
                        {
                            _localizer["Region"],
                            (row, item) => item.Region = row[_localizer["Region"]].ToString()
                        },
                        {
                            _localizer["MotifRejet"],
                            (row, item) => item.MotifRejet = row[_localizer["MotifRejet"]].ToString()
                        },

                    }, _localizer["Requisitions"]);


                if (!ExcelDataRequest.Succeeded)
                    return await Result<int>.FailAsync(ExcelDataRequest.Messages);

                var requisitions = ExcelDataRequest.Data;
                int nbRequisitionsImport = 0;

                foreach (var requisition in requisitions)
                {
                    if (!_unitOfWork.Repository<Requisition>().Entities.Any(_ => _.NumeroRequisition == requisition.NumeroRequisition))
                    {
                        await _unitOfWork.Repository<Requisition>().AddAsync(new Requisition()
                        {
                            NumeroRequisition = requisition.NumeroRequisition,
                            NomRequerant = requisition.NomRequerant,
                            PrenomRequerant = requisition.PrenomRequerant,
                            Localite = requisition.Localite,
                            DateRequisition = requisition.DateRequisition,
                            BureauRequisition = requisition.BureauRequisition,
                            Geometre = requisition.Geometre,
                            DateBornage = requisition.DateBornage,
                            EquipeBornage = requisition.EquipeBornage,
                            DateTransmission = requisition.DateTransmission,
                            TypePrestation = requisition.TypePrestation,
                            NumeroJORT = requisition.NumeroJORT,
                            DateInsertionJORT = requisition.DateInsertionJORT,
                            DateAffichage = requisition.DateAffichage,
                            DatePublication = requisition.DatePublication,
                            NumeroTitre = requisition.NumeroTitre,
                            DateSigned = requisition.DateSigned,
                            DateRetrait = requisition.DateRetrait,
                            StatutRequisition = requisition.StatutRequisition,
                            Region = requisition.Region,
                            MotifRejet = requisition.MotifRejet
                        });
                        nbRequisitionsImport++;
                    }
                }
                try
                {
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllRequisitions);
                }
                catch (Exception e)
                {
                    return await Result<int>.FailAsync(_localizer[e.Message]);
                }

                return await Result<int>.SuccessAsync(nbRequisitionsImport, _localizer[$"Importation de {nbRequisitionsImport} réquisitions effectuée avec succès !"]);
            }
        }
    }
}
