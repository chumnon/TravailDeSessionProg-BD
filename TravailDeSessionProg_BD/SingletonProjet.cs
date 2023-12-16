using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TravailDeSessionProg_BD
{
    internal class SingletonProjet
    {
        ObservableCollection<Projet> liste;
        static SingletonProjet instance = null;

        MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=2272411-lamothe-arthur;Uid=2272411;Pwd=2272411;");

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

        public ObservableCollection<Projet> GetListeProjet()
        {
            liste.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand("get_liste_projet");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    Projet unProjet = new Projet
                    {
                        Numero = r["numero"].ToString(),
                        Titre = r["titre"].ToString(),
                        DateDebut = r["dateDebut"].ToString().Substring(0,10),
                        Description = r["description"].ToString(),
                        Budget = r["budget"].ToString(),
                        NbrEmploye = int.Parse(r["nbrEmploye"].ToString()),
                        SalaireTotal = double.Parse(r["salaireTotal"].ToString()),
                        SonClient = int.Parse(r["client"].ToString()),
                        Statut = r["statut"].ToString()
                    };
                    liste.Add(unProjet);

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

        public List<Projet> GetListeToSave()
        {
            List<Projet> uneListe = new List<Projet>();
            try
            {
                MySqlCommand commande = new MySqlCommand("get_liste_projet");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    Projet unProjet = new Projet
                    {
                        Numero = r["numero"].ToString(),
                        Titre = r["titre"].ToString(),
                        DateDebut = r["dateDebut"].ToString(),
                        Description = r["description"].ToString(),
                        Budget = r["budget"].ToString(),
                        NbrEmploye = int.Parse(r["nbrEmploye"].ToString()),
                        SalaireTotal = double.Parse(r["salaireTotal"].ToString()),
                        SonClient = int.Parse(r["client"].ToString()),
                        Statut = r["statut"].ToString()
                    };
                    uneListe.Add(unProjet);
                }
                r.Close();
                con.Close();
                return uneListe;

            }
            catch (MySqlException ex)
            {
                con.Close();
                return null;
            }
        }

        public Projet getProjet(string numero)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("get_un_projet");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("in_numero", numero);
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                Projet unProjet = new Projet();
                while (r.Read())
                {
                    unProjet = new Projet
                    {
                        Numero = r["numero"].ToString(),
                        Titre = r["titre"].ToString(),
                        DateDebut = r["dateDebut"].ToString().Substring(0,10),
                        Description = r["description"].ToString(),
                        Budget = r["budget"].ToString(),
                        NbrEmploye = int.Parse(r["nbrEmploye"].ToString()),
                        SalaireTotal = double.Parse(r["salaireTotal"].ToString()),
                        SonClient = int.Parse(r["client"].ToString()),
                        Statut = r["statut"].ToString()
                    };
                }
                r.Close();
                con.Close();
                return unProjet;

            }
            catch (MySqlException ex)
            {
                con.Close();
                return null;
            }
        }

        public bool ajouterProjet(Projet unProjet)
        {
            bool err;
            try
            {

                MySqlCommand commande = new MySqlCommand("add_projet");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("in_titre", unProjet.Titre);
                commande.Parameters.AddWithValue("in_dateDebut", unProjet.DateDebut);
                commande.Parameters.AddWithValue("in_description", unProjet.Description);
                commande.Parameters.AddWithValue("in_budget", unProjet.Budget);
                commande.Parameters.AddWithValue("in_nbrEmploye", unProjet.NbrEmploye);
                commande.Parameters.AddWithValue("in_client", unProjet.SonClient);

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

        /*public bool CheckNbrEmployeProjet(string numero)
        {
            bool result;
            try
            {
                MySqlCommand commande = new MySqlCommand("check_nbrEmploye_projet");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("in_numero", numero);

                con.Open();

                MySqlDataReader r = commande.ExecuteReader();
                result = false;
                while (r.Read())
                {
                    result = (bool)r["check_nbrEmploye_projet(\"203-93-2024\")"];
                }
                return result;
            }
            catch (Exception ex)
            {
                result = true;
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        */
        /*public void modifierProjet(int position, Projet unProjet)
        {
        }*/
    }
}
