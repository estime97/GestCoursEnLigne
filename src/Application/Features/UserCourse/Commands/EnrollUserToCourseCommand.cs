using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Cours;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.UserCourse.Commands
{
    public class EnrollUserToCourseCommand : IRequest<Result<int>>
    {
        public int id { get; set; }
        public int CourseId { get; set; }
        public string UserId { get; set; }
    }

    public class EnrollUserToCourseCommandHandler : IRequestHandler<EnrollUserToCourseCommand, Result<int>>
    {
       public readonly IUnitOfWork<int> _unitOfWork;

        public EnrollUserToCourseCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }   

        public async Task<Result<int>> Handle(EnrollUserToCourseCommand request, CancellationToken cancellationToken)
        {
            if (request.id == 0) {

                var enrollment = new BlazorHero.CleanArchitecture.Domain.Entities.Cours.UserCourse
                {
                    UserId = request.UserId,
                    CourseId = request.CourseId
                };
                await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.UserCourse>().AddAsync(enrollment);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllEnrollUserToCourseCacheKey);
                return await Result<int>.SuccessAsync(enrollment.Id);

            }
            else {  return Result<int>.Fail(); }
        }
    }
   
}
