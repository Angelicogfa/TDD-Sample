using MediatR;
using Projeto.Dominio.Mercadorias.Repository;
using Projeto.Dominio.Transporte.Commands;
using Projeto.Dominio.Transporte.Repository;
using Projeto.Dominio.Veiculos.Entities;
using Projeto.Dominio.Veiculos.Repository;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Projeto.Dominio.Transporte.Sagas
{
    public class ViagemSaga : ISagaNotification,
        IRequestHandler<NovaViagemCommand, Guid>
    {
        readonly IMercadoriaRepository mercadoriaRepository;
        readonly IVeiculoRepository veiculoRepository;
        readonly IViagemRepository viagemRepository;

        private List<string> erros = new List<string>();
        private List<string> alertas = new List<string>();

        public ViagemSaga(IMercadoriaRepository mercadoriaRepository, IVeiculoRepository veiculoRepository, IViagemRepository viagemRepository)
        {
            this.veiculoRepository = veiculoRepository;
            this.mercadoriaRepository = mercadoriaRepository;
            this.viagemRepository = viagemRepository;
        }

        public bool ExisteErros => erros.Count > 0;
        public bool ExisteAlerta => alertas.Count > 0;

        public Task<Guid> Handle(NovaViagemCommand request, CancellationToken cancellationToken)
        {
            erros.Clear();
            alertas.Clear();

            Veiculo veiculo = veiculoRepository.BuscarPordId(request.VeiculoId);
            if (veiculo == null)
            {
                erros.Add($"Veiculo não localizado para o Id {veiculo.Id}");
                return Task.FromResult(Guid.Empty);
            }

            var viagem = veiculo.NovaViagem(request.Origem, request.Destino);
            foreach (var mercadoriaId in request.Mercadorias)
            {
                var mercadoria = mercadoriaRepository.BuscarPordId(mercadoriaId);
                if (mercadoria == null)
                {
                    alertas.Add($"Mercadoria não localizada para o Id {mercadoria.Id}");
                    continue;
                }

                viagem.AdicionarMercadoria(mercadoria);
            }

            if (!viagem.EhValida())
            {
                foreach (var erro in viagem.ValidationResult.Errors)
                    erros.Add($"O campo {erro.PropertyName} esta invalido: {erro.ErrorMessage}");
                return Task.FromResult(Guid.Empty);
            }

            viagemRepository.Adicionar(viagem);
            return Task.FromResult(viagem.Id);
        }

        public IEnumerable<string> ObterAlertas()
        {
            return alertas;
        }

        public IEnumerable<string> ObterErros()
        {
            return erros;
        }
    }
}
