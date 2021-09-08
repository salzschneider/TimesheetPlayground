using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TimesheetPlayground.UI.Models.Enum;

namespace TimesheetPlayground.UI.Common
{
    public static class UserClaim
    {
        public static int? GetUserId(ClaimsPrincipal user)
        {
            int? resultUserId = null;

            if(user != null)
            {
                var userId = user.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

                if (userId != null)
                {
                    resultUserId = Convert.ToInt32(userId);
                }
            }

            return resultUserId;
        }

        public static RoleEnum? GetUserRole(ClaimsPrincipal user)
        {
            RoleEnum? resultUserRole = null;

            if (user != null)
            {
                var userRole = user.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault()?.Value;

                if (userRole != null)
                {
                    resultUserRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userRole);
                }
            }

            return resultUserRole;
        }

        public static string GetUserName(ClaimsPrincipal user)
        {
            string userName = null;

            if (user != null)
            {
                userName = user.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            }

            return userName;
        }

        public static bool IsManager(ClaimsPrincipal user)
        {
            bool isManager = false;

            if (user != null)
            {
                isManager = user.IsInRole(RoleEnum.Manager.ToString());
            }

            return isManager;
        }
    }
}
