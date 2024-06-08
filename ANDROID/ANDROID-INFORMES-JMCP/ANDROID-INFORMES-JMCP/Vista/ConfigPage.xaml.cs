namespace UrbeXPlorer.Vista;
using UrbeXPlorer.Modelo;

/// <summary>
/// Página para gestionar los ajustes del usuario
/// </summary>
public partial class ConfigPage : ContentPage
{
    private List<string> listaIdiomas;
    private List<string> listaVisuales;
    private List<string> listaProvincias;
    private List<string> listaTamLetra;

    /// <summary>
    /// Inicializamos las listas de los elementos visuales y provincias, así como los métodos de los picker asociados
    /// </summary>
    public ConfigPage()
	{
        InitializeComponent();
        BindingContext = this;

        IniciaListaIdiomas();
        IniciaListaVisuales();
        IniciaListaProvincias();
        IniciaListaTamLetra();
        
        pkIdiomaCfg.SelectedIndexChanged += PkIdioma_SelectedIndexChanged;
        pkTemaCfg.SelectedIndexChanged += PkTemaCfg_SelectedIndexChanged;
        pkProvCfg.SelectedIndexChanged += PkProvCfg_SelectedIndexChanged;
        pkTamLetra.SelectedIndexChanged += PkTamLetra_SelectedIndexChanged;
    }

    /// <summary>
    /// Rellenamos los pickers con las listas teniendo en cuenta los diccionarios cargados y establecemos la nueva configuración seleccionada por el usuario, haciendo persistencia en la BD.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PkTamLetra_SelectedIndexChanged(object? sender, EventArgs e)
    {
        string selectedElement = (string)pkTamLetra.SelectedItem;
        if (selectedElement.Equals("Grande") || selectedElement.Equals("Big"))
        {
            ControladorComun.CurrentBD!.LoggedUserProps!.TamFuente = "big";
            ControladorComun.CurrentBD.SetTamFuente("big", ControladorComun.CurrentBD!.LoggedUserProps!.UserName, ControladorComun.CurrentBD!.LoggedUserProps!.Password);
        }
        else if (selectedElement.Equals("Pequeña") || selectedElement.Equals("Small"))
        {
            ControladorComun.CurrentBD!.LoggedUserProps!.TamFuente = "little";
            ControladorComun.CurrentBD.SetTamFuente("little", ControladorComun.CurrentBD!.LoggedUserProps!.UserName, ControladorComun.CurrentBD!.LoggedUserProps!.Password);
        }
        ControladorComun.CurrentBD!.LoggedUserProps!.ActualizaEstilos(true);
    }

    /// <summary>
    /// Rellenamos los pickers con las listas teniendo en cuenta los diccionarios cargados y establecemos la nueva configuración seleccionada por el usuario, haciendo persistencia en la BD.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PkProvCfg_SelectedIndexChanged(object? sender, EventArgs e)
    {
        string selectedElement = (string)pkProvCfg.SelectedItem;
        DisplayAlert(Title, "Tu nueva provincia es "+selectedElement+". Tenlo en cuenta cuando uses las búsquedas y ubicaciones","Vale");
        ControladorComun.CurrentBD!.LoggedUserProps!.Provincia = selectedElement; //Actualizamos el objeto usuario cargado actualmente
        ControladorComun.CurrentBD.SetProvincia(selectedElement, ControladorComun.CurrentBD!.LoggedUserProps!.UserName, ControladorComun.CurrentBD!.LoggedUserProps!.Password); //Y hacemos persistencia en la BD. Prefiero hacer las dos cosas por separado
    }

    /// <summary>
    /// Rellenamos los pickers con las listas teniendo en cuenta los diccionarios cargados y establecemos la nueva configuración seleccionada por el usuario, haciendo persistencia en la BD.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PkTemaCfg_SelectedIndexChanged(object? sender, EventArgs e)
    {
        string selectedElement = (string)pkTemaCfg.SelectedItem;
        if (selectedElement.Equals("Original (recomendado)") || selectedElement.Equals("Original (recommended)"))
        {
            ControladorComun.CurrentBD!.LoggedUserProps!.VisualTheme = "OriginalTheme"; 
            ControladorComun.CurrentBD.SetVisualTheme("OriginalTheme", ControladorComun.CurrentBD.LoggedUserProps.UserName, ControladorComun.CurrentBD!.LoggedUserProps!.Password); 
        }
        else if (selectedElement.Equals("Alto contraste") || selectedElement.Equals("High contrast"))
        {
            ControladorComun.CurrentBD!.LoggedUserProps!.VisualTheme = "TemaAC";
            ControladorComun.CurrentBD.SetVisualTheme("TemaAC", ControladorComun.CurrentBD.LoggedUserProps.UserName, ControladorComun.CurrentBD!.LoggedUserProps!.Password);
        }
        ControladorComun.CurrentBD!.LoggedUserProps!.ActualizaEstilos(true);
    }

    /// <summary>
    /// Rellenamos los pickers con las listas teniendo en cuenta los diccionarios cargados y establecemos la nueva configuración seleccionada por el usuario, haciendo persistencia en la BD.
    /// Para que se traduzca el propio picker ha sido necesario desapilar la página de ajustes y reabrirla, 
    /// pues no accedía al diccionario de idiomas a tiempo para rellenar el picker y daba error de objeto nulo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PkIdioma_SelectedIndexChanged(object? sender, EventArgs e)
    {
        string selectedElement = (string)pkIdiomaCfg.SelectedItem;
        if (selectedElement.Equals("English") || selectedElement.Equals("Inglés"))
        {
            DisplayAlert("Advertencia", "Tu nuevo idioma es " + selectedElement + ". Ahora tus búsquedas en noticias se harán en " + selectedElement, "Vale");
            ControladorComun.CurrentBD!.LoggedUserProps!.Idioma = "EN"; 
            ControladorComun.CurrentBD.SetIdioma("EN",ControladorComun.CurrentBD.LoggedUserProps.UserName, ControladorComun.CurrentBD!.LoggedUserProps!.Password);   
        }
        else if (selectedElement.Equals("Castellano") || selectedElement.Equals("Castillian"))
        {
            DisplayAlert("Warning", "Your current language will be " + selectedElement + ". All new searchings will be in " + selectedElement + " language", "OK");
            ControladorComun.CurrentBD!.LoggedUserProps!.Idioma = "ES";
            ControladorComun.CurrentBD.SetIdioma("ES", ControladorComun.CurrentBD.LoggedUserProps.UserName, ControladorComun.CurrentBD.LoggedUserProps.Password);
        }
        ControladorComun.CurrentBD!.LoggedUserProps!.ActualizaEstilos(true);

        //Como los picker no se actualizan automáticamewnte al cambiar el idioma y tampoco recargando las listas, recargamos la ContentPage
        ConfigPage currentPage = this;
        ConfigPage newPage = new ConfigPage();
        Navigation.RemovePage(currentPage);
        Navigation.PushAsync(newPage);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        ControladorComun.SalirApp();
    }

    /// <summary>
    /// Inicia la lista de idiomas teniendo en cuenta los diccionarios que tengamos cargados en el momento
    /// </summary>
    private void IniciaListaIdiomas()
    {
        listaIdiomas = new List<string>();
        listaIdiomas.Add((string)App.Current!.Resources["idiomaCastellano"]);
        listaIdiomas.Add((string)App.Current!.Resources["idiomaEnglish"]);
        pkIdiomaCfg.ItemsSource = listaIdiomas;
        if (ControladorComun.CurrentBD!.LoggedUserProps!.Idioma.Equals("ES")) pkIdiomaCfg.SelectedItem = (string)App.Current!.Resources["idiomaCastellano"];
        if (ControladorComun.CurrentBD!.LoggedUserProps!.Idioma.Equals("EN")) pkIdiomaCfg.SelectedItem = (string)App.Current!.Resources["idiomaEnglish"];
    }

    /// <summary>
    /// Inicia la lista de temas visuales teniendo en cuenta los diccionarios que tengamos cargados en el momento
    /// </summary>
    private void IniciaListaVisuales()
    {
        listaVisuales = new List<string>();
        listaVisuales.Add((string)App.Current!.Resources["temaOriginalName"]);
        listaVisuales.Add((string)App.Current.Resources["temaACName"]);
        pkTemaCfg.ItemsSource = listaVisuales;
        if (ControladorComun.CurrentBD!.LoggedUserProps!.VisualTheme.Equals("OriginalTheme")) pkTemaCfg.SelectedIndex = 0;
        if (ControladorComun.CurrentBD.LoggedUserProps.VisualTheme.Equals("TemaAC")) pkTemaCfg.SelectedIndex = 1;
    }

    /// <summary>
    /// Inicia la lista de provincias teniendo en cuenta los diccionarios que tengamos cargados en el momento
    /// </summary>
    private void IniciaListaProvincias()
    {
        listaProvincias = new List<string>();
        listaProvincias = ControladorComun.CurrentBD!.GetListaProvincias();
        pkProvCfg.ItemsSource = listaProvincias;
        pkProvCfg.SelectedItem = ControladorComun.CurrentBD!.LoggedUserProps!.Provincia;
    }

    /// <summary>
    /// Inicia la lista de fuentes teniendo en cuenta los diccionarios que tengamos cargados en el momento
    /// </summary>
    private void IniciaListaTamLetra()
    {
        listaTamLetra = new List<string>();
        listaTamLetra.Add((string)App.Current!.Resources["tamLetraBig"]);
        listaTamLetra.Add((string)App.Current!.Resources["tamLetraSmall"]);
        pkTamLetra.ItemsSource = listaTamLetra;
        if (ControladorComun.CurrentBD!.LoggedUserProps!.TamFuente.Equals("big")) pkTamLetra.SelectedIndex = 0;
        if (ControladorComun.CurrentBD!.LoggedUserProps!.TamFuente.Equals("little")) pkTamLetra.SelectedIndex = 1;
    }

}