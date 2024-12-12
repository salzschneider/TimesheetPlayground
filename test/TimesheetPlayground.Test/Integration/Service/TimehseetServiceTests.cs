using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using TimesheetPlayground.UI.BLL;
using TimesheetPlayground.UI.Models.DTO;
using TimesheetPlayground.UI.Models.Enum;
using System.Data;
using TimesheetPlayground.DAL.Entities;

namespace TimesheetPlayground.Test.Integration.Service
{
    public class TimehseetServiceTests
    {
        private readonly TimesheetService timesheetService;

        private static DateTime lastStartDate;

        private static void AssertAllFields(TimesheetDO expected, TimesheetDO actual)
        {
            Assert.Equal(expected.UserId, actual.UserId);
            Assert.Equal(expected.Status, actual.Status);
            Assert.Equal(0, DateTime.Compare(actual.StartDate.Date, expected.StartDate.Date));
        }

        private static DateTime GetLastStartDate()
        {
            using TimesheetPlaygroundContext dbContext = new();

            var last = dbContext.Timesheets.OrderBy(t => t.StartDate).LastOrDefault();

            return last != null ? last.StartDate : new DateTime(2021, 3, 1);
        }

        public TimehseetServiceTests()
        {
            timesheetService = new TimesheetService();
            lastStartDate = GetLastStartDate();
        }

        public static IEnumerable<object[]> ValidTimesheetDOs =>
            [
                new object[]{ new TimesheetDO()
                {
                    UserId = 1,
                    Status = StatusEnum.Draft,
                }},
            ];

        [Theory]
        [MemberData(nameof(ValidTimesheetDOs))]
        public async Task InsertTimesheetAsync_ValidTimesheetDO_ReturnNewTimehseetDO(TimesheetDO expected)
        {
            // arrange
            lastStartDate = expected.StartDate = lastStartDate.AddDays(31);
            
            // act
            var actual = await timesheetService.InsertTimesheetAsync(expected);

            // assert
            AssertAllFields(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ValidTimesheetDOs))]
        public async Task GetTimesheetsById_ValidTimesheetId_ReturnTimesheet(TimesheetDO timesheetDO)
        {
            // arrange
            lastStartDate = timesheetDO.StartDate = lastStartDate.AddDays(31);
            var expected = await timesheetService.InsertTimesheetAsync(timesheetDO);

            // act
            var actual = await timesheetService.GetTimesheetsById(expected.Id);

            // assert
            Assert.NotNull(actual);
            AssertAllFields(expected, actual);
        }

        [Fact]
        public async Task GetTimesheetsById_InvalidTimesheetId_ReturnNull()
        {
            // arrange, act
            var actual = await timesheetService.GetTimesheetsById(-1);

            // assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task GetTimesheetsByUser_InvalidTimesheetId_ReturnEmptyList()
        {
            // arrange, act
            var actual = await timesheetService.GetTimesheetsByUser(-1);

            // assert
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }
    }
}
