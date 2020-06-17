using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.System
{
    /// <summary>
    /// Alter user table
    /// </summary>
    [Migration(103, "added email, dateJoined, dateOfBirth, Salary")]
    public class v_0_0_1_AlterTable_User_2 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();   
        }

        public override void Up()
        {
            Execute.EmbeddedScript("v_0_0_1_AlterTable_User_2.sql");
        }
    }
}
