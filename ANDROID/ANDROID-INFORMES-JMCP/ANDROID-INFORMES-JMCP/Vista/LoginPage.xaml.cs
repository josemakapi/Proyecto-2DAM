using System.Diagnostics;
using UrbeXPlorer.Datos;
using UrbeXPlorer.Modelo;


namespace UrbeXPlorer.Vista;

public partial class LoginPage : ContentPage
{
    /// <summary>
    /// Usada para el men� contextual que conmuta el tema visual
    /// </summary>
    private bool isAcTheme = false;

    private AppShell receivedAppShell;

    public LoginPage(AppShell miAppShell)
    {
        InitializeComponent();
        this.receivedAppShell = miAppShell;
    }

    /// <summary>
    /// M�todo para dar la bienvenida al usuario al programa en caso de que no haya usuarios registrados
    /// Es llamado desde el evento click del bot�n de Conectar en la pantalla de login si �ste detecta que no hay ning�n usuario en la BD
    /// </summary>
    private async void Bienvenida()
    {
        bool abrirReg;
        abrirReg = await DisplayAlert("Bienvenida", "�Bienvenidos a UrbeXPlorer!\nEn primer lugar, te animamos a leerte " +
            "el apartado de ayuda situado en la parte superior derecha.\nDado que acabas de hacerte con tu copia de este programa, " +
            "lo primero que debes hacer es crearte una cuenta.\nLa cuenta que crear�s esta primera vez tiene permisos de administrador, " +
            "por lo que debes asegurarte de que no la pierdes, pues de lo contrario no podr�s configurar opciones avanzadas ni hacer " +
            "administradores a otros usuarios.\n�Te llevamos a la p�gina de registro ahora?", "Si", "No");
        Debug.WriteLine("Respuesta: ");
        if (abrirReg)
        {
            Navigation?.PushAsync(new RegisterPage());
        }
    }

    private void btnMostrar_Pressed(object sender, EventArgs e)
    {
        txtPass.IsPassword = false;
    }

    private void btnMostrar_Released(object sender, EventArgs e)
    {
        txtPass.IsPassword = true;
    }

    /// <summary>
    /// Desencadena la conexi�n a la BD y la inicializaci�n de los objetos est�ticos: 
    /// la BD y las propiedades del usuario, para ser utilizados de forma centralizada 
    /// en todo el programa. Este objeto usuario est�tico ser� accesible a trav�s de SharedObjects.CurrentBD.LoggedUserProps.
    /// Se cargan y aplican los estilos y configuraciones que tuviera configurados en la BD.
    /// Tambi�n se comprueba si hay usuarios en la BD. Si no hay, se lanza la bienvenida.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnConectar_Clicked(object sender, EventArgs e)
    {
        ControladorComun.CurrentBD!.ConectaBD();
        //Ojo, nos rellenar� nuestro objeto est�tico SharedObjects.CurrentBD.LoggedUserProps con todas las propiedades del usuario actual
        if (ControladorComun.CurrentBD.CompruebaExistenUsuarios()) {
            if (ControladorComun.CurrentBD.CheckCredentials(txtUser.Text, txtPass.Text)) { 
                if (ControladorComun.CurrentBD.AppLogin(txtUser.Text, txtPass.Text))
                {
                    receivedAppShell.SetConectado();
                    ControladorComun.CurrentBD.LoggedUserProps!.ActualizaEstilos(true); //Con esto aplicamos los estilos que tenga el usuario configurados (como los acabamos de leer de la BD y cargado en la clase est�tica...)
                }
                else
                {
                    DisplayAlert("Error", "Usuario/contrase�a no v�lidos", "OK");
                }
            }
            else
            {
                DisplayAlert("Error", "Usuario/contrase�a inv�lidos", "OK");
            }
        }
        else
        {
            Bienvenida();
        }
    }
    private void btnRegistrar_Clicked(object sender, EventArgs e)
    {
        ControladorComun.CurrentBD!.ConectaBD();
        Navigation.PushAsync(new RegisterPage());
    }

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        ControladorComun.SalirApp();
    }

    /// <summary>
    /// Controla el comportamiento del click contextual que permite conmutar entre los temas visuales en la pantalla de Login
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ItemTema_Clicked(object? sender, EventArgs e)
    {
        ICollection<ResourceDictionary> misDiccionarios = Application.Current!.Resources.MergedDictionaries;
        if (!isAcTheme)
        {
            
            misDiccionarios.Clear();
            misDiccionarios.Add(new UrbeXPlorer.Resources.Themes.TemaAC());
            misDiccionarios.Add(new UrbeXPlorer.Resources.Strings.Castellano());
            misDiccionarios.Add(new UrbeXPlorer.Resources.Styles.SmallFontsMode());
            this.Contextual.Text = "Cambiar a tema Original";
            isAcTheme = true;
        }
        else
        {
            
            misDiccionarios.Clear();
            misDiccionarios.Add(new UrbeXPlorer.Resources.Themes.OriginalTheme());
            misDiccionarios.Add(new UrbeXPlorer.Resources.Strings.Castellano());
            misDiccionarios.Add(new UrbeXPlorer.Resources.Styles.SmallFontsMode());
            this.Contextual.Text = "Cambiar a tema Alto Contraste";
            isAcTheme = false;
        }

    }
}