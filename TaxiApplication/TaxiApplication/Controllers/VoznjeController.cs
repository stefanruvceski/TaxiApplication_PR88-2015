﻿using System;
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
        [Route("api/voznje/getbyflag/{flag:int}")]
        [Authorize(Roles = "Dispecer")]
        [HttpGet]
        public List<GetDispecerVoznjaBindingModel> GetByFlag(int flag)
        {
            List<GetDispecerVoznjaBindingModel> lista = new List<GetDispecerVoznjaBindingModel>();
            GetDispecerVoznjaBindingModel model;
            foreach (KeyValuePair<int, Voznja> voznja in DataBase.voznje)
            {
                if (voznja.Key != -1)
                {
                    model = new GetDispecerVoznjaBindingModel();
                    model.VoznjaID = voznja.Key.ToString();
                    model.TipAutomobila = voznja.Value.TipAutomobila.ToString();
                    model.DatumIVreme = voznja.Value.DatumIVreme.ToString();
                    if (DataBase.komentari[voznja.Value.KomentarID].Ocena == Ocene.Nula)
                        model.Ocena = "Nedefinisano";
                    else
                        model.Ocena = DataBase.komentari[voznja.Value.KomentarID].Ocena.ToString();
                    model.Polaziste = DataBase.adrese[DataBase.lokacije[voznja.Value.LokacijaID].AdresaID].ToBindingString();
                    model.StatusVoznje = voznja.Value.StatusVoznje.ToString();
                    if (voznja.Value.VozacID == "-1")
                    {
                        model.Vozac = "Nedefinisano";
                        model.BrojTaksija = "Nedefinisano";
                        model.Odrediste = "Nedefinisano";

                    }
                    else
                    {
                        model.Vozac = voznja.Value.VozacID;
                        model.BrojTaksija = DataBase.automobili[((Vozac)DataBase.Korisnici[voznja.Value.VozacID]).AutomobilID].BrojTaksiVozila.ToString();
                        if (DataBase.adrese[DataBase.lokacije[voznja.Value.OdredisteID].AdresaID].Id == "-1")
                            model.Odrediste = "Nedefinisano";
                        else
                            model.Odrediste = DataBase.adrese[DataBase.lokacije[voznja.Value.OdredisteID].AdresaID].ToBindingString();
                    }
                    if (voznja.Value.MusterijaID != "-1")
                        model.Musterija = DataBase.Korisnici[voznja.Value.MusterijaID].KorisnickoIme;
                    else
                        model.Musterija = "Nedefinisano";

                    if (voznja.Value.DispecerID != "-1")
                        model.Dispecer = DataBase.Korisnici[voznja.Value.DispecerID].KorisnickoIme;
                    else
                        model.Dispecer = "Nedefinisano";

                    lista.Add(model);
                }
            }

            if(flag == 1)
            {
                return lista;
            }
            else
            {
                List<GetDispecerVoznjaBindingModel> pom = new List<GetDispecerVoznjaBindingModel>();
                for(int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Dispecer == User.Identity.Name)
                        pom.Add(lista[i]);
                }

                return pom;
            }
            
        }

        // GET: api/Voznje
        [Route("api/voznje/getmy/{flag:int}")]
        public List<GetKorisnikVoznjaBindingModel> GetMy(int flag)
        {
            List<GetKorisnikVoznjaBindingModel> voznje = new List<GetKorisnikVoznjaBindingModel>();
            //voznje.Add(new Voznja());
            
            if (DataBase.lokacije.Count > 1)
            {
                foreach (int vv in DataBase.Korisnici[User.Identity.Name].VoznjeID)
                {

                    string polazisteID = DataBase.lokacije[DataBase.voznje[vv].LokacijaID].AdresaID;

                    string odredisteID = "Nedefinisano";
                    if (DataBase.voznje[vv].OdredisteID != "-1")
                        odredisteID = DataBase.adrese[DataBase.lokacije[DataBase.voznje[vv].OdredisteID].AdresaID].ToBindingString();

                    int komentarID = DataBase.voznje[vv].KomentarID;
                    string brojtaksija = "";
                    string vozacID = DataBase.voznje[vv].VozacID;
                    string automobilID = "Nedefinisano";
                    if (vozacID != "-1")
                        automobilID = ((Vozac)DataBase.Korisnici[vozacID]).AutomobilID.ToString();

                    if (vozacID == "-1")
                    {
                        brojtaksija = "Nedefinisano";
                    }
                    else
                    {
                        brojtaksija = DataBase.automobili[int.Parse(automobilID)].BrojTaksiVozila.ToString();
                    }
                    string ocena = DataBase.komentari[komentarID].Ocena.ToString();
                    if (ocena == "Nula")
                        ocena = "Nedefinisano";


                    GetKorisnikVoznjaBindingModel v = new GetKorisnikVoznjaBindingModel()
                    {
                        VoznjaID = vv.ToString(),
                        Polaziste = DataBase.adrese[polazisteID].ToBindingString(),
                        Odrediste = odredisteID,
                        Ocena = ocena,
                        TipAutomobila = DataBase.voznje[vv].TipAutomobila.ToString(),
                        DatumIVreme = DataBase.voznje[vv].DatumIVreme.ToString(),
                        StatusVoznje = DataBase.voznje[vv].StatusVoznje.ToString(),
                        BrojTaksija = brojtaksija
                    };
                    voznje.Add(v);
                }
            }

            if (flag == -1)
            {
                return voznje;
            }
            else
            {
                StatusiVoznje s = (StatusiVoznje)flag;
                List<GetKorisnikVoznjaBindingModel> l = new List<GetKorisnikVoznjaBindingModel>();

                foreach (var item in voznje)
                {
                    if (s.ToString() == item.StatusVoznje)
                        l.Add(item);
                }

                return l;

            }

        }

        [Route("api/voznje/preuzmi/{id:int}")]
        [Authorize(Roles ="Vozac")]
        [HttpGet]
        public IHttpActionResult Preuzmi(int id)
        {
            DataBase.voznje[id].StatusVoznje = StatusiVoznje.Prihvacena;
            DataBase.voznje[id].VozacID = User.Identity.Name;
            DataBase.Korisnici[User.Identity.Name].VoznjeID.Add(id);

            return Ok();
        }

        [Route("api/voznje/getmyd/{flag:int}")]
        [Authorize(Roles = "Vozac")]
        public List<VozacVoznjaBindingModel> GetMyD(int flag)
        {
            List<VozacVoznjaBindingModel> voznje = new List<VozacVoznjaBindingModel>();
            //voznje.Add(new Voznja());
            if (flag == 1)
            {
                if (DataBase.lokacije.Count > 1)
                {
                    foreach (int vv in DataBase.Korisnici[User.Identity.Name].VoznjeID)
                    {

                        string polazisteID = DataBase.lokacije[DataBase.voznje[vv].LokacijaID].AdresaID;
                        string korisnikID;
                        if (DataBase.voznje[vv].MusterijaID != "-1")
                            korisnikID = DataBase.voznje[vv].MusterijaID;
                        else
                            korisnikID = "Nedefinisano";

                        string dispecerID;
                        if (DataBase.voznje[vv].DispecerID != "-1")
                            dispecerID = DataBase.voznje[vv].DispecerID;
                        else
                            dispecerID = "Nedefinisano";

                        string odredisteID = "Nedefinisano";
                        if (DataBase.voznje[vv].OdredisteID != "-1")
                            odredisteID = DataBase.adrese[DataBase.lokacije[DataBase.voznje[vv].OdredisteID].AdresaID].ToBindingString();

                        int komentarID = DataBase.voznje[vv].KomentarID;

                        string ocena = DataBase.komentari[komentarID].Ocena.ToString();
                        if (ocena == "Nula")
                            ocena = "Nedefinisano";
                        string iznos;
                        if (DataBase.voznje[vv].Iznos == -1)
                            iznos = "Nedefinisano";
                        else
                            iznos = DataBase.voznje[vv].Iznos.ToString();

                        VozacVoznjaBindingModel v = new VozacVoznjaBindingModel()
                        {
                            VoznjaID = vv.ToString(),
                            Polaziste = DataBase.adrese[polazisteID].ToBindingString(),
                            Odrediste = odredisteID,
                            Ocena = ocena,
                            KorisnikID = korisnikID,
                            DispecerID = dispecerID,
                            DatumIVreme = DataBase.voznje[vv].DatumIVreme.ToString(),
                            StatusVoznje = DataBase.voznje[vv].StatusVoznje.ToString(),
                            Iznos = iznos
                        };
                        voznje.Add(v);
                    }
                }
            }
            else if (flag == -1)
            {
                foreach(KeyValuePair<int,Voznja> vv in DataBase.voznje)
                {
                    if(vv.Value.StatusVoznje == StatusiVoznje.Kreirana_NaCekanju 
                        && vv.Value.Id != -1 
                        && vv.Value.TipAutomobila == DataBase.automobili[((Vozac)DataBase.Korisnici[User.Identity.Name]).AutomobilID].TipAutomobila)
                    {
                        VozacVoznjaBindingModel v = new VozacVoznjaBindingModel()
                        {
                            VoznjaID = vv.Key.ToString(),
                            Polaziste = DataBase.adrese[vv.Value.LokacijaID].ToBindingString(),
                            Odrediste = "Nedefinisano",
                            Ocena = "Nedefinisano",
                            KorisnikID = DataBase.Korisnici[DataBase.voznje[vv.Key].MusterijaID].KorisnickoIme,
                            DispecerID = "Nepoznato",
                            DatumIVreme = DataBase.voznje[vv.Key].DatumIVreme.ToString(),
                            StatusVoznje = DataBase.voznje[vv.Key].StatusVoznje.ToString(),
                            Iznos = "Nepoznato"
                        };

                        voznje.Add(v);
                    }
                }
            }


            return voznje;

        }

        [Route("api/voznje/zavrsena")]
        [Authorize(Roles ="Vozac")]
        public IHttpActionResult StatusVoznjePost([FromBody]StatusVoznjeBindingModel model)
        {
            try
            {
                StatusiVoznje v; Enum.TryParse(model.Status, out v);

                if (v == StatusiVoznje.Neuspesna)
                {
                    Komentar k = new Komentar(model.Opis, DateTime.Now, User.Identity.Name, int.Parse(model.Id));
                    DataBase.komentari.Add(k.Id, k);
                    DataBase.voznje[int.Parse(model.Id)].KomentarID = k.Id;
                }
                else
                {
                    Adresa a = new Adresa(model.Broj, model.Ulica, model.Grad, model.PostanskiBroj);
                    DataBase.adrese.Add(a.Id,a);
                    Lokacija l = new Lokacija(a.Id);
                    DataBase.lokacije.Add(l.Id, l);
                    DataBase.voznje[int.Parse(model.Id)].OdredisteID = l.Id;
                    ((Vozac)DataBase.Korisnici[User.Identity.Name]).LokacijaID = l.Id;
                    DataBase.voznje[int.Parse(model.Id)].Iznos = long.Parse(model.Iznos);
                }

                DataBase.voznje[int.Parse(model.Id)].StatusVoznje = v;

                return Ok();
            }
            catch
            {
                return InternalServerError(new Exception("Internal error"));
            }
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

        [Route("api/voznje/details/{id:int}")]
        [HttpGet]

        public VoznjaDetailsBindingModel Detail(int id)
        {
            //int id = int.Parse(idd);
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

        [Route("api/voznje/process/{id:int}")]
        [HttpGet]
        [Authorize(Roles = "Dispecer")]
        public ProcessDispecerVoznjaBindingModel Process(int id)
        {
            Adresa a1 = DataBase.adrese[DataBase.lokacije[DataBase.voznje[id].LokacijaID].AdresaID];
            Automobil a = DataBase.automobili[((Vozac)DataBase.Korisnici[DataBase.voznje[id].VozacID]).AutomobilID];
            ProcessDispecerVoznjaBindingModel voznja = new ProcessDispecerVoznjaBindingModel();

            voznja.Adresa = a1.ToBindingString();
            voznja.Musterija = DataBase.Korisnici[DataBase.voznje[id].MusterijaID].KorisnickoIme;
            voznja.StatusVoznje = DataBase.voznje[id].StatusVoznje.ToString();
            voznja.TipAutomobila = DataBase.voznje[id].TipAutomobila.ToString();
            voznja.Vozac = "Nedefinisano";

            voznja.Vozaci = new List<string>();

            bool isAvailable = true;
            foreach (Korisnik k in DataBase.Korisnici.Values)
            {
                if (k.Uloga == Uloge.Vozac && k.KorisnickoIme != "-1")
                {
                    foreach (int i in k.VoznjeID)
                        if (DataBase.voznje[i].StatusVoznje == StatusiVoznje.Prihvacena || DataBase.voznje[i].StatusVoznje == StatusiVoznje.Obradjena || DataBase.voznje[i].StatusVoznje == StatusiVoznje.Formirana)
                            isAvailable = false;

                    if (isAvailable && DataBase.automobili[((Vozac)k).AutomobilID].TipAutomobila.ToString() == voznja.TipAutomobila )
                        voznja.Vozaci.Add(k.KorisnickoIme);
                    isAvailable = true;
                }
            }
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
                        MusterijaID = User.Identity.Name,
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

        [Route("api/voznje/dispecerpost")]
        [HttpPost]
        [Authorize(Roles = "Dispecer")]
        public IHttpActionResult DispecerPost([FromBody]DispecerVoznjaBindingModel value)
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
                        DispecerID = User.Identity.Name,
                        LokacijaID = lokacija.Id,
                        TipAutomobila = t,
                        DatumIVreme = DateTime.Now,
                        VozacID = value.VozacID,
                        StatusVoznje = StatusiVoznje.Formirana
                    };
                    DataBase.Korisnici[value.VozacID].VoznjeID.Add(voznja.Id);
                    DataBase.voznje.Add(voznja.Id, voznja);
                    if (DataBase.Korisnici[User.Identity.Name].VoznjeID == null)
                        DataBase.Korisnici[User.Identity.Name].VoznjeID = new List<int>();
                    DataBase.Korisnici[User.Identity.Name].VoznjeID.Add(voznja.Id);
                }
                else
                {
                    string lokacijaID = DataBase.voznje[int.Parse(value.VoznjaID)].LokacijaID;
                    string adresaID = DataBase.lokacije[lokacijaID].AdresaID;
                    Adresa adresa = new Adresa(adresaID, value.Broj, value.Ulica, value.Grad, value.PostanskiBroj);
                    DataBase.adrese[adresaID] = adresa;
                    TipoviAutomobila t; Enum.TryParse(value.TipAutomobila, out t);
                    DataBase.voznje[int.Parse(value.VoznjaID)].TipAutomobila = t;
                    DataBase.voznje[int.Parse(value.VoznjaID)].VozacID = value.VozacID;
                    DataBase.voznje[int.Parse(value.VoznjaID)].StatusVoznje = StatusiVoznje.Formirana;
                    DataBase.Korisnici[value.VozacID].VoznjeID.Add(int.Parse(value.VoznjaID));
                }
                return Ok();
            }
            catch
            {
                return InternalServerError(new Exception("Order error"));
            }

        }

        [Route("api/voznje/dispecerprocess")]
        [HttpPost]
        [Authorize(Roles = "Dispecer")]
        public IHttpActionResult DispecerProcess([FromBody]ProcessVoznjaBindingModel value)
        {
            try
            {
                DataBase.voznje[int.Parse(value.VoznjaID)].VozacID = value.VozacID;
                DataBase.voznje[int.Parse(value.VoznjaID)].DispecerID = User.Identity.Name;
                DataBase.Korisnici[value.VozacID].VoznjeID.Add(int.Parse(value.VoznjaID));
                DataBase.voznje[int.Parse(value.VoznjaID)].StatusVoznje = StatusiVoznje.Obradjena;

              
                
                return Ok();
            }
            catch
            {
                return InternalServerError(new Exception("Process error"));
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
