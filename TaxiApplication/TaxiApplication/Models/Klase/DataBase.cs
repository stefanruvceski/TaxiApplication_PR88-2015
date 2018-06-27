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
        #region Local Storage
        public static Dictionary<string, Korisnik> Korisnici = new Dictionary<string, Korisnik>();
        public static Dictionary<int, Voznja> voznje = new Dictionary<int, Voznja>();
        public static Dictionary<string, Lokacija> lokacije = new Dictionary<string, Lokacija>();
        public static Dictionary<string, Adresa> adrese = new Dictionary<string, Adresa>();
        public static Dictionary<int, Automobil> automobili = new Dictionary<int, Automobil>();
        public static Dictionary<int, Komentar> komentari = new Dictionary<int, Komentar>();
        #endregion

        #region Helpers
        public static void Run()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                string line;
                var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Models\Databases\korisnici.txt");
                using (System.IO.TextReader readFile = new StreamReader(fullPath))
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

                fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Models\Databases\voznje.txt");
                using (System.IO.TextReader readFile = new StreamReader(fullPath))
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

                fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Models\Databases\lokacije.txt");
                using (System.IO.TextReader readFile = new StreamReader(fullPath))
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

                fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Models\Databases\adrese.txt");
                using (System.IO.TextReader readFile = new StreamReader(fullPath))
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

                fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Models\Databases\automobili.txt");
                using (System.IO.TextReader readFile = new StreamReader(fullPath))
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

                fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Models\Databases\komentari.txt");
                using (System.IO.TextReader readFile = new StreamReader(fullPath))
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
                    fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Models\Databases\korisnici.txt");
                    using (TextWriter tw = new StreamWriter(fullPath))
                    {
                        foreach (var item in Korisnici.Values.ToList())
                        {
                            tw.WriteLine(item.ToString());
                        }
                    }

                    fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Models\Databases\voznje.txt");
                    using (TextWriter tw = new StreamWriter(fullPath))
                    {
                        foreach (var item in voznje.Values.ToList())
                        {
                            tw.WriteLine(item.ToString());
                        }
                    }

                    fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Models\Databases\lokacije.txt");
                    using (TextWriter tw = new StreamWriter(fullPath))
                    {
                        foreach (var item in lokacije.Values.ToList())
                        {
                            tw.WriteLine(item.ToString());
                        }
                    }

                    fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Models\Databases\adrese.txt");
                    using (TextWriter tw = new StreamWriter(fullPath))
                    {
                        foreach (var item in adrese.Values.ToList())
                        {
                            tw.WriteLine(item.ToString());
                        }
                    }

                    fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Models\Databases\automobili.txt");
                    using (TextWriter tw = new StreamWriter(fullPath))
                    {
                        foreach (var item in automobili.Values.ToList())
                        {
                            tw.WriteLine(item.ToString());
                        }
                    }

                    fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Models\Databases\komentari.txt");
                    using (TextWriter tw = new StreamWriter(fullPath))
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
        #endregion
    }
}