﻿using System.Collections.Generic;

namespace Product.Domain.Helpers;

public class PagedResult<T>
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int RecordNumber { get; set; }
    public int TotalPages { get; set; }
    public List<T> Items { get; set; } = [];
}