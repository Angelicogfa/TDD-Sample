using Projeto.Dominio.Veiculos.Entities;
using System;
using System.Linq;

namespace Projeto.Dominio.Veiculos.Repository
{
    public interface IVeiculoRepository
    {
        void Adicionar(Veiculo veiculo);
        void Atualizar(Veiculo veiculo);
        void Remover(Veiculo veiculo);
        Veiculo BuscarPordId(Guid Id);
        IQueryable<Veiculo> ObterVeiculos();
    }
}
