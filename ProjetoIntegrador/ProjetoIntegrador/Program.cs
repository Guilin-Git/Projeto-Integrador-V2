using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Components;
using ProjetoIntegrador.Data;
using ProjetoIntegrador.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });

builder.Services.AddTransient<EmailSender>();

// Configura��o do banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") +
                         ";Encrypt=True;TrustServerCertificate=True;"));

// Configura��o do Identity
builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configura��o da autoriza��o com pol�ticas de Roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
    options.AddPolicy("Medico", policy => policy.RequireRole("Medico"));
    options.AddPolicy("Recepcionista", policy => policy.RequireRole("Recepcionista"));
    options.AddPolicy("Paciente", policy => policy.RequireRole("Paciente"));
});

// Adiciona suporte ao Razor Components
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
app.UseAntiforgery();

// Adiciona autentica��o e autoriza��o ao pipeline HTTP
app.UseAuthentication();
app.UseAuthorization();

// Configura��o das rotas e Razor Components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Cria Roles no in�cio da aplica��o
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = ["Administrador", "Medico", "Recepcionista", "Paciente"];

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

app.Run();
