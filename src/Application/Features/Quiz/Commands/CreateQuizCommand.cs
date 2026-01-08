using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using MediatR;

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
    internal class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, Result<int>>
    {
        public readonly IUnitOfWork<int> _unitOfWork;

        public CreateQuizCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                var quiz = new BlazorHero.CleanArchitecture.Domain.Entities.Cours.Quiz()
                {
                    CourseId = request.CourseId,
                    Title = request.Title,
                };
                var newQuiz = await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Quiz>().AddAsync(quiz);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.QuizzesCacheKey);
                return await Result<int>.SuccessAsync(newQuiz.Id, "Quizz saved!");
            }
            else
            {
                var quiz = await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Quiz>().GetByIdAsync(request.Id);
                if (quiz != null)
                {
                    quiz.Title = request.Title;
                    quiz.CourseId = request.CourseId;
                    await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Quiz>().UpdateAsync(quiz);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.QuizzesCacheKey);
                    return await Result<int>.SuccessAsync(quiz.Id, "Quizz updated!");
                }
                else
                {
                    return await Result<int>.FailAsync("Quizz not found!");
                }
            }
        }
    }
}
