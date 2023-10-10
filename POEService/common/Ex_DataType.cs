using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace POEService.common
{
    public static class Ex_DataType
    {
        /// <summary>
        /// 简单加密
        /// </summary>
        /// <param name="data">待加密的数据</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        private static string Encrypt(this string data, string key)
        {
            byte[] keybytes = Encoding.UTF8.GetBytes(key);
            byte[] encryptdata = Encoding.UTF8.GetBytes(data);
            RijndaelManaged rdel = new RijndaelManaged();
            rdel.Key = keybytes;
            rdel.IV = keybytes;
            rdel.Mode = CipherMode.CBC;
            rdel.Padding = PaddingMode.PKCS7;
            var ic = rdel.CreateEncryptor();
            var result = ic.TransformFinalBlock(encryptdata, 0, encryptdata.Length);
            return Convert.ToBase64String(result);

        }
        /// <summary>
        /// 简单解密
        /// </summary>
        /// <param name="EncryptStr">待解密的数据</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        private static string Decrypt(this string EncryptStr, string key)
        {
            byte[] keybytes = Encoding.UTF8.GetBytes(key);

            byte[] encryptdata = Convert.FromBase64String(EncryptStr);
            RijndaelManaged rdel = new RijndaelManaged();
            rdel.Key = keybytes;
            rdel.IV = keybytes;
            rdel.Mode = CipherMode.CBC;
            rdel.Padding = PaddingMode.PKCS7;
            var ic = rdel.CreateDecryptor();
            var result = ic.TransformFinalBlock(encryptdata, 0, encryptdata.Length);
            return Encoding.UTF8.GetString(result);
        }
        public static string ToXmlString(this DataTable dt)
        {
            try
            {
                if (string.IsNullOrEmpty(dt.TableName))
                {
                    dt.TableName = "XMLTable";
                }
                MemoryStream stream = null;
                System.Xml.XmlTextWriter writer = null;
                try
                {
                    stream = new MemoryStream();
                    writer = new System.Xml.XmlTextWriter(stream, Encoding.UTF8);

                    dt.WriteXml(writer, XmlWriteMode.IgnoreSchema);
                    int count = (int)stream.Length;
                    byte[] arr = new byte[count];
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.Read(arr, 0, count);

                    return Encoding.UTF8.GetString(arr);
                }
                catch
                {
                    return string.Empty;
                }
                finally
                {
                    if (writer != null)
                        writer.Close();
                }
            }


            catch
            {
                return null;
            }
        }
        private static IOrderedQueryable<T> InternalOrder<T>(IQueryable<T> source, string propertyName, string methodName)
        {
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "p");
            PropertyInfo property = type.GetProperty(propertyName);
            Expression expr = Expression.Property(arg, property);
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), property.PropertyType);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            return (IOrderedQueryable<T>)typeof(Queryable).GetMethods().Single(
                p => string.Equals(p.Name, methodName, StringComparison.Ordinal)
                    && p.IsGenericMethodDefinition
                    && p.GetGenericArguments().Length == 2
                    && p.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.PropertyType)
                .Invoke(null, new object[] { source, lambda });
        }


        /// <summary>  
        /// 将指定的对象序列化成 JSON 数据。  
        /// </summary>  
        /// <param name="dt">要序列化的对象。</param>  
        /// <returns></returns>  
        public static List<T> ToModelList<T>(this DataTable dt) where T : class, new()
        {
            try
            {

                List<T> dl = new List<T>();
                var ps = typeof(T).GetProperties().Where(p => p.CanWrite && dt.Columns.Contains(p.Name)).ToList();

                foreach (DataRow dr in dt.Rows)
                {
                    T tmp = new T();
                    foreach (var p in ps)
                    {
                        try
                        {
                            p.SetValue(tmp, Convert.ChangeType(dr[p.Name], p.PropertyType, System.Globalization.CultureInfo.CurrentCulture), null);


                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                    }
                    dl.Add(tmp);
                }


                return dl;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }






        /// <summary>  
        /// 将指定的对象序列化成 JSON 数据。  
        /// </summary>  
        /// <param name="obj">要序列化的对象。</param>  
        /// <param name="DateTimeFormat">日期格式</param>
        /// <returns></returns>  
        public static string ToJson(this object obj, string DateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            try
            {
                if (null == obj)
                    return null;
                return JsonConvert.SerializeObject(obj, Formatting.None, CreateSetting(DateTimeFormat));
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
        /// <param name="DateTimeFormat">日期格式</param>
        /// <returns></returns>  
        public static T ToJsonObject<T>(this string json, string DateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            try
            {
                if (json == null)
                    return default;
                return JsonConvert.DeserializeObject<T>(json, CreateSetting(DateTimeFormat));
            }
            catch
            {
                return default;
            }
        }


        #region 私有方法

        private static object objtmp = new object();
        public static SortedList<string, JsonSerializerSettings> cachesetting { get; set; }
        private static JsonSerializerSettings CreateSetting(string DateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            lock (objtmp)
            {
                if (cachesetting == null)
                {
                    cachesetting = new SortedList<string, JsonSerializerSettings>();
                }
                if (!cachesetting.ContainsKey(DateTimeFormat))
                {

                    IsoDateTimeConverter datetimeConverter = new IsoDateTimeConverter();
                    datetimeConverter.DateTimeFormat = DateTimeFormat;

                    var _jsonSettings = new JsonSerializerSettings();
                    _jsonSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                    _jsonSettings.NullValueHandling = NullValueHandling.Ignore;
                    _jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    _jsonSettings.Converters.Add(datetimeConverter);
                    cachesetting.Add(DateTimeFormat, _jsonSettings);
                }
                return cachesetting[DateTimeFormat];
            }


        }

        #endregion

    }

}
