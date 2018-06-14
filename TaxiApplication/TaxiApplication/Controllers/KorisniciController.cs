using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaxiApplication.Models.Klase;

namespace TaxiApplication.Controllers
{
    [Authorize]
    public class KorisniciController : ApiController
    {
        // GET: api/Korisnici
        public IEnumerable<Korisnik> Get()
        {
            return new Korisnik[1];
        }

        // GET: api/Korisnici/5
        public Korisnik Get(int id)
        {
            return new Musterija();
        }

        // POST: api/Korisnici
        public void Post([FromBody]Korisnik value)
        {
        }

        // PUT: api/Korisnici/5
        public void Put(int id, [FromBody]Korisnik value)
        {
        }

        // DELETE: api/Korisnici/5
        public void Delete(int id)
        {
        }
    }
}
