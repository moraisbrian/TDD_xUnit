using CursoOnline.Dominio.Base;

namespace CursoOnline.Dominio.Cursos
{
    public class Curso : Entidade
    {
        // Para EF
        private Curso() {}

        public Curso(string nome, string descricao, double cargaHoraria, EPublicoAlvo publicoAlvo, double valor)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .Quando(cargaHoraria < 1, Resource.CargaHorariaInvalida)
                .Quando(valor < 1, Resource.ValorInvalido)
                .DispararExcecaoSeExistir();

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public EPublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }

        public void AlterarNome(string nome)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();

            Nome = nome;
        }

        public void AlterarCargaHoraria(double cargaHoraria)
        {
            ValidadorDeRegra.Novo()
                .Quando(cargaHoraria < 1, Resource.CargaHorariaInvalida)
                .DispararExcecaoSeExistir();

            CargaHoraria = cargaHoraria;
        }

        public void AlterarValor(double valor)
        {
            ValidadorDeRegra.Novo()
                .Quando(valor < 1, Resource.ValorInvalido)
                .DispararExcecaoSeExistir();

            Valor = valor;
        }
    }
}