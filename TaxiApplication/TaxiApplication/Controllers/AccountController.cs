﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using TaxiApplication.Models;
using TaxiApplication.Models.Klase;
using TaxiApplication.Providers;
using TaxiApplication.Results;

namespace TaxiApplication.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        #region Fields&Properties
        private const string LocalLoginProvider = "Local";
        private  ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public  ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
        #endregion

        #region Registration
        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(MusterijaBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Polovi p; Enum.TryParse(model.Pol, out p);
            Musterija m = new Musterija()
            {
                KorisnickoIme = model.KorisnickoIme,
                Ime = model.Ime,
                Prezime = model.Prezime,
                Lozinka = model.Password,
                Pol = p,
                Jmbg = model.Jmbg,
                KontaktTelefon = model.KontaktTelefon,
                Email = model.Email,
                Uloga = Uloge.Musterija,
                VoznjeID = new List<int>(),
            };

            DataBase.Korisnici.Add(m.KorisnickoIme, m);

            var user = new ApplicationUser() { UserName = m.KorisnickoIme, Email = m.Email, PhoneNumber = m.KontaktTelefon };
            IdentityUserRole r = new IdentityUserRole();
            r.UserId = user.Id;
            r.RoleId = "3";
            user.Roles.Add(r);
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/Register
        [Authorize(Roles = "Dispecer")]
        [Route("RegisterDriver")]
        public async Task<IHttpActionResult> Register(VozacBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //string[] s = Roles.GetAllRoles();

            Polovi p; Enum.TryParse(model.Pol, out p);
            TipoviAutomobila t; Enum.TryParse(model.TipAutomobila, out t);
            Automobil automobil = new Automobil(int.Parse(model.BrojTaksiVozila), int.Parse(model.GodisteAutomobila), model.KorisnickoIme, model.BrojRegistarskeOznake, t);
            DataBase.automobili.Add(automobil.BrojTaksiVozila, automobil);
            Vozac m = new Vozac()
            {
                KorisnickoIme = model.KorisnickoIme,
                Ime = model.Ime,
                Prezime = model.Prezime,
                Lozinka = model.Password,
                Pol = p,
                Jmbg = model.Jmbg,
                KontaktTelefon = model.KontaktTelefon,
                Email = model.Email,
                Uloga = Uloge.Vozac,
                VoznjeID = new List<int>(),
                AutomobilID = automobil.BrojTaksiVozila

            };

            DataBase.Korisnici.Add(m.KorisnickoIme, m);

            var user = new ApplicationUser() { UserName = m.KorisnickoIme, Email = m.Email, PhoneNumber = m.KontaktTelefon };
            IdentityUserRole r = new IdentityUserRole();
            r.UserId = user.Id;
            r.RoleId = "2";
            user.Roles.Add(r);
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        #endregion

        #region Edit
        [Authorize(Roles = "Musterija,Dispecer")]
        [Route("Edit")]
        public async Task<IHttpActionResult> Edit([FromBody]EditKorisnikBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DataBase.Korisnici[model.KorisnickoIme].Ime = model.Ime;
            DataBase.Korisnici[model.KorisnickoIme].Prezime = model.Prezime;
            DataBase.Korisnici[model.KorisnickoIme].Email = model.Email;
            DataBase.Korisnici[model.KorisnickoIme].KontaktTelefon = model.KontaktTelefon;
            //if(DataBase.Korisnici[User.Identity.Name].Uloga == Uloge.Vozac)
            //{
            //    DataBase.automobili[((Vozac)DataBase.Korisnici[model.KorisnickoIme]).AutomobilID].BrojRegistarskeOznake = model.BrojRegistarskeOznake;
            //    DataBase.automobili[((Vozac)DataBase.Korisnici[model.KorisnickoIme]).AutomobilID].Godiste = int.Parse(model.GodisteAutomobila);
            //    TipoviAutomobila t; Enum.TryParse(model.TipAutomobila, out t);
            //    DataBase.automobili[((Vozac)DataBase.Korisnici[model.KorisnickoIme]).AutomobilID].TipAutomobila = t;
            //}
            ApplicationUser applicationUser = await UserManager.FindAsync(model.KorisnickoIme, DataBase.Korisnici[model.KorisnickoIme].Lozinka);

            IdentityResult result1 = UserManager.SetEmail(applicationUser.Id, model.Email);

            IdentityResult result2 = UserManager.SetPhoneNumber(applicationUser.Id, model.KontaktTelefon);

            if (!result1.Succeeded)
            {
                return GetErrorResult(result1);
            }
            else if (!result2.Succeeded)
            {
                return GetErrorResult(result2);
            }

            return Ok();
        }

        [Authorize(Roles= "Vozac")]
        [Route("EditVozac")]
        public async Task<IHttpActionResult> Edit([FromBody]EditVozacBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoviAutomobila t; Enum.TryParse(model.tipAutomobila, out t);
            Automobil a = new Automobil(int.Parse(model.BrojTaksiVozila), int.Parse(model.godisteAutomobila), model.BrojRegistarskeOznake, model.KorisnickoIme, t);
            DataBase.automobili[int.Parse(model.BrojTaksiVozila)] = a;
            DataBase.Korisnici[model.KorisnickoIme].Ime = model.Ime;
            DataBase.Korisnici[model.KorisnickoIme].Prezime = model.Prezime;
            DataBase.Korisnici[model.KorisnickoIme].Email = model.Email;
            DataBase.Korisnici[model.KorisnickoIme].KontaktTelefon = model.KontaktTelefon;
            ((Vozac)DataBase.Korisnici[model.KorisnickoIme]).AutomobilID = a.BrojTaksiVozila;

            ApplicationUser applicationUser = await UserManager.FindAsync(model.KorisnickoIme, DataBase.Korisnici[model.KorisnickoIme].Lozinka);

            IdentityResult result1 = UserManager.SetEmail(applicationUser.Id, model.Email);

            IdentityResult result2 = UserManager.SetPhoneNumber(applicationUser.Id, model.KontaktTelefon);

            if (!result1.Succeeded)
            {
                return GetErrorResult(result1);
            }
            else if (!result2.Succeeded)
            {
                return GetErrorResult(result2);
            }

            return Ok();
        }
        #endregion

        #region Logout
        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }
        #endregion

        #region Change Password
        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);
            
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            DataBase.Korisnici[User.Identity.Name].Lozinka = model.NewPassword;

            return Ok();
        }
        #endregion
        
        #region Helpers

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
