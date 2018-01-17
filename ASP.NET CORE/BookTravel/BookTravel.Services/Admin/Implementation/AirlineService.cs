using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using BookTravel.Data;
using BookTravel.Data.Models;
using BookTravel.Services.Admin.Models;

namespace BookTravel.Services.Admin.Implementation
{
    public class AirlineService : IAirlineService
    {
        private readonly TravelDbContext db;
        public AirlineService(TravelDbContext db)
        {
            this.db = db;
        }

        public bool AddAirline(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length > 50)
            {
                return false;
            }

            var airline = new Airline()
            {
                Name = name
            };

            this.db.Airlines.Add(airline);
            this.db.SaveChanges();
            return true;
        }

        public bool DeleteAirlines(int id)
        {
            var airline = this.db.Airlines
                .FirstOrDefault(d => d.Id == id);

            if (airline == null)
            {
                return false;
            }

            this.db.Airlines.Remove(airline);
            this.db.SaveChanges();
            return true;
        }

        public bool EditAirlines(int id, string name)
        {
            var airline = this.db.Airlines
                .FirstOrDefault(d => d.Id == id);

            if (airline == null || name == null)
            {
                return false;
            }

            airline.Name = name;

            this.db.SaveChanges();

            return true;
        }

        public IQueryable<AirlineServiceModel> GetAirlineById(int airlineId)
        {
            var airline = this.db.Airlines.Where(a => a.Id == airlineId)
                .ProjectTo<AirlineServiceModel>();
            if (airline == null)
            {
                return null;
            }

            return airline;
        }

        public IEnumerable<AirlineServiceModel> GetAirlines()
        {
            return this.db.Airlines
                        .ProjectTo<AirlineServiceModel>()
                        .ToList();
        }
    }
}
