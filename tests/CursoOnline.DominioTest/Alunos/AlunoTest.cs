using Xunit;
using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Base;
using ExpectedObjects;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Util;
using CursoOnline.Dominio.Alunos;

namespace CursoOnline.DominioTest.Alunos
{
    public class AlunoTest
    {
        private readonly Faker _faker;
        private readonly string _nome;
        private readonly string _email;

        public AlunoTest()
        {
            _faker = new Faker();
            _nome = _faker.Person.FullName;
            _email = _faker.Person.Email;
        }

        [Theory]
        [InlineData("569.570.600-98")]
        [InlineData("675.224.290-99")]
        [InlineData("653.440.570-91")]
        public void DeveCadastrarAluno(string cpfEsperado)
        {
            var alunoEsperado = new
            {
                Nome = _nome,
                Cpf = cpfEsperado,
                Email = _email,
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
        public void NaoDeveAlunoTerNomeInvalido(string nomeInvalido)
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
        public void NaoDeveAlunoTerCpfInvalido(string cpfInvlido)
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
        public void NaoDeveAlunoTerEmailInvalido(string emailInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                AlunoBuilder.Novo().ComEmail(emailInvalido).Build())
            .ComMensagem(Resource.EmailInvalido);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void DeveAlterarNome(string nomeInvalido)
        {
            var aluno = AlunoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() =>
                aluno.AlterarNome(nomeInvalido))
            .ComMensagem(Resource.NomeInvalido);
        }
    }
}