﻿namespace Product.Domain.Helpers;

public class QueryParameters
{
    public int StartIndex { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; } = 15;
}