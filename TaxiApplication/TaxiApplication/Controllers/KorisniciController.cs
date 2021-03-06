﻿using Microsoft.AspNet.Identity.Owin;
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
        #region Stranica-Proveriti zasto ne radi
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
        #endregion

        #region Korisnik po sesiji
        // GET: api/Korisnici
        [Route("api/korisnici/getme")]
        public KorisnikBindingModel GetMe()
        {
            KorisnikBindingModel model = new KorisnikBindingModel();
            Korisnik k = DataBase.Korisnici[User.Identity.Name];
            
            model.KorisnickoIme = k.KorisnickoIme;
            model.Ime = k.Ime;
            model.Prezime = k.Prezime;
            model.Jmbg = k.Jmbg;
            model.KontaktTelefon = k.KontaktTelefon;
            model.Password = k.Lozinka;
            model.Pol = k.Pol.ToString();
            model.Email = k.Email;
            model.Uloga = k.Uloga.ToString();
            if (User.IsInRole("Vozac"))
            {
                Automobil a = DataBase.automobili[((Vozac)k).AutomobilID];
                Lokacija l = DataBase.lokacije[((Vozac)DataBase.Korisnici[User.Identity.Name]).LokacijaID];
                Adresa aa = DataBase.adrese[DataBase.lokacije[((Vozac)DataBase.Korisnici[User.Identity.Name]).LokacijaID].AdresaID];
                model.BrojTaksiVozila = a.BrojTaksiVozila.ToString();
                model.BrojRegistarskeOznake = a.BrojRegistarskeOznake;
                model.TipAutomobila = a.TipAutomobila.ToString();
                model.GodisteAutomobila = a.Godiste.ToString();
                model.Ulica = aa.Ulica;
                model.Broj = aa.Broj.ToString();
                model.Grad = aa.Grad;
                model.PostanskiBroj = aa.PostanskiBroj.ToString();
                model.XKoordinata = l.XKoordinata;
                model.YKoordinata= l.YKoordinata;
            }

            return model;
        }
        #endregion

        #region Dispecer menja prava pristupa korisnika
        // GET: api/Korisnici
        [Route("api/korisnici/changeAccess/{korisnik}")]
        [HttpGet]
        public IHttpActionResult ChangeAccess(string korisnik)
        {
            try
            {
                if(DataBase.Korisnici[korisnik].Uloga == Uloge.Musterija)
                    ((Musterija)DataBase.Korisnici[korisnik]).DozvoljenPristup = !(((Musterija)DataBase.Korisnici[korisnik]).DozvoljenPristup);
                else if(DataBase.Korisnici[korisnik].Uloga == Uloge.Vozac)
                     ((Vozac)DataBase.Korisnici[korisnik]).DozvoljenPristup = !(((Vozac)DataBase.Korisnici[korisnik]).DozvoljenPristup);

                return Ok();
            }
            catch
            {
                return BadRequest("Ne postoji korisnik sa datim korisnickim imenom");
            }
        }
        #endregion

        #region Korisnik proverava svoja prava
        // GET: api/Korisnici
        [Route("api/korisnici/getAccess/")]
        [HttpGet]
        public IHttpActionResult GetAccess()
        {
            if(DataBase.Korisnici[User.Identity.Name].Uloga == Uloge.Dispecer)
            {
                return Ok();
            }
            else if (DataBase.Korisnici[User.Identity.Name].Uloga == Uloge.Musterija && ((Musterija)DataBase.Korisnici[User.Identity.Name]).DozvoljenPristup)
                return Ok();
            else if (DataBase.Korisnici[User.Identity.Name].Uloga == Uloge.Vozac && ((Vozac)DataBase.Korisnici[User.Identity.Name]).DozvoljenPristup)
                return Ok();
            else
                return InternalServerError(new Exception("Pristup sajtu vam je zabranjen"));

        }
        #endregion

        #region Svi korisnici 
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
        #endregion

        #region Svi slobodni vozaci odredjene kategorije vozila za proces/adresa
        // GET: api/Korisnici/flag
        
        public List<string> GetAvailableDrivers(string flag)
        {
            List<string> lista = new List<string>();

            bool isAvailable = true;
            foreach (Korisnik k in DataBase.Korisnici.Values)
            {
                if (k.Uloga == Uloge.Vozac && k.KorisnickoIme != "-1")
                {
                    foreach (int i in k.VoznjeID)
                        if (DataBase.voznje[i].StatusVoznje == StatusiVoznje.Prihvacena || DataBase.voznje[i].StatusVoznje == StatusiVoznje.Obradjena || DataBase.voznje[i].StatusVoznje == StatusiVoznje.Formirana)
                            isAvailable = false;

                    if (isAvailable && DataBase.automobili[((Vozac)k).AutomobilID].TipAutomobila.ToString() == flag)
                        lista.Add(k.KorisnickoIme);
                    isAvailable = true;
                }
            }

            return lista;   
        }
        #endregion

        #region Svi slobodni vozaci odredjene kategorije vozila za proces/lokacija
        // GET: api/Korisnici/flag/x/y
        public List<string> GetAvailableDrivers(string flag, double x, double y)
        {
            List<string> lista = new List<string>();
            List<string> pom = new List<string>();
            bool isAvailable = true;
            foreach (Korisnik k in DataBase.Korisnici.Values)
            {
                if (k.Uloga == Uloge.Vozac && k.KorisnickoIme != "-1")
                {
                    foreach (int i in k.VoznjeID)
                        if (DataBase.voznje[i].StatusVoznje == StatusiVoznje.Prihvacena || DataBase.voznje[i].StatusVoznje == StatusiVoznje.Obradjena || DataBase.voznje[i].StatusVoznje == StatusiVoznje.Formirana)
                            isAvailable = false;

                    if (isAvailable && DataBase.automobili[((Vozac)k).AutomobilID].TipAutomobila.ToString() == flag)
                        pom.Add(k.KorisnickoIme);
                    isAvailable = true;
                }
            }

            for (int i = 0; i < pom.Count - 1; i++)
            {
                for (int j = i + 1; j < pom.Count; j++)
                {
                    Lokacija l1 = DataBase.lokacije[((Vozac)DataBase.Korisnici[pom[i]]).LokacijaID];
                    double razdaljina1 = Math.Sqrt(Math.Pow((x - l1.XKoordinata), 2) + Math.Pow((y- l1.YKoordinata), 2));
                    Lokacija l2 = DataBase.lokacije[((Vozac)DataBase.Korisnici[pom[j]]).LokacijaID];
                    double razdaljina2 = Math.Sqrt(Math.Pow((x - l2.XKoordinata), 2) + Math.Pow((y - l2.YKoordinata), 2));
                    if (razdaljina1 > razdaljina2)
                    {
                        string temp = pom[i];
                        pom[i] = pom[j];
                        pom[j] = temp;
                    }
                }
            }
            if (pom.Count > 5)
                lista = pom.GetRange(0, 5);
            else
                lista = pom;


            return lista;
        }
        #endregion

        #region slobodni korisnici za formiranje
        [Route("api/korisnici/getavailabledrivers")]
        [Authorize(Roles = "Dispecer")]
        [HttpGet]
        public List<string> DriversGet()
        {
            bool isAvailable = true;
            List<string> lista = new List<string>();
            foreach (Korisnik k in DataBase.Korisnici.Values)
            {
                if (k.Uloga == Uloge.Vozac && k.KorisnickoIme != "-1")
                {
                    foreach (int i in k.VoznjeID)
                        if (DataBase.voznje[i].StatusVoznje == StatusiVoznje.Prihvacena || DataBase.voznje[i].StatusVoznje == StatusiVoznje.Obradjena || DataBase.voznje[i].StatusVoznje == StatusiVoznje.Formirana)
                            isAvailable = false;

                    if (isAvailable && DataBase.automobili[((Vozac)k).AutomobilID].TipAutomobila == TipoviAutomobila.PutnickiAutomobil)
                        lista.Add(k.KorisnickoIme);
                    isAvailable = true;
                }
            }

            return lista;
        }
        #endregion

        #region Korisnik po ID
        // GET: api/Korisnici/5
        public Korisnik Get(string id)
        {
            return DataBase.Korisnici[id];
        }
        [Route("api/korisnici/editlocation")]
        #endregion

        #region Promena Lokacije Vozaca
        public IHttpActionResult EditLocation([FromBody]LokacijaBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Lokacija lokacija;
            Adresa adresa = new Adresa( model.Broj,model.Ulica, model.Grad, model.PostanskiBroj);
            if (model.XKoordinata == -1 && model.YKoordinata == -1)
                lokacija  = new Lokacija(adresa.Id);
            else
                lokacija = new Lokacija(model.XKoordinata,model.YKoordinata,adresa.Id);
            DataBase.adrese.Add(adresa.Id,adresa);
            DataBase.lokacije.Add(lokacija.Id, lokacija);
            ((Vozac)DataBase.Korisnici[User.Identity.Name]).LokacijaID = lokacija.Id;

            return Ok();
        }
        #endregion

        #region Brisanje Korisnika po ID
        // DELETE: api/Korisnici/5
        public void Delete(string id)
        {
            DataBase.Korisnici.Remove(id);
        }
        #endregion
    }
}
