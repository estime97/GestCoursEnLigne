using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.SuiviRequisition;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Requisitions.Commands
{
    public static class SynchronizeRequisitionDccf
    {
        public record Command : IRequest<Result<string>>
        {
            public RequisitionResponse Requisition { get; set; }
        }
        internal class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;
            public Handler(IUnitOfWork<int> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<string>> Handle(Command command, CancellationToken cancellationToken)
            {
                var requisition = await _unitOfWork.Repository<Requisition>().Entities.FirstOrDefaultAsync(_ => _.NumeroRequisition == command.Requisition.NumeroRequisition);
                if (requisition != null)
                {
                    requisition.NumeroTitreFoncier = command.Requisition.NumeroTitreFoncier;
                    requisition.DateRequisition = command.Requisition.DateRequisition;
                    requisition.NomRequerant = command.Requisition.NomRequerant;
                    requisition.PrenomRequerant = command.Requisition.PrenomRequerant;
                    requisition.Bureau = command.Requisition.Bureau;
                    requisition.Region = command.Requisition.Region;
                    requisition.Statut = command.Requisition.Statut;
                    requisition.MotifRejet = command.Requisition.MotifRejet;
                    requisition.PiecesManquantes = command.Requisition.PiecesManquantes;
                    requisition.DateCreation = command.Requisition.DateCreation;
                    await _unitOfWork.Repository<Requisition>().UpdateAsync(requisition);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllRequisitions);
                    return await Result<string>.SuccessAsync(requisition.NumeroRequisition, "Requisition updated successfully.");
                }
                var response = await _unitOfWork.Repository<Requisition>().AddAsync(new Requisition()
                {
                    NumeroRequisition = command.Requisition.NumeroRequisition,
                    NumeroTitreFoncier = command.Requisition.NumeroTitreFoncier,
                    DateRequisition = command.Requisition.DateRequisition,
                    NomRequerant = command.Requisition.NomRequerant,
                    PrenomRequerant = command.Requisition.PrenomRequerant,
                    Bureau = command.Requisition.Bureau,
                    Region = command.Requisition.Region,
                    Statut = command.Requisition.Statut,
                    MotifRejet = command.Requisition.MotifRejet,
                    PiecesManquantes = command.Requisition.PiecesManquantes,
                    DateCreation = command.Requisition.DateCreation,
                });
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllRequisitions);
                return await Result<string>.SuccessAsync(response.NumeroRequisition, "Requisition saved successfully.");
            }
        }
    }
}
