using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CursoOnline.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public List<Curso> Index()
        {
            var cursos = _cursoRepositorio.Consultar();
            
            if (cursos.Any())
            {
                if (cursos.Any())
                    return cursos;
            }

            HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            return new List<Curso>();
        }

        [HttpPost]
        public IActionResult Salvar(CursoDto model)
        {
            try
            {
                _armazenadorDeCurso.Armazenar(model);
                return Created("body", model);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("{id}")]
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
