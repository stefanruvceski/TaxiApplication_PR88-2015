using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Lokacija
    {
        double xKoordinata;
        double yKooridinata;
        Adresa adresa; //Format: Ulica broj,Grad pozivni broj -> Sutjeska 3, Novi Sad 21000

        public Lokacija() { }
        public Lokacija(double xKoordinata, double yKooridinata, Adresa adresa)
        {
            this.xKoordinata = xKoordinata;
            this.yKooridinata = yKooridinata;
            this.adresa = adresa;
        }

        public double XKoordinata { get => xKoordinata; set => xKoordinata = value; }
        public double YKooridinata { get => yKooridinata; set => yKooridinata = value; }
        public Adresa Adresa { get => adresa; set => adresa = value; }
    }
}