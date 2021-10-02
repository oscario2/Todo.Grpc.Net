using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Todo.Grpc.Api.Services;

namespace Todo.Grpc.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // grpc
            services.AddGrpc();
            services.AddGrpcHttpApi();

            // .net controllers
            services.AddControllers();

            // authentication
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    const string projectId = "net-auth-290ff";
                    options.Authority = "https://securetoken.google.com/" + projectId;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/" + projectId,
                        ValidateAudience = true,
                        ValidAudience = projectId,
                        ValidateLifetime = true,
                    };

                    // 1. https://firebase.google.com/docs/reference/rest/auth#section-refresh-token
                    options.Events = new JwtBearerEvents()
                    {
                        // 2. https://firebase.google.com/docs/reference/rest/auth#section-refresh-token
                        OnTokenValidated = async ctx =>
                        {
                            // https://stackoverflow.com/a/57204002
                            var name = ctx.Principal?.Claims.First(c => c.Type == "user_id").Value;
                            
                            // Get userManager out of DI
                            // var userManager = ctx.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();

                            // retrieves the roles that the user has
                            // var user = await userManager.FindByNameAsync(name);
                            // var userRoles = await userManager.GetRolesAsync(user);

                            // adds the role as a new claim 
                            // if (ctx.Principal?.Identity is ClaimsIdentity identity)
                            //    foreach (var role in userRoles)
                            //        identity.AddClaim(new Claim(ClaimTypes.Role, role));
                        }
                    };
                });

            // add [authorization] headers
            services.AddAuthorization();

            // add cors policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            // swagger
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Todo.API",
                    Description = "API for GRPC related tasks"
                });

                s.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT authorization the Bearer scheme (Example: 'Bearer b64de4==')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddGrpcSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowAll");
            
            app.UseEndpoints(route =>
            {
                // map net controllers
                route.MapControllers();

                // map proto controllers
                route.MapGrpcService<EchoService>();
                route.MapGrpcService<AdminService>();

                route.MapGet("/",
                    async context =>
                    {
                        await context.Response.WriteAsync(
                            "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                    });
            });
        }
    }
}