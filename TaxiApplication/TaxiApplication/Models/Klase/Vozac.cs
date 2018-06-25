using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Vozac: Korisnik
    {
        string lokacijaID = "-1";
        int automobilID = -1;
        bool dozvoljenPristup = true;

        public Vozac() { }
        public Vozac(string korisnikoIme, string lozinka, string ime, string prezime, string pol, string jmbg, string kontaktTelefon, string email,int automobil)
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
            Uloga = Uloge.Vozac;
            lokacijaID = korisnikoIme;
            this.automobilID = automobil;
        }

       

        public string LokacijaID { get => lokacijaID; set => lokacijaID = value; }
        public int AutomobilID { get => automobilID; set => automobilID = value; }
        public bool DozvoljenPristup { get => dozvoljenPristup; set => dozvoljenPristup = value; }

        public override string ToString()
        {
            string retVal = "";
            retVal += $"{KorisnickoIme}|{Ime}|{Prezime}|{Lozinka}|{Jmbg}|{Pol.ToString()}|{KontaktTelefon}|{Email}|{Uloga.ToString()}|{lokacijaID}|{automobilID}|{dozvoljenPristup}|[";
            foreach (int item in VoznjeID)
            {
                retVal += $"{item}-";
            }
            retVal += "]";

            return retVal;
        }

        public new static Vozac FromString(string red)
        {
            
            Vozac k = new Vozac();
            string[] s = red.Split('|');
            Uloge u;
            Polovi p;
            Enum.TryParse(s[5], out p);
            Enum.TryParse(s[8], out u);
          

            k.KorisnickoIme = s[0];
            k.Ime = s[1];
            k.Prezime = s[2];
            k.Lozinka = s[3];
            k.Jmbg = s[4];
            k.Pol = p;
            k.KontaktTelefon = s[6];
            k.Email = s[7];
            k.Uloga = u;
            k.dozvoljenPristup = bool.Parse(s[11]);
            string[] ss = s[12].Split(']', '-', '[');
            for (int i = 1; i < ss.Length - 1; i++)
            {
                if (ss[i] != "")
                    k.VoznjeID.Add(int.Parse(ss[i]));
            }
            k.lokacijaID = s[9];
            k.automobilID = int.Parse(s[10]);

            return k;
        }
    }
}