using Projeto.Dominio.Veiculos.Entities;
using System;
using System.Linq;
using Xunit;

namespace Projeto.Dominio.Teste
{
    public class VeiculosTest
    {
        Veiculo veiculo = null;

        [Fact]
        [Trait("Entity", "Veiculo")]
        public void DeveRetornarUmVeiculoValido()
        {
            //Arrange
            veiculo = new Veiculo("SANTA MARIA", VeiculoTipo.Navio, 1000000);

            //Act
            bool valido = veiculo.EhValido();

            //Assert
            Assert.True(valido);
            Assert.NotEqual(Guid.Empty, veiculo.Id);
            Assert.Equal("SANTA MARIA", veiculo.Nome);
            Assert.Equal(VeiculoTipo.Navio, veiculo.Tipo);
            Assert.Equal(1000000, veiculo.CapaxidadeMaxima);
        }

        [Theory()]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("A")]
        [InlineData("AAAAAAAAAAAAAAAAAAAAA")]
        [Trait("Entity", "Veiculo")]
        public void DeveRetornarUmVeiculoInvalidoPorNome(string nome)
        {
            //Arrange
            veiculo = new Veiculo(nome, VeiculoTipo.Caminhao, 100000);

            //Act
            bool valido = veiculo.EhValido();

            //Assert
            Assert.False(valido);
            Assert.True(veiculo.ValidationResult.Errors.All(t=> t.PropertyName == nameof(veiculo.Nome)));
        }

        [Fact]
        [Trait("Entity", "Veiculo")]
        public void DeveRetornarUmVeiculoInvalidoPoTipo()
        {
            //Arrange
            veiculo = new Veiculo("TREM", 0, 100000);

            //Act
            bool valido = veiculo.EhValido();

            //Assert
            Assert.False(valido);
            Assert.Single(veiculo.ValidationResult.Errors);
            Assert.True(veiculo.ValidationResult.Errors.All(t => t.PropertyName == nameof(veiculo.Tipo)));
        }

        [Fact]
        [Trait("Entity", "Veiculo")]
        public void DeveRetornarUmVeiculoInvalidoPorQuantidade()
        {
            //Arrange
            veiculo = new Veiculo("TREM", VeiculoTipo.Aviao, 0);

            //Act
            bool valido = veiculo.EhValido();

            //Assert
            Assert.False(valido);
            Assert.True(veiculo.ValidationResult.Errors.All(t => t.PropertyName == nameof(veiculo.CapaxidadeMaxima)));
        }
    }
}
