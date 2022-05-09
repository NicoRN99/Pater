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
using Pater.DataModel;

namespace Pater
{
    internal static class XmlFunctions
    {

        internal static void UpdateXmlText(XmlDocument xdoc, string XmlPath, string text)    // Perchè internal qui? non basta davanti alla classe???
        {
            XmlNode node = xdoc.SelectSingleNode(XmlPath);
            node.InnerText = text;
        }



        internal static void AddReferente2Xml(XmlDocument xdoc, XmlNode node_referenti, Referente referente, int id_value)
        {
            XmlElement node_referente = xdoc.CreateElement("referente");

            XmlAttribute id = xdoc.CreateAttribute("id");
            id.Value = id_value.ToString();
            node_referente.Attributes.Append(id);

            node_referenti.AppendChild(node_referente);


            XmlElement node_nome = xdoc.CreateElement("nome");
            node_nome.InnerText = referente.Nome;
            node_referente.AppendChild(node_nome);

            XmlElement node_cognome = xdoc.CreateElement("cognome");
            node_cognome.InnerText = referente.Cognome;
            node_referente.AppendChild(node_cognome);

            XmlElement node_email = xdoc.CreateElement("email");
            node_email.InnerText = referente.Email;
            node_referente.AppendChild(node_email);

            XmlElement node_telefono = xdoc.CreateElement("telefono");
            node_telefono.InnerText = referente.Telefono;
            node_referente.AppendChild(node_telefono);
        }



        internal static void SalvaXmlCliente(Cliente client)
        {
            string path2template = $"{Program.TemplatePath}\\template_cliente.xml";
            string path2save = $"{Program.ClientPath}\\{client.Nickname}.xml";

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path2template);

            // Aggiungere nick, id e partita iva
            XmlAttribute nick = xdoc.CreateAttribute("nickname");
            XmlAttribute id = xdoc.CreateAttribute("id_cliente");
            XmlAttribute partitaiva = xdoc.CreateAttribute("partita_iva");
            nick.InnerText = client.Nickname;
            id.InnerText = client.Id_cliente;
            partitaiva.InnerText = client.PartitaIva;

            XmlNode node_cliente = xdoc.SelectSingleNode("cliente");
            node_cliente.Attributes.Append(nick);
            node_cliente.Attributes.Append(id);
            node_cliente.Attributes.Append(partitaiva);


            UpdateXmlText(xdoc, "cliente/società", client.Società);
            UpdateXmlText(xdoc, "cliente/sede/stato", client.Sede.Stato);
            UpdateXmlText(xdoc, "cliente/sede/provincia", client.Sede.Provincia);
            UpdateXmlText(xdoc, "cliente/sede/città", client.Sede.Città);
            UpdateXmlText(xdoc, "cliente/sede/indirizzo", client.Sede.Indirizzo);
            UpdateXmlText(xdoc, "cliente/sede/cap", client.Sede.Cap);

            int id_counter = 1;
            XmlNode referenti = xdoc.SelectSingleNode("cliente/referenti");

            foreach (Referente referente in client.Lista_referenti)
            {
                AddReferente2Xml(xdoc, referenti, referente, id_counter);
                id_counter++;

            }

            xdoc.Save(path2save);
        }



        internal static void CompilaClienteXml(Offerta offerta, XmlDocument xdoc)
        {
            // Aggiungere nick e id
            XmlAttribute nick = xdoc.CreateAttribute("nickname");
            XmlAttribute id = xdoc.CreateAttribute("id_cliente");
            nick.InnerText = offerta.Cliente.Nickname;
            id.InnerText = offerta.Cliente.Id_cliente;

            XmlNode node_cliente = xdoc.SelectSingleNode("offerta/cliente");
            node_cliente.Attributes.Append(nick);
            node_cliente.Attributes.Append(id);

            UpdateXmlText(xdoc, "offerta/cliente/società", offerta.Cliente.Società);   /////////////////// Controlla qui

            UpdateXmlText(xdoc, "offerta/cliente/sede/stato", offerta.Cliente.Sede.Stato);
            UpdateXmlText(xdoc, "offerta/cliente/sede/provincia", offerta.Cliente.Sede.Provincia);
            UpdateXmlText(xdoc, "offerta/cliente/sede/città", offerta.Cliente.Sede.Città);
            UpdateXmlText(xdoc, "offerta/cliente/sede/indirizzo", offerta.Cliente.Sede.Indirizzo);
            UpdateXmlText(xdoc, "offerta/cliente/sede/cap", offerta.Cliente.Sede.Cap);

            XmlNode node_referenti = xdoc.SelectSingleNode("offerta/cliente/referenti");

            foreach (Referente referente in offerta.Cliente.Lista_referenti)
            {
                AddReferente2Xml(xdoc, node_referenti, referente, offerta.Cliente.Lista_referenti.IndexOf(referente) + 1);
            }
        }

        internal static void AddArticolo2Xml(XmlDocument xdoc, Articolo articolo)
        {
            XmlNode node_articoli = xdoc.SelectSingleNode("offerta/articoli");
            XmlElement node_articolo = xdoc.CreateElement("articolo");

            node_articoli.AppendChild(node_articolo);

            XmlElement modello = xdoc.CreateElement("modello");
            modello.InnerText = articolo.Modello;
            node_articolo.AppendChild(modello);

            XmlElement tipovendita = xdoc.CreateElement("tipovendita");
            tipovendita.InnerText = articolo.TipoVendita;
            node_articolo.AppendChild(tipovendita);

            XmlElement descrizione = xdoc.CreateElement("descrizione");
            descrizione.InnerText = articolo.Descrizione;
            node_articolo.AppendChild(descrizione);

            XmlElement codice_articolo = xdoc.CreateElement("codice_articolo");
            codice_articolo.InnerText = articolo.CodiceArticolo;
            node_articolo.AppendChild(codice_articolo);

            XmlElement quantità = xdoc.CreateElement("quantità");
            quantità.InnerText = articolo.Quantità;
            node_articolo.AppendChild(quantità);

            XmlElement listino = xdoc.CreateElement("listino");
            listino.InnerText = articolo.Listino;
            node_articolo.AppendChild(listino);

            XmlElement costoVG = xdoc.CreateElement("costoVG");
            costoVG.InnerText = articolo.CostoVG;
            node_articolo.AppendChild(costoVG);

            XmlElement prezzo_di_vendita = xdoc.CreateElement("prezzo_di_vendita");
            prezzo_di_vendita.InnerText = articolo.PrezzoDiVendita;
            node_articolo.AppendChild(prezzo_di_vendita);

            XmlElement percentuale_scontoVG = xdoc.CreateElement("percentuale_scontoVG");
            percentuale_scontoVG.InnerText = articolo.PercentualeScontoVG;
            node_articolo.AppendChild(percentuale_scontoVG);

            XmlElement prezzo_totale = xdoc.CreateElement("prezzo_totale");
            prezzo_totale.InnerText = articolo.PrezzoTotale;
            node_articolo.AppendChild(prezzo_totale);
        }

        internal static void SalvaXmlFile(Offerta offerta)
        {
            DateTime dataora = DateTime.Now;  // Da correggere qui, prendere la data dal protocollo oppure mettere la data come property

            string path2template = $"{Program.TemplatePath}\\template_offerta.xml";
            string path2save = $"{Program.DataPath}\\Offerte\\{offerta.Protocollo}.xml";

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path2template);

            //Inserire protocollo e data 
            XmlAttribute protocollo = xdoc.CreateAttribute("protocollo");
            XmlAttribute data = xdoc.CreateAttribute("data");
            protocollo.InnerText = offerta.Protocollo;
            data.InnerText = dataora.Date.ToString()[..10];

            XmlNode node_offerta = xdoc.SelectSingleNode("offerta");
            node_offerta.Attributes.Append(protocollo);
            node_offerta.Attributes.Append(data);

            CompilaClienteXml(offerta, xdoc);

            foreach (Articolo articolo in offerta.Lista_articoli)
            {
                AddArticolo2Xml(xdoc, articolo);
            }

            xdoc.Save(path2save);
        }

        internal static void UpdateFirstTextOfPara(Paragraph para, string new_text)
        {
            Text text = (Text)para.GetFirstChild<Run>().GetFirstChild<Text>();
            text.Text = new_text;
        }

        internal static void AddArticolo2WordTable(Table table, TableRow row, Articolo articolo)
        {
            /// clone_row è il clone della seconda riga della tabella del template_word, e contiene le 4 celle con la relativa formattazione.
            /// Aggiungenendo una terza riga da 3 celle nel template, si porta scegliere quale clone usare per far scegliere all'utente se indicare 
            /// prezzo totale e cada uno, o solo prezzo totale.

            TableRow clone_row = (TableRow)row.Clone();

            TableCell cell_descrizione = clone_row.Elements<TableCell>().ElementAt(0);
            TableCell cell_quantità = clone_row.Elements<TableCell>().ElementAt(1);
            TableCell cell_cad = clone_row.Elements<TableCell>().ElementAt(2);
            TableCell cell_totale = clone_row.Elements<TableCell>().ElementAt(3);

            Text text_descrizione = cell_descrizione.Elements<Paragraph>().First().Elements<Run>().First().Elements<Text>().First();
            text_descrizione.Text = articolo.Descrizione;

            Text text_quantità = cell_quantità.Elements<Paragraph>().First().Elements<Run>().First().Elements<Text>().First();
            text_quantità.Text = articolo.Quantità;

            Text text_cad = cell_cad.Elements<Paragraph>().First().Elements<Run>().First().Elements<Text>().First();
            text_cad.Text = "€ " + articolo.PrezzoDiVendita;

            Text text_totale = cell_totale.Elements<Paragraph>().First().Elements<Run>().First().Elements<Text>().First();
            text_totale.Text = "€ " + articolo.PrezzoTotale;

            table.AppendChild(clone_row);
        }


        internal static void SaveWordFileOpenXML(Offerta offer)
        {
            string path2template = $"{Program.TemplatePath}\\template_word.docx";
            string path2save = $"{Program.DataPath}\\Ordini\\{offer.Protocollo}.docx";

            WordprocessingDocument xdoc = WordprocessingDocument.CreateFromTemplate(path2template);
            
            /*
            // ----------------- Debug per capire struttura del template ------------------
            var pa = xdoc.MainDocumentPart.Document.Body.Elements<Paragraph>();

            int counter = 0;
            foreach (var p in pa)
            {
                Console.WriteLine($"__{counter}******************************************************__{counter}");
                Console.WriteLine(p.InnerText);
                Console.WriteLine($"__{counter}******************************************************__{counter}");
                counter++;
            }
            // ----------------- Debug per capire struttura del template ------------------
            */

            var paralist = xdoc.MainDocumentPart.Document.Body.ChildElements;

            Paragraph para_società = (Paragraph)paralist[1];
            Paragraph para_indirizzo = (Paragraph)paralist[2];
            Paragraph para_cap = (Paragraph)paralist[3];
            Paragraph para_piva = (Paragraph)paralist[4];
            Paragraph para_ca = (Paragraph)paralist[6];
            Paragraph para_data = (Paragraph)paralist[10];
            Paragraph para_proto = (Paragraph)paralist[13];
            Paragraph para_ogg = (Paragraph)paralist[17];
            Paragraph para_dffm = (Paragraph)paralist[52];
            Paragraph para_dataoff = (Paragraph)paralist[53];
            Paragraph para_dispo = (Paragraph)paralist[54];

            UpdateFirstTextOfPara(para_società, $"{offer.Cliente.Società}");
            UpdateFirstTextOfPara(para_indirizzo, $"{offer.Cliente.Sede.Indirizzo}");
            UpdateFirstTextOfPara(para_cap, $"{offer.Cliente.Sede.Cap} {offer.Cliente.Sede.Città} {offer.Cliente.Sede.Provincia}");
            UpdateFirstTextOfPara(para_piva, $"P. Iva {offer.Cliente.PartitaIva}");
            UpdateFirstTextOfPara(para_ca, $"C.A {offer.Cliente.Lista_referenti[0].Nome} {offer.Cliente.Lista_referenti[0].Cognome}");
            UpdateFirstTextOfPara(para_data, $"Milano: {DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}");
            UpdateFirstTextOfPara(para_proto, $"Protocollo: {offer.Protocollo}");
            UpdateFirstTextOfPara(para_ogg, $"Oggetto: {offer.Oggetto}");
            UpdateFirstTextOfPara(para_dffm, $"Modalità di pagamento: bonifico bancario {offer.Modalità_pagamento} giorni data fattura fine mese");
            UpdateFirstTextOfPara(para_dataoff, $"Validità offerta: {offer.Validità_Offerta}");
            UpdateFirstTextOfPara(para_dispo, $"Disponibilità: {offer.Disponibilità} giorni data ricevimento ordine, salvo esaurimento merce");


            // Aggiungere articoli alla tabella

            Table table = xdoc.MainDocumentPart.Document.Body.Elements<Table>().First();
            TableRow row_to_be_cloned = table.Elements<TableRow>().ElementAt(1);

            TableRow cloned_row = (TableRow)row_to_be_cloned.Clone();

            row_to_be_cloned.Remove();

            foreach (Articolo article in offer.Lista_articoli)
            {
                AddArticolo2WordTable(table, cloned_row, article);
            }


            var newdoc = xdoc.SaveAs(path2save);
            newdoc.Close();
            xdoc.Close();
            newdoc = null;
            xdoc = null;

            /* WordprocessingDocument x = (WordprocessingDocument)xdoc.Clone();
             * Non c'è necessità di clonare perché è utilizzato dal metodo SaveAs, che infatti restituisce il packeìage OpenXml clonato,
             * sarebbe comunque stata la strada giusta. Il problema era che non assegnavo una variabile al file che clonavo e non 
             * potevo quindi controllarne la chiusura
             */
        }
    }
}
