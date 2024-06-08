namespace ANDROID_INFORMES_JMCP.Controlador
{

    public class ControladorComun
    {
        /// <summary>
        /// La madre del cordero. Este objeto sustenta el funcionamiento de todas las configuraciones del usuario y sus interacciones con la BD.
        /// Se instancia en el momento de hacer Login y se pone a null en el momento de hacer logout mediante el método DesconectaBD() de la clase BBDD. Lee las configuraciones del usuario en la BD y genera 
        /// un objeto LoggedUserProps con todas sus propiedades para que se puedan usar en la App. 
        /// Más detalles en las clases de la BD y LoggedUserProps.
        /// </summary>
        public static Datos.BDMongo? CurrentBD;

        /// <summary>
        /// El nombre lo dice todo. No le demos más vueltas.
        /// </summary>
        public static void SalirApp()
        {
            Application.Current!.Quit();
        }

        /// <summary>
        /// El nombre lo dice todo. Abre el navegador que tengas predeterminado en tu S.O y accede a la URL
        /// </summary>
        /// <param name="url">La URL en formato string</param>
        public static void AbrirUrl(string url)
        {
            Launcher.OpenAsync(new Uri(url));
        }
    }
}
