using System.Xml.Linq;

namespace GestaoTarefas;

class Program
{
    static List<Responsavel> listaDeResponsaveis = new List<Responsavel>();
    static List<Tarefa> listaDeTarefas = new List<Tarefa>();

    static void Main()
    {
        int opcao;

        do
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Opções ===");
            Console.WriteLine("1 - Cadastrar novo responsável");
            Console.WriteLine("2 - Excluir responsável");
            Console.WriteLine("3 - Listar Responsáveis");
            Console.WriteLine("4 - Cadastrar Tarefa");
            Console.WriteLine("5 - Excluir Tarefa");
            Console.WriteLine("6 - Atualizar Status da Tarefa");
            Console.WriteLine("7 - Listar Tarefas");
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
                        excluirResponsavel();
                        break;
                    case 3:
                        listarResponsavel();
                        break;
                    case 4:
                        cadastrarNovaTarefa();
                        break;
                    case 5:
                        excluirTarefa();
                        break;
                    case 6:
                        atualizarStatusTarefa();
                        break;
                    case 7:
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
        Console.Write("Insira o nome do responsável: ");
        string nome = Console.ReadLine();
        Console.Write("Insira o e-mail do responsável: ");
        string email = Console.ReadLine();

        listaDeResponsaveis.Add(new Responsavel(nome, email));
        Console.WriteLine($"Responsável {nome} cadastrado com sucesso!");
    }

    static void cadastrarNovaTarefa()
    {
        Console.Write("Insira o nome da tarefa: ");
        string nomeTarefa = Console.ReadLine();

        Console.WriteLine("Deseja vincular um responsável a esta tarefa?");

        Console.Write("Digite o nome do responsável ou pressione Enter para deixar sem responsável: ");
        string nomeResponsavel = Console.ReadLine();

        Responsavel? responsavel = listaDeResponsaveis.FirstOrDefault(r => r.Nome == nomeResponsavel);

        listaDeTarefas.Add(new Tarefa(nomeTarefa, responsavel));
        Console.WriteLine($"Tarefa '{nomeTarefa}' cadastrada com sucesso!");
    }

    static void excluirResponsavel()
    {
        Console.Write("Insira o nome do responsável que deseja excluir: ");

        string nome = Console.ReadLine();

        Responsavel? responsavel = listaDeResponsaveis.FirstOrDefault(r => r.Nome == nome);

        if (responsavel != null)
        {
            listaDeResponsaveis.Remove(responsavel);
            Console.WriteLine($"O responsável {nome} foi excluído.");
        }
        else
        {
            Console.WriteLine("Responsável não encontrado.");
        }
    }

    static void excluirTarefa()
    {
        Console.Write("Insira o nome da tarefa que deseja excluir: ");
        string nome = Console.ReadLine();

        Tarefa? tarefa = listaDeTarefas.FirstOrDefault(t => t.Nome == nome);

        if (tarefa != null)
        {
            listaDeTarefas.Remove(tarefa);
            Console.WriteLine($"A tarefa '{nome}' foi excluída.");
        }
        else
        {
            Console.WriteLine("Tarefa não encontrada.");
        }
    }

    static void atualizarStatusTarefa()
    {
        Console.Write("Insira o nome da tarefa que deseja atualizar: ");
        string nome = Console.ReadLine();

        Tarefa? tarefa = listaDeTarefas.FirstOrDefault(t => t.Nome == nome);
        if (tarefa != null)
        {
            tarefa.MarcarComoConcluida();
            Console.WriteLine($"A tarefa '{nome}' foi marcada como concluída.");
        }
        else
        {
            Console.WriteLine("Tarefa não encontrada.");
        }
    }

    static void listarResponsavel()
    {
        Console.WriteLine("\nResponsáveis cadastrados:");
        if (listaDeResponsaveis.Count == 0)
        {
            Console.WriteLine("Nenhum responsável cadastrado.");
        }
        else
        {
            foreach (var responsavel in listaDeResponsaveis)
            {
                Console.WriteLine(responsavel);
            }
        }
    }

    static void listarTarefa()
    {
        Console.WriteLine("\nTarefas cadastradas:");
        if (listaDeTarefas.Count == 0)
        {
            Console.WriteLine("Nenhuma tarefa cadastrada.");
        }
        else
        {
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

