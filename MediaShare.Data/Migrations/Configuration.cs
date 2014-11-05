using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MediaShareContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            ContextKey = "MediaShare.Data.MediaShareContext";
        }
    }
}