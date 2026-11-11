using BlazorAppMimados.Models;
using Mimados_WebApp.Components.Pages.Cliente;
using Mimados_WebApp.Models;
using Mimados_WebApp.Models.DTO;
using System.Buffers.Text;

namespace Mimados_WebApp.Services.SedeServicioService
{
    public class SedeServicioService:ISedeServicioService
{
        private readonly HttpClient _http;

        public SedeServicioService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<SedeServicioDTO>> ObtenerServiciosPorSedeAsync(int sedeid)
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<List<SedeServicioDTO>>>(
            $"SedeServicioRS/listarSedeServicios/{sedeid}");

            return respuesta?.Data;
        }

        /*
        public async Task<List<Servicio>> ObtenerServiciosDisponiblesAsync(int sedeid)
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<List<SedeServicioDTO>>>(
            $"SedeServicioRS/listarSedeServicios/{sedeid}");

            return respuesta?.Data;
        }
        */

        public async Task<bool> EliminarSedeServicioAsync(int sedeServicioId)
        {
           
            
                var response = await _http.DeleteAsync($"SedeServicioRS/{sedeServicioId}");
                return response.IsSuccessStatusCode;
            
        }
        public async Task<List<Servicio>> ObtenerServiciosDisponiblesAsync(int sedeid)
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<List<Servicio>>>(
            $"SedeServicioRS/listarServiciosDisponibles/{sedeid}");
            return respuesta?.Data;
        }
        public async Task<bool> CrearSedeServicioAsync(SedeServicio sedeServicio)
        {
            var datos = new
            {
                id_sede = sedeServicio.Sede.Id,
                id_servicio = sedeServicio.Servicio.Id
               

            };


            var response = await _http.PostAsJsonAsync("SedeServicioRS/registrar", datos);

            if (!response.IsSuccessStatusCode)
                return false;

            var resultado = await response.Content
                .ReadFromJsonAsync<ApiResponse<object>>();

            return resultado?.Success == true;

        }
    }
}
