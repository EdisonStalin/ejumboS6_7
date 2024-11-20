using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Text;

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

    private async void OnAgregarClicked(object sender, EventArgs e)
    {
        // Crear un nuevo estudiante (puedes reemplazar esto con datos del usuario)
        var nuevoEstudiante = new Estudiante
        {
            //codigo = 0, // El servidor generará el código automáticamente
            nombre = "Nuevo",
            apellido = "Estudiante",
            edad = 20
        };

        var json = JsonConvert.SerializeObject(nuevoEstudiante);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var respuesta = await cliente.PostAsync(Url, content);

        if (respuesta.IsSuccessStatusCode)
        {
            await DisplayAlert("Éxito", "Estudiante creado correctamente", "OK");
            Obtener(); // Refrescar lista
        }
        else
        {
            await DisplayAlert("Error", "No se pudo crear el estudiante", "OK");
        }
    }

    private async void OnActualizarClicked(object sender, EventArgs e)
    {
        // Seleccionar un estudiante para actualizar (puedes reemplazar esto con lógica de selección)
        if (estud == null || estud.Count == 0)
        {
            await DisplayAlert("Error", "No hay estudiantes para actualizar", "OK");
            return;
        }

        var estudianteActualizado = estud[0]; // Ejemplo: tomar el primer estudiante
        estudianteActualizado.nombre = "Actualizado";

        var urlPut = $"{Url}?codigo={estudianteActualizado.codigo}";
        var json = JsonConvert.SerializeObject(estudianteActualizado);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var respuesta = await cliente.PutAsync(urlPut, content);

        if (respuesta.IsSuccessStatusCode)
        {
            await DisplayAlert("Éxito", "Estudiante actualizado correctamente", "OK");
            Obtener(); // Refrescar lista
        }
        else
        {
            await DisplayAlert("Error", "No se pudo actualizar el estudiante", "OK");
        }
    }

    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        // Seleccionar un estudiante para eliminar (puedes reemplazar esto con lógica de selección)
        if (estud == null || estud.Count == 0)
        {
            await DisplayAlert("Error", "No hay estudiantes para eliminar", "OK");
            return;
        }

        var estudianteAEliminar = estud[0]; // Ejemplo: tomar el primer estudiante
        var urlDelete = $"{Url}?codigo={estudianteAEliminar.codigo}";
        var respuesta = await cliente.DeleteAsync(urlDelete);

        if (respuesta.IsSuccessStatusCode)
        {
            await DisplayAlert("Éxito", "Estudiante eliminado correctamente", "OK");
            Obtener(); // Refrescar lista
        }
        else
        {
            await DisplayAlert("Error", "No se pudo eliminar el estudiante", "OK");
        }
    }

}


