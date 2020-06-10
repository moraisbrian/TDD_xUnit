using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Base
{
    public static class Resource
    {
        // Curso
        public static readonly string NomeInvalido = "Nome Inválido";
        public static readonly string CargaHorariaInvalida = "Carga Horária Inválida";
        public static readonly string ValorInvalido = "Valor Inválido";

        // ArmazenadorDeCurso
        public static readonly string PublicoAlvoInvalido = "Publico alvo inválido";
        public static readonly string NomeDeCursoExistente = "Nome do curso já consta no banco de dados";

        // Aluno
        public static readonly string CpfInvalido = "CPF inválido";
        public static readonly string EmailInvalido = "E-mail inválido";
    }
}
