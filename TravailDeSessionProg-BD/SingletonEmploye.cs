using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    internal class SingletonEmploye
    {
        ObservableCollection<Employe> liste;
        static SingletonEmploye instance = null;

        MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=2272411-lamothe-arthur;Uid=2272411;Pwd=2272411;");

        public SingletonEmploye()
        {
            liste = new ObservableCollection<Employe>();
        }

        public static SingletonEmploye getInstance()
        {
            if (instance == null)
                instance = new SingletonEmploye();

            return instance;
        }

        public ObservableCollection<Employe> GetListeEmploye()
        {
            liste.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand("get_employe");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    Employe unEmploye = new Employe
                    {
                        Matricule = r["matricule"].ToString(),
                        Nom = r["nom"].ToString(),
                        Prenom = r["prenom"].ToString(),
                        DateDeNaissance = r["dateDeNaissance"].ToString(),
                        Email = r["email"].ToString(),
                        Adresse = r["adresse"].ToString(),
                        DateEmbauche = r["dateEmbauche"].ToString(),
                        TauxHoraire = double.Parse(r["tauxHoraire"].ToString()),
                        Photo = r["photo"].ToString(),
                        Statut = r["statut"].ToString()
                    };
                    liste.Add(unEmploye);

                }

                r.Close();
                con.Close();
                return liste;

            }
            catch (MySqlException ex)
            {
                con.Close();
                return null;
            }
        }

        public Employe getEmploye(int position)
        {
            return liste[position];
        }

        /*public void ajouterEmploye(Employe unEmploye)
        {
            Boolean err;
            try
            {
            }
            catch (Exception ex)
            {
              
            }
        }*/

        /*public void modifierEmploye(int position, Employe unEmploye)
        {
        }*/

        /*public void modifierStatutEmploye(int position, Employe unEmploye)
        {
        }*/

        /*public void supprimerEmploye(int position)
        {
            try
            {
                int Code = liste[position];
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "DELETE from produits where Code = '" + Code + "'";

                con.Open();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
        }*/
    }
}
