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
    /// Lógica de interacción para los cierres de caja
    /// </summary>
    public partial class VentanaCierreCaja : Window
    {
        public VentanaCierreCaja()
        {
            InitializeComponent();
        }

        private void btnCierreCaja_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
    }
}
