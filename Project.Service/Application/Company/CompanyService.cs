using AutoMapper;
using Project.Common;
using Project.Common.System;
using Project.DAL.EntityModels;
using Project.Model;
using Project.Repository.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{

    public class CompanyService : ICompanyService
    {
        public CompanyService(IMapper mapper, ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            CompanyRepository = companyRepository;
            UnitOfWork = unitOfWork;
        }

        public IMapper Mapper { get; }
        public ICompanyRepository CompanyRepository { get; }
        public IUnitOfWork UnitOfWork { get; }

        public async Task<ICompanyModel> GetCompanyNoTrackingAsync(Guid id)
        {
            var company = await CompanyRepository.GetAsyncNoTracking(id);
            var companyModel = Mapper.Map<CompanyModel>(company);

            return companyModel;
        }

        public async Task<IPagedList<CompanyModel>> FindCompanysAsync(ICompanyParameters companyParameters)
        {
            var companys = await CompanyRepository.FindCompanyAsync(companyParameters);
            return Mapper.Map<PagedList<Company>, PagedList<CompanyModel>>((PagedList<Company>)companys);
        }

        public async Task<ICompanyModel> AddCompanyAsync(ICompanyModel companyModel)
        {
            var company = Mapper.Map<Company>(companyModel);

            var addedCompany = await CompanyRepository.AddAsync(company);
            await UnitOfWork.CommitAsync();

            return Mapper.Map<ICompanyModel>(addedCompany);
        }

        public async Task<ICompanyModel> UpdateCompanyAsync(ICompanyModel companyModel)
        {
            var company = Mapper.Map<Company>(companyModel);
            var updatedCompany = CompanyRepository.Update(company);
            await UnitOfWork.CommitAsync();
            return Mapper.Map<CompanyModel>(updatedCompany);
        }

        public async Task<int> DeactivateCompanyAsync(Guid id)
        {
            await CompanyRepository.DeactivateAsync(id);
            return await UnitOfWork.CommitAsync();
        }

        public async Task<int> DeleteCompanyAsync(Guid id)
        {
            await CompanyRepository.Delete(id);
            return await UnitOfWork.CommitAsync();

        }
    }


}
