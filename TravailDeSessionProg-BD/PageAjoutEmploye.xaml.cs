using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailDeSessionProg_BD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAjoutEmploye : Page
    {
        public PageAjoutEmploye()
        {
            this.InitializeComponent();
            inDateEmbauche.MaxYear = DateTimeOffset.Now;
            inDateDeNaissance.MaxYear = DateTimeOffset.Now.AddYears(-18);
            inDateDeNaissance.MinYear = DateTimeOffset.Now.AddYears(-65);
        }

        private async void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            resetErreurs();
            bool valide = true;

            if (ValidationEmploye.getInstance().isPrenomValide(inPrenom.Text) == false)
            {
                ErrPrenom.Text = "Veuillez entrer le prenom de l'employé";
                valide = false;
            }

            if (ValidationEmploye.getInstance().isNomValide(inNom.Text) == false)
            {
                ErrNom.Text = "Veuillez entrer le nom de l'employé";
                valide = false;
            }

            if (ValidationEmploye.getInstance().isDateDeNaissanceValide(inDateDeNaissance.Text) == 0)  
            {
                ErrDateDeNaissance.Text = "L'employé est trop jeune pour travailler ici";
                valide = false;
            }
            else if(ValidationEmploye.getInstance().isDateDeNaissanceValide(inDateDeNaissance.Text) == 1)
            {
                ErrDateDeNaissance.Text = "L'employé est trop vieux pour travailler ici";
                valide = false;
            }

            if (ValidationEmploye.getInstance().isEmailValide(inEmail.Text) == false)
            {
                ErrEmail.Text = "Veuillez entrer l'adresse email de l'employé";
                valide = false;
            }

            if (ValidationEmploye.getInstance().isDateEmbaucheValide(inDateEmbauche.Text) == 0)
            {
                ErrDateEmbauche.Text = "La date d'embauche ne peut pas être dans le future";
                valide = false;
            }
            else if (ValidationEmploye.getInstance().isDateDeNaissanceValide(inDateEmbauche.Text) == 1)
            {
                ErrDateEmbauche.Text = "Veuillez choisir la date d'embauche";
                valide = false;
            }

            if (ValidationEmploye.getInstance().isAdresseValide(inAdresse.Text) == false)
            {
                ErrAdresse.Text = "Veuillez entrer l'adresse de l'employé";
                valide = false;
            }

            if (ValidationEmploye.getInstance().isTauxHoraireValide(inTauxHoraire.Text) == false)
            {
                ErrTauxHoraire.Text = "Veuillez entrer le taux horaire de l'employé";
                valide = false;
            }

            if (ValidationEmploye.getInstance().isPhotoValide(inPhoto.Text) == 0)
            {
                ErrPhoto.Text = "Veuillez entrer une photo de l'employé";
                valide = false;
            }
            else if(ValidationEmploye.getInstance().isPhotoValide(inPhoto.Text) == 1)
            {
                ErrPhoto.Text = "Veuillez entrer une photo de format valide";
                valide = false;
            }


            if (valide == true)
            {
                Employe unEmploye = new Employe
                {
                    Matricule= "",
                    Prenom = inPrenom.Text,
                    Nom = inNom.Text,
                    DateDeNaissance = inDateDeNaissance.Text,
                    Email = inEmail.Text,
                    Adresse = inAdresse.Text,
                    DateEmbauche = inDateEmbauche.Text,
                    TauxHoraire = inTauxHoraire.Text,
                    Photo = inPhoto.Text
                };

                int err = SingletonEmploye.getInstance().ajouterEmploye(unEmploye);

                if (err == 0)
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = mainGrid.XamlRoot;
                    dialog.Title = "Ajout de l'employé";
                    dialog.PrimaryButtonText = "OK";
                    dialog.DefaultButton = ContentDialogButton.Primary;
                    dialog.Content = "L'employé a été ajouté avec succès";

                    ContentDialogResult resultat = await dialog.ShowAsync();

                    this.Frame.Navigate(typeof(PageAffichageEmploye));
                }
                else if (err == 1)
                {
                    ErrTauxHoraire.Text = "L'employer ne peut pas être trop payer (max 75$/h)";
                }
                else if (err == 2)
                {
                    ErrTauxHoraire.Text = "L'employer ne peut pas être sous-payer (min 15$/h)";
                }
                else if (err == 3)
                {
                    ErrEmail.Text = "L'adresse email n'est pas valide";
                }
                else if (err == 4)
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = mainGrid.XamlRoot;
                    dialog.Title = "Ajout de l'employé";
                    dialog.PrimaryButtonText = "OK";
                    dialog.DefaultButton = ContentDialogButton.Primary;
                    dialog.Content = "Erreur, l'employé n'a pas pu être ajouter";

                    ContentDialogResult resultat = await dialog.ShowAsync();
                }
            }
        }

        private void resetErreurs()
        {
            ErrPrenom.Text = string.Empty;
            ErrNom.Text = string.Empty;
            ErrDateDeNaissance.Text = string.Empty;
            ErrEmail.Text = string.Empty;
            ErrDateEmbauche.Text = string.Empty;
            ErrAdresse.Text = string.Empty;
            ErrTauxHoraire.Text = string.Empty;
            ErrStatut.Text = string.Empty;
            ErrPhoto.Text = string.Empty;
        }
    }
}
