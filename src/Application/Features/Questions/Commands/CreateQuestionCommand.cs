using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Cours;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using MediatR;

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Questions.Commands
{
    public class CreateQuestionCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; } // doit être "A", "B", "C" ou "D"
        public int QuizId { get; set; }
    }

    internal class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ILogger<CreateQuestionCommandHandler> _logger;
        private readonly IStringLocalizer<CreateQuestionCommandHandler> _localizer;

        public CreateQuestionCommandHandler(
            IUnitOfWork<int> unitOfWork,
            ILogger<CreateQuestionCommandHandler> logger,
            IStringLocalizer<CreateQuestionCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var quiz = await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Quiz>()
                    .GetByIdAsync(command.QuizId);

                if (quiz == null)
                {
                    _logger.LogDebug("Quiz non trouvé pour l'id {Id}", command.QuizId);
                    return await Result<int>.FailAsync(_localizer["Parent Quiz Not Found!"]);
                }

                // CorrectAnswer doit être A, B, C ou D
                var validAnswers = new[] { "A", "B", "C", "D" };
                if (string.IsNullOrWhiteSpace(command.CorrectAnswer) || !validAnswers.Contains(command.CorrectAnswer.ToUpper()))
                {
                    return await Result<int>.FailAsync(
                        _localizer["CorrectAnswer must be A, B, C or D!"]);
                }

                var question = new Question
                {
                    Text = command.Text,
                    OptionA = command.OptionA,
                    OptionB = command.OptionB,
                    OptionC = command.OptionC,
                    OptionD = command.OptionD,
                    CorrectAnswer = command.CorrectAnswer.ToUpper(),
                    QuizId = command.QuizId
                };

                await _unitOfWork.Repository<Question>().AddAsync(question);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllQuestionsCacheKey);

                return await Result<int>.SuccessAsync(question.Id, _localizer["Question Created"]);

            }
            else
            {
                // Update existing question
                var question = await _unitOfWork.Repository<Question>().GetByIdAsync(command.Id);
                if (question == null)
                {
                    _logger.LogDebug("Question non trouvée pour l'id {Id}", command.Id);
                    return await Result<int>.FailAsync(_localizer["Question Not Found!"]);
                }

                // If QuizId changed, ensure new parent quiz exists
                if (question.QuizId != command.QuizId)
                {
                    var quiz = await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Domain.Entities.Cours.Quiz>()
                        .GetByIdAsync(command.QuizId);

                    if (quiz == null)
                    {
                        _logger.LogDebug("Quiz non trouvé pour l'id {Id}", command.QuizId);
                        return await Result<int>.FailAsync(_localizer["Parent Quiz Not Found!"]);
                    }
                }

                // Validate CorrectAnswer
                var validAnswers = new[] { "A", "B", "C", "D" };
                if (string.IsNullOrWhiteSpace(command.CorrectAnswer) || !validAnswers.Contains(command.CorrectAnswer.ToUpper()))
                {
                    return await Result<int>.FailAsync(
                        _localizer["CorrectAnswer must be A, B, C or D!"]);
                }

                // Mettre à jour les champs
                question.Text = command.Text;
                question.OptionA = command.OptionA;
                question.OptionB = command.OptionB;
                question.OptionC = command.OptionC;
                question.OptionD = command.OptionD;
                question.CorrectAnswer = command.CorrectAnswer.ToUpper();
                question.QuizId = command.QuizId;

                // Mise à jour via le repository
                await _unitOfWork.Repository<Question>().UpdateAsync(question);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllQuestionsCacheKey);

                return await Result<int>.SuccessAsync(question.Id, _localizer["Question Updated"]);
            }
        }

    }
}
