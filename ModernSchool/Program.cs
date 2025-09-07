using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ModernSchool.DataAcces;
using ModernSchool;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CustomDbContext>(opt => opt.UseInMemoryDatabase("StudentsList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
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
app.MapGroup("ModerSchool/v1")
.MapModernSchoolApiV1()
.WithTags();

app.Run();