using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TaxiApplication.Models.Klase;

namespace TaxiApplication.Models
{
    #region Lozinka
    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    #endregion

    #region Korisnik
    public class MusterijaBindingModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string KorisnickoIme { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Ime { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Prezime { get; set; }

        [Required]
        public string Pol { get; set; }

        [Required]
        [MinLength(13,ErrorMessage ="JMBG mora imati 13 cifara")]
        [MaxLength(13, ErrorMessage = "JMBG mora imati 13 cifara")]
        
        public string Jmbg { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string KontaktTelefon { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

    public class KorisnikBindingModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string KorisnickoIme { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Ime { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Prezime { get; set; }

        [Required]
        public string Pol { get; set; }

        [Required]
        [MinLength(13, ErrorMessage = "JMBG mora imati 13 cifara")]
        [MaxLength(13, ErrorMessage = "JMBG mora imati 13 cifara")]
        public string Jmbg { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string KontaktTelefon { get; set; }

        [Required]
        public string Uloga { get; set; }


        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Ulica { get; set; }

        [Required]
        public string Broj { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Grad { get; set; }

        [Required]
        public string PostanskiBroj { get; set; }

        [Required]
        public string BrojTaksiVozila { get; set; }

        public string TipAutomobila { get; set; }

        [Required]
        public string GodisteAutomobila { get; set; }

        [Required]
        public string BrojRegistarskeOznake { get; set; }
        [Required]
        public double XKoordinata { get; set; }
        [Required]
        public double YKoordinata { get; set; }
    }

    public class EditKorisnikBindingModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string KorisnickoIme { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Ime { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Prezime { get; set; }
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string KontaktTelefon { get; set; }

        [Required]
        public string Email { get; set; }
    }
    
    public class EditVozacBindingModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string KorisnickoIme { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Ime { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Prezime { get; set; }
        
        [Required]
        public string KontaktTelefon { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Email { get; set; }
        
        [Required]
        public string BrojTaksiVozila { get; set; }

        [Required]
        public string godisteAutomobila { get; set; }

        [Required]
        public string tipAutomobila { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(12)]
        public string BrojRegistarskeOznake { get; set; }
    }

    public class VozacBindingModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string KorisnickoIme { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Ime { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Prezime { get; set; }

        [Required]
        public string Pol { get; set; }

        [Required]
        [MinLength(13, ErrorMessage = "JMBG mora imati 13 cifara")]
        [MaxLength(13, ErrorMessage = "JMBG mora imati 13 cifara")]
        public string Jmbg { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string KontaktTelefon { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string BrojTaksiVozila { get; set; }
        [Required]
        public string GodisteAutomobila { get; set; }
        [Required]
        public string TipAutomobila { get; set; }
        [Required]
        public string BrojRegistarskeOznake { get; set; }
    }
    #endregion

    #region Lokacija
    public class LokacijaBindingModel
    {
        public double XKoordinata { get; set; }
        public double YKoordinata { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Ulica { get; set; }
        [Required]
        public int Broj { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Grad { get; set; }
        [Required]
        public int PostanskiBroj { get; set; }
    }
    #endregion

    #region Voznja
    public class VozacVoznjaBindingModel
    {
        [Required]
        public string VoznjaID { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Polaziste { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Odrediste { get; set; }

        [Required]
        public string KorisnikID { get; set; }

        [Required]
        public string DispecerID { get; set; }

        [Required]
        public string StatusVoznje { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public string DatumIVreme { get; set; }

        [Required]
        public string Ocena { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public string Iznos { get; set; }

        [Required]
        public int Flag { get; set; }

    }

    public class StatusVoznjeBindingModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Ulica { get; set; }

        [Required]
        public int Broj { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Grad { get; set; }

        [Required]
        public int PostanskiBroj { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(300)]
        public string Opis { get; set; }

        [Required]
        public string Iznos { get; set; }
        public double XKoordinata { get; set; }
        public double YKoordinata { get; set; }
    }

    public class VoznjaDetailsBindingModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Adresa1 { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Adresa2 { get; set; }

        [Required]
        public string TipAutomobila { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string KorisnickoImeKorisnika { get; set; }

        [Required]
        public string Ocena { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(350)]
        public string Opis { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string StatusVoznje { get; set; }

        [Required]
        public string Iznos { get; set; }

        [Required]
        public string BrojTaksija { get; set; }

        [Required]
        public string DatumIVreme { get; set; }
    }

    public class KorisnikVoznjaBindingModel
    {
        [Display(Name = "X Koordinata")]
        public double XKoordinata { get; set; }

        [Display(Name = "Y Koordinata")]
        public double YKoordinata { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Ulica { get; set; }

        [Required]
        public int Broj { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Grad { get; set; }

        [Required]
        public int PostanskiBroj { get; set; }

        [Required]
        public string TipAutomobila { get; set; }

        [Required]
        public string VoznjaID { get; set; }
    }
    public class DispecerVoznjaBindingModel
    {
        public double XKoordinata { get; set; }

        public double YKoordinata { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Ulica { get; set; }

        [Required]
        public int Broj { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [Display(Name = "Grad")]
        public string Grad { get; set; }

        [Required]
        public int PostanskiBroj { get; set; }

        [Required]
        public string TipAutomobila { get; set; }

        [Required]
        public string VoznjaID { get; set; }

        [Required]
        public string VozacID { get; set; }
    }

    public class GetDispecerVoznjaBindingModel
    {
        //[Required]
        //[Display(Name = "X Koordinata")]
        //public double XKoordinata { get; set; }
        //[Required]
        //[Display(Name = "Y Koordinata")]
        //public double YKoordinata { get; set; }

        [Required]
        public string VoznjaID { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Vozac { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Musterija { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Dispecer { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Polaziste { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Odrediste { get; set; }

        [Required]
        public string TipAutomobila { get; set; }

        [Required]
        public string Ocena { get; set; }

        [Required]
        public string StatusVoznje { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public string DatumIVreme { get; set; }

        [Required]
        public string BrojTaksija { get; set; }
    }

    public class ProcessDispecerVoznjaBindingModel
    {
        //[Required]
        //[Display(Name = "X Koordinata")]
        //public double XKoordinata { get; set; }
        //[Required]
        //[Display(Name = "Y Koordinata")]
        //public double YKoordinata { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Vozac { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Musterija { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Adresa { get; set; }

        [Required]
        public List<string> Vozaci { get; set; }

        [Required]
        public string TipAutomobila { get; set; }

        [Required]
        public string StatusVoznje { get; set; }
    }

    public class ProcessVoznjaBindingModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string VozacID { get; set; }

        [Required]
        public string VoznjaID { get; set; }
    }

    public class GetKorisnikVoznjaBindingModel
    {
        //[Required]
        //[Display(Name = "X Koordinata")]
        //public double XKoordinata { get; set; }
        //[Required]
        //[Display(Name = "Y Koordinata")]
        //public double YKoordinata { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string VoznjaID { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Polaziste { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Odrediste { get; set; }

        [Required]
        public string TipAutomobila { get; set; }

        [Required]
        public string Ocena { get; set; }

        [Required]
        public string StatusVoznje { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public string DatumIVreme { get; set; }

        [Required]
        public string BrojTaksija { get; set; }

        
    }
    #endregion

    #region Komentar
    public class KomentarBindingModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(350)]
        public string Opis { get; set; }
        [Required]
        public string VoznjaID { get; set; }
        [Required]
        public string Ocena { get; set; }
    }
    #endregion
}
