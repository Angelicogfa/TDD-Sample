using Projeto.Dominio.Veiculos.Entities;
using System.Linq;

namespace Projeto.Dominio.Veiculos.Repository
{
    public static class VeiculoRepositoryExtension
    {
        public static IQueryable<Veiculo> BuscarPorTipo(this IQueryable<Veiculo> query, VeiculoTipo tipo)
        {
            return query.Where(t => t.Tipo == tipo);
        }

        public static IQueryable<Veiculo> BuscarPorCapacidadeMaxima(this IQueryable<Veiculo> query, double AteCapacidadeMaxima)
        {
            return query.Where(t => t.CapaxidadeMaxima <= AteCapacidadeMaxima);
        }
    }
}
