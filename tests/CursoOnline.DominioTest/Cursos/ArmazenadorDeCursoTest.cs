using System;
using Moq;
using Xunit;
using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest.Util;
using CursoOnline.DominioTest.Builders;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDto _cursoDto;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly Mock<ICursoRepositorio> _mock;

        public ArmazenadorDeCursoTest()
        {
            Faker faker = new Faker();

            _cursoDto = new CursoDto
            {
                Nome = faker.Random.Word(),
                Descricao = faker.Lorem.Paragraph(),
                CargaHoraria = faker.Random.Double(50, 1000),
                PublicoAlvo = "Estudante",
                Valor = faker.Random.Double(500, 1000)
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

            Assert.Throws<ArgumentException>(() => 
                _armazenadorDeCurso.Armazenar(_cursoDto)
            )
            .ComMensagem("Publico alvo inválido");
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();

            _mock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ArgumentException>(() => 
                _armazenadorDeCurso.Armazenar(_cursoDto)
            )
            .ComMensagem("Nome do curso já consta no banco de dados");
        }
    }
}