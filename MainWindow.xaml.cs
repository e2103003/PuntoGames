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

namespace Punto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowPopUpBDD(); // Appelez la méthode pour afficher le UserControl PopUpBDD.
        }

        private void ShowPopUpBDD()
        {
            // Créez une instance de votre UserControl.
            var popUpBDD = new PopUpBDD();

            // Affichez le UserControl dans la fenêtre principale.
            this.Content = popUpBDD;
        }
    }
}
