﻿
namespace Project.Model
{
    public interface ICompanyTypeModel : IBaseModel
    {
        string Abrv { get; set; }
        string Name { get; set; }
    }
}