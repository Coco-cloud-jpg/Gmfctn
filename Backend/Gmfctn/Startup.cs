using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Data_;
using Data_.Interfaces;
using AutoMapper;
using Data_.Profiles;
using Services;
using Data_.Entities;
using Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Threading.Tasks;

namespace Gmfctn
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
            services.AddDbContext<GmfctnContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection"), b => b.MigrationsAssembly("Gmfctn")));

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AchievementProfile());
                mc.AddProfile(new UserProfile());
            });


            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IAchievementService, AchievementService>();
            services.AddTransient<IUserService, UserService>();

            var TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("JWTToken")["TokenSecretString"])),
                ValidateLifetime = true,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero,
            };
            services.AddSingleton(TokenValidationParameters);
            services.AddAuthentication(Options =>
           {
               Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           }).AddJwtBearer(Options =>
           {
               Options.RequireHttpsMetadata = false;
               Options.SaveToken = true;
               Options.TokenValidationParameters = TokenValidationParameters;
           });

            services.AddTransient<GenericRepository<Achievement>>();
            services.AddTransient<GenericRepository<Role>>();
            services.AddTransient<GenericRepository<User>>();
            services.AddTransient<GenericRepository<Thank>>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IJwtAuthenticationService, JwtAuthenticationService>();
            services.AddTransient<IThankService, ThankService>();
            services.AddTransient<IProfileService, ProfileService>();

            services.AddControllers();
            services.AddCors();
            services.AddAuthorization();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gmfctn", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gmfctn v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
