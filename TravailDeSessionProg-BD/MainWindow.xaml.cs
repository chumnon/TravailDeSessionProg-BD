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
        public MainWindow()
        {
            this.InitializeComponent();
            mainFrame.Navigate(typeof(PageAffichageProjet));
        }

        private void abButtonListeProjet_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(PageAffichageProjet));
        }

        private void abButtonAjoutProjet_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(PageAjoutProjet));
        }

        private void abButtonListeClient_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(PageAffichageClient));
        }

        private void abButtonAjoutClient_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(PageAjoutClient));
        }

        private void abButtonListeEmploye_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(PageAffichageEmploye));
        }

        private void abButtonAjoutEmploye_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(PageAjoutEmploye));
        }

        private async void abButtonSaveProjet_Click(object sender, RoutedEventArgs e)
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
