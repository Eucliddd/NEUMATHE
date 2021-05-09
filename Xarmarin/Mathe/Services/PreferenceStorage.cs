using Xamarin.Essentials;

namespace Mathe.Services
{
    /// <summary>
    /// 偏好存储
    /// </summary>
    public class PreferenceStorage : IPreferenceStorage
    {
        /// <summary>
        /// 设置偏好
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Set(string key, int value) => Preferences.Set(key, value);

        /// <summary>
        /// 获取偏好
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public int Get(string key, int defaultValue) => Preferences.Get(key, defaultValue);

        /// <summary>
        /// 设置偏好
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Set(string key, string value) => Preferences.Set(key, value);

        /// <summary>
        /// 获取偏好
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public string Get(string key, string defaultValue) => Preferences.Get(key, defaultValue);
    }
}