using KcalIntakeTracker_KIT.Models;
using KcalIntakeTracker_KIT.Dto;

namespace KcalIntakeTracker_KIT.Interfaces
{
    public interface IUserRepository
    {
        ICollection<UserDto> GetUsers();

        User GetUser(int userId);

        bool UserExists(int userId);   

        bool CreateUser(User user);

        bool UpdateUser(User user);

        bool DeleteUser(User userId);
    }
}
