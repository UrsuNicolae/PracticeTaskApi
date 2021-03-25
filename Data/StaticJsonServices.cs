using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Data
{
    public class StaticJsonServices : IStaticJsonService
    {
        private readonly IHostEnvironment _hostEnvironment;
        public StaticJsonServices(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public List<Movie> GetMoviesFromJson(string fileName)
        {
            var movies = new List<Movie>();
            using (StreamReader file = File.OpenText(GetRootPath(fileName)))
            {
                JsonSerializer serializer = new JsonSerializer();
                movies = (List<Movie>)serializer.Deserialize(file, typeof(List<Movie>)) ?? movies;
            }
            return movies;
        }

        private string GetRootPath(string fileName)
        {
            string[] folders = _hostEnvironment.ContentRootPath.Split('\\');
            var rootPath = new StringBuilder();
            for (int i = 0; i < folders.Length - 1; i++)
            {
                rootPath.Append(folders[i] + "\\");
            }
            rootPath.Append("Domain\\Static\\" + fileName);
            return rootPath.ToString();
        }
    }
}
