﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApplication.Models.Klase
{
    public class Automobil
    {
        //Key
        int brojTaskiVozila;
        string vozac;
        int godiste;
        string brojRegistarskeOznake;
        TipoviAutomobila tipAutomobila;

        public Automobil(int brojTaskiVozila, int godiste,string vozac, string brojRegistarskeOznake, TipoviAutomobila tipAutomobila)
        {
            this.brojTaskiVozila = brojTaskiVozila;
            this.vozac = vozac;
            this.godiste = godiste;
            this.brojRegistarskeOznake = brojRegistarskeOznake;
            this.tipAutomobila = tipAutomobila;
        }

        public int BrojTaskiVozila { get => brojTaskiVozila; set => brojTaskiVozila = value; }
        public string Vozac { get => vozac; set => vozac = value; }
        public int Godiste { get => godiste; set => godiste = value; }
        public string BrojRegistarskeOznake { get => brojRegistarskeOznake; set => brojRegistarskeOznake = value; }
        public TipoviAutomobila TipAutomobila { get => tipAutomobila; set => tipAutomobila = value; }
    }
}