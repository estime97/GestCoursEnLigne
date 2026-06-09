using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Cours;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using MediatR;

using Microsoft.Extensions.Localization;

using Microsoft.Extensions.Logging;

using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Quiz.Commands
{
    public class CreateQuizCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
    }

    internal class CreateQuizCommandHandler
        : IRequestHandler<CreateQuizCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ILogger<CreateQuizCommandHandler> _logger;
        private readonly IStringLocalizer<CreateQuizCommandHandler> _localizer;

        public CreateQuizCommandHandler(
            IUnitOfWork<int> unitOfWork,
            ILogger<CreateQuizCommandHandler> logger,
            IStringLocalizer<CreateQuizCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(
            CreateQuizCommand command,
            CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                // Vérifier que le cours parent existe
                var course = await _unitOfWork.Repository<Course>()
                    .GetByIdAsync(command.CourseId);

                if (course == null)
                {
                    _logger.LogDebug("Course non trouvé pour l'id {Id}", command.CourseId);
                    return await Result<int>.FailAsync(
                        _localizer["Parent Course Not Found!"]);
                }

                var quiz = new BlazorHero.CleanArchitecture.Domain.Entities.Cours.Quiz
                {
                    Title = command.Title,
                    CourseId = command.CourseId
                };

                await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Quiz>().AddAsync(quiz);
                await _unitOfWork.CommitAndRemoveCache(
                    cancellationToken,
                    ApplicationConstants.Cache.GetAllQuizzesCacheKey);

                return await Result<int>.SuccessAsync(
                    quiz.Id, _localizer["Quiz Created"]);
            }
            else
            {
                var quiz = await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Quiz>()
                    .GetByIdAsync(command.Id);

                if (quiz == null)
                {
                    _logger.LogDebug("Quiz non trouvé pour l'id {Id}", command.Id);
                    return await Result<int>.FailAsync(_localizer["Quiz Not Found!"]);
                }

                // Si le CourseId change, vérifier que le nouveau cours existe
                if (quiz.CourseId != command.CourseId)
                {
                    var course = await _unitOfWork.Repository<Course>()
                        .GetByIdAsync(command.CourseId);

                    if (course == null)
                    {
                        _logger.LogDebug("Course non trouvé pour l'id {Id}", command.CourseId);
                        return await Result<int>.FailAsync(
                            _localizer["Parent Course Not Found!"]);
                    }
                }

                quiz.Title = command.Title ?? quiz.Title;
                quiz.CourseId = command.CourseId;

                await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Quiz>().UpdateAsync(quiz);
                await _unitOfWork.CommitAndRemoveCache(
                    cancellationToken,
                    ApplicationConstants.Cache.GetAllQuizzesCacheKey);

                return await Result<int>.SuccessAsync(
                    quiz.Id, _localizer["Quiz Updated"]);
            }
        }
    }
}
