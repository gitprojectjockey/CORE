using LMS.DataTransfer.Objects;
using System;
using System.Collections.Generic;

namespace LMS.Services.Helpers
{
    public class DataHelper
    {
        public static IEnumerable<string> HumanizeBusinessHours(IEnumerable<BranchHourDto> branchHours)
        {
            var hours = new List<string>();

            foreach (var time in branchHours)
            {
                var day = HumanizeDayOfWeek(time.DayOfWeek);
                var openTime = HumanizeTime(time.OpenTime);
                var closeTime = HumanizeTime(time.CloseTime);
                var timeEntry = $"{day} {openTime} to {closeTime}";
                hours.Add(timeEntry);
            };

            return hours;
        }

        private static string HumanizeDayOfWeek(int number)
        {
            return Enum.GetName(typeof(DayOfWeek), number);
        }

        private static string HumanizeTime(int time)
        {
            TimeSpan result = TimeSpan.FromHours(time);
            return result.ToString("hh':'mm");
        }
    }
}

