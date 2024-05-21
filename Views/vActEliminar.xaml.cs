using mperugachiAC6.Models;
using System.Net;

namespace mperugachiAC6.Views;

public partial class vActEliminar : ContentPage
{
    public vActEliminar(Estudiante datos)
    {
        InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre;
        txtApellido.Text = datos.apellido;
        txtEdad.Text = datos.edad.ToString();
    }
    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool confirmacion = await DisplayAlert("Confirmar", "¿Está seguro de actualizar al estudiante seleccionado?", "Sí", "No");

            if (confirmacion)
            {
                string updCodigo = txtCodigo.Text;
                string updNombre = txtNombre.Text;
                string updApellido = txtApellido.Text;
                string updEdad = txtEdad.Text;

                string updUrl = $"http://192.168.100.84/moviles/wsestudiantes.php?codigo=" +
                    $"{Uri.EscapeDataString(updCodigo)}" +
                    $"&nombre={Uri.EscapeDataString(updNombre)}" +
                    $"&apellido={Uri.EscapeDataString(updApellido)}" +
                    $"&edad={Uri.EscapeDataString(updEdad)}";

                using (HttpClient estudiante = new HttpClient())
                {
                    HttpResponseMessage respuesta = await estudiante.PutAsync(updUrl, null);
                    if (respuesta.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Éxito", "Estudiante actualizado correctamente.", "Aceptar");
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo actualizar el estudiante.", "Aceptar");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
        await Navigation.PushAsync(new vEstudiante());
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool confirmacion = await DisplayAlert("Confirmar", "¿Está seguro de eliminar al estudiante seleccionado?", "Sí", "No");

            if (confirmacion)
            {
                string delCodigo = txtCodigo.Text;

                string delUrl = $"http://192.168.100.84/moviles/wsestudiantes.php?codigo={Uri.EscapeDataString(delCodigo)}";

                using (HttpClient estudiante = new HttpClient())
                {
                    HttpResponseMessage respuesta = await estudiante.DeleteAsync(delUrl);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Éxito", "Estudiante eliminado correctamente.", "Aceptar");
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo eliminar el estudiante.", "Aceptar");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
        await Navigation.PushAsync(new vEstudiante());
    }
}