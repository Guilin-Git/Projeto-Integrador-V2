using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _currentUser;

        public CustomAuthenticationStateProvider()
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var state = new AuthenticationState(_currentUser);
            return Task.FromResult(state);
        }

        // Método para atualizar o estado de autenticação
        public void NotifyAuthenticationStateChanged(AuthenticationState authenticationState)
        {
            // Notifica a mudança no estado da autenticação
            base.NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        public void MarkUserAsAuthenticated(ClaimsPrincipal user)
        {
            _currentUser = user;
            NotifyAuthenticationStateChanged(new AuthenticationState(_currentUser));  // Notifica a mudança do estado de autenticação
        }

        public void MarkUserAsLoggedOut()
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(new AuthenticationState(_currentUser));  // Notifica a mudança do estado de autenticação
        }
    }
}