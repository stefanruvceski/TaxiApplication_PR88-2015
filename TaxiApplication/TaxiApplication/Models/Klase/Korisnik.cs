using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public abstract class Korisnik
    {
        //Key
        string korisnickoIme;
        string lozinka;
        string ime;
        string prezime;
        Polovi pol;
        string jmbg;
        string kontaktTelefon;
        string email;
        Uloge uloga;
        List<Voznja> voznje;

       
        [Key]
        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public Polovi Pol { get => pol; set => pol = value; }
        public string Jmbg { get => jmbg; set => jmbg = value; }
        public string KontaktTelefon { get => kontaktTelefon; set => kontaktTelefon = value; }
        public string Email { get => email; set => email = value; }
        public Uloge Uloga { get => uloga; set => uloga = value; }
        public List<Voznja> Voznje { get => voznje; set => voznje = value; }
    }
}