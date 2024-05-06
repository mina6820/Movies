using Instagram_Clone.Repositories;
using Movies.Models;
using TestingMVC.Repo;
using Instagram_Clone.Repositories;
using Microsoft.EntityFrameworkCore;
using Movies.Authentication;
using Movies.Models;

namespace Movies.Repositories.MovieCommentRepo
{
    public class MovieCommentRepository : Repository<MovieComment>, IMovie_CommentRepo
    {
        private readonly Context context;

        public MovieCommentRepository(Context _context) : base(_context)
        {
            context = _context;
        }

        //Adding comment to movie
        public void AddComment(string UserId, int MovieID, string comment)
        {
            AppUser user = context.Users.FirstOrDefault(u => u.Id == UserId);
            Movie movie = context.Movies.FirstOrDefault(m => m.Id == MovieID);

            if (movie != null && user != null)
            {
                var movieComment = new MovieComment
                {
                    MovieID = MovieID,
                    UserID = user.Id,
                    Text = comment
                };
                context.MovieComments.Add(movieComment);
                context.SaveChanges();
            }

        }

        //Get all comments of movie 
        public List<MovieComment> GetAll_MovieComments(int MovieID)
        {
            Movie movie = context.Movies.FirstOrDefault(m => m.Id == MovieID);

            if (movie != null)
            {
                List<MovieComment> movieComments =

                    context.MovieComments
                    .Where(mc => mc.MovieID == MovieID && mc.IsDeleted == false)
                    .Include(mc => mc.User)
                    .ToList();



                return movieComments;
            }
            else
            {
                return new List<MovieComment>();
            }


        }


        //Get all comments of User in movie 
        public List<MovieComment> GetAll_UserComments(string UserID)
        {
            AppUser user = context.Users.FirstOrDefault(u => u.Id == UserID);

            if (user != null)
            {
                List<MovieComment> movieComments =

                    context.MovieComments
                    .Where(mc => mc.UserID == UserID && mc.IsDeleted == false)
                    .Include(mc => mc.Movie)
                    .ToList();



                return movieComments;
            }
            else
            {
                return new List<MovieComment>();
            }


        }
        // Remove a comment movie
        public void RemoveMovieComment(int MovieCommentID)
        {


            MovieComment movieComment = context.MovieComments.FirstOrDefault(mc => mc.Id == MovieCommentID);
            if (movieComment != null)
            {
                movieComment.IsDeleted = true;
                context.SaveChanges();

            }
        }


        //Get specific Comment 
        public MovieComment GetSpecific_Comment(int MovieCommentID)
        {
            MovieComment comment = context.MovieComments
                .Include(mc => mc.Movie)
                .Include(mc => mc.User)
                .FirstOrDefault(mc => mc.Id == MovieCommentID && mc.IsDeleted == false);

            if (comment != null)
            {
                return comment;
            }
            return null;
        }

        //Get count of Comments in certain movie 
        public int GetNumberofComment_Movie(int MovieID)
        {
            Movie movie = context.Movies.FirstOrDefault(m => m.Id == MovieID);
            if (movie != null)
            {
                //int numberofComment = context.MovieComments
                //    .Where(mc => mc.MovieID == MovieID)
                //    .Count();
                int numberofComment =
                    context.MovieComments
                    .Count(mc => mc.MovieID == MovieID && mc.IsDeleted == false);
                return numberofComment;
            }
            else return 0;
        }

        //Get count of Comments in certain user 
        public int GetNumberofComment_User(string UserID)
        {
            AppUser user = context.Users.FirstOrDefault(u => u.Id == UserID);
            if (user != null)
            {
                //int numberofComment = context.MovieComments
                //    .Where(mc => mc.UserID == UserID)
                //    .Count();
                int numberofComment =
                    context.MovieComments
                    .Count(mc => mc.UserID == UserID && mc.IsDeleted == false);
                return numberofComment;
            }
            else return 0;
        }
    }
}
