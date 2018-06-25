using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Musterija : Korisnik
    {
        bool dozvoljenPristup = true;
        public Musterija() { }
        public Musterija(string korisnikoIme, string lozinka, string ime, string prezime, string pol, string jmbg, string kontaktTelefon, string email)
        {
            KorisnickoIme = korisnikoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            if (pol.Equals("Muski"))
                Pol = Polovi.Muski;
            else
                Pol = Polovi.Zenski;
            Jmbg = jmbg;
            KontaktTelefon = kontaktTelefon;
            Email = email;
            Uloga = Uloge.Musterija;
        }

        public bool DozvoljenPristup { get => dozvoljenPristup; set => dozvoljenPristup = value; }

        public override string ToString()
        {
            string retVal = "";
            retVal += $"{KorisnickoIme}|{Ime}|{Prezime}|{Lozinka}|{Jmbg}|{Pol.ToString()}|{KontaktTelefon}|{Email}|{Uloga.ToString()}|{dozvoljenPristup}|[";
            foreach (int item in VoznjeID)
            {
                retVal += $"{item}-";
            }
            retVal += "]";

            return retVal;
        }

        public new static Musterija FromString(string red)
        {
            Musterija k = new Musterija();
            string[] s = red.Split('|');
            Uloge u;
            Polovi p;
            Enum.TryParse(s[5], out p);
            Enum.TryParse(s[8], out u);
           

            k.VoznjeID = new List<int>();
            k.KorisnickoIme = s[0];
            k.Ime = s[1];
            k.Prezime = s[2];
            k.Lozinka = s[3];
            k.Jmbg = s[4];
            k.Pol = p;
            k.KontaktTelefon = s[6];
            k.Email = s[7];
            k.Uloga = u;
            k.dozvoljenPristup = bool.Parse(s[9]);
            string[] ss = s[10].Split(']', '-', '[');
            for (int i = 1; i < ss.Length - 1; i++)
            {
                if (ss[i] != "")
                    k.VoznjeID.Add(int.Parse(ss[i]));
            }
            return k;
        }
    }
}