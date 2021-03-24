using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace USP_Congig_generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Clipboard_Weapons_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Output_cfgweapons.Text);
        }
        private void Clipboard_Vehicles_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Output_cfgvehicles.Text);
        }
    }
}
