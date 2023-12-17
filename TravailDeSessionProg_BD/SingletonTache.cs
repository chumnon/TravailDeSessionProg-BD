using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    internal class SingletonTache
    {
        ObservableCollection<Tache> liste;
        static SingletonTache instance = null;

        MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=2272411-lamothe-arthur;Uid=2272411;Pwd=2272411;");

        public SingletonTache()
        {
            liste = new ObservableCollection<Tache>();
        }

        public static SingletonTache getInstance()
        {
            if (instance == null)
                instance = new SingletonTache();

            return instance;
        }

        public ObservableCollection<Tache> GetListeTacheDunProjet(string numeroProjet)
        {
            liste.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand("get_liste_carte_tache_dun_projet");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("in_numero_projet", numeroProjet);
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    Tache uneTache = new Tache
                    {
                        NumTache = r["numTache"].ToString(),
                        LEmploye = r["employe"].ToString(),
                        TauxHoraireEmploye = double.Parse(r["tauxHoraire"].ToString()),
                        LeProjet = r["projet"].ToString(),
                        Salaire = double.Parse(r["salaire"].ToString()),
                        NbrHeure = int.Parse(r["nbrHeure"].ToString())
                    };
                    liste.Add(uneTache);

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

        public bool ajouterTache(Tache uneTache)
        {
            bool err;
            try
            {
                MySqlCommand commande = new MySqlCommand("add_tache");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("in_employe", uneTache.LEmploye);
                commande.Parameters.AddWithValue("in_projet", uneTache.LeProjet);
                commande.Parameters.AddWithValue("in_nbrHeure", uneTache.NbrHeure);

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
    }
}
