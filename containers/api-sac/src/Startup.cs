using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SGM.SAC.Infra.Crosscutting.Bootstrappings;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SGM.SAC.Api
{
    public class Startup
    {
        private readonly string _corsSpecifications = "_corsSpecifications";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddCors(options =>
            {
                options.AddPolicy(_corsSpecifications,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                             .AllowAnyHeader()
                                             .AllowAnyMethod();
                                  });
            });
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.DocumentFilter<BasePathFilter>();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "Citizen Service API",
                    Description = "SAC (Citizen Service) is an API for exposing property and rural taxes in the municipality of Bom Destino."
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                 {
                   new OpenApiSecurityScheme
                   {
                     Reference = new OpenApiReference
                     {
                       Type = ReferenceType.SecurityScheme,
                       Id = "Bearer"
                     }
                    },
                    new string[] { }
                  }
                });
            });

            ServicesBootStrap.RegisterServices(services, Configuration);

            // MediatR
            services.AddMediatR(typeof(Startup).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "Citizen Service API");               
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(_corsSpecifications);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class BasePathFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var path = "/sgm-sac-api";
#if DEBUG
            path= string.Empty;
#endif

            if (!string.IsNullOrEmpty(path))
            {
                swaggerDoc.Servers.Add(new OpenApiServer() { Url = path });
            }
        }
    }
}
