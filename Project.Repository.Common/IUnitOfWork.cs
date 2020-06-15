using Project.DAL;
using Project.DAL.Context;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IUnitOfWork
    {
        DatabaseContext Context { get; set; }
        Task<int> CommitAsync();
        Task RollBackAsync();
    }
}