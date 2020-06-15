using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;

namespace CursoOnline.Tests.Dominio.Builders
{
    public class MatriculaBuilder
    {
        //private int Id;
        private Aluno Aluno;
        private Curso Curso;
        private double ValorPago;

        public static MatriculaBuilder Novo()
        {
            var curso = CursoBuilder.Novo().Build();

            return new MatriculaBuilder
            {
                Aluno = AlunoBuilder.Novo().Build(),
                Curso = curso,
                ValorPago = curso.Valor
            };
        }

        public MatriculaBuilder ComCurso(Curso curso)
        {
            Curso = curso;
            return this;
        }

        public MatriculaBuilder ComAluno(Aluno aluno)
        {
            Aluno = aluno;
            return this;
        }

        public MatriculaBuilder ComValorPago(double valorPago)
        {
            ValorPago = valorPago;
            return this;
        }

        //public MatriculaBuilder ComId(int id)
        //{
        //    Id = id;
        //    return this;
        //}

        public Matricula Build()
        {
            var matricula = new Matricula(Aluno, Curso, ValorPago);

            //if (Id > 0)
            //{
            //    var propertyInfo = matricula.GetType().GetProperty("Id");
            //    propertyInfo.SetValue(matricula, Convert.ChangeType(Id, propertyInfo.PropertyType), null);
            //}

            return matricula;
        }

    }
}