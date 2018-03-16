using Projeto.Dominio.Veiculos.Entities;
using Projeto.Dominio.Veiculos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.Dominio.Teste.Mocks
{
    public class VeiculoMock : IVeiculoRepository
    {
        List<Veiculo> veiculos = new List<Veiculo>();

        public VeiculoMock()
        {
            int tipo = 1;
            for (int i = 0; i < 20; i++)
            {
                veiculos.Add(new Veiculo($"Veiculo {i + 1}", (VeiculoTipo)Enum.ToObject(typeof(VeiculoTipo), i), 60 * i + 1));
                ++tipo;
                if (tipo == 5)
                    tipo = 1;
            }
        }

        public void Adicionar(Veiculo veiculo)
        {
            veiculos.Add(veiculo);
        }

        public void Atualizar(Veiculo veiculo)
        {
            var old = veiculos.FirstOrDefault(t => t.Id == veiculo.Id);
            if (old != null)
            {
                int index = veiculos.IndexOf(old);
                veiculos[index] = veiculo;
            }
            else
                throw new InvalidOperationException($"Veículos não localizada para o Id {veiculo.Id}");
        }

        public Veiculo BuscarPordId(Guid Id)
        {
            return veiculos.FirstOrDefault(t => t.Id == Id);
        }

        public IQueryable<Veiculo> ObterVeiculos()
        {
            return veiculos.AsQueryable();
        }

        public void Remover(Veiculo veiculo)
        {
            var old = veiculos.FirstOrDefault(t => t.Id == veiculo.Id);
            if (old != null)
            {
                int index = veiculos.IndexOf(old);
                veiculos.RemoveAt(index);
            }
            else
                throw new InvalidOperationException($"Veículo não localizada para o Id {veiculo.Id}");
        }
    }
}
