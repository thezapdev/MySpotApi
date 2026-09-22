using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;
using MySpot.Api.Models;
using MySpot.Core.ValueObjects;

namespace MySpot.Api.Services;

public class ReservationService : IReservationService
{
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
    private readonly IClock _clock;

    public ReservationService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotsRepository)
    {
        _weeklyParkingSpotRepository = weeklyParkingSpotsRepository;
        _clock = clock;
    }

    public ReservationDto Get(Guid id) => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<ReservationDto> GetAllWeekly() => _weeklyParkingSpotRepository.GetAll().SelectMany(x => x.Reservations)
        .Select(x => new ReservationDto
        {
            Id = x.Id,
            ParkingSpotId = x.ParkingSpotId,
            EmployeeName = x.EmployeeName,
            LicensePlate = x.LicensePlate,
            Date = x.Date.Value.Date
        });

    public Guid? Create(CreateReservation command)
    {
        var weeklyParkingSpot = _weeklyParkingSpotRepository.GetAll().SingleOrDefault(x => x.Id == command.ParkingSpotId);
        if (weeklyParkingSpot is null)
        {
            return default;
        }

        var reservationDate = new Date(command.Date);
        var reservation = new Reservation(
            command.ReservationId, 
            command.ParkingSpotId, 
            command.EmployeeName, 
            command.LicensePlate,
            reservationDate
        );

        var now = _clock.Current();
        weeklyParkingSpot.AddReservation(reservation, new Date(now));
        
        return reservation.Id;
    }

    public bool Update(ChangeReservationLicensePlate command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpotByReservationId(command.ReservationId);
        if (weeklyParkingSpot is null)
        {
            return false;
        }

        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
        if (existingReservation is null)
        {
            return false;
        }

        var currentDate = _clock.Current().Date;
        if (existingReservation.Date.Value.Date <= currentDate)
        {
            return false;
        }

        existingReservation.ChangeLicensePlate(command.LicensePlate);
        return true;
    }

    public bool Delete(DeleteReservation command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpotByReservationId(command.ReservationId);
        if (weeklyParkingSpot is null)
        {
            return false;
        }

        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
        if (existingReservation is null)
        {
            return false;
        }

        weeklyParkingSpot.RemoveReservation(command.ReservationId);
        return true;
    }

    private WeeklyParkingSpot? GetWeeklyParkingSpotByReservationId(Guid reservationId)
    {
        return _weeklyParkingSpotRepository.GetAll().SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
    }
}