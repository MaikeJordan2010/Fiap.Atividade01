using Atividade.CrossCutting;
using Atividade01;
using Atividade01.Aplicacao._Contato.Comandos;
using Atividade01.Aplicacao._Contato.Consultas;
using Atividade01.Aplicacao.Services;
using Atividade01.Dominio.Sistemicas;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddInfraestrutura(builder.Configuration);

builder.Services.AddScoped<IContatoComandos, ContatoComandos>();
builder.Services.AddScoped<IContatoConsultas, ContatoConsultas>();

builder.Services.AddScoped<IObterMunicipios, ObterMunicipios>();


builder.Services.AddHttpClient(
               Configuration.HttpClientName,
               x =>
               {
                   x.BaseAddress = new Uri(Configuration.UrlAPIBrasil);
                   x.BaseAddress = new Uri("http://localhost:59730");
               }
    );


builder.Services.AddMudServices();

await builder.Build().RunAsync();
