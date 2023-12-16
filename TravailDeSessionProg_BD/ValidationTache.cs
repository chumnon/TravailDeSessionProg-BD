using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    internal class ValidationTache
    {
        static ValidationTache instance;
        public ValidationTache() { }

        public static ValidationTache getInstance()
        {
            if (instance == null)
                instance = new ValidationTache();

            return instance;
        }

        public bool isEmployeValide(string employe)
        {
            if (!string.IsNullOrEmpty(employe.Trim()))
                return true;
            else
                return false;
        }

        public bool isNbrHeureValide(string nbrHeure)
        {
            int p = 0;
            if (!string.IsNullOrEmpty(nbrHeure.Trim()))
            {
                if (int.TryParse(nbrHeure, out p))
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }
}
