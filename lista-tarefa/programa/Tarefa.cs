using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas
{
    class Tarefa
    {
        public string Nome { get; set; }
        public string Status { get; set; }
        public Responsavel? Responsavel { get; set; }

        public Tarefa(string nome, Responsavel? responsavel = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome da tarefa não pode ser nulo ou vazio.");

            Nome = nome.Trim();
            Status = "Pendente";
            Responsavel = responsavel;
        }

        public void MarcarComoConcluida()
        {
            Status = "Concluida";
        }

        public override string ToString()
        {
            string status = Status;
            string responsavelInfo = Responsavel != null ? Responsavel.Nome : "Sem responsável";
            return $"{Nome} - Status: {status} - Responsável: {responsavelInfo}";
        }
    }
}
