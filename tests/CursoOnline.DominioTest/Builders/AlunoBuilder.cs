using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest.Alunos;

namespace CursoOnline.DominioTest.Builders
{
    public class AlunoBuilder
    {
        private string _nome = "Anderson";
        private string _email = "contato@contato.com";
        private string _cpf = "569.570.600-98";
        private EPublicoAlvo _publicoAlvo = EPublicoAlvo.Estudante;

        public static AlunoBuilder Novo()
        {
            return new AlunoBuilder();
        }

        public AlunoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public AlunoBuilder ComEmail(string email)
        {
            _email = email;
            return this;
        }

        public AlunoBuilder ComCpf(string cpf)
        {
            _cpf = cpf;
            return this;
        }

        public AlunoBuilder ComPublicoAlvo(EPublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public Aluno Build()
        {
            return new Aluno(_nome, _cpf, _email, _publicoAlvo);
        }
    }
}