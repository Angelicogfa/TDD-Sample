using Projeto.Dominio.Mercadorias.Entity;
using System.Linq;

namespace Projeto.Dominio.Mercadorias.Repository
{
    public static class MercadoriaRepositoryExtension
    {
        public static IQueryable<Mercadoria> BuscarPorTipo(this IQueryable<Mercadoria> query, MercadoriaTipo tipo)
        {
            return query.Where(t => t.Tipo == tipo);
        }

        public static IQueryable<Mercadoria> BuscarFrageis(this IQueryable<Mercadoria> query, bool fragil = true)
        {
            return query.Where(t => t.Fragil == fragil);
        }
    }
}
