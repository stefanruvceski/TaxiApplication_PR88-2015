using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Dispecer : Korisnik
    {
        public Dispecer(string korisnikoIme, string lozinka, string ime, string prezime, string pol, string jmbg, string kontaktTelefon, string email)
        {
            KorisnickoIme = korisnikoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            if (pol.Equals("m"))
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