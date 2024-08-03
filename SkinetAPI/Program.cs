using Infrastructure;
using Microsoft.EntityFrameworkCore;
using SkinetAPI.Extensions;
using SkinetAPI.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddDbContext<SkiNetDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SkiNet")));

//Custom
builder.Services.AddSwaggerDocumentation();
builder.Services.ApplicationServices();

var app = builder.Build();

//The below creating the data base and seeding the sample to the database if not exist.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<SkiNetDbContext>();
        await context.Database.MigrateAsync();
        await SeedDataService.SeedAsync(context, loggerFactory);

    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error accured during migration");
        throw;
    }
}


// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

//Execute when the no enpoinds are not matching.
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseAuthorization();

//Custom
app.UseSwaggerDocumentation();

app.MapControllers();





app.Run();


