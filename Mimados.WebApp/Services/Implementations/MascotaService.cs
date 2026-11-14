using Mimados.WebApp.Services.Interfaces;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Mimados.WebApp.Models;


namespace Mimados.WebApp.Services.Implementations;

    public class MascotaService : IMascotaService
{
    private const string PATH = "mascotas";
    private readonly HttpClient _httpClient;
    private readonly ILogger<MascotaService> _logger;

    public MascotaService(HttpClient httpClient, ILogger<MascotaService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public string ObtenerUrlPdfHistorial(int mascotaId)
    {
        return new Uri(_httpClient.BaseAddress!, $"{PATH}/historial/{mascotaId}/pdf").ToString();
    }

    public async Task<Mascota?> ObtenerMascotaPorIdAsync(int mascotaId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{PATH}/{mascotaId}");
            if (!response.IsSuccessStatusCode) return null;

            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var d = JsonSerializer.Deserialize<MascotaJavaResponse>(jsonString, options);

            if (d == null) return null;

            return new Mascota
            {
                MascotaId = d.Id,
                Nombre = d.Nombre ?? "",
                Especie = d.Especie ?? "CANINO",
                Raza = d.Raza ?? "",
                Sexo = d.Sexo ?? "MACHO",
                FechaNacimiento = d.FechaNacimiento,
                PesoReferencial = d.PesoReferencial,
                Activo = d.Activo,
                ClienteId = d.Cliente?.Usuario?.Id ?? 0
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la mascota {Id}", mascotaId);
            return null;
        }
    }



    public async Task<List<Mascota>> ObtenerMascotasPorClienteAsync(int clienteId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{PATH}/cliente/{clienteId}");
            if (!response.IsSuccessStatusCode) return new List<Mascota>();

            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Deserializamos usando una clase DTO interna para atrapar el JSON anidado
            var dataJava = JsonSerializer.Deserialize<List<MascotaJavaResponse>>(jsonString, options);

            // Traducimos al modelo plano que usa tu UI de Blazor
            return dataJava?.Select(d => new Mascota
            {
                MascotaId = d.Id,
                Nombre = d.Nombre ?? "",
                Especie = d.Especie ?? "CANINO",
                Raza = d.Raza ?? "",
                Sexo = d.Sexo ?? "MACHO",
                FechaNacimiento = d.FechaNacimiento,
                PesoReferencial = d.PesoReferencial,
                Activo = d.Activo,
                ClienteId = d.Cliente?.Usuario?.Id ?? 0
            }).ToList() ?? new List<Mascota>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener mascotas del cliente {ClienteId}", clienteId);
            return new List<Mascota>();
        }
    }



    public async Task<bool> CrearMascotaAsync(Mascota mascota)
    {
        try
        {
            // Ensamblamos el JSON anidado exacto que espera GlassFish
            var payload = new
            {
                nombre = mascota.Nombre,
                especie = mascota.Especie,
                raza = mascota.Raza,
                sexo = mascota.Sexo,
                fechaNacimiento = mascota.FechaNacimiento?.ToString("yyyy-MM-dd"),
                pesoReferencial = mascota.PesoReferencial,
                cliente = new { usuario = new { id = mascota.ClienteId } }
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(PATH, content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear mascota");
            return false;
        }
    }



    public async Task<bool> ActualizarMascotaAsync(Mascota mascota)
    {
        try
        {
            var payload = new
            {
                id = mascota.MascotaId,
                nombre = mascota.Nombre,
                especie = mascota.Especie,
                raza = mascota.Raza,
                sexo = mascota.Sexo,
                fechaNacimiento = mascota.FechaNacimiento?.ToString("yyyy-MM-dd"),
                pesoReferencial = mascota.PesoReferencial,
                cliente = new { usuario = new { id = mascota.ClienteId } }
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(PATH, content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar mascota {Id}", mascota.MascotaId);
            return false;
        }
    }


    public async Task<bool> EliminarMascotaAsync(int mascotaId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{PATH}/{mascotaId}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar mascota {Id}", mascotaId);
            return false;
        }
    }


    public async Task<List<RegistroMedico>> ObtenerHistorialAsync(int mascotaId)
    {
        try
        {
            // Ajusta la URL si tu endpoint base es diferente
            
            var url = $"{PATH}/historial/{mascotaId}";

            var historial = await _httpClient.GetFromJsonAsync<List<RegistroMedico>>(url);
            return historial ?? new List<RegistroMedico>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el historial médico de la mascota {Id}", mascotaId);
            return new List<RegistroMedico>(); // Retorna lista vacía en caso de error
        }
    }





}

