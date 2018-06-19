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
    public class KomentariController : ApiController
    {
        // GET: api/Komentari
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Komentari/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Komentari
        public void Post([FromBody]KomentarBindingModel value)
        {
            Ocene o;
            Enum.TryParse(value.Ocena, out o);
            Komentar k = new Komentar(value.Opis, DateTime.Now, User.Identity.Name,int.Parse( value.VoznjaID), o);
            DataBase.komentari.Add(k.Id, k);
            DataBase.voznje[k.VoznjaID].KomentarID = k.Id;
        }

        // PUT: api/Komentari/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Komentari/5
        public void Delete(int id)
        {
        }
    }
}
