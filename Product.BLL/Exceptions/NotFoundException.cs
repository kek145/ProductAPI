using System;

namespace Product.BLL.Exceptions;

[Serializable]
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}