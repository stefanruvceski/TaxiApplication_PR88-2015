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
        #region Postavi Komentar
        // POST: api/Komentari
        public IHttpActionResult Post([FromBody]KomentarBindingModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ocene o;
            Enum.TryParse(value.Ocena, out o);
            Komentar k = new Komentar(value.Opis, DateTime.Now, User.Identity.Name,int.Parse( value.VoznjaID), o);
            DataBase.komentari.Add(k.Id, k);
            DataBase.voznje[k.VoznjaID].KomentarID = k.Id;

            return Ok();
        }
        #endregion
    }
}
