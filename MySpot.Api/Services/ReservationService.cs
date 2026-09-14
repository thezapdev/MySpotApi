using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;
using MySpot.Api.Models;
using MySpot.Api.ValueObjects;
using MySpot.Core.ValueObjects;

namespace MySpot.Api.Services;

public class ReservationService
{
    private static Clock _clock = new Clock();

    private static readonly List<WeeklyParkingSpot> WeeklyParkingSpots = new()
    {
        new WeeklyParkingSpot(
            new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000001")),
            new Week(_clock.Current()),
            new ParkingSpotName("P1")
        ),
        new WeeklyParkingSpot(
            new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000002")),
            new Week(_clock.Current()),
            new ParkingSpotName("P2")
        ),
        new WeeklyParkingSpot(
            new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000003")),
            new Week(_clock.Current()),
            new ParkingSpotName("P3")
        ),
        new WeeklyParkingSpot(
            new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000004")),
            new Week(_clock.Current()),
            new ParkingSpotName("P4")
        ),
        new WeeklyParkingSpot(
            new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000005")),
            new Week(_clock.Current()),
            new ParkingSpotName("P5")
        )
    };

    public Reservation Get(Guid id) => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<Reservation> GetAllWeekly() => WeeklyParkingSpots.SelectMany(x => x.Reservations)
        .Select((x => new Reservation(x.Id, x.EmployeeName, x.LicensePlate, x.Date)));

    public Guid? Create(CreateReservation command)
    {
        var weeklyParkingSpot = WeeklyParkingSpots.SingleOrDefault(x => x.Id == command.ParkingSpotId);
        if (weeklyParkingSpot is null)
        {
            return default;
        }

        var reservation =
            new Reservation(command.ReservationId, command.EmployeeName, command.LicensePlate, command.Date);
        weeklyParkingSpot.AddReservation(reservation, _clock.Current());
        return reservation.Id;
    }

    public bool Update(ChangeReservationLicensePlate command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpotByReservationId(command.ReservationId);
        if (weeklyParkingSpot is null)
        {
            return false;
        }

        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
        if (existingReservation.Date <= _clock.Current())
        {
            return false;
        }

        existingReservation.ChangeLicensePlate(command.LicensePlate);
        return true;
    }

    public bool Delete(DeleteReservation command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpotByReservationId(command.ReservationId);
        if (weeklyParkingSpot is null)
        {
            return false;
        }

        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
        if (existingReservation is null)
        {
            return false;
        }

        weeklyParkingSpot.RemoveReservation(command.ReservationId);
        return true;
    }

    private WeeklyParkingSpot GetWeeklyParkingSpotByReservationId(Guid reservationId)
    {
        return WeeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
    }
}