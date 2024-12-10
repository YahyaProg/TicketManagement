using System;

public class BadRequestException : Exception
{
    public int ErrorCode { get; set; }
    public BadRequestException()
    { }

    public BadRequestException(int errorCode)
    {
        ErrorCode = errorCode;
    }

    public BadRequestException(string message)
        : base(message)
    {
    }

    public BadRequestException(string message, Exception inner)
        : base(message, inner)
    {
    }
}