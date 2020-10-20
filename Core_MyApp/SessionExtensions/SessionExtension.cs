using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Core_MyApp.SessionExtensions
{
    public static class SessionExtension
    {
        public static void SetSesionObject<T>(this ISession session, string key, T data)
        {
            // Save data in session in JSON form
            session.SetString(key, JsonConvert.SerializeObject(data));  
        }

        public static T GetSessionObject<T>(this ISession session, string key)
        {
            // read JSON string from Session
            var data = session.GetString(key);
            if (data == null)
            {
                // if no data the return default value of object
                return default(T);
            }
            // deserialoze the JSON into CLR object
            return JsonConvert.DeserializeObject<T>(data);
        }

    }
}
