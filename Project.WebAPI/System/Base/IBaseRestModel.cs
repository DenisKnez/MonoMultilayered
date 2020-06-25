using System;

namespace Project.WebAPI.System
{
    public interface IBaseRestModel
    {
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
        Guid Id { get; set; }
        bool IsActive { get; set; }
    }
}