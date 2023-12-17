using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                MySqlCommand commande = new MySqlCommand("get_liste_employe");
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
                        DateDeNaissance = r["dateDeNaissance"].ToString().Substring(0, 10),
                        Email = r["email"].ToString(),
                        Adresse = r["adresse"].ToString(),
                        DateEmbauche = r["dateEmbauche"].ToString().Substring(0, 10),
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

        public ObservableCollection<Employe> GetListeEmployeSansTravail()
        {
            liste.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand("get_liste_employe_sans_travail");
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
                        DateDeNaissance = r["dateDeNaissance"].ToString().Substring(0, 10),
                        Email = r["email"].ToString(),
                        Adresse = r["adresse"].ToString(),
                        DateEmbauche = r["dateEmbauche"].ToString().Substring(0, 10),
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

        public Employe getEmploye(string matricule)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("get_un_employe");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("in_matricule", matricule);
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                Employe unEmploye = new Employe();
                while (r.Read())
                {
                    unEmploye = new Employe
                    {
                        Matricule = r["matricule"].ToString(),
                        Nom = r["nom"].ToString(),
                        Prenom = r["prenom"].ToString(),
                        DateDeNaissance = r["dateDeNaissance"].ToString().Substring(0, 10),
                        Email = r["email"].ToString(),
                        Adresse = r["adresse"].ToString(),
                        DateEmbauche = r["dateEmbauche"].ToString().Substring(0, 10),
                        TauxHoraire = double.Parse(r["tauxHoraire"].ToString()),
                        Photo = r["photo"].ToString(),
                        Statut = r["statut"].ToString()
                    };
                }
                r.Close();
                con.Close();
                return unEmploye;

            }
            catch (MySqlException ex)
            {
                con.Close();
                return null;
            }
        }

        public int ajouterEmploye(Employe unEmploye)
        {
            int err;
            try
            {

                MySqlCommand commande = new MySqlCommand("add_employe");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("in_nom", unEmploye.Nom);
                commande.Parameters.AddWithValue("in_prenom", unEmploye.Prenom);
                commande.Parameters.AddWithValue("in_dateDeNaissance", unEmploye.DateDeNaissance);
                commande.Parameters.AddWithValue("in_email", unEmploye.Email);
                commande.Parameters.AddWithValue("in_adresse", unEmploye.Adresse);
                commande.Parameters.AddWithValue("in_dateEmbauche", unEmploye.DateEmbauche);
                commande.Parameters.AddWithValue("in_tauxHoraire", unEmploye.TauxHoraire);
                commande.Parameters.AddWithValue("in_photo", unEmploye.Photo);

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
                if (ex.Number == 1644)
                {
                    if (ex.Message.Contains("L'employer ne peut pas être trop payer (max 75$/h)"))
                    {
                        err = 1;
                    }
                    else if (ex.Message.Contains("L'employer ne peut pas être sous-payer (min 15$/h)"))
                    {
                        err = 2;
                    }
                    else if (ex.Message.Contains("L'adresse email n'est pas valide"))
                    {
                        err = 3;
                    }
                    else
                    {
                        err = 4;
                    }
                }
                else
                {
                    err = 4;
                }
                return err;
            }
            catch (Exception ex)
            {
                con.Close();
                err = 4;
                return err;
            }
        }

        public int modifierEmploye(Employe unEmploye)
        {
            int err = 0;
            try
            {
                MySqlCommand commande = new MySqlCommand("mod_employe");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("in_matricule", unEmploye.Matricule);
                commande.Parameters.AddWithValue("in_nom", unEmploye.Nom);
                commande.Parameters.AddWithValue("in_prenom", unEmploye.Prenom);
                commande.Parameters.AddWithValue("in_email", unEmploye.Email);
                commande.Parameters.AddWithValue("in_adresse", unEmploye.Adresse);
                commande.Parameters.AddWithValue("in_tauxHoraire", unEmploye.TauxHoraire);
                commande.Parameters.AddWithValue("in_photo", unEmploye.Photo);
                commande.Parameters.AddWithValue("in_statut", unEmploye.Statut);

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
                if (ex.Number == 1644)
                {
                    if (ex.Message.Contains("L'employer ne peut pas être trop payer (max 75$/h)"))
                    {
                        err = 1;
                    }
                    else if (ex.Message.Contains("L'employer ne peut pas être sous-payer (min 15$/h)"))
                    {
                        err = 2;
                    }
                    else if (ex.Message.Contains("L'adresse email n'est pas valide"))
                    {
                        err = 3;
                    }
                    else
                    {
                        err = 4;
                    }
                }
                else
                {
                    err = 4;
                }
                return err;
            }
            catch (Exception ex)
            {
                con.Close();
                err = 4;
                return err;
            }

        }
        public int checkAncienneteEmploye(string matricule)
        {
            int result;
            try
            {

                MySqlCommand commande = new MySqlCommand("check_anciennete_employe");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("in_employe", matricule);

                con.Open();
                commande.Prepare();

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
