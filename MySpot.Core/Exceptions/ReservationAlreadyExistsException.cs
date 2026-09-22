using MySpot.Core.ValueObjects;

namespace MySpot.Api.Exceptions;

public sealed class ReservationAlreadyExistsException : CustomException
{
    public string Name { get; }
    public Date Date { get; }
    
    public ReservationAlreadyExistsException(string name, Date date) : base($"Reservation for {name} on {date:d} already exists")
    {
        Name = name;
        Date = date;
    }
}