using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public record DataBase(int Id, int UserID, string Name, bool Completed);

public class ApiClient
{
    private readonly HttpClient _http;  // skappar Dependency Injection: den skapas och inkapsling i klassen 
    private static readonly JsonSerializerOptions JsonOpts =  //Det är en klass som styr hur System.Text.Json ska: skriva ut & läsa in
    new(JsonSerializerDefaults.Web); // Skapar en ny JsonSerializerOptions med färdiga inställningar för webben (( SKlinad är storlek på bokstaver ))
    public ApiClient(HttpClient http) => _http = http; // Kort konstruktor: tar emot en HttpClient och sparar den i fältet _http.

    public async Task<DataBase?> GetTodoAsync(int id) // Get reques ((läsa!!!))
    {
        var res = await _http.GetAsync($"Todos/{id}");

        if (!res.IsSuccessStatusCode)
        {
            res.EnsureSuccessStatusCode(); // Error message 
        }
        else
        {
            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DataBase>(json, JsonOpts);
        }

        return null;
    }
    public async Task<DataBase?> CreateDataBaseAsync(DataBase input) //Post request ((Skapa!!))
    {
        var json = JsonSerializer.Serialize(input, JsonOpts); // göt objektet till json

        using var content = new StringContent(json, Encoding.UTF8, "application/json");  // Lägg JSON i en HTTP body

        var res = await _http.PostAsync("Todos", content); // Skicka POST till API:t

        if (!res.IsSuccessStatusCode)
        {
            res.EnsureSuccessStatusCode();
        }
        else
        {
            var body = await res.Content.ReadAsStringAsync(); // Läs svaret
            return JsonSerializer.Deserialize<DataBase>(body, JsonOpts); // Gör om JSON till DataBase-objekt
        }

        return null;

    }



    public async Task<DataBase?> UpdateDataBaseAsync(int Id, DataBase input) //PUT request ((Uppdatera!!))
    {
        var json = JsonSerializer.Serialize(input, JsonOpts); // gör objektet till json

        using var content = new StringContent(json, Encoding.UTF8, "application/json");  // Lägg JSON i en HTTP body

        var res = await _http.PutAsync($"Todos/{Id}", content); // Skicka PUT till API:t + id 

        if (!res.IsSuccessStatusCode)
        {
            res.EnsureSuccessStatusCode();
        }
        else
        {
            var body = await res.Content.ReadAsStringAsync(); // Läs svaret
            return JsonSerializer.Deserialize<DataBase>(body, JsonOpts); // Gör om JSON till DataBase-objekt
        }

        return null;

    }
    

    // public async Task<DataBase?> DeleteDataBaseAsync(int Id, int UserID, string Name, bool Completed, DataBase input) //Delete request ((ta bort!!))
    // {

    // }

}
