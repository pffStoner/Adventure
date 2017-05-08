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

        protected override void Seed(Adventure.DbContext.ApplicationDbContext context)
        {
            Initializer.SeedRoles(context);
           // Initializer.SeedUser(context);
        }
    }
}
