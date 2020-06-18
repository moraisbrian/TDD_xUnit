using Xunit;
using Bogus;
using CursoOnline.Dominio.Base;
using ExpectedObjects;
using CursoOnline.DominioTests.Builders;
using CursoOnline.DominioTests.Util;
using CursoOnline.Dominio.Alunos;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio.PublicosAlvo;

namespace CursoOnline.DominioTests.Alunos
{
    public class AlunoTest
    {
        private readonly Faker _faker;

        public AlunoTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveCriarAluno()
        {
            var alunoEsperado = new
            {
                Nome = _faker.Person.FullName,
                Cpf = _faker.Person.Cpf(),
                Email = _faker.Person.Email,
                PublicoAlvo = EPublicoAlvo.Estudante
            };

            var aluno = new Aluno(
                alunoEsperado.Nome,
                alunoEsperado.Cpf,
                alunoEsperado.Email,
                alunoEsperado.PublicoAlvo
            );

            alunoEsperado.ToExpectedObject().ShouldMatch(aluno);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCriarAlunoComNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                AlunoBuilder.Novo().ComNome(nomeInvalido).Build())
            .ComMensagem(Resource.NomeInvalido);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("569.570.600-9888")]
        [InlineData("569.570")]
        public void NaoDeveCriarAlunoComCpfInvalido(string cpfInvlido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                AlunoBuilder.Novo().ComCpf(cpfInvlido).Build())
            .ComMensagem(Resource.CpfInvalido);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("fulano.com.br")]
        [InlineData("ciclano@")]
        [InlineData("email@email")]
        public void NaoDeveCriarAlunoComEmailInvalido(string emailInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                AlunoBuilder.Novo().ComEmail(emailInvalido).Build())
            .ComMensagem(Resource.EmailInvalido);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarAlunoComNomeInvalido(string nomeInvalido)
        {
            var aluno = AlunoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() =>
                aluno.AlterarNome(nomeInvalido))
            .ComMensagem(Resource.NomeInvalido);
        }

        [Fact]
        public void DeveAlterarNomeCadastrado()
        {
            var nome = _faker.Person.FullName;
            var aluno = AlunoBuilder.Novo().Build();

            aluno.AlterarNome(nome);

            Assert.Equal(nome, aluno.Nome);
        }
    }
}