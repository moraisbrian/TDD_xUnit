using Moq;
using Xunit;
using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTests.Util;
using CursoOnline.DominioTests.Builders;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.PublicosAlvo;

namespace CursoOnline.DominioTests.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDto _cursoDto;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly Mock<ICursoRepositorio> _cursoRepositorio;
        private readonly Mock<IConversorDePublicoAlvo> _conversorDePublicoAlvo;
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

            _cursoRepositorio = new Mock<ICursoRepositorio>();
            _conversorDePublicoAlvo = new Mock<IConversorDePublicoAlvo>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorio.Object, _conversorDePublicoAlvo.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Cadastrar(_cursoDto);

            // _mock.Verify(r => r.Armazenar(It.IsAny<Curso>()));

            _cursoRepositorio.Verify(r => r.Armazenar(
                It.Is<Curso>(
                    c => c.Nome == _cursoDto.Nome
                    && c.Descricao == _cursoDto.Descricao
                    && c.CargaHoraria == _cursoDto.CargaHoraria
                    && c.Valor == _cursoDto.Valor
                )
            ));
        }

        // O teste passou a ser feito no ConversorDePublicoAlvoTest
        //[Fact]
        //public void NaoDeveInformarPublicoAlvoInvalido()
        //{
        //    string publicoAlvoInvalido = "Médico";
        //    _cursoDto.PublicoAlvo = publicoAlvoInvalido;

        //    Assert.Throws<ExcecaoDeDominio>(() => 
        //        _armazenadorDeCurso.Cadastrar(_cursoDto)
        //    )
        //    .ComMensagem(Resource.PublicoAlvoInvalido);
        //}

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo()
                .ComId(562)
                .ComNome(_cursoDto.Nome)
                .Build();

            _cursoRepositorio.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ExcecaoDeDominio>(() => 
                _armazenadorDeCurso.Cadastrar(_cursoDto)
            )
            .ComMensagem(Resource.NomeDeCursoExistente);
        }

        [Fact]
        public void DeveAlterarDadosDoCurso()
        {
            _cursoDto.Id = _faker.Random.Int(1, 1000);
            var curso = CursoBuilder.Novo().Build();
            _cursoRepositorio.Setup(r => r.ObterPorId(_cursoDto.Id)).Returns(curso);

            _armazenadorDeCurso.Cadastrar(_cursoDto);

            Assert.Equal(_cursoDto.Nome, curso.Nome);
            Assert.Equal(_cursoDto.Valor, curso.Valor);
            Assert.Equal(_cursoDto.CargaHoraria, curso.CargaHoraria);
        }

        [Fact]
        public void NaoDeveAdicionarNoRepositorioQuandoCursoJaExiste()
        {
            _cursoDto.Id = _faker.Random.Int(1, 1000);
            var curso = CursoBuilder.Novo().Build();
            _cursoRepositorio.Setup(r => r.ObterPorId(_cursoDto.Id)).Returns(curso);

            _armazenadorDeCurso.Cadastrar(_cursoDto);

            _cursoRepositorio.Verify(r => r.Armazenar(It.IsAny<Curso>()), Times.Never);
        }
    }
}