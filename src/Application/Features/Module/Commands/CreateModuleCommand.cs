using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Module.Commands
{
    public class CreateModuleCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public string CreatedById { get; set; }
    }
    internal class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public CreateModuleCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                var module = new BlazorHero.CleanArchitecture.Domain.Entities.Cours.Module()
                {
                    Id = 0,
                    Title = request.Title,
                    CourseId = request.CourseId,
                    CreatedOn = DateTime.Now
                };
                var response = await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Module>().AddAsync(module);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.ModulesCacheKey);
                return await Result<int>.SuccessAsync(response.Id, "Module Created");

            }
            else
            {
                var oldModule = await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Module>().GetByIdAsync(request.Id);
                if (oldModule != null)
                {
                    oldModule.Title = request.Title;
                    await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Module>().UpdateAsync(oldModule);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.ModulesCacheKey);
                    return await Result<int>.SuccessAsync(oldModule.Id, "Module Updated");
                }
                else
                {
                    return await Result<int>.FailAsync("Module Not Found");
                }
            }
        }
    }
}
