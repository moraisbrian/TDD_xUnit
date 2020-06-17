using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Tests.Dominio.Builders;
using Xunit;
using ExpectedObjects;
using CursoOnline.Dominio.Base;
using CursoOnline.Tests.Dominio.Util;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.PublicosAlvo;

namespace CursoOnline.Tests.Dominio.Matriculas
{
    public class MatriculaTest
    {
        [Fact]
        public void DeveCriarMatricula()
        {
            var curso = CursoBuilder.Novo().Build();

            var matriculaEsperada = new
            {
                Aluno = AlunoBuilder.Novo().Build(),
                Curso = curso,
                ValorPago = curso.Valor
            };

            var matricula = new Matricula(
                matriculaEsperada.Aluno, 
                matriculaEsperada.Curso, 
                matriculaEsperada.ValorPago
            );

            matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemAluno()
        {
            Aluno alunoInvalido = null;

            Assert.Throws<ExcecaoDeDominio>(() => 
                MatriculaBuilder.Novo().ComAluno(alunoInvalido).Build())
            .ComMensagem(Resource.AlunoInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemCurso()
        {
            Curso cursoInvalido = null;

            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo().ComCurso(cursoInvalido).Build())
            .ComMensagem(Resource.CursoInvalido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void NaoDeveCriarMatriculaComValorPagoInvalido(double valorPago)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo().ComValorPago(valorPago).Build())
            .ComMensagem(Resource.ValorPagoInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorMaiorQueValorDoCurso()
        {
            var curso = CursoBuilder.Novo().ComValor(100).Build();
            double valorPagoMaiorQueCurso = curso.Valor + 1;

            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(valorPagoMaiorQueCurso).Build())
            .ComMensagem(Resource.ValorPagoMaiorQueValorDoCurso);
        }

        [Fact]
        public void DeveIndicarQueHouveDescontoNaMatricula()
        {
            var curso = CursoBuilder.Novo().ComValor(100).Build();
            double valorComDesconto = curso.Valor - 1;

            var matricula = MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(valorComDesconto).Build();

            Assert.True(matricula.TemDesconto);
        }

        [Fact]
        public void NaoDevePublicoAlvoDeAlunoECursoSeremDiferentes()
        {
            var aluno = AlunoBuilder.Novo().ComPublicoAlvo(EPublicoAlvo.Universitario).Build();
            var curso = CursoBuilder.Novo().ComPublicoAlvo(EPublicoAlvo.Empreendedor).Build();

            Assert.Throws<ExcecaoDeDominio>(() => 
                MatriculaBuilder.Novo().ComAluno(aluno).ComCurso(curso).Build())
            .ComMensagem(Resource.PublicosAlvoDiferente);
        }

        [Fact]
        public void DeveInformarANotaDoAlunoParaMatricula()
        {
            const double notaDoAlunoEsperada = 9.5;
            var matricula = MatriculaBuilder.Novo().Build();

            matricula.InformarNota(notaDoAlunoEsperada);

            Assert.Equal(notaDoAlunoEsperada, matricula.NotaDoAluno);
        }

        [Fact]
        public void DeveIndicarQueCursoFoiConcluido()
        {
            const double notaDoAlunoEsperada = 9.5;
            var matricula = MatriculaBuilder.Novo().Build();

            matricula.InformarNota(notaDoAlunoEsperada);

            Assert.True(matricula.CursoConcluido);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        [InlineData(10.1)]
        public void NaoDeveInformarANotaDoAlunoInvalidaParaMatricula(double notaInvalida)
        {
            var matricula = MatriculaBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() =>
                matricula.InformarNota(notaInvalida))
            .ComMensagem(Resource.NotaInvalida);
        }
    }
}
