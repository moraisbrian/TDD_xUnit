using System;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Alunos
{
    public class ArmazenadorDeAluno
    {
        private readonly IAlunoRepositorio _alunoRepositorio;

        public ArmazenadorDeAluno(IAlunoRepositorio alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        public void Cadastrar(AlunoDto alunoDto)
        {
            ValidadorDeRegra.Novo()
                .Quando(!Enum.TryParse<EPublicoAlvo>(alunoDto.PublicoAlvo, out var publicoAlvo), Resource.PublicoAlvoInvalido)
                .DispararExcecaoSeExistir();

            Aluno aluno = new Aluno(
                alunoDto.Nome,
                alunoDto.Cpf,
                alunoDto.Email,
                publicoAlvo
            );

            if (alunoDto.Id == 0)
                _alunoRepositorio.Adicionar(aluno);
            else if (alunoDto.Id > 0)
            {
                aluno = _alunoRepositorio.ObterPorId(alunoDto.Id);
                aluno.AlterarNome(alunoDto.Nome);
            }        
        }
    }
}