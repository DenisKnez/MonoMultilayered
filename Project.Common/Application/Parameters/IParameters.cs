using System;

namespace Project.Common.Application
{
    public interface IParameters
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
        bool IsActive { get; set; }
        Guid Id { get; set; }
    }
}