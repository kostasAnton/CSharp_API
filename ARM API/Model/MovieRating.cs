using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ARM_API
{
    public class MovieRating
    {
        public Movie movie { get; set; }
        public double rating { get; set; }
        public String username { get; set; }
        public MovieRating(Movie movie, double rating,String username)
        {
            this.movie = movie;
            this.rating = rating;
            this.username = username;
        }


    }
}
