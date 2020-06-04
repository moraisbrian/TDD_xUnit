using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CursoOnline.Web.Util;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Base;
using System.Linq;

namespace CursoOnline.Web.Controllers
{
    public class CursoController : Controller
    {
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly IRepositorio<Curso> _cursoRepositorio;

        public CursoController(ArmazenadorDeCurso armazenadorDeCurso, IRepositorio<Curso> cursoRepositorio)
        {
            _armazenadorDeCurso = armazenadorDeCurso;
            _cursoRepositorio = cursoRepositorio;
        }

        public IActionResult Index()
        {
            var cursos = _cursoRepositorio.Consultar();
            if (cursos.Any())
            {
                var dtos = cursos.Select(c => new CursoDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    CargaHoraria = c.CargaHoraria,
                    PublicoAlvo = c.PublicoAlvo.ToString(),
                    Valor = c.Valor
                });
                return View("Index", PaginatedList<CursoDto>.Create(dtos, Request));
            }

            return View("Index", PaginatedList<CursoDto>.Create(new List<CursoDto>(), Request));
        }

        public IActionResult Novo()
        {
            return View("NovoOuEditar", new CursoDto());
        }

        [HttpPost]
        public IActionResult Salvar(CursoDto model)
        {
            _armazenadorDeCurso.Armazenar(model);
            return Redirect("Index");
        }
    }
}