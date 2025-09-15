using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ModernSchool.DataAcces;
using ModernSchool;


var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("StudentsList"));
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<INoteService, Noteservice>();
builder.Services.AddTransient<IProfService, ProfService>();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "ModernSchoolAPI";
    config.Title = "ModernSchoolAPI V1";
    config.Version = "V1";
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "ModernSchoolAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

// All EntryPoints 
app.MapGroup("Api/v1")
.MapModernSchoolApiV1()
.WithTags();

app.Run();