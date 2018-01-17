using AutoMapper.QueryableExtensions;
using BookTravel.Data;
using BookTravel.Data.Models;
using BookTravel.Services.Admin.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookTravel.Services.Admin.Implementation
{
    public class AirportService : IAirportService
    {
        private readonly TravelDbContext db;
        public AirportService(TravelDbContext db)
        {
            this.db = db;
        }

        public bool AddAirport(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length > 50)
            {
                return false;
            }

            var airport = new Airport()
            {
                Name = name
            };

            this.db.Airports.Add(airport);
            this.db.SaveChanges();
            return true;
        }

        public bool DeleteAirport(int id)
        {
            var airport = this.db.Airports
                .FirstOrDefault(d => d.Id == id);

            if (airport == null)
            {
                return false;
            }

            this.db.Airports.Remove(airport);
            this.db.SaveChanges();
            return true;
        }

        public bool EditAirport(int id, string name)
        {
            var airport = this.db.Airports
                .FirstOrDefault(d => d.Id == id);

            if (airport == null || name == null)
            {
                return false;
            }

            airport.Name = name;

            this.db.SaveChanges();

            return true;
        }

        public IQueryable<AirportServiceModel> GetAirportById(int airportId)
        {
            var airport = this.db.Airports.Where(a => a.Id == airportId)
                .ProjectTo<AirportServiceModel>();
            if (airport == null)
            {
                return null;
            }

            return airport;

        }

        public IEnumerable<AirportServiceModel> GetAirports()
        {
           return this.db.Airports
                .ProjectTo<AirportServiceModel>()
                .ToList();
        }
    }
}
