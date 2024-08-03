using Infrastructure;
using Infrastructure.Services.GenericRepositoryService;
using Infrastructure.Services.RepositoryService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SkinetAPI.Errors;
using SkinetAPI.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SkiNet API", Version = "V1" });
});

builder.Services.AddDbContext<SkiNetDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SkiNet")));
builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
builder.Services.AddAutoMapper(typeof(Program).Assembly);


//This order is important because it overrriting the controller [ApiController] attribute behaviours
//for sending validation error consistently with help of ApiValidationErrorResponse custom object
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
                     .Where(e => e.Value.Errors.Count > 0)
                     .SelectMany(e => e.Value.Errors)
                     .Select(e => e.ErrorMessage)
                     .ToArray();

        var errorResponse = new ApiValidationErrorResponse() { Errors = errors };

        return new BadRequestObjectResult(errorResponse);
    };
});

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=> { c.SwaggerEndpoint("/swagger/v1/swagger.json", "SkiNet API V1"); });
}

app.MapControllers();





app.Run();


