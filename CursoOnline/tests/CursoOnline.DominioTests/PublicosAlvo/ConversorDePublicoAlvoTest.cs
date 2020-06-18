using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DominioTests.Util;
using Xunit;

namespace CursoOnline.DominioTests.PublicosAlvo
{
    public class ConversorDePublicoAlvoTest
    {
        private readonly ConversorDePublicoAlvo _conversor = new ConversorDePublicoAlvo();

        [Theory]
        [InlineData(EPublicoAlvo.Universitario, "Universitario")]
        [InlineData(EPublicoAlvo.Estudante, "Estudante")]
        [InlineData(EPublicoAlvo.Empreendedor, "Empreendedor")]
        [InlineData(EPublicoAlvo.Empregado, "Empregado")]
        public void DeveConverterPublicoAlvo(EPublicoAlvo publicoAlvoEsperado, string publicoAlvoString)
        {
            var publicoAlvoConvertido = _conversor.Converter(publicoAlvoString);

            Assert.Equal(publicoAlvoEsperado, publicoAlvoConvertido);
        }

        [Fact]
        public void NaoDeveConverterQuandoPublicoAlvoForInvalido()
        {
            var publicoAlvoInvalido = "Inválido";

            Assert.Throws<ExcecaoDeDominio>(() => _conversor.Converter(publicoAlvoInvalido))
                .ComMensagem(Resource.PublicoAlvoInvalido);
        }
    }
}
