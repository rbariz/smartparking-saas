namespace SmartParking.Application.Common
{
    public sealed class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }

    }
}
