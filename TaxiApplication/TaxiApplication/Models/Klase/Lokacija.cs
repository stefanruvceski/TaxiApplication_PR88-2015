using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Lokacija
    {
        double xKoordinata = 0;
        double yKoordinata = 0;
        Adresa adresa; //Format: Ulica broj,Grad pozivni broj -> Sutjeska 3, Novi Sad 21000

        public Lokacija() { adresa = new Adresa(); }
        public Lokacija(double xKoordinata, double yKooridinata, Adresa adresa)
        {
            this.xKoordinata = xKoordinata;
            this.yKoordinata = yKooridinata;
            this.adresa = adresa;
        }

        public double XKoordinata { get => xKoordinata; set => xKoordinata = value; }
        public double YKoordinata { get => yKoordinata; set => yKoordinata = value; }
        public Adresa Adresa { get => adresa; set => adresa = value; }
    }
}