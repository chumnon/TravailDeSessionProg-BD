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
    
    public sealed partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public MainWindow()
        {
            this.InitializeComponent();

            int result = SingletonAdmin.getInstance().checkAdmin();
            if (result == 0)
            {
                mainFrame.Navigate(typeof(PageAjoutAdmin));
            }
            else if(result == 1)
            {
                mainFrame.Navigate(typeof(PageAffichageProjet));
            }
            else if (result == 2)
            {
                erreur.Text = "Erreur lors de la connexion, veuillez redémarer le programme pour pouvoir accéder en temps qu'admin";
            }

            Instance = this;

            RafraichirMainWindow();
        }

        public void RafraichirMainWindow()
        {
            bool isAdmin = modeAdmin.Admin;
            if (isAdmin == true)
            {
                info.Text = "mode admin";
                abButtonAjoutAdmin.Visibility = Visibility.Visible;
                abButtonAjoutClient.Visibility = Visibility.Visible;
                abButtonAjoutEmploye.Visibility = Visibility.Visible;
                abButtonAjoutProjet.Visibility = Visibility.Visible;
                abButtonSaveProjet.Visibility = Visibility.Visible;
                abButtonCon.Visibility = Visibility.Collapsed;
                abButtonDecon.Visibility = Visibility.Visible;
            }
            else
            {
                info.Text = "";
                abButtonAjoutAdmin.Visibility = Visibility.Collapsed;
                abButtonAjoutClient.Visibility = Visibility.Collapsed;
                abButtonAjoutEmploye.Visibility = Visibility.Collapsed;
                abButtonAjoutProjet.Visibility = Visibility.Collapsed;
                abButtonSaveProjet.Visibility = Visibility.Collapsed;
                abButtonCon.Visibility = Visibility.Visible;
                abButtonDecon.Visibility = Visibility.Collapsed;
            }
        }

        private void abButtonCon_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(PageConnexion));
        }

        private async void abButtonDecon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                modeAdmin.Admin = false;

                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = GridMaitre.XamlRoot;
                dialog.Title = "Déconnexion";
                dialog.PrimaryButtonText = "OK";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = "Vous vous êtes bien déconnecté ";

                ContentDialogResult resultat = await dialog.ShowAsync();

                mainFrame.Navigate(typeof(PageAffichageProjet));
            }
            catch {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = GridMaitre.XamlRoot;
                dialog.Title = "Déconnexion";
                dialog.PrimaryButtonText = "OK";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = "Erreur lors de la déconnexion";

                ContentDialogResult resultat = await dialog.ShowAsync();

                mainFrame.Navigate(typeof(PageAffichageProjet));
            }
            RafraichirMainWindow();
        }

        private void abButtonAjoutAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (modeAdmin.Admin == true)
            {
                mainFrame.Navigate(typeof(PageAjoutAdmin));
            }
        }

        
        private void abButtonListeProjet_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(PageAffichageProjet));
        }
        
        private void abButtonAjoutProjet_Click(object sender, RoutedEventArgs e)
        {
            if (modeAdmin.Admin == true)
            {
                mainFrame.Navigate(typeof(PageAjoutProjet));
            } 
        }

        private void abButtonListeClient_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(PageAffichageClient));
        }

        private void abButtonAjoutClient_Click(object sender, RoutedEventArgs e)
        {
            if (modeAdmin.Admin == true)
            {
                mainFrame.Navigate(typeof(PageAjoutClient));
            }
        }

        private void abButtonListeEmploye_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(PageAffichageEmploye));
        }

        private void abButtonAjoutEmploye_Click(object sender, RoutedEventArgs e)
        {
            if (modeAdmin.Admin == true)
            {
                mainFrame.Navigate(typeof(PageAjoutEmploye));
            }
        }

        private async void abButtonSaveProjet_Click(object sender, RoutedEventArgs e)
        {
            if (modeAdmin.Admin == true)
            { 
                try
                 {
                     var picker = new Windows.Storage.Pickers.FileSavePicker();

                     var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
                     WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

                     picker.SuggestedFileName = "listeProjet";
                     picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".csv" });

                     //crée le fichier
                     Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

                     List<Projet> liste = SingletonProjet.getInstance().GetListeToSave();

                     await Windows.Storage.FileIO.WriteLinesAsync(monFichier, liste.ConvertAll(x => x.ToStringProjet()),
                         Windows.Storage.Streams.UnicodeEncoding.Utf8);

                     ContentDialog dialog = new ContentDialog();
                     dialog.XamlRoot = GridMaitre.XamlRoot;
                     dialog.Title = "Enregistrer";
                     dialog.PrimaryButtonText = "OK";
                     dialog.DefaultButton = ContentDialogButton.Primary;
                     dialog.Content = "La liste a bien été enregistré";

                     ContentDialogResult resultat = await dialog.ShowAsync();
                }
                catch (Exception ex)
                {
                     ContentDialog dialog = new ContentDialog();
                     dialog.XamlRoot = GridMaitre.XamlRoot;
                     dialog.Title = "Erreur";
                     dialog.PrimaryButtonText = "OK";
                     dialog.DefaultButton = ContentDialogButton.Primary;
                     dialog.Content = "Une erreur c'est produite lors de l'enregistrement";

                     ContentDialogResult resultat = await dialog.ShowAsync();
                }
            }
        }
    }
}
