using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARM_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {




            setMoviesDictionary();
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ARM_API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ARM_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void setMoviesDictionary() 
        {

            //Set the first movie with its ratings
            Dictionary<String,double> usersList = new Dictionary<String, double>();
            usersList.Add("user1",4.1);
            usersList.Add("user2", 3.2);
            usersList.Add("user3", 2.3);
            usersList.Add("user4", 5.0);
            VisualDB.moviesList.Add(new MovieAdapter(new Movie("Terminator 1","id1" ,"Adventure", 2001, 3), usersList));

            //Set the second movie with its ratings
            usersList = new Dictionary<String, double>();
            usersList.Add("user1", 1.1);
            usersList.Add("user2", 1.2);
            usersList.Add("user3", 2.3);
            usersList.Add("user4", 4.4);
            VisualDB.moviesList.Add(new MovieAdapter(new Movie("Norbit","id2" ,"Comedy", 2002, 3), usersList));

            //Set the third movie with its ratings
            usersList = new Dictionary<String, double>();
            usersList.Add("user1", 1.1);
            usersList.Add("user2", 5.1);
            usersList.Add("user3", 4.3);
            VisualDB.moviesList.Add(new MovieAdapter(new Movie("Harry Potter","id3" ,"Drama", 2003, 2), usersList));

            //Set the fourth movie with its ratings
            usersList = new Dictionary<String, double>();
            usersList.Add("user1", 2.2);
            usersList.Add("user2", 4.3);
            usersList.Add("user3", 3.4);
            VisualDB.moviesList.Add(new MovieAdapter(new Movie("Rush Hour","id4" ,"Comedy", 2003, 2), usersList));

            //Set the fifth movie with its ratings
            usersList = new Dictionary<String, double>();
            usersList.Add("user1", 1.1);
            usersList.Add("user2", 3.1);
            usersList.Add("user3", 2.2);
            VisualDB.moviesList.Add(new MovieAdapter(new Movie("Terminator 2", "id5", "Adventure", 2005, 2), usersList));

            //Set the sixth movie with its ratings
            usersList = new Dictionary<String, double>();
            usersList.Add("user1", 2.1);
            usersList.Add("user2", 2.4);
            usersList.Add("user3", 3.5);
            usersList.Add("user4", 4.6);
            VisualDB.moviesList.Add(new MovieAdapter(new Movie("The Best Exotic Marigolg Hotel", "id6", "Comedy", 2008, 2), usersList));


            //Set the seveneth movie with its ratings
            usersList = new Dictionary<String, double>();
            usersList.Add("user1", 4.1);
            usersList.Add("user2", 4.2);
            usersList.Add("user3", 4.3);
            usersList.Add("user4", 5.0);
            VisualDB.moviesList.Add(new MovieAdapter(new Movie("Batman", "id7", "Adventure", 2009, 2), usersList));


            //Set the 8th movie with its ratings
            usersList = new Dictionary<String, double>();
            usersList.Add("user1", 1.1);
            usersList.Add("user2", 1.2);
            usersList.Add("user3", 2.3);
            usersList.Add("user4", 1.0);
            VisualDB.moviesList.Add(new MovieAdapter(new Movie("Disaster movie", "id8", "Comedy", 2007, 2), usersList));


            //Set the 9th movie with its ratings
            usersList = new Dictionary<String, double>();
            usersList.Add("user1", 2.1);
            usersList.Add("user2", 2.2);
            usersList.Add("user3", 2.3);
            usersList.Add("user4", 2.0);
            VisualDB.moviesList.Add(new MovieAdapter(new Movie("Random name movie", "id9", "Horror", 2008, 2), usersList));

        }
    }
}
