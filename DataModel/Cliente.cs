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


internal class Cliente
{
    // Field and Properities

    public string Nickname { get; set; }
    public string Id_cliente { get; set; }
    public string Società { get; set; }
    public string PartitaIva { get; set; }
    public Sede Sede { get; set; }
    public List<Referente> Lista_referenti { get; set; } = new List<Referente>();


    // Metodo __init__
    public Cliente()
    {
        // Metto qui l'init di base?
    }

    public void SetNickname()
    {
        Console.WriteLine("Inserire il nickname del cliente: ");
        string nick = Console.ReadLine();
        Nickname = nick;
    }

    public void SetIdCliente()
    {
        Console.WriteLine("Inserire l'id cliente: ");
        string ID = Console.ReadLine();
        Id_cliente = ID;
    }

    public void SetSocietà()
    {
        Console.WriteLine("Inserire il nome della società: ");
        string nome_società = Console.ReadLine();
        Società = nome_società;
    }

    public void SetPartitaIva()
    {
        Console.WriteLine("Inserire la partita iva del cliente: ");
        string partita_iva = Console.ReadLine();
        PartitaIva = partita_iva;
    }

    public void SetSede()
    {
        Sede sede = new Sede();
        sede.CompilaSede();
        Sede = sede;
    }

    public void AddReferente()
    {
        Referente referente = new Referente();
        referente.CompilaReferente();
        Lista_referenti.Add(referente);
    }

    public void CompilaCliente()
    {
        SetNickname();
        SetIdCliente();
        SetSocietà();
        SetPartitaIva();
        SetSede();
        AddReferente();

        Console.WriteLine("Aggungere un altro referente? [Si/si/No/no] ");
        string risposta = Console.ReadLine();

        while (risposta.ToLower() == "si")
        {
            AddReferente();
            Console.WriteLine("Aggungere un altro referente? [Si/si/No/no] ");
            risposta = Console.ReadLine();
        }
    }

    public void CompilaClienteFromXml(string nickname)
    {
        XmlDocument xdoc = new XmlDocument();
        xdoc.Load($"{Program.ClientPath}\\{nickname}.xml");

        XmlNode node_cliente = xdoc.SelectSingleNode("cliente");

        XmlAttribute nick = node_cliente.Attributes["nickname"];
        XmlAttribute id = node_cliente.Attributes["id_cliente"];
        XmlAttribute partita_iva = node_cliente.Attributes["partita_iva"];

        Nickname = nick.Value;
        Id_cliente = id.Value;
        PartitaIva = partita_iva.Value;

        XmlNode node_società = xdoc.SelectSingleNode("cliente/società");

        XmlNode node_stato = xdoc.SelectSingleNode("cliente/sede/stato");
        XmlNode node_regione = xdoc.SelectSingleNode("cliente/sede/provincia");
        XmlNode node_città = xdoc.SelectSingleNode("cliente/sede/città");
        XmlNode node_indirizzo = xdoc.SelectSingleNode("cliente/sede/indirizzo");
        XmlNode node_cap = xdoc.SelectSingleNode("cliente/sede/cap");

        Sede sede = new Sede(node_stato.InnerText, node_regione.InnerText, node_città.InnerText,
                             node_indirizzo.InnerText, node_cap.InnerText);

        Società = node_società.InnerText;
        Sede = sede;

        XmlNodeList nodes_referenti = xdoc.SelectNodes("cliente/referenti/referente");

        foreach (XmlNode node_referente in nodes_referenti)
        {
            XmlNodeList nodes = node_referente.ChildNodes;

            XmlNode node_nome = nodes[0];       //XmlElement node_nome = (XmlElement)nodes[0]; sarebbe ok!
            XmlNode node_cognome = nodes[1];
            XmlNode node_email = nodes[2];
            XmlNode node_telefono = nodes[3];

            Referente referente = new Referente(node_nome.InnerText, node_cognome.InnerText,
                                                node_email.InnerText, node_telefono.InnerText);

            Lista_referenti.Add(referente);
        }
    }
}