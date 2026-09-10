namespace MySpot.Api.Exceptions;

public sealed class InvalidReservationDateException : CustomException
{
    public DateTime Date { get; }
    public InvalidReservationDateException(DateTime dateTime) : base($"Reservation date: {dateTime:d} is invalid")
    {
        Date = dateTime;
    }
}