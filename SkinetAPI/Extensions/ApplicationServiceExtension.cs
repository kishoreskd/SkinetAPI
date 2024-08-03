using Infrastructure.Services.GenericRepositoryService;
using Infrastructure.Services.RepositoryService;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SkinetAPI.Errors;

namespace SkinetAPI.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddAutoMapper(typeof(Program).Assembly);

            //This order is important because it overrriting the controller [ApiController] attribute behaviours
            //for sending validation error consistently with help of ApiValidationErrorResponse custom object
            services.Configure<ApiBehaviorOptions>(options =>
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

            return services;
        }
    }
}
