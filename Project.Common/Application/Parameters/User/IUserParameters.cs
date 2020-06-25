using System;

namespace Project.Common
{
    public interface IUserParameters : IParameters
    {
        string Name { get; set; }
    }
}