using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model
{
    public class CompanyModel : BaseModel, ICompanyModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateFounded { get; set; }
        public Guid CompanyTypeId { get; set; }

        public CompanyTypeModel CompanyTypeModel { get; set; }


    }
}
