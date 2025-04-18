﻿namespace GestaoTarefas;

class Responsavel
{
    public string Nome { get; set; }
    public string Email { get; set; }

    public Responsavel(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }

    public override string ToString()
    {
        return $"{Nome} ({Email})";
    }
}
