using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;


class Program
{
    static async Task Main()
    {
        using var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
        };

        // Testa GET
        await GetAsync(httpClient);

        // Testa POST
        await PostAsync(httpClient);
    }

    static async Task GetAsync(HttpClient httpClient)
    {
        var relative = "todos/3";
        Console.WriteLine($"GET {httpClient.BaseAddress}{relative} HTTP/1.1");

        using var request = new HttpRequestMessage(HttpMethod.Get, relative);
        using var response = await httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine(jsonResponse);
    }

    static async Task PostAsync(HttpClient httpClient)
    {
        var newTodo = new
        {
            userId = 5,
            title = "Mohamad .... ",
            completed = false
        };
        var json = JsonSerializer.Serialize(newTodo);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var response = await httpClient.PostAsync("todos", content);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine(result);
    }
}

// Testa ApiClient (hela CRUD)

// class Program
// {
//     static async Task Main()
//     {
//         using var http = new HttpClient { BaseAddress = new Uri("https://jsonplaceholder.typicode.com/") };
//         var api = new ApiClient(http);

//         var created = await api.CreateDataBaseAsync(new DataBase(0, 1, "Skapad via klient", false));
//         Console.WriteLine("POST -> " + JsonSerializer.Serialize(created));

//         var got = await api.GetTodoAsync(3);
//         Console.WriteLine("GET -> " + JsonSerializer.Serialize(got));

//         var updated = await api.UpdateDataBaseAsync(3, new DataBase(3, 1, "Uppdaterad titel", true));
//         Console.WriteLine("PUT -> " + JsonSerializer.Serialize(updated));

//         var success = await api.DeleteDataBaseAsync(3);
//         Console.WriteLine("DELETE -> " + success);
//     }
// }
