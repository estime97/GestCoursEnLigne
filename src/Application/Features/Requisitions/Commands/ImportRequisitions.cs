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
                            _localizer["NumeroTitreFoncier"],
                            (row, item) => item.NumeroTitreFoncier = row[_localizer["NumeroTitreFoncier"]].ToString()
                        },
                        {
                            _localizer["DateRequisition"],
                            (row, item) => item.DateRequisition = row[_localizer["DateRequisition"]].ToString()
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
                            _localizer["Bureau"],
                            (row, item) => item.Bureau = row[_localizer["Bureau"]].ToString()
                        },
                        {
                            _localizer["Region"],
                            (row, item) => item.Region = row[_localizer["Region"]].ToString()
                        },
                        {
                            _localizer["Statut"],
                            (row, item) => item.Statut = row[_localizer["Statut"]].ToString()
                        },
                        {
                            _localizer["MotifRejet"],
                            (row, item) => item.MotifRejet = row[_localizer["MotifRejet"]].ToString()
                        },
                        {
                            _localizer["PiecesManquantes"],
                            (row, item) => item.PiecesManquantes = row[_localizer["PiecesManquantes"]].ToString()
                        },
                        {
                            _localizer["DateCreation"],
                            (row, item) => item.DateCreation = row[_localizer["DateCreation"]].ToString()
                        }

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
                            NumeroTitreFoncier = requisition.NumeroTitreFoncier,
                            DateRequisition = requisition.DateRequisition,
                            NomRequerant = requisition.NomRequerant,
                            PrenomRequerant = requisition.PrenomRequerant,
                            Bureau = requisition.Bureau,
                            Region = requisition.Region,
                            Statut = requisition.Statut,
                            MotifRejet = requisition.MotifRejet,
                            PiecesManquantes = requisition.PiecesManquantes,
                            DateCreation = requisition.DateCreation
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
