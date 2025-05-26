using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.SuiviRequisition;
using BlazorHero.CleanArchitecture.Domain.Enums;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Enums;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Requisitions.Commands
{
    public static class UpdateRequisition
    {
        public record Command : IRequest<Result<string>>
        {
            public string NumeroRequisition { get; set; }
            public string OldStatut { get; set; }
            public string ActualStatut { get; set; }
            public string MotifRejet { get; set; }
            public ActualPosition PositionActuelle { get; set; }
            public string PiecesManquantes { get; set; }
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
                var response = await _unitOfWork.Repository<Requisition>().Entities
                    .FirstOrDefaultAsync(x => x.NumeroRequisition == command.NumeroRequisition, cancellationToken);
                if (response == null)
                {
                    return await Result<string>.FailAsync("Requisition not found.");
                }
                if (!string.IsNullOrWhiteSpace(command.ActualStatut))
                {
                    response.Statut = command.ActualStatut;
                }
                response.MotifRejet = command.MotifRejet;
                response.PiecesManquantes = command.PiecesManquantes;
                response.Position = EnumHelper.GetDescription(command.PositionActuelle);
                await _unitOfWork.Repository<Requisition>().UpdateAsync(response);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllRequisitions);
                return await Result<string>.SuccessAsync(response.NumeroRequisition, "Requisition updated.");
            }
        }
    }
}
