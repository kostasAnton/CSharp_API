using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ARM_API
{
    public class Movie
    {
        public String title { get; set; }

        public String id { get; set; }

        public String genre { get; set; }

        public int yearOfRelease { get; set; }

        public int runningTime { get; set; }

        public double averageRating { get; set; }

        public Movie(String title, String id, String genre, int yearOfRelease, int runningTime)
        {
            this.title = title;
            this.genre = genre;
            this.yearOfRelease = yearOfRelease;
            this.runningTime = runningTime;
            this.id = id;
        }

        public String movieJson()
        {
            String jSonMovie = JsonConvert.SerializeObject(this);
            return jSonMovie;
        }



    }
}
