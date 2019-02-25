using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication11
{
    class Program
    {
        static void Main(string[] args)
        {
        
            ///<summary>
            ///Main Methode um das Programm zu initialiesieren bzw. Methoden aufzurufen.
            ///Abfrage welche aktion bzw Methode der Nutzer ausführen wil,
            ///mithilfe von auslesen von Benutzereingaben.
            ///</summary>
            ISBN isbn = new ISBN();
            EAN ean = new EAN();
            string input;
            string state;
            Console.WriteLine("Auswahl:" + "\n" + "ISBN: 1" + "\n" + "EAN: 2");
            input = Console.ReadLine();
            if (input == "1")
            {
                state = "ISBN";
            }
            else if (input == "2")
            {
                state = "EAN";
            }
            else
            {
                Console.WriteLine("Eingabe nicht erkannt");
                return;
            }
            if (state == "EAN" || state == "ISBN")
            {
                Console.WriteLine("Modus: " + state.ToString());
                Console.WriteLine("Auswahl:" + "\n" + "Datei lesen: 1" + "\n" + "Liste Generieren: 2" + "\n" + "Einzelne Nummer prüfen: 3");
                input = Console.ReadLine();
                if (input == "1" || input == "2" || input == "3")
                {
                    if (state == "ISBN" && isbn != null)
                    {

                        if (input == "1")
                        {
                            Console.WriteLine("Dateiname eingeben");
                            string File = Console.ReadLine().ToString();
                            string[] ISBNListe = isbn.ISBNreader(File);
                            string[,] ISBNListe_fertig = isbn.CheckISBNListe(ISBNListe);
                            isbn.ISBNwriter(ISBNListe_fertig);
                        }
                        else if (input == "2")
                        {
                            Console.WriteLine("Dateiname eingeben");
                            string File = Console.ReadLine().ToString();
                            isbn.ISBNGenerator(File);
                        }
                        else if (input == "3")
                        {
                            Console.WriteLine("ISBN Nummer eingeben");
                            string ISBN = Console.ReadLine().ToString();
                            isbn.CheckISBN(ISBN);
                        }
                    }
                    else if (state == "EAN" && ean != null)
                    {
                        if (input == "1")
                        {
                            Console.WriteLine("Dateiname eingeben");
                            string File = Console.ReadLine().ToString();
                            string[] EANListe = ean.EANreader(File);
                            string[,] EANListe_fertig = ean.CheckEANListe(EANListe);
                            ean.EANwriter(EANListe_fertig);
                        }
                        else if (input == "2")
                        {
                            Console.WriteLine("Dateiname eingeben");
                            string File = Console.ReadLine().ToString();
                            ean.EANGenerator(File);

                        }
                        else if (input == "3")
                        {
                            Console.WriteLine("EAN Nummer eingeben");
                            string EAN = Console.ReadLine().ToString();
                            ean.CheckEAN(EAN);
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Eingabe nicht erkannt");
                    Console.ReadLine();
                }

                Console.ReadLine();
            }
        }
    }
    class ISBN
    {
        public void ISBNwriter(string[,] ISBNListe_fertig)
        {
            ///<param name="EANListe_fertig">Ausgewertete EAN Liste als Matrix</param>
            ///<summary>
            ///Lesen eines EAN Arrays und anschließendes schreiben in eine csv Datei.
            ///</summary>
            using (StreamWriter sw = new StreamWriter("ISBN_Fertig" + DateTime.Now.ToString("yyyy-dd-M/HH-mm-ss") + ".csv"))
            {
                for (int p = 0; p < ISBNListe_fertig.GetLength(0); p++)
                {
                    if (ISBNListe_fertig[p, 0] != null && ISBNListe_fertig[p, 1] != null)
                    {
                        var line = String.Format("{0};{1}", ISBNListe_fertig[p, 0], ISBNListe_fertig[p, 1]);
                        sw.WriteLine(line);
                    }
                    else if (ISBNListe_fertig[p, 0] == null)
                    {
                        var line = String.Format("{0};{1}", "", ISBNListe_fertig[p, 1]);
                        sw.WriteLine(line);
                    }
                    else if (ISBNListe_fertig[p, 1] == null)
                    {
                        var line = String.Format("{0};{1}", ISBNListe_fertig[p, 0], "");
                        sw.WriteLine(line);
                    }

                }
            }

        }

        public string[] ISBNreader(string file)
        {
            ///<param name="file">Dateiname unter der die ISBN Liste zu finden ist</param>
            ///<summary>
            ///Lesen einer Datei um ISBN Nummern auszulessn und anschließendes Konvertieren in ein Array.
            ///</summary>
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;

                    List<string> ISBNlist = new List<string>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        ISBNlist.Add(line);
                    }
                    string[] ISBNNumbers = ISBNlist.ToArray();

                    return ISBNNumbers;
                    ///<return>Rückgabe bzw. Ausgabe des aus der Datei gelesenen ISBN Arrays</return>
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Datei konnte nicht gefunden werden");
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw new FileNotFoundException();
                ///<return>Rückgabe bzw. Ausgabe das die Datei nicht gefunden werden konnte</return>
            }
        }

        public void ISBNGenerator(string file)
        {
            ///<param name="file">Dateiname unter der die Generierten ISBN Nummern speichern zu sind</param>
            ///<summary>
            ///Generieren einer Liste mit 500 ISBN Elementen die zufällig erstellt werden und im Nachhinein in eine Datei gespeichert werden.
            ///</summary>
            string[] ISBNListeGenerated = new string[500];

            var random = new Random();
            for (int i = 0; i < ISBNListeGenerated.Length; i++)
            {
                string ISBN = "";
                for (int j = 0; j < 10; j++)
                {
                    ISBN += random.Next(10).ToString();
                }

                ISBNListeGenerated[i] = ISBN;
            }
            using (StreamWriter sw = new StreamWriter(file + ".csv"))
            {
                for (int p = 0; p < ISBNListeGenerated.Length; p++)
                {
                    sw.WriteLine(ISBNListeGenerated[p]);
                }
            }
            Console.WriteLine("Liste geschrieben zu: " + file + ".csv");
            Console.ReadLine();
            ///<return>Rückgabe bzw. Ausgabe des Dateinamens</return>
        }

        public void CheckISBN(string ISBN)
        {
            ///<param name="ISBN">ISBN Nummer die zu prüfen ist</param>
            ///<summary>
            ///Prüfen einer ISBN Nummer bzw berechnen der Prüfziffer 
            ///</summary>
            var CheckNum = 0;

            for (int j = 0; j < ISBN.Length; j++)
            {
                if (j < 9)
                {
                    CheckNum += Int32.Parse(ISBN[j].ToString()) * (j + 1);
                    Console.WriteLine("Prüfziffer: " + CheckNum);
                }
            }
            CheckNum = CheckNum % 11;
            
            if (ISBN.Length == 10)
            {
                if (CheckNum == 10 && ISBN[9].ToString() == "X" || ISBN[9].ToString() != "X" && CheckNum.ToString() == ISBN[9].ToString())
                {
                    if (CheckNum == 10)
                    {
                        Console.WriteLine("Richtig " + ISBN);
                        Console.WriteLine("Prüfziffer: " + "X");
                        ///<return>Rückgabe bzw. Ausgabe der Richtigen ISBN Nummer und ihrer Prüfziffer</return>
                    }
                    else
                    {
                        Console.WriteLine("Richtig " + ISBN);
                        Console.WriteLine("Prüfziffer: " + CheckNum);
                        ///<return>Rückgabe bzw. Ausgabe der Richtigen ISBN Nummer und ihrer Prüfziffer</return>
                    }
                }
                else
                {
                    if (CheckNum == 10)
                    {
                        Console.WriteLine("Falsch " + ISBN);
                        Console.WriteLine("Prüfziffer: " + "X");
                        ///<return>Rückgabe bzw. Ausgabe der Falschen ISBN Nummer und X da die Prüfziffer = 10 ist</return>
                    }
                    else
                    {
                        Console.WriteLine("Falsch " + ISBN);
                        Console.WriteLine("Prüfziffer: " + CheckNum);
                        ///<return>Rückgabe bzw. Ausgabe der Falschen ISBN Nummer und ihrer Richtigen Prüfziffer</return>
                    }
                }
            }
            else
            {
                if (CheckNum == 10)
                {
                    Console.WriteLine("ISBN: " + ISBN);
                    Console.WriteLine("Prüfziffer: " + "X");
                    ///<return>Rückgabe bzw. Ausgabe der ISBN Nummer und X da die Prüfziffer = 10 ist</return>
                }
                else
                {
                    Console.WriteLine("ISBN: " + ISBN);
                    Console.WriteLine("Prüfziffer: " + CheckNum);
                    ///<return>Rückgabe bzw. Ausgabe der ISBN Nummer und ihrer Prüfziffer</return>
                }

            }
        }

        public string[,] CheckISBNListe(string[] liste)
        {
            ///<param name="liste">Liste von ISBN Nummern die zu prüfen sind</param>
            ///<summary>
            ///Prüfen einer Liste von ISBN Nummern bzw berechnen der Prüfziffern und unterteilen in ein Array in Richtig und Falsch
            /// </summary>
            Console.WriteLine("Checke ISBN-Liste");

            List<string> rList = new List<string>();
            List<string> fList = new List<string>();
            rList.Add("Richtig:");
            fList.Add("Falsch:");
            if (liste.Length != 0)
            {

                for (int i = 0; i < liste.Length; i++)
                {
                    var ISBNchar = liste[i].ToString();
                    int CheckNum = 0;
                    for (int j = 0; j < ISBNchar.Length; j++)
                    {
                        if (j < 9)
                        {
                            CheckNum += Int32.Parse(ISBNchar[j].ToString()) * (j + 1);
                        }
                    }
                    CheckNum = CheckNum % 11;
                    if (ISBNchar.Length == 10)
                    {
                        if (CheckNum == 10 && ISBNchar[9].ToString() == "X" || ISBNchar[9].ToString() != "X" && CheckNum.ToString() == ISBNchar[9].ToString())
                        {
                            rList.Add(liste[i].ToString());
                        }
                        else
                        {
                            fList.Add(liste[i].ToString());
                        }
                    }
                    else
                    {
                        if (CheckNum == 10)
                        {
                            rList.Add(liste[i].ToString() + "X");
                        }
                        else
                        {
                            rList.Add(liste[i].ToString() + CheckNum);
                        }
                    }
                }


                Console.WriteLine("Fertig mit Checken von ISBN-Liste");
                string[] rArray = rList.ToArray();
                string[,] ISBNListe_fertig;
                string[] fArray = fList.ToArray();
                int array_length;
                if (rArray.Length > fArray.Length)
                {
                    array_length = rArray.Length;
                }
                else
                {
                    array_length = fArray.Length;
                }

                ISBNListe_fertig = new string[array_length, 2];
                for (int u = 0; u < rArray.Length; u++)
                {
                    ISBNListe_fertig[u, 0] = rArray[u].ToString();
                }
                for (int h = 0; h < fArray.Length; h++)
                {
                    ISBNListe_fertig[h, 1] = fArray[h].ToString();
                }
                Console.WriteLine("Verarbeiten in Richtig und Falsch fertig");
                return ISBNListe_fertig;
                ///<return>Rückgabe bzw. Ausgabe der bearbeiteten ISBN Liste in Richtig und Falsch unterteilt</return>


            }
            else
                Console.WriteLine("Liste ist leer");
            Console.ReadLine();
            return null;
            ///<return>keine Rückgabe da die Liste leer ist und Nachricht an den Nutzer</return>
        }

    }

    class EAN
    {

        public void EANwriter(string[,] EANListe_fertig)
        {

            ///<param name="EANListe_fertig">Ausgewertete EAN Liste als Matrix</param>
            ///<summary>
            ///Lesen eines EAN Arrays und anschließendes schreiben in eine csv Datei.
            ///</summary>
            using (StreamWriter sw = new StreamWriter("EAN_Fertig" + DateTime.Now.ToString("yyyy-dd-M/HH-mm-ss") + ".csv"))
            {
                for (int p = 0; p < EANListe_fertig.GetLength(0); p++)
                {
                    if (EANListe_fertig[p, 0] != null && EANListe_fertig[p, 1] != null)
                    {
                        var line = String.Format("{0};{1}", EANListe_fertig[p, 0], EANListe_fertig[p, 1]);
                        sw.WriteLine(line);
                    }
                    else if (EANListe_fertig[p, 0] == null)
                    {
                        var line = String.Format("{0};{1}", "", EANListe_fertig[p, 1]);
                        sw.WriteLine(line);
                    }
                    else if (EANListe_fertig[p, 1] == null)
                    {
                        var line = String.Format("{0};{1}", EANListe_fertig[p, 0], "");
                        sw.WriteLine(line);
                    }

                }
            }

        }
        public string[] EANreader(string file)
        {
            ///<param name="file">Dateiname unter der die EAN Liste zu finden ist</param>
            ///<summary>
            ///Lesen einer Datei um EAN Nummern auszulessn und anschließendes Konvertieren in ein Array.
            ///</summary>
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;

                    List<string> EANlist = new List<string>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        EANlist.Add(line);
                    }
                    string[] EANNumbers = EANlist.ToArray();

                    return EANNumbers;
                    ///<return>Rückgabe bzw. Ausgabe des aus der Datei gelesenen EAN Arrays</return>
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Datei konnte nicht gefunden werden");
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw new FileNotFoundException();
                ///<return>Rückgabe bzw. Ausgabe das die Datei nicht gefunden werden konnte</return>
            }
        }

        public void EANGenerator(string file)
        {
            ///<param name="file">Dateiname unter der die Generierten EAN Nummern speichern zu sind</param>
            ///<summary>
            ///Generieren einer Liste mit 500 EAN Elementen die zufällig erstellt werden und im Nachhinein in eine Datei gespeichert werden.
            ///</summary>
            string[] EANListeGenerated = new string[500];
            var random = new Random();
            for (int i = 0; i < EANListeGenerated.Length; i++)
            {
                string EAN = "";
                for (int j = 0; j < 13; j++)
                {
                    EAN += random.Next(10).ToString();
                }
                EANListeGenerated[i] = EAN;
            }
            using (StreamWriter sw = new StreamWriter(file + ".csv"))
            {
                for (int p = 0; p < EANListeGenerated.Length; p++)
                {
                    sw.WriteLine(EANListeGenerated[p]);
                }
            }
            Console.WriteLine("Liste geschrieben zu: " + file + ".csv");
            Console.ReadLine();
            ///<return>Rückgabe bzw. Ausgabe des Dateinamens</return>
        }

        public void CheckEAN(string EAN)
        {

            ///<param name="EAN">EAN Nummer die zu prüfen ist</param>
            ///<summary>
            ///Prüfen einer EAN Nummer bzw berechnen der Prüfziffer 
            ///</summary>
            int CheckNum = 0;
            int z = 1;
            for (int j = 0; j < EAN.Length; j++)
            {
                if (j < 12)
                {
                    CheckNum += Int32.Parse(EAN[j].ToString()) * z;
                    if (z == 1)
                    {
                        z = 3;
                    }
                    else
                    {
                        z = 1;
                    }
                }
            }
            if (CheckNum % 10 != 0)
            {
                while (CheckNum % 10 != 0)
                {
                    CheckNum = CheckNum + 1 % 10;
                }
                CheckNum = CheckNum / 10;
            }
            else
            {
                CheckNum = CheckNum / 10;
            }
            if (EAN.Length == 13)
            {
                if (Int32.Parse(EAN[12].ToString()) == CheckNum)
                {

                    Console.WriteLine("Richtig " + EAN);
                    Console.WriteLine("Prüfziffer: " + CheckNum);
                    ///<return>Rückgabe bzw. Ausgabe der Richtigen EAN Nummer und ihrer Prüfziffer</return>
                }
                else
                {
                    Console.WriteLine("Falsch " + EAN);
                    Console.WriteLine("Prüfziffer: " + CheckNum);
                    ///<return>Rückgabe bzw. Ausgabe der Falschen EAN Nummer und ihrer Prüfziffer</return>
                }
            }
            else
            {
                Console.WriteLine("EAN: " + EAN);
                Console.WriteLine("Prüfziffer: " + CheckNum);
                ///<return>Rückgabe bzw. Ausgabe der EAN Nummer und ihrer Prüfziffer</return>
            }
        }



        public string[,] CheckEANListe(string[] liste)

        {
            ///<param name="liste">Liste von EAN Nummern die zu prüfen sind</param>
            ///<summary>
            ///Prüfen einer Liste von EAN Nummern bzw berechnen der Prüfziffern und unterteilen in ein Array in Richtig und Falsch
            /// </summary>
            Console.WriteLine("Checke EAN-Liste");

            List<string> rList = new List<string>();
            List<string> fList = new List<string>();
            rList.Add("Richtig:");
            fList.Add("Falsch:");
            if (liste.Length != 0)
            {

                for (int i = 0; i < liste.Length; i++)
                {
                    var EANchar = liste[i].ToString();
                    int CheckNum = 0;
                    int z = 1;
                    for (int j = 0; j < EANchar.Length; j++)
                    {

                        if (j < 12)
                        {
                            CheckNum += Int32.Parse(EANchar[j].ToString()) * z;
                            if (z == 1)
                            {
                                z = 3;
                            }
                            else
                            {
                                z = 1;
                            }
                        }
                    }
                    if (CheckNum % 10 != 0)
                    {
                        while (CheckNum % 10 != 0)
                        {
                            CheckNum = CheckNum + 1 % 10;
                        }
                        CheckNum = CheckNum / 10;
                    }
                    else
                    {
                        CheckNum = CheckNum / 10;
                    }
                    if (EANchar.Length == 13)
                    {
                        if (Int32.Parse(EANchar[12].ToString()) == CheckNum)
                        {
                            rList.Add(liste[i].ToString());
                        }
                        else
                        {
                            fList.Add(liste[i].ToString());
                        }
                    }
                    else
                    {
                        rList.Add(liste[i].ToString() + CheckNum);
                    }
                }

                Console.WriteLine("Fertig mit Checken von EAN-Liste");
                string[] rArray = rList.ToArray();
                string[,] EANListe_fertig;
                string[] fArray = fList.ToArray();
                int array_length;
                if (rArray.Length > fArray.Length)
                {
                    array_length = rArray.Length;
                }
                else
                {
                    array_length = fArray.Length;
                }

                EANListe_fertig = new string[array_length, 2];
                for (int u = 0; u < rArray.Length; u++)
                {
                    EANListe_fertig[u, 0] = rArray[u].ToString();
                }
                for (int h = 0; h < fArray.Length; h++)
                {
                    EANListe_fertig[h, 1] = fArray[h].ToString();
                }
                Console.WriteLine("Verarbeiten in Richtig und Falsch fertig");
                return EANListe_fertig;
                ///<return>Rückgabe bzw. Ausgabe der bearbeiteten EAN Liste in Richtig und Falsch unterteilt</return>
            }
            else
                Console.WriteLine("Liste ist leer");
                Console.ReadLine();
                return null;
                ///<return>keine Rückgabe da die Liste leer ist und Nachricht an den Nutzer</return>
        }

    }

}

