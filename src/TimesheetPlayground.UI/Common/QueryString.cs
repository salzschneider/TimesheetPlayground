using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TimesheetPlayground.UI.Models.Enum;

namespace TimesheetPlayground.UI.Common
{
    public static class QueryString
    {
        public static Dictionary<string, string> GetTimesheetQueryString(int userId, int timesheetId)
        {
            return new Dictionary<string, string>()
            {
                {"userId", userId.ToString() },
                {"timesheetId", timesheetId.ToString() },
            };
        }
    }
}
