using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Project.DAL.System;

namespace Project.WebAPI.Controllers
{
    //TODO: move runner to the service layer and add try, catch so it returns status codes when it fails
    /// <summary>
    /// Controller for controling migrations
    /// </summary>
    [Route("api/maintenance")]
    [ApiController]
    public class MigrationController : ControllerBase
    {
        public IServiceProvider ServiceProvider { get; set; }
        public IInitial_Maintenance Initial_Maintenance { get; }

        public MigrationController(IServiceProvider serviceProvider, Initial_Maintenance initial_Maintenance)
        {
            ServiceProvider = serviceProvider;
            Initial_Maintenance = initial_Maintenance;
        }

        /// <summary>
        /// Trigger initial migration
        /// </summary>
        // POST: api/Maintenance
        [HttpPost("Initial")]
        public void InitialMigration()
        {
            Initial_Maintenance.Initiate();
        }

        /// <summary>
        /// Trigger migrations
        /// </summary>
        // POST: api/Maintenance
        [HttpPost("{version}")]
        public void MigrationByVersion(long version)
        {
            // Instantiate the runner
            var runner = ServiceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations to the version specified
            runner.MigrateUp(version);

            runner.ListMigrations();
        }

        /// <summary>
        /// Trigger migrations
        /// </summary>
        // POST: api/Maintenance
        [HttpPost("latest")]
        public void LatestMigration()
        {
            // Instantiate the runner
            var runner = ServiceProvider.GetRequiredService<IMigrationRunner>();

            // Execute all the migrations
            runner.MigrateUp();

            runner.ListMigrations();
        }


    }
}
