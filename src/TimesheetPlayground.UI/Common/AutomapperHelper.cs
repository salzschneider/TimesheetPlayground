using AutoMapper;

namespace TimesheetPlayground.UI.Common
{
    public class AutomapperHelper
    {
        public static IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            return mapperConfig.CreateMapper();
        }
    }
}
