/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
*/
using System.IO;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Pater.DataModel;


internal class Referente
{
    public string Nome { get; private set; }
    public string Cognome { get; private set; }
    public string Email { get; private set; }
    public string Telefono { get; private set; }


    public Referente(string nome = null, string cognome = null, string email = null, string telefono = null)
    {
        // Serve il costruttore?
        Nome = nome;
        Cognome = cognome;
        Email = email;
        Telefono = telefono;
    }


    public void CompilaNome()
    {
        Console.WriteLine("Inserire il nome del referente: ");
        string nome = Console.ReadLine();

        Nome = nome;
    }

    public void CompilaCognome()
    {
        Console.WriteLine("Inserire il cognome del referente: ");
        string cognome = Console.ReadLine();

        Cognome = cognome;
    }

    public void CompilaEmail()
    {
        Console.WriteLine("Inserire l'email del referente: ");
        string email = Console.ReadLine();

        Email = email;
    }

    public void CompilaTelefono()
    {
        Console.WriteLine("Inserire il telefono del referente: ");
        string telefono = Console.ReadLine();

        Telefono = telefono;
    }


    public void CompilaReferente()
    {
        CompilaNome();
        CompilaCognome();
        CompilaEmail();
        CompilaTelefono();
    }

}
