using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aniversariantes.Dados;
using Aniversariantes.Model;

namespace Aniversarios.Cadastro
{
    class Apresentacao
    {
        private static void Escreva(string texto)
        {
            Console.WriteLine(texto);
        }
        private static void LimparTela() //Limpar o console
        {
            Console.Clear();
        }
        public static void MenuPrincipal()
        {
            Escreva("Menu do sistema:");
            Escreva("Selecione uma opção");
            Escreva("1 - Cadastrar Aniversariante");
            Escreva("2 - Consultar Aniversariante");
            Escreva("3 - Sair");


            char operacao = Console.ReadLine().ToCharArray()[0];

            switch (operacao)
            {
                case '1': CadastrarAniversariante(); break;
                case '2': ConsultarPessoa(); break;
                //case '3': EditarPessoa(); break;
                //case '4': ExcluirPessoa(); break;
                default: Escreva("Opção inexistente"); break;
            }
        }

        private static void CadastrarAniversariante()
        {
            LimparTela();

            Escreva("Entre com o Nome:");
            string nome = Console.ReadLine();

            Escreva("Entre com o Sobrenome:");
            string sobrenome = Console.ReadLine();

            Console.WriteLine("Digite o dia - Padrao dd (2 digitos)");
            int dia = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite o mes - Padrao mm (2 digitos)");
            int mes = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite o ano - Padrao yyyy (4 digitos)");
            int ano = int.Parse(Console.ReadLine());

            Pessoa pessoa = new Pessoa(nome, sobrenome, dia, mes, ano);
            BancoDeDados.Salvar(pessoa);

            MenuPrincipal();
        }

        private static void ConsultarPessoa()
        {
            LimparTela();

            ExibirOpcoesDeFiltro();
        }

        private static void ExibirOpcoesDeFiltro()
        {
            Escreva("Escolha uma opção de filtro");
            Escreva("1 - Consultar pelo nome");
            Escreva("2 - Consultar pelo nome completo");

            string tipoDeConsulta = Console.ReadLine();

            switch (tipoDeConsulta)
            {
                case "1":
                    ConsultarPeloNome();
                    break;
                case "2":
                    ConsultarPeloNomeCompleto();
                    break;
                default:
                    Escreva("Consulta incorreta");
                    ExibirOpcoesDeFiltro();
                    break;
            }
        }

        private static void ConsultarPeloNome()
        {
            Escreva("Entre com o nome da pessoa");
            string nome = Console.ReadLine();

            var pessoasEncontradas = BancoDeDados.BuscarPessoas(nome);
            int count = 0;
            if(pessoasEncontradas.Count() > 0)
            {
                
                LimparTela();
                Escreva("---------------------------------------------------------------");
                Escreva($"Pessoas encontradas com o nome {nome}");
                foreach (var pessoa in pessoasEncontradas)
                {            
                    Escreva($"{count} - {pessoa.Nome.ToUpper()} {pessoa.Sobrenome.ToUpper()}");
                    count++;
                }
                Escreva("---------------------------------------------------------------");

                Escreva("Selecione um aniversariante:");
                int operacao = int.Parse(Console.ReadLine());

                var selecionado = pessoasEncontradas.ToList()[operacao];
                LimparTela();
                Escreva("---------------------------------------------------------------");
                Escreva($"Nome Completo: {selecionado.Nome.ToUpper()} {selecionado.Sobrenome.ToUpper()}");
                Escreva($"Data do Aniversário: {selecionado.DataAniversario.ToString("dd/MM/yyyy")}");
                Escreva($"Faltam: {selecionado.DiasQueFaltam} dias");
                Escreva("---------------------------------------------------------------");
                Escreva("Para realizar outra operação pressione qualquer tecla.");
                Console.ReadLine();
            }
            else
            {
               LimparTela();
               Escreva($"Nenhuma pessoa encontrada com o nome: {nome}");
            }

            OperacaoTerminada();
        }

        private static void ConsultarPeloNomeCompleto()
        {
            Escreva("Entre com o nome");
            string nome = Console.ReadLine();

            Escreva("Entre com o sobrenome");
            string sobrenome = Console.ReadLine();

            var pessoaEncontrada = BancoDeDados.BuscarPorNomeCompleto(nome, sobrenome);

            if (pessoaEncontrada != null)
            {
                LimparTela();
                Escreva("Pessoa Encontrada: ");
                Escreva("---------------------------------------------------------------");
                Escreva($"Nome Completo: {pessoaEncontrada.Nome.ToUpper()} {pessoaEncontrada.Sobrenome.ToUpper()}");
                Escreva($"Data do Aniversário: {pessoaEncontrada.DataAniversario.ToString("dd/MM/yyyy")}");
                Escreva($"Faltam: {pessoaEncontrada.DiasQueFaltam} dias");
                Escreva("---------------------------------------------------------------");
                Escreva("Para realizar outra operação pressione qualquer tecla.");
                Console.ReadLine();
            }
            else
            {
                Escreva($"Nenhuma pessoa encontrada com o nome: {pessoaEncontrada.Nome.ToUpper()} {pessoaEncontrada.Sobrenome.ToUpper()}");
            }

            OperacaoTerminada();
        }

        public static RepositorioDeAniversariantes BancoDeDados
        {
            get
            {
                return new RepositoriaDeAniversariantesMemoria();
            }
        }
        private static void OperacaoTerminada()
        {
            Escreva(" 1 - Voltar ao menu principal");
            Escreva(" 2 - Continuar a consulta");
            Escreva(" 3 - Para terminar a operação");

            char operacao = Console.ReadLine().ToCharArray()[0];

            switch (operacao)
            {
                case '1':
                    LimparTela();
                    MenuPrincipal();
                    break;
                case '2':
                    LimparTela();
                    ConsultarPessoa();
                    break;
                case '3':
                    LimparTela();
                    Escreva("Operacao Encerrada!!");
                    break;
                default:
                    LimparTela();
                    Escreva("Operacao Encerrada!!");
                    break;
            };
        }
    }
}
