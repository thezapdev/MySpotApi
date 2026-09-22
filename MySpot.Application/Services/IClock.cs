using MySpot.Core.ValueObjects;

namespace MySpot.Api.Services;

public interface IClock
{
    DateTime Current();
}