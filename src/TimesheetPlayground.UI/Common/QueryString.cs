using System.Collections.Generic;

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
