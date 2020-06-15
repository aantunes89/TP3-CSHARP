using Aniversariantes.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Aniversariantes.Dados
{
    public class RepositoriaDeAniversariantesMemoria : RepositorioDeAniversariantes
    {
        private static List<Pessoa> pessoasCadastradas = new List<Pessoa>();

        protected override void CriarNovo(Pessoa pessoa)
        {
            pessoasCadastradas.Add(pessoa);
        }

         protected override void Editar(Pessoa pessoa)
        {
            pessoasCadastradas.Remove(pessoa);
            pessoasCadastradas.Add(pessoa);
        }
        public override IEnumerable<Pessoa> BuscarPessoas()
        {
            return pessoasCadastradas;
        }
        public override IEnumerable<Pessoa> BuscarPessoas(string nome)
        {
            return pessoasCadastradas
                    .Where(pessoa => pessoa.Nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase));
        }

        public override Pessoa BuscarPorNomeCompleto(string nome, string sobrenome)
        {
            return pessoasCadastradas.SingleOrDefault(pessoa => (
                    (pessoa.Nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase)
                    &&
                    (pessoa.Sobrenome.Contains(sobrenome, StringComparison.InvariantCultureIgnoreCase)
                    ))
                )
            );
        }

        public override void Excluir(Pessoa pessoa)
        {
            pessoasCadastradas.Remove(pessoa);
        }
    }
}
