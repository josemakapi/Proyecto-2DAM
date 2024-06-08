namespace ANDROID_INFORMES_JMCP.Vista;
using ANDROID_INFORMES_JMCP.Datos;
using System.Diagnostics;
using ANDROID_INFORMES_JMCP.Controlador;

/// <summary>
/// P�gina administrativa a la que tienen acceso s�lamente los usuarios con peril < 2 en la BD
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
    /// Controla el comportamiento del bot�n de borrado de usuarios no administradores. Usa un control as�ncrono para preguntar al usuario y no bloquear la aplicaci�n
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void btnBorraUser_Clicked(object sender, EventArgs e)
    {
        if (pkUser.SelectedItem!=null) {
            bool respuesta;
            respuesta = await DisplayAlert("Advertencia", "Al borrar un usuario borrar�s tambi�n todo el contenido que tuviera almacenado, como sus b�squedas guardadas y lugares\n�Confirmas su borrado?", "Si", "No");
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
            await DisplayAlert("Advertencia","Debes tener seleccionado alg�n usuario","OK");
        }
    }

    /// <summary>
    /// Controla el comportamiento del bot�n de elevaci�n de usuarios no administradores. Usa un control as�ncrono para preguntar al usuario y no bloquear la aplicaci�n
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void btnElevaUser_Clicked(object sender, EventArgs e)
    {
        if (pkUser.SelectedItem != null)
        {
            bool respuesta;
            respuesta = await DisplayAlert("Advertencia", "Al hacer administrador a un usuario ya no podr� ser eliminado.\n�Confirmas hacer administrador a "+pkUser.SelectedItem+"?", "Si", "No");
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
            await DisplayAlert("Advertencia", "Debes tener seleccionado alg�n usuario", "OK");
        }
    }
}