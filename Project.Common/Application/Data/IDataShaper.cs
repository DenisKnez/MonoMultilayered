﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Project.Common.Application
{
    public interface IDataShaper<T>
    {
        IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString);
        ExpandoObject ShapeData(T entity, string fieldsString);
    }
}
