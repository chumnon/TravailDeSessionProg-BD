using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    internal class SingletonProjet
    {
        ObservableCollection<Projet> liste;
        static SingletonProjet instance = null;

        MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=2272411-arthur-lamothe;Uid=2272411;Pwd=2272411;");

        public SingletonProjet()
        {
            liste = new ObservableCollection<Projet>();
        }

        public static SingletonProjet getInstance()
        {
            if (instance == null)
                instance = new SingletonProjet();

            return instance;
        }

        /*public ObservableCollection<Projet> GetListeProjet()
        {
        }*/

        public Projet getProjet(int position)
        {
            return liste[position];
        }

        /*public void ajouterProjet(Projet unProjet)
        {
            Boolean err;
            try
            {
            }
            catch (Exception ex)
            {
              
            }
        }*/

        /*public void modifierProjet(int position, Projet unProjet)
        {
        }*/

        /*public void supprimerProjet(int position)
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
