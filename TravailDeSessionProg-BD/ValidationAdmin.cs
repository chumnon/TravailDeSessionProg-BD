using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    internal class ValidationAdmin
    {
        static ValidationAdmin instance;
        public ValidationAdmin() { }

        public static ValidationAdmin getInstance()
        {
            if (instance == null)
                instance = new ValidationAdmin();

            return instance;
        }

        public bool isNomValide(string nom)
        {
            if (!string.IsNullOrEmpty(nom.Trim()))
                return true;
            else
                return false;
        }

        public bool isMdpValide(string mdp)
        {
            if (!string.IsNullOrEmpty(mdp.Trim()))
                return true;
            else
                return false;
        }

        public bool isMdpConfValide(string mdp, string conf)
        {
            if (mdp.Trim() == conf.Trim())
                return true;
            else
                return false;
        }
    }
}
