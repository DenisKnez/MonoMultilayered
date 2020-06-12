using Project.DAL;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public DatabaseContext Context { get; set; }

        public UnitOfWork(DatabaseContext context)
        {
            Context = context;
        }

        public async void CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        public async void RollBackAsync()
        {
            await Context.DisposeAsync();
        }

    }
}
