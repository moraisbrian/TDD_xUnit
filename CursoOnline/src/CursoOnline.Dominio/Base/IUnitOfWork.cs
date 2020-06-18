using System.Threading.Tasks;

namespace CursoOnline.Dominio.Base
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}