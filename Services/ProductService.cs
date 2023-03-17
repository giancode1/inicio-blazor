using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp1;

public class ProductService: IProductService
{
    // private porq se usan solamente dentro del servicio
    private readonly HttpClient client;

    private readonly JsonSerializerOptions options;
    //Recibimos como inyección de dependecias el HttpClient que ua se encuentra conf de manera global en la app de blazor
    //como toda app de .net lo recibimos en el constructor
    public ProductService(HttpClient httpClient)
    {
        this.client = httpClient;
        options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
        //PropertyNameCaseInsensitive: nos garantiza que pueda mapear cada una de las propiedades que viene del json al modelo creado
    }

    // los diferentes métodos:

    
    public async Task<List<Product>?> Get() // interrogacion para indicar q la lista puede estar vacia
    {// mejor esta implementacion a la q estaba.
        var response = await client.GetAsync("v1/products");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<List<Product>>(content, options);
    }

    public async Task Add(Product product)
    {
        var response = await client.PostAsync("v1/products", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task<Product> GetById(int productId)
    {
        var response = await client.GetAsync($"v1/products/{productId}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        var product = JsonSerializer.Deserialize<Product>(content, options);
        return product;
    }


    public async Task Update(int productId, Product product)
    {
        var response = await client.PutAsync($"v1/products/{productId}", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task Delete(int productId)
    {
        var response = await client.DeleteAsync($"v1/products/{productId}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
}

public interface IProductService
{
    Task<List<Product>?> Get();
    Task Add(Product product);
    Task<Product> GetById(int productId);
    Task Update(int productId, Product product);
    Task Delete(int productId);
}