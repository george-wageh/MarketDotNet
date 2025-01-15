using MarketApi.Data;
using Microsoft.AspNetCore.Identity;

namespace MarketApi.Services
{
    public class StartupTaskService: IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public StartupTaskService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Logic to execute on server start
            Console.WriteLine("Server is starting...");

            using (var scope = _serviceProvider.CreateScope())
            {
                // Access scoped services (e.g., DbContext, Repositories)
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await roleManger.CreateAsync(new IdentityRole() { Name = "User" });
                await roleManger.CreateAsync(new IdentityRole() { Name = "Admin" });
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Logic to execute on server shutdown
            Console.WriteLine("Server is stopping...");
            return Task.CompletedTask;
        }
    }
}
