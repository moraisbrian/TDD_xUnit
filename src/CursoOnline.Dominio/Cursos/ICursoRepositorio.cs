using CursoOnline.Dominio.Base;

namespace CursoOnline.Dominio.Cursos
{
    public interface ICursoRepositorio : IRepositorio<Curso>
    {
        void Armazenar(Curso curso);
        Curso ObterPeloNome(string nome);
    }
}