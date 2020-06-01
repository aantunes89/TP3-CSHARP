using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Aniversariantes.Model
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataAniversario { get; private set; }
        public int DiasQueFaltam { get; set; }

        public Pessoa(string nome, string sobrenome, int dia, int mes, int ano)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            DataAniversario = new DateTime(ano, mes, dia);
            DiasQueFaltam = DiasAteAniversario(dia, mes);
            
        }

        private int DiasAteAniversario(int dia, int mes)
        {
            var anoCorrente = DateTime.Today.Year;
            var hoje = DateTime.Today;
            var aniversarioEsteAno = new DateTime(anoCorrente, mes, dia);

            int diferenca = (int)aniversarioEsteAno.Subtract(hoje).TotalDays;

            if(diferenca >= 0)
            {
                return diferenca;
            }
            else
            {
                var aniversarioProximoAno = new DateTime((anoCorrente + 1), mes, dia);
                return (int)aniversarioProximoAno.Subtract(hoje).TotalDays;
            }
        }
    }
}
