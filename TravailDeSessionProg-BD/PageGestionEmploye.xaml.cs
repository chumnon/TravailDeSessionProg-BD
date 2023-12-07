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
using System.Reflection;
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
    public sealed partial class PageGestionEmploye : Page
    {
        string leNumProjet = "";
        public PageGestionEmploye()
        {
            this.InitializeComponent();
            ListeEmploye.ItemsSource = SingletonEmploye.getInstance().GetListeEmployeSansTravail();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                leNumProjet = (int)e.Parameter;
            }
        }

        private async void btAjoutTache_Click(object sender, RoutedEventArgs e)
        {
            resetErreurs();
            bool valide = true;

            if (ValidationTache.getInstance().isEmployeValide(inEmploye.SelectedValue) == false)
            {
                ErrEmploye.Text = "Veuillez choisir un employé";
                valide = false;
            }

            if (ValidationTache.getInstance().isNbrHeureValide(inNbrHeure.Text) == false)
            {
                ErrNbrHeure.Text = "Veuillez choisir un nombre d'heure valide (int)";
                valide = false;
            }

            if (valide == true)
            {
                Tache uneTache = new Tache
                {
                    NumTache = "",
                    LEmploye = inEmploye.SelectedValue,
                    LeProjet = leNumProjet,
                    Salaire = 0,
                    NbrHeure = int.Parse(inNbrHeure.Text)
                };

                bool err = SingletonTache.getInstance().ajouterTache(uneTache);

                if (err == false)
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = mainGrid.XamlRoot;
                    dialog.Title = "Ajout de la tâche";
                    dialog.PrimaryButtonText = "OK";
                    dialog.DefaultButton = ContentDialogButton.Primary;
                    dialog.Content = "La tache a bien été assigné";

                    ContentDialogResult resultat = await dialog.ShowAsync();

                    this.Frame.Navigate(typeof(PageZoomProjet), leNumProjet);
                }
                else if (err == true)
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = mainGrid.XamlRoot;
                    dialog.Title = "Ajout de la tâche";
                    dialog.PrimaryButtonText = "OK";
                    dialog.DefaultButton = ContentDialogButton.Primary;
                    dialog.Content = "Erreur, la tâche n'a pas pu être donné";

                    ContentDialogResult resultat = await dialog.ShowAsync();
                }
            }
        }

        private void resetErreurs()
        {
            ErrEmploye.Text = string.Empty;
            ErrNbrHeure.Text = string.Empty;
        }
    }
}
