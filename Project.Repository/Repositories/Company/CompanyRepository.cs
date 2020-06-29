using Microsoft.EntityFrameworkCore;
using Project.Common;
using Project.Common.System;
using Project.DAL.EntityModels;
using Project.Repository.Common;
using Project.Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{

    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(IUnitOfWork uow) : base(uow)
        {

        }

        public Task<IPagedList<Company>> FindCompanyAsync(ICompanyParameters companyParameters)
        {
            IQueryable<Company> query = UnitOfWork.Context.Set<Company>();

            InitializeFilter(ref query, companyParameters);
            InitializeSorting(ref query, companyParameters.OrderBy);


             return base.FindAsync(companyParameters, query);
        }


        public void InitializeFilter(ref IQueryable<Company> query, ICompanyParameters companyParameters)
        {

            if (!string.IsNullOrWhiteSpace(companyParameters.Name))
            {
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{companyParameters.Name}%"));
            }

            InitializeBaseFilter(ref query, companyParameters);

        }
    }

}
