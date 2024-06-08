
namespace ANDROID_INFORMES_JMCP.Modelo
{
    /// <summary>
    /// Objeto que define una búsqueda guardada en la app. Tiene persistencia en la BD.
    /// Tiene correlación con la tabla 'busquedas' en la BD.
    /// La combinación de Link + Usuario será la forma inequívoca de identificar una noticia para su manejo y eliminación.
    /// </summary>
    public class Busqueda
    {
        public string Titular { get; set; }
        public string Link { get; set; } 
        public string Provincia { get; set; }
        public string Municipio { get; set; }
        public int UserId { get; set; }

        public Busqueda(string titular, string link, string provincia, string municipio, int userId)
        {
            Titular = titular;
            Link = link;
            Provincia = provincia;
            Municipio = municipio;
            UserId = userId;
        }
    }
}
