namespace CursoOnline.Dominio.Base
{
    public static class Resource
    {
        // Curso
        public static readonly string NomeInvalido = "Nome inválido";
        public static readonly string CargaHorariaInvalida = "Carga horária inválida";
        public static readonly string ValorInvalido = "Valor inválido";
        
        // ArmazenadorDeCurso
        public static readonly string PublicoAlvoInvalido = "Publico alvo inválido";
        public static readonly string NomeDeCursoExistente = "Nome do curso já consta no banco de dados";

        // Aluno
        public static readonly string CpfInvalido = "CPF inválido";
        public static readonly string EmailInvalido = "E-mail inválido";
        
        // ArmazenadorDeAluno
        public static readonly string CpfJaCadastrado = "CPF já cadastrado";

        // Matricula
        public static readonly string AlunoInvalido = "Aluno iválido";
        public static readonly string CursoInvalido = "Curso inválido";
        public static readonly string ValorPagoInvalido = "Valor pago inválido";
        public static readonly string ValorPagoMaiorQueValorDoCurso = "Valor pago maior que valor do curso";
        public static readonly string PublicosAlvoDiferente = "Publico alvo do aluno e curso são diferentes";

        // CriacaoDaMatricula
        public static readonly string CursoNaoEncontrado = "Curso não encontrado";
        public static readonly string AlunoNaoEncontrado = "Aluno não encontrado";
        public static readonly string NotaInvalida = "Nota inválida";
        public static readonly string MatriculaNaoEncontrada = "Matricula não encontrada";
    }
}
