using mperugachiAC6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace mperugachiAC6.Views;

public partial class vEstudiante : ContentPage
{
	private const string url = "http://192.168.100.84/moviles/wsestudiantes.php";
	private readonly HttpClient cliente = new HttpClient();
	private ObservableCollection<Estudiante> est;
	public vEstudiante()
	{
		InitializeComponent();
		ObtenerDatos();
	}
	public async void ObtenerDatos()
	{
		var content = await cliente.GetStringAsync(url);
		List<Estudiante> mostrar = JsonConvert.DeserializeObject<List<Estudiante>>(content);
		est = new ObservableCollection<Estudiante>(mostrar);
		listEstudiante.ItemsSource = est;
	}
    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new vAgregar());
    }
    private void listEstudiante_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		var objEstudiante = (Estudiante)e.SelectedItem;
		Navigation.PushAsync(new vActEliminar(objEstudiante));
    }
}