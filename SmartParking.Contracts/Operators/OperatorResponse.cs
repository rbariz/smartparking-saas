namespace SmartParking.Contracts.Operators
{
    public sealed record OperatorResponse(
    Guid Id,
    string Name,
    string ContactEmail,
    string ContactPhone,
    DateTime CreatedAtUtc
);
}
