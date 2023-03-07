using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DMTool
{
    public static class JsonHelper
    {
        private static JsonSerializerSettings _jsonSettings;

        static JsonHelper()
        {
            IsoDateTimeConverter datetimeConverter = new IsoDateTimeConverter();
            datetimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            _jsonSettings = new JsonSerializerSettings();
            _jsonSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            _jsonSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            _jsonSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            _jsonSettings.Converters.Add(datetimeConverter);
        }
        /// <summary>  
        /// 将指定的对象序列化成 JSON 数据。  
        /// </summary>  
        /// <param name="obj">要序列化的对象。</param>  
        /// <returns></returns>  
        public static string ObjectToJson(this object obj)
        {
            try
            {
                if (null == obj)
                    return null;

                return JsonConvert.SerializeObject(obj, Formatting.None, _jsonSettings);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public static T JsonCopy<T>(this T obj)where T :class
        {
           return obj.ToJson().FromJson<T>();
        }
        /// <summary>  
        /// 将指定的对象序列化成 JSON 数据。  
        /// </summary>  
        /// <param name="obj">要序列化的对象。</param>  
        /// <returns></returns>  
        public static string ToJson(this object obj)
        {
            try
            {
                if (null == obj)
                    return null;

                return JsonConvert.SerializeObject(obj, Formatting.None, _jsonSettings);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        /// <summary>  
        /// 将指定的对象序列化成 JSON 数据。  
        /// </summary>  
        /// <param name="obj">要序列化的对象。</param>  
        /// <returns></returns>  
        public static T FromJson<T>(this string jsonBody, T obj)
        {
            try
            {
                if (null == jsonBody)
                    return default(T);

                return JsonConvert.DeserializeObject<T>(jsonBody, _jsonSettings);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
        /// <summary>  
        /// 将指定的对象序列化成 JSON 数据。  
        /// </summary>  
        /// <param name="obj">要序列化的对象。</param>  
        /// <returns></returns>  
        public static T FromJson<T>(this string jsonBody)
        {
            try
            {
                if (null == jsonBody)
                    return default(T);

                return JsonConvert.DeserializeObject<T>(jsonBody, _jsonSettings);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
        /// <summary>  
        /// 将指定的对象序列化成 JSON 数据。  
        /// </summary>  
        /// <param name="dr">要序列化的对象。</param>  
        /// <returns></returns>  
        public static Dictionary<string, object> ToDictionary(this DataRow dr)
        {
            try
            {

                Dictionary<string, object> dl = new Dictionary<string, object>();
                foreach (DataColumn dc in dr.Table.Columns)
                {
                    dl.Add(dc.ColumnName, dr[dc.ColumnName]);
                }


                return dl;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }


        /// <summary>  
        /// 将指定的对象序列化成 JSON 数据。  
        /// </summary>  
        /// <param name="dt">要序列化的对象。</param>  
        /// <returns></returns>  
        public static List<Dictionary<string, object>> ToDictionary(this DataTable dt)
        {
            try
            {

                List<Dictionary<string, object>> dl = new List<Dictionary<string, object>>();
                foreach (DataRow dr in dt.Rows)
                {
                    dl.Add(dr.ToDictionary());
                }


                return dl;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }



    }
}
