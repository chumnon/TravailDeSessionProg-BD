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
    public sealed partial class PageAffichageEmploye : Page
    {
        public PageAffichageEmploye()
        {
            this.InitializeComponent();
            gvListeEmploye.Items.Clear();
            gvListeEmploye.ItemsSource = SingletonEmploye.getInstance().GetListeEmploye();

            if (modeAdmin.Admin == false)
            {
                btModifier.Visibility = Visibility.Collapsed;
            }
        }

        private async void btModifier_Click(object sender, RoutedEventArgs e)
        {
            if (gvListeEmploye.SelectedIndex >= 0)
            {
                var matriculeEmploye = (gvListeEmploye.SelectedItem as Employe)?.Matricule;
                this.Frame.Navigate(typeof(PageModifierEmploye), matriculeEmploye);
            }
        }
    }
}
