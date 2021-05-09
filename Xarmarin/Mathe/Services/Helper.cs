using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mathe.Services
{
    /// <summary>
    /// 帮助类
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// 根据资源文件名获取资源路径
        /// </summary>
        /// <param name="name">资源文件名</param>
        /// <returns>资源路径字符串</returns>
        public static string GetResourcePath(string name)
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder
                    .LocalApplicationData), name);
        }

        /// <summary>
        /// 从嵌入式资源中复制文件到系统中
        /// </summary>
        /// <param name="name">资源文件名</param>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        public static async Task CopyStreamFromEmbedded(string name)
        {
            var path = GetResourcePath(name);
            if (!File.Exists(path))
            {
                using var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                using var assetStream =
                    Assembly.GetExecutingAssembly().GetManifestResourceStream(nameof(Mathe) + "." + name);
                if (assetStream != null)
                {
                    await assetStream.CopyToAsync(fileStream);
                }
                else
                {
                    throw new DataException("There Are No {0} Be Embedded.".Replace("{0}", name));
                }
            }
        }
    }
}