//* Estructura General para ejecutar el proyecto
using BlazorApp1;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Aqui hacemos la configuración de los servicios, es decir
//Todas las dependecias que inyectaremos en la app
var apiUrl = builder.Configuration.GetValue<string>("apiUrl");
builder.Services.AddBlazoredToast();  //Blazored Toast
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });

//vamos a realizar la inyección de dependecias para todos los componentes:
builder.Services.AddScoped<IProductService, ProductService>();  //para la interfaz IProductService, el objeto q se crea internamnete cuando se inyecte la dependencia es ProductService
builder.Services.AddScoped<ICategoryService, CategoryService>();  // lo mismo


await builder.Build().RunAsync();
