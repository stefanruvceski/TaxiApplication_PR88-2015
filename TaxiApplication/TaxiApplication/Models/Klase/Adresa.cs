using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Adresa
    {
        string id = "-1";
        int broj = -1;
        string ulica = "-1";
        string grad = "-1";
        int postanskiBroj = -1;

        public Adresa() { }
        public Adresa(int broj, string ulica, string grad, int postanskiBroj)
        {
            this.Id = DataBase.adrese.Count.ToString();
            this.broj = broj;
            this.ulica = ulica;
            this.grad = grad;
            this.postanskiBroj = postanskiBroj;
        }

        public Adresa(string id, int broj, string ulica, string grad, int postanskiBroj)
        {
            this.id = id;
            this.broj = broj;
            this.ulica = ulica;
            this.grad = grad;
            this.postanskiBroj = postanskiBroj;
        }

        public int Broj { get => broj; set => broj = value; }
        public string Ulica { get => ulica; set => ulica = value; }
        public string Grad { get => grad; set => grad = value; }
        public int PostanskiBroj { get => postanskiBroj; set => postanskiBroj = value; }
        [Key]
        public string Id { get => id; set => id = value; }

        public string ToBindingString()
        {
            return $"{Ulica},{Broj},{Grad},{PostanskiBroj}";
        }

        public override string ToString()
        {
            return $"{Id}|{Ulica}|{Broj}|{Grad}|{PostanskiBroj}";
        }

        public static Adresa FromString(string red)
        {
            string[] s = red.Split('|');
            Adresa adresa = new Adresa
            {
                Id = s[0],
                ulica = s[1],
                broj = int.Parse(s[2]),
                Grad = s[3],
                postanskiBroj = int.Parse(s[4]),
            };

            return adresa;
        }
    }
}