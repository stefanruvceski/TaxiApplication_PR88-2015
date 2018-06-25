using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public static class DataBase
    {
        #region svi posebno
        //public static Dictionary<string, Musterija> Musterije = new Dictionary<string, Musterija>()
        //{
        //    {"musterija1", new Musterija("musterija1","lozinka1","Ime1","Prezime1","m",12345,"065111111","email11@gmail.com") },
        //    {"musterija2", new Musterija("musterija2","lozinka2","Ime2","Prezime2","z",123456,"065111112","email12@gmail.com") },
        //    {"musterija3", new Musterija("musterija3","lozinka3","Ime3","Prezime3","m",123457,"065111113","email13@gmail.com") },
        //    {"musterija4", new Musterija("musterija4","lozinka4","Ime4","Prezime4","z",123458,"065111114","email14@gmail.com") },
        //    {"musterija5", new Musterija("musterija5","lozinka5","Ime5","Prezime5","m",123459,"065111115","email15@gmail.com") },
        //};

        //public static Dictionary<string, Dispecer> Dispeceri = new Dictionary<string, Dispecer>()
        //{
        //    {"dispecer1", new Dispecer("dispecer1","lozinka1","Ime1","Prezime1","m",123455,"065111111","email21@gmail.com") },
        //    {"dispecer2", new Dispecer("dispecer2","lozinka2","Ime2","Prezime2","z",1234556,"065111112","email22@gmail.com") },
        //    {"dispecer3", new Dispecer("dispecer3","lozinka3","Ime3","Prezime3","m",1234557,"065111113","email23@gmail.com") },
        //};

        //public static Dictionary<string, Vozac> Vozaci = new Dictionary<string, Vozac>()
        //{
        //    {"vozac1", new Vozac("vozac1","lozinka1","Ime1","Prezime1","m",123456,"065111111","email31@gmail.com",new Automobil(111,2011,"NS-010-LE","vozac1",TipoviAutomobila.PutnickiAutomobil)) },
        //    {"vozac2", new Vozac("vozac2","lozinka2","Ime2","Prezime2","z",1234566,"065111112","email32@gmail.com",new Automobil(222,2012,"NS-020-LE","vozac2",TipoviAutomobila.KombiVozilo)) },
        //    {"vozac3", new Vozac("vozac3","lozinka3","Ime3","Prezime3","m",1234567,"065111113","email33@gmail.com",new Automobil(333,2013,"NS-030-LE","vozac3",TipoviAutomobila.PutnickiAutomobil)) },
        //};
        #endregion

        #region Default Objects
        static Korisnik defaultKorisnik = new Vozac("-1", "-1", "-1", "-1", "m", "-1", "-1", "-1", -1);
        static Automobil defualtAutomobil = new Automobil(-1, -1, "Nepoznatno", "Nedefinisano", TipoviAutomobila.PutnickiAutomobil);
        static Voznja defaultVoznja = new Voznja(-1, "-1", -1, "-1");
        static Lokacija defaultLokacija = new Lokacija("-1", "-1");
        static Adresa defaultAdresa = new Adresa("-1", -1, "-1", "-1", -1);
        static Komentar defaultKomentar = new Komentar(-1, "-1", new DateTime(), "-1", -1);
        #endregion

        public static Dictionary<string, Korisnik> Korisnici = new Dictionary<string, Korisnik>()
        {
            

        };


        public static Dictionary<int, Voznja> voznje = new Dictionary<int, Voznja>();
        public static Dictionary<string, Lokacija> lokacije = new Dictionary<string, Lokacija>();
        public static Dictionary<string, Adresa> adrese = new Dictionary<string, Adresa>();
        public static Dictionary<int, Automobil> automobili = new Dictionary<int, Automobil>();
        public static Dictionary<int, Komentar> komentari = new Dictionary<int, Komentar>();
        

        public static void Run()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                string line;
                
                using (System.IO.TextReader readFile = new StreamReader(@"D:\GitHub\III Godina\VI Semestar\TaxiApplication_PR88-2015\TaxiApplication\TaxiApplication\Models\Databases\korisnici.txt"))
                {
                    Korisnik k = null;
                    while (true)
                    {
                        line = readFile.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        if (line.Split('|')[8] == "Vozac")
                            k = Vozac.FromString(line);
                        else if (line.Split('|')[8] == "Musterija")
                            k = Musterija.FromString(line);
                        else
                            k = Korisnik.FromString(line);

                        Korisnici.Add(k.KorisnickoIme, k);
                    }
                }

                using (System.IO.TextReader readFile = new StreamReader(@"D:\GitHub\III Godina\VI Semestar\TaxiApplication_PR88-2015\TaxiApplication\TaxiApplication\Models\Databases\voznje.txt"))
                {
                    while (true)
                    {
                        line = readFile.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        Voznja k = Voznja.FromString(line);
                        voznje.Add(k.Id, k);
                    }
                }

                using (System.IO.TextReader readFile = new StreamReader(@"D:\GitHub\III Godina\VI Semestar\TaxiApplication_PR88-2015\TaxiApplication\TaxiApplication\Models\Databases\lokacije.txt"))
                {
                    while (true)
                    {
                        line = readFile.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        Lokacija k = Lokacija.FromString(line);
                        lokacije.Add(k.Id, k);
                    }
                }

                using (System.IO.TextReader readFile = new StreamReader(@"D:\GitHub\III Godina\VI Semestar\TaxiApplication_PR88-2015\TaxiApplication\TaxiApplication\Models\Databases\adrese.txt"))
                {
                    while (true)
                    {
                        line = readFile.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        Adresa k = Adresa.FromString(line);
                        adrese.Add(k.Id, k);
                    }
                }

                using (System.IO.TextReader readFile = new StreamReader(@"D:\GitHub\III Godina\VI Semestar\TaxiApplication_PR88-2015\TaxiApplication\TaxiApplication\Models\Databases\automobili.txt"))
                {
                    while (true)
                    {
                        line = readFile.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        Automobil k = Automobil.FromString(line);
                        automobili.Add(k.BrojTaksiVozila, k);
                    }
                }

                using (System.IO.TextReader readFile = new StreamReader(@"D:\GitHub\III Godina\VI Semestar\TaxiApplication_PR88-2015\TaxiApplication\TaxiApplication\Models\Databases\komentari.txt"))
                {
                    while (true)
                    {
                        line = readFile.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        Komentar k = Komentar.FromString(line);
                        komentari.Add(k.Id, k);
                    }
                }

                while (true)
                {
                    using (TextWriter tw = new StreamWriter(@"D:\GitHub\III Godina\VI Semestar\TaxiApplication_PR88-2015\TaxiApplication\TaxiApplication\Models\Databases\korisnici.txt"))
                    {
                        foreach (var item in Korisnici.Values.ToList())
                        {
                            tw.WriteLine(item.ToString());
                        }
                    }

                    using (TextWriter tw = new StreamWriter(@"D:\GitHub\III Godina\VI Semestar\TaxiApplication_PR88-2015\TaxiApplication\TaxiApplication\Models\Databases\voznje.txt"))
                    {
                        foreach (var item in voznje.Values.ToList())
                        {
                            tw.WriteLine(item.ToString());
                        }
                    }

                    using (TextWriter tw = new StreamWriter(@"D:\GitHub\III Godina\VI Semestar\TaxiApplication_PR88-2015\TaxiApplication\TaxiApplication\Models\Databases\Lokacije.txt"))
                    {
                        foreach (var item in lokacije.Values.ToList())
                        {
                            tw.WriteLine(item.ToString());
                        }
                    }

                    using (TextWriter tw = new StreamWriter(@"D:\GitHub\III Godina\VI Semestar\TaxiApplication_PR88-2015\TaxiApplication\TaxiApplication\Models\Databases\Adrese.txt"))
                    {
                        foreach (var item in adrese.Values.ToList())
                        {
                            tw.WriteLine(item.ToString());
                        }
                    }

                    using (TextWriter tw = new StreamWriter(@"D:\GitHub\III Godina\VI Semestar\TaxiApplication_PR88-2015\TaxiApplication\TaxiApplication\Models\Databases\Automobili.txt"))
                    {
                        foreach (var item in automobili.Values.ToList())
                        {
                            tw.WriteLine(item.ToString());
                        }
                    }

                    using (TextWriter tw = new StreamWriter(@"D:\GitHub\III Godina\VI Semestar\TaxiApplication_PR88-2015\TaxiApplication\TaxiApplication\Models\Databases\Komentari.txt"))
                    {
                        foreach (var item in komentari.Values.ToList())
                        {
                            tw.WriteLine(item.ToString());
                        }
                    }
                    Thread.Sleep(10000);
                }                
            }).Start();
        }

    }
}