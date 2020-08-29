using APISorteio.Data;
using APISorteio.Data.Repositories;
using APISorteio.Data.Repositories.Interfaces;
using APISorteio.Models;
using APISorteio.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace APISorteio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddIdentity<Administrador, ApplicationRole>()
                .AddDefaultTokenProviders();

            services.AddTransient<IUserStore<Administrador>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
            services.AddTransient<IEnderecoRepository, EnderecoRepository>();
            services.AddTransient<IParticipanteRepository, ParticipanteRepository>();
            services.AddTransient<ISorteioRepository, SorteioRepository>();
            services.AddTransient<IParticipanteSorteioRepository, ParticipanteSorteioRepository>();
            services.AddScoped<SorteioService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
