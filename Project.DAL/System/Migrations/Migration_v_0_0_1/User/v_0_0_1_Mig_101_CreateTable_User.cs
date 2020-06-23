using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.System
{
    /// <summary>
    /// Create user table
    /// </summary>
    [Migration(101, "Create user table")]
    public class v_0_0_1_Mig_101_CreateTable_User : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();   
        }

        public override void Up()
        {
            Execute.EmbeddedScript("v_0_0_1_Mig_101_CreateTable_User.sql");
        }
    }
}
