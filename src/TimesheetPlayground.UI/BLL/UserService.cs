using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetPlayground.DAL.Entities;
using TimesheetPlayground.UI.Common;
using TimesheetPlayground.UI.Models.DTO;
using TimesheetPlayground.UI.Models.Enum;

namespace TimesheetPlayground.UI.BLL
{
    public class UserService : ServiceBase, IUserService
    {
        public async Task<List<UserDO>> GetAllUsersAsync()
        {
            using (TimesheetPlaygroundContext dbContext = new TimesheetPlaygroundContext())
            {
                var allUserEnitiy = await dbContext.Users.ToListAsync();
                var allUsers = allUserEnitiy.Select(u => mapper.Map<UserDO>(u)).ToList();

                return allUsers;
            }
        }

        public async Task<UserDO> GetFirstUserAsync()
        {
            using (TimesheetPlaygroundContext dbContext = new TimesheetPlaygroundContext())
            {
                var firstUser = await dbContext.Users.FirstAsync();

                return mapper.Map<UserDO>(firstUser);
            }
        }

        public async Task<UserDO> GetUserByIdAsync(int id)
        {
            using (TimesheetPlaygroundContext dbContext = new TimesheetPlaygroundContext())
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                UserDO resultUser = null;

                if (user != null)
                {
                    resultUser = mapper.Map<UserDO>(user);
                }

                return resultUser;
            }
        }

        public async Task<UserDO> GetUserByNamePasswordAsync(string name, string password)
        {
            using (TimesheetPlaygroundContext dbContext = new TimesheetPlaygroundContext())
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Name == name && u.Password == password);
                UserDO resultUser = null;

                if (user != null)
                {
                    resultUser = mapper.Map<UserDO>(user);
                }

                return resultUser;
            }
        }

        public UserDO InsertUser(UserDO userDO)
        {
            using (TimesheetPlaygroundContext dbContext = new TimesheetPlaygroundContext())
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Name == userDO.Name);

                if (user != null)
                {
                    throw new ApplicationException("Existing user");
                }

                var newUser = mapper.Map<User>(userDO);

                dbContext.Add(newUser);
                dbContext.SaveChanges();

                return mapper.Map<UserDO>(newUser);
            }
        }
    }
}
