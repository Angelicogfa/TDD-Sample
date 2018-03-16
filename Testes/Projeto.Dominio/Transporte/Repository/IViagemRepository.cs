using Projeto.Dominio.Transporte.Entity;
using System;
using System.Linq;

namespace Projeto.Dominio.Transporte.Repository
{
    public interface IViagemRepository
    {
        void Adicionar(Viagem viagem);
        void Atualizar(Viagem viagem);
        void Remover(Viagem viagem);
        IQueryable<Viagem> BuscarViagens();
        Viagem BuscarPordId(Guid Id);
    }
}
