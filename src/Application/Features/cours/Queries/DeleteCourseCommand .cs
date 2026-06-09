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

namespace BlazorHero.CleanArchitecture.Application.Features.cours.Queries
{
    public class DeleteCourseCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ILogger<DeleteCourseCommandHandler> _logger;
        private readonly IStringLocalizer<DeleteCourseCommandHandler> _localizer;

        public DeleteCourseCommandHandler( IUnitOfWork<int> unitOfWork,ILogger<DeleteCourseCommandHandler> logger,IStringLocalizer<DeleteCourseCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteCourseCommand command,CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.Repository<Course>().GetByIdAsync(command.Id);

            if (course != null)
            {
                try
                {
                    await _unitOfWork.Repository<Course>().DeleteAsync(course);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken,ApplicationConstants.Cache.GetAllCourseCacheKey);

                    return await Result<int>.SuccessAsync(course.Id, _localizer["Course Deleted"]);
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
                _logger.LogDebug("Course non trouvé pour l'id {Id}", command.Id);
                return await Result<int>.FailAsync(_localizer["Course Not Found!"]);
            }
        }
    }
}
