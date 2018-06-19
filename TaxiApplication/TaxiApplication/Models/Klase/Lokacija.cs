using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Lokacija
    {
        string id = "-1";
        double xKoordinata = -1;
        double yKoordinata = -1;
        string adresaID = "-1"; //Format: Ulica broj,Grad pozivni broj -> Sutjeska 3, Novi Sad 21000

        public Lokacija() { }

        public Lokacija(string adresaID)
        {
            this.id = DataBase.lokacije.Count.ToString();
            this.adresaID = adresaID;
        }

        public Lokacija(string id,string adresaID)
        {
            this.id = id;
            this.adresaID = adresaID;
        }

        public Lokacija(double xKoordinata, double yKooridinata, string adresaID)
        {
            this.id = DataBase.lokacije.Count.ToString();
            this.xKoordinata = xKoordinata;
            this.yKoordinata = yKooridinata;
            this.adresaID = adresaID;
        }

        public Lokacija(string id, double xKoordinata, double yKoordinata, string adresaID)
        {
            this.id = id;
            this.xKoordinata = xKoordinata;
            this.yKoordinata = yKoordinata;
            this.adresaID = adresaID;
        }

        public double XKoordinata { get => xKoordinata; set => xKoordinata = value; }
        public double YKoordinata { get => yKoordinata; set => yKoordinata = value; }
        public string AdresaID { get => adresaID; set => adresaID = value; }
        [Key]
        public string Id { get => id; set => id = value; }

        public override string ToString()
        {
            return $"{Id}|{AdresaID}|{XKoordinata}|{yKoordinata}";
        }

        public static Lokacija FromString(string red)
        {
            string[] s = red.Split('|');
            Lokacija lokacija = new Lokacija
            {
                Id = s[0],
                adresaID = s[1],
                XKoordinata = double.Parse(s[2]),
                YKoordinata = double.Parse(s[3]),
            };

            return lokacija;
        }
    }
}