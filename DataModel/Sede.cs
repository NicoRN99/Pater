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


internal class Sede
{
    public string Stato { get; private set; }
    public string Provincia { get; private set; }
    public string Città { get; private set; }
    public string Indirizzo { get; private set; }
    public string Cap { get; private set; }


    public Sede(string stato = null, string regione = null, string città = null,
                string indirizzo = null, string cap = null)
    {
        // Serve sto costruttore?
        Stato = stato;
        Provincia = regione;
        Città = città;
        Indirizzo = indirizzo;
        Cap = cap;
    }


    public void CompilaStato()
    {
        Console.WriteLine("Inserire lo stato di appartenenza: ");
        string stato = Console.ReadLine();

        Stato = stato;
    }

    public void CompilaProvincia()
    {
        Console.WriteLine("Inserire l'abbreviazione della provincia di appartenza: ");
        string regione = Console.ReadLine();

        Provincia = '(' + regione.ToUpper() + ')';
    }

    public void CompilaCittà()
    {
        Console.WriteLine("Inserire la città della società: ");
        string città = Console.ReadLine();

        Città = città;
    }

    public void CompilaIndirizzo()
    {
        Console.WriteLine("Inserire l'indirizzo della società: ");
        string indirizzo = Console.ReadLine();

        Indirizzo = indirizzo;
    }

    public void CompilaCap()
    {
        Console.WriteLine("Inserire il cap della città: ");
        string cap = Console.ReadLine();

        Cap = cap;
    }

    public void CompilaSede()
    {
        CompilaStato();
        CompilaProvincia();
        CompilaCittà();
        CompilaIndirizzo();
        CompilaCap();
    }
}