using Movies.Models;

namespace Movies.Repositories.SeriesCommentRepo
{
    public interface ISeries_CommentRepo
    {
        public void AddComment(string UserId, int SeriesID, string comment);


        public List<SeriesComment> GetAll_SeriesComments(int SeriesID);

        public List<SeriesComment> GetAll_UserComments(string UserID);


        public void RemoveSeriesComment(int SeriesCommentID);

        public SeriesComment GetSpecific_Comment(int SeriesCommentID);

        public int GetNumberofComment_Series(int SeriesID);


        public int GetNumberofComment_User(string UserID);




    }
}