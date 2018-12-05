using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace netCoreSample
{
    public class SessionSample
    {
        private const string SessionVists = nameof(SessionVists);
        private const string SessionTimeCreated = nameof(SessionTimeCreated);


        public static async Task SessionAsync(HttpContext context) {

            int visits = context.Session.GetInt32(SessionVists) ?? 0;
            string timeCreated = context.Session.GetString(SessionTimeCreated) ?? string.Empty;
            if (string.IsNullOrEmpty(timeCreated)) {
                timeCreated = DateTime.Now.ToString("t",CultureInfo.InstalledUICulture);
                context.Session.SetString(SessionTimeCreated, timeCreated);
            }

            DateTime timeCreated2 = DateTime.Parse(timeCreated);
            context.Session.SetInt32(SessionVists, ++visits);
            await context.Response.WriteAsync(
             $"Number of visits within this session: {visits} " +
             $"that was created at {timeCreated2:T}; " +
             $"current time: {DateTime.Now:T}");
        }

    }
}
