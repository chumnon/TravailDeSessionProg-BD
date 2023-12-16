using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    internal class ValidationClient
    {
        static ValidationClient instance;
        public ValidationClient() { }

        public static ValidationClient getInstance()
        {
            if (instance == null)
                instance = new ValidationClient();

            return instance;
        }

        public bool isNomValide(string nom)
        {
            if (!string.IsNullOrEmpty(nom.Trim()))
                return true;
            else
                return false;
        }
        public bool isAdresseValide(string adresse)
        {
            if (!string.IsNullOrEmpty(adresse.Trim()))
                return true;
            else
                return false;
        }
        public bool isNumTelValide(string numTel)
        {
            if (!string.IsNullOrEmpty(numTel.Trim()))
                return true;
            else
                return false;
        }
        public bool isEmailValide(string email)
        {
            if (!string.IsNullOrEmpty(email.Trim()))
                return true;
            else
                return false;
        }
    }
}
