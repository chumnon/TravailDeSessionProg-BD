﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    internal class Employe
    {
        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string DateDeNaissance { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }
        public string DateEmbauche { get; set; }
        public double TauxHoraire { get; set; }
        public string FormatTauxHoraire { get => TauxHoraire.ToString() + "$/h"; }
        public string Photo { get; set; }
        public string Statut { get; set; }
    }
}
