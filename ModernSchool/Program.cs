using System.Reflection.Metadata.Ecma335;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World my name is cristiano!");
app.MapGet("/getStudent",()=> "coucou");
app.Run();
