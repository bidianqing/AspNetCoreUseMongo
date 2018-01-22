using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreUseMongo.Mongo
{
    public static class MongoServiceCollectionExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, Action<MongoOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddOptions();
            services.Configure(setupAction);
            services.AddScoped(typeof(MongoRepository<>));

            return services;
        }
    }
}
