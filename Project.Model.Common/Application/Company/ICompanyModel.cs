using Project.Model.Common.System;
using System;

namespace Project.Model.Common
{
    public interface ICompanyModel : IBaseModel
    {
        string Address { get; set; }
        Guid CompanyTypeId { get; set; }
        ICompanyTypeModel CompanyTypeModel { get; set; }
        DateTime DateFounded { get; set; }
        string Email { get; set; }
        string Name { get; set; }
        string Phone { get; set; }
    }
}