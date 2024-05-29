using GraduationProjectBMS.Models;

namespace GraduationProjectBMS.Repositories.User
{
    public interface IUserFunctions
    {
        List<AppUser> GetUsers();
        void DeleteUser(string id);
        AppUser GetUser(string id);
    }
}
