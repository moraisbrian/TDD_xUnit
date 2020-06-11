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
            var comCpfJaCadastrado = _alunoRepositorio.ObterPeloCpf(alunoDto.Cpf);

            ValidadorDeRegra.Novo()
                .Quando(comCpfJaCadastrado != null && comCpfJaCadastrado.Id != alunoDto.Id, Resource.CpfJaCadastrado)
                .Quando(!Enum.TryParse<EPublicoAlvo>(alunoDto.PublicoAlvo, out var publicoAlvo), Resource.PublicoAlvoInvalido)
                .DispararExcecaoSeExistir();

            if (alunoDto.Id == 0)
            {
                Aluno aluno = new Aluno(
                    alunoDto.Nome,
                    alunoDto.Cpf,
                    alunoDto.Email,
                    publicoAlvo
                );
                _alunoRepositorio.Adicionar(aluno);
            }
            else if (alunoDto.Id > 0)
            {
                var aluno = _alunoRepositorio.ObterPorId(alunoDto.Id);
                aluno.AlterarNome(alunoDto.Nome);
            }        
        }
    }
}