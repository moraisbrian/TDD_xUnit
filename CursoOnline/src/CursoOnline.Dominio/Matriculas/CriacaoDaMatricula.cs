using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Matriculas
{
    public class CriacaoDaMatricula
    {
        private IMatriculaRepositorio _matriculaRepositorio;
        private IAlunoRepositorio _alunoRepositorio;
        private ICursoRepositorio _cursoRepositorio;

        public CriacaoDaMatricula(IAlunoRepositorio alunoRepositorio, ICursoRepositorio cursoRepositorio, IMatriculaRepositorio matriculaRepositorio)
        {
            _matriculaRepositorio = matriculaRepositorio;
            _alunoRepositorio = alunoRepositorio;
            _cursoRepositorio = cursoRepositorio;
        }

        public void Criar(MatriculaDto matriculaDto)
        {
            var curso = _cursoRepositorio.ObterPorId(matriculaDto.CursoId);
            var aluno = _alunoRepositorio.ObterPorId(matriculaDto.AlunoId);

            ValidadorDeRegra.Novo()
                .Quando(curso == null, Resource.CursoNaoEncontrado)
                .Quando(aluno == null, Resource.AlunoNaoEncontrado)
                .DispararExcecaoSeExistir();

            var matricula = new Matricula(aluno, curso, matriculaDto.ValorPago);

            _matriculaRepositorio.Adicionar(matricula);
        }
    }

}
