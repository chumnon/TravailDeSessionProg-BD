using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    internal class SingletonClient
    {
        ObservableCollection<Client> liste;
        static SingletonClient instance = null;

        MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=2272411-arthur-lamothe;Uid=2272411;Pwd=2272411;");

        public SingletonClient()
        {
            liste = new ObservableCollection<Client>();
        }

        public static SingletonClient getInstance()
        {
            if (instance == null)
                instance = new SingletonClient();

            return instance;
        }

        /*public ObservableCollection<Employe> GetListeMateriel()
        {
        }*/

        public Client getClient(int position)
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
