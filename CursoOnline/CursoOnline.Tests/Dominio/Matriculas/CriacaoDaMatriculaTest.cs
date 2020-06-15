using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Tests.Dominio.Builders;
using CursoOnline.Tests.Dominio.Util;
using Moq;
using Xunit;

namespace CursoOnline.Tests.Dominio.Matriculas
{
    public class CriacaoDaMatriculaTest
    {
        private readonly Mock<IAlunoRepositorio> _alunoRepositorio;
        private readonly Mock<ICursoRepositorio> _cursoRepositorio;
        private readonly Mock<IMatriculaRepositorio> _matriculaRepositorio;
        private readonly Aluno _aluno;
        private readonly MatriculaDto _matriculaDto;
        private readonly CriacaoDaMatricula _criacaoDeMatricula;
        private readonly Curso _curso;

        public CriacaoDaMatriculaTest()
        {
            _alunoRepositorio = new Mock<IAlunoRepositorio>();
            _cursoRepositorio = new Mock<ICursoRepositorio>();
            _matriculaRepositorio = new Mock<IMatriculaRepositorio>();

            _aluno = AlunoBuilder.Novo().ComId(26).ComPublicoAlvo(EPublicoAlvo.Universitario).Build();
            _alunoRepositorio.Setup(r => r.ObterPorId(_aluno.Id)).Returns(_aluno);

            _curso = CursoBuilder.Novo().ComId(68).ComPublicoAlvo(EPublicoAlvo.Universitario).Build();
            _cursoRepositorio.Setup(r => r.ObterPorId(_curso.Id)).Returns(_curso);

            _matriculaDto = new MatriculaDto { AlunoId = _aluno.Id, CursoId = _curso.Id, ValorPago = _curso.Valor };

            _criacaoDeMatricula = new CriacaoDaMatricula(_alunoRepositorio.Object, _cursoRepositorio.Object, _matriculaRepositorio.Object);
        }

        [Fact]
        public void DeveNotificarQuandoCursoNaoForEncontrado()
        {
            Curso cursoIvalido = null;
            _cursoRepositorio.Setup(r => r.ObterPorId(_matriculaDto.CursoId)).Returns(cursoIvalido);

            Assert.Throws<ExcecaoDeDominio>(() => _criacaoDeMatricula.Criar(_matriculaDto))
                .ComMensagem(Resource.CursoNaoEncontrado);
        }

        [Fact]
        public void DeveNotificarQuandoAlunoNaoForEncontrado()
        {
            Aluno alunoIvalido = null;
            _alunoRepositorio.Setup(r => r.ObterPorId(_matriculaDto.AlunoId)).Returns(alunoIvalido);

            Assert.Throws<ExcecaoDeDominio>(() => _criacaoDeMatricula.Criar(_matriculaDto))
                .ComMensagem(Resource.AlunoNaoEncontrado);
        }

        [Fact]
        public void DeveAdicionarMatricula()
        {
            _criacaoDeMatricula.Criar(_matriculaDto);

            _matriculaRepositorio.Verify(r => r.Adicionar(It.Is<Matricula>(
                m => m.Aluno == _aluno && m.Curso == _curso)));
        }
    }
}
