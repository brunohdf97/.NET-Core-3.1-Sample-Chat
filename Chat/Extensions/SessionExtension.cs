using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Chat.Extensions
{
    public static class SessionExtension
    {
        public static void SetJsonSesssion<T>(this ISession session, string key, T obj)
        {
            var json_obj = JsonConvert.SerializeObject(obj);
            session.SetString(key, json_obj);

        }

        public static bool ExistJsonSession(this ISession session, string key)
        {
            if (session.Keys.Contains(key))
                return true;

            return false;
        }

        public static T GetJsonSession<T>(this ISession session, string key)
        {
            try
            {
                if (!session.ExistJsonSession(key))
                    return default(T);

                // Retrieve
                string json_obj = session.GetString(key);
                T obj = JsonConvert.DeserializeObject<T>(json_obj);
                return obj;
            }
            catch
            {
                return default(T);
            }
        }
    }
}
