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
        Musterija musterija;
        Lokacija odrediste;
        Dispecer dispecer;
        Vozac vozac;
        double iznos;
        string komentar;
        StatusiVoznje statusVoznje;

        public Voznja(DateTime datumIVreme, Lokacija lokacija, double iznos, Vozac vozac, TipoviAutomobila tipAutomobila = TipoviAutomobila.PutnickiAutomobil, Musterija musterija = null,Dispecer dispecer = null,string komentar = "")
        {
            this.datumIVreme = datumIVreme;
            this.lokacija = lokacija;
            this.tipAutomobila = tipAutomobila;
            this.musterija = musterija;
            this.odrediste = null;
            this.dispecer = dispecer;
            this.vozac = vozac;
            this.iznos = iznos;
            this.komentar = komentar;
            this.statusVoznje = StatusiVoznje.Kreirana_NaCekanju;
        }

        public DateTime DatumIVreme { get => datumIVreme; set => datumIVreme = value; }
        public Lokacija Lokacija { get => lokacija; set => lokacija = value; }
        public TipoviAutomobila TipAutomobila { get => tipAutomobila; set => tipAutomobila = value; }
        public Musterija Musterija { get => musterija; set => musterija = value; }
        public Lokacija Odrediste { get => odrediste; set => odrediste = value; }
        public Dispecer Dispecer { get => dispecer; set => dispecer = value; }
        public Vozac Vozac { get => vozac; set => vozac = value; }
        public double Iznos { get => iznos; set => iznos = value; }
        public string Komentar { get => komentar; set => komentar = value; }
        public StatusiVoznje StatusVoznje { get => statusVoznje; set => statusVoznje = value; }
    }
}