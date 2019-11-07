using CityRates.Core.Domain;
using CityRates.Core.Domain.Belagroprombank;
using CityRates.Core.Domain.Belarusbank;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CityRates.Core.Utils
{

    public class WorkTimeUtils
    {
        private static string BELARUSBANK_PATTERN = @"(?<main>\p{L}{2} (?<from_time>\d{1,2} \d{1,2}) (?<to_time>\d{1,2} \d{1,2}) (?<break_from>\d{1,2} \d{1,2}) (?<break_to>\d{1,2} \d{1,2}))|(?<empty>\p{L}{1,2}\s+\|)|(?<no_break>\p{L}{2} (?<nbr_from>\d{1,2} \d{1,2}) (?<nbr_to>\d{1,2} \d{1,2}))";
        private static string BELAGROPROMBANK_PATTERN = @"(?<from_hour>\d{1,2})[:.](?<from_minute>\d{1,2})[:.]\d{1,2}\s*[-]\s*(?<to_hour>\d{1,2})[:.](?<to_minute>\d{1,2})[:.]\d{1,2}";

        public static List<WorkTime> parseDateTimeFromBelagroprombank(Belagroprombank domain)
        {
            string dateTimeRepresentation = domain.BankWorkTimeRu;
            var match = Regex.Match(dateTimeRepresentation, BELAGROPROMBANK_PATTERN);

            List<WorkTime> workTimes = new List<WorkTime>();
            if (match.Success)
            {
                int fromHour = int.Parse(match.Groups["from_hour"].Value);
                int fromMinute = int.Parse(match.Groups["from_minute"].Value);
                int toHour = int.Parse(match.Groups["to_hour"].Value);
                int toMinute = int.Parse(match.Groups["to_minute"].Value);

                int openMinutesFromDayStart = fromHour * 60 + fromMinute;
                int closeMinutesFromDayStart = toHour * 60 + toMinute;

                foreach (var day in getDaysArray())
                {
                    if (day.Equals("SAT") || day.Equals("SUN"))
                    {
                        workTimes.Add(new WorkTime(day, true));
                    } else
                    {
                        workTimes.Add(new WorkTime(day, openMinutesFromDayStart, closeMinutesFromDayStart));
                    }
                }
            }
            else
            {
                throw new Exception("Unable to parse regular expression for: " + dateTimeRepresentation);
            }

            return workTimes;
        }

        public static List<WorkTime> parseDateTimeFromBelarusbank(Belarusbank domain)
        {
            string dateTimeRepresentation = domain.InfoWorktime;
            MatchCollection matches = Regex.Matches(dateTimeRepresentation, BELARUSBANK_PATTERN);

            List<WorkTime> workTimes = new List<WorkTime>();
            foreach (Match match in matches)
            {
                if (match.Groups["main"].Success)
                {
                    string fromTime = match.Groups["from_time"].Value; //24 00
                    string toTime = match.Groups["to_time"].Value;     //00 00 
                    string dayName = match.Groups["main"].Value.Substring(0, 2);

                    WorkTime wt = getWorkTimeFromString(dayName, fromTime, toTime);
                    workTimes.Add(wt);
                }
                else if (match.Groups["empty"].Success)
                {
                    string dayName = match.Groups["empty"].Value.Substring(0, 2);
                    WorkTime wt = getDayOffFromString(dayName);
                    workTimes.Add(wt);
                }
                else if (match.Groups["no_break"].Success)
                {
                    string fromTime = match.Groups["nbr_from"].Value;
                    string toTime = match.Groups["nbr_to"].Value;
                    string dayName = match.Groups["no_break"].Value.Substring(0, 2);
                    WorkTime wt = getWorkTimeFromString(dayName, fromTime, toTime);
                    workTimes.Add(wt);
                }
                else
                {
                    throw new Exception("Unable to parse regular expression for: " + dateTimeRepresentation);
                }
            }

            return workTimes;
        }

        private static WorkTime getWorkTimeFromString(string dayName, string fromTime, string toTime)
        {
            int hourFrom = int.Parse(fromTime.Split(' ')[0].Trim());
            int minuteFrom = int.Parse(fromTime.Split(' ')[1].Trim());

            int hourTo = int.Parse(toTime.Split(' ')[0].Trim());
            int minuteTo = int.Parse(toTime.Split(' ')[1].Trim());

            string convertedDayName = convertDayName(dayName);

            return new WorkTime(convertedDayName, hourFrom * 60 + minuteFrom, hourTo * 60 + minuteTo);
        }

        private static WorkTime getDayOffFromString(string dayName)
        {
            return new WorkTime(convertDayName(dayName), true);
        }

        private static string convertDayName(string dayName)
        {
            switch (dayName.ToLower())
            {
                case "пн":
                    return "MON";
                case "вт":
                    return "TUE";
                case "ср":
                    return "WED";
                case "чт":
                    return "THU";
                case "пт":
                    return "FRI";
                case "сб":
                    return "SAT";
                case "вс":
                    return "SUN";
                default:
                    Console.WriteLine("UNK");
                    return "UNK";
            }
        }

        private static string[] getDaysArray()
        {
            return new string[] { "MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN" };
        }
    }
}
