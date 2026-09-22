using MySpot.Core.ValueObjects;

namespace MySpot.Api.Services;

public class Clock : IClock
{
    public DateTime Current() => DateTime.Now;
}