﻿using Movies.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.DTOs
{
    public class SeasonsDTO
    {
        public int NumOfEpisodes { get; set; }

        public int SeriesID { get; set; }
       // public Series Series { get; set; }
    }
}