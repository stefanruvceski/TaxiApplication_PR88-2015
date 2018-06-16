using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Voznja
    {
        DateTime datumIVreme;
        Lokacija lokacija;
        TipoviAutomobila tipAutomobila;
        Korisnik korisnik; // vidi da li treba i dispecer i musterija ili samo samo
        Lokacija odrediste;
        
        Vozac vozac;
        double iznos;
        Komentar komentar;
        StatusiVoznje statusVoznje;
        
        public Voznja() { }
        public Voznja( Lokacija lokacija, double iznos, Vozac vozac, TipoviAutomobila tipAutomobila = TipoviAutomobila.PutnickiAutomobil, Musterija musterija = null,Dispecer dispecer = null,Komentar komentar = null,StatusiVoznje statusVoznje = StatusiVoznje.Kreirana_NaCekanju)
        {
            this.datumIVreme = DateTime.Now;
            this.lokacija = lokacija;
            this.tipAutomobila = tipAutomobila;
            this.korisnik = musterija;
            this.odrediste = null;
           
            this.vozac = vozac;
            this.iznos = iznos;
            this.komentar = komentar;
            this.statusVoznje = StatusVoznje;
        }

        public DateTime DatumIVreme { get => datumIVreme; set => datumIVreme = value; }
        public Lokacija Lokacija { get => lokacija; set => lokacija = value; }
        public TipoviAutomobila TipAutomobila { get => tipAutomobila; set => tipAutomobila = value; }
        public Korisnik Korisnik { get => korisnik; set => korisnik = value; }
        public Lokacija Odrediste { get => odrediste; set => odrediste = value; }
        
        public Vozac Vozac { get => vozac; set => vozac = value; }
        public double Iznos { get => iznos; set => iznos = value; }
        public Komentar Komentar { get => komentar; set => komentar = value; }
        public StatusiVoznje StatusVoznje { get => statusVoznje; set => statusVoznje = value; }
    }
}