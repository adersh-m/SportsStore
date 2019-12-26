using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            session.SetString(key, JsonConvert.SerializeObject(value,jsSettings));
        }
        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.Get(key);
            if (sessionData == null) {
                return default(T);
            }
            else
            {
                string json = System.Text.Encoding.UTF8.GetString(sessionData);
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }
}
