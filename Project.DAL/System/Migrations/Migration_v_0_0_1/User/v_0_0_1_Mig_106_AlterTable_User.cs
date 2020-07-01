using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.System
{
    /// <summary>
    /// Alter user table
    /// </summary>
    [Migration(106, "added company id column and company table fk constraint")]
    public class v_0_0_1_Mig_106_AlterTable_User : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();   
        }

        public override void Up()
        {
            Execute.EmbeddedScript("v_0_0_1_Mig_106_AlterTable_User.sql");
        }
    }
}
