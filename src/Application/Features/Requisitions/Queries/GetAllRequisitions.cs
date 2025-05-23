using BlazorHero.CleanArchitecture.Application.Extensions;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Application.Requests;
using BlazorHero.CleanArchitecture.Application.Specifications.Base;
using BlazorHero.CleanArchitecture.Domain.Entities.SuiviRequisition;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using MediatR;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Requisitions.Queries
{
    public static class GetAllRequisitions
    {
        #region
        public class Request : PaginateRequest, IRequest<PaginatedResult<RequisitionResponse>>
        {

        }
        public class Specification : HeroSpecification<Requisition>
        {
            public Specification(string search)
            {
                Criteria = _ => true;
                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();

                    Criteria = Criteria.And(_ =>
                    _.NumeroRequisition.ToLower().Contains(search) ||
                    _.NumeroTitreFoncier.ToLower().Contains(search) ||
                    _.NomRequerant.ToLower().Contains(search) ||
                    _.PrenomRequerant.ToLower().Contains(search) ||
                    _.Region.ToLower().Contains(search) ||
                    _.Bureau.ToLower().Contains(search) ||
                    _.Statut.ToLower().Contains(search));
                }
            }
        }
        #endregion
        internal class Handler : IRequestHandler<Request, PaginatedResult<RequisitionResponse>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;
            public Handler(IUnitOfWork<int> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<PaginatedResult<RequisitionResponse>> Handle(Request request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Repository<Requisition>().Entities
                    .Specify(new Specification(request.SearchString))
                    .Select(_ => new RequisitionResponse()
                    {
                        NumeroRequisition = _.NumeroRequisition,
                        NumeroTitreFoncier = _.NumeroTitreFoncier,
                        DateRequisition = _.DateRequisition,
                        NomRequerant = _.NomRequerant,
                        PrenomRequerant = _.PrenomRequerant,
                        Bureau = _.Bureau,
                        Region = _.Region,
                        Statut = _.Statut,
                        MotifRejet = _.MotifRejet,
                        PiecesManquantes = _.PiecesManquantes,
                        DateCreation = _.DateCreation
                    })
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            }
        }
    }
}
