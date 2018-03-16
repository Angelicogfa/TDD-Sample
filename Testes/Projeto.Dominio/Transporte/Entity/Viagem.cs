using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Projeto.Dominio.Mercadorias.Entity;
using Projeto.Dominio.Transporte.Validations;
using Projeto.Dominio.Veiculos.Entities;

namespace Projeto.Dominio.Transporte.Entity
{
    public class Viagem
    {
        private List<Mercadoria> mercadorias = new List<Mercadoria>();

        public Viagem(Veiculo veiculo, string origem, string destino)
        {
            Id = Guid.NewGuid();
            TransportadorId = veiculo.Id;
            CapacidadeMaxima = veiculo.CapaxidadeMaxima;
            Origem = origem;
            Destino = destino;
        }

        public Guid Id { get; private set; }
        public Guid TransportadorId { get; private set; }
        public string Origem { get; private set; }
        public string Destino { get; private set; }
        public double CapacidadeMaxima { get; private set; }
        public double CapacidadeAtual { get { return mercadorias.Sum(t => t.Peso); } }

        public ValidationResult ValidationResult { get; private set; }

        public void AdicionarMercadoria(Mercadoria mercadoria)
        {
            mercadorias.Add(mercadoria);
        }

        public bool EhValida()
        {
            ValidationResult = new ViagemEstaConsistente().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
