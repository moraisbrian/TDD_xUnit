using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DominioTests.Builders;
using CursoOnline.DominioTests.Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTests.Matriculas
{
    public class ConclusaoDaMatriculaTest
    {
        private readonly ConclusaoDaMatricula _conclusaoDaMatricula;
        private readonly Mock<IMatriculaRepositorio> _matriculaRepositorio;

        public ConclusaoDaMatriculaTest()
        {
            _matriculaRepositorio = new Mock<IMatriculaRepositorio>();
            _conclusaoDaMatricula = new ConclusaoDaMatricula(_matriculaRepositorio.Object);
        }

        [Fact]
        public void DeveInformarNotaDoAluno()
        {
            const double notaDoAlunoEsperada = 8;
            var matricula = MatriculaBuilder.Novo().Build();
            _matriculaRepositorio.Setup(r => r.ObterPorId(matricula.Id)).Returns(matricula);

            _conclusaoDaMatricula.Concluir(matricula.Id, notaDoAlunoEsperada);

            Assert.Equal(notaDoAlunoEsperada, matricula.NotaDoAluno);
        }

        [Fact]
        public void DeveNotificarQuandoMatriculaNaoForEncontrada()
        {
            Matricula matriculaInvalida = null;
            const int matriculaIdInvalida = 1;
            const double notaDoAluno = 2;
            _matriculaRepositorio.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns(matriculaInvalida);

            Assert.Throws<ExcecaoDeDominio>(() =>
                _conclusaoDaMatricula.Concluir(matriculaIdInvalida, notaDoAluno))
            .ComMensagem(Resource.MatriculaNaoEncontrada);
        }
    }
}
