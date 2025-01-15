
using MarketApi.Data;
using MarketApi.IRepositories;
using MarketApi.Models;
using MarketApi.Repositories;
using MarketApi.Services;
using MarketApi.UnitWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace MarketApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    policy => policy
                        .WithOrigins("https://localhost:7074", "https://localhost:7190") // Allow this origin
                        .AllowAnyMethod()                        // Allow any HTTP methods
                        .AllowAnyHeader()                        // Allow any headers
                        .AllowCredentials());                    // Allow credentials if needed
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("MyConnection")
            ));

            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IFavoriteProductRepository, FavoriteProductRepository>();
            builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<UnitWorkApp, UnitWorkApp>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();


            builder.Services.AddScoped<AccountService, AccountService>();
            builder.Services.AddScoped<AccountAdminService, AccountAdminService>();
            builder.Services.AddScoped<AddressService, AddressService>();
            builder.Services.AddScoped<CartService, CartService>();
            builder.Services.AddScoped<CommentService, CommentService>();
            builder.Services.AddScoped<FavoriteProductService, FavoriteProductService>();
            builder.Services.AddScoped<ProductService, ProductService>();

            builder.Services.AddScoped<OrderService, OrderService>();

            builder.Services.AddScoped<PaymentService, PaymentService>();
            builder.Services.AddScoped<CategoryService, CategoryService>();

            builder.Services.AddScoped<UserService, UserService>();
            builder.Services.AddScoped<ProfileService, ProfileService>();

            builder.Services.AddHostedService<StartupTaskService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            // Build the app
            var app = builder.Build();

            // Configure the HTTP request pipeline
            app.UseCors("AllowSpecificOrigin");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enable serving static files from the `wwwroot` folder

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                RequestPath = "/sfroot",
                EnableDefaultFiles = true
            });
            //app.UseStaticFiles();

            // HTTPS redirection
            app.UseHttpsRedirection();

            // Routing
            app.UseRouting();

            // Authorization
            app.UseAuthorization();

            // Map controllers
            app.MapControllers();

            app.Run();
        }
    }
}