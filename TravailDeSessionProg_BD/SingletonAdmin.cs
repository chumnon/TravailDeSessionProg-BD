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

        public string connexion(string nom, string mdp)
        {
            string message;
            try
            {

                MySqlCommand commande = new MySqlCommand("use_connexion");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("in_nom", nom);
                commande.Parameters.AddWithValue("in_mdp", mdp);
                con.Open();
                commande.Prepare();

                commande.CommandType = System.Data.CommandType.Text;

                message = commande.ExecuteScalar().ToString();

                con.Close();
                return message;
            }
            catch (Exception ex)
            {
                con.Close();
                message = "Il y a une erreur lors de la connexion à la base de données";
                return message;
            }
        }

        public int checkAdmin()
        {
            int result;
            try
            {

                MySqlCommand commande = new MySqlCommand("use_checkAdmin");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();

                result = Convert.ToInt32(commande.ExecuteScalar());

                con.Close();
                return result;
            }
            catch (Exception ex)
            {
                con.Close();
                result = 2;
                return result;
            }
        }
    }
}
