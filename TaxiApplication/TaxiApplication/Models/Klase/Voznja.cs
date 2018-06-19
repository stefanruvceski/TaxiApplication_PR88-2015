using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Voznja
    {
        int id = -1;
        DateTime datumIVreme = DateTime.Now;
        string lokacijaID = "-1";
        TipoviAutomobila tipAutomobila = TipoviAutomobila.PutnickiAutomobil;
        string korisnikID = "-1";
        string odredisteID = "-1";
        string vozacID = "-1";
        double iznos = -1;
        int komentarID = -1;
        StatusiVoznje statusVoznje = StatusiVoznje.Kreirana_NaCekanju;
        
        public Voznja() { this.id = DataBase.voznje.Count; }
        public Voznja( string lokacija, double iznos, string vozac = "-1", TipoviAutomobila tipAutomobila = TipoviAutomobila.PutnickiAutomobil, string musterija = "-1",string dispecer = "-1",int komentar = -1,StatusiVoznje statusVoznje = StatusiVoznje.Kreirana_NaCekanju)
        {
            this.id = DataBase.voznje.Count;
            this.datumIVreme = DateTime.Now;
            this.lokacijaID = lokacija;
            this.tipAutomobila = tipAutomobila;
            this.korisnikID = musterija;
            this.odredisteID = null;
           
            this.vozacID = vozac;
            this.iznos = iznos;
            this.komentarID = komentar;
            this.statusVoznje = StatusVoznje;
        }

        public Voznja(int id,string lokacija, double iznos, string vozac = "-1", TipoviAutomobila tipAutomobila = TipoviAutomobila.PutnickiAutomobil, string musterija = "-1", string dispecer = "-1", int komentar = -1, StatusiVoznje statusVoznje = StatusiVoznje.Kreirana_NaCekanju)
        {
            this.id = id;
            this.datumIVreme = DateTime.Now;
            this.lokacijaID = lokacija;
            this.tipAutomobila = tipAutomobila;
            this.korisnikID = musterija;
            this.odredisteID = null;

            this.vozacID = vozac;
            this.iznos = iznos;
            this.komentarID = komentar;
            this.statusVoznje = StatusVoznje;
        }

        public DateTime DatumIVreme { get => datumIVreme; set => datumIVreme = value; }
        public string LokacijaID { get => lokacijaID; set => lokacijaID = value; }
        public TipoviAutomobila TipAutomobila { get => tipAutomobila; set => tipAutomobila = value; }
        public string KorisnikID { get => korisnikID; set => korisnikID = value; }
        public string OdredisteID { get => odredisteID; set => odredisteID = value; }
        
        public string VozacID { get => vozacID; set => vozacID = value; }
        public double Iznos { get => iznos; set => iznos = value; }
        public int KomentarID { get => komentarID; set => komentarID = value; }
        public StatusiVoznje StatusVoznje { get => statusVoznje; set => statusVoznje = value; }
        public int Id { get => id; set => id = value; }

        public override string ToString()
        {
            return $"{Id}|{datumIVreme}|{statusVoznje.ToString()}|{tipAutomobila.ToString()}|{Iznos}|{vozacID}|{korisnikID}|{lokacijaID}|{odredisteID}|{komentarID}";
        }

        public static Voznja FromString(string red)
        {
            if (red != "")
            {
                string[] s = red.Split('|');
                //6 / 18 / 2018 10:13:04 PM
                string[] ss = s[1].Split('/', ':');
                string[] sss = ss[2].Split(' ');
                StatusiVoznje status;
                TipoviAutomobila tip;
                string korisnik = "-1";
                if (s[6] != "")
                    korisnik = s[6];
                Enum.TryParse(s[3], out tip);
                Enum.TryParse(s[2], out status);
                Voznja voznja = new Voznja
                {
                    Id = int.Parse(s[0]),
                    DatumIVreme = new DateTime(int.Parse(sss[0]), int.Parse(ss[0]), int.Parse(ss[1]), int.Parse(sss[1]), int.Parse(ss[3]), int.Parse(ss[4].Substring(0, 2))),
                    statusVoznje = status,
                    tipAutomobila = tip,
                    Iznos = double.Parse(s[4]),
                    vozacID = s[5],
                    korisnikID = korisnik,
                    lokacijaID = s[7],
                    odredisteID = s[8],
                    komentarID = int.Parse(s[9])
                };
                return voznja;
            }
            else
            {
                return new Voznja();
            }
            
        }


    }
}