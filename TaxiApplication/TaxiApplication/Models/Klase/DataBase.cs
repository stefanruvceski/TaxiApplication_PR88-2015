using System;
using System.Collections.Generic;
using System.Linq;
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
        public static Korisnik ulogovan = null;
        
        public static Dictionary<string, Korisnik> Korisnici = new Dictionary<string, Korisnik>()
        {
            {"stefanrrr", new Dispecer("stefanrrr","Terminator_96","Stefan","Ruvceski","m","0606996800011","0658601731","stefanruvceski@gmail.com") },
            {"teodorar", new Musterija("reodorar","Teodora_96","Teodora","Ruvceski","z","1234555","0653103123","teodoraruvceski@gmail.com") },
            {"musterija1", new Musterija("musterija1","lozinka1","Ime1","Prezime1","m","12345","065111111","email11@gmail.com") },
            {"musterija2", new Musterija("musterija2","lozinka2","Ime2","Prezime2","z","123456","065111112","email12@gmail.com") },
            {"musterija3", new Musterija("musterija3","lozinka3","Ime3","Prezime3","m","123457","065111113","email13@gmail.com") },
            {"musterija4", new Musterija("musterija4","lozinka4","Ime4","Prezime4","z","123458","065111114","email14@gmail.com") },
            {"musterija5", new Musterija("musterija5","lozinka5","Ime5","Prezime5","m","123459","065111115","email15@gmail.com") },
             {"dispecer1", new Dispecer("dispecer1","lozinka1","Ime1","Prezime1","m","123455","065111111","email21@gmail.com") },
            {"dispecer2", new Dispecer("dispecer2","lozinka2","Ime2","Prezime2","z","1234556","065111112","email22@gmail.com") },
            {"dispecer3", new Dispecer("dispecer3","lozinka3","Ime3","Prezime3","m","1234557","065111113","email23@gmail.com") },
            {"vozac", new Vozac("vozac","Vozac_96","Vozac","Vozic","m","123456","065111111","email31@gmail.com",new Automobil(111,2011,"NS-010-LE","Vozac96",TipoviAutomobila.PutnickiAutomobil)) },
            {"Vozac2", new Vozac("vozac2","Vozac2_96","Ime2","Prezime2","z","1234566","065111112","email32@gmail.com",new Automobil(222,2012,"NS-020-LE","vozac2",TipoviAutomobila.KombiVozilo)) },
            {"vozac3", new Vozac("vozac3","lozinka3","Ime3","Prezime3","m","1234567","065111113","email33@gmail.com",new Automobil(333,2013,"NS-030-LE","vozac3",TipoviAutomobila.PutnickiAutomobil)) },

        };

    }
}