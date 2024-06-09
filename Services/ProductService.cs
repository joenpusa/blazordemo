using System.Net.Http.Json;
using System.Text.Json;

namespace blazorappdemo.Services;

public class ProductService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions options;

    public ProductService (HttpClient httpClient, JsonSerializerOptions optionsJson)
    {
        client = httpClient;
        options = optionsJson;
    }

    /*
    * Metodo Get para conectarse a la api y obtener la lista de productos
    */
    public async Task<List<Product>?> Get()
    {
        var response = await client.GetAsync("/v1/products");
        return await JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync());
    }

    /*
    * Metodo Add para conectarse a la api y agregar un producto la lista
    */
    public async Task Add(Product product)
    {
        var response = await client.PostAsync("v1/products", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
    /*
    * Metodo Delete para conectarse a la api y eliminar un producto con base a su ID
    */
    public async Task Delete(int productId)
    {
        var response = await client.DeleteAsync($"v1/products/{productId}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    /*
    * Metodo Put para conectarse a la api y editar un producto la lista
    */
    public async Task Put(Product product)
    {
        var response = await client.PutAsync($"v1/products/{product.Id}", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
}