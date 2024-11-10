using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace ejumboS6_7;

public partial class MainPage : ContentPage
{
	private const string Url = "http://10.0.2.2/moviles/post.php";
	private readonly HttpClient cliente = new HttpClient();
	private ObservableCollection<Estudiante> estud;

    public MainPage()
	{
		InitializeComponent();
		Obtener();
	}

	private async void Obtener()
	{
		var contect = await cliente.GetStringAsync(Url);
		List<Estudiante> mostrarEst = JsonConvert.DeserializeObject<List<Estudiante>>(contect);
		estud = new ObservableCollection<Estudiante>(mostrarEst);
		listaEstudiantes.ItemsSource = estud;

    }
}


