﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas
{
    class Tarefa
    {
        public string Nome { get; set; }
        public bool Concluida { get; set; }
        public Responsavel? Responsavel { get; set; }

        public Tarefa(string nome, Responsavel? responsavel = null)
        {
            Nome = nome;
            Concluida = false;
            Responsavel = responsavel;
        }

        public void MarcarComoConcluida()
        {
            Concluida = true;
        }

        public override string ToString()
        {
            string status = Concluida ? "Concluída" : "Pendente";
            string responsavelInfo = Responsavel != null ? Responsavel.Nome : "Sem responsável";
            return $"{Nome} - Status: {status} - Responsável: {responsavelInfo}";
        }
    }
}
