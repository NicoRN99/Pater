using System.Xml;
using System.IO;
//using System.IO.Packaging;
//using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Pater.DataModel;

namespace Pater;



class Program  // farlo static?
{
    static public string Installation_path { get; private set; }    // "C:\\Users\\nicod\\source\\repos\\Pater";  // Perché da errore senza static?

    static public string DataPath { get; private set; }
    static public string TemplatePath { get; private set; }
    static public string ConfigPath { get; private set; }
    static public string LogPath { get; private set; }
    static public string ClientPath { get; private set; }
    


    static public void InitializeProgram()
    {
        string net6_dir = Directory.GetCurrentDirectory();

        DirectoryInfo debug_dir = Directory.GetParent(net6_dir);
        DirectoryInfo bin_dir = Directory.GetParent(debug_dir.FullName);
        DirectoryInfo pater_dir = Directory.GetParent(bin_dir.FullName);

        Installation_path = pater_dir.FullName;

        DataPath = Installation_path + "\\Data";
        TemplatePath = Installation_path + "\\Template";
        ConfigPath = Installation_path + "\\config.txt";
        LogPath = Installation_path + "\\Log";
        ClientPath = DataPath + "\\Clienti";
    }


    /*
    static private void InitializeProgram2()
    {
        try
        {
            string pwd = Directory.GetCurrentDirectory();
            string path2config = $"{pwd}\\config.txt";

            string[] lines = File.ReadAllLines(path2config);  // Da modificare

            Installation_path = lines[0].Split('=')[1];
        }
        catch (Exception ex)
        {
            //Log(ex); //Implementare una funzione che logga tutto ogni qualvolta viene sollevata un'eccezione
            Console.WriteLine(ex.Message);
            Console.WriteLine("\nIl programma potrebbe non funzionare; chiudere il programma e contattare l'amministratore.\n");
        }
        
    }
    */



    static void Main(string[] args)
    {
        InitializeProgram();


        Console.WriteLine("Pater Shell [Versione 0.1]");
        Console.WriteLine("Copyright (c) D'Angelo Corporation. Tutti i diritti riservati.\n");


        while (true)   // Si crea etichetta aggiungendo davanti "nome_etichetta:"
        {
            Console.WriteLine(">>> Scegliere quale azione eseguire:\n");
            Console.WriteLine("\t1- Creare nuova offerta");
            Console.WriteLine("\t2- Modifica offerta");
            Console.WriteLine("\t3- Aggiungere nuovo cliente");
            Console.WriteLine("\t4- Rimuovere cliente esistente");
            Console.WriteLine("\t5- Arresta il programma\n");

            string lettura = Console.ReadLine();
            bool should_break = false;

            Console.WriteLine();

            switch (lettura)
            {
                case "1":

                    Offerta offer = new Offerta();
                    offer.CompilaOfferta();
                    XmlFunctions.SalvaXmlFile(offer);
                    //off.SalvaXmlFile();
                    XmlFunctions.SaveWordFileOpenXML(offer);
                    //offer.SaveWordFileOpenXML();
                    Console.WriteLine("\n>>> Offerta creata\n");
                    offer = null;
                    
                    continue;

                case "2":

                    Console.WriteLine("Funzionalità ancora da implementare\n");

                    continue;

                case "3":

                    Cliente client = new Cliente();
                    client.CompilaCliente();
                    XmlFunctions.SalvaXmlCliente(client);
                    //cl.SalvaXmlCliente();
                    Console.WriteLine("\n>>> Cliente aggiunto\n");
                    client = null;

                    continue;

                case "4":

                    Console.WriteLine("Funzionalità ancora da implementare\n");

                    continue;

                case "5":

                    Console.WriteLine("************* Grazie per avere usato il programma *************");
                    should_break = true;

                    break;

                default:

                    Console.WriteLine("Scelta non valida! Digitare un numero tra 1 e 5.\n");

                    continue;
            }

            if (should_break)
                break;

        }
    }
}