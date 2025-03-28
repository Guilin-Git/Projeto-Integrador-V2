using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ProjetoIntegrador.Models;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Data;

namespace ProjetoIntegrador.Services { 
public class AuthService
{
    private readonly CustomAuthenticationStateProvider _authenticationStateProvider;
    private readonly ApplicationDbContext _context;  // Aqui está a variável para o contexto
    public AuthService(CustomAuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> Register(Usuario usuario)
    {
        // Verifique se o email já está em uso
        var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuario.Email);
        if (existingUser != null)
        {
            return false; // Email já registrado
        }

        // Adiciona o novo usuário ao banco de dados
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        // Crie a identidade do usuário
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email)
        }, "custom");

        var claimsPrincipal = new ClaimsPrincipal(identity);

        // Notifique a mudança no estado da autenticação
        var customAuthStateProvider = (CustomAuthenticationStateProvider)_authenticationStateProvider;
        customAuthStateProvider.MarkUserAsAuthenticated(claimsPrincipal);

        return true;
    }

    public async Task<bool> Login(string email, string password)
    {
        var user = AuthenticateUser(email, password);
        if (user != null)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Email, user.Email)
            }, "custom");

            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Chamando o método MarkUserAsAuthenticated do CustomAuthenticationStateProvider
            _authenticationStateProvider.MarkUserAsAuthenticated(claimsPrincipal);

            return true;
        }

        return false;
    }

    public void Logout()
    {
        var anonymous = new ClaimsIdentity();
        var userClaimsPrincipal = new ClaimsPrincipal(anonymous);

        // Atualiza o estado de autenticação para anônimo
        _authenticationStateProvider.MarkUserAsLoggedOut();
    }

    private Usuario AuthenticateUser(string email, string password)
    {
        // Lógica para buscar o usuário no banco de dados, validar a senha, etc.
        // Retorne o usuário se for válido ou nulo caso contrário.
        return new Usuario { Nome = "Test User", Email = email };  // Exemplo fictício
    }
 }
}