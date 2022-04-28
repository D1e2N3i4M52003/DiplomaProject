using System.ComponentModel.DataAnnotations;
using Business.JSONModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.Net.Http;
using System.Net.Http.Json;

namespace PresentationLayer.Services
{
    public interface IAuthenticationService
    {
        Task Initialize();
        Task Login(AuthenticateRequest user);
        Task Logout();
    }

    public class AuthenticationService : IAuthenticationService
    {
        private HttpClient Http;
        private HttpResponseMessage response;
        private AuthenticateResponse res;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorage;
        private IJWTService _jwt;

        public String Token { get; private set; }

        public AuthenticationService(
            HttpClient http,
            NavigationManager navigationManager,
            ILocalStorageService localStorage,
            IJWTService jwt
        )
        {
            Http = http;
            _navigationManager = navigationManager;
            _localStorage = localStorage;
            response = new HttpResponseMessage();
            res = new AuthenticateResponse();
            _jwt = jwt;
        }

        public async Task Initialize()
        {
            Token = await _localStorage.GetItemAsync<String>("token");
        }

        public async Task Login(AuthenticateRequest user)
        {
            response = await Http.PostAsJsonAsync<AuthenticateRequest>("https://localhost:7269/Users/Authenticate", user);
            string r = await response.Content.ReadAsStringAsync();
            res = JsonConvert.DeserializeObject<AuthenticateResponse>(r);
            await _localStorage.SetItemAsync("token", res.Token);
            _navigationManager.NavigateTo("");
        }

        public async Task Logout()
        {
            string s = await _localStorage.GetItemAsync<string>("token");
                await _localStorage.RemoveItemAsync("token");
                _navigationManager.NavigateTo("");
        }
    }
}