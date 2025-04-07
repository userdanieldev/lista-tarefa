// Daniek Victor
// Felipe Garcia
// Gustavo Henrique

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
            Console.WriteLine("7 - Mudar Status da Tarefa");

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

    // Método para cadastrar novo responsável
    static void cadastrarNovoResponsavel()
    {
        string nome = LerEntradaObrigatoria("Insira o nome do responsável: ");
        
        if (listaDeResponsaveis.Any(r => r.Nome == nome))
        {
            Console.WriteLine($"Já existe um responsável com o nome \"{nome}\".");
            return;
        }

        string email = LerEntradaObrigatoria("Insira o e-mail do responsável: ");

        listaDeResponsaveis.Add(new Responsavel(nome, email));
        Console.WriteLine($"Responsável {nome} cadastrado com sucesso!");
    }

    // Método para cadastrar nova tarefa
    static void cadastrarNovaTarefa()
    {
        string nomeTarefa = LerEntradaObrigatoria("Insira o nome da tarefa: ");

        if (listaDeTarefas.Any(t => t.Nome == nomeTarefa))
        {
            Console.WriteLine($"Já existe uma tarefa com o nome \"{nomeTarefa}\".");
            return;
        }

        listarResponsavel();

        string nomeResponsavel = LerEntradaObrigatoria("\n\nDigite o nome do responsável: ");
        Responsavel? responsavel = listaDeResponsaveis.FirstOrDefault(r => r.Nome == nomeResponsavel);

        if (responsavel == null)
        {
            Console.WriteLine($"Responsável \"{nomeResponsavel}\" não encontrado. A tarefa não será cadastrada.");
            return;
        }

        int tarefasEmAndamento = listaDeTarefas
            .Where(t => t.Responsavel?.Nome == responsavel.Nome && t.Status == StatusTarefa.Em_Andamento)
            .Count();

        if (tarefasEmAndamento >= 3)
        {
            Console.WriteLine($"O responsável \"{responsavel.Nome}\" já possui 3 tarefas em andamento. A nova tarefa não pode ser cadastrada.");
            return;
        }

        DateTime dataLimite;
        while (true)
        {
            Console.Write("Digite a data limite (formato dd/MM/yyyy): ");
            string entradaData = Console.ReadLine() ?? "";

            if (DateTime.TryParseExact(entradaData, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dataLimite))
            {
                if (dataLimite < DateTime.Now.Date)
                {
                    Console.WriteLine("A data limite não pode ser anterior à data atual.");
                    continue;
                }
                break;
            }
            else
            {
                Console.WriteLine("Data inválida. Tente novamente.");
            }
        }

        Console.WriteLine("Escolha a prioridade:");
        Console.WriteLine("1 - Baixa\n2 - Média\n3 - Alta");

        PrioridadeTarefa prioridade;
        while (true)
        {
            Console.Write("Digite a prioridade: ");
            if (int.TryParse(Console.ReadLine(), out int prioridadeEscolhida) &&
                Enum.IsDefined(typeof(PrioridadeTarefa), prioridadeEscolhida - 1))
            {
                prioridade = (PrioridadeTarefa)(prioridadeEscolhida - 1);
                break;
            }
            else
            {
                Console.WriteLine("Valor inválido. Tente novamente.");
            }
        }

        if (prioridade == PrioridadeTarefa.Alta && dataLimite > DateTime.Now.AddDays(7))
        {
            Console.WriteLine("Tarefas de alta prioridade não podem ter data limite superior a 7 dias.");
            return;
        }

        try
        {
            var novaTarefa = new Tarefa(nomeTarefa, dataLimite, prioridade, responsavel);
            listaDeTarefas.Add(novaTarefa);
            Console.WriteLine($"Tarefa '{nomeTarefa}' cadastrada com sucesso!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro ao cadastrar a tarefa: {ex.Message}");
        }
    }
    // Métodos para excluir responsável e tarefa
    static void excluirResponsavel()
    {
        listarResponsavel();


        string nome = LerEntradaObrigatoria("\n\nInsira o nome do responsável que deseja excluir: ");

        Responsavel? responsavel = listaDeResponsaveis.FirstOrDefault(r => r.Nome == nome);

        if (responsavel == null)
        {
            Console.WriteLine($"Responsável \"{nome}\" não encontrado.");
            return;
        }

        listaDeResponsaveis.Remove(responsavel);
        Console.WriteLine($"O responsável \"{nome}\" foi excluído.");
    }

    // Método para excluir tarefa
    static void excluirTarefa()
    {

        listaService.Listar(TipoListagem.Todas);

        string nome = LerEntradaObrigatoria("\n\nInsira o nome da tarefa que deseja excluir: ");

        Tarefa? tarefa = listaDeTarefas.FirstOrDefault(t => t.Nome == nome);

        if (tarefa == null)
        {
            Console.WriteLine($"Tarefa \"{nome}\" não encontrada.");
            return;
        }

        listaDeTarefas.Remove(tarefa);
        Console.WriteLine($"A tarefa \"{nome}\" foi excluída.");
    }

    // Método para atualizar o status da tarefa
    static void atualizarStatusTarefa()
    {
        listaService.Listar(TipoListagem.Todas);

        string nome = LerEntradaObrigatoria("\n\nInsira o nome da tarefa que deseja atualizar: ");
        Tarefa? tarefa = listaDeTarefas.FirstOrDefault(t => t.Nome == nome);

        if (tarefa == null)
        {
            Console.WriteLine($"Tarefa \"{nome}\" não encontrada.");
            return;
        }

        Console.WriteLine("Selecione o novo status:");
        Console.WriteLine("1 - A Fazer\n2 - Em Andamento\n3 - Concluída");

        if (int.TryParse(Console.ReadLine(), out int novoStatus) &&
            Enum.IsDefined(typeof(StatusTarefa), novoStatus - 1))
        {
            tarefa.AlterarStatus((StatusTarefa)(novoStatus - 1));
            Console.WriteLine($"Status da tarefa \"{nome}\" atualizado para {tarefa.Status}.");
        }
        else
        {
            Console.WriteLine("Status inválido.");
        }
    }
    // Método para listar responsáveis
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

    // Método para listar tarefas
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

    // Método para ler entrada obrigatória
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

