using System;
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
                .Quando(string.IsNullOrEmpty(nome), "Nome Inválido")
                .Quando(cargaHoraria < 1, "Carga Horária Inválida")
                .Quando(valor < 1, "Valor Inválido")
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
    }
}