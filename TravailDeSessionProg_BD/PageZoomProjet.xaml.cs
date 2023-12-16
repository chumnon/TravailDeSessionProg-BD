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
    public sealed partial class PageZoomProjet : Page
    {
        string index = "";
        public PageZoomProjet()
        {
            this.InitializeComponent();
            gvListeTache.Items.Clear();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                index = e.Parameter.ToString();
                gvListeTache.ItemsSource = SingletonTache.getInstance().GetListeTacheDunProjet(index);
                Projet unProjet = SingletonProjet.getInstance().getProjet(index);

                outNumero.Text = "Numéro: " + unProjet.Numero.ToString();
                outTitre.Text = unProjet.Titre.ToString();
                outDateDebut.Text = "Date de début: " + unProjet.DateDebut.ToString();
                outDescription.Text = "Desciption: \n" + unProjet.Description.ToString();
                outBudget.Text = "Budget: " + unProjet.Budget.ToString();
                outNbrEmploye.Text = "Nombre d'employe: " + unProjet.NbrEmploye.ToString();
                outSalaireTotal.Text = "Salaire total: " + unProjet.SalaireTotal.ToString();
                outClient.Text = "Client: " + unProjet.SonClient.ToString();
                outStatut.Text = "Statut: " + unProjet.Statut.ToString();
            }
        }

        private async void btAjoutTache_Click(object sender, RoutedEventArgs e)
        {
            /*if(SingletonProjet.getInstance().CheckNbrEmployeProjet(index) == true)
            {*/
                this.Frame.Navigate(typeof(PageGestionEmploye), index);
            /*}
            else
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = mainGrid.XamlRoot;
                dialog.Title = "Ajout d'une tâche";
                dialog.PrimaryButtonText = "OK";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = "Vous ne pouvez pas ajouter plus de tâche à ce projet";

                ContentDialogResult resultat = await dialog.ShowAsync();
            }*/
            
        }

        private async void btTerminerProjet_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageGestionEmploye), index);
        }
    }
}
