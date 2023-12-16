using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    class ValidationEmploye
    {

        static ValidationEmploye instance;
        public ValidationEmploye() { }

        public static ValidationEmploye getInstance()
        {
            if (instance == null)
                instance = new ValidationEmploye();

            return instance;
        }

        public bool isNomValide(string nom)
        {
            if (!string.IsNullOrEmpty(nom.Trim()))
                return true;
            else
                return false;
        }
        public bool isPrenomValide(string prenom)
        {
            if (!string.IsNullOrEmpty(prenom.Trim()))
                return true;
            else
                return false;
        }
        public int isDateDeNaissanceValide(string dateDeNaissance)
        {
            DateTime dateMax = DateTime.Now.AddYears(-18);
            DateTime dateMin = DateTime.Now.AddYears(-65);
            DateTime dateChoisi = DateTime.Parse(dateDeNaissance);

            if (dateMax < dateChoisi)
            {
                return 0;
            }
            else if (dateMin > dateChoisi)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public bool isEmailValide(string email)
        {
            if (!string.IsNullOrEmpty(email.Trim()))
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
        public int isDateEmbaucheValide(string dateEmbauche)
        {
            DateTime dateMax = DateTime.Now;
            DateTime dateChoisi = DateTime.Parse(dateEmbauche);

            if (dateMax < dateChoisi)
            {
                return 0;
            }
            else if (dateEmbauche == "dimanche 31 décembre 1600")
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        public bool isTauxHoraireValide(string tauxHoraire)
        {
            double p = 0;
            if (!string.IsNullOrEmpty(tauxHoraire.Trim()))
            {
                if (double.TryParse(tauxHoraire, out p))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public int isPhotoValide(string photo)
        {
            Uri photoUri;
            if (string.IsNullOrEmpty(photo.Trim()))
            {
                return 0;
            }
            else
            {
                try
                {
                    photoUri = new Uri(photo);
                    if (photoUri == null)
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;

                    }
                }
                catch
                {
                    return 1;
                }
            }
        }
    }
}
