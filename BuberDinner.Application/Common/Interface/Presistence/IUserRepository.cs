using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Interface.Presistence;

public interface IUserRepository
{
    void Add(User user);
    User? GetUserByEmail(string email);
}
