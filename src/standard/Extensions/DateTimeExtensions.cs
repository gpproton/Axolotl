/*
  Copyright (c) 2024 <Godwin peter. O>
  
  Permission is hereby granted, free of charge, to any person obtaining a copy
  of this software and associated documentation files (the "Software"), to deal
  in the Software without restriction, including without limitation the rights
  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
  copies of the Software, and to permit persons to whom the Software is
  furnished to do so, subject to the following conditions:
  
  The above copyright notice and this permission notice shall be included in all
  copies or substantial portions of the Software.
  
  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
  SOFTWARE.
  
   Author: Godwin peter. O (me@godwin.dev)
   Created At: Wed 11 Dec 2024 23:38:41
   Modified By: Godwin peter. O (me@godwin.dev)
   Modified At: Wed 11 Dec 2024 23:38:41
*/

namespace Axolotl.Extensions;

public static class DateTimeExtensions {
    public static TimeOnly ToTimeOnly(this DateTime dateTime) => TimeOnly.FromDateTime(dateTime);

    public static DateOnly ToDateOnly(this DateTime dateTime) => DateOnly.FromDateTime(dateTime);

    public static DateTimeOffset Parse(this DateTimeOffset offset, DateTime time) => new(time, TimeSpan.Zero);

    public static string TimeAgo(this DateTime dateTime) {
        string result;
        TimeSpan timeSpan = DateTime.Now.Subtract(dateTime);

        if (timeSpan <= TimeSpan.FromSeconds(60))
            result = $"{timeSpan.Seconds} seconds ago";
        else if (timeSpan <= TimeSpan.FromMinutes(60))
            result = timeSpan.Minutes > 1 ? $"{timeSpan.Minutes} minutes ago" : "a minute ago";
        else if (timeSpan <= TimeSpan.FromHours(24))
            result = timeSpan.Hours > 1 ? $"{timeSpan.Hours} hours ago" : "an hour ago";
        else if (timeSpan <= TimeSpan.FromDays(30))
            result = timeSpan.Days > 1 ? $"{timeSpan.Days} days ago" : "yesterday";
        else {
            result = timeSpan <= TimeSpan.FromDays(365)
            ? timeSpan.Days > 30 ? $"{timeSpan.Days / 30} months ago" : "a month ago"
            : timeSpan.Days > 365 ? $"{timeSpan.Days / 365} years ago" : "a year ago";
        }

        return result;
    }
}