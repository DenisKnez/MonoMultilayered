using System;

namespace Project.Model
{
    public interface ILeastEmployeesCompanyModel
    {
        string Name { get; set; }
        public Guid Id { get; set; }
    }
}