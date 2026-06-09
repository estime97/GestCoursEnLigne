using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using MediatR;

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Module.Queries
{
    public class DeleteModuleCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ILogger<DeleteModuleCommandHandler> _logger;
        private readonly IStringLocalizer<DeleteModuleCommandHandler> _localizer;

        public DeleteModuleCommandHandler(IUnitOfWork<int> unitOfWork, ILogger<DeleteModuleCommandHandler> logger, IStringLocalizer<DeleteModuleCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteModuleCommand command, CancellationToken cancellationToken)
        {
            var module = await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Module>()
                .GetByIdAsync(command.Id);

            if (module != null)
            {
                try
                {
                    await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Module>().DeleteAsync(module);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.ModulesCacheKey);

                    return await Result<int>.SuccessAsync(module.Id, _localizer["Module Deleted"]);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                    _logger.LogError(e, e.InnerException?.Message);
                    return await Result<int>.FailAsync(e.Message);
                }
            }
            else
            {
                _logger.LogDebug("Module non trouvé pour l'id {Id}", command.Id);
                return await Result<int>.FailAsync(_localizer["Module Not Found!"]);
            }
        }
    }
}
