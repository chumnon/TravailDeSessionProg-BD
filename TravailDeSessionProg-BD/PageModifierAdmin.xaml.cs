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
    public sealed partial class PageModifierAdmin : Page
    {
        public PageModifierAdmin()
        {
            this.InitializeComponent();
        }

        private async void btModifier_Click(object sender, RoutedEventArgs e)
        {
            resetErreurs();
            bool valide = true;

            if (ValidationAdmin.getInstance().isNomValide(inOldNom.Text) == false)
            {
                ErrNom.Text = "Veuillez entrer l'ancien nom de l'admin";
                valide = false;
            }

            if (ValidationAdmin.getInstance().isNomValide(inNewNom.Text) == false)
            {
                ErrNom.Text = "Veuillez entrer le nouveau nom de l'admin";
                valide = false;
            }


            if (ValidationAdmin.getInstance().isMdpValide(inOldMdp.Text) == false)
            {
                ErrOldMdp.Text = "Veuillez entrer un mot de passe";
                valide = false;
            }

            if (ValidationAdmin.getInstance().isMdpValide(inNewMdp.Text) == false)
            {
                ErrOldMdp.Text = "Veuillez entrer un mot de passe";
                valide = false;
            }

            if (ValidationAdmin.getInstance().isMdpConfValide(inNewMdp.Text, inMdpConf.Text) == false)
            {
                ErrMdpConf.Text = "Les mots de passe ne sont pas identique";
                valide = false;
            }

            if (valide == true)
            {
                string message = SingletonAdmin.getInstance().connexion(inOldNom.Text, inOldMdp.Text);

                if (message == "Connexion réussi")
                {
                    Admin unAdmin = new Admi
                    {
                        Id = 0,
                        Nom = inNewNom.Text,
                        Mdp = inNewMdp.Text
                    };

                    
                    Boolean err = SingletonAdmin.getInstance().modifierAdmin(unAdmin);

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
                else if (message == "Mots de passe invalide")
                {
                    ErrOldMdp.Text = "Mauvais ancien mots de passe";
                }
                else
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = mainGrid.XamlRoot;
                    dialog.Title = "Modification admin";
                    dialog.PrimaryButtonText = "OK";
                    dialog.DefaultButton = ContentDialogButton.Primary;
                    dialog.Content = "Erreur: " + message;

                    ContentDialogResult resultat = await dialog.ShowAsync();
                }
            }
        }

        private void resetErreurs()
        {
            ErrNom.Text = string.Empty;
            ErrOldMdp.Text = string.Empty;
            ErrNewMdp.Text = string.Empty;
            ErrMdpConf.Text = string.Empty;
        }
    }
}
