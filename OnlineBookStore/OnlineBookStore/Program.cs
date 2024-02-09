using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineBookStore.Contexts;
using OnlineBookStore.Interfaces;
using OnlineBookStore.Models;
using OnlineBookStore.Repositories;
using OnlineBookStore.Services;
using System.Text;
namespace OnlineBookStore
{
    public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                             new string[] {}

                     }
                 });
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"])),
                    ValidateIssuerSigningKey = true
                };
            });

        #region CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("reactApp", opts =>
            {
                opts.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
            });
        });
        #endregion
        builder.Services.AddDbContext<OnlineBookAppContext>(opts =>
        {
            opts.UseSqlServer(builder.Configuration.GetConnectionString("BookAppapj"));
        });


        #region REPOSITORIES
        builder.Services.AddScoped<IRepository<string, User>, UserRepository>();
        builder.Services.AddScoped<IRepository<int, Book>, BookRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        #endregion



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseExceptionHandler("/Home/Error");
        }
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors("reactApp");
        app.UseAuthentication();
        app.MapControllers();
        app.UseAuthorization();
        /**app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");**/

        //app.MapRazorPages();

        app.Run();
    }
}
}