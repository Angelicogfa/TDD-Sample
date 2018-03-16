using FluentValidation;
using Projeto.Dominio.Transporte.Entity;

namespace Projeto.Dominio.Transporte.Validations
{
    public class ViagemEstaConsistente : AbstractValidator<Viagem>
    {
        public ViagemEstaConsistente()
        {
            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("Id não informado");

            RuleFor(t => t.TransportadorId)
                .NotEmpty().WithMessage("Transportador não informado");

            RuleFor(t => t.CapacidadeMaxima)
                .NotEmpty().WithMessage("Capacidade máxima não informada");

            RuleFor(t => t.Origem)
                .NotEmpty().WithMessage("Origem não informada")
                .Length(2, 20).WithMessage("Origem deve ter entre 2 e 20 caractéres");

            RuleFor(t => t.Destino)
                .NotEmpty().WithMessage("Destino não informada")
                .Length(2, 20).WithMessage("Destino deve ter entre 2 e 20 caractéres");

            RuleFor(t => t.CapacidadeAtual)
                .Custom((prop, ctx) => 
                {
                    Viagem viagem = ctx.ParentContext.InstanceToValidate as Viagem;
                    if (prop > viagem.CapacidadeMaxima)
                        ctx.AddFailure("Capacidade atual está acima da máxima permitida");
                });
        }
    }
}
