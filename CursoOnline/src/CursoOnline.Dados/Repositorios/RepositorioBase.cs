using System.Collections.Generic;
using System.Linq;
using CursoOnline.Dominio.Base;

namespace CursoOnline.Dados.Repositorios
{
    public class RepositorioBase<TEntidade> : IRepositorio<TEntidade> where TEntidade : Entidade
    {
        protected readonly ApplicationDbContext Context;

        public RepositorioBase(ApplicationDbContext context)
        {
            Context = context;
        }

        public void Adicionar(TEntidade entity)
        {
            Context.Set<TEntidade>().Add(entity);
        }

        public List<TEntidade> Consultar()
        {
            var entidades = Context.Set<TEntidade>().ToList();
            return entidades.Any() ? entidades : new List<TEntidade>();
        }

        public TEntidade ObterPorId(int id)
        {
            var query = Context.Set<TEntidade>().Where(e => e.Id == id);
            return query.Any() ? query.First() : null;
        }
    }
}