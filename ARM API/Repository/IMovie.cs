using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM_API
{
    interface IMovie
    {
        public IEnumerable<Movie> getMoviesByFilter(Dictionary<String, String> filter);

        public IEnumerable<Movie> getToFiveMovies_ByRating();

        public IEnumerable<MovieRating> getToFiveMovies_ByRatingUser(String userName);

        public bool Add_UpdateUserRating(Dictionary<String,String> userRatingData);

    }
}
