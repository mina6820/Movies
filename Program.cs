
using Instagram_Clone.Repositories;
using Microsoft.EntityFrameworkCore;
using Movies.Models;
using Movies.Repositories.CategoryRepo;
using Movies.Repositories.MovieRepo;
using TestingMVC.Repo;
using Movies.Repositories.ActroRepo;
using Movies.Repositories.DirectorRepo;
using Movies.Repositories.FavMovieRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Movies.Authentication;

namespace Movies
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
         
            builder.Services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });

            builder.Services.AddScoped<ICategoryRepository , CategoryRepository>();
            builder.Services.AddScoped<IMovieRepository , MovieRepository>();

            builder.Services.AddScoped<IActorRepository, ActorRepository>();
            builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();
            builder.Services.AddScoped<IFavMovieRepository, FavMovieRepository>();




            builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<Context>();

            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIss"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAud"],
                    IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecritKey"]))
                };
            });


            /////////////////////////// Builder /////////////////////////////////////////////
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
