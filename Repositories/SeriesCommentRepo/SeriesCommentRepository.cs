using Instagram_Clone.Repositories;
using Microsoft.EntityFrameworkCore;
using Movies.Authentication;
using Movies.Models;

namespace Movies.Repositories.SeriesCommentRepo
{
    public class SeriesCommentRepository : Repository<SeriesComment>, ISeries_CommentRepo
    {
        private readonly Context context;
        public SeriesCommentRepository(Context _context) : base(_context)
        {
            this.context = _context;

        }
        //Adding comment to Series
        public void AddComment(string UserId, int SeriesID, string comment)
        {
            AppUser user = context.Users.FirstOrDefault(u => u.Id == UserId);
            Series series = context.Series.FirstOrDefault(m => m.Id == SeriesID);

            if (series != null && user != null)
            {
                var seriesComment = new SeriesComment
                {
                    SeriesID = SeriesID,
                    UserID = user.Id,
                    Text = comment
                };
                context.SeriesComments.Add(seriesComment);
                context.SaveChanges();
            }

        }

        //Get all comments of Series 
        public List<SeriesComment> GetAll_SeriesComments(int SeriesID)
        {
            Series series = context.Series.FirstOrDefault(m => m.Id == SeriesID);

            if (series != null)
            {
                List<SeriesComment> seriesComments =

                    context.SeriesComments
                    .Where(mc => mc.SeriesID == SeriesID && mc.IsDeleted == false)
                    .Include(mc => mc.User)
                    .ToList();



                return seriesComments;
            }
            else
            {
                return null;
            }


        }


        //Get all comments of User in series 
        public List<SeriesComment> GetAll_UserComments(string UserID)
        {
            AppUser user = context.Users.FirstOrDefault(u => u.Id == UserID);

            if (user != null)
            {
                List<SeriesComment> seriesComments =

                    context.SeriesComments
                    .Where(mc => mc.UserID == UserID && mc.IsDeleted == false)
                    .Include(mc => mc.Series)
                    .ToList();



                return seriesComments;
            }
            else
            {
                return null;
            }


        }
        // Remove a comment series
        public void RemoveSeriesComment(int SeriesCommentID)
        {


            SeriesComment seriesComment = context.SeriesComments.FirstOrDefault(mc => mc.Id == SeriesCommentID);
            if (seriesComment != null)
            {
                seriesComment.IsDeleted = true;
                context.SaveChanges();

            }
        }


        //Get specific Comment 
        public SeriesComment GetSpecific_Comment(int SeriesCommentID)
        {
            SeriesComment comment = context.SeriesComments
                .Include(mc => mc.Series)
                .Include(mc => mc.User)
                .FirstOrDefault(mc => mc.Id == SeriesCommentID && mc.IsDeleted == false);

            if (comment != null)
            {
                return comment;
            }
            return null;
        }

        //Get count of Comments in certain series 
        public int GetNumberofComment_Series(int SeriesID)
        {
            Series series = context.Series.FirstOrDefault(m => m.Id == SeriesID);
            if (series != null)
            {
                //int numberofComment = context.SeriesComments
                //    .Where(mc => mc.SeriesID == SeriesID)
                //    .Count();
                int numberofComment =
                    context.SeriesComments
                    .Count(mc => mc.SeriesID == SeriesID && mc.IsDeleted == false);
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
                //int numberofComment = context.SeriesComments
                //    .Where(mc => mc.SeriesID == SeriesID)
                //    .Count();
                int numberofComment =
                    context.SeriesComments
                    .Count(mc => mc.UserID == UserID && mc.IsDeleted == false);
                return numberofComment;
            }
            else return 0;
        }
    }
}