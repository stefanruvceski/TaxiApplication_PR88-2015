using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaxiApplication.Models
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    #region MOJE
    public class MusterijaBindingModel
    {
        [Required]
        [Display(Name = "Korisnicko Ime")]
        public string KorisnickoIme { get; set; }
        [Required]
        [Display(Name = "Ime")]
        public string Ime { get; set; }
        [Required]
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }
        [Required]
        [Display(Name = "Pol")]
        public string Pol { get; set; }
        [Required]
        [Display(Name = "JMBG")]
        public string Jmbg { get; set; }
        [Required]
        [Display(Name = "Kontakt Telefon")]
        public string KontaktTelefon { get; set; }
        

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrda Lozinke")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class EditKorisnikBindingModel
    {
        [Required]
        [Display(Name = "Korisnicko Ime")]
        public string KorisnickoIme { get; set; }
        [Required]
        [Display(Name = "Ime")]
        public string Ime { get; set; }
        [Required]
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }
       
        [Required]
        [Display(Name = "Kontakt Telefon")]
        public string KontaktTelefon { get; set; }


        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        
    }

    public class EditVozacBindingModel
    {
        [Required]
        [Display(Name = "Korisnicko Ime")]
        public string KorisnickoIme { get; set; }
        [Required]
        [Display(Name = "Ime")]
        public string Ime { get; set; }
        [Required]
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }

        [Required]
        [Display(Name = "Kontakt Telefon")]
        public string KontaktTelefon { get; set; }


        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Broj Taksi Vozila")]
        public string BrojTaksiVozila { get; set; }
        [Required]
        [Display(Name = "Godiste Automobila")]
        public string godisteAutomobila { get; set; }
        [Required]
        [Display(Name = "Tip Automobila")]
        public string tipAutomobila { get; set; }
        [Required]
        [Display(Name = "Broj Registarske Oznake")]
        public string BrojRegistarskeOznake { get; set; }


    }

    public class VozacBindingModel
    {
        [Required]
        [Display(Name = "Korisnicko Ime")]
        public string KorisnickoIme { get; set; }
        [Required]
        [Display(Name = "Ime")]
        public string Ime { get; set; }
        [Required]
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }
        [Required]
        [Display(Name = "Pol")]
        public string Pol { get; set; }
        [Required]
        [Display(Name = "JMBG")]
        public string Jmbg { get; set; }
        [Required]
        [Display(Name = "Kontakt Telefon")]
        public string KontaktTelefon { get; set; }


        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrda Lozinke")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Broj Taksi Vozila")]
        public string BrojTaksiVozila { get; set; }
        [Required]
        [Display(Name = "Godiste Automobila")]
        public string godisteAutomobila { get; set; }
        [Required]
        [Display(Name = "Tip Automobila")]
        public string tipAutomobila { get; set; }
        [Required]
        [Display(Name = "Broj Registarske Oznake")]
        public string BrojRegistarskeOznake { get; set; }
    }

    public class LokacijaBindingModel
    {
        [Required]
        [Display(Name = "X Koordinata")]
        public double XKoordinata { get; set; }
        [Required]
        [Display(Name = "Y Koordinata")]
        public double YKoordinata { get; set; }
        
        [Required]
        [Display(Name = "Ulica")]
        public string Ulica { get; set; }
        [Required]
        [Display(Name = "Broj Ulice")]
        public int Broj { get; set; }
        [Required]
        [Display(Name = "Grad")]
        public string Grad { get; set; }
        [Required]
        [Display(Name = "Postanski Broj")]
        public int PostanskiBroj { get; set; }
    }
    #endregion

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
