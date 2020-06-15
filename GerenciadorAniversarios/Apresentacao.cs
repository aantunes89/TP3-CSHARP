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
                int operacao;
                var num = int.TryParse(Console.ReadLine(), out operacao);
                
                if (num)
                {
                    var selecionado = pessoasEncontradas.ToList()[operacao];
                    LimparTela();
                    Escreva("---------------------------------------------------------------");
                    Escreva($"Nome Completo: {selecionado.Nome.ToUpper()} {selecionado.Sobrenome.ToUpper()}");
                    Escreva($"Data do Aniversário: {selecionado.DataAniversario.ToString("dd/MM/yyyy")}");
                    Escreva($"Faltam: {selecionado.DiasQueFaltam} dias");
                    Escreva("---------------------------------------------------------------");

                    Escreva("Para editar as informacoes deste usuario digite 1");
                    Escreva("Para realizar outra operação pressione qualquer outra tecla.");
                    var opcao = Console.ReadLine();

                    if(int.Parse(opcao) == 1)
                    {
                        EditarPessoa(selecionado);
                    }
                    Console.ReadLine();
                } else
                {
                    Escreva("Opção Inválida");
                    ConsultarPeloNome();
                }
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
                Escreva("Para editar as informacoes deste usuario digite 1");
                Escreva("Para realizar outra operação pressione qualquer outra tecla.");
                var opcao = Console.ReadLine();

                if (int.Parse(opcao) == 1)
                {
                    EditarPessoa(pessoaEncontrada);
                }
                Console.ReadLine();
            }
            else
            {
                Escreva($"Nenhuma pessoa encontrada com o nome: {pessoaEncontrada.Nome.ToUpper()} {pessoaEncontrada.Sobrenome.ToUpper()}");
            }

            OperacaoTerminada();
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

        private static void EditarPessoa(Pessoa pessoa)
        {
            var pessoaOriginal = pessoa;
            Escreva($"Para mudar nome de {pessoa.Nome} digite 1");
            Escreva($"Para mudar nome de {pessoa.Sobrenome} digite 2");
            Escreva($"Para mudar nome de {pessoa.DataAniversario} digite 3");
            Escreva($"Para mudar todas as informacoes de {pessoa.Nome} ${pessoa.Sobrenome}");
            char operacao = Console.ReadLine().ToCharArray()[0];
            switch (operacao)
            {
                case '1': 
                    Escreva("Digite o novo nome:");
                    string nome = Console.ReadLine();
                    pessoa.Nome = nome;
                    BancoDeDados.Salvar(pessoa);
                    break;
                case '2':
                    string sobrenome = Console.ReadLine();
                    pessoa.Sobrenome = sobrenome;
                    BancoDeDados.Salvar(pessoa);
                    break;
                case '3':
                    Console.WriteLine("Digite o dia - Padrao dd (2 digitos)");
                    int dia = int.Parse(Console.ReadLine());
                    Console.WriteLine("Digite o mes - Padrao mm (2 digitos)");
                    int mes = int.Parse(Console.ReadLine());
                    Console.WriteLine("Digite o ano - Padrao yyyy (4 digitos)");
                    int ano = int.Parse(Console.ReadLine());
                    pessoa.DataAniversario = new DateTime(ano, mes, dia);
                    BancoDeDados.Salvar(pessoa);
                    break;
                case '4':
                    Escreva("Digite o novo nome:");
                    string todaInfoNome = Console.ReadLine();
                    pessoa.Nome = todaInfoNome != null ? todaInfoNome : pessoa.Nome;

                    Escreva("Digite o novo sobrenome:");
                    string todaInfoSobreNome = Console.ReadLine();
                    pessoa.Sobrenome = todaInfoSobreNome != null ? todaInfoSobreNome : pessoa.Sobrenome; ;

                    Console.WriteLine("Digite o dia - Padrao dd (2 digitos)");
                    var todaInfoDia = Console.ReadLine();
                    
                    Console.WriteLine("Digite o mes - Padrao mm (2 digitos)");
                    var todaInfoMes = Console.ReadLine();
                    
                    Console.WriteLine("Digite o ano - Padrao yyyy (4 digitos)");
                    var todaInfoAno = Console.ReadLine();
                    
                    if( 
                           !String.IsNullOrWhiteSpace(todaInfoDia)
                        && !String.IsNullOrWhiteSpace(todaInfoMes) 
                        && !String.IsNullOrWhiteSpace(todaInfoAno)
                    )
                    {
                        int novoDia, novoMes, novoAno;
                        int.TryParse(todaInfoDia, out novoDia);
                        int.TryParse(todaInfoDia, out novoMes);
                        int.TryParse(todaInfoDia, out novoAno);
                        if (novoDia is int && novoMes is int && novoAno is int )
                        {
                            var novaData = new DateTime(novoDia, novoMes, novoAno);
                            pessoa.DataAniversario = novaData != null ? novaData : pessoa.DataAniversario;
                        }
                        
                    }


                    BancoDeDados.Salvar(pessoa);
                    break;
                default: Escreva("Opção inexistente"); break;
            }
            MenuPrincipal();
        }

        public static RepositorioDeAniversariantes BancoDeDados
        {
            get
            {
                return new RepositoriaDeAniversariantesMemoria();
            }
        }
    }
}
