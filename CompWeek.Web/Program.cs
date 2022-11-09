using Blazored.LocalStorage;
using Blazored.SessionStorage;
using CompWeek.Web;
using CompWeek.Web.Business;
using CompWeek.Web.Business.Services;
using CompWeek.Web.Domain.Interfaces;
using CompWeek.Web.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var baseAddress = builder.Configuration.GetValue<string>("BaseUrl");

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();

await builder.Build().RunAsync();