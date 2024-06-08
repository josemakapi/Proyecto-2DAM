namespace ANDROID_INFORMES_JMCP.Modelo
{
    /// <summary>
    /// Objeto que define un lugar en la app. Tiene persistencia en la BD.
    /// Tiene correlación con la tabla 'lugares' en la BD.
    /// La combinación de Link + UserId será la forma inequívoca de identificar un lugar para su manejo y eliminación.
    /// </summary>
    public class Lugar
    {
        public string Link { get; set; }
        public string Municipio { get; set; }
        public string Notas { get; set; }
        public int UserId { get; set; }

        public Lugar(string link, string municipio, string notas, int userId)
        {
            Link = link;
            Municipio = municipio;
            Notas = notas;
            UserId = userId;
        }  
    }
}
