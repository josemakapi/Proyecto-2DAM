namespace ANDROID_INFORMES_JMCP.Modelo
{
    /// <summary>
    /// Objeto que define una noticia en la app.
    /// Los datos que alimentan esta clase provienen de la API Rest GNews. No tiene persistencia en la BD, aunque exista la tabla 'noticias' en la BD para propósitos experimentales.
    /// </summary>
    public class Noticia
    {
        public string Titulo {  get; set; }
        public string Detalle { get; set; }
        public string Link { get; set; }
        public string Imagen {  get; set; }

        public Noticia(string titulo, string detalle, string link, string imagen)
        {
            Titulo = titulo;
            Detalle = detalle;
            Link = link;
            Imagen = imagen;
        }

    }
}
