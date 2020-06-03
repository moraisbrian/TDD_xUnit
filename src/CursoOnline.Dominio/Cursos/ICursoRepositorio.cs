namespace CursoOnline.Dominio.Cursos
{
    public interface ICursoRepositorio
    {
        void Armazenar(Curso curso);
        Curso ObterPeloNome(string nome);
    }
}