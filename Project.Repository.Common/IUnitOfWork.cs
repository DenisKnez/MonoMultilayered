using Project.DAL;

namespace Project.Repository.Common
{
    public interface IUnitOfWork
    {
        DatabaseContext Context { get; set; }
        void CommitAsync();
        void RollBackAsync();
    }
}