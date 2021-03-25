using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Movie
    {
        public bool Adult { get; set; }

        public string Backdrop_Path { get; set; }
        
        public MovieCollection Belongs_to_collection { get; set; }

        public int Budget { get; set; }

        public List<Genres> Genres { get; set; }
        
        public string Homepage { get; set; }
        
        public int Id { get; set; }

        public string Imdb_Id { get; set; }
        
        public string Original_Language { get; set; }

        public string Original_Title { get; set; }

        public string Overview { get; set; }

        public double Popularity { get; set; }

        public string Poster_Path { get; set; }
        
        public List<Production_companies> Production_Companies { get; set; }

        public List<Production_countries> Production_Countries { get; set; }

        public DateTime Release_Date { get; set; }

        public int Revenue { get; set; }

        public int Runtime { get; set; }

        public List<Spoken_languages> Spoken_Languages { get; set; }

        public string Status { get; set; }

        public string Tagline { get; set; }

        public string Title { get; set; }

        public bool Video { get; set; }

        public double Vote_Average { get; set; }

        public int Vote_Count { get; set; }
    }
}
