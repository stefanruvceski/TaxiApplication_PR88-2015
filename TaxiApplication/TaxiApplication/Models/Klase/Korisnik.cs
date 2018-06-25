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
        string korisnickoIme= "-1";
        string lozinka = "-1";
        string ime = "-1";
        string prezime = "-1";
        Polovi pol = Polovi.Muski;
        string jmbg = "-1";
        string kontaktTelefon = "-1";
        string email = "-1";
        Uloge uloga= Uloge.Musterija;
        List<int> voznjeID = new List<int>();

       
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
        public List<int> VoznjeID { get => voznjeID; set => voznjeID = value; }

        public override string ToString()
        {
            string retVal = "";
            retVal += $"{korisnickoIme}|{ime}|{prezime}|{lozinka}|{jmbg}|{pol.ToString()}|{kontaktTelefon}|{email}|{uloga.ToString()}|[";
            foreach(int item in voznjeID)
            {
                retVal += $"{item}-";
            }
            retVal += "]";

            return retVal;
        }

        public  static Korisnik FromString(string red)
        {
            Korisnik k = new Musterija();
            string[] s = red.Split('|');
            Uloge u;
            Polovi p;
            Enum.TryParse(s[5], out p);
            Enum.TryParse(s[8], out u);
            if (u == Uloge.Dispecer)
                k = new Dispecer();
           

            k.voznjeID = new List<int>();
            k.korisnickoIme = s[0];
            k.ime = s[1];
            k.prezime = s[2];
            k.lozinka = s[3];
            k.jmbg = s[4];
            k.pol = p;
            k.kontaktTelefon = s[6];
            k.email = s[7];
            k.uloga = u;

            string[] ss = s[9].Split(']', '-', '[');
            for (int i = 1; i < ss.Length-1; i++)
            {
                if(ss[i] != "")
                    k.voznjeID.Add(int.Parse(ss[i]));
            }
            return k;
        }
    }
}