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


internal class Offerta
{
    /// <summary>
    /// qui la spiegazione, per fare questa docstring premi tre volte / 
    /// </summary>

    public string Oggetto { get; set; } = "Offerta prodotti hardware";  // da togliere il valore di default
    public string Modalità_pagamento { get; set; } = "30";   // da togliere il valore di default
    public string Validità_Offerta { get; set; } = "23-05-2022";   // da togliere il valore di default
    public string Disponibilità { get; set; } = "5";     // da togliere il valore di default
    public string Protocollo { get; set; }
    public Cliente Cliente { get; set; }
    public List<Articolo> Lista_articoli { get; set; } = new List<Articolo>();
    //public List<Servizio> Lista_servizi { get; set; } = new List<Servizio>();


    public void CompilaOggetto()
    {
        Console.WriteLine("Inserire l'oggetto dell'offerta: ");
        Oggetto = Console.ReadLine();
    }

    public void CompilaModalitàPagamento()
    {
        Console.WriteLine("Inserire la modalità di pagamento: ");
        Modalità_pagamento = Console.ReadLine();

        /*
        do
        {
            Console.WriteLine("Inserire la modalità di pagamento: ");
            Modalità_pagamento = Console.ReadLine();
        }
        while (Modalità_pagamento is null);
        */
    }

    public void CompilaValiditàOfferta()
    {
        Console.WriteLine("Inserire la validità dell'offerta: ");
        Validità_Offerta = Console.ReadLine();
        /*
        do
        {
            Console.WriteLine("Inserire la validità dell'offerta: ");
            Validità_Offerta = Console.ReadLine();
        }
        while (Validità_Offerta is null);
        */

    }

    public void CompilaDisponibilità()
    {
        Console.WriteLine("Inserire la disponibilità: ");
        Disponibilità = Console.ReadLine();
    }

    public void CompilaCliente()
    {
        Console.WriteLine("Inserire cliente esistente? [Si/si/No/no]: ");
        string risposta = Console.ReadLine();

        if (risposta.ToLower() == "si")
        {
            Console.WriteLine("Inserire il nickname del cliente: ");
            string nickname_cliente = Console.ReadLine();

            Cliente cl = new Cliente();
            cl.CompilaClienteFromXml(nickname_cliente);
            Cliente = cl;
        }
        else
        {
            Cliente cl = new Cliente();
            cl.CompilaCliente();
            Cliente = cl;

            //cl.SalvaXmlCliente();    //Salvo per il futuro
            XmlFunctions.SalvaXmlCliente(cl);
        }
    }

    public void AddArticolo()
    {
        Articolo articolo = new Articolo();
        articolo.CompilaArticolo();
        Lista_articoli.Add(articolo);
    }

    public void GeneraProtocollo()
    {
        DateTime dataora = DateTime.Now;

        string giorno = dataora.Day.ToString();
        if (giorno.Length == 1) giorno = '0' + giorno;

        string mese = dataora.Month.ToString();
        if (mese.Length == 1) mese = '0' + mese;

        string anno = dataora.Year.ToString()[2..];

        string ora = dataora.Hour.ToString();
        if (ora.Length == 1) ora = '0' + ora;

        string minuto = dataora.Minute.ToString();
        if (minuto.Length == 1) minuto = '0' + minuto;

        Protocollo = $"{Cliente.Id_cliente}{giorno}{mese}{anno}H{ora}{minuto}"; //CA + data + H + ora
    }

    public void CompilaOfferta()
    {
        CompilaOggetto();               //Queste sono solo per il documento word
        CompilaModalitàPagamento();
        CompilaValiditàOfferta();
        CompilaDisponibilità();

        CompilaCliente();
        AddArticolo();

        GeneraProtocollo();

        Console.WriteLine("Aggungere un altro articolo? [Si/si/No/no] ");
        string risposta = Console.ReadLine();

        while (risposta.ToLower() == "si")
        {
            AddArticolo();
            Console.WriteLine("Aggungere un altro articolo? [Si/si/No/no] ");
            risposta = Console.ReadLine();
        }
    }

    public void CompilaOffertaFromXML()
    {
        /// Serve per implementare la modifica di una offerta
        return;
    }
}