namespace MySpot.Api.Commands;

public record CreateReservation(ParkingSpotId ParkingSpotId, Guid ReservationId, string EmployeeName, string LicensePlate, DateTime Date)
{
    
}