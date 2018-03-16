using FluentValidation;
using Projeto.Dominio.Veiculos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Dominio.Veiculos.Validations
{
    public class VeiculoEstaConsistente : AbstractValidator<Veiculo>
    {
        public VeiculoEstaConsistente()
        {
            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("Id não informado");

            RuleFor(t => t.Nome)
                .NotEmpty().WithMessage("Nome não informado")
                .Length(2, 20).WithMessage("Nome deve ter entre 2 e 20 caractéres");

            RuleFor(t => t.Tipo)
                .IsInEnum().WithMessage("Tipo inválido");

            RuleFor(t => t.CapaxidadeMaxima)
                .NotEmpty().WithMessage("Quantidade não informada")
                .GreaterThan(0).WithMessage("Quantidade deve ser maior que zero");
        }
    }
}
