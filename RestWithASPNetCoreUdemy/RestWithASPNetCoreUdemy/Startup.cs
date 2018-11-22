using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestWithASPNetCoreUdemy.Model.Context;
using RestWithASPNetCoreUdemy.Business;
using RestWithASPNetCoreUdemy.Repository;
using RestWithASPNetCoreUdemy.Repository.Generic;

namespace RestWithASPNetCoreUdemy
{
    public class Startup
    {
        private readonly ILogger _logger;        
        public IConfiguration _configuration{get;}
        public IHostingEnvironment _environment{get;}
         

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

            //configuração do Migration, se ambiente desenvolvimento, para iniciar a base de dados
            if (_environment.IsDevelopment())
            {
                try
                {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                    var evolve = new Evolve.Evolve("evolve.json", evolveConnection, msg => _logger.LogInformation(msg))
                    {
                        Locations = new List<string> {"db/migrations"},
                        //não apaga conteúdo, caso já existe alguma informação no BD
                        IsEraseDisabled = true,
                    };

                    evolve.Migrate();
                    
                }
                catch(Exception ex)
                {
                    _logger.LogCritical("Database migration faield.", ex);
                    throw;
                }
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //para habilitar o versionamento, foi necessário adicionar via nuget:
            //dotnet add package Microsoft.AspNetCore.Mvc.Versioning
            services.AddApiVersioning();

            //dependency injection
            services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
            services.AddScoped<IBookBusiness, BookBusinessImpl>();
            services.AddScoped<IPersonRepository, PersonRepositoryImpl>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
