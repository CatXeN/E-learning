using E_learningAPI.Application.Queries;
using MediatR;
using System.Reflection;

namespace E_learningAPI.Extentions
{
    public static class LibraryExtension
    {
        public static void AddLibraryExtension(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetAuthorizeQuery).GetTypeInfo().Assembly);
            services.AddSwaggerGen();
        }
    }
}
