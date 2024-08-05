using FluentValidation;

namespace Atividade01.Dominio.Validadores.Contato
{
    public class ValidarAtualizarContato : AbstractValidator<ViewModel.Contato>
    {
        public ValidarAtualizarContato()
        {
            RuleFor(x => x.Guid).NotNull().WithMessage("O Guid não pode ser nulo")
                               .NotEmpty().WithMessage("O Guid não pode ser vazio!");
            RuleFor(x => x.Nome).NotNull().WithMessage("O Nome não pode ser nulo")
                                .NotEmpty().WithMessage("O Nome não pode ser vazio!");
            RuleFor(x => x.Estado).NotNull().WithMessage("O Estado não pode ser nulo")
                                .NotEmpty().WithMessage("O Estado não pode ser vazio!");
            RuleFor(x => x.Municipio).NotNull().WithMessage("O Municipio não pode ser nulo")
                                .NotEmpty().WithMessage("O Municipio não pode ser vazio!");
            RuleFor(x => x.DDD).NotNull().WithMessage("O DDD não pode ser nulo")
                               .NotEmpty().WithMessage("O DDD não pode ser vazio!")
                               .MaximumLength(2).WithMessage("O DDD não pode ter mais de 2 digitos")
                               .MinimumLength(2).WithMessage("O DDD não pode ter menos de 2 digitos");
            RuleFor(x => x.Telefone).NotNull().WithMessage("O telefone não pode ser nulo")
                               .NotEmpty().WithMessage("O telfone não pode ser vazio!")
                               .MaximumLength(9).WithMessage("O telefone não pode ter mais de 9 digitos")
                               .MinimumLength(8).WithMessage("O telefone não pode ter menos de 8 digitos");
            RuleFor(x => x.Email).NotNull().WithMessage("O e-mail não pode ser nulo")
                                .NotEmpty().WithMessage("O e-mail não pode ser vazio!")
                                .Matches(@"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$").WithMessage("O e-mail está com formato incorreto");
        }


        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await base.ValidateAsync(ValidationContext<ViewModel.Contato>.CreateWithOptions((ViewModel.Contato)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };

    }
}
