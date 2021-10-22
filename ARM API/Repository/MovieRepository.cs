using System;
using System.Collections.Generic;
using System.Linq;

namespace ARM_API
{
    public class MovieRepository:IMovie
    {

        public MovieRepository() 
        {
        
        }

        public IEnumerable<Movie> getMoviesByFilter(Dictionary<String, String> filter)
        {
            List<Movie> resultMovies=new List<Movie>();
            int filterCount = 0;
            foreach (var key in filter.Keys) 
            {
                if (key.ToString().Equals("title")) 
                {
                   resultMovies= (List<Movie>)getMoviesByTitle(filter[key]);
                }
                if (key.ToString().Equals("year"))
                {
                    if (resultMovies.Count == 0 && filterCount==0)
                    {//If no results from previous filter. We will run this only if the key is the first one.
                     //Any other case, we will filter existing results
                        resultMovies = (List<Movie>)getMoviesByYear(filter[key]);
                    }
                    else
                    {//If results exist from previous filter
                        resultMovies = (List<Movie>)getMoviesByYear(filter[key],resultMovies);
                    }
                }
                if (key.ToString().Equals("genre")) 
                {
                    if (resultMovies.Count == 0 && filterCount == 0)
                    {//If no results from previous filter. We will run this only if the key is the first one.
                     //Any other case, we will filter existing results
                        resultMovies = (List<Movie>)getMoviesByGenre(filter[key]);
                    }
                    else
                    {//If results exist from previous filter
                        resultMovies = (List<Movie>)getMoviesByGenre(filter[key], resultMovies);
                    }
                }
                filterCount++;
            }
            return resultMovies;
        }
        public IEnumerable<Movie> getToFiveMovies_ByRating()
        {
            var sorted = VisualDB.moviesList.OrderByDescending(movieAdapter => movieAdapter.movie.averageRating);
            //Create a collection of movies to return
            List<Movie> movies = new List<Movie>();
            foreach (MovieAdapter adapter in sorted) 
            {
                movies.Add(adapter.movie);
            }
            return movies.Take(5);
        }

        public IEnumerable<MovieRating> getToFiveMovies_ByRatingUser(String userName) 
        {
            List<MovieRating> result = new List<MovieRating>();
            //Get the movie adapters which contain the user.
            foreach(MovieAdapter adapter in VisualDB.moviesList)
            { 
                //IF the user has provided a rating, store/create a MovieRating object
                if (adapter.userRatings.ContainsKey(userName)) 
                {
                    result.Add(new MovieRating(adapter.movie,adapter.userRatings[userName],userName));
                }
            }
            //Order our movieRatings objects by user ratings values and get the first 5
            var sorted = result.OrderByDescending(movieRating=>movieRating.rating).Take(5);
            return sorted;
        }

        private IEnumerable<Movie> getMoviesByTitle(String filterTitle) 
        {
            List<Movie> resultMovies = new List<Movie>();
            foreach (var movieAdapter in VisualDB.moviesList) 
            {
                if (movieAdapter.movie.title.ToLower().Contains(filterTitle.ToLower())) 
                {
                    resultMovies.Add(movieAdapter.movie);
                }
            }
            return resultMovies;
        }

        private IEnumerable<Movie> getMoviesByYear(String filterYear)
        {
            List<Movie> resultMovies = new List<Movie>();
            foreach (var movieAdapter in VisualDB.moviesList)
            {
                if (movieAdapter.movie.yearOfRelease == Convert.ToInt32(filterYear))
                {
                    resultMovies.Add(movieAdapter.movie);
                }
            }
            return resultMovies;
        }
        //If there is any result from preivous filter we are calling this.
        private IEnumerable<Movie> getMoviesByYear(String filterYear,List<Movie> movies)
        {
            List<Movie> resultMovies = new List<Movie>();
            foreach (var movie in movies)
            {
                if (movie.yearOfRelease == Convert.ToInt32(filterYear))
                {
                    resultMovies.Add(movie);
                }
            }
            return resultMovies;
        }


        private IEnumerable<Movie> getMoviesByGenre(String filterGenre)
        {
            List<Movie> resultMovies = new List<Movie>();
            foreach (var movieAdapter in VisualDB.moviesList)
            {
                if (movieAdapter.movie.genre.ToLower().Contains(filterGenre.ToLower()))
                {
                    resultMovies.Add(movieAdapter.movie);
                }
            }
            return resultMovies;
        }

        private IEnumerable<Movie> getMoviesByGenre(String filterGenre,List<Movie> movies)
        {
            List<Movie> resultMovies = new List<Movie>();
            foreach (var movie in movies)
            {
                if (movie.genre.ToLower().Contains(filterGenre.ToLower()))
                {
                    resultMovies.Add(movie);
                }
            }
            return resultMovies;
        }


        public bool Add_UpdateUserRating(Dictionary<String, String> userRatingData) 
        {
            foreach (MovieAdapter adapter in VisualDB.moviesList) 
            {
                if (adapter.movie.title.ToLower().Equals(userRatingData["movie"].ToLower())) 
                {
                    //Update Existing
                    if (adapter.userRatings.ContainsKey(userRatingData["username"]))
                    {
                        adapter.userRatings[userRatingData["username"]] = Convert.ToDouble(userRatingData["rating"]);
                        double newRatingAvg = adapter.calculateAvgRating();
                        Console.WriteLine("New average rating:" + newRatingAvg);
                        return true;
                    }
                    else 
                    {//Add rating by new user
                        adapter.userRatings.Add(userRatingData["username"], Convert.ToDouble(userRatingData["rating"]));
                        return true;
                    }
                    
                }
            }
            return false;
        }
    }
}
