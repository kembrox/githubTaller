using Abstracciones.Modelos;
using Abstracciones.Reglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Marcas
{
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public VehiculoResponse marcas { get; set; }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarMarca");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Post, endpoint);
            var respuesta = await cliente.PostAsJsonAsync(endpoint, marcas);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./IndexMarcas");
        }
    }
}
