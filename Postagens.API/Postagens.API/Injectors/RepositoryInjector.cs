using Postagens.Application.Interfaces;
using Postagens.Application.Services;
using Postagens.Repository.Interfaces;
using Postagens.Repository.Repositories;

namespace Postagens.API.Injectors
{
    public static class RepositoryInjector
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IImagemRepository, ImagemRepository>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IImagemService, ImagemService>();

            return services;
        }
    }
}
