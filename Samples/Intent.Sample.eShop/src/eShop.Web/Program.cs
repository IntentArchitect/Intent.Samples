using System;
using eShop.Web.Client;
using eShop.Web.Client.Services;
using eShop.Web.Common;
using eShop.Web.Components;
using eShop.Web.Components.Account;
using eShop.Web.Configuration;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AspNetCore.Program", Version = "1.0")]

namespace eShop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.ConfigureProblemDetails();
            builder.Services.AddClientServices(builder.Configuration);
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient("jwtClient", client => client.BaseAddress = builder.Configuration.GetValue<Uri?>("TokenEndpoint:Uri"));
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddApiAuthorization();
            builder.Services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthService, JwtAuthService>();
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(options =>
                                    {
                                        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                                    }).AddCookie();
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();
            builder.Services.AddMudServices();

            // IntentIgnore
            builder.Services.AddScoped<BasketState>();
            // IntentIgnore
            builder.Services.AddScoped<ProductImageUrlProvider>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseWebAssemblyDebugging();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}