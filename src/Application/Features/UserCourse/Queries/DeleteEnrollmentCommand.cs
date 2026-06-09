using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.UserCourse.Queries
{
    public class DeleteEnrollmentCommand : IRequest<Result<string>>
    {
        public string UserId { get; set; }
        public int CourseId { get; set; }
    }

    internal class DeleteEnrollmentCommandHandler
        : IRequestHandler<DeleteEnrollmentCommand, Result<string>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ILogger<DeleteEnrollmentCommandHandler> _logger;
        private readonly IStringLocalizer<DeleteEnrollmentCommandHandler> _localizer;

        public DeleteEnrollmentCommandHandler(
            IUnitOfWork<int> unitOfWork,
            ILogger<DeleteEnrollmentCommandHandler> logger,
            IStringLocalizer<DeleteEnrollmentCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(
            DeleteEnrollmentCommand command,
            CancellationToken cancellationToken)
        {
            // Recherche par clé composite UserId + CourseId
            var enrollment = await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.UserCourse>().Entities
                             .FirstOrDefaultAsync(x =>
                             x.UserId == command.UserId &&
                             x.CourseId == command.CourseId,
                             cancellationToken);


            if (enrollment != null)
            {
                try
                {
                    await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.UserCourse>().DeleteAsync(enrollment);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllEnrollUserToCourseCacheKey);

                    return await Result<string>.SuccessAsync(
                        command.UserId, _localizer["Enrollment Deleted"]);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                    _logger.LogError(e, e.InnerException?.Message);
                    return await Result<string>.FailAsync(e.Message);
                }
            }
            else
            {
                _logger.LogDebug(
                    "Enrollment non trouvé pour UserId {UserId} et CourseId {CourseId}",
                    command.UserId, command.CourseId);
                return await Result<string>.FailAsync(_localizer["Enrollment Not Found!"]);
            }
        }
    }
}
