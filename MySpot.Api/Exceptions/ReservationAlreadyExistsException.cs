namespace MySpot.Api.Exceptions;

public sealed class ReservationAlreadyExistsException : CustomException
{
    public string Name { get; }
    public DateTime Date { get; }
    
    public ReservationAlreadyExistsException(string name, DateTime date) : base($"Reservation for {name} on {date:d} already exists")
    {
        Name = name;
        Date = date;
    }
}