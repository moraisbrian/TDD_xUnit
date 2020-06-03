using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CursoOnline.Web.Util;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Web.Controllers
{
    public class CursoController : Controller
    {
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;

        public CursoController(ArmazenadorDeCurso armazenadorDeCurso)
        {
            _armazenadorDeCurso = armazenadorDeCurso;
        }

        public IActionResult Index()
        {
            var cursos = new List<CursoParaListagemDto>();
            return View("Index", PaginatedList<CursoParaListagemDto>.Create(cursos, Request));
        }

        public IActionResult Novo()
        {
            return View("NovoouEditar", new CursoDto());
        }

        [HttpPost]
        public IActionResult Salvar(CursoDto model)
        {
            _armazenadorDeCurso.Armazenar(model);
            return Ok();
        }
    }
}