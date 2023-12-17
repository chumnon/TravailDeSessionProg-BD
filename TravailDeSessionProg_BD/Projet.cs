using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    internal class Projet
    {
        public string Numero { get; set; }
        public string Titre { get; set; }
        public string DateDebut { get; set; }
        public string Description { get; set; }
        public string Budget { get; set; }
        public int NbrEmploye { get; set; }
        public double SalaireTotal { get; set; }
        public string SalaireTotalFormat { get => SalaireTotal.ToString("C"); }
        public int SonClient { get; set; }
        public string Statut { get; set; }
        public string ToStringProjet()
        {
            string leString = Numero + ";" + Titre + ";" + DateDebut.Substring(0, 10) + ";" + Description + ";" + Budget + ";" + NbrEmploye.ToString() + ";" + SalaireTotal.ToString() + ";" + SonClient.ToString() + ";" + Statut;
            return leString;
        }
    }
}
