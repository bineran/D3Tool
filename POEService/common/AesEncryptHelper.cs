using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace POEService.common
{
    public class AesEncryptHelper
    {
        public const string AES_OFB_NOPADDING = "AES/OFB/NoPadding";
        public static bool IsBase64String(string base64)
        {
            if (base64 != null && base64.Length % 4 != 0)
            {
                return false;
            }
            var b = Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,2}$");
            if (b)
            {
                Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
                return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
            }
            else
            {
                return false;
            }
        }



        public static string Decrypt(string content, string key, string iv, string algorithm = AES_OFB_NOPADDING)
        {
            try
            {
                if (key == null || key.Length != 32)
                {
                    throw new Exception("key length must 32 char ");
                }
                if (iv == null || iv.Length != 16)
                {
                    throw new Exception("iv length must 16 char ");
                }
                if (!IsBase64String(content))
                {
                    return null;

                }


                var keyparameter = ParameterUtilities.CreateKeyParameter("AES", Encoding.UTF8.GetBytes(key));
                var ivparameter = new ParametersWithIV(keyparameter, Encoding.UTF8.GetBytes(iv));

                var bodyBytes = Convert.FromBase64String(content);
                var cu = CipherUtilities.GetCipher(algorithm);
                cu.Init(false, ivparameter);
                var result = cu.DoFinal(bodyBytes);
                return Encoding.UTF8.GetString(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static string Encrypt(string content, string key, string iv, string algorithm = AES_OFB_NOPADDING)
        {
            try
            {
                if (key == null || key.Length != 32)
                {
                    throw new Exception("key length must 32 char ");
                }
                if (iv == null || iv.Length != 16)
                {
                    throw new Exception("iv length must 16 char ");
                }

                var keyparameter = ParameterUtilities.CreateKeyParameter("AES", Encoding.UTF8.GetBytes(key));
                var ivparameter = new ParametersWithIV(keyparameter, Encoding.UTF8.GetBytes(iv));

                var bodyBytes = Encoding.UTF8.GetBytes(content);
                var cu = CipherUtilities.GetCipher(algorithm);
                cu.Init(true, ivparameter);
                var result = cu.DoFinal(bodyBytes);
                return Convert.ToBase64String(result);

            }
            catch
            {
                throw new Exception("AesEncrypt Error");
            }
        }

        public static string NetCoreConfig256EncryptOrDecryptFile(string FilePath, string pwd, string iv, string DateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            var str = File.ReadAllText(FilePath, Encoding.UTF8);
            JObject obj = NetCoreConfig256EncryptJsonBody(JObject.Parse(str), pwd, iv);

            return obj.ToJson(DateTimeFormat); ;

        }
        public static string NetCoreConfig256EncryptFile(string FilePath, string pwd, string iv, string DateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            var str = File.ReadAllText(FilePath, Encoding.UTF8);
            JObject obj = NetCoreConfig256EncryptJsonBody(JObject.Parse(str), pwd, iv);
            return obj.ToJson(DateTimeFormat); ;

        }
        public static string NetCoreConfig256DecryptFile(string FilePath, string pwd, string iv, string DateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            var str = File.ReadAllText(FilePath, Encoding.UTF8);
            JObject obj = NetCoreConfig256DecryptJsonBody(JObject.Parse(str), pwd, iv);
            return obj.ToJson(DateTimeFormat); ;

        }
        public static string NetCoreConfig256EncryptString(string str, string pwd, string iv, string DateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            JObject obj = NetCoreConfig256EncryptJsonBody(JObject.Parse(str), pwd, iv);
            return obj.ToJson(DateTimeFormat); ;

        }
        public static string NetCoreConfig256DecryptString(string str, string pwd, string iv, string DateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            JObject obj = NetCoreConfig256DecryptJsonBody(JObject.Parse(str), pwd, iv);
            return obj.ToJson(DateTimeFormat); ;

        }


        public static JObject NetCoreConfig256EncryptJsonBody(JObject req, string pwd, string iv, string DateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            ForEncryptItem(req, pwd, iv);
            return req;
        }


        private static void ForEncryptItem(JToken p, string pwd, string iv)
        {

            foreach (JToken item in p.Children())
            {
                if (item.Count() > 0)
                {
                    ForEncryptItem(item, pwd, iv);
                }

                if (item.Type == JTokenType.String)
                {
                    var j = (JValue)item;
                    var str = j.Value.ToString();



                    j.Value = Encrypt(str, pwd, iv);

                }


            }
        }

        public static JObject NetCoreConfig256DecryptJsonBody(JObject req, string pwd, string iv)
        {
            ForDecryptItem(req, pwd, iv);
            return req;
        }

        private static void ForDecryptItem(JToken p, string pwd, string iv)
        {

            foreach (JToken item in p.Children())
            {
                if (item.Count() > 0)
                {
                    ForDecryptItem(item, pwd, iv);
                }

                if (item.Type == JTokenType.String)
                {
                    var j = (JValue)item;
                    var str = j.Value.ToString();


                    string AESDecryptStr = null;
                    try
                    {
                        AESDecryptStr = Decrypt(str, pwd, iv);
                    }
                    catch
                    {

                    }
                    if (AESDecryptStr != null)
                    {
                        j.Value = AESDecryptStr;
                    }

                }


            }
        }
    }
}
