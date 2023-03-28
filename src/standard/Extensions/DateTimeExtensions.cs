// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Proton.Common.Standard.Extensions;

public static class DateTimeExtensions {
    public static TimeOnly ToTimeOnly(this DateTime dateTime) {
        return TimeOnly.FromDateTime(dateTime);
    }

    public static DateOnly ToDateOnly(this DateTime dateTime) {
        return DateOnly.FromDateTime(dateTime);
    }

    public static string TimeAgo(this DateTime dateTime) {
        string result;
        var timeSpan = DateTime.Now.Subtract(dateTime);

        if (timeSpan <= TimeSpan.FromSeconds(60))
            result = $"{timeSpan.Seconds} seconds ago";
        else if (timeSpan <= TimeSpan.FromMinutes(60))
            result = timeSpan.Minutes > 1 ? $"{timeSpan.Minutes} minutes ago" : "a minute ago";
        else if (timeSpan <= TimeSpan.FromHours(24))
            result = timeSpan.Hours > 1 ? $"{timeSpan.Hours} hours ago" : "an hour ago";
        else if (timeSpan <= TimeSpan.FromDays(30))
            result = timeSpan.Days > 1 ? $"{timeSpan.Days} days ago" : "yesterday";
        else if (timeSpan <= TimeSpan.FromDays(365))
            result = timeSpan.Days > 30 ? $"{timeSpan.Days / 30} months ago" : "a month ago";
        else
            result = timeSpan.Days > 365 ? $"{timeSpan.Days / 365} years ago" : "a year ago";

        return result;
    }
}