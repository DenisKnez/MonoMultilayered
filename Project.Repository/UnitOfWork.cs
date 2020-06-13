using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.DAL.Context;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public DatabaseContext Context { get; set; }

        //public IUserRepository UserRepository{ get; set; }

        public UnitOfWork(DatabaseContext context)
        {
            Context = context;
        }

        public async Task CommitAsync()
        {
            int numberOfChanges = await Context.SaveChangesAsync();
            Debug.WriteLine("Number of changes: " + numberOfChanges);
        }

        public async Task RollBackAsync()
        {
            await Context.DisposeAsync();
        }

    }
}
