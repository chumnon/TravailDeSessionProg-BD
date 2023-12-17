using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    internal class Tache
    {
        public string NumTache { get; set; }
        public string LEmploye { get; set; }
        public double TauxHoraireEmploye { get; set; }
        public string TauxHoraireEmployeFormat{ get => TauxHoraireEmploye.ToString("C"); }
        public string LeProjet { get; set; }
        public double Salaire { get; set; }
        public string SalaireFormat { get => Salaire.ToString("C"); }
        public int NbrHeure { get; set; }
    }


}
