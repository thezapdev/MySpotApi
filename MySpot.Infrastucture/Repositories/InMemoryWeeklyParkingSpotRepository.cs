using MySpot.Api.Entities;
using MySpot.Api.Models;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
using MySpot.Core.ValueObjects;

namespace MySpot.Api.Repositories;

internal class InMemoryWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
{
    private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;
    private readonly IClock _clock;

    public InMemoryWeeklyParkingSpotRepository(IClock clock)
    {
        _clock = clock;
        _weeklyParkingSpots = new()
        {
            new WeeklyParkingSpot(
                new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000001")),
                new Week(clock.Current()),
                new ParkingSpotName("P1")
            ),
            new WeeklyParkingSpot(
                new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000002")),
                new Week(clock.Current()),
                new ParkingSpotName("P2")
            ),
            new WeeklyParkingSpot(
                new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000003")),
                new Week(clock.Current()),
                new ParkingSpotName("P3")
            ),
            new WeeklyParkingSpot(
                new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000004")),
                new Week(clock.Current()),
                new ParkingSpotName("P4")
            ),
            new WeeklyParkingSpot(
                new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000005")),
                new Week(clock.Current()),
                new ParkingSpotName("P5")
            )
        };
    }

    public IEnumerable<WeeklyParkingSpot> GetAll() => _weeklyParkingSpots;

    public WeeklyParkingSpot Get(ParkingSpotId id) => _weeklyParkingSpots.SingleOrDefault(x => x.Id == id);

    public void Add(WeeklyParkingSpot weeklyParkingSpot) => _weeklyParkingSpots.Add(weeklyParkingSpot);

    public void Update(WeeklyParkingSpot weeklyParkingSpot)
    {
    }

    public void Delete(WeeklyParkingSpot weeklyParkingSpot) => _weeklyParkingSpots.Remove(weeklyParkingSpot);
}