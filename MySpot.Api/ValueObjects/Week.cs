using Date = System.DateOnly;
namespace MySpot.Core.ValueObjects;

public sealed record Week
{
    public Date From { get; }
    public Date To { get; }

    public Week(DateTimeOffset value)
    {
        var pastDays = value.DayOfWeek is DayOfWeek.Sunday ? 6 : (int)value.DayOfWeek - 1;
        var monday = value.AddDays(-pastDays);

        From = Date.FromDateTime(monday.DateTime);
        To = From.AddDays(6);
    }

    public override string ToString() => $"{From:yyyy-MM-dd} -> {To:yyyy-MM-dd}";
}