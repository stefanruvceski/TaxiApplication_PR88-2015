using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Komentar
    {
        int id = -1;
        string opis = "-1";
        DateTime datumObjave = DateTime.Now;
        string korisnikID = "-1";
        int voznjaID = -1;
        Ocene ocena = Ocene.Nula;

        public Komentar() { }
        public Komentar(string opis, DateTime datumObjave, string korisnikID, int voznjaID, Ocene ocena = Ocene.Nula)
        {
            this.Id = DataBase.komentari.Count;
            this.opis = opis;
            this.datumObjave = datumObjave;
            this.korisnikID = korisnikID;
            this.voznjaID = voznjaID;
            this.ocena = ocena;
        }

        public Komentar(int id, string opis, DateTime datumObjave, string korisnikID, int voznjaID, Ocene ocena = Ocene.Nula)
        {
            this.id = id;
            this.opis = opis;
            this.datumObjave = datumObjave;
            this.korisnikID = korisnikID;
            this.voznjaID = voznjaID;
            this.ocena = ocena;
        }

        public string Opis { get => opis; set => opis = value; }
        public DateTime DatumObjave { get => datumObjave; set => datumObjave = value; }
        public string KorisnikID { get => korisnikID; set => korisnikID = value; }
        public int VoznjaID { get => voznjaID; set => voznjaID = value; }
        public Ocene Ocena { get => ocena; set => ocena = value; }
        [Key]
        public int Id { get => id; set => id = value; }

        public override string ToString()
        {
            return $"{Id}|{opis}|{Ocena.ToString()}|{datumObjave}|{voznjaID}|{korisnikID}";
        }

        public static Komentar FromString(string red)
        {
            string[] s = red.Split('|');
            Ocene o;
            Enum.TryParse(s[2], out o);
            string[] ss = s[3].Split('/', ':');
            string[] sss = ss[2].Split(' ');
            Komentar komentar = new Komentar
            {
                Id = int.Parse(s[0]),
                opis = s[1],
                ocena = o,
                datumObjave = new DateTime(int.Parse(sss[0]), int.Parse(ss[0]), int.Parse(ss[1]), int.Parse(sss[1]), int.Parse(ss[3]), int.Parse(ss[4].Substring(0, 2))),
                voznjaID = int.Parse(s[4]),
                korisnikID = s[5]
            };

            return komentar;
        }
    }
}