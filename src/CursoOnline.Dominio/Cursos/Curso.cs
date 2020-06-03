using System;
using CursoOnline.Dominio.Base;

namespace CursoOnline.Dominio.Cursos
{
    public class Curso : Entidade
    {
        public Curso(string nome, string descricao, double cargaHoraria, EPublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome Inválido");

            if (cargaHoraria < 1)
                throw new ArgumentException("Carga Horária Inválida");

            if (valor < 1)
                throw new ArgumentException("Valor Inválido");

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