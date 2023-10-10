using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace POEService.common
{
    public class JsonConfigurationAes256Provider : JsonConfigurationProvider
    {
        private string AesPassword { get; set; }
        public JsonConfigurationAes256Provider(JsonConfigurationAes256Source source) : base(source)
        {

            AesPassword = source.AesPassword;
            if (AesPassword != null)
            {
                AesPassword = AesPassword.Trim();
                if (AesPassword.Length < 32)
                {
                    AesPassword = AesPassword.PadRight(32, '*');
                }
                else
                {
                    AesPassword = AesPassword.Substring(0, 32);
                }
            }
        }


        public override void Load(Stream stream)
        {
            base.Load(stream);
            // 读取配置文件需要的节点数据，然后解密
            // 请自已实现加密和解密的方法，这里未提供加密/解密的实现


            if (AesPassword != null)
            {
                List<string> alkey = new List<string>();
                alkey.AddRange(Data.Keys.ToArray());
                for (int i = 0; i < alkey.Count; i++)
                {
                    var str = Data[alkey[i]];
                    try
                    {
                        if (str != null)
                        {
                            try
                            {
                                var result = AesEncryptHelper.Decrypt(str, AesPassword, AesPassword.Substring(0, 16));
                                //var result = NETCore.Encrypt.EncryptProvider.AESDecrypt(str, AesPassword);
                                if (result != null)
                                {
                                    Data[alkey[i]] = result;
                                }
                            }
                            catch
                            {

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        var data = new
                        {
                            Key = alkey[i],
                            Body = str,
                            ExceptionMessage = ex.Message
                        }.ToJson();
                        Console.WriteLine(data);
                    }


                }
            }


        }



    }



    public class JsonConfigurationAes256Source : JsonConfigurationSource
    {
        public string AesPassword { get; set; }
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);

            return new JsonConfigurationAes256Provider(this);
        }
    }

    public static class JsonConfigurationAes256Extensions
    {
        public static IConfigurationBuilder AddJsonFileAes256(this IConfigurationBuilder builder, string path, bool optional,
            bool reloadOnChange)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("File path must be a non-empty string.");
            }

            var source = new JsonConfigurationAes256Source
            {
                FileProvider = null,
                Path = path,
                Optional = optional,
                ReloadOnChange = reloadOnChange
            };

            source.ResolveFileProvider();
            builder.Add(source);
            return builder;
        }
        public static IConfigurationBuilder AddJsonFileAes256(this IConfigurationBuilder builder, string path, bool optional,
         bool reloadOnChange, string aesPassword)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("File path must be a non-empty string.");
            }

            var source = new JsonConfigurationAes256Source
            {
                FileProvider = null,
                Path = path,
                Optional = optional,
                ReloadOnChange = reloadOnChange,
                AesPassword = aesPassword
            };

            source.ResolveFileProvider();
            builder.Add(source);
            return builder;
        }
    }



}
