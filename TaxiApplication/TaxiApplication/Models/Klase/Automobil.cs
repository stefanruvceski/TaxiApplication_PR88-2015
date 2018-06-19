using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Automobil
    {
        //Key
        int brojTaksiVozila = -1;
        string vozacID = "-1";
        int godiste = -1;
        string brojRegistarskeOznake ="-1";
        TipoviAutomobila tipAutomobila = TipoviAutomobila.PutnickiAutomobil;

        public Automobil() { }
        public Automobil(int brojTaskiVozila, int godiste,string vozac, string brojRegistarskeOznake, TipoviAutomobila tipAutomobila)
        {
            this.brojTaksiVozila = brojTaskiVozila;
            this.vozacID = vozac;
            this.godiste = godiste;
            this.brojRegistarskeOznake = brojRegistarskeOznake;
            this.tipAutomobila = tipAutomobila;
        }
        [Key]
        public int BrojTaksiVozila { get => brojTaksiVozila; set => brojTaksiVozila = value; }
        public string VozacID { get => vozacID; set => vozacID = value; }
        public int Godiste { get => godiste; set => godiste = value; }
        public string BrojRegistarskeOznake { get => brojRegistarskeOznake; set => brojRegistarskeOznake = value; }
        public TipoviAutomobila TipAutomobila { get => tipAutomobila; set => tipAutomobila = value; }

        public override string ToString()
        {
            return $"{BrojTaksiVozila}|{BrojRegistarskeOznake}|{godiste}|{tipAutomobila.ToString()}|{VozacID}";
        }

        public static Automobil FromString(string red)
        {
            string[] s = red.Split('|');
            TipoviAutomobila tip;
            Enum.TryParse(s[3], out tip);
            Automobil automobil = new Automobil
            {
                brojTaksiVozila = int.Parse(s[0]),
                BrojRegistarskeOznake = s[1],
                godiste = int.Parse(s[2]),
                tipAutomobila = tip,
                vozacID = s[4]
            };

            return automobil;
        }
    }
}