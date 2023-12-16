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
    public sealed partial class PageAffichageProjet : Page
    {
        public PageAffichageProjet()
        {
            this.InitializeComponent();
            gvListeProjet.Items.Clear();
            gvListeProjet.ItemsSource = SingletonProjet.getInstance().GetListeProjet();
        }

        private void gvListeProjet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gvListeProjet.SelectedIndex >= 0)
            {
                var numeroProjet = (gvListeProjet.SelectedItem as Projet)?.Numero;
                this.Frame.Navigate(typeof(PageZoomProjet), numeroProjet);
            }
        }
    }
}