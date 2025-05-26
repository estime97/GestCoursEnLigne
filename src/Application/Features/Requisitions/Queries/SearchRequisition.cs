using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.SuiviRequisition;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Requisitions.Queries
{
    public static class SearchRequisition
    {
        public record Request(string NumeroRequisition) : IRequest<Result<RequisitionResponse>>;

        internal class Handler : IRequestHandler<Request, Result<RequisitionResponse>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;
            public Handler(IUnitOfWork<int> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<RequisitionResponse>> Handle(Request request, CancellationToken cancellationToken)
            {
                var response = await _unitOfWork.Repository<Requisition>().Entities
                    .FirstOrDefaultAsync(x => x.NumeroRequisition == request.NumeroRequisition, cancellationToken: cancellationToken);
                if (response != null)
                {
                    return await Result<RequisitionResponse>.SuccessAsync(new RequisitionResponse()
                    {
                        NumeroRequisition = response.NumeroRequisition,
                        NumeroTitreFoncier = response.NumeroTitreFoncier,
                        DateRequisition = response.DateRequisition,
                        NomRequerant = response.NomRequerant,
                        PrenomRequerant = response.PrenomRequerant,
                        Bureau = response.Bureau,
                        Region = response.Region,
                        Position = response.Position,
                        Statut = GetMessageRequerant(response),
                        MotifRejet = response.MotifRejet,
                        PiecesManquantes = response.PiecesManquantes,
                        DateCreation = response.DateCreation,
                    }, "Requisition found.");
                }
                return await Result<RequisitionResponse>.FailAsync("Requisition not found.");
            }

            private string GetMessageRequerant(Requisition requisition)
            {
                return requisition.Statut switch
                {
                    "Rejected" => "VOTRE RÉQUISITION  A ÉTÉ REJETÉE EN RAISON DE " + requisition.MotifRejet + " " + requisition.Region,

                    "Cancelled" => "VOTRE RÉQUISITION  A ÉTÉ ANNULÉE  EN RAISON DE " + requisition.MotifRejet,

                    "Created" => "VOTRE RÉQUISITION  EST CRÉÉE",

                    "FormalitePrealable" => "VOTRE RÉQUISTION EST TRANSMISE AU BUREAU PRÉALABLE " + requisition.Region + " POUR LES FORMALITÉS DE PUBLICATION AU JOURNAL OFFICIEL",

                    "EnvoieEditeur" => "VOTRE RÉQUISITION EST EN COURS DE PUBLICATION AU JOURNAL OFFICIEL",

                    "RetourEditeur" => "VOTRE RÉQUISITION EST PUBLIÉE AU JOURNAL OFFICIEL",

                    "ProgrammationBornage" => "VOTRE RÉQUISITION EST PROGRAMMÉE POUR LE BORNAGE " +
                    " A " + requisition.Region + ". VEUILLEZ PRENDRE TOUTES LES MESURES NÉCÉSSAIRES POUR EVITER LE BORNAGE NUL",

                    "Affichage" => "VOTRE RÉQUISITION EST PASSÉE A L'ÉTAPE AFFICHAGE. VEUILLEZ PASSER AU BUREAU DES FOMALITES PREALABLES DE LA DCCF " + requisition.Region + " POUR RETIRER LES ACCUSÉS",

                    "Bornage" => "LE BORNAGE DE VOTRE RÉQUISITION PRÉVU A ÉTÉ EXECUTÉ",

                    "SecurisationFonciere" => "VOTRE RÉQUISITION EST EN COURS DE SÉCURISATION. CONTACTEZ LES SERVICES DE LA DCCF " + requisition.Region,

                    "PlanValidation" => "LE PLAN DE VOTRE RÉQUISITION EST VALIDÉ. LE DOSSIER EST TRANSFERÉ A LA CONSERVATION FONCIERE. CONTACTEZ LES SERVICES DE LA DCCF " + requisition.Region,

                    "CadastreFiscal" => "",

                    "BorderauAnalytique" => "VOTRE REQUISITION EST VALIDEE ET TRANSMISE A LA SAISIE DU BORDEREAU ANALYTIQUE",

                    "ValidationBordereau" => "VOTRE REQUISITION EST VALIDEE ET TRANSMISE POUR LA CREATION DU TITRE FONCIER",

                    "Opposition" => "VOTRE DEMANDE D'IMMATRICULATION A FAIT L'OBJET D'UNE OPPOSITION. VEUILLEZ VOUS RAPPROCHER DU BUREAU CONTENTIEUX DE LA DCCF- " + requisition.Region + ". OTR-DCCF",

                    "Immatriculation" => "UN NUMERO DE TITRE FONCIER EST CREE A LA SUITE DE VOTRE REQUISITION ET EN ATTENTE DE SIGNATURE",

                    "Conservation" => "VOTRE REQUISITION EST VALIDEE ET TRANSMISE POUR LA CREATION DU TITRE FONCIER",

                    "Contentieux" => "VOTRE REQUISITION EST TRANSMISE AU CONTENTIEUX POUR " + requisition.MotifRejet + " VEUILLEZ VOUS RAPPROCHER DU BUREAU CONTENTIEUX DE LA DCCF- " + requisition.Region + ". OTR-DCCF",

                    _ => "Statut inconnu"
                };
            }
        }
    }
}
