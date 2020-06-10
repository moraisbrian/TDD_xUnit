using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CursoOnline.Api.Controllers
{
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly IRepositorio<Curso> _cursoRepositorio;

        public CursoController(ArmazenadorDeCurso armazenadorDeCurso, IRepositorio<Curso> cursoRepositorio)
        {
            _armazenadorDeCurso = armazenadorDeCurso;
            _cursoRepositorio = cursoRepositorio;
        }

        [HttpGet]
        [Route("api/Cursos")]
        public List<Curso> Index()
        {
            var cursos = _cursoRepositorio.Consultar();

            if (cursos.Any())
                return cursos;

            return new List<Curso>();
        }

        [HttpPost]
        [Route("api/Cursos")]
        public IActionResult Salvar(CursoDto model)
        {
            try
            {
                _armazenadorDeCurso.Armazenar(model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut]
        [Route("api/Cursos/{id}")]
        public IActionResult Editar(int id, CursoDto cursoDto)
        {
            var curso = _cursoRepositorio.ObterPorId(id);

            if (curso != null)
            {
                try
                {
                    cursoDto.Id = id;
                    _armazenadorDeCurso.Armazenar(cursoDto);
                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
    }
}
