using Abstracciones.Modelos;
using Abstracciones.Reglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Marcas
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public VehiculoResponse marca { get; set; }


        public async Task<ActionResult> OnGet(Guid? IdMarca)
        {
            if (IdMarca == Guid.Empty)
            {
                return NotFound();
            }
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerMarca");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, IdMarca));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                marca = JsonSerializer.Deserialize<VehiculoResponse>(resultado, opciones);

            }
            return Page();
        }

        public async Task<ActionResult> OnPut()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string endpoint = _configuracion.ObtenerMetodo("APIEndPoints", "EditarMarca");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Put, endpoint);
            var respuesta = await cliente.PostAsJsonAsync<VehiculoResponse>(string.Format(endpoint, marca.Id), new VehiculoResponse
            {
                Marca = marca.Marca
            });
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./IndexMarcas");
        }
    }
}
