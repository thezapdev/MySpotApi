using MySpot.Api.Services;


namespace MySpotTests.Shared;

public class TestClock : IClock
{
    public DateTime Current() => new (2022, 08, 11);

}