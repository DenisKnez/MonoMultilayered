using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.System
{
    /// <summary>
    /// Create company type lookup table
    /// </summary>
    [Migration(104, "Create company type lookup table")]
    public class v_0_0_1_Mig_104_CreateLookup_CompanyType : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();   
        }

        public override void Up()
        {
            Execute.EmbeddedScript("v_0_0_1_Mig_104_CreateLookup_CompanyType.sql");
        }
    }
}
