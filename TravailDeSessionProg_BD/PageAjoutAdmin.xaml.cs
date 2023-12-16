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
    public sealed partial class PageAjoutAdmin : Page
    {
        public PageAjoutAdmin()
        {
            this.InitializeComponent();
        }

        private async void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            resetErreurs();
            bool valide = true;

            if (ValidationAdmin.getInstance().isNomValide(inNom.Text) == false)
            {
                ErrNom.Text = "Veuillez entrer le nom de l'admin";
                valide = false;
            }

            if (ValidationAdmin.getInstance().isMdpValide(inMdp.Text) == false)
            {
                ErrMdp.Text = "Veuillez entrer un mot de passe";
                valide = false;
            }

            if (ValidationAdmin.getInstance().isMdpConfValide(inMdp.Text, inMdpConf.Text) == 1)
            {
                ErrMdpConf.Text = "Les mots de passe ne sont pas identique";
                valide = false;
            } else if (ValidationAdmin.getInstance().isMdpConfValide(inMdp.Text, inMdpConf.Text) == 2)
            {
                ErrMdpConf.Text = "Veuillez remplire la confirmation du mot de passe";
                valide = false;
            }

                if (valide == true)
            {

                Admin unAdmin = new Admin
                {
                    Id = 1,
                    Nom = inNom.Text,
                    Mdp = inMdp.Text
                };

                Boolean err = SingletonAdmin.getInstance().ajouterAdmin(unAdmin);

                    if (err == false)
                    {
                        ContentDialog dialog = new ContentDialog();
                        dialog.XamlRoot = mainGrid.XamlRoot;
                        dialog.Title = "Ajout de l'admin";
                        dialog.PrimaryButtonText = "OK";
                        dialog.DefaultButton = ContentDialogButton.Primary;
                        dialog.Content = "L'admin a été ajouté avec succès";

                        ContentDialogResult resultat = await dialog.ShowAsync();

                        this.Frame.Navigate(typeof(PageAffichageProjet));
                    }
                    else if (err == true)
                    {
                        ContentDialog dialog = new ContentDialog();
                        dialog.XamlRoot = mainGrid.XamlRoot;
                        dialog.Title = "Ajout de l'admin";
                        dialog.PrimaryButtonText = "OK";
                        dialog.DefaultButton = ContentDialogButton.Primary;
                        dialog.Content = "Erreur, l'admin n'a pas été ajouter";

                        ContentDialogResult resultat = await dialog.ShowAsync();
                    }
            }
        }
        private void resetErreurs()
        {
            ErrNom.Text = string.Empty;
            ErrMdp.Text = string.Empty;
            ErrMdpConf.Text = string.Empty;
        }
    }
}
