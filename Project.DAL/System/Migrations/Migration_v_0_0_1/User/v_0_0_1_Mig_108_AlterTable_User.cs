using FluentMigrator;
using System;

namespace Project.DAL.System
{
    /// <summary>
    /// Alter user table
    /// </summary>
    [Migration(108, "set dateJoined to nullable and make the constraint for the columns salary, dateJoined and CompanyId")]
    public class v_0_0_1_Mig_108_AlterTable_User : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Execute.EmbeddedScript("v_0_0_1_Mig_108_AlterTable_User.sql");
        }
    }
}