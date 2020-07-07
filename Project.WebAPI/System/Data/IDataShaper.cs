using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Project.WebAPI.System
{
    public interface IDataShaper<T>
    {
        List<object> ShapeData(IEnumerable<T> models, string fieldsString);
        object ShapeData(T model, string fieldsString);
    }
}
