using ANDROID_INFORMES_JMCP.Controlador;

namespace ANDROID_INFORMES_JMCP.Vista;

/// <summary>
/// P�gina para el about
/// </summary>
public partial class AboutPage : ContentPage
{
    public AboutPage()
	{
		InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        ControladorComun.SalirApp();
    }
}