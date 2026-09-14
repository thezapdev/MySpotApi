using MySpot.Api.Exceptions;

namespace MySpot.Api.ValueObjects;

public class ReservationId
{
    public Guid Value { get; }
    
    public ReservationId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static ReservationId Create() => new(Guid.NewGuid());
    
    public static implicit operator Guid(ReservationId reservationId) => reservationId.Value;
    public static implicit operator ReservationId(Guid value) => new ReservationId(value);
}