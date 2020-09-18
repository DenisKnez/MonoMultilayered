using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Project.Common;
using Project.Common.Filters;
using Project.Common.System;
using Project.Model;
using Project.Service.Common;
using Project.WebAPI.System;
using System;
using System.Threading.Tasks;

namespace Project.WebAPI
{
    [Route("api/companys")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        public DataShaper<CompanyRestModel> DataShaper { get; set; }

        public CompanyController(IMapper mapper, ICompanyService companyService)
        {
            Mapper = mapper;
            CompanyService = companyService;
            DataShaper = new DataShaper<CompanyRestModel>();
        }

        public IMapper Mapper { get; }
        public ICompanyService CompanyService { get; }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyAsync(Guid id, string fields = "")
        {
            var company = await CompanyService.GetCompanyNoTrackingAsync(id);

            if (company != null)
            {
                CompanyRestModel restCompany = Mapper.Map<CompanyRestModel>(company);

                return Ok(DataShaper.ShapeData(restCompany, fields));
            }
            else
            {
                return NotFound("The company was not found");
            }
        }

        [HttpGet]
        public async Task<ApiResponse> GetCompanys([FromQuery] Parameters<CompanyFilter> companyIParameters, string fields = "")
        {
            var companys = await CompanyService.FindCompanysAsync(companyIParameters);

            var restCompanys = Mapper.Map<PagedList<CompanyRestModel>>(companys);

            object response = DataShaper.PaginatedShapeData(restCompanys, fields);

            return new ApiResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddCompanyAsync([FromBody]CompanyRestModel companyRestModel)
        {
            var company = Mapper.Map<CompanyModel>(companyRestModel);
            await CompanyService.AddCompanyAsync(company);

            if (company != null)
            {
                var restCompany = Mapper.Map<CompanyRestModel>(company);
                return Ok(restCompany);
            }
            else
            {
                return NotFound("The company was not found");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany([FromBody]CompanyRestModel companyRestModel)
        {
            var company = await CompanyService.GetCompanyNoTrackingAsync(companyRestModel.Id);
            var companyModel = Mapper.Map<CompanyModel>(companyRestModel);
            var anotherCompany = Mapper.Map(companyModel, company);

            await CompanyService.UpdateCompanyAsync(anotherCompany);

            if (company != null)
            {
                var restCompany = Mapper.Map<CompanyRestModel>(company);
                return Ok(restCompany);
            }
            else
            {
                return NotFound("The company was not found");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task DeleteCompany(Guid id)
        {
            await CompanyService.DeleteCompanyAsync(id);
        }
    }

    public class CompanyTypeRestModel : BaseRestModel
    {
        public string Name { get; set; }
        public string Abrv { get; set; }
    }

    public class CompanyRestModel : BaseRestModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateFounded { get; set; }
        public Guid CompanyTypeId { get; set; }

        public CompanyTypeRestModel CompanyType { get; set; }
    }
}