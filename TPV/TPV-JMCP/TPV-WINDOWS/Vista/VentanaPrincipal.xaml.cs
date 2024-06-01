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
using TPV_WINDOWS.Controlador;

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
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            VentanaAcciones ventanaAcciones = new VentanaAcciones();
            this.Show();
            ventanaAcciones.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ControladorComun.CerrarPrograma();
        }

        private void btnCobrar_Click(object sender, RoutedEventArgs e)
        {
            ControladorComun.TpvBase!.GeneraTicket();
        }

        private void btnFuncEnc_Click(object sender, RoutedEventArgs e)
        {
            ControladorComun.TpvBase!.InsertarProductoTest();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            ControladorComun.CerrarPrograma();
        }
        public void ActualizaInfoUsuario()
        {
            lblUsuario.Content = ControladorComun.TpvBase!.UsuarioActual!.Nombre;
        }

        private void imgExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ControladorComun.CerrarPrograma();
        }

        private void imgAvatar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
