using Microsoft.EntityFrameworkCore;
using Project.Common;
using Project.Common.Filters;
using Project.Common.System;
using Project.DAL.EntityModels;
using Project.Repository.Common;
using Project.Repository.Core;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public Task<IPagedList<Company>> FindCompanyAsync(IParameters<CompanyFilter> companyIParameters)
        {
            IQueryable<Company> query = UnitOfWork.Context.Set<Company>();

            InitializeFilter(ref query, companyIParameters);
            InitializeSorting(ref query, companyIParameters.OrderBy);

            return base.FindAsync<IParameters<CompanyFilter>, CompanyFilter>(companyIParameters, query);
        }

        public void InitializeFilter(ref IQueryable<Company> query, IParameters<CompanyFilter> companyIParameters)
        {
            if (companyIParameters.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(companyIParameters.Filter.Name))
                {
                    query = query.Where(x => EF.Functions.Like(x.Name, $"%{companyIParameters.Filter.Name}%"));
                }
            }

            InitializeIBaseFilter<IParameters<CompanyFilter>, CompanyFilter>(ref query, companyIParameters);
        }
    }
}