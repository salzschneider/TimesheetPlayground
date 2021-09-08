using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
