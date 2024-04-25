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
using System.Windows.Shapes;

namespace TPV_WINDOWS.Vista
{
    /// <summary>
    /// Lógica de interacción para VentanaPrincipal.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window
    {
        public VentanaPrincipal()
        {
            InitializeComponent();
            Modelo.TPVMaster procesoMaster = new Modelo.TPVMaster();
            //procesoMaster.Iniciar();
            VentanaTecladoNumericoUsuario tecladoWindow = new VentanaTecladoNumericoUsuario();
            this.Show();
            //tecladoWindow.ShowDialog();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            VentanaAcciones ventanaAcciones = new VentanaAcciones();
            this.Show();
            ventanaAcciones.ShowDialog();
        }
    }
}
