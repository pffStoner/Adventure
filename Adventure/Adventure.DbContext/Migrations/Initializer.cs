using Adventure.Entities;
using Adventure.Entities.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project.Common;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Adventure.DbContext.Migrations
{
    internal class Initializer
    {
        internal static void SeedRoles(ApplicationDbContext context)

        {
        
            string[] roles =

            {

                "Manager",

                "Editor",

                "Buyer",

                "Seller",

                "Subscriber",

                "Guest",

                "Student",

                "Performer",

                "Publisher",

                "Blogger"

            };



            foreach (var role in roles)

            {

                var roleStore = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));



                if (!context.Roles.Any(r => r.Name == role))

                {

                    roleStore.Create(new IdentityRole(role));

                }

            }

        }



        internal static void SeedUsers(ApplicationDbContext context)

        {

            string anio = "anio";

            string mitko = "mitko";

            string owner = "Owner";

            string admin = "Administrator";

            var userRoleOwner = new IdentityRole { Id = new CustomId().ToString(), Name = owner };

            var userRoleAdmin = new IdentityRole { Id = new CustomId().ToString(), Name = admin };

            context.Roles.Add(userRoleOwner);

            context.Roles.Add(userRoleAdmin);



            var hasher = new PasswordHasher();



            var anio1 = new User

            {

                UserName = anio,

                PasswordHash = hasher.HashPassword("1"),

                Email = "mitko@abv.com",

                EmailConfirmed = true,

                SecurityStamp = new CustomId().ToString()

            };

            var mitko1 = new User

            {

                UserName = mitko,

                PasswordHash = hasher.HashPassword("aA199406."),

                Email = "mitko@abv.com",

                EmailConfirmed = true,

                SecurityStamp = new CustomId().ToString()

            };



            anio1.Roles.Add(new IdentityUserRole { RoleId = userRoleOwner.Id, UserId = anio1.Id });

            mitko1.Roles.Add(new IdentityUserRole { RoleId = userRoleAdmin.Id, UserId = mitko1.Id });

            context.Users.Add(anio1);

            context.Users.Add(mitko1);

        }



        internal static void SeedEventTopics(ApplicationDbContext context)

        {

            context.EventTopics.AddOrUpdate(

                    e => e.Name,

                    new EventTopic("Public"),

                    new EventTopic("Private")

                );

        }



        internal static void SeedVenues(ApplicationDbContext context)

        {

            context.Venues.AddOrUpdate(

                    v => v.PubName,

                    new Venue("UFO's Maniacs", 280),

                    new Venue("ITEligistic", 400),

                    new Venue("New World Order", 360),

                    new Venue("The Humanoid Era", 140),

                    new Venue("Renegate", 500)

                );

        }
    }
}