using Project.Common;
using Project.Common.Filters;
using Project.Common.System;
using Project.DAL.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<IPagedList<Company>> FindCompanyAsync(IParameters<ICompanyFilter> companyParameters);
    }
}
