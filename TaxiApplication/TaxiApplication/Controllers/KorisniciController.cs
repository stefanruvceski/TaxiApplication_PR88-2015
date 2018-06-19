using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Script.Serialization;
using TaxiApplication.Models;
using TaxiApplication.Models.Klase;

namespace TaxiApplication.Controllers
{
    [Authorize]
    public class KorisniciController : ApiController
    {

        [Route("api/korisnici/getpage")]
        public string GetPage()
        {
            if (User.IsInRole("Dispecer"))
                return "DispecerMain.html";
            else if (User.IsInRole("Vozac"))
                return "VozacMain.html";
            else
                return "MusterijaMain.html";
            
        }

        // GET: api/Korisnici
        [Route("api/korisnici/getme")]
        public Korisnik GetMe()
        {
            var json = new JavaScriptSerializer().Serialize(DataBase.Korisnici[User.Identity.Name]);
            return DataBase.Korisnici[User.Identity.Name];
        }

        // GET: api/Korisnici
        public IEnumerable<Korisnik> Get()
        {
            if (User.IsInRole("Dispecer"))
            {
                return DataBase.Korisnici.Values.ToList();
            }
            else
            {
                return DataBase.Korisnici.Values.ToList().FindAll(x => x.KorisnickoIme == User.Identity.Name);
            }
        }
        [Route("api/korisnici/getavailabledrivers")]
        //[Authorize(Roles ="Dispecer")]
        [HttpGet]
        public List<string> DriversGet()
        {
            bool isAvailable = true;
            List<string> lista = new List<string>();
            foreach(Korisnik k in DataBase.Korisnici.Values)
            {
                if (k.Uloga == Uloge.Vozac && k.KorisnickoIme != "-1")
                {
                    foreach (int i in k.VoznjeID)
                        if (DataBase.voznje[i].StatusVoznje == StatusiVoznje.Prihvacena || DataBase.voznje[i].StatusVoznje == StatusiVoznje.Obradjena || DataBase.voznje[i].StatusVoznje == StatusiVoznje.Formirana)
                            isAvailable = false;
                        
                    if(isAvailable)
                        lista.Add(k.KorisnickoIme);
                    isAvailable = true;
                }
            }

            return lista;
        }

        // GET: api/Korisnici/5
        public Korisnik Get(string id)
        {
            return DataBase.Korisnici[id];
        }
        [Route("api/korisnici/editlocation")]
        public IHttpActionResult EditLocation([FromBody]LokacijaBindingModel model)
        {
            Adresa adresa = new Adresa(User.Identity.Name,model.Broj, model.Ulica, model.Grad, model.PostanskiBroj);
            
            DataBase.adrese[User.Identity.Name] = adresa;
            DataBase.lokacije[User.Identity.Name].AdresaID = adresa.Id;
            return Ok();
        }

        // POST: api/Korisnici
        public  void Post([FromBody]Korisnik value)
        {
        }

        // PUT: api/Korisnici/5
        public  void Put(string id, [FromBody]Korisnik value)
        {
           
        }

        // DELETE: api/Korisnici/5
        public void Delete(string id)
        {
            DataBase.Korisnici.Remove(id);
        }
    }
}
