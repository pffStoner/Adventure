using Adventure.DbContext.Migrations;
using Adventure.Entities;
using Adventure.Entities.Common;
using Adventure.Entities.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public virtual IDbSet<Adventures> ITGigs { get; set; }

        public virtual IDbSet<EventTopic> EventTopics { get; set; }

        public virtual IDbSet<Location> Locations { get; set; }

        public virtual IDbSet<MailingAdress> MailingAddresses { get; set; }

        public virtual IDbSet<Venue> Venues { get; set; }


        public ApplicationDbContext()
        : base("Adventure-DevConnection", throwIfV1Schema: false)
        //: base("AdventureConnection", throwIfV1Schema: false)

        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
