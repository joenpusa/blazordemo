using System.Net.Http.Json;
using System.Text.Json;
using blazorappdemo.Models;

namespace blazorappdemo.Services;

public class CategoryService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions options;
    public CategoryService(HttpClient client)
    {
        this.client = client;
        options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
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

    public async Task Delete(int categoryId)
    {
        var response = await client.DeleteAsync($"v1/categories/{categoryId}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
    public async Task Put(Category category)
    {
        var response = await client.PutAsync($"v1/categories/{category.Id}", JsonContent.Create(category));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
}