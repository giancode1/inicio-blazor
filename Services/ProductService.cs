using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp1;

public class ProductService
{
    // private porq se usan solamente dentro del servicio
    private readonly HttpClient client;

    private readonly JsonSerializerOptions options;
    //Recibimos como inyección de dependecias el HttpClient que ua se encuentra conf de manera global en la app de blazor
    //como toda app de .net lo recibimos en el constructor
    public ProductService(HttpClient httpClient, JsonSerializerOptions optionsJson)
    {
        client = httpClient;
        options = optionsJson;
    }

    // los diferentes métodos:

    public async Task<List<Product>?> Get() // interrogacion para indicar q la lista puede estar vacia
    {
        var response = await client.GetAsync("/v1/products");
        return await JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync());
    }

    public async Task Add(Product product)
    {
        var response = await client.PostAsync("/v1/products", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task Delete(int productId)
    {
        var response = await client.DeleteAsync($"/v1/products/{productId}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

}