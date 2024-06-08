namespace ANDROID_INFORMES_JMCP.Vista;
using ANDROID_INFORMES_JMCP.Datos;
using System.Diagnostics;
using ANDROID_INFORMES_JMCP.Controlador;

/// <summary>
/// Página administrativa a la que tienen acceso sólamente los usuarios con peril < 2 en la BD
/// </summary>
public partial class AdminPage : ContentPage
{
	public AdminPage()
	{
		InitializeComponent();
        CargaListaUsuariosPicker();
	}

    /// <summary>
    /// Como indica su nombre, carga y recarga la lista de usuarios para el picker que lo muestra en pantalla.
    /// </summary>
    private void CargaListaUsuariosPicker() 
    {
        pkUser.SelectedItem = null;
        pkUser.ItemsSource = ControladorComun.CurrentBD!.GetListaUsuariosNoAdmin();
        pkUser.SelectedIndex = 0;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        ControladorComun.SalirApp();
    }

    /// <summary>
    /// Controla el comportamiento del botón de borrado de usuarios no administradores. Usa un control asíncrono para preguntar al usuario y no bloquear la aplicación
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void btnBorraUser_Clicked(object sender, EventArgs e)
    {
        if (pkUser.SelectedItem!=null) {
            bool respuesta;
            respuesta = await DisplayAlert("Advertencia", "Al borrar un usuario borrarás también todo el contenido que tuviera almacenado, como sus búsquedas guardadas y lugares\n¿Confirmas su borrado?", "Si", "No");
            Debug.WriteLine("Respuesta: ");
            if (respuesta)
            {

                if (ControladorComun.CurrentBD!.BorraUsuarioBD((string)pkUser.SelectedItem))
                {
                    await DisplayAlert("Info", "Usuario borrado correctamente", "OK");
                    this.CargaListaUsuariosPicker();
                }
                else 
                { 
                    await DisplayAlert("Advertencia", "Error al borrar usuario", "OK");
                }
            }
        }
        else
        {
            await DisplayAlert("Advertencia","Debes tener seleccionado algún usuario","OK");
        }
    }

    /// <summary>
    /// Controla el comportamiento del botón de elevación de usuarios no administradores. Usa un control asíncrono para preguntar al usuario y no bloquear la aplicación
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void btnElevaUser_Clicked(object sender, EventArgs e)
    {
        if (pkUser.SelectedItem != null)
        {
            bool respuesta;
            respuesta = await DisplayAlert("Advertencia", "Al hacer administrador a un usuario ya no podrá ser eliminado.\n¿Confirmas hacer administrador a "+pkUser.SelectedItem+"?", "Si", "No");
            Debug.WriteLine("Respuesta: ");
            if (respuesta)
            {

                if (ControladorComun.CurrentBD!.ElevaUsuarioBD((string)pkUser.SelectedItem))
                {
                    await DisplayAlert("Info", pkUser.SelectedItem+" ya es administrador", "OK");
                    this.CargaListaUsuariosPicker();
                }
                else
                {
                    await DisplayAlert("Advertencia", "Error al intentar hacer administrador al usuario", "OK");
                }
            }
        }
        else
        {
            await DisplayAlert("Advertencia", "Debes tener seleccionado algún usuario", "OK");
        }
    }
}