using System.Net.Http.Headers;

namespace UserAPI.BlazorUI.Services
{
    public class JwtAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly TokenStorage _tokenStorage;

        public JwtAuthorizationMessageHandler(TokenStorage tokenStorage)
        {
            _tokenStorage = tokenStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenStorage.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
