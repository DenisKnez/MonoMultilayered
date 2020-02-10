﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    public class PageList<T> : IPageList<T>
    {

        public int TotalPages { get; set; }

        public int PageIndex { get; set; }

        public List<T> Items { get; set; }

    }
}
