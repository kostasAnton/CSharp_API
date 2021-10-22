using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Net;
using System.Web.Http;


namespace ARM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;

        //valid movie filters.
        private static readonly List<String> validFilters = new List<string>{ "title", "year", "genre" };
        private static readonly List<String> valid_Put = new List<string> { "username", "movie", "rating" };
        public MoviesController(ILogger<MoviesController> logger)
        {
            _logger = logger;
        }
        /*
         * I think an approach of managing those two cases in Get, fits us:
         * Get top 5 movies based on average rating if user name is blank
         * Get top 5 movies based on average rating using specific user name as filter
         */
        [HttpGet]
        public IEnumerable<Object> Get(String userName)
        {
            MovieRepository repo = new MovieRepository();
            if (!String.IsNullOrWhiteSpace(userName))
            {
                List<MovieRating> movies = new List<MovieRating>();
                movies = repo.getToFiveMovies_ByRatingUser(userName).ToList();

                //Set Status
                if (movies.Count > 0)
                {
                    Response.StatusCode = 200;
                    return movies;
                }
                else if (movies.Count == 0)
                {
                    Response.StatusCode = 404;
                    return movies;
                }
                else
                {
                    Response.StatusCode = 400;
                    return movies;
                }
            }
            else 
            {
                List<Movie> movies = new List<Movie>();
                movies = repo.getToFiveMovies_ByRating().ToList();

                //Set Status
                if (movies.Count > 0)
                {
                    Response.StatusCode = 200;
                    return movies;
                }
                else if (movies.Count == 0)
                {
                    Response.StatusCode = 404;
                    return movies;
                }
                else
                {
                    Response.StatusCode = 400;
                    return movies;
                }
            }
           
        }
        /*
         As per my understanding:
         Criteria for a valid dictionary:
        Keys count should be 3.We need the username, movie title and the input rating to update the ratings.
        And the key names should be the following:
        username,movie,rating
        rating should be a number between 1 and 5
         */

        [HttpPut]
        public void Put(Dictionary<String, String> userRatingData)
        {
            //Invalid count of criteria
            if (userRatingData.Count!=3) 
            {
                Response.StatusCode = 400;
                return;
            }
            foreach (var key in userRatingData.Keys) 
            {
                if (!valid_Put.Contains(key)) 
                {//Invalid name of criteria
                    Response.StatusCode = 400;
                    return;
                }
            }
            if (Convert.ToInt32(userRatingData["rating"]) < 1 || Convert.ToInt32(userRatingData["rating"]) > 5)
            {// if it's not between 1 and 5(as per the instructions)
                Response.StatusCode = 400;
                return;
            }



            MovieRepository repository = new MovieRepository();
            bool resultUpdate = repository.Add_UpdateUserRating(userRatingData);
            if (resultUpdate)
            {
                Response.StatusCode = 200;
                return;
            }
            else 
            {
                Response.StatusCode = 404;
                return;
            }
        }

        /*
         Criteria count should be between 1-3
         The parameters should be the correct ones and not random parameters names

         */

        [HttpPost]
        public IEnumerable<Movie> Post(Dictionary<String,String> filter)
        {

            MovieRepository repo = new MovieRepository();
            List<Movie> result =  new List<Movie>();
            //invalid count of parameters
            if (filter.Count==0 || filter.Count>3) 
            {
                //set the status code to 400
                Response.StatusCode = 400;
                return result;

            }
            //Invalid parameter in http body
            foreach (var key in filter.Keys) 
            {
                if (!validFilters.Contains(key)) 
                {
                    //set the status code to 400
                    Response.StatusCode = 400;
                    return result;
                }

            }
            result=(List<Movie>)repo.getMoviesByFilter(filter);
            if (result.Count>0) 
            {
                Response.StatusCode = 200;
                return result;
            }
            //Set the status code to 404-Not found
            Response.StatusCode = 404;
            return result;
        
        }
    }
}
