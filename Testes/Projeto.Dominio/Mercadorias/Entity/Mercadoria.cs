using FluentValidation.Results;
using Projeto.Dominio.Mercadorias.Validations;
using System;

namespace Projeto.Dominio.Mercadorias.Entity
{
    public class Mercadoria
    {
        public Mercadoria(MercadoriaTipo tipo, double peso, bool fragil)
        {
            Id = Guid.NewGuid();
            Tipo = tipo;
            Peso = peso;
            Fragil = fragil;
        }

        public Guid Id { get; private set; }
        public MercadoriaTipo Tipo { get; private set; }
        public double Peso { get; private set; }
        public bool Fragil { get; private set; }

        public ValidationResult ValidationResult { get; private set; }

        public bool EhValido()
        {
            ValidationResult = new MercadoriaEstaConsistente().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
