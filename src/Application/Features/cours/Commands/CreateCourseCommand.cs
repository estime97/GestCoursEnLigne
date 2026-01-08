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

namespace BlazorHero.CleanArchitecture.Application.Features.cours.Commands
{
    public class CreateCourseCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public string CreatedById { get; set; }
    }
    internal class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public CreateCourseCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                var course = new Course()
                {
                    Id = 0,
                    Title = request.Title,
                    Description = request.Description,
                    ThumbnailUrl = request.ThumbnailUrl,
                    CreatedOn = DateTime.Now
                };
             await  _unitOfWork.Repository<Course>().AddAsync(course);
             await  _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllCourseCacheKey);
                return await Result<int>.SuccessAsync(course.Id, "Course Created");
            }
            else
            {
                var course = _unitOfWork.Repository<Course>().GetByIdAsync(request.Id).Result;
                if (course != null)
                {
                    course.Title = request.Title;
                    course.Description = request.Description;
                    course.ThumbnailUrl = request.ThumbnailUrl;                  
              await _unitOfWork.Repository<Course>().UpdateAsync(course);
              await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllCourseCacheKey);
                    return await Result<int>.SuccessAsync(course.Id, "Course Updated");
                }
                else
                {
                    return await Result<int>.FailAsync("Course Not Found");
                }
            }
        }
    }
}
