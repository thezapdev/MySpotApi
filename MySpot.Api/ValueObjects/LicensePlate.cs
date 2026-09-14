namespace MySpot.Api.ValueObjects;

public record LicensePlate
{
    public string Value { get; } = string.Empty;

    public LicensePlate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("License plate cannot be empty.", nameof(value));
        }

        if (value.Length is 5 or > 8)
        {
            throw new InvalidLicensePlateException(value);
        }
        Value = value;
    }

    public static implicit operator string(LicensePlate licensePlate) => licensePlate.Value;
    public static implicit operator LicensePlate(string value) => new LicensePlate(value); 
}