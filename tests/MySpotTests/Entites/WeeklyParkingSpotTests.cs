using MySpot.Api.Entities;
using MySpot.Api.Exceptions;
using MySpot.Api.Models;
using MySpot.Core.ValueObjects;
using Shouldly;

namespace MySpotTests;

public class WeeklyParkingSpotTests
{
    #region

    private readonly Date _date;
    private readonly WeeklyParkingSpot _weeklyParkingSpot;

    public WeeklyParkingSpotTests()
    {
        _date = new Date(new DateTimeOffset(2022, 08, 10, 0, 0, 0, TimeSpan.Zero));
        _weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(_date), "P1");
    }

    #endregion

    [Theory]
    [InlineData("2022-08-09")]
    [InlineData("2022-08-17")]
    public void given_invalid_date_add_reservation_should_fail(string dateString)
    {
        // Arrange
      
        var invalidDate = new Date(DateTimeOffset.Parse(dateString));
        var reservation = new Reservation(Guid.NewGuid(),_weeklyParkingSpot.Id, "John Doe", "ABC123", invalidDate);

        // Act 
        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, _date));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidReservationDateException>();
    }

    [Fact]
    public void given_reservation_for_already_existing_date_add_reservation_should_fail()
    {
        // Arrange
        var reservationDate = _date.AddDays(1);
        var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "ABC123", reservationDate);
    
        _weeklyParkingSpot.AddReservation(reservation, _date);
    
        var nextReservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "Jane Smith", "XYZ789", reservationDate);

        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(nextReservation, _date));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ReservationAlreadyExistsException>();
    }
    [Fact]
    public void given_valid_reservation_add_reservation_should_succeed()
    {
        // Arrange
        var reservationDate = _date.AddDays(1);
        var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "ABC123", reservationDate);

        // Act
        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, _date));

        // Assert
        exception.ShouldBeNull();
        _weeklyParkingSpot.Reservations.ShouldContain(reservation);
    }
}