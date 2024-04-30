﻿using System.ComponentModel.DataAnnotations.Schema;
using Movies.Authentication;

namespace Movies.Models
{
    public class MovieRating
    {
        public int Id { get; set; }

        // From 1 : 5
        public int Rate { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public AppUser User { get; set; }


        [ForeignKey("Movie")]
        public int MovieID { get; set; }
        public Movie Movie { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}