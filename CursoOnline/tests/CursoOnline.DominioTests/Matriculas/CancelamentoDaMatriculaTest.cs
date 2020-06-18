using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DominioTests.Builders;
using CursoOnline.DominioTests.Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTests.Matriculas
{
    public class CancelamentoDaMatriculaTest
    {
        private readonly CancelamentoDaMatricula _cancelamentoDaMatricula;
        private readonly Mock<IMatriculaRepositorio> _matriculaRepositorio;

        public CancelamentoDaMatriculaTest()
        {
            _matriculaRepositorio = new Mock<IMatriculaRepositorio>();
            _cancelamentoDaMatricula = new CancelamentoDaMatricula(_matriculaRepositorio.Object);
        }

        [Fact]
        public void DeveCancelarUmaMatricula()
        {
            var matricula = MatriculaBuilder.Novo().Build();
            _matriculaRepositorio.Setup(r => r.ObterPorId(matricula.Id)).Returns(matricula);

            _cancelamentoDaMatricula.Cancelar(matricula.Id);

            Assert.True(matricula.Cancelada);
        }

        [Fact]
        public void DeveNotificarQuandoMatriculaNaoForEncontrada()
        {
            Matricula matriculaInvalida = null;
            const int matriculaIdInvalida = 1;
            _matriculaRepositorio.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns(matriculaInvalida);

            Assert.Throws<ExcecaoDeDominio>(() =>
                _cancelamentoDaMatricula.Cancelar(matriculaIdInvalida))
            .ComMensagem(Resource.MatriculaNaoEncontrada);
        }
    }
}
