namespace Adventure.DbContext.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Adventure.DbContext.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;//za da ne mi pozvolqva da triq neshta ot koito zavisqt drugi neshta

        }
        protected override void Seed(ApplicationDbContext context)

        {

            if (!context.Roles.Any())

            {

                Initializer.SeedRoles(context);

            }



            if (!context.Users.Any())

            {

                Initializer.SeedUsers(context);

            }



            Initializer.SeedEventTopics(context);

            Initializer.SeedVenues(context);


        }
    }
}
