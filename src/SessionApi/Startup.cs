using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using SessionApi.Models;
using SessionApi.Mutations;
using SessionApi.Queries;

namespace SessionApi
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; private set; }
        public IConfiguration Configuration { get; private set; }
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Environment = env;
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {

                var mongoConnectionUrl = new MongoUrl(Configuration.GetSection("ConnectionString:mongodb").Value);
                var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);

                var client = new MongoClient(mongoClientSettings);
                var database = client.GetDatabase("session_db");
                return database.GetCollection<Session>("session");
            });

            services.AddGraphQLServer()
                .AddQueryType<SessionQuery>()
                .AddMutationType<SessionMutation>()

                .EnableRelaySupport()
                .AddMongoDbFiltering()
                .AddMongoDbSorting()
                .AddMongoDbProjections();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
