using System.Text.Json;
using System.Collections.Generic;

namespace ConsoleSample
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Extends
    {
        public static string ToJson(this object obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        /// <summary>
        /// JSON转对象
        /// </summary>
        /// <param name="str"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToObject<T>(this string str)
        {
            return JsonSerializer.Deserialize<T>(str);
        }

        /// <summary>
        /// 字段串转转集合
        /// </summary>
        /// <param name="str"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ToIEnumerable<T>(this string str)
        {
            return JsonSerializer.Deserialize<IEnumerable<T>>(str);
        }
    }
}