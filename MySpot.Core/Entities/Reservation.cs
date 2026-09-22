using System.ComponentModel;
using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;
using MySpot.Core.ValueObjects;

namespace MySpot.Api.Models;

public class Reservation
{
    public ReservationId Id { get;  }
    public ParkingSpotId ParkingSpotId { get; set; }
    public EmployeeName EmployeeName { get; private set; }
    public LicensePlate LicensePlate { get; private set; }
    public Date Date { get; private set; }

    public Reservation(Guid id,ParkingSpotId spotId, string? employeeName, LicensePlate licensePlate, Date date)
    {
        Id = id;
        ParkingSpotId = spotId;
        EmployeeName = employeeName;
       ChangeLicensePlate(licensePlate);
        Date = date;
    }
    public void ChangeLicensePlate(LicensePlate licensePlate) => LicensePlate = licensePlate;
   
}