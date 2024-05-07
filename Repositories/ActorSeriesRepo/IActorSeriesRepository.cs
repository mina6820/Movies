﻿using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.ActorSeriesRepo
{
    public interface IActorSeriesRepository:IRepository<ActorSeries>
    {
        public bool GetActorAndSeries(int ActorId, int SeriesId);
        public bool IsActorInSeries(int ActorId, int SeriesId);

    }
}