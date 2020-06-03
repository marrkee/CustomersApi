namespace Customers.WebApi
{
    using System.Text;
    using Customers.Persistence.Context;
    using Customers.Persistence.Extensions;
    using Customers.Persistence.Models.Mapping;
    using Customers.Persistence.Repositories;
    using Customers.Persistence.Repositories.Interfaces;
    using Customers.Persistence.Services;
    using Customers.Persistence.Services.Interfaces;
    using Customers.WebApi.Configuration;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
                services.AddSwaggerGen(c =>
                {
                  c.SwaggerDoc("v1", new OpenApiInfo()
                  {
                    Title = "My API",
                    Version = "v1",
                  });
                  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                  {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                  });
                  c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                   {
                     new OpenApiSecurityScheme()
                     {
                       Reference = new OpenApiReference()
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer",
                       },
                     },
                     new string[] { }
                   },
                  });
                });
                services.AddDbContext<CustomersContext>(
                options =>
                {
                     options.UseSqlServer(this.Configuration.GetConnectionString("CustomersContext"));
                }, ServiceLifetime.Transient);
                services.AddSingleton(AutoMapperConfiguration.ConfigureForWeb().CreateMapper());
                services.AddTransient<IManagersRepository, ManagersRepository>();
                services.AddTransient<ICustomerRepository, CustomersRepository>();
                services.AddTransient<ICustomersService, CustomersService>();
                services.AddTransient<IManagersService, ManagersService>();

                services.AddCors(options =>
                {
                    options.AddPolicy(
                        "CorsPolicy",
                        builder => builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
                });

                var appSettingsSection = this.Configuration.GetSection("AppSettings");
                services.Configure<AppSettings>(appSettingsSection);

                // configure jwt authentication
                var appSettings = appSettingsSection.Get<AppSettings>();
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);
                services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            // configure DI for application services
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IUserService, UserService>();

                services.AddControllers().AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseCors("CorsPolicy");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
                });
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
