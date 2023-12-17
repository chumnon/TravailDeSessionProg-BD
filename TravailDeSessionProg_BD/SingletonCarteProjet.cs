using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    internal class SingletonCarteProjet
    {
        ObservableCollection<CarteProjet> liste;
        static SingletonCarteProjet instance = null;

        MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=2272411-lamothe-arthur;Uid=2272411;Pwd=2272411;");

        public SingletonCarteProjet()
        {
            liste = new ObservableCollection<CarteProjet>();
        }

        public static SingletonCarteProjet getInstance()
        {
            if (instance == null)
                instance = new SingletonCarteProjet();

            return instance;
        }

        public ObservableCollection<CarteProjet> GetListeCarteProjet()
        {

            liste.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand("get_liste_carte_projet");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    CarteProjet uneCarteProjet = new CarteProjet
                    {
                        Numero = r["numero"].ToString(),
                        Titre = r["titre"].ToString(),
                        NomClient = r["nom"].ToString(),
                        DateDebut = r["dateDebut"].ToString().Substring(0, 10),
                        Budget = r["budget"].ToString()
                    };
                    liste.Add(uneCarteProjet);

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
    }
}
