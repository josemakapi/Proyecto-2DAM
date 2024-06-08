using UrbeXPlorer.Modelo;
namespace UrbeXPlorer.Vista;

/// <summary>
/// Página de ayuda al usuario
/// </summary>
public partial class HelpPage : ContentPage
{
	private AppShell receivedAppShell;
	public HelpPage(AppShell shell)
	{
		InitializeComponent();
		this.receivedAppShell = shell;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		ControladorComun.SalirApp();
    }
}