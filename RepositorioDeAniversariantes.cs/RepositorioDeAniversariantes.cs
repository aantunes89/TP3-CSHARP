using Aniversariantes.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Aniversariantes.Dados
{
    public abstract class RepositorioDeAniversariantes
    {
        public void Salvar(Pessoa pessoa)
        {

            if (PessoaExistente(pessoa))
            {
                Editar(pessoa);
            }
            else
            {
                CriarNovo(pessoa);
            }

        }

        private bool PessoaExistente(Pessoa pessoa)
        {
            var nome = pessoa.Nome;
            var sobrenome = pessoa.Sobrenome;
            var pessoaEncontrada = BuscarPorNomeCompleto(nome, sobrenome);

            if (pessoaEncontrada != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected abstract void CriarNovo(Pessoa pessoa);

        protected abstract void Editar(Pessoa pessoa);

        public abstract void Excluir(Pessoa pessoa);

        public abstract IEnumerable<Pessoa> BuscarPessoas();

        public abstract IEnumerable<Pessoa> BuscarPessoas(string nome);

        public abstract Pessoa BuscarPorNomeCompleto(string nome, string sobrenome);
    }
}
