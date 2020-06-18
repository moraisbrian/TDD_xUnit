using Moq;
using Bogus;
using Xunit;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Base;
using Bogus.Extensions.Brazil;
using CursoOnline.DominioTests.Builders;
using CursoOnline.DominioTests.Util;
using CursoOnline.Dominio.PublicosAlvo;

namespace CursoOnline.DominioTests.Alunos
{
    public class ArmazenadorDeAlunoTest
    {
        private readonly AlunoDto _alunoDto;
        private readonly ArmazenadorDeAluno _armazenadorDeAluno;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorio;
        private readonly Mock<IConversorDePublicoAlvo> _conversorDePublicoAlvo;
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

            _alunoRepositorio = new Mock<IAlunoRepositorio>();
            _conversorDePublicoAlvo = new Mock<IConversorDePublicoAlvo>();
            _armazenadorDeAluno = new ArmazenadorDeAluno(_alunoRepositorio.Object, _conversorDePublicoAlvo.Object);
        }

        [Fact]
        public void DeveAdicionarAluno()
        {
            _armazenadorDeAluno.Cadastrar(_alunoDto);

            _alunoRepositorio.Verify(r => r.Adicionar(
                It.Is<Aluno>(
                    a => a.Nome == _alunoDto.Nome &&
                    a.Email == _alunoDto.Email &&
                    a.Cpf == _alunoDto.Cpf
                ))
            );
        }

        // O teste passou a ser feito no ConversorDePublicoAlvoTest
        //[Fact]
        //public void NaoDeveInformarPublicoAlvoInvalido()
        //{
        //    string publicoAlvoInvalido = "Médico";
        //    _alunoDto.PublicoAlvo = publicoAlvoInvalido;

        //    Assert.Throws<ExcecaoDeDominio>(() => 
        //        _armazenadorDeAluno.Cadastrar(_alunoDto)
        //    )
        //    .ComMensagem(Resource.PublicoAlvoInvalido);
        //}

        [Fact]
        public void DeveAlterarNomeDoAluno()
        {
            _alunoDto.Id = _faker.Random.Int(1, 1000);
            _alunoDto.Nome = _faker.Person.FullName;
            var aluno = AlunoBuilder.Novo().Build();
            _alunoRepositorio.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(aluno);

            _armazenadorDeAluno.Cadastrar(_alunoDto);

            Assert.Equal(_alunoDto.Nome, aluno.Nome);
        }

        [Fact]
        public void NaoDeveAdicionarQuandoForEdicao()
        {
            _alunoDto.Id = _faker.Random.Int(1, 1000);
            var aluno = AlunoBuilder.Novo().Build();
            _alunoRepositorio.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(aluno);

            _armazenadorDeAluno.Cadastrar(_alunoDto);

            _alunoRepositorio.Verify(r => r.Adicionar(It.IsAny<Aluno>()), Times.Never);
        }

        [Fact]
        public void NaoDeveEditarDemaisInformacoes()
        {
            _alunoDto.Id = _faker.Random.Int(1, 1000);
            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            var cpfEsperado = alunoJaSalvo.Cpf;
            var emailEsperado = alunoJaSalvo.Email;
            var publicoAlvoEsperado = alunoJaSalvo.PublicoAlvo;
            _alunoRepositorio.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            _armazenadorDeAluno.Cadastrar(_alunoDto);

            Assert.Equal(cpfEsperado, alunoJaSalvo.Cpf);
            Assert.Equal(emailEsperado, alunoJaSalvo.Email);
            Assert.Equal(publicoAlvoEsperado, alunoJaSalvo.PublicoAlvo);
        }

        [Fact]
        public void NaoDeveAdicionarQuandoCpfFoiCadastrado()
        {
            var alunoComCpf = AlunoBuilder.Novo().ComId(25).Build();
            _alunoRepositorio.Setup(r => r.ObterPeloCpf(_alunoDto.Cpf)).Returns(alunoComCpf);

            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeAluno.Cadastrar(_alunoDto))
                .ComMensagem(Resource.CpfJaCadastrado);
        }
    }
}  