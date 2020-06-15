using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Matriculas
{
    public class Matricula
    {
        public Matricula(Aluno aluno, Curso curso, double valorPago)
        {
            ValidadorDeRegra.Novo()
                .Quando(aluno == null, Resource.AlunoInvalido)
                .Quando(curso == null, Resource.CursoInvalido)
                .Quando(valorPago < 1, Resource.ValorPagoInvalido)
                .Quando(curso != null && valorPago > curso.Valor, Resource.ValorPagoMaiorQueValorDoCurso)
                .DispararExcecaoSeExistir();

            Aluno = aluno;
            Curso = curso;
            ValorPago = valorPago;
            TemDesconto = valorPago < curso.Valor;
        }

        public Aluno Aluno { get; private set; }
        public Curso Curso { get; private set; }
        public double ValorPago { get; private set; }
        public bool TemDesconto { get; private set; }
    }
}
