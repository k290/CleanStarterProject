using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Presenters;

namespace MyMovieLibrary.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWeb(this IServiceCollection services)
        {
            services.AddTransient<IGetAllMoviesPresenter, GetAllMoviesPresenter>();
           
            return services;
        }
    }
}
