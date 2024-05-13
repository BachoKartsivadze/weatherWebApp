using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using WeatherConsoleApp;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var API_key = "8259c6f2bf41ebda9c6006062d0127ec";

//app.MapGet("/", () => "Hello World!");

//var todos = new List<Todo>();
var weather = new WeatherInfo();

/*app.MapPost("/todos", (Todo task) =>
{
    todos.Add(task);
    return TypedResults.Created("/todos/{id}", task);
});*/

app.MapGet("/weather/{city}", async (string city) =>
{
    var currWeather = new WeatherInfo();

    HttpClient client = new HttpClient();

    // create url
    string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={API_key}&units=metric";

    // Send a GET request asynchronously and read the response as a string
    var response = await client.GetAsync(url);
    var json = await response.Content.ReadAsStringAsync();

    // Deserialize the JSON response into your WeatherInfo class
     WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

    return Info;
});

app.MapPost("/weather", (WeatherInfo weather) =>
{
    //todos.Add(task);
    return TypedResults.Created("/weather/{city}", weather);
});

app.Run();

//public record Todo(int Id, string Name, DateTime DueDate, bool IsCompleted);
