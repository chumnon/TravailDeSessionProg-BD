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
    public sealed partial class PageAjoutClient : Page
    {
        public PageAjoutClient()
        {
            this.InitializeComponent();
        }

        private async void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            resetErreurs();
            bool valide = true;

            if (ValidationClient.getInstance().isNomValide(inNom.Text) == false)
            {
                ErrNom.Text = "Veuillez entrer le nom du client";
                valide = false;
            }

            if (ValidationClient.getInstance().isAdresseValide(inAdresse.Text) == false)
            {
                ErrAdresse.Text = "Veuillez entrer une adresse";
                valide = false;
            }

            if (ValidationClient.getInstance().isNumTelValide(inNumTel.Text) == false)
            {
                ErrNumTel.Text = "Veuillez entrer un numéro de téléphone";
                valide = false;
            }

            if (ValidationClient.getInstance().isEmailValide(inEmail.Text) == false)
            {
                ErrEmail.Text = "Veuillez entrer une adresse email";
                valide = false;
            }

            if (valide == true)
            {
                Client unClient = new Client
                {
                    Id = 0,
                    Nom = inNom.Text,
                    Adresse = inAdresse.Text,
                    NumTel = inNumTel.Text,
                    Email = inEmail.Text
                };

                int err = SingletonClient.getInstance().ajouterClient(unClient);

                if (err == 0)
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = mainGrid.XamlRoot;
                    dialog.Title = "Ajout du client";
                    dialog.PrimaryButtonText = "OK";
                    dialog.DefaultButton = ContentDialogButton.Primary;
                    dialog.Content = "Le client a été ajouté avec succès";

                    ContentDialogResult resultat = await dialog.ShowAsync();

                    this.Frame.Navigate(typeof(PageAffichageClient));
                }
                else if (err == 1)
                {
                    ErrEmail.Text = "Le format de l'email est incorect";
                }
                else if (err == 2)
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = mainGrid.XamlRoot;
                    dialog.Title = "Ajout du client";
                    dialog.PrimaryButtonText = "OK";
                    dialog.DefaultButton = ContentDialogButton.Primary;
                    dialog.Content = "Erreur, le client n'a pas pu être ajouter";

                    ContentDialogResult resultat = await dialog.ShowAsync();
                }
            }
        }

        private void resetErreurs()
        {
            ErrNom.Text = string.Empty;
            ErrAdresse.Text = string.Empty;
            ErrNumTel.Text = string.Empty;
            ErrEmail.Text = string.Empty;
        }
    }
}
