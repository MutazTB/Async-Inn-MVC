using Async_Inn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Data
{
    public class AsyncInnDbContext : IdentityDbContext<ApplicationUser>
    {

        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        
        public DbSet<Room> Rooms { get; set; }
        
        public DbSet<HotelRoom> HotelRoom { get; set; }
        
        public DbSet<Amenities> Amenities { get; set; }

        public DbSet<RoomAmenities> RoomAmenities { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, and Identity needs it
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().HasData(
              new Hotel { Id = 1, Name = "Branch one", StreetAddress = "Street one", City = "Amman" , Phone = "123456789"   },
              new Hotel { Id = 2, Name = "Branch two", StreetAddress = "Street two", City = "Jarash", Phone = "123456789" },
              new Hotel { Id = 3, Name = "Branch three", StreetAddress = "Street three", City = "AlSalt", Phone = "123456789" }
            );
            modelBuilder.Entity<Room>().HasData(
             new Room { Id = 1, Name = "Room one", Layout = "1" },
             new Room { Id = 2, Name = "Room two", Layout = "2" },
             new Room { Id = 3, Name = "Room three" , Layout = "3" }
           );
            modelBuilder.Entity<Amenities>().HasData(
             new Amenities { Id = 1, Name = "Amenity one" },
             new Amenities { Id = 2, Name = "Amenity two" },
             new Amenities { Id = 3, Name = "Amenity three" }
           );
            modelBuilder.Entity<RoomAmenities>().HasKey(
                x => new { x.AmenitiesId, x.RoomId });

            modelBuilder.Entity<HotelRoom>().HasKey(
                x => new { x.HotelId, x.RoomNumber });

            //[Authorize(Roles = "Agent")]
            SeedRoles(modelBuilder, "District Manager", "create", "update", "delete");
            SeedRoles(modelBuilder, "Property Manager", "create", "update");
            SeedRoles(modelBuilder, "Agent", "create", "update", "delete");            
        }
        private int id = 1;
        private void SeedRoles(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()

            };
            modelBuilder.Entity<IdentityRole>().HasData(role);

            var RoleClaims = permissions.Select(permission =>
            new IdentityRoleClaim<string>
            {
                Id = id++,
                RoleId = role.Id,
                ClaimType = "permissions",
                ClaimValue = permission
            }
            ).ToArray();

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(RoleClaims);
        }
    }
}
