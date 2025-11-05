using Bibliotekarz.Client.Extensions;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices().AddBlazoredLocalStorage();
builder
    .AddHttpClient()
    .AddApiClients()
    .AddJwtAuthentication();

await builder.Build().RunAsync();
