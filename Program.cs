
using Instagram_Clone.Repositories;
using Microsoft.EntityFrameworkCore;
using Movies.Models;
using Movies.Repositories.CategoryRepo;
using Movies.Repositories.MovieRepo;
using TestingMVC.Repo;
using Movies.Repositories.ActroRepo;
using Movies.Repositories.DirectorRepo;

using Movies.Repositories.CategoryMovieRepo;

using Movies.Repositories.FavMovieRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Movies.Authentication;
using Movies.Repositories.SeriesRepo;
using Movies.Repositories.SeasonsRepo;
using Microsoft.OpenApi.Models;

using Movies.Repositories.MovieLikeRepo;
using Movies.Repositories.SeriesLikeRepo;
using Movies.Repositories.MovieCommentRepo;
using Movies.Repositories.SeriesCommentRepo;

using Movies.Repositories.FavSeriesRepo;
using Movies.Repositories.SeriesCategoryRepo;
using Movies.Repositories.ActorSeriesRepo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI;
using Movies.Repositories.MovieRatingRepo;


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
            builder.Services.AddScoped<ICategoryMovieRepository , CategoryMovieRepository>();

            builder.Services.AddScoped<IActorRepository, ActorRepository>();
            builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();
            builder.Services.AddScoped<IFavMovieRepository, FavMovieRepository>();
            builder.Services.AddScoped<IFavSeriesRepository, FavSeriesRepository>();

            builder.Services.AddScoped<ISeriesRepository, SeriesRepository>();
            builder.Services.AddScoped<ISeasonsRepo, SeasonsRepo>();


            builder.Services.AddScoped<IMovie_LikeRepo,MovieLikeRepository>();
            builder.Services.AddScoped<ISeries_LikeRepo,SeriesLikeRepo>();

            builder.Services.AddScoped<IMovie_CommentRepo,MovieCommentRepository>();
            builder.Services.AddScoped<ISeries_CommentRepo,SeriesCommentRepository>();


            builder.Services.AddScoped<ISeriesCategoryRepository,SeriesCategoryRepository>();
            builder.Services.AddScoped<IActorSeriesRepository, ActorSeriesRepository>();

            builder.Services.AddScoped<IMovieRatingRepository, MovieRatingRepository>();


            // IHttpContextAccessor
            //   builder.Services.AddScoped<IHttpContextAccessor>();




            builder.Services.AddIdentity<AppUser, IdentityRole>
                (options => options.SignIn.RequireConfirmedAccount = true)
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



            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                               .AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });




            //        builder.Services.AddControllers()
            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            //});

            /*-----------------------------Swagger PArt-----------------------------*/
            #region Swagger REgion
            //builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 5 Web API",
                    Description = " ITI Projrcy"
                });
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            #endregion
            //--------

            //************************************************

            /////////////////////////// Builder /////////////////////////////////////////////
            var app = builder.Build();


            // Roles
            // Access the built-in service provider
            //using (var scope = app.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //    var userManager = services.GetRequiredService<UserManager<AppUser>>();

            //    // Create roles if they don't exist
            //    if (!roleManager.RoleExistsAsync("Admin").Result)
            //    {
            //        roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
            //    }
            //    if (!roleManager.RoleExistsAsync("User").Result)
            //    {
            //        roleManager.CreateAsync(new IdentityRole("User")).Wait();
            //    }

            //    // Create default admin user if it doesn't exist
            //    var adminEmail = "admin@admin.com";
            //    var adminUserName = "admin";
            //    var defaultAdmin = userManager.FindByEmailAsync(adminEmail).Result;
            //    if (defaultAdmin == null)
            //    {
            //        var adminUser = new AppUser
            //        {
            //            UserName = adminUserName,
            //            Email = adminEmail,
            //        };
            //        var result = userManager.CreateAsync(adminUser, "Admin").Result;
            //        if (result.Succeeded)
            //        {
            //            userManager.AddToRoleAsync(adminUser, "Admin").Wait();
            //        }
            //    }
            //}

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseCors("AllowAllOrigins");

            app.MapControllers();

            app.Run();
        }
    }
}
