using E_learningAPI.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace E_learningAPI.Extentions
{
    public static class DataExtension
    {
        public static void AddDataExtension(this IServiceCollection services, string connectionString)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            //Repositories depedency
        }
    }
}
