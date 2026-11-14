using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Mimados.WebApp.Models;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Security;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/auth/login", async (HttpContext context, IAuthValidationService authService) =>
        {
            var form = await context.Request.ReadFormAsync();
            var usuario = form["usuario"].ToString();
            var contrasena = form["contrasena"].ToString();
            var rolSeleccionado = form["rol"].ToString(); // "Cliente", "Administrador", "Veterinario"
            var rememberMe = form["rememberMe"].ToString() == "on";

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                return Results.LocalRedirect("/login?error=Por%20favor%20complete%20todos%20los%20campos.");
            }

            var resultado = await authService.ValidarCredencialesAsync(usuario, contrasena);

            if (!resultado.Exito)
            {
                var errorMsg = Uri.EscapeDataString(resultado.MensajeError ?? "Credenciales inválidas.");
                return Results.LocalRedirect($"/login?error={errorMsg}");
            }

            // Validación de coincidencia con el rol seleccionado si el usuario especificó uno
            if (!string.IsNullOrWhiteSpace(rolSeleccionado))
            {
                if (rolSeleccionado.Equals("Administrador", StringComparison.OrdinalIgnoreCase) ||
                    rolSeleccionado.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    if (!resultado.EsAdmin && !resultado.Rol.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        return Results.LocalRedirect("/login?error=Esta%20cuenta%20no%20cuenta%20con%20privilegios%20de%20Administrador.");
                    }
                }
                else if (rolSeleccionado.Equals("Veterinario", StringComparison.OrdinalIgnoreCase))
                {
                    if (!resultado.Rol.Equals("Veterinario", StringComparison.OrdinalIgnoreCase) &&
                        !resultado.Rol.Equals("Admin", StringComparison.OrdinalIgnoreCase) &&
                        !resultado.EsAdmin)
                    {
                        return Results.LocalRedirect("/login?error=Esta%20cuenta%20no%20est%C3%A1%20registrada%20como%20Personal%20Veterinario.");
                    }
                }
                else if (rolSeleccionado.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
                {
                    if (!resultado.Rol.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
                    {
                        return Results.LocalRedirect("/login?error=Esta%20cuenta%20no%20est%C3%A1%20registrada%20como%20Cliente.");
                    }
                }
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, resultado.Email),
                new(ClaimTypes.Email, resultado.Email),
                new(ClaimTypes.Role, resultado.Rol),
                new("DisplayName", resultado.NombreCompleto),
                new("UsuarioId", resultado.UsuarioId.ToString()),
                new("Dni", resultado.Dni),
                new("EsAdmin", resultado.EsAdmin ? "true" : "false")
            };

            // Compatibilidad de roles cruzados
            if (resultado.EsAdmin || resultado.Rol.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
                claims.Add(new Claim(ClaimTypes.Role, "Veterinario"));
                claims.Add(new Claim(ClaimTypes.Role, "Empleado"));
            }
            else if (resultado.Rol.Equals("Veterinario", StringComparison.OrdinalIgnoreCase))
            {
                claims.Add(new Claim(ClaimTypes.Role, "Empleado"));
            }

            if (!string.IsNullOrEmpty(resultado.CodigoCmvp))
            {
                claims.Add(new Claim("CodigoCmvp", resultado.CodigoCmvp));
            }

            var principal = new ClaimsPrincipal(
                new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = rememberMe,
                    ExpiresUtc = rememberMe
                        ? DateTimeOffset.UtcNow.AddHours(8)
                        : DateTimeOffset.UtcNow.AddMinutes(30)
                });

            // Redirección hacia la sección correspondiente del sistema según rol seleccionado o rol devuelto
            string destino;
            if (!string.IsNullOrWhiteSpace(rolSeleccionado))
            {
                destino = rolSeleccionado switch
                {
                    "Administrador" => "/admin/inicio",
                    "Admin" => "/admin/inicio",
                    "Veterinario" => "/empleado/citas",
                    _ => "/cliente/inicio"
                };
            }
            else
            {
                destino = resultado.Rol.ToLowerInvariant() switch
                {
                    "admin" => "/admin/inicio",
                    "veterinario" => "/empleado/citas",
                    _ => "/cliente/inicio"
                };
            }

            return Results.LocalRedirect(destino);
        });

        endpoints.MapGet("/auth/logout", async (HttpContext context) =>
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Results.LocalRedirect("/login");
        });

        return endpoints;
    }
}
