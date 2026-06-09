using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Cours;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using MediatR;

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Questions.Queries
{
    public class DeleteQuestionCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteQuestionCommandHandler: IRequestHandler<DeleteQuestionCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ILogger<DeleteQuestionCommandHandler> _logger;
        private readonly IStringLocalizer<DeleteQuestionCommandHandler> _localizer;

        public DeleteQuestionCommandHandler(IUnitOfWork<int> unitOfWork, ILogger<DeleteQuestionCommandHandler> logger, IStringLocalizer<DeleteQuestionCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteQuestionCommand command,CancellationToken cancellationToken)
        {
            var question = await _unitOfWork.Repository<Question>().GetByIdAsync(command.Id);

            if (question != null)
            {
                try
                {
                    await _unitOfWork.Repository<Question>().DeleteAsync(question);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken,ApplicationConstants.Cache.GetAllQuestionsCacheKey);

                    return await Result<int>.SuccessAsync(
                        question.Id, _localizer["Question Deleted"]);
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
                _logger.LogDebug("Question non trouvée pour l'id {Id}", command.Id);
                return await Result<int>.FailAsync(_localizer["Question Not Found!"]);
            }
        }
    }
}
