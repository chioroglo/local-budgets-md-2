namespace MbdcLocalBudgetsDomain.Options;

public class BasicAuthOptions
{
    public required string Realm { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}