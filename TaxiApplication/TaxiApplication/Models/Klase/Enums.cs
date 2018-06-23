using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public enum Polovi { Muski,Zenski};
    public enum Uloge { Musterija,Vozac,Dispecer};
    public enum TipoviAutomobila { PutnickiAutomobil,KombiVozilo};
    public enum StatusiVoznje { Kreirana_NaCekanju =0, Formirana=1, Obradjena=2, Prihvacena=3, Otkazana=4, Neuspesna=5, Uspesna=6 };

    public enum Ocene {Nedefinisano=-1, Nula=0,Jedan=1,Dva=2,Tri=3,Cetiri=4,Pet=5 };


}