using BuberDinner.Application.Common.Interface.Services;

namespace BuberDinner.Infrastructure.Authentication.Services;

public class DateTimeProvider :IDateTimeProvider
{
        public DateTime Now => DateTime.UtcNow;

}
