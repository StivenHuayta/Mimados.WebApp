using BlazorAppMimados.Models;
using Mimados_WebApp.Components.Pages.Cliente;
using Mimados_WebApp.Models;
using Mimados_WebApp.Services.MascotaServices;
using Mimados_WebApp.Services.SedeService;
using System.Text.Json;

namespace Mimados_WebApp.Services.SedeService
{
    public class SedeServiceImpl: ISedeService
{
        private readonly HttpClient _http;

        public SedeServiceImpl(HttpClient http)
        {
            _http = http;
        }

        /*
        private const string BASE_URL = "http://localhost:8080/MimadosRS/webresources/SedeRS";
    private readonly HttpClient _httpClient;//comantario
    private readonly ILogger<SedeServiceImpl> _logger;
    public SedeServiceImpl(HttpClient httpClient, ILogger<SedeServiceImpl> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    */
        public async Task<Sede?> ObtenerSedePorIdAsync(int id)
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<Sede>>(
            $"SedeRS/{id}");

            return respuesta?.Data;

        }


        public async Task<List<Sede>> ObtenerSedesAsync()
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<List<Sede>>>(
                $"SedeRS/listarSede");

            return respuesta?.Data ?? new List<Sede>();
        }



        public async Task<bool> CrearSedeAsync(Sede sede)
        {
            var datos = new
            {
                nombre = sede.Nombre,
                direccion = sede.Direccion,
                telefono = sede.Telefono
                
            };


            var response = await _http.PostAsJsonAsync($"SedeRS/registrar", datos);

            if (!response.IsSuccessStatusCode)
                return false;

            var resultado = await response.Content
                .ReadFromJsonAsync<ApiResponse<object>>();

            return resultado?.Success == true;
        }

        public async Task<bool> ActualizarSedeAsync(Sede sede)
        {
            var datos = new
            {
                id = sede.Id,
                nombre = sede.Nombre,
                direccion = sede.Direccion,
                telefono = sede.Telefono,
                activo = sede.Activo
            };

            var response = await _http.PutAsJsonAsync($"SedeRS", datos);

            if (!response.IsSuccessStatusCode)
            {
                // los actualizadores devuelven un ok, no mensajes
                string errorMsg = await response.Content.ReadAsStringAsync();
                throw new Exception(string.IsNullOrEmpty(errorMsg) ? "No se pudo actualizar la sede." : errorMsg);
            }

            return true;
        }





        public class SedeJavaResponse
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public bool Activo { get; set; }

    }
        

}
}
