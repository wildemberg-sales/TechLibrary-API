using FluentValidation;
using TechLibrary.Communication.Requests;

namespace TechLibrary.API.UseCases.Books.Create
{
    public class CreateBookValidator : AbstractValidator<RequestCreateBookJson>
    {
        public CreateBookValidator()
        {
            RuleFor(request => request.Title).NotEmpty().WithMessage("O título é obrigatório. ");
            RuleFor(request => request.Author).NotEmpty().WithMessage("O autor é obrigatório. ");
            RuleFor(request => request.Amount).GreaterThanOrEqualTo(1).WithMessage("A quantidade é obrigatória e deve ser maior que 0. ");
            When(request => !string.IsNullOrEmpty(request.Title), () =>
            {
                RuleFor(request => request.Title.Length).GreaterThanOrEqualTo(4).WithMessage("O título deve ter mais que 4 caracteres. ");
            });
            When(request => !string.IsNullOrEmpty(request.Author), () =>
            {
                RuleFor(request => request.Author.Length).GreaterThanOrEqualTo(4).WithMessage("O nome do autor deve ter mais que 4 caracteres. ");
            });

        }
    }
}
