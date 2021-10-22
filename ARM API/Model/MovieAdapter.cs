using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ARM_API
{
    public class MovieAdapter 
    {
        public Dictionary<String, double> userRatings { get; set; }
        public Movie movie { get; set; }
        
        public MovieAdapter(Movie movie, Dictionary<String, double> userRatings)
        {
            this.movie = movie;
            this.userRatings = userRatings;
            movie.averageRating = calculateAvgRating();
        }

        public double calculateAvgRating() 
        {
            if (userRatings.Count>0) 
            {
                double sum = 0;
                foreach (var userRating in userRatings) 
                {
                    sum += userRating.Value;
                }
                return Math.Round(sum / userRatings.Count);                
            }
            throw new Exception("User rating dictionary is not Set.");
        }
    }
}
