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

        public Task<IPagedList<Company>> FindCompanyAsync(IParameters<CompanyFilter> companyParameters)
        {
            IQueryable<Company> query = UnitOfWork.Context.Set<Company>();

            InitializeFilter(ref query, companyParameters);
            InitializeSorting(ref query, companyParameters.OrderBy);

            return base.FindAsync<IParameters<CompanyFilter>, CompanyFilter>(companyParameters, query);
        }

        public void InitializeFilter(ref IQueryable<Company> query, IParameters<CompanyFilter> companyParameters)
        {
            if (!string.IsNullOrWhiteSpace(companyParameters.Filter.Name))
            {
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{companyParameters.Filter.Name}%"));
            }

            InitializeBaseFilter<IParameters<CompanyFilter>, CompanyFilter>(ref query, companyParameters);
        }
    }
}