using System;

namespace Project.Common.Application
{
    public interface IUserParameters : IParameters
    {
        string Name { get; set; }
    }
}