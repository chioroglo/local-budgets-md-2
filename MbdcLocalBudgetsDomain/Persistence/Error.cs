namespace MbdcLocalBudgetsDomain.Persistence;

public class Error(string code, string message) : IEquatable<Error>
{
    public string Code { get; } = code;

    public string Message { get; } = message;

    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");

    public static implicit operator string(Error error) => error.Code;

    public bool Equals(Error? other) => other is not null &&
                                        string.CompareOrdinal(Code, other.Code) == 0 &&
                                        string.CompareOrdinal(Message, other.Message) == 0;

    public override bool Equals(object? obj) => obj is Error error && Equals(error);

    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Error? a, Error? b) => !(a == b);

    public override int GetHashCode() => HashCode.Combine(Code, Message);

    public override string ToString() => Code;
}