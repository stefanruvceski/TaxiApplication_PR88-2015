using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public enum Polovi { Muski,Zenski};
    public enum Uloge { Musterija,Vozac,Dispecer};
    public enum TipoviAutomobila { PutnickiAutomobil,KombiVozilo};
    public enum StatusiVoznje { Kreirana_NaCekanju, Formirana, Obradjena, Prihvacena, Otkazana, Neuspesna, Uspesna };

    public enum Ocene { Nula,Jedan,Dva,Tri,Cetiri,Pet };


}