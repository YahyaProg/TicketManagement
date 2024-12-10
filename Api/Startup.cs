using Api.Util;
using Application;
using Core;
using Core.Entities;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace Api
{
    public class Startup
    {

        public IConfiguration Configuration { get; set; }
        private readonly string _swaggerEnable;
        /// private readonly ISettings _settings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _swaggerEnable = Configuration["SwaggerEnable"].ToString();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );
            });

            services.AddSingleton(Configuration);

            services.AddAuthentication(delegate (AuthenticationOptions options)
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(delegate (JwtBearerOptions jwt)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(Configuration.GetSection("JwtSecret").Value);
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(bytes),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });

            services.AddSwaggerGen(c =>
            {
                OpenApiSecurityScheme jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put *ONLY* your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
                c.DescribeAllParametersInCamelCase();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicketManagementApi", Version = "v1.0.0" });

                c.EnableAnnotations();
            });
            services.AddControllers()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            )
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddFluentValidationAutoValidation(x => x.DisableDataAnnotationsValidation = false);
            services.AddFluentValidationClientsideAdapters();

            services.AddDbContext<DBContext>(x =>
            {
                var con = Configuration.GetConnectionString("DefaultConnection");
                Console.WriteLine(con);
                x.UseSqlServer(con);
                x.EnableSensitiveDataLogging();
            });

            services.AddCore();
            services.AddInfrastructure();
            services.AddMemoryCache();

            services.AddApplication();
            services.AddHttpContextAccessor();

            //Forgery of X-Frame_Options not allowed
            services.AddAntiforgery(options =>
            {
                options.SuppressXFrameOptionsHeader = true;
            });

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete("Configure")]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            /// app.Map("/health", HealthMap);
            app.UseDeveloperExceptionPage();

            if (!string.IsNullOrEmpty(_swaggerEnable) && _swaggerEnable == "1")
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger(c =>
                {
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        swaggerDoc.Servers = new List<OpenApiServer> {
                            new() {Url = $"{httpReq.Scheme}://{httpReq.Host.Value}"},
                            new() { Url = $"{httpReq.Scheme}s://{httpReq.Host.Value}/Core" }
                        };
                    });

                });
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "Api v1");
                });
            }


            app.Use(async (context, next) =>
            {
                context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
                context.Response.Headers.Append("Content-Security-Policy", "default-src 'self';");
                await next();
            });


            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseMiddleware<CustomMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors("CorsPolicy");
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }

    }
}
