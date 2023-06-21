using LessonRegistration.Data.Models;
using LessonRegistration.Data.Services;
using LessonRegistration.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LessonRegistration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("variables.json");


            // Add services to the container.
            builder.Services.AddSingleton<IOpenIdApi, KeyCloakOpenIdApi>((services) =>
            {
                return new KeyCloakOpenIdApi(
                    builder.Configuration.GetBaseUri("KeyCloakRealm"),
                    builder.Configuration.GetKeyCloakSetting("client_secret"),
                    builder.Configuration.GetKeyCloakSetting("client_id"));
            });

            builder.Services.AddDbContext<AppDBContext>(optionsAction =>
            {
                var t = builder.Configuration.GetConnectionString("postgre");
                optionsAction.UseNpgsql(t);
            });
            builder.Services.AddTransient<Institutes>();
            builder.Services.AddTransient<Departments>();


            builder.Services.AddAuthentication(configureOptions =>
            {
                configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(async configureOptions =>
            {
                configureOptions.TokenValidationParameters =
                    await new JWTConfigurerRemote(builder.Configuration.GetBaseUri("KeyCloakRealm"))
                    .GetTokenValidationParameters();

                //configureOptions.Events.OnTokenValidated = async context =>
                //{
                //    //context.Principal
                //    var id = context.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                //    var cache = context.HttpContext.RequestServices.GetRequiredService<IDistributedCache>();
                //    var userId = cache.GetString(id);
                //    if (userId == null)
                //    {
                //        var dbContext = context.HttpContext.RequestServices.GetRequiredService<AppDBContext>();
                //        // db: find user by nameidentifier

                //        cache.SetString(id, userId, new DistributedCacheEntryOptions()
                //        {
                //            AbsoluteExpiration = context.SecurityToken.ValidTo
                //        });
                //    }
                //    ((ClaimsIdentity)context.Principal.Identity).AddClaim(new Claim("userId", userId));
                //};
            });

            builder.Services.AddAuthorization(builder =>
            {
            });

            builder.Services.AddControllers().AddJsonOptions(builder =>
            {
                builder.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDistributedMemoryCache();

            var app = builder.Build();

            //app.Use((content, next) =>
            //{
            //    Console.WriteLine("requested" + content.Request.Path.Value);

            //    return next(content);
            //});

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();

            //app.Use((context, next) =>
            //{
            //    var headers = context.Request.Headers;
            //    var tokens = headers.Authorization.ToString().Split(" ");
            //    if (tokens.Length < 2)
            //    {
            //        return next(context);
            //    }
            //    var token = tokens[1];

            //    var handler = new JwtSecurityTokenHandler();
            //    var jwtSecurityToken = handler.ReadJwtToken(token);

            //    return next(context);
            //});

            app.UseCors(options =>
            {
                //var clientUri = builder.Configuration.GetBaseUri("WebClient");
                //options.SetIsOriginAllowed((originURI) =>
                //    originURI == clientUri);
                //options.AllowAnyMethod();
                //options.WithOrigins(clientUri).AllowAnyHeader();
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            //app.Use((context, next) =>
            //{
            //    var id = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //    var dbContext = context.RequestServices.GetRequiredService<AppDBContext>();

            //    return next(context);
            //});


            app.MapControllers();

            app.Run();
        }
    }
}