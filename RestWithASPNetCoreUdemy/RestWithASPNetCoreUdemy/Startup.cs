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
using Microsoft.Net.Http.Headers;
using Tapioca.HATEOAS;
using RestWithASPNetCoreUdemy.Hypermedia;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;

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
            ExecuteMigrations(connectionString);

            //configurações de autenticação
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();

            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                _configuration.GetSection("TokenConfigurations")
            )
            .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticationScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
            })

            //adicionando retorno XML para a API
            //comentei as linhas relacionadas a XML para garantir o retorno padrão 
            //da API para JSON.
            var s = services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                //--linha comentada retorno XML -->options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            });
            
            s.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //--linha comentada retorno XML --> s.AddXmlSerializerFormatters();


            //HATEOAS
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add( new PersonEnricher());
            services.AddSingleton(filterOptions);

            //para habilitar o versionamento, foi necessário adicionar via nuget:
            //dotnet add package Microsoft.AspNetCore.Mvc.Versioning
            services.AddApiVersioning();

            //adicionando swagger
            services.AddSwaggerGen(c =>{
                c.SwaggerDoc("v1", new Info
                                   {
                                        Title="RESTful API With ASP.NET Core 2.0",
                                        Version = "v1"
                                    });
            });

            //dependency injection
            services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
            services.AddScoped<IBookBusiness, BookBusinessImpl>();
            services.AddScoped<IUserRepository, UserRepositoryImpl>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        private void ExecuteMigrations(string connectionString)
        {
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

                    evolve.Repair();
                    evolve.Migrate();
                    
                }
                catch(Exception ex)
                {
                    _logger.LogCritical("Database migration faield.", ex);
                    throw;
                }
            }
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "Defaultapi",
                    template: "{controller=Values}/{id}"
                );
            });
        }
    }
}
