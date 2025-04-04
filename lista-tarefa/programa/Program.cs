using System.Xml.Linq;

namespace GestaoTarefas;

class Program
{
    static List<Responsavel> listaDeResponsaveis = new List<Responsavel>();
    static List<Tarefa> listaDeTarefas = new List<Tarefa>();
    static TarefaService listaService = new TarefaService(listaDeTarefas);


    static void Main()
    {
        int opcao;

        do
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Opções ===");
            Console.WriteLine("1 - Cadastrar novo responsável");
            Console.WriteLine("2 - Cadastrar Tarefa");
            Console.WriteLine("3 - Listar Responsáveis");
            Console.WriteLine("4 - Listar Tarefas");
            Console.WriteLine("5 - Excluir responsável");
            Console.WriteLine("6 - Excluir Tarefa");
            Console.WriteLine("7 - Mudar Status da Tarefa para Conlcuido");
            
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
                        listarResponsavel();
                        break;
                    case 4:
                        listarTarefa();
                        break;
                    case 5:
                        excluirResponsavel();
                        break;
                    case 6:
                        excluirTarefa();
                        break;
                    case 7:
                        atualizarStatusTarefa();
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
        string nome = LerEntradaObrigatoria("Insira o nome do responsável: ");
        string email = LerEntradaObrigatoria("Insira o e-mail do responsável: ");

        listaDeResponsaveis.Add(new Responsavel(nome, email));
        Console.WriteLine($"Responsável {nome} cadastrado com sucesso!");
    }


    static void cadastrarNovaTarefa()
    {
        string nomeTarefa = LerEntradaObrigatoria("Insira o nome da tarefa: ");

        Console.Write("Digite o nome do responsável (ou deixe em branco para nenhuma): ");
        string? nomeResponsavel = Console.ReadLine()?.Trim();

        Responsavel? responsavel = null;

        if (!string.IsNullOrWhiteSpace(nomeResponsavel))
        {
            responsavel = listaDeResponsaveis.FirstOrDefault(r => r.Nome == nomeResponsavel);

            if (responsavel == null)
            {
                Console.WriteLine($"Responsável \"{nomeResponsavel}\" não encontrado. A tarefa não será cadastrada.");
                return;
            }
        }

        listaDeTarefas.Add(new Tarefa(nomeTarefa, responsavel));
        Console.WriteLine($"Tarefa '{nomeTarefa}' cadastrada com sucesso!");
    }

    static void excluirResponsavel()
    {
        string nome = LerEntradaObrigatoria("Insira o nome do responsável que deseja excluir: ");

        Responsavel? responsavel = listaDeResponsaveis.FirstOrDefault(r => r.Nome == nome);

        if (responsavel == null)
        {
            Console.WriteLine($"Responsável \"{nome}\" não encontrado.");
            return;
        }

        listaDeResponsaveis.Remove(responsavel);
        Console.WriteLine($"O responsável \"{nome}\" foi excluído.");
    }

    static void excluirTarefa()
    {
        string nome = LerEntradaObrigatoria("Insira o nome da tarefa que deseja excluir: ");

        Tarefa? tarefa = listaDeTarefas.FirstOrDefault(t => t.Nome == nome);

        if (tarefa == null)
        {
            Console.WriteLine($"Tarefa \"{nome}\" não encontrada.");
            return;
        }

        listaDeTarefas.Remove(tarefa);
        Console.WriteLine($"A tarefa \"{nome}\" foi excluída.");
    }

    static void atualizarStatusTarefa()
    {
        string nome = LerEntradaObrigatoria("Insira o nome da tarefa que deseja atualizar: ");

        Tarefa? tarefa = listaDeTarefas.FirstOrDefault(t => t.Nome == nome);

        if (tarefa == null)
        {
            Console.WriteLine($"Tarefa \"{nome}\" não encontrada.");
            return;
        }

        if (tarefa.Concluida)
        {
            Console.WriteLine($"A tarefa \"{nome}\" já está marcada como concluída.");
            return;
        }

        tarefa.MarcarComoConcluida();
        Console.WriteLine($"A tarefa \"{nome}\" foi marcada como concluída.");
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
            listaService.ListarMenuDeTarefas();
        }
    }

    static string LerEntradaObrigatoria(string mensagem)
    {
        string? entrada;
        do
        {
            Console.Write(mensagem);
            entrada = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(entrada))
            {
                Console.WriteLine("Entrada inválida! O valor não pode estar vazio.");
            }

        } while (string.IsNullOrWhiteSpace(entrada));

        return entrada;
    }
}

