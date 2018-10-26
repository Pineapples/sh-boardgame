using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecretHitler.API.GameStates;
using SecretHitler.API.Hubs;
using SecretHitler.API.Repositories;
using SecretHitler.API.Services;
using SecretHitler.Models.Entities;
using Swashbuckle.AspNetCore.Swagger;

namespace SecretHitler.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<DataContext>(options =>
               options.UseSqlite("Filename=./sqlite_database.sqlite"));
            services.AddSignalR();
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });


            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            //    c.IncludeXmlComments(string.Format(@"{0}\SecretHitler.API.xml", System.AppDomain.CurrentDomain.BaseDirectory));
            //});

            RegisterStateTypes();
            RegisterDependencyInjection(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials());

            app.UseSignalR(routes =>
            {
                routes.MapHub<GameHub>("GameConnectionHub");
            });

            //app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //});

            app.UseMvc();
        }

        private void RegisterStateTypes()
        {
            GameStateTypeProvider.Register<IStateObject<ChoosePresidentGameState>>(GameState.ChoosePresident);
            GameStateTypeProvider.Register<IStateObject<ChooseChancellorGameState>>(GameState.ChooseChancellor);
            GameStateTypeProvider.Register<IStateObject<VoteForGovernmentGameState>>(GameState.VoteForGovernment);
            GameStateTypeProvider.Register<IStateObject<PresidentPolicyPickGameState>>(GameState.PresidentPolicyPick);
            GameStateTypeProvider.Register<IStateObject<ChancellorPolicyPickGameState>>(GameState.ChancellorPolicyPick);
        }

        private void RegisterDependencyInjection(IServiceCollection services)
        {
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IPolicyRepository, PolicyRepository>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IGameStateProvider, GameStateProvider>();

            //Gamestates
            services.AddTransient(typeof(IStateObject<ChoosePresidentGameState>), typeof(ChoosePresidentGameState));
            services.AddTransient(typeof(IStateObject<ChooseChancellorGameState>), typeof(ChooseChancellorGameState));
            services.AddTransient(typeof(IStateObject<VoteForGovernmentGameState>), typeof(VoteForGovernmentGameState));
            services.AddTransient(typeof(IStateObject<PresidentPolicyPickGameState>), typeof(PresidentPolicyPickGameState));
            services.AddTransient(typeof(IStateObject<ChancellorPolicyPickGameState>), typeof(ChancellorPolicyPickGameState));
        }
    }
}
