using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;


namespace MyEcom.Core.Session
{
    public static class SessionSet<T> where T : class, new()
    {

        public static T CurrentUser(string key)
        {
            return Get(key);
        }

        public static void Set(T model, string key)
        {

            HttpContext.Current.Session[key] = JsonConvert.SerializeObject(model);
            HttpContext.Current.Session.Timeout = 60;
        }


        public static T Get(string key)
        {

            return JsonConvert.DeserializeObject<T>(HttpContext.Current.Session[key].ToString());
        }


        public static void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

    }
}