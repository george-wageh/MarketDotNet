using BlazorAppUser.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;

namespace BlazorAppUser.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddTransient<AuthorizationMessageHandler>();

            builder.Services.AddHttpClient("Client", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7247/");
            }).AddHttpMessageHandler<AuthorizationMessageHandler>();

            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<AccountService>();
            builder.Services.AddScoped<CartService>();
            builder.Services.AddScoped<AddressService>();

            builder.Services.AddScoped<CommentsService>();

            builder.Services.AddScoped<OrdersService>();
            builder.Services.AddScoped<PaymentsService>();

            builder.Services.AddScoped<CategoriesService>();

            builder.Services.AddScoped<FavoriteProductService>();

            builder.Services.AddScoped<ProfileService>();

            builder.Services.AddBlazoredLocalStorage();

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}