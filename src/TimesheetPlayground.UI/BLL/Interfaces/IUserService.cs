using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetPlayground.UI.Models.DTO;

namespace TimesheetPlayground.UI.BLL
{
    public interface IUserService
    {
        Task<List<UserDO>> GetAllUsersAsync();

        Task<UserDO> GetFirstUserAsync();

        Task<UserDO> GetUserByIdAsync(int id);

        Task<UserDO> GetUserByNamePasswordAsync(string name, string password);
    }
}
