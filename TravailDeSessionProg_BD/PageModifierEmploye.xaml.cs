using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Reflection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailDeSessionProg_BD
{
    public sealed partial class PageModifierEmploye : Page
    {
        string matricule = "";
        public PageModifierEmploye()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                matricule = e.Parameter.ToString();

                Employe unEmploye = SingletonEmploye.getInstance().getEmploye(matricule);

                inPrenom.Text = unEmploye.Prenom;
                inNom.Text = unEmploye.Nom;
                inEmail.Text = unEmploye.Email;
                inAdresse.Text = unEmploye.Adresse;
                inTauxHoraire.Text = unEmploye.TauxHoraire.ToString();
                inPhoto.Text = unEmploye.Photo;
            }
        }
        private async void btModifier_Click(object sender, RoutedEventArgs e)
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

            if (ValidationEmploye.getInstance().isEmailValide(inEmail.Text) == false)
            {
                ErrEmail.Text = "Veuillez entrer l'adresse email de l'employé";
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
            else if (ValidationEmploye.getInstance().isPhotoValide(inPhoto.Text) == 1)
            {
                ErrPhoto.Text = "Veuillez entrer une photo de format valide";
                valide = false;
            }


            if (valide == true)
            {
                Employe unEmploye = new Employe
                {
                    Matricule = "",
                    Prenom = inPrenom.Text,
                    Nom = inNom.Text,
                    DateDeNaissance = "",
                    Email = inEmail.Text,
                    Adresse = inAdresse.Text,
                    DateEmbauche = "",
                    TauxHoraire = double.Parse(inTauxHoraire.Text),
                    Photo = inPhoto.Text
                };

                /*int err = SingletonEmploye.getInstance().modifierEmploye(matricule,unEmploye);

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
                }*/
            }
        }

        private void resetErreurs()
        {
            ErrPrenom.Text = string.Empty;
            ErrNom.Text = string.Empty;
            ErrEmail.Text = string.Empty;
            ErrAdresse.Text = string.Empty;
            ErrTauxHoraire.Text = string.Empty;
            ErrPhoto.Text = string.Empty;
        }
    }
}
