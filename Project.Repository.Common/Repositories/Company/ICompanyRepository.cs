using Project.Common;
using Project.Common.Filters;
using Project.Common.System;
using Project.DAL.EntityModels;
using Project.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<List<LeastEmployeesCompanyModel>> FindCompaniesWithLeastAmountOfEmployeesAsync(int numberOfCompanies);

        Task<IPagedList<Company>> FindCompanyAsync(IParameters<CompanyFilter> companyIParameters);
    }
}