using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using TPV_WINDOWS.Modelo;

namespace TPV_WINDOWS.Vista
{
    /// <summary>
    /// Lógica de interacción para VentanaPreInicio.xaml
    /// </summary>
    public partial class VentanaPreInicio : Window
    {
        //private List<Tienda>? _tiendas;
        //public List<Tienda>? Tiendas { get => _tiendas; set => _tiendas = value; }




        public VentanaPreInicio()
        {
            InitializeComponent();
            grpTienda.Visibility = Visibility.Hidden;
            btnTienda.Visibility = Visibility.Hidden;

        }

        private void txtBPuerto_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnConectar_Click(object sender, RoutedEventArgs e)
        {
            if (ControladorComun.IniciarBD(txtBHost.Text,Convert.ToInt16(txtBPuerto.Text),txtBUsuario.Text,txtBPassword.Password))
            {
                ControladorComun.PreInicializaTienda();
                
                cmbBTiendas.ItemsSource = ControladorComun.Tiendas;
                grpBD.Visibility = Visibility.Hidden;
                grpTienda.Visibility = Visibility.Visible;
                btnTienda.Visibility = Visibility.Visible;
                btnConectar.Content = "¡Conectado!";
                btnConectar.IsEnabled = false;                
            }
            else
            {
                MessageBox.Show("Error al conectar a la BD. Corrija los datos e inténtelo de nuevo");
            }
        }

        private async void btnTienda_Click(object sender, RoutedEventArgs e)
        {
            ControladorComun.TiendaActual = (Tienda)cmbBTiendas.SelectedItem;
            await ControladorComun.CargarPantallaVentas();
            this.Close();
        }
    }


}
