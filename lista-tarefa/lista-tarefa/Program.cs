namespace GestaoTarefas;

class Program
{
    static void Main()
    {
        int opcao;

        do
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Opções ===");
            Console.WriteLine("1 - Cadastrar novo responsável");
            Console.WriteLine("2 - Cadastrar Tarefa");
            Console.WriteLine("3 - Excluir Tarefa");
            Console.WriteLine("4 - Atualizar Status da Tarefa");
            Console.WriteLine("5 - Listar Tarefas");
            Console.WriteLine("0 - Sair");
            Console.Write("\nEscolha uma opção: ");

            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        cadastrarNovoResponsavel();
                        break;
                    case 2:
                        cadastrarNovaTarefa();
                        break;
                    case 3:
                        excluirTarefa();
                        break;
                    case 4:
                        atualizarStatusTarefa();
                        break;
                    case 5:
                        listarTarefa();
                        break;
                    case 0:
                        Console.WriteLine("\nSaindo...");
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida! Tente novamente.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida! Digite um número.");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();

        } while (opcao != 0);
    }

    static void cadastrarNovoResponsavel()
    {
        Console.WriteLine("Cadastrar novo responsavel");
    }

    static void cadastrarNovaTarefa()
    {
        Console.WriteLine("Cadastrar nova tarefa");
    }

    static void excluirTarefa()
    {
        Console.WriteLine("Excluir tarefa");
    }

    static void atualizarStatusTarefa()
    {
        Console.WriteLine("Atualizar Status Tarefa");
    }

    static void listarTarefa()
    {

        Console.WriteLine("Listar Tarefas");

        int opcaoListar;

        do
        {
            Console.Clear();
            Console.WriteLine("=== Listar Tarefas ===");
            Console.WriteLine("1 - Listar Tarefas Pendentes");
            Console.WriteLine("2 - Listar Tarefas Concluidas");
            Console.WriteLine("3 - Listar Tarefas Por Responsavel");
            Console.WriteLine("4 - Listar Tarefas Pendentes Por Responsavel");
            Console.WriteLine("5 - Listar Tarefas Concluidas Por Responsavel");
            Console.WriteLine("0 - voltar");
            Console.Write("\nEscolha uma opção: ");

            if (int.TryParse(Console.ReadLine(), out opcaoListar))
            {
                switch (opcaoListar)
                {
                    case 1:
                        listarTarefasPendentes();
                        break;
                    case 2:
                        listarTarefasConcluidas();
                        break;
                    case 3:
                        listarTarefaResponsavel();
                        break;
                    case 4:
                        listarTarefaPendenteResponsavel();
                        break;
                    case 5:
                        listarTarefaConcluidaResponsavel();
                        break;
                    case 0:
                        Console.WriteLine("\nVoltando...");
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida! Tente novamente.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida! Digite um número.");
            }

        } while (opcaoListar != 0);
    }

    static void listarTarefasPendentes()
    {
        Console.WriteLine("listar tarefas pendentes");
    }

    static void listarTarefasConcluidas()
    {
        Console.WriteLine("listar tarefas concluidas");
    }

    static void listarTarefaResponsavel()
    {
        Console.WriteLine("listar tarefas por responsavel");
    }

    static void listarTarefaPendenteResponsavel()
    {
        Console.WriteLine("listar tarefas pendentes por responsavel");
    }

    static void listarTarefaConcluidaResponsavel()
    {
        Console.WriteLine("listar tarefas concluidas por responsavel");
    }
}