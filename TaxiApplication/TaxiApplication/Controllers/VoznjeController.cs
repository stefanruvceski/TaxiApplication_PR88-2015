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
            foreach (Voznja vv in DataBase.Korisnici[User.Identity.Name].Voznje)
            {
                GetKorisnikVoznjaBindingModel v = new GetKorisnikVoznjaBindingModel()
                {

                    Polaziste = vv.Lokacija.Adresa.ToString(),    
                    Odrediste = vv.Odrediste.Adresa.ToString(),
                    Ocena = vv.Komentar.Ocena.ToString(),
                    TipAutomobila = vv.TipAutomobila.ToString(),
                    DatumIVreme = vv.DatumIVreme.ToString(),
                    StatusVoznje = vv.StatusVoznje.ToString(),
                    BrojTaksija = vv.Vozac.Automobil.BrojTaksiVozila.ToString()
                };
                voznje.Add(v);
            }
                

            return voznje;
            
        }

        // GET: api/Voznje/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Voznje
        public IHttpActionResult Post([FromBody]KorisnikVoznjaBindingModel value)
        {
            try
            {
                Lokacija lokacija = new Lokacija
                {
                    
                    Adresa = new Adresa(value.Broj, value.Ulica, value.Grad, value.PostanskiBroj),
                };
                TipoviAutomobila t; Enum.TryParse(value.TipAutomobila, out t);
                Voznja voznja = new Voznja
                {
                    Korisnik = (Musterija)DataBase.Korisnici[User.Identity.Name],
                    Lokacija = lokacija,
                    TipAutomobila = t,
                    DatumIVreme = DateTime.Now,
                    Odrediste = new Lokacija(),
                    Vozac = new Vozac(),
                    Iznos = 0,
                    Komentar = new Komentar(),


                };
                DataBase.Korisnici[User.Identity.Name].Voznje.Add(voznja);

                return Ok();
            }
            catch
            {
                return InternalServerError(new Exception("Order error"));
            }

        }

        // PUT: api/Voznje/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Voznje/5
        public void Delete(int id)
        {
        }
    }
}
