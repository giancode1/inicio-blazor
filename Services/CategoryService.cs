using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp1;

public class CategoryService: ICategoryService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions options;
    public CategoryService(HttpClient client)
    {
        this.client = client;
        options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
    }

    public async Task<List<Category>?> Get()
    {
        var response = await client.GetAsync("v1/categories");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<List<Category>>(content, options);
    }

    public async Task Add(Category category)
    {
        var response = await client.PostAsync("v1/categories", JsonContent.Create(category));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task<Category> GetById(int categoryId)
    {
        var response = await client.GetAsync($"v1/categories/{categoryId}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<Category>(content, options);
    }

    public async Task Update(int caregoryId, Category category)
    {
        var response = await client.PutAsync($"v1/categories/{caregoryId}", JsonContent.Create(category));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task Delete(int categoryId)
    {
        var response = await client.DeleteAsync($"v1/categories/{categoryId}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
}

public interface ICategoryService
{
    // método:
    Task<List<Category>?> Get();
    Task Add(Category category);
    Task<Category> GetById(int categoryId);
    Task Update(int categoryId, Category category);
    Task Delete(int categoryId);
}