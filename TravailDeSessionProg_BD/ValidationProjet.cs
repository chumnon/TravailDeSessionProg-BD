using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    class ValidationProjet
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
            DateTime dateMin = DateTime.Now.Date;
            DateTime dateChoisi = DateTime.Parse(dateDebut).Date;

            if (dateMin <= dateChoisi)
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

        public bool isNbrEmployeValide(int nbrEmployeIndex)
        {
            if (nbrEmployeIndex >= 0)
                return true;
            else
                return false;
        }

        public bool isClientValide(int clientIndex)
        {
            if (clientIndex >= 0)
                return true;
            else
                return false;
        }
    }
}
