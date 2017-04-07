using SMETA.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMETA.Web.Services
{
    public class GroupingService
    {
        public static IEnumerable<IGrouping<DateTime, Post>> GroupByHour(List<Post> posts)
        {
            return posts.GroupBy(x =>
            {
                var stamp = x.PostedDate;
                stamp = stamp.AddMinutes(-stamp.Minute);
                stamp = stamp.AddSeconds(-stamp.Second);
                stamp = stamp.AddMilliseconds(-stamp.Millisecond);
                return stamp;
            });
        }

        public static IEnumerable<IGrouping<DateTime, Post>> GroupByDay(List<Post> posts)
        {
            return posts.GroupBy(x =>
            {
                var stamp = x.PostedDate;
                stamp = stamp.AddHours(-stamp.Hour);
                stamp = stamp.AddMinutes(-stamp.Minute);
                stamp = stamp.AddSeconds(-stamp.Second);
                stamp = stamp.AddMilliseconds(-stamp.Millisecond);
                return stamp;
            });
        }

        public static IEnumerable<IGrouping<DateTime, Post>> GroupByMonth(List<Post> posts)
        {
            return posts.GroupBy(x =>
            {
                var stamp = x.PostedDate;
                stamp = stamp.AddDays(-stamp.Day);
                stamp = stamp.AddHours(-stamp.Hour);
                stamp = stamp.AddMinutes(-stamp.Minute);
                stamp = stamp.AddSeconds(-stamp.Second);
                stamp = stamp.AddMilliseconds(-stamp.Millisecond);
                return stamp;
            });
        }

        public static IEnumerable<IGrouping<DateTime, Post>> GroupByYear(List<Post> posts)
        {
            return posts.GroupBy(x =>
            {
                var stamp = x.PostedDate;
                stamp = stamp.AddMonths(-stamp.Month);
                stamp = stamp.AddDays(-stamp.Day);
                stamp = stamp.AddHours(-stamp.Hour);
                stamp = stamp.AddMinutes(-stamp.Minute);
                stamp = stamp.AddSeconds(-stamp.Second);
                stamp = stamp.AddMilliseconds(-stamp.Millisecond);
                return stamp;
            });
        }
    }
}