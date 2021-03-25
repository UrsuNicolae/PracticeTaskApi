using Domain.IOptionClasses;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PracticeTaskServer.Extensions;
using System;
using System.Reflection;
using Data.Queries;
using Data.Handlers;

namespace PracticeTaskServer.Installers
{
    public class ToolsInstaller : IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSwaggerDocumentation();
            
            services.AddMediatR(typeof(QueryGetPaginatedPopularMovies).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(QueryGetPaginatedTopRatedMovies).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(QueryGetMovieByIdHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(SetMovieToFavoriteHandler).GetTypeInfo().Assembly);


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddOptions<OpenAPISettings>()
            .Bind(Configuration.GetSection("OpenAPISettings"))
            .ValidateDataAnnotations();
        }
    }
}
