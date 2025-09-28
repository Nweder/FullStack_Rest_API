// using System;
// using System.Net.Http;
// using System.Threading.Tasks;
// using System;
// using System.Net.Http;
// using System.Text;
// using System.Text.Json;
// using System.Threading.Tasks;

// class Program
// {
//     static async Task Main()
//     {
//         using var httpClient = new HttpClient
//         {
//             BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
//         };

//         await GetAsync(httpClient);
//     }

//     static async Task GetAsync(HttpClient httpClient)
//     {
//         // Egen enkel request-logg
//         var relative = "todos/3";
//         Console.WriteLine($"GET {httpClient.BaseAddress}{relative} HTTP/1.1");

//         using var request = new HttpRequestMessage(HttpMethod.Get, relative);
//         using var response = await httpClient.SendAsync(request);

//         response.EnsureSuccessStatusCode();

//         var jsonResponse = await response.Content.ReadAsStringAsync();
//         Console.WriteLine(jsonResponse);
//     }

//     //post method

//     static async Task PostAsync(HttpClient httpClient)
//     {
//         // Egen enkel request-logg
//         var NewTodo = new
//         {
//             UserID = 5,
//             title = "Mohamad .... ",
//             completed = false
//         };
//         var json = JsonSerializer.Serialize(NewTodo);
//         var content = new StringContent(json, Encoding.UTF8, "application/json");

//         using var response = await httpClient.PostAsync("todos", content);
//         response.EnsureSuccessStatusCode();

//         var reslut = await response.Content.ReadAsStringAsync();
//         Console.WriteLine(reslut);
//     }
// }


// // GET https://jsonplaceholder.typicode.com/todos/3 HTTP/1.1
// // {
// //   "userId": 1,
// //   "id": 3,
// //   "title": "fugiat veniam minus",
// //   "completed": false
// // }