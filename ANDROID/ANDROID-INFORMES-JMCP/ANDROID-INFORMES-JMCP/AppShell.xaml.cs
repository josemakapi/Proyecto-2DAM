using ANDROID_INFORMES_JMCP.Controlador;
namespace ANDROID_INFORMES_JMCP
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            ControladorComun.CurrentBD = new Datos.BDMongo(); 
            ControladdorComun.CurrentBD.ConectaBD();
            loginSC.Icon = "about.png";
            loginSC.Content = new LoginPage(this);
        }
    }
}
