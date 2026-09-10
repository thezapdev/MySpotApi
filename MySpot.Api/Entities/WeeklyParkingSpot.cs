using MySpot.Api.Exceptions;
using MySpot.Api.Models;

namespace MySpot.Api.Entities;

public class WeeklyParkingSpot
{
    public readonly HashSet<Reservation> _reservations = new();
    public Guid Id { get; }
    public DateTime From { get; }
    public DateTime To { get; }
    public string Name { get; }
    public IEnumerable<Reservation> Reservations => _reservations;

    public WeeklyParkingSpot(Guid id, DateTime from, DateTime to, string name)
    {
        Id = id;
        From = from;
        To = to;
        Name = name;
    }
    public void AddReservation(Reservation reservation)
    {
        var isInvalidDate = reservation.Date.Date < From || reservation.Date.Date > To || reservation.Date.Date < DateTime.UtcNow.Date;
        if (isInvalidDate)
        {
            throw new InvalidReservationDateException(reservation.Date);
        }

        var existingReservation = Reservations.Any(x => reservation.Date.Date == x.Date);
        if(!existingReservation)
        {
            throw new ReservationAlreadyExistsException(Name, reservation.Date);
        }
        _reservations.Add(reservation);
    }
    public void RemoveReservation(Guid guid)
    {
        _reservations.Remove(_reservations.SingleOrDefault(x => x.Id == guid));
    }
}
