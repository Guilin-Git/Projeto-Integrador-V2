using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Components;
using ProjetoIntegrador.Data; // Certifique-se de que ApplicationDbContext está importado
using ProjetoIntegrador.Services; // Certifique-se de que o serviço de autenticação personalizado está importado

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") +
                         ";Encrypt=True;TrustServerCertificate=True;"));

// Configuração do serviço de autenticação personalizado
builder.Services.AddScoped<CustomAuthenticationStateProvider>(); // Registra o CustomAuthenticationStateProvider
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddScoped<AuthService>(); // Adiciona o AuthService que você criou

// Outros serviços necessários para o Razor Components e para seu app
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configuração do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// **Adicione app.UseAntiforgery() antes de app.UseRouting()**
app.UseAntiforgery();  // Middleware de anti-forgery

// Configuração das rotas e Razor Components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
