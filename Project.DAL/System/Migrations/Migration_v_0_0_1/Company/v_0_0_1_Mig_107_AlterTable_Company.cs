using FluentMigrator;
using System;

namespace Project.DAL.System
{
    /// <summary>
    /// Alter company table
    /// </summary>
    [Migration(107, "Alter company table: add unique constaint to the name column")]
    public class v_0_0_1_Mig_107_AlterTable_Company : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Execute.EmbeddedScript("v_0_0_1_Mig_107_AlterTable_Company.sql");
        }
    }
}