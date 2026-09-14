using System.ComponentModel;
using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Models;

public class Reservation
{
    public ReservationId Id { get;  }
    public ParkingSpotId ParkingSpotId { get; set; }
    public EmployeeName EmployeeName { get; private set; }
    public LicensePlate LicensePlate { get; private set; }
    public DateTime Date { get; private set; }

    public Reservation(Guid id, string? employeeName, LicensePlate licensePlate, DateTime date)
    {
        Id = id;
        EmployeeName = employeeName;
       ChangeLicensePlate(licensePlate);
        Date = date;
    }
    public void ChangeLicensePlate(LicensePlate licensePlate) => LicensePlate = licensePlate;
   
}