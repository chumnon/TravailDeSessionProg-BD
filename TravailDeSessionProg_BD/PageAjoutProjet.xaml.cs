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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailDeSessionProg_BD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAjoutProjet : Page
    {
        public PageAjoutProjet()
        {
            this.InitializeComponent();
            inDateDebut.MinYear = DateTimeOffset.Now;
            inClient.ItemsSource = SingletonClient.getInstance().GetListeClient();
        }

        private async void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            resetErreurs();
            bool valide = true;

            if (ValidationProjet.getInstance().isTitreValide(inTitre.Text) == false)
            {
                ErrTitre.Text = "Veuillez entrer le titre du projet";
                valide = false;
            }

            if (ValidationProjet.getInstance().isDateDebutValide(inDateDebut.Date.ToString()) == false)
            {
                ErrDateDebut.Text = "Veuillez choisir une date qui commence au plus tôt aujourd'hui";
                valide = false;
            }

            if (ValidationProjet.getInstance().isDescriptionValide(inDescription.Text) == false)
            {
                ErrDescription.Text = "Veuillez ajouter une description au projet";
                valide = false;
            }

            if (ValidationProjet.getInstance().isBudgetValide(inBudget.Text) == false)
            {
                ErrBudget.Text = "Veuillez entrer un budget au projet";
                valide = false;
            }

            if (ValidationProjet.getInstance().isNbrEmployeValide(inNbrEmploye.SelectedIndex) == false)
            {
                ErrNbrEmploye.Text = "Veuillez choisir un nombre d'employe";
                valide = false;
            }

            if (ValidationProjet.getInstance().isClientValide(inClient.SelectedIndex) == false)
            {
                ErrClient.Text = "Veuillez choisire le client qui a proposé le projet";
                valide = false;
            }


            if (valide == true)
            {
                Client selectedClient = (Client)inClient.SelectedItem;
                Projet unProjet = new Projet
                {
                    Numero = "",
                    Titre = inTitre.Text,
                    DateDebut = inDateDebut.Date.ToString().Substring(0,10),
                    Description = inDescription.Text,
                    Budget = inBudget.Text,
                    NbrEmploye = int.Parse(inNbrEmploye.SelectedValue.ToString()),
                    SonClient = selectedClient.Id,
                    Statut = "en cours"
                };

                bool err = SingletonProjet.getInstance().ajouterProjet(unProjet);

                if (err == false)
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = mainGrid.XamlRoot;
                    dialog.Title = "Ajout du projet";
                    dialog.PrimaryButtonText = "OK";
                    dialog.DefaultButton = ContentDialogButton.Primary;
                    dialog.Content = "Le projet a été ajouté avec succès";

                    ContentDialogResult resultat = await dialog.ShowAsync();

                    this.Frame.Navigate(typeof(PageAffichageProjet));
                }
                else if (err == true)
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = mainGrid.XamlRoot;
                    dialog.Title = "Ajout du projet";
                    dialog.PrimaryButtonText = "OK";
                    dialog.DefaultButton = ContentDialogButton.Primary;
                    dialog.Content = "Erreur, le projet n'a pas pu être ajouter";

                    ContentDialogResult resultat = await dialog.ShowAsync();
                }
            }
        }

        private void resetErreurs()
        {
            ErrTitre.Text = string.Empty;
            ErrDateDebut.Text = string.Empty;
            ErrDescription.Text = string.Empty;
            ErrBudget.Text = string.Empty;
            ErrNbrEmploye.Text = string.Empty;
            ErrClient.Text = string.Empty;
        }
    }
}
