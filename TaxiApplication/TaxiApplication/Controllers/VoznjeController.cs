using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaxiApplication.Models;
using TaxiApplication.Models.Klase;

namespace TaxiApplication.Controllers
{
    [Authorize]
    public class VoznjeController : ApiController
    {
        // GET: api/Voznje
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Voznje
        [Route("api/voznje/getmy")]
        public List<GetKorisnikVoznjaBindingModel> GetMy()
        {
            List<GetKorisnikVoznjaBindingModel> voznje = new List<GetKorisnikVoznjaBindingModel>();
            //voznje.Add(new Voznja());
            if (DataBase.lokacije.Count > 1)
            {
                foreach (int vv in DataBase.Korisnici[User.Identity.Name].VoznjeID)
                {

                    string polazisteID = DataBase.lokacije[DataBase.voznje[vv].LokacijaID].AdresaID;

                    string odredisteID = "-1";
                    if (DataBase.voznje[vv].OdredisteID != "-1")
                        odredisteID = DataBase.lokacije[DataBase.voznje[vv].OdredisteID].AdresaID;

                    int komentarID = DataBase.voznje[vv].KomentarID;

                    string vozacID = DataBase.voznje[vv].VozacID;

                    int automobilID = -1;
                    if (vozacID != "-1")
                        automobilID = ((Vozac)DataBase.Korisnici[vozacID]).AutomobilID;

                    GetKorisnikVoznjaBindingModel v = new GetKorisnikVoznjaBindingModel()
                    {
                        VoznjaID = vv.ToString(),
                        Polaziste = DataBase.adrese[polazisteID].ToBindingString(),
                        Odrediste = DataBase.adrese[odredisteID].ToBindingString(),
                        Ocena = DataBase.komentari[komentarID].Ocena.ToString(),
                        TipAutomobila = DataBase.voznje[vv].TipAutomobila.ToString(),
                        DatumIVreme = DataBase.voznje[vv].DatumIVreme.ToString(),
                        StatusVoznje = DataBase.voznje[vv].StatusVoznje.ToString(),
                        BrojTaksija = DataBase.automobili[automobilID].BrojTaksiVozila.ToString()
                    };
                    voznje.Add(v);
                }
            }
                

            return voznje;
            
        }

        // GET: api/Voznje/5
        public KorisnikVoznjaBindingModel Get(int id)
        {
            Adresa a = DataBase.adrese[DataBase.lokacije[DataBase.voznje[id].LokacijaID].AdresaID];
            KorisnikVoznjaBindingModel voznja = new KorisnikVoznjaBindingModel()
            {
                Broj = a.Broj,
                Ulica = a.Ulica,
                Grad = a.Grad,
                PostanskiBroj = a.PostanskiBroj,
                TipAutomobila = DataBase.voznje[id].TipAutomobila.ToString()
            };
            return voznja;
        }

        [Route("api/voznje/details/")]
        [HttpGet]
        public VoznjaDetailsBindingModel Details(int id)
        {
            Adresa a1 = DataBase.adrese[DataBase.lokacije[DataBase.voznje[id].LokacijaID].AdresaID];
            Adresa a2 = DataBase.adrese[DataBase.lokacije[DataBase.voznje[id].OdredisteID].AdresaID];
            Komentar k = DataBase.komentari[DataBase.voznje[id].KomentarID];
            Automobil a = DataBase.automobili[((Vozac)DataBase.Korisnici[DataBase.voznje[id].VozacID]).AutomobilID];
            VoznjaDetailsBindingModel voznja = new VoznjaDetailsBindingModel();

            voznja.Adresa1 = a1.ToBindingString();
            if (a2.PostanskiBroj != -1)
                voznja.Adresa2 = a2.ToBindingString();
            else
                voznja.Adresa2 = "Nedefinisano";
            voznja.TipAutomobila = DataBase.voznje[id].TipAutomobila.ToString();
            voznja.KorisnickoImeKorisnika = User.Identity.Name;
            voznja.Ocena = k.Ocena.ToString();
            if (k.Opis != "-1")
                voznja.Opis = k.Opis;
            else
                voznja.Opis = "Nedefinisano";
            voznja.StatusVoznje = DataBase.voznje[id].StatusVoznje.ToString();
            if (a.BrojTaksiVozila != -1)
                voznja.BrojTaksija = a.BrojTaksiVozila.ToString();
            else
                voznja.BrojTaksija = "Nedefinisano";

            
            return voznja;
        }

        // POST: api/Voznje
        public IHttpActionResult Post([FromBody]KorisnikVoznjaBindingModel value)
        {
            try
            {
                if (value.VoznjaID == "-1")
                {
                    Adresa adresa = new Adresa(value.Broj, value.Ulica, value.Grad, value.PostanskiBroj);
                    DataBase.adrese.Add(adresa.Id, adresa);
                    Lokacija lokacija = new Lokacija(adresa.Id);
                    DataBase.lokacije.Add(lokacija.Id, lokacija);
                    TipoviAutomobila t; Enum.TryParse(value.TipAutomobila, out t);
                    Voznja voznja = new Voznja()
                    {
                        KorisnikID = User.Identity.Name,
                        LokacijaID = lokacija.Id,
                        TipAutomobila = t,
                        DatumIVreme = DateTime.Now
                    };
                    DataBase.voznje.Add(voznja.Id, voznja);
                    if (DataBase.Korisnici[User.Identity.Name].VoznjeID == null)
                        DataBase.Korisnici[User.Identity.Name].VoznjeID = new List<int>();
                    DataBase.Korisnici[User.Identity.Name].VoznjeID.Add(voznja.Id);
                }
                else
                {
                    string lokacijaID = DataBase.voznje[int.Parse(value.VoznjaID)].LokacijaID;
                    string adresaID = DataBase.lokacije[lokacijaID].AdresaID;
                    Adresa adresa = new Adresa(adresaID,value.Broj, value.Ulica, value.Grad, value.PostanskiBroj);
                    DataBase.adrese[adresaID] = adresa;
                    TipoviAutomobila t; Enum.TryParse(value.TipAutomobila, out t);
                    DataBase.voznje[int.Parse(value.VoznjaID)].TipAutomobila = t;
                   
                }
                return Ok();
            }
            catch
            {
                return InternalServerError(new Exception("Order error"));
            }

        }

        // PUT: api/Voznje/5
        public void Put(int id, [FromBody]int value)
        {
        }

        // DELETE: api/Voznje/5
        public IHttpActionResult Delete(int id)
        {
            DataBase.voznje[id].StatusVoznje = StatusiVoznje.Otkazana;

            return Ok();
        }
    }
}
