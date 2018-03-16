using FluentValidation.Results;
using Projeto.Dominio.Veiculos.Validations;
using System;

namespace Projeto.Dominio.Veiculos.Entities
{
    public class Veiculo
    {
        public Veiculo(string nome, VeiculoTipo tipo, int capacidadeMaxima)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Tipo = tipo;
            CapaxidadeMaxima = capacidadeMaxima;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public VeiculoTipo Tipo { get; private set; }
        public double CapaxidadeMaxima { get; private set; }

        public ValidationResult ValidationResult { get; private set; }

        public bool EhValido()
        {
            ValidationResult = new VeiculoEstaConsistente().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
