using CursoOnline.Dominio.Cursos;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        [Fact]
        public void DeveAdicionarCurso()
        {
            var cursoDto = new CursoDto
            {
                Nome = "Curso A",
                Descricao = "Descricao",
                CargaHoraria = 80,
                PublicoAlvoId = 1,
                Valor = 850.00
            };

            var mock = new Mock<ICursoRepositorio>();
            var armazenadorDeCurso = new ArmazenadorDeCurso(mock.Object);
            armazenadorDeCurso.Armazenar(cursoDto);

            mock.Verify(r => r.Armazenar(It.IsAny<Curso>()));
        }
    }

    public interface ICursoRepositorio
    {
        void Armazenar(Curso curso);
    }

    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var curso = new Curso(
                cursoDto.Nome,
                cursoDto.Descricao,
                cursoDto.CargaHoraria,
                EPublicoAlvo.Estudante,
                cursoDto.Valor
            );

            _cursoRepositorio.Armazenar(curso);
        }
    }

    public class CursoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public int PublicoAlvoId { get; set; }
        public double Valor { get; set; }
    }
}