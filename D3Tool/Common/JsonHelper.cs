using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace D3Tool
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
        /// <summary>  
        /// 将指定的 JSON 数据反序列化成指定对象。  
        /// </summary>  
        /// <typeparam name="T">对象类型。</typeparam>  
        /// <param name="json">JSON 数据。</param>  
        /// <returns></returns>  
        public static T FromJson<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, _jsonSettings);
            }
            catch (Exception ex)
            {
                return default(T);
                throw ex;
            }
        }


    }
}
