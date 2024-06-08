using ANDROID_INFORMES_JMCP.Resources.Themes;
using ANDROID_INFORMES_JMCP.Resources.Styles;
using ANDROID_INFORMES_JMCP.Resources.Strings;


namespace ANDROID_INFORMES_JMCP.Modelo
{
    /// <summary>
    /// Clase con todas las propiedades del usuario logado. Se instancia desde la clase BBDD y se asocia como propiedad de ésta. 
    /// A su vez, la clase de BBDD es instanciada de forma estática desde SharedObjects para estar disponible a lo largo de toda la App.
    /// Si quieres acceder a las propiedades del usuario, debes hacer SharedObjects.CurrentBD.LoggedUserProps.>'Tu propiedad de usuario va aquí'<
    /// </summary>
    public class LoggedUserProps
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Perfil {  get; set; }
        public string Provincia { get; set; }
        public string Idioma {  get; set; }
        public string VisualTheme {  get; set; }
        public string TamFuente { get; set; }
        public int UserId { get; set; }


        public LoggedUserProps(string userName, string pass, int perfil, string provincia, string idioma, string visualTheme, string tamFuente, int userId)
        {
            this.UserName = userName;
            this.Password = pass;
            this.Perfil = perfil;
            this.Provincia = provincia;
            this.Idioma = idioma;
            this.VisualTheme = visualTheme;
            this.TamFuente = tamFuente;
            this.UserId = userId;
        }

        public override string ToString()
        {
            return $"Nombre: {UserName}, Perfil: {Perfil}, Provincia: {Provincia} Idioma: {Idioma} Tema visual: {VisualTheme}, Tamaño de fuente: {TamFuente}.";
        }

        /// <summary>
        /// Con esta clase controlamos los estilos de la aplicación.
        /// Según las propiedades del usuario cargado (y controlado en nuestra clase estática LoggedUserProps) se establecerán los estilos.
        /// </summary>
        /// <param name="modoOnline">Con este parámetro a false establecemos un conjunto de estilos predeterminados para áreas comunes e inicio de la aplicación justo antes de deslogar al usuario.
        /// Para funcionar necesita que la BD esté levantada. En true aplicará los diccionarios de estilos que tengamos definidos en el usuario logado actualmente</param> 
        public void ActualizaEstilos(bool modoOnline)
        {
            ICollection<ResourceDictionary> misDiccionarios = Application.Current!.Resources.MergedDictionaries;
            misDiccionarios.Clear();

            if (modoOnline) { 
                switch (VisualTheme)
                {
                    case "OriginalTheme":
                        misDiccionarios.Add(new OriginalTheme());
                        break;
                    case "TemaAC":
                        misDiccionarios.Add(new TemaAC());
                        break;
                }
                switch (Idioma)
                {
                    case "ES":
                        misDiccionarios.Add(new Castellano());
                        break;
                    case "EN":
                        misDiccionarios.Add(new English());
                        break;
                }
                switch (TamFuente)
                {
                    case "big":
                        misDiccionarios.Add(new BigFontsMode());
                        break;
                    case "little":
                        misDiccionarios.Add(new SmallFontsMode());
                        break;
                }
            }
            else
            {
                misDiccionarios.Add(new ANDROID_INFORMES_JMCP.Resources.Themes.OriginalTheme());
                misDiccionarios.Add(new ANDROID_INFORMES_JMCP.Resources.Strings.Castellano());
                misDiccionarios.Add(new ANDROID_INFORMES_JMCP.Resources.Styles.SmallFontsMode());
            }
        }
    }
}
