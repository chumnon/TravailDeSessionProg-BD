using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    internal class ValidationProjet
    {
        static ValidationProjet instance;
        public ValidationProjet() { }

        public static ValidationProjet getInstance()
        {
            if (instance == null)
                instance = new ValidationProjet();

            return instance;
        }

        public bool isTitreValide(string titre)
        {
            if (!string.IsNullOrEmpty(titre.Trim()))
                return true;
            else
                return false;
        }

        public bool isDateDebutValide(string dateDebut)
        {
            DateTime dateMin = DateTime.Now;
            DateTime dateChoisi = DateTime.Parse(dateDebut);

            if (dateMin < dateChoisi)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isDescriptionValide(string description)
        {
            if (!string.IsNullOrEmpty(description.Trim()))
                return true;
            else
                return false;
        }

        public bool isBudgetValide(string budget)
        {
            if (!string.IsNullOrEmpty(budget.Trim()))
                return true;
            else
                return false;
        }

        public bool isNbrEmployeValide(string nbrEmploye)
        {
            int p = 0;
            if (!string.IsNullOrEmpty(nbrEmploye.Trim()))
            {
                if (int.TryParse(nbrEmploye, out p))
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

        public bool isClientValide(string client)
        {
            int p = 0;
            if (!string.IsNullOrEmpty(client.Trim()))
            {
                if (int.TryParse(client, out p))
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
    }
}
