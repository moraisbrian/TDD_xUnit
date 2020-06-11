using Moq;
using Bogus;
using Xunit;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Base;
using Bogus.Extensions.Brazil;
using CursoOnline.DominioTest.Util;
using CursoOnline.DominioTest.Builders;

namespace CursoOnline.DominioTest.Alunos
{
    public class ArmazenadorDeAlunoTest
    {
        private readonly AlunoDto _alunoDto;
        private readonly ArmazenadorDeAluno _armazenadorDeAluno;
        private readonly Mock<IAlunoRepositorio> _mock;
        private readonly Faker _faker;

        public ArmazenadorDeAlunoTest()
        {
            _faker = new Faker();

            _alunoDto = new AlunoDto
            {
                Nome = _faker.Person.FullName,
                Cpf = _faker.Person.Cpf(),
                Email = _faker.Person.Email,
                PublicoAlvo = "Estudante"
            };

            _mock = new Mock<IAlunoRepositorio>();
            _armazenadorDeAluno = new ArmazenadorDeAluno(_mock.Object);
        }

        [Fact]
        public void DeveAdicionarAluno()
        {
            _armazenadorDeAluno.Cadastrar(_alunoDto);

            _mock.Verify(r => r.Adicionar(
                It.Is<Aluno>(
                    a => a.Nome == _alunoDto.Nome &&
                    a.Email == _alunoDto.Email &&
                    a.Cpf == _alunoDto.Cpf
                ))
            );
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            string publicoAlvoInvalido = "Médico";
            _alunoDto.PublicoAlvo = publicoAlvoInvalido;

            Assert.Throws<ExcecaoDeDominio>(() => 
                _armazenadorDeAluno.Cadastrar(_alunoDto)
            )
            .ComMensagem(Resource.PublicoAlvoInvalido);
        }

        [Fact]
        public void DeveAlterarNomeDoAluno()
        {
            _alunoDto.Id = _faker.Random.Int(1, 1000);
            var aluno = AlunoBuilder.Novo().Build();
            _mock.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(aluno);

            _armazenadorDeAluno.Cadastrar(_alunoDto);

            Assert.Equal(_alunoDto.Nome, aluno.Nome);
        }
    }
}