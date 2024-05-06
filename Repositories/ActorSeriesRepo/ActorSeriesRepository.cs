using Instagram_Clone.Repositories;
using Movies.Models;

namespace Movies.Repositories.ActorSeriesRepo
{
    public class ActorSeriesRepository : Repository<ActorSeries>, IActorSeriesRepository
    {
        private readonly Context context;
        public ActorSeriesRepository(Context _context) : base(_context)
        {
            this.context = _context;
        }

        public bool GetActorAndSeries(int ActorId , int SeriesId)
        {
            Actor actor = context.Actors.FirstOrDefault(a=>a.ID==ActorId);
            Series series = context.Series.FirstOrDefault(s=>s.Id==SeriesId);

            if (actor != null && series!=null)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public bool IsActorInSeries(int ActorId, int SeriesId)
        {
           bool isFOundInDataBAse= GetActorAndSeries(ActorId, SeriesId);

            if (isFOundInDataBAse)
            {
                ActorSeries actorSeries = context.ActorSeries.FirstOrDefault(a=>a.SeriesID==SeriesId&&a.ActorID==ActorId);
                if (actorSeries!=null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }else
            {
                return false;
            }
        }


        public bool DeleteActorFromSeries(int ActorId, int SeriesId)
        {
            bool IsFound = IsActorInSeries(ActorId, SeriesId);
            if (IsFound)
            {
                ActorSeries actorSeries = context.ActorSeries
                    .FirstOrDefault(a => a.ActorID == ActorId && a.SeriesID==SeriesId);
                context.ActorSeries.Remove(actorSeries);
                Save();
                return true;
            }
            else
            {
                return false;
            }

        }

        public ActorSeries GetActorSeries(int ActorId , int SeriesId)
        {
          return  context.ActorSeries.FirstOrDefault(a => a.SeriesID == SeriesId && a.ActorID == ActorId);
        }  
        

    }




}
