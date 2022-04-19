using Async_Inn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Data
{
    public class AsyncInnDbContext : DbContext
    {

        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        
        public DbSet<Room> Rooms { get; set; }
        
        public DbSet<HotelRoom> HotelRoom { get; set; }
        
        public DbSet<Amenities> Amenities { get; set; }

        public DbSet<RoomAmenities> RoomAmenities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, but does nothing
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().HasData(
              new Hotel { Id = 1, Name = "Branch one", StreetAddress = "Street one", City = "Amman" , Phone = "123456789"   },
              new Hotel { Id = 2, Name = "Branch two", StreetAddress = "Street two", City = "Jarash", Phone = "123456789" },
              new Hotel { Id = 3, Name = "Branch three", StreetAddress = "Street three", City = "AlSalt", Phone = "123456789" }
            );
            modelBuilder.Entity<Room>().HasData(
             new Room { Id = 1, Name = "Room one", Layout = 1 },
             new Room { Id = 2, Name = "Room two", Layout = 2 },
             new Room { Id = 3, Name = "Room three" , Layout = 3 }
           );
            modelBuilder.Entity<Amenities>().HasData(
             new Amenities { Id = 1, Name = "Amenity one" },
             new Amenities { Id = 2, Name = "Amenity two" },
             new Amenities { Id = 3, Name = "Amenity three" }
           );
        }

    }
}
