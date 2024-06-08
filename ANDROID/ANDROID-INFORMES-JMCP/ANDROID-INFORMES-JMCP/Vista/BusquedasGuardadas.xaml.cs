namespace UrbeXPlorer.Vista;
using UrbeXPlorer.Datos;
using UrbeXPlorer.Modelo;
using UrbeXPlorer.Servicios;
using System.Windows.Input;
using System.Diagnostics;

/// <summary>
/// P�gina desde la que gestionamos las noticias previamente guardadas y las convertimos en lugares
/// </summary>
public partial class BusquedasGuardadas : ContentPage
{
    /// <summary>
    /// Para poder abrir links desde controles XAML a los que se les pueda aplicar comandos
    /// </summary>
    public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
    /// <summary>
    /// Lista de b�squedas que luego pintaremos por pantalla y/o guardaremos
    /// </summary>
    private List<Busqueda> listaBusquedas;
    public BusquedasGuardadas()
	{
		InitializeComponent();
        BindingContext = this;
        listaBusquedas = new List<Busqueda>();
    }

    /// <summary>
    /// Sobreescribimos el m�todo que controla el foco de la ContentPage para refrescar las b�squedas guardadas 
    /// cada vez que aparezca esta ContentPage sin necesidad de implementar un bot�n manual
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.DestruyeBusquedas();
        this.CreaListaBusquedas();
        this.PintaListaBusquedas();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        ControladorComun.SalirApp();
    }

    /// <summary>
    /// Generamos la lista de B�squedas partiendo de las propiedades del objeto usuario
    /// </summary>
    private void CreaListaBusquedas()
    {
        this.listaBusquedas = ControladorComun.CurrentBD!.GetListaBusquedas(ControladorComun.CurrentBD.LoggedUserProps!.UserId);
    }

    private void PintaListaBusquedas()
    {
        foreach (Busqueda busqueda in listaBusquedas)
        {
            PintaBusqueda(busqueda);
        }
    }

    /// <summary>
    /// M�todo que pinta las b�squedas guardadas en el grid principal usando frames que armamos desde aqu� y generamos din�micamente.
    /// Tiene los elementos gr�ficos necesarios para una b�squeda guardada, permite abrir el link de la noticia en un navegador haciendo 
    /// click izquierdo en la b�squeda y gestionarla para convertirla en una lugar guardado si se desea.
    /// </summary>
    /// <param name="miBusqueda">La lista de objetos Busqueda que hemos cargado al abrir la ContentPage</param>
    private void PintaBusqueda(Busqueda miBusqueda)
    {
        Frame miFrame = new Frame
        {
            BorderColor = (Color)App.Current!.Resources["ColorBordeFrame"],
            CornerRadius = 5,
            BackgroundColor = (Color)App.Current.Resources["ColorFondoFrame"],
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20, 0, 0)
        };
        SemanticProperties.SetDescription(miFrame, "Esto es el recuadro de una b�squeda guardada");

        // Crear el VerticalStackLayout dentro del Frame
        var stackLayout = new VerticalStackLayout
        {
            WidthRequest = 900,
            HorizontalOptions = LayoutOptions.Center
        };

        // Crear el Label para el t�tulo
        var lblTitulo = new Label
        {
            Text = "Provincia: " + miBusqueda.Provincia,
            FontSize = 14,
            FontAttributes = FontAttributes.Bold
        };

        // Crear el BoxView para la l�nea
        var linea = new BoxView
        {
            //Color = Color.FromArgb("#7FFF0000"),
            Color = (Color)App.Current.Resources["ColorLineaFrame"],
            HeightRequest = 2,
            HorizontalOptions = LayoutOptions.Center
        };

        // Crear el Label para el detalle
        var lblDetalle = new Label
        {
            Text = miBusqueda.Titular
        };
        lblDetalle.Padding = 10;

        var lblLink = new Label
        {
            Text = miBusqueda.Link
        };
        lblLink.Padding = 10;

        //Un Horizontal para nuestros botones
        var HstackLayout = new HorizontalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center
        };

        //Y nuestros botones
        var btnBorra = new Button
        {
            Text = (string)App.Current.Resources["borraBusc"],
            AutomationId = "btnBorra",
            HorizontalOptions = LayoutOptions.Center,
            Style = (Style)App.Current.Resources["btnBoxStyle"]
        };
        btnBorra.Clicked += BtnBorra_Clicked;
        async void BtnBorra_Clicked(object? sender, EventArgs e)
        {
            //Borrar y refrescar
            if (ControladorComun.CurrentBD!.BorraBusqueda(miBusqueda.UserId, miBusqueda.Link))
            {
                this.DestruyeBusquedas();
                this.CreaListaBusquedas();
                this.PintaListaBusquedas();
            }
            else
            {
                await DisplayAlert("Error", "Error al borrar la b�squeda", "Vale");
            }
        }

        var btnAgrega = new Button
        {
            Text = (string)App.Current.Resources["addMyPlaces"],
            AutomationId = "btnAgrega",
            HorizontalOptions = LayoutOptions.Center,
            Style = (Style)App.Current.Resources["btnBoxStyle"]
        };
        if (miBusqueda.Municipio.Length < 1) { btnAgrega.IsEnabled = false; }
        btnAgrega.Clicked += BtnAgrega_Clicked;
        async void BtnAgrega_Clicked(object? sender, EventArgs e)
        {
            //Agregar a Lugares
            if (ControladorComun.CurrentBD!.InsertaLugar(miBusqueda.UserId, miBusqueda.Link, miBusqueda.Municipio))
            {
                //Borrar la agregada y refrescar
                if (ControladorComun.CurrentBD.BorraBusqueda(miBusqueda.UserId, miBusqueda.Link))
                {
                    this.DestruyeBusquedas();
                    this.CreaListaBusquedas();
                    this.PintaListaBusquedas();
                }
                else
                {
                    await DisplayAlert("Error", "Error al borrar la b�squeda", "Vale");
                }
            }
            else
            {
                await DisplayAlert("Error", "Error al crear el lugar", "Vale");
            }
        }

        var btnAsignaMunicipio = new Button
        {
            Text = (string)App.Current.Resources["asigMuni"],
            AutomationId = "btnAsignaMuni",
            HorizontalOptions = LayoutOptions.Center,
            Style = (Style)App.Current.Resources["btnBoxStyle"]
        };
        if (miBusqueda.Municipio.Length < 1) { btnAsignaMunicipio.IsEnabled = true; } else { btnAsignaMunicipio.IsEnabled = false; }
        btnAsignaMunicipio.Clicked += BtnAsignaMunicipio_Clicked;
        async void BtnAsignaMunicipio_Clicked(object? sender, EventArgs e)
        {
            string respuestaMuni = await DisplayPromptAsync("Pregunta", "�Sabes el municipio de esta noticia? Si lo sabes, escr�belo y pulsa 'Vale'. De lo contrario cancela, abre la noticia y d�melo para que lo guarde y podamos continuar", "Vale", "Cancelar");
            miBusqueda.Municipio = respuestaMuni;
            if (respuestaMuni?.Length > 0) //Comprobaci�n adicional para controlar que el usuario haya dado a cancelar que no marque como asignado el municipio estando vac�o. Ojo a este null
            {
                if (ControladorComun.CurrentBD!.SetMuniBusqueda(ControladorComun.CurrentBD.LoggedUserProps!.UserId, miBusqueda.Link, respuestaMuni))
                {
                    btnAgrega.IsEnabled = true;
                    btnAsignaMunicipio.IsEnabled = false;
                }
                else
                {
                    await DisplayAlert("Error", "Error al asignar el municipio", "Vale");
                }
            }
        }

        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += OnLabelTapped!;

        // Agregar elementos al VerticalStackLayout
        stackLayout.Children.Add(linea);
        stackLayout.Children.Add(lblLink);
        stackLayout.Children.Add(lblTitulo);
        stackLayout.Children.Add(lblDetalle);
        stackLayout.Children.Add(HstackLayout);
        // Nuestro Horizontal
        HstackLayout.Children.Add(btnBorra);
        HstackLayout.Children.Add(btnAgrega);
        HstackLayout.Children.Add(btnAsignaMunicipio);
        
        stackLayout.GestureRecognizers.Add(tapGestureRecognizer);

        // Agregar el VerticalStackLayout al Frame
        miFrame.Content = stackLayout;

        // Agregar el Frame a la nueva fila
        gridBusquedasG.Add(miFrame);

        //A�adimos gridBusquedas al scroll
        scrollBusquedasG.AddLogicalChild(gridBusquedasG);
    }

    /// <summary>
    /// Usado para limpiar el grid de b�squedas
    /// </summary>
    private void DestruyeBusquedas()
    {
        gridBusquedasG.Clear();
        listaBusquedas.Clear();
    }

    /// <summary>
    /// M�todo que controla el evento de click izquierdo sobre el frame que contiene las b�squedas guardadas.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnLabelTapped(object sender, EventArgs e)
    {
        VerticalStackLayout container = (VerticalStackLayout)sender;
        var label = container.Children.OfType<Label>().FirstOrDefault();
        if (label != null)
        {
            //Lo tratamos en otro m�todo, pues la mierda del DisplayAlert es asincr�nico
            PreguntaAbrir("Pregunta", "�Abrimos este enlace?:", label.Text);
        }
    }

    /// <summary>
    /// M�todo usado para mostrar una pregunta al usuario (en este caso para el m�todo de OnLabelTapped para abrir un v�nculo)
    /// </summary>
    /// <param name="tituloVentana">String con el t�tulo que queremos dar a la ventana de pregunta</param>
    /// <param name="contenido">Contenido de la pregunta</param>
    /// <param name="enlace">Link asociado</param>
    private async void PreguntaAbrir(string tituloVentana, string contenido, string enlace)
    {
        bool pasarNoticia;
        pasarNoticia = await DisplayAlert(tituloVentana, contenido, "Si", "No");
        Debug.WriteLine("Respuesta: ");
        if (pasarNoticia)
        {
            ControladorComun.AbrirUrl(enlace);
        }
    }
}