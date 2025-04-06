using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas
{
    enum StatusTarefa
    {
        A_Fazer,
        Em_Andamento,
        Concluida
    }

    enum PrioridadeTarefa
    {
        Baixa,
        Media,
        Alta
    }

    class Tarefa
    {
        public string Nome { get; private set; }
        public DateTime DataLimite { get; private set; }
        public StatusTarefa Status { get; private set; }
        public PrioridadeTarefa Prioridade { get; private set; }
        public Responsavel Responsavel { get; private set; }

        public Tarefa(string nome, DateTime dataLimite, PrioridadeTarefa prioridade, Responsavel responsavel)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome da tarefa não pode ser vazio.");

            if (dataLimite.Date < DateTime.Now.Date)
                throw new ArgumentException("A data limite da tarefa não pode ser menor que a data atual.");

            if (prioridade == PrioridadeTarefa.Alta && dataLimite > DateTime.Now.AddDays(7))
                throw new ArgumentException("Tarefas de alta prioridade não podem ter data limite superior a 7 dias.");

            Nome = nome.Trim();
            DataLimite = dataLimite;
            Prioridade = prioridade;
            Responsavel = responsavel ?? throw new ArgumentNullException(nameof(responsavel));
            Status = StatusTarefa.A_Fazer;
        }

        public void MarcarComoEmAndamento()
        {
            Status = StatusTarefa.Em_Andamento;
        }

        public override string ToString()
        {
            string responsavelInfo = Responsavel != null ? Responsavel.Nome : "Sem responsável";
            return $"{Nome} - Limite: {DataLimite:dd/MM/yyyy} - Status: {Status} - Prioridade: {Prioridade} - Responsável: {responsavelInfo}";
        }

        public void AlterarStatus(StatusTarefa novoStatus)
        {
            Status = novoStatus;
        }
    }
}
