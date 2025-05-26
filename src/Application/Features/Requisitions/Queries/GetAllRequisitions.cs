using BlazorHero.CleanArchitecture.Application.Extensions;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Application.Interfaces.Services;
using BlazorHero.CleanArchitecture.Application.Interfaces.Services.Identity;
using BlazorHero.CleanArchitecture.Application.Requests;
using BlazorHero.CleanArchitecture.Application.Specifications.Base;
using BlazorHero.CleanArchitecture.Domain.Entities.SuiviRequisition;
using BlazorHero.CleanArchitecture.Shared.Enums;
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
            private readonly ICurrentUserService _currentUserService;
            private readonly IUserService _userService;
            public Handler(IUnitOfWork<int> unitOfWork, ICurrentUserService currentUserService, IUserService userService)
            {
                _unitOfWork = unitOfWork;
                _currentUserService = currentUserService;
                _userService = userService;
            }

            public async Task<PaginatedResult<RequisitionResponse>> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _userService.GetAsync(_currentUserService.UserId);
                if (user == null) return (PaginatedResult<RequisitionResponse>)await PaginatedResult<RequisitionResponse>.FailAsync("User not found !");

                return await _unitOfWork.Repository<Requisition>().Entities
                    .Specify(new Specification(request.SearchString))
                    .Where(_ => _.Bureau == EnumHelper.GetDescription(user.Data.Bureau))
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
                        DateCreation = _.DateCreation,
                        Position = _.Position
                    })
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            }
        }
    }
}
