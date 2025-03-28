using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Components;
using ProjetoIntegrador.Data; // Certifique-se de que ApplicationDbContext est� importado
using ProjetoIntegrador.Services; // Certifique-se de que o servi�o de autentica��o personalizado est� importado

var builder = WebApplication.CreateBuilder(args);

// Configura��o do banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") +
                         ";Encrypt=True;TrustServerCertificate=True;"));

// Configura��o do servi�o de autentica��o personalizado
builder.Services.AddScoped<CustomAuthenticationStateProvider>(); // Registra o CustomAuthenticationStateProvider
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddScoped<AuthService>(); // Adiciona o AuthService que voc� criou

// Outros servi�os necess�rios para o Razor Components e para seu app
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configura��o do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// **Adicione app.UseAntiforgery() antes de app.UseRouting()**
app.UseAntiforgery();  // Middleware de anti-forgery

// Configura��o das rotas e Razor Components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
