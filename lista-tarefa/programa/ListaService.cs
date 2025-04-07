namespace GestaoTarefas;

class TarefaService
{
    private List<Tarefa> _tarefas;

    public TarefaService(List<Tarefa> tarefas)
    {
        _tarefas = tarefas;
    }

    /// Método para listar tarefas
    public void ListarMenuDeTarefas()
    {
        int opcaoListar;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Listar Tarefas ===");
            Console.WriteLine("1 - Todas as Tarefas");
            Console.WriteLine("2 - Tarefas Pendentes");
            Console.WriteLine("3 - Tarefas Concluídas");
            Console.WriteLine("0 - Voltar");
            Console.Write("\nEscolha uma opção: ");

            if (int.TryParse(Console.ReadLine(), out opcaoListar))
            {
                switch (opcaoListar)
                {
                    case 1:
                        SubMenuFiltro(TipoListagem.Todas);
                        break;
                    case 2:
                        SubMenuFiltro(TipoListagem.Pendentes);
                        break;
                    case 3:
                        SubMenuFiltro(TipoListagem.Concluidas);
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

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();

        } while (opcaoListar != 0);
    }

    /// Método para exibir o submenu de filtro
    private void SubMenuFiltro(TipoListagem tipo)
    {
        int filtro;
        do
        {
            Console.WriteLine("\nDeseja listar:");
            Console.WriteLine("1 - Todas");
            Console.WriteLine("2 - Por Responsável");
            Console.Write("Escolha uma opção: ");

        } while (!int.TryParse(Console.ReadLine(), out filtro) || (filtro != 1 && filtro != 2));

        if (filtro == 1)
            Listar(tipo);
        else
            ListarPorResponsavel(tipo);
    }

    /// Método para listar tarefas de acordo com o tipo de listagem
    public void Listar(TipoListagem tipo)
    {
        var tarefas = FiltrarTarefas(tipo).OrderBy(t => t.DataLimite).ToList();

        string titulo = tipo switch
        {
            TipoListagem.Todas => "Todas as Tarefas:",
            TipoListagem.Pendentes => "Tarefas Pendentes:",
            TipoListagem.Concluidas => "Tarefas Concluídas:",
            _ => "Tarefas:"
        };

        Console.WriteLine($"\n{titulo}");
        if (!tarefas.Any())
            Console.WriteLine("Nenhuma tarefa encontrada.");
        else
            tarefas.ForEach(t => Console.WriteLine(t));
    }

    /// Método para listar tarefas por responsável
    private void ListarPorResponsavel(TipoListagem tipo)
    {
        string nome = LerEntradaObrigatoria("Digite o nome do responsável: ");

        var tarefasDoResponsavel = FiltrarTarefas(tipo)
            .Where(t => t.Responsavel?.Nome == nome)
            .OrderBy(t => t.DataLimite)
            .ToList();

        if (!tarefasDoResponsavel.Any())
        {
            Console.WriteLine($"\nNenhuma tarefa encontrada para o responsável \"{nome}\".");
            return;
        }

        string titulo = tipo switch
        {
            TipoListagem.Todas => $"Todas as tarefas do responsável {nome}:",
            TipoListagem.Pendentes => $"Tarefas pendentes de {nome}:",
            TipoListagem.Concluidas => $"Tarefas concluídas de {nome}:",
            _ => $"Tarefas de {nome}:"
        };

        Console.WriteLine($"\n{titulo}");
        tarefasDoResponsavel.ForEach(t => Console.WriteLine(t));
    }

    private List<Tarefa> FiltrarTarefas(TipoListagem tipo)
    {
        return tipo switch
        {
            TipoListagem.Todas => _tarefas.ToList(),
            TipoListagem.Pendentes => _tarefas.Where(t => t.Status != StatusTarefa.Concluida).ToList(),
            TipoListagem.Concluidas => _tarefas.Where(t => t.Status == StatusTarefa.Concluida).ToList(),
            _ => new List<Tarefa>()
        };
    }

    private string LerEntradaObrigatoria(string mensagem)
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

enum TipoListagem
{
    Todas,
    Pendentes,
    Concluidas
}
