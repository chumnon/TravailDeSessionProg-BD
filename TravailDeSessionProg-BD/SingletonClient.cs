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

        public int ajouterClient(Client unClient)
        {
            int err;
            try
            {

                MySqlCommand commande = new MySqlCommand("add_client");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("in_nom", unClient.Nom);
                commande.Parameters.AddWithValue("in_adresse", unClient.Adresse);
                commande.Parameters.AddWithValue("in_numTel", unClient.NumTel);
                commande.Parameters.AddWithValue("in_email", unClient.Email);
                con.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();

                con.Close();
                err = 0;
                return err;
            }
            catch (MySqlException ex)
            {
                con.Close();
                if (ex.Number == 45000)
                {
                    err = 1;
                } else
                {
                    err = 2;
                }
                return err;
            }
            catch (Exception ex)
            {
                con.Close();
                err = 2;
                return err;
            }
        }

        /*public void modifierEmploye(int position, Employe unEmploye)
        {
        }*/

        /*public void modifierStatutEmploye(int position, Employe unEmploye)
        {
        }*/
    }
}
