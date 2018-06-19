using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Musterija : Korisnik
    {
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
    }
}