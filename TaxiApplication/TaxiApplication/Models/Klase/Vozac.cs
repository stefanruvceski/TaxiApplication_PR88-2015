using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Vozac: Korisnik
    {
        Lokacija lokacija;
        Automobil automobil;

        public Vozac() { }
        public Vozac(string korisnikoIme, string lozinka, string ime, string prezime, string pol, long jmbg, string kontaktTelefon, string email,Automobil automobil)
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
            Uloga = Uloge.Vozac;
            this.Lokacija = new Lokacija();
            this.Automobil = automobil;
        }

        public Lokacija Lokacija { get => lokacija; set => lokacija = value; }
        public Automobil Automobil { get => automobil; set => automobil = value; }
    }
}