﻿using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;
using System;

namespace CursoOnline.Dominio.Matriculas
{
    public class Matricula : Entidade
    {
        public Matricula(Aluno aluno, Curso curso, double valorPago)
        {
            ValidadorDeRegra.Novo()
                .Quando(aluno == null, Resource.AlunoInvalido)
                .Quando(curso == null, Resource.CursoInvalido)
                .Quando(valorPago < 1, Resource.ValorPagoInvalido)
                .Quando(curso != null && valorPago > curso.Valor, Resource.ValorPagoMaiorQueValorDoCurso)
                .Quando((aluno != null && curso != null) && (aluno.PublicoAlvo != curso.PublicoAlvo), Resource.PublicosAlvoDiferente)
                .DispararExcecaoSeExistir();

            Aluno = aluno;
            Curso = curso;
            ValorPago = valorPago;
            TemDesconto = valorPago < curso.Valor;

        }

        public Aluno Aluno { get; private set; }
        public Curso Curso { get; private set; }
        public double ValorPago { get; private set; }
        public bool TemDesconto { get; private set; }
        public double NotaDoAluno { get; private set; }
        public bool MatriculaConcluida { get; private set; }
        public bool Cancelada { get; private set; }

        public void InformarNota(double nota)
        {
            ValidadorDeRegra.Novo()
                .Quando(nota < 0 || nota > 10, Resource.NotaInvalida)
                .Quando(Cancelada, Resource.MatriculaCancelada)
                .DispararExcecaoSeExistir();

            NotaDoAluno = nota;
            MatriculaConcluida = true;
        }

        public void Cancelar()
        {
            ValidadorDeRegra.Novo()
                .Quando(MatriculaConcluida, Resource.MatriculaConcluida)
                .DispararExcecaoSeExistir();

            Cancelada = true;
        }
    }
}
