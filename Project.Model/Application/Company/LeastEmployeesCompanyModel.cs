using System;

namespace Project.Model
{
    public class LeastEmployeesCompanyModel : ILeastEmployeesCompanyModel
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}