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

namespace BlazorHero.CleanArchitecture.Application.Features.Lessons.Queries
{
    public class DeleteLessonCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ILogger<DeleteLessonCommandHandler> _logger;
        private readonly IStringLocalizer<DeleteLessonCommandHandler> _localizer;

        public DeleteLessonCommandHandler(IUnitOfWork<int> unitOfWork, ILogger<DeleteLessonCommandHandler> logger, IStringLocalizer<DeleteLessonCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteLessonCommand command, CancellationToken cancellationToken)
        {
            var lesson = await _unitOfWork.Repository<Lesson>()
                .GetByIdAsync(command.Id);

            if (lesson != null)
            {
                try
                {
                    await _unitOfWork.Repository<Lesson>().DeleteAsync(lesson);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllLessonsCacheKey);

                    return await Result<int>.SuccessAsync(
                        lesson.Id, _localizer["Lesson Deleted"]);
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
                _logger.LogDebug("Lesson non trouvée pour l'id {Id}", command.Id);
                return await Result<int>.FailAsync(_localizer["Lesson Not Found!"]);
            }
        }
    }
}
