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
                    requisition.StatutRequisition = command.Requisition.StatutRequisition;
                    requisition.NomRequerant = command.Requisition.NomRequerant;
                    requisition.PrenomRequerant = command.Requisition.PrenomRequerant;
                    requisition.Localite = command.Requisition.Localite;
                    requisition.DateRequisition = command.Requisition.BureauRequisition;
                    requisition.Geometre = command.Requisition.DateBornage;
                    requisition.EquipeBornage = command.Requisition.EquipeBornage;
                    requisition.DateTransmission = command.Requisition.DateTransmission;
                    requisition.TypePrestation = command.Requisition.TypePrestation;
                    requisition.NumeroJORT = command.Requisition.NumeroJORT;
                    requisition.DateInsertionJORT = command.Requisition.DateInsertionJORT;
                    requisition.DateAffichage = command.Requisition.DateAffichage;
                    requisition.DatePublication = command.Requisition.DatePublication;
                    requisition.NumeroTitre = command.Requisition.NumeroTitre;
                    requisition.DateSigned = command.Requisition.DateSigned;
                    requisition.DateRetrait = command.Requisition.DateRetrait;
                    requisition.StatutRequisition = command.Requisition.StatutRequisition;
                    requisition.Region = command.Requisition.Region;
                    requisition.MotifRejet = command.Requisition.MotifRejet;
                    await _unitOfWork.Repository<Requisition>().UpdateAsync(requisition);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllRequisitions);
                    return await Result<string>.SuccessAsync(requisition.NumeroRequisition, "Requisition updated successfully.");
                }
                var response = await _unitOfWork.Repository<Requisition>().AddAsync(new Requisition()
                {
                    NumeroRequisition = command.Requisition.NumeroRequisition,
                    NomRequerant = command.Requisition.NomRequerant,
                    PrenomRequerant = command.Requisition.PrenomRequerant,
                    Localite = command.Requisition.Localite,
                    DateRequisition = command.Requisition.DateRequisition,
                    BureauRequisition = command.Requisition.BureauRequisition,
                    Geometre = command.Requisition.Geometre,
                    DateBornage = command.Requisition.DateBornage,
                    EquipeBornage = command.Requisition.EquipeBornage,
                    DateTransmission = command.Requisition.DateTransmission,
                    TypePrestation = command.Requisition.TypePrestation,
                    NumeroJORT = command.Requisition.NumeroJORT,
                    DateInsertionJORT = command.Requisition.DateInsertionJORT,
                    DateAffichage = command.Requisition.DateAffichage,
                    DatePublication = command.Requisition.DatePublication,
                    NumeroTitre = command.Requisition.NumeroTitre,
                    DateSigned = command.Requisition.DateSigned,
                    DateRetrait = command.Requisition.DateRetrait,
                    StatutRequisition = command.Requisition.StatutRequisition,
                    Region = command.Requisition.Region,
                    MotifRejet = command.Requisition.MotifRejet
                });
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllRequisitions);
                return await Result<string>.SuccessAsync(response.NumeroRequisition, "Requisition saved successfully.");
            }
        }
    }
}
