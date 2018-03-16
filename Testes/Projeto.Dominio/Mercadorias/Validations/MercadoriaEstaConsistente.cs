using FluentValidation;
using Projeto.Dominio.Mercadorias.Entity;

namespace Projeto.Dominio.Mercadorias.Validations
{
    public class MercadoriaEstaConsistente : AbstractValidator<Mercadoria>
    {
        public MercadoriaEstaConsistente()
        {
            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("Id não informado");

            RuleFor(t => t.Tipo)
                .IsInEnum().WithMessage("Tipo não informado");

            RuleFor(t => t.Peso)
                .NotEmpty().WithMessage("Peso não informado")
                 .GreaterThan(0).WithMessage("Peso deve ser maior que zero"); ;
        }
    }
}
