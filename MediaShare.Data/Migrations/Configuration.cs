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

            //TO DO: Set to false
            this.AutomaticMigrationDataLossAllowed = true;
            ContextKey = "MediaShare.Data.MediaShareContext";
        }
    }
}