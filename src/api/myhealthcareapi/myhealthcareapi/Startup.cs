using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using myhealthcareapi.AutoMappers;
using myhealthcareapi.DataAccesLayers;
using myhealthcareapi.Services;
using myhealthcareapi.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myhealthcareapi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "myhealthcareapi", Version = "v1" });
            });

            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IMedicService,MedicService>();



            var configuration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ClientMappingProfile());
            });


            services.AddSingleton(configuration.CreateMapper());

            services.AddMvc();


            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var result = new BadRequestObjectResult(new ValidationError { Code = 400, Error = ErrorMessagesCreator.CreateErrorMessagesForProperties(context.ModelState), Result = null });


                        return result;
                    };


                });

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {

                        context.HandleResponse();

                        var payload = new ValidationError
                        {
                            Code = 401,
                            Error = "Invalid token",
                            Result = null
                        };

                        var json = JsonConvert.SerializeObject(payload);

                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 401;

                        return context.Response.WriteAsync(json);
                    },
                    OnForbidden = context =>
                    {

                        var payload = new ValidationError
                        {
                            Code = 403,
                            Error = "Forbidden",
                            Result = null
                        };

                        var json = JsonConvert.SerializeObject(payload);

                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 403;

                        return context.Response.WriteAsync(json);
                    },
                };

            });



            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "myhealthcareapi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
