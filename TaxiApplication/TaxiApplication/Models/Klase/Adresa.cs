using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Adresa
    {
        int broj;
        string ulica;
        string grad;
        int postanskiBroj;

        public Adresa(int broj, string ulica, string grad, int postanskiBroj)
        {
            this.broj = broj;
            this.ulica = ulica;
            this.grad = grad;
            this.postanskiBroj = postanskiBroj;
        }

        public int Broj { get => broj; set => broj = value; }
        public string Ulica { get => ulica; set => ulica = value; }
        public string Grad { get => grad; set => grad = value; }
        public int PostanskiBroj { get => postanskiBroj; set => postanskiBroj = value; }
    }
}