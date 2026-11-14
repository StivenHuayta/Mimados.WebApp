using Mimados.WebApp.Components;
using Mimados.WebApp.Models;
using Mimados.WebApp.Security;
using Mimados.WebApp.Services;
using Mimados.WebApp.Services.Implementations;
using Mimados.WebApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCookieAuth();

// URL Base del Backend Java REST leída de appsettings.json o variable de entorno
string apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:8080/MimadosRS/webresources/";
if (!apiBaseUrl.EndsWith("/"))
{
    apiBaseUrl += "/";
}

void ConfigureClient(HttpClient client)
{
    client.BaseAddress = new Uri(apiBaseUrl);
}

// Registro unificado de clientes HTTP consumiendo de la configuración
builder.Services.AddHttpClient<IAuthValidationService, AuthValidationService>(ConfigureClient);
builder.Services.AddHttpClient<IEmpleadoService, EmpleadoService>(ConfigureClient);
builder.Services.AddHttpClient<IHorarioLaboralService, HorarioLaboralService>(ConfigureClient);
builder.Services.AddHttpClient<ICitaService, CitaService>(ConfigureClient);
builder.Services.AddHttpClient<IDisponibilidadService, DisponibilidadService>(ConfigureClient);
builder.Services.AddHttpClient<IVeterinarioService, VeterinarioService>(ConfigureClient);
builder.Services.AddHttpClient<IMascotaService, MascotaService>(ConfigureClient);
builder.Services.AddHttpClient<IUsuarioService, UsuarioService>(ConfigureClient);
builder.Services.AddHttpClient<ISedeService, SedeService>(ConfigureClient);
builder.Services.AddHttpClient<IServicioService, ServicioService>(ConfigureClient);
builder.Services.AddHttpClient<ISedeServicioService, SedeServicioService>(ConfigureClient);
//nuevos
builder.Services.AddHttpClient<IReporteService, ReporteService>(ConfigureClient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

// Middlewares de Autenticación y Autorización
app.UseAuthentication();
app.UseAuthorization();

// Endpoints de autenticación (/auth/login y /auth/logout)
app.MapAuthEndpoints();

app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
