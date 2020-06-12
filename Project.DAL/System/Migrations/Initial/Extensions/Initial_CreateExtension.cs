using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.System
{
    /// <summary>
    /// Create citext and uuid-opps extensions
    /// </summary>
    [Migration(3, "Create citext and uuid-opps extensions")]
    public class Initial_CreateExtension : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Execute.EmbeddedScript("Initial_CreateExtension_citext.sql");
            Execute.EmbeddedScript("Initial_CreateExtension_uuid-ossp.sql");
        }
    }
}
