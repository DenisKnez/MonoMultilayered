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

        public UnitOfWork(DatabaseContext context)
        {
            Context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public async Task RollBackAsync()
        {
            await Context.DisposeAsync();
        }

    }
}
