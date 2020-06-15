using System;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.PublicosAlvo;

namespace CursoOnline.Dominio.Alunos
{
    public class ArmazenadorDeAluno
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IConversorDePublicoAlvo _conversorDePublicoAlvo;

        public ArmazenadorDeAluno(IAlunoRepositorio alunoRepositorio, IConversorDePublicoAlvo conversorDePublicoAlvo)
        {
            _alunoRepositorio = alunoRepositorio;
            _conversorDePublicoAlvo = conversorDePublicoAlvo;
        }

        public void Cadastrar(AlunoDto alunoDto)
        {
            var comCpfJaCadastrado = _alunoRepositorio.ObterPeloCpf(alunoDto.Cpf);

            ValidadorDeRegra.Novo()
                .Quando(comCpfJaCadastrado != null && comCpfJaCadastrado.Id != alunoDto.Id, Resource.CpfJaCadastrado)
                .DispararExcecaoSeExistir();

            if (alunoDto.Id == 0)
            {
                var publicoAlvoConvertido = _conversorDePublicoAlvo.Converter(alunoDto.PublicoAlvo);
                Aluno aluno = new Aluno(
                    alunoDto.Nome,
                    alunoDto.Cpf,
                    alunoDto.Email,
                    publicoAlvoConvertido
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