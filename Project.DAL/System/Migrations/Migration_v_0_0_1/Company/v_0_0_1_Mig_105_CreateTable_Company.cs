using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.System
{
    /// <summary>
    /// Create company table
    /// </summary>
    [Migration(105, "Create company table")]
    public class v_0_0_1_Mig_105_CreateTable_Company : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();   
        }

        public override void Up()
        {
            Execute.EmbeddedScript("v_0_0_1_Mig_105_CreateTable_Company.sql");
        }
    }
}
