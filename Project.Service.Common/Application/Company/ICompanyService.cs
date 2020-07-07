using Project.Common;
using Project.Common.Filters;
using Project.Common.System;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{

    public interface ICompanyService
    {
        Task<ICompanyModel> GetCompanyNoTrackingAsync(Guid id);

        Task<ICompanyModel> AddCompanyAsync(ICompanyModel companyModel);

        Task<ICompanyModel> UpdateCompanyAsync(ICompanyModel companyModel);

        Task<int> DeactivateCompanyAsync(Guid id);

        Task<int> DeleteCompanyAsync(Guid id);

        Task<IPagedList<CompanyModel>> FindCompanysAsync(IParameters<ICompanyFilter> companyParameters);

    }



}
