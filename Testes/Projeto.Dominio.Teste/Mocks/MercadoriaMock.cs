using Projeto.Dominio.Mercadorias.Entity;
using Projeto.Dominio.Mercadorias.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.Dominio.Teste.Mocks
{
    public class MercadoriaMock : IMercadoriaRepository
    {
        List<Mercadoria> mercadorias = new List<Mercadoria>();

        public MercadoriaMock()
        {
            int tipo = 1;
            for (int i = 0; i < 50; i++)
            {
                mercadorias.Add(new Mercadoria((MercadoriaTipo)Enum.ToObject(typeof(MercadoriaTipo), tipo), 50 * (i + 1), i % 2 == 0));
                ++tipo;
                if (tipo == 5)
                    tipo = 1;
            }
        }

        public void Adicionar(Mercadoria mercadoria)
        {
            mercadorias.Add(mercadoria);
        }

        public void Atualizar(Mercadoria mercadoria)
        {
            var old = mercadorias.FirstOrDefault(t => t.Id == mercadoria.Id);
            if (old != null)
            {
                int index = mercadorias.IndexOf(old);
                mercadorias[index] = mercadoria;
            }
            else
                throw new InvalidOperationException($"Mercadoria não localizada para o Id {mercadoria.Id}");
        }

        public Mercadoria BuscarPordId(Guid Id)
        {
            return mercadorias.FirstOrDefault(t => t.Id == Id);
        }

        public IQueryable<Mercadoria> ObterMercadorias()
        {
            return mercadorias.AsQueryable();
        }

        public void Remover(Mercadoria mercadoria)
        {
            var old = mercadorias.FirstOrDefault(t => t.Id == mercadoria.Id);
            if (old != null)
            {
                int index = mercadorias.IndexOf(old);
                mercadorias.RemoveAt(index);
            }
            else
                throw new InvalidOperationException($"Mercadoria não localizada para o Id {mercadoria.Id}");
        }
    }
}
