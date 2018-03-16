using Projeto.Dominio.Mercadorias.Entity;
using System;
using System.Linq;
using Xunit;

namespace Projeto.Dominio.Teste
{
    public class MercadoriaTest
    {
        Mercadoria mercadoria = null;

        [Fact]
        [Trait("Entity", "Mercadoria")]
        public void DeveRetornarMercadoriaValida()
        {
            //Arrange
            mercadoria = new Mercadoria(MercadoriaTipo.Caixa, 15, true);

            //Act
            bool valido = mercadoria.EhValido();

            //Assert
            Assert.True(valido);
            Assert.NotEqual(Guid.Empty, mercadoria.Id);
            Assert.Equal(MercadoriaTipo.Caixa, mercadoria.Tipo);
            Assert.Equal(15, mercadoria.Peso);
            Assert.True(mercadoria.Fragil);
        }

        [Fact]
        [Trait("Entity", "Mercadoria")]
        public void DeveRetornarMercadoriaInvalidaSeTipoNaoInformado()
        {
            //Arrange
            mercadoria = new Mercadoria(0, 15, true);

            //Act
            bool valido = mercadoria.EhValido();

            //Assert
            Assert.False(valido);
            Assert.True(mercadoria.ValidationResult.Errors.All(t => t.PropertyName == nameof(mercadoria.Tipo)));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [Trait("Entity", "Mercadoria")]
        public void DeveRetornarMercadoriaInvalidaSeQuantidadeForaRange(double peso)
        {
            //Arrange
            mercadoria = new Mercadoria(MercadoriaTipo.Container, peso, true);

            //Act
            bool valido = mercadoria.EhValido();

            //Assert
            Assert.False(valido);
            Assert.True(mercadoria.ValidationResult.Errors.All(t => t.PropertyName == nameof(mercadoria.Peso)));
        }
    }
}
