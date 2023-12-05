using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    internal class SingletonAdmin
    {
        ObservableCollection<Admin> liste;
        static SingletonAdmin instance = null;

        MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=2272411-lamothe-arthur;Uid=2272411;Pwd=2272411;");

        public SingletonAdmin()
        {
            liste = new ObservableCollection<Admin>();
        }

        public static SingletonAdmin getInstance()
        {
            if (instance == null)
                instance = new SingletonAdmin();

            return instance;
        }

        public Boolean ajouterAdmin(Admin unAdmin)
        {
            Boolean err;
            try
            {

                MySqlCommand commande = new MySqlCommand("add_admin");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("in_nom", unAdmin.Nom);
                commande.Parameters.AddWithValue("in_mdp", unAdmin.Mdp);
                con.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();

                con.Close();
                err = false;
                return err;
            }
            catch (Exception ex)
            {
                con.Close();
                err = true;
                return err;
            }
        }

        /*public ObservableCollection<Admin> GetListeClient()
        {
            liste.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand("get_admin");
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
        */
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
