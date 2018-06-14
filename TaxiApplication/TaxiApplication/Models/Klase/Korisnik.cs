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
        Int64 jmbg;
        string kontaktTelefon;
        string email;
        Uloge uloga;
        Dictionary<string,Voznja> voznje;
        bool ulogovan = false;

       
        [Key]
        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public Polovi Pol { get => pol; set => pol = value; }
        public long Jmbg { get => jmbg; set => jmbg = value; }
        public string KontaktTelefon { get => kontaktTelefon; set => kontaktTelefon = value; }
        public string Email { get => email; set => email = value; }
        public Uloge Uloga { get => uloga; set => uloga = value; }
        public Dictionary<string, Voznja> Voznje { get => voznje; set => voznje = value; }
        public bool Ulogovan { get => ulogovan; set => ulogovan = value; }
    }
}