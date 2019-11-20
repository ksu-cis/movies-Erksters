using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies = null;


        public static List<Movie> All {
            get {
                if(movies == null)
                {
                    using (StreamReader file = System.IO.File.OpenText("movies.json"))
                    {
                        string json = file.ReadToEnd();
                        movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                    }
                }
                return movies;
            }
        }

        public static List<Movie> SearchAndFilter(string searchString, List<string> rating)
        {
            //Case 0: neither Search string or ratings
            if (searchString == null && rating.Count == 0) return All;

            List<Movie> results = new List<Movie>();
            foreach(Movie movie in movies)
            {
                //Case 1: Serach String AND ratings
                if (searchString != null && rating.Count > 0)
                {
                    if (movie.Title != null
                        && movie.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
                        && rating.Contains(movie.MPAA_Rating)
                    )
                    {
                        results.Add(movie);
                    }

                }
                
                //Case2: Search String only
                else if(searchString != null)
                {
                    if (movie.Title != null && movie.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase))
                    {
                        results.Add(movie);
                    }
                }
                
                //Case 3: ratings only
                else if(rating.Count > 0)
                {
                    if(rating.Contains(movie.MPAA_Rating)){
                        results.Add(movie);
                    }
                }
            }
            return results;
        }

        public static List<Movie> Search(List<Movie> movies, string term)
        {
            List < Movie > results=  new List<Movie>();
            foreach (Movie movie in movies)
            {
                if(movie.Title != null &&  movie.Title.Contains(term, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(movie);
                }
            }

            return results;
        }

        public static List<Movie> FilterByMPAA(List<Movie> movies, List<string> MPAA)
        {
            List<Movie> results = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (MPAA.Contains(movie.MPAA_Rating))
                {
                    results.Add(movie);

                }

            }
            return results;
        }

        public static List<Movie> FilterByMinIMDB(List<Movie> movies, float minRating)
        {
            List<Movie> results = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.IMDB_Rating != null && movie.IMDB_Rating >= minRating )
                {
                    results.Add(movie);

                }

            }
            return results;

        }

        public static List<Movie> FilterByMaxIMDB(List<Movie> movies, float maxRating)
        {
            List<Movie> results = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.IMDB_Rating != null && movie.IMDB_Rating <= maxRating)
                {
                    results.Add(movie);

                }

            }
            return results;
        }


    }
}
