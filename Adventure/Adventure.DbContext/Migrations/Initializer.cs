using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
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

        internal static void SeedUser(ApplicationDbContext context)
        {
           
        }
    }
}