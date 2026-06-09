using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using MediatR;

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Quiz.Queries
{
    public class DeleteQuizCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteQuizCommandHandler
        : IRequestHandler<DeleteQuizCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ILogger<DeleteQuizCommandHandler> _logger;
        private readonly IStringLocalizer<DeleteQuizCommandHandler> _localizer;

        public DeleteQuizCommandHandler(
            IUnitOfWork<int> unitOfWork,
            ILogger<DeleteQuizCommandHandler> logger,
            IStringLocalizer<DeleteQuizCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(
            DeleteQuizCommand command,
            CancellationToken cancellationToken)
        {
            var quiz = await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Quiz>()
                .GetByIdAsync(command.Id);

            if (quiz != null)
            {
                try
                {
                    await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Quiz>().DeleteAsync(quiz);
                    await _unitOfWork.CommitAndRemoveCache(
                        cancellationToken,
                        ApplicationConstants.Cache.GetAllQuizzesCacheKey);

                    return await Result<int>.SuccessAsync(
                        quiz.Id, _localizer["Quiz Deleted"]);
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
                _logger.LogDebug("Quiz non trouvé pour l'id {Id}", command.Id);
                return await Result<int>.FailAsync(_localizer["Quiz Not Found!"]);
            }
        }
    }
}
