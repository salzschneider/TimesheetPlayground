using AutoMapper;
using TimesheetPlayground.UI.Common;

namespace TimesheetPlayground.UI.BLL
{
    public class ServiceBase
    {
        protected readonly IMapper mapper;

        public ServiceBase()
        {
            mapper = AutomapperHelper.GetMapper();
        }
    }
}
