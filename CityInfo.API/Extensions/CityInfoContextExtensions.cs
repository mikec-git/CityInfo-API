using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Extensions
{
    public static class CityInfoContextExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with that big park."
                },
                new City
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never really finished."
                },
                new City
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "The one with that big tower."
                }
            );

            modelBuilder.Entity<PointOfInterest>().HasData(
                 new
                 {
                     Id = 1,
                     Name = "Central Park",
                     Description = "The most visited urban park in the United States",
                     CityId = 1
                 },
                new
                {
                    Id = 2,
                    Name = "Empire state Building",
                    Description = "A 102-story skyscraper located in Midtown Manhattan",
                    CityId = 1
                },
                new
                {
                    Id = 3,
                    Name = "Cathedral of Our Lady",
                    Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans.",
                    CityId = 2
                },
                new
                {
                    Id = 4,
                    Name = "Antwerp Central Station",
                    Description = "The the finest example of railway architecture in Belgium.",
                    CityId = 2
                },
                new
                {
                    Id = 5,
                    Name = "Eiffel Tower",
                    Description = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel.",
                    CityId = 3
                },
                new
                {
                    Id = 6,
                    Name = "The Louvre",
                    Description = "The world's largest museum.",
                    CityId = 3
                }
            );
        }
    }
}
