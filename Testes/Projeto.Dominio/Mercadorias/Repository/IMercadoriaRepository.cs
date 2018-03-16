using Projeto.Dominio.Mercadorias.Entity;
using System;
using System.Linq;

namespace Projeto.Dominio.Mercadorias.Repository
{
    public interface IMercadoriaRepository
    {
        void Adicionar(Mercadoria mercadoria);
        void Atualizar(Mercadoria mercadoria);
        void Remover(Mercadoria mercadoria);
        Mercadoria BuscarPordId(Guid Id);
        IQueryable<Mercadoria> ObterMercadorias();
    }
}
