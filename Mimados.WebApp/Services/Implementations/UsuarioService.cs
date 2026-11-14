using System.Net.Http.Json;
using Mimados.WebApp.Models;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Services.Implementations;

public class UsuarioService : IUsuarioService
{
    private const string PATH = "UsuarioRS";
    private readonly HttpClient _httpClient;
    private readonly ILogger<UsuarioService> _logger;

    public UsuarioService(HttpClient httpClient, ILogger<UsuarioService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<UsuarioPerfilModel?> ObtenerPerfilAsync(int idUsuario)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<UsuarioPerfilModel>($"{PATH}/perfil/{idUsuario}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el perfil del usuario {IdUsuario}", idUsuario);
            return null;
        }
    }

    public async Task<bool> ActualizarPerfilAsync(UsuarioPerfilModel perfil)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{PATH}/perfil", perfil);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar el perfil del usuario {IdUsuario}", perfil.Id);
            return false;
        }
    }

    public async Task<bool> EliminarCuentaLogicaAsync(int idUsuario)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{PATH}/eliminar/{idUsuario}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar la cuenta del usuario {IdUsuario}", idUsuario);
            return false;
        }
    }
}
