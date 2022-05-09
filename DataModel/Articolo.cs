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


internal class Articolo
{
    public string Modello { get; set; }
    public string TipoVendita { get; set; }
    public string Descrizione { get; set; }
    public string CodiceArticolo { get; set; }
    public string Quantità { get; set; }
    public string Listino { get; set; }
    public string CostoVG { get; set; }
    public string PrezzoDiVendita { get; set; }
    public string PercentualeScontoVG { get; set; }
    public string PrezzoTotale { get; set; }

    public void CompilaModello()
    {
        Console.WriteLine("Inserire il modello dell'articolo: ");
        Modello = Console.ReadLine();
    }

    public void CompilaTipoVendita()
    {
        Console.WriteLine("Inserire il tipo di vendita: ");
        TipoVendita = Console.ReadLine();
    }

    public void CompilaDescrizione()
    {
        Console.WriteLine("Inserire la descrizione dell'articolo: ");
        Descrizione = Console.ReadLine();
    }

    public void CompilaCodiceArticolo()
    {
        Console.WriteLine("Inserire il codice dell'articolo: ");
        CodiceArticolo = Console.ReadLine();
    }
    public void CompilaQuantità()
    {
        Console.WriteLine("Inserire la quantità dell'articolo: ");
        Quantità = Console.ReadLine();
    }
    public void CompilaListino()
    {
        Console.WriteLine("Inserire il prezzo di listino del singolo articolo: ");
        Listino = Console.ReadLine();
    }
    public void CompilaCostoVG()
    {
        Console.WriteLine("Inserire il costo dell'articolo per Var Group: ");
        CostoVG = Console.ReadLine();
    }
    public void CompilaPrezzoDiVendita()
    {
        while (true)
        {
            Console.WriteLine("Inserire il prezzo di vendita o la percentuale di margine che si vuole raggiungere: ");
            string lettura = Console.ReadLine();

            int length_lettura = lettura.Length;


            if (lettura[length_lettura - 1] == '%')
            {
                string string_percentuale = lettura.Substring(0, length_lettura - 1);

                double double_percentuale = Convert.ToDouble(string_percentuale);
                double double_CostoVG = Convert.ToDouble(CostoVG);
                double prezzo_di_vendita = double_CostoVG * (100.0 + double_percentuale) / 100.0;

                PrezzoDiVendita = prezzo_di_vendita.ToString(".00");
                //PrezzoDiVendita = Convert.ToString(prezzo_di_vendita, ".00");  sintassi giusta per usarlo così?

                break;
            }
            else
            {
                try
                {
                    double double_lettura = Convert.ToDouble(lettura);
                    PrezzoDiVendita = double_lettura.ToString(".00");
                    //PrezzoDiVendita = lettura.ToString(".00");

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("Non si è inserito un numero o una percentuale. Riprovare");
                    continue;
                }
            }
        }
    }
    public void CompilaPercentualeScontoVG()
    {
        double costo_vg = Convert.ToDouble(CostoVG);
        double listino = Convert.ToDouble(Listino);

        double percentuale_sconto_vg = (listino - costo_vg) / (double)listino * 100;   // cast necessario per assicurarsi che l'operazione venga svolta in double

        PercentualeScontoVG = Convert.ToString(percentuale_sconto_vg) + '%';
    }
    public void CompilaPrezzoTotale()
    {
        double prezzo_di_vendita = Convert.ToDouble(PrezzoDiVendita);
        double quantità_articolo = Convert.ToDouble(Quantità);

        double prezzo_totale = prezzo_di_vendita * quantità_articolo;

        PrezzoTotale = prezzo_totale.ToString(".00");      //PrezzoTotale = Convert.ToString(prezzo_totale);
    }

    public void CompilaArticolo()
    {
        CompilaModello();
        CompilaTipoVendita();
        CompilaDescrizione();
        CompilaCodiceArticolo();
        CompilaQuantità();
        CompilaListino();
        CompilaCostoVG();
        CompilaPrezzoDiVendita();
        CompilaPercentualeScontoVG();
        CompilaPrezzoTotale();
    }
}