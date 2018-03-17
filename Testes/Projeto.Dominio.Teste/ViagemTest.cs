using Projeto.Dominio.Mercadorias.Entity;
using Projeto.Dominio.Transporte.Entity;
using Projeto.Dominio.Veiculos.Entities;
using System;
using System.Linq;
using Xunit;

namespace Projeto.Dominio.Teste
{
    public class ViagemTest
    {
        Viagem viagem;

        [Fact]
        [Trait("Entity","Viagem")]
        public void ObterViagemValida()
        {
            //Arrange
            Veiculo veiculo = new Veiculo("AR LINE", VeiculoTipo.Aviao, 60000);

            //Act
            viagem = veiculo.NovaViagem("Canada", "Brasil");
            viagem.AdicionarMercadoria(new Mercadoria(MercadoriaTipo.Tambor, 600, false));
            viagem.AdicionarMercadoria(new Mercadoria(MercadoriaTipo.Tambor, 600, false));
            viagem.AdicionarMercadoria(new Mercadoria(MercadoriaTipo.Tambor, 600, false));

            viagem.AdicionarMercadoria(new Mercadoria(MercadoriaTipo.Container, 4000, false));

            bool valida = viagem.EhValida();

            //Assert
            Assert.True(valida);
            Assert.NotEqual(Guid.Empty, viagem.Id);
            Assert.Equal(veiculo.Id, viagem.TransportadorId);
            Assert.Equal(veiculo.CapaxidadeMaxima, viagem.CapacidadeMaxima);
            Assert.Equal(5800, viagem.CapacidadeAtual);
            Assert.Equal("Canada", viagem.Origem);
            Assert.Equal("Brasil", viagem.Destino);
        }

        [Fact]
        [Trait("Entity", "Viagem")]
        public void ObterViagemInvalidaPorQuantidadeAcimaDoSuportavel()
        {
            //Arrange
            Veiculo veiculo = new Veiculo("AR LINE", VeiculoTipo.Caminhao, 1000);

            //Act
            viagem = veiculo.NovaViagem("Canada", "Brasil");
            viagem.AdicionarMercadoria(new Mercadoria(MercadoriaTipo.Tambor, 600, false));
            viagem.AdicionarMercadoria(new Mercadoria(MercadoriaTipo.Tambor, 600, false));
            viagem.AdicionarMercadoria(new Mercadoria(MercadoriaTipo.Tambor, 600, false));

            bool valida = viagem.EhValida();

            //Assert
            Assert.False(valida);
            Assert.True(viagem.ValidationResult.Errors.All(t => t.PropertyName == nameof(viagem.CapacidadeAtual)));
        }

        [Fact]
        [Trait("Entity", "Viagem")]
        public void ObterViagemInvalidaPorQuantidadeMercadoriaZero()
        {
            //Arrange
            Veiculo veiculo = new Veiculo("AR LINE", VeiculoTipo.Caminhao, 1000);

            //Act
            viagem = veiculo.NovaViagem("Canada", "Brasil");

            bool valida = viagem.EhValida();

            //Assert
            Assert.False(valida);
            Assert.True(viagem.ValidationResult.Errors.All(t => t.PropertyName == nameof(viagem.CapacidadeAtual)));
        }
    }
}
