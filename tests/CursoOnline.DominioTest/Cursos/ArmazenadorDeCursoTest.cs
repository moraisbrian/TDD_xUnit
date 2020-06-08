using System;
using Moq;
using Xunit;
using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest.Util;
using CursoOnline.DominioTest.Builders;
using CursoOnline.Dominio.Base;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDto _cursoDto;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly Mock<ICursoRepositorio> _mock;
        private readonly Faker _faker;

        public ArmazenadorDeCursoTest()
        {
            _faker = new Faker();

            _cursoDto = new CursoDto
            {
                Nome = _faker.Random.Word(),
                Descricao = _faker.Lorem.Paragraph(),
                CargaHoraria = _faker.Random.Double(50, 1000),
                PublicoAlvo = "Estudante",
                Valor = _faker.Random.Double(500, 1000)
            };

            _mock = new Mock<ICursoRepositorio>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_mock.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDto);

            // _mock.Verify(r => r.Armazenar(It.IsAny<Curso>()));

            _mock.Verify(r => r.Armazenar(
                It.Is<Curso>(
                    c => c.Nome == _cursoDto.Nome
                    && c.Descricao == _cursoDto.Descricao
                    && c.CargaHoraria == _cursoDto.CargaHoraria
                    && c.Valor == _cursoDto.Valor
                )
            ));
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            string publicoAlvoInvalido = "Médico";
            _cursoDto.PublicoAlvo = publicoAlvoInvalido;

            Assert.Throws<ExcecaoDeDominio>(() => 
                _armazenadorDeCurso.Armazenar(_cursoDto)
            )
            .ComMensagem(Resource.PublicoAlvoInvalido);
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo()
                .ComId(562)
                .ComNome(_cursoDto.Nome)
                .Build();

            _mock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ExcecaoDeDominio>(() => 
                _armazenadorDeCurso.Armazenar(_cursoDto)
            )
            .ComMensagem(Resource.NomeDeCursoExistente);
        }

        [Fact]
        public void DeveAlterarDadosDoCurso()
        {
            _cursoDto.Id = _faker.Random.Int(1, 1000);
            var curso = CursoBuilder.Novo().Build();
            _mock.Setup(r => r.ObterPorId(_cursoDto.Id)).Returns(curso);

            _armazenadorDeCurso.Armazenar(_cursoDto);

            Assert.Equal(_cursoDto.Nome, curso.Nome);
            Assert.Equal(_cursoDto.Valor, curso.Valor);
            Assert.Equal(_cursoDto.CargaHoraria, curso.CargaHoraria);
        }

        [Fact]
        public void NaoDeveAdicionarNoRepositorioQuandoCursoJaExiste()
        {
            _cursoDto.Id = _faker.Random.Int(1, 1000);
            var curso = CursoBuilder.Novo().Build();
            _mock.Setup(r => r.ObterPorId(_cursoDto.Id)).Returns(curso);

            _armazenadorDeCurso.Armazenar(_cursoDto);

            _mock.Verify(r => r.Armazenar(It.IsAny<Curso>()), Times.Never);
        }
    }
}