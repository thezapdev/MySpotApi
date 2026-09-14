using MySpot.Api.Exceptions;
using MySpot.Api.Models;
using MySpot.Api.ValueObjects;
using MySpot.Core.ValueObjects;

namespace MySpot.Api.Entities;

public class WeeklyParkingSpot
{
    private readonly HashSet<Reservation> _reservations = new();

    public ParkingSpotId Id { get; }
    public Week Week { get; private set; }
    public ParkingSpotName Name { get; }
    public IEnumerable<Reservation> Reservations => _reservations;

    public WeeklyParkingSpot(ParkingSpotId id, Week week, ParkingSpotName name)
    {
        Id = id;
        Week = week;
        Name = name;
    }

    public void AddReservation(Reservation reservation, DateTime now)
    {
        var reservationDate = DateOnly.FromDateTime(reservation.Date);
        var nowDate = DateOnly.FromDateTime(now);

        var isInvalidDate = reservationDate < Week.From || reservationDate > Week.To || reservationDate < nowDate;
        if (isInvalidDate)
        {
            throw new InvalidReservationDateException(reservation.Date);
        }

        var reservationAlreadyExists = _reservations.Any(x => DateOnly.FromDateTime(x.Date) == reservationDate);
        if (reservationAlreadyExists)
        {
            throw new ReservationAlreadyExistsException(Name, reservation.Date);
        }

        _reservations.Add(reservation);
    }

    public void RemoveReservation(ReservationId reservationId)
    {
        _reservations.RemoveWhere(x => x.Id == reservationId);
    }
}