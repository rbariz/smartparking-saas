namespace SmartParking.Contracts.Operators
{
    public sealed record OperatorCreateRequest(
    string Name,
    string ContactEmail,
    string ContactPhone
);
}
