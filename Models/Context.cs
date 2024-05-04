using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Authentication;

namespace Movies.Models
{
    public class Context : IdentityDbContext<AppUser>
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }
        public DbSet<ActorSeries> ActorSeries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryMovie> CategorieMovies { get; set; }
        public DbSet<CategorySeries> CategorieSeries { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<FavouriteMovie> FavouriteMovies { get; set; }
        public DbSet<FavouriteSeries> FavouriteSeries { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieComment> MovieComments { get; set; }
        public DbSet<MovieLike> MovieLikes { get; set; }
        public DbSet<MovieRating> movieRatings { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<SeriesComment> SeriesComments { get; set; }
        public DbSet<SeriesLike> SeriesLikes { get; set; }
        public DbSet<SeriesRating> SeriesRatings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Actor>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<FavouriteMovie>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<FavouriteSeries>().HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Director>().HasQueryFilter(e=>!e.IsDeleted);
            modelBuilder.Entity<Season>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Series>().HasQueryFilter(e => !e.IsDeleted);

        }
    }
}
