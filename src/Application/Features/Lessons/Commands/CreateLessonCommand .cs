using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Cours;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using MediatR;

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Lessons.Commands
{
    public class CreateLessonCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ContentUrl { get; set; }
        public string Type { get; set; } // "video" ou "pdf"
        public int ModuleId { get; set; }
    }

    internal class CreateLessonCommandHandler
        : IRequestHandler<CreateLessonCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ILogger<CreateLessonCommandHandler> _logger;
        private readonly IStringLocalizer<CreateLessonCommandHandler> _localizer;

        public CreateLessonCommandHandler(
            IUnitOfWork<int> unitOfWork,
            ILogger<CreateLessonCommandHandler> logger,
            IStringLocalizer<CreateLessonCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(
            CreateLessonCommand command,
            CancellationToken cancellationToken)
        {
            // Validation du type
            var validTypes = new[] { "video", "pdf" };
            if (!validTypes.Contains(command.Type?.ToLower()))
            {
                return await Result<int>.FailAsync(
                    _localizer["Type must be 'video' or 'pdf'!"]);
            }

            if (command.Id == 0)
            {
                // Vérifier que le module parent existe
                var module = await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Module>()
                    .GetByIdAsync(command.ModuleId);

                if (module == null)
                {
                    _logger.LogDebug("Module non trouvé pour l'id {Id}", command.ModuleId);
                    return await Result<int>.FailAsync(
                        _localizer["Parent Module Not Found!"]);
                }

                var lesson = new Lesson
                {
                    Title = command.Title,
                    ContentUrl = command.ContentUrl,
                    Type = command.Type.ToLower(),
                    ModuleId = command.ModuleId
                };

                await _unitOfWork.Repository<Lesson>().AddAsync(lesson);
                await _unitOfWork.CommitAndRemoveCache(
                    cancellationToken,
                    ApplicationConstants.Cache.GetAllLessonsCacheKey);

                return await Result<int>.SuccessAsync(
                    lesson.Id, _localizer["Lesson Created"]);
            }
            else
            {
                var lesson = await _unitOfWork.Repository<Lesson>()
                    .GetByIdAsync(command.Id);

                if (lesson == null)
                {
                    _logger.LogDebug("Lesson non trouvée pour l'id {Id}", command.Id);
                    return await Result<int>.FailAsync(_localizer["Lesson Not Found!"]);
                }

                // Si le ModuleId change, vérifier que le nouveau module existe
                if (lesson.ModuleId != command.ModuleId)
                {
                    var module = await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Module>()
                        .GetByIdAsync(command.ModuleId);

                    if (module == null)
                    {
                        _logger.LogDebug("Module non trouvé pour l'id {Id}", command.ModuleId);
                        return await Result<int>.FailAsync(
                            _localizer["Parent Module Not Found!"]);
                    }
                }

                lesson.Title = command.Title ?? lesson.Title;
                lesson.ContentUrl = command.ContentUrl ?? lesson.ContentUrl;
                lesson.Type = command.Type.ToLower();
                lesson.ModuleId = command.ModuleId;

                await _unitOfWork.Repository<Lesson>().UpdateAsync(lesson);
                await _unitOfWork.CommitAndRemoveCache(
                    cancellationToken,
                    ApplicationConstants.Cache.GetAllLessonsCacheKey);

                return await Result<int>.SuccessAsync(
                    lesson.Id, _localizer["Lesson Updated"]);
            }
        }
    }
}
