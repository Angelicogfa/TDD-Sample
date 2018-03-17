using Moq;
using Projeto.Dominio.Mercadorias.Entity;
using Projeto.Dominio.Teste.Mocks;
using Projeto.Dominio.Transporte.Repository;
using Projeto.Dominio.Veiculos.Entities;
using System.Collections.Generic;
using Xunit;
using Projeto.Dominio.Veiculos.Repository;
using System.Linq;
using Projeto.Dominio.Transporte.Commands;
using System;
using Projeto.Dominio.Transporte.Sagas;
using System.Threading;

namespace Projeto.Dominio.Teste
{
    public class ViagemSagaTest
    {
        MercadoriaMock mercadoriaRepository = new MercadoriaMock();
        VeiculoMock veiculoRepository = new VeiculoMock();
        Mock<IViagemRepository> viagemRepository = new Mock<IViagemRepository>();



        [Fact]
        [Trait("Saga", "ViagemSaga")]
        public void NovaViagemCommandDeveGerarViagem()
        {
            //Arrange
            Veiculo veiculo = veiculoRepository.ObterVeiculos().BuscarPorCapacidadeMaxima(4000).OrderBy(t => t.CapaxidadeMaxima).LastOrDefault();
            IEnumerable<Mercadoria> mercadorias = mercadoriaRepository.ObterMercadorias().Take(5);

            NovaViagemCommand novaViagemCommand = new NovaViagemCommand(veiculo.Id, "SAO PAULO", "RIO DE JANEIRO", mercadorias.Select(t => t.Id));
            ViagemSaga saga = new ViagemSaga(mercadoriaRepository, veiculoRepository, viagemRepository.Object);

            //Act
            CancellationToken token = new CancellationToken();
            Guid viagemId = saga.Handle(novaViagemCommand, token).Result;

            //Assert
            Assert.NotEqual(Guid.Empty, viagemId);
            Assert.False(saga.ExisteErros);
            Assert.False(saga.ExisteAlerta);
        }

        [Fact]
        [Trait("Saga", "ViagemSaga")]
        public void NovaViagemCommandDeveGerarViagemCommAlerta()
        {
            //Arrange
            Veiculo veiculo = veiculoRepository.ObterVeiculos().BuscarPorCapacidadeMaxima(4000).OrderBy(t => t.CapaxidadeMaxima).LastOrDefault();
            List<Mercadoria> mercadorias = mercadoriaRepository.ObterMercadorias().Take(5).ToList();
            mercadorias.Add(new Mercadoria(MercadoriaTipo.Tambor, 20, false));


            NovaViagemCommand novaViagemCommand = new NovaViagemCommand(veiculo.Id, "SAO PAULO", "RIO DE JANEIRO", mercadorias.Select(t => t.Id));
            ViagemSaga saga = new ViagemSaga(mercadoriaRepository, veiculoRepository, viagemRepository.Object);

            //Act
            CancellationToken token = new CancellationToken();
            Guid viagemId = saga.Handle(novaViagemCommand, token).Result;

            //Assert
            Assert.NotEqual(Guid.Empty, viagemId);
            Assert.False(saga.ExisteErros);
            Assert.True(saga.ExisteAlerta);
        }

        [Fact]
        [Trait("Saga", "ViagemSaga")]
        public void NovaViagemCommandNaoDeveGerarViagem()
        {
            //Arrange
            Veiculo veiculo = new Veiculo("HERCULES", VeiculoTipo.Aviao, 10000);
            List<Mercadoria> mercadorias = mercadoriaRepository.ObterMercadorias().Take(5).ToList();

            NovaViagemCommand novaViagemCommand = new NovaViagemCommand(veiculo.Id, "SAO PAULO", "RIO DE JANEIRO", mercadorias.Select(t => t.Id));
            ViagemSaga saga = new ViagemSaga(mercadoriaRepository, veiculoRepository, viagemRepository.Object);

            //Act
            CancellationToken token = new CancellationToken();
            Guid viagemId = saga.Handle(novaViagemCommand, token).Result;

            //Assert
            Assert.Equal(Guid.Empty, viagemId);
            Assert.True(saga.ExisteErros);
            Assert.False(saga.ExisteAlerta);
        }
    }
}
