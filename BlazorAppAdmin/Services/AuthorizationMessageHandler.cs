using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;

namespace BlazorAppAdmin.Services
{
    public class AuthorizationMessageHandler: DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager navigationManager;

        public AuthorizationMessageHandler(ILocalStorageService localStorage, NavigationManager navigationManager)
        {
            _localStorage = localStorage;
            this.navigationManager = navigationManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>("token");
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch (Exception ex)
            {

            }


            // Send the request and capture the response
            var response = await base.SendAsync(request, cancellationToken);

            // If the response is unauthorized, redirect to login
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await _localStorage.RemoveItemAsync("token");
                navigationManager.NavigateTo("login");
                throw new Exception("Auth");
            }

            return response;
        }
    }
}
