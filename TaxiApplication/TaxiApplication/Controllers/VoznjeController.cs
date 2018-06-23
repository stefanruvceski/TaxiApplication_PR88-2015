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
        #region Lista voznji za dispecera po flag-u ->ne koristi se vise
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
                    model.DatumIVreme = voznja.Value.DatumIVreme.ToLongDateString();
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
        #endregion
        #region Lista voznji za musteriju po svim pretragama i filterima
        // GET: api/Voznje/flag/datum/ocena/odDatum/doDatum/odOcena/doOcena/odCena/doCena
        public List<GetKorisnikVoznjaBindingModel> GetM(int flagg, string datum, string ocena, DateTime odDatum, DateTime doDatum, int odOcena, int doOcena, int odCena, int doCena)
        {
            List<GetKorisnikVoznjaBindingModel> lista = new List<GetKorisnikVoznjaBindingModel>();
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
                    string ocenaa = DataBase.komentari[komentarID].Ocena.ToString();
                    if (ocenaa == "Nula")
                        ocenaa = "Nedefinisano";


                    GetKorisnikVoznjaBindingModel v = new GetKorisnikVoznjaBindingModel()
                    {
                        VoznjaID = vv.ToString(),
                        Polaziste = DataBase.adrese[polazisteID].ToBindingString(),
                        Odrediste = odredisteID,
                        Ocena = ocenaa,
                        TipAutomobila = DataBase.voznje[vv].TipAutomobila.ToString(),
                        DatumIVreme = DataBase.voznje[vv].DatumIVreme.ToString(),
                        StatusVoznje = DataBase.voznje[vv].StatusVoznje.ToString(),
                        BrojTaksija = brojtaksija
                    };
                    lista.Add(v);
                }
            }
            
            if (flagg == -1)
            {
               
            }
            else
            {
                StatusiVoznje s = (StatusiVoznje)flagg;
                List<GetKorisnikVoznjaBindingModel> l = new List<GetKorisnikVoznjaBindingModel>();

                foreach (var item in lista)
                {
                    if (s.ToString() == item.StatusVoznje)
                        l.Add(item);
                }

                lista = l;
            }

            List<GetKorisnikVoznjaBindingModel> pom2 = new List<GetKorisnikVoznjaBindingModel>();
            lista.ForEach(x => pom2.Add(x));
            for (int i = 0; i < lista.Count; i++)
            {
                if (DateTime.Parse(lista[i].DatumIVreme) < odDatum || DateTime.Parse(lista[i].DatumIVreme) > doDatum)
                {
                    try
                    {
                        pom2.Remove(lista[i]);
                    }
                    catch { }
                }
            }

            if (odCena == 0)
                odCena = -1;
            for (int i = 0; i < lista.Count; i++)
            {
                if ((int)DataBase.komentari[DataBase.voznje[int.Parse(lista[i].VoznjaID)].KomentarID].Ocena < odOcena || (int)DataBase.komentari[DataBase.voznje[int.Parse(lista[i].VoznjaID)].KomentarID].Ocena > doOcena)
                {
                    try
                    {
                        pom2.Remove(lista[i]);
                    }
                    catch { }
                }
            }

            for (int i = 0; i < lista.Count; i++)
            {
                if (DataBase.voznje[int.Parse(lista[i].VoznjaID)].Iznos < odCena || DataBase.voznje[int.Parse(lista[i].VoznjaID)].Iznos > doCena)
                {
                    try
                    {
                        pom2.Remove(lista[i]);
                    }
                    catch { }
                }
            }
            if (datum == "datum" && ocena == "ocena")
            {
                pom2 = pom2.OrderByDescending(z => Enum.Parse(typeof(Ocene), z.Ocena)).ThenByDescending(x => DateTime.Parse(x.DatumIVreme)).ToList();
            }
            else if (datum == "datum")
            {
                pom2 = pom2.OrderByDescending(x => DateTime.Parse(x.DatumIVreme)).ToList();
            }
            else if (ocena == "ocena")
            {
                pom2 = pom2.OrderByDescending(z => Enum.Parse(typeof(Ocene), z.Ocena)).ToList();
            }

            return pom2;

        }
        #endregion
        #region Vozac preuzima voznju na cekanju
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
        #endregion

        #region Lista voznji za vozaca po flag-u ->treba napraviti za filtere
        // GET: api/Voznje/flag/datum/ocena/odDatum/doDatum/odOcena/doOcena/odCena/doCena
        [Authorize(Roles = "Vozac")]
        public List<VozacVoznjaBindingModel> GetV(int flaggg, string datum, string ocena, DateTime odDatum, DateTime doDatum, int odOcena, int doOcena, int odCena, int doCena)
        {
            List<VozacVoznjaBindingModel> pom2 = new List<VozacVoznjaBindingModel>();
            List<VozacVoznjaBindingModel> lista = new List<VozacVoznjaBindingModel>();
            //voznje.Add(new Voznja());
            if (flaggg == 1)
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

                        string ocenaa = DataBase.komentari[komentarID].Ocena.ToString();
                        if (ocenaa == "Nula")
                            ocenaa = "Nedefinisano";
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
                            Ocena = ocenaa,
                            KorisnikID = korisnikID,
                            DispecerID = dispecerID,
                            DatumIVreme = DataBase.voznje[vv].DatumIVreme.ToString(),
                            StatusVoznje = DataBase.voznje[vv].StatusVoznje.ToString(),
                            Iznos = iznos
                        };
                        lista.Add(v);
                    }
                }
            }
            else if (flaggg == -1)
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

                        lista.Add(v);
                    }
                }
            }
            if (lista.Count == 0)
            {
                lista.Add(new VozacVoznjaBindingModel());
                pom2 = lista;
            }
            else
            {
               
                lista.ForEach(x => pom2.Add(x));
                for (int i = 0; i < lista.Count; i++)
                {
                    if (DateTime.Parse(lista[i].DatumIVreme) < odDatum || DateTime.Parse(lista[i].DatumIVreme) > doDatum)
                    {
                        try
                        {
                            pom2.Remove(lista[i]);
                        }
                        catch { }
                    }
                }

                if (odCena == 0)
                    odCena = -1;
                for (int i = 0; i < lista.Count; i++)
                {
                    if ((int)DataBase.komentari[DataBase.voznje[int.Parse(lista[i].VoznjaID)].KomentarID].Ocena < odOcena || (int)DataBase.komentari[DataBase.voznje[int.Parse(lista[i].VoznjaID)].KomentarID].Ocena > doOcena)
                    {
                        try
                        {
                            pom2.Remove(lista[i]);
                        }
                        catch { }
                    }
                }

                for (int i = 0; i < lista.Count; i++)
                {
                    if (DataBase.voznje[int.Parse(lista[i].VoznjaID)].Iznos < odCena || DataBase.voznje[int.Parse(lista[i].VoznjaID)].Iznos > doCena)
                    {
                        try
                        {
                            pom2.Remove(lista[i]);
                        }
                        catch { }
                    }
                }
                if (datum == "datum" && ocena == "ocena")
                {
                    pom2 = pom2.OrderByDescending(z => Enum.Parse(typeof(Ocene), z.Ocena)).ThenByDescending(x => DateTime.Parse(x.DatumIVreme)).ToList();
                }
                else if (datum == "datum")
                {
                    pom2 = pom2.OrderByDescending(x => DateTime.Parse(x.DatumIVreme)).ToList();
                }
                else if (ocena == "ocena")
                {
                    pom2 = pom2.OrderByDescending(z => Enum.Parse(typeof(Ocene), z.Ocena)).ToList();
                }

                
            }

            if (DataBase.voznje.Values.ToList().FindAll(x=>(x.VozacID == User.Identity.Name) && (x.StatusVoznje == StatusiVoznje.Obradjena || x.StatusVoznje == StatusiVoznje.Prihvacena || x.StatusVoznje == StatusiVoznje.Formirana)).Count > 0)
            {
                pom2[0].Flag = 1;
            }
            else
            {
                pom2[0].Flag = -1;
            }

            return pom2;





        }
        #endregion
        #region Vozac zavrsio voznju
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
        #endregion
        #region Voznja po id -> proveriti da li se igde koristi
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
        #endregion
        #region Lista voznji za dispecere po svim pretragama i filterima
        // GET: api/Voznje/flag/datum/ocena/odDatum/doDatum/odOcena/doOcena/odCena/doCena/vIme/vPrezime/mIme/mPrezime
        public List<GetDispecerVoznjaBindingModel> GetD(int flag,string datum,string ocena, DateTime odDatum, DateTime doDatum, int odOcena, int doOcena, int odCena, int doCena,string mIme,string mPrezime, string vIme, string vPrezime)
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

            if (flag == 1)
            {
               
            }
            else
            {
                List<GetDispecerVoznjaBindingModel> pom = new List<GetDispecerVoznjaBindingModel>();
                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Dispecer == User.Identity.Name)
                        pom.Add(lista[i]);
                }

                lista = pom;
            }

            List<GetDispecerVoznjaBindingModel> pom2 = new List<GetDispecerVoznjaBindingModel>();
            lista.ForEach(x => pom2.Add(x));
            for (int i = 0; i < lista.Count; i++)
            {
                if (DateTime.Parse( lista[i].DatumIVreme) < odDatum || DateTime.Parse(lista[i].DatumIVreme) > doDatum)
                {
                    try
                    {
                        pom2.Remove(lista[i]);
                    }
                    catch { }
                }
            }

            if (odCena == 0)
                odCena = -1;
            for (int i = 0; i < lista.Count; i++)
            {
                if ((int)DataBase.komentari[DataBase.voznje[int.Parse(lista[i].VoznjaID)].KomentarID].Ocena < odOcena || (int)DataBase.komentari[DataBase.voznje[int.Parse(lista[i].VoznjaID)].KomentarID].Ocena > doOcena)
                {
                    try
                    {
                        pom2.Remove(lista[i]);
                    }
                    catch { }
                }
            }

            for (int i = 0; i < lista.Count; i++)
            {
                if (DataBase.voznje[int.Parse(lista[i].VoznjaID)].Iznos < odCena || DataBase.voznje[int.Parse(lista[i].VoznjaID)].Iznos > doCena)
                {
                    try
                    {
                        pom2.Remove(lista[i]);
                    }
                    catch { }
                }
            }
            Korisnik musterija = null;
            Korisnik vozac = null;
            try
            {
                musterija = DataBase.Korisnici.Values.ToList().Find(x => x.Ime.ToLower() == mIme.ToLower() && x.Prezime.ToLower() == mPrezime.ToLower());
            }
            catch { }
            try
            {
                vozac = DataBase.Korisnici.Values.ToList().Find(x => x.Ime.ToLower() == vIme.ToLower() && x.Prezime.ToLower() == vPrezime.ToLower());
            }
            catch { }
            if(musterija != null)
            {
                pom2 = pom2.FindAll(x => x.Musterija == musterija.KorisnickoIme);
            }

            if (vozac != null)
            {
                pom2 = pom2.FindAll(x => x.Vozac == vozac.KorisnickoIme);
            }


            if (datum == "datum" && ocena == "ocena")
            {
                pom2 = pom2.OrderByDescending(z => Enum.Parse(typeof(Ocene), z.Ocena)).ThenByDescending(x => DateTime.Parse(x.DatumIVreme)).ToList();
            }
            else if(datum == "datum")
            {
                pom2 = pom2.OrderByDescending(x => DateTime.Parse(x.DatumIVreme)).ToList();
            }
            else if(ocena == "ocena")
            {
                pom2 = pom2.OrderByDescending(z=>Enum.Parse(typeof(Ocene), z.Ocena)).ToList();
            }

            return pom2;
        }
        #endregion
        #region Detalji o voznji
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

            if (DataBase.voznje[id].Iznos == -1)
                voznja.Iznos = "Nedefinisano";
            else
                voznja.Iznos = DataBase.voznje[id].Iznos.ToString();


            return voznja;
        }
        #endregion
        #region Dispecer obradjuje voznju koja je na cekanju
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
        #endregion
        #region Musterija narucuje taksi
        // POST: api/Voznje
        [Authorize(Roles ="Musterija")]
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
        #endregion
        #region Dispecer formira voznju
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
        #endregion
        #region Dispecer obradjuje voznju koja je na cekanju->ne koristi se koliko vidim
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
        #endregion

        #region put voznje ne koristim
        // PUT: api/Voznje/5
        public void Put(int id, [FromBody]int value)
        {
        }
        #endregion
        #region Korisnik brise voznju tj stavlja je u status otkazana
        // DELETE: api/Voznje/5
        public IHttpActionResult Delete(int id)
        {
            DataBase.voznje[id].StatusVoznje = StatusiVoznje.Otkazana;

            return Ok();
        }
        #endregion
    }
}
