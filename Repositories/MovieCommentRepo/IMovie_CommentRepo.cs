using Movies.Models;

namespace Movies.Repositories.MovieCommentRepo
{
    public interface IMovie_CommentRepo
    {
        public void AddComment(string UserId, int MovieID, string comment);


        public List<MovieComment> GetAll_MovieComments(int MovieID);

        public List<MovieComment> GetAll_UserComments(string UserID);

        public void RemoveMovieComment(int MovieCommentID);

        public MovieComment GetSpecific_Comment(int MovieCommentID);

        public int GetNumberofComment_Movie(int MovieID);

        public int GetNumberofComment_User(string UserID);

    }
}