using AutoMapper.QueryableExtensions;
using BookTravel.Data;
using BookTravel.Data.Models;
using BookTravel.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookTravel.Services.Admin.Implementation
{
    public class AdminDestinationService : IAdminDestinationService
    {
        private readonly TravelDbContext db;
        public AdminDestinationService(TravelDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<DestinationServiceModel> GetDestinations()
        {
           return this.db.Destinations
                .ProjectTo<DestinationServiceModel>()
                .ToList();
        }

        public bool AddDestination(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length>100)
            {
                return false;
            }

            var destination = new Destination()
            {
                Name = name
            };

            this.db.Destinations.Add(destination);
            this.db.SaveChanges();
            return true;
        }

        public IQueryable<DestinationServiceModel> GetDestinationById(int destinationId)
        {
            var destination = this.db.Destinations
                .Where(d => d.Id == destinationId)
                .ProjectTo<DestinationServiceModel>();

            if (destination == null)
            {
                return null;
            }

            return destination;
        }

        public bool EditDestination(int id, string name)
        {
            var destination = this.db.Destinations
                .FirstOrDefault(d => d.Id == id);

            if (destination == null || name == null)
            {
                return false;
            }

            destination.Name = name;

            this.db.SaveChanges();

            return true;
        }

        public bool DeleteDestination(int id)
        {
            var destination = this.db.Destinations
                .FirstOrDefault(d => d.Id == id);

            if (destination == null)
            {
                return false;
            }

            this.db.Destinations.Remove(destination);
            this.db.SaveChanges();
            return true;
        }
    }
}
