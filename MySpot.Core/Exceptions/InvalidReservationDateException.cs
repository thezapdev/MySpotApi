using MySpot.Core.ValueObjects;

namespace MySpot.Api.Exceptions;

public sealed class InvalidReservationDateException : CustomException
{
    public Date Date { get; }
    public InvalidReservationDateException(Date dateTime) : base($"Reservation date: {dateTime:d} is invalid")
    {
        Date = dateTime;
    }
}