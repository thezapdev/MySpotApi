using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Models;
using MySpot.Api.Repositories;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
using MySpot.Core.ValueObjects;
using MySpotTests.Shared;
using Shouldly;

namespace MySpotTests.Services;

public class ReservationServiceTests
{
    [Fact]
    public void given_reservation_for_not_taken_date_add_reservation_should_succeed()
    {
        // Arrange
        var parkingSpot = _weeklyParkingSpotsRepository.GetAll().First();
        var command = new CreateReservation(parkingSpot.Id, Guid.NewGuid(), "John Doe", "ABC123", _clock.Current().AddDays(1));

// Act
var reservationId = _reservationService.Create(command);
        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId) ;
// Assert
    }
    
    #region Arrange 
    private readonly IReservationService _reservationService;
    private static readonly IClock _clock = new TestClock();
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotsRepository;

    public ReservationServiceTests()
    {
        _weeklyParkingSpotsRepository = new InMemoryWeeklyParkingSpotRepository(_clock);
        _reservationService = new ReservationService(_clock, _weeklyParkingSpotsRepository);
    }
    #endregion
}