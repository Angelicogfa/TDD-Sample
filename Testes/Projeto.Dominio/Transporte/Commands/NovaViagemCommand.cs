using MediatR;
using System;
using System.Collections.Generic;

namespace Projeto.Dominio.Transporte.Commands
{
    public class NovaViagemCommand : IRequest<Guid>
    {
        public NovaViagemCommand(Guid veiculoId, string origem, string destino, IEnumerable<Guid> mercadorias)
        {
            VeiculoId = veiculoId;
            Origem = origem;
            Destino = destino;
            Mercadorias = mercadorias;
        }

        public Guid VeiculoId { get; private set; }
        public string Origem { get; private set; }
        public string Destino { get; private set; }

        public IEnumerable<Guid> Mercadorias { get; private set; }
    }
}
