using Project.Common;
using Project.Common.Filters;
using Project.Common.System;
using Project.DAL.EntityModels;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<IPagedList<Company>> FindCompanyAsync(IParameters<CompanyFilter> companyParameters);
    }
}