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

        MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=2272411-lamothe-arthur;Uid=2272411;Pwd=2272411;");

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

        public ObservableCollection<Client> GetListeClient()
        {
            liste.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand("get_client");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    Client unClient = new Client
                    {
                        Id = int.Parse(r["id"].ToString()),
                        Nom = r["nom"].ToString(),
                        Adresse = r["adresse"].ToString(),
                        NumTel = r["numTel"].ToString(),
                        Email = r["email"].ToString()
                    };
                    liste.Add(unClient);

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
