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
    public sealed partial class PageConnexion : Page
    {

        public PageConnexion()
        {
            this.InitializeComponent();
        }
        private async void btConnexion_Click(object sender, RoutedEventArgs e)
        {
            string message = SingletonAdmin.getInstance().connexion(inNom.Text, inMdp.Text);
            if (message == "Connexion réussi")
            {
                modeAdmin.Admin = true;

                MainWindow.Instance.RafraichirMainWindow();

                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = mainGrid.XamlRoot;
                dialog.Title = "Connexion";
                dialog.PrimaryButtonText = "OK";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = "Vous vous êtes bien connecté";

                ContentDialogResult resultat = await dialog.ShowAsync();

                this.Frame.Navigate(typeof(PageAffichageProjet));
            }
            else
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = mainGrid.XamlRoot;
                dialog.Title = "Connexion";
                dialog.PrimaryButtonText = "OK";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = "Erreur: " + message;

                ContentDialogResult resultat = await dialog.ShowAsync();
            }
        }
    }
}
