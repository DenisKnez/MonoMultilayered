using System;

namespace Project.Model
{
    public interface ICompanyModel : IBaseModel
    {
        string Address { get; set; }
        Guid CompanyTypeId { get; set; }
        CompanyTypeModel CompanyTypeModel { get; set; }
        DateTime DateFounded { get; set; }
        string Email { get; set; }
        string Name { get; set; }
        string Phone { get; set; }
    }
}