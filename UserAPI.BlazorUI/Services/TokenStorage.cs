using Blazored.LocalStorage;

namespace UserAPI.BlazorUI.Services
{
    public class TokenStorage
    {
        private readonly ILocalStorageService _localStorage;

        public TokenStorage(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task SaveTokenAsync(string token) =>
            await _localStorage.SetItemAsync("authToken", token);

        public async Task<string?> GetTokenAsync() =>
            await _localStorage.GetItemAsync<string>("authToken");
    }
}