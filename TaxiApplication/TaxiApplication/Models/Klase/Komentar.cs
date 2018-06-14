using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Komentar
    {
        string opis;
        DateTime datumObjave;
        Korisnik korisnik;
        Voznja voznja;
        Ocene ocena;

        public Komentar(string opis, DateTime datumObjave, Korisnik korisnik, Voznja voznja, Ocene ocena = Ocene.Nula)
        {
            this.opis = opis;
            this.datumObjave = datumObjave;
            this.korisnik = korisnik;
            this.voznja = voznja;
            this.ocena = ocena;
        }

        public string Opis { get => opis; set => opis = value; }
        public DateTime DatumObjave { get => datumObjave; set => datumObjave = value; }
        public Korisnik Korisnik { get => korisnik; set => korisnik = value; }
        public Voznja Voznja { get => voznja; set => voznja = value; }
        public Ocene Ocena { get => ocena; set => ocena = value; }
    }
}