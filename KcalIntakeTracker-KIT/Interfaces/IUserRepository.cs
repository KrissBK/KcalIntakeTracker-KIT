using KcalIntakeTracker_KIT.Models;

namespace KcalIntakeTracker_KIT.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
    }
}
