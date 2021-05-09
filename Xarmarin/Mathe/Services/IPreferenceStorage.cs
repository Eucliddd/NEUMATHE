namespace Mathe.Services
{
    /// <summary>
    /// 偏好存储接口
    /// </summary>
    public interface IPreferenceStorage
    {
        /// <summary>
        /// 设置偏好
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void Set(string key, int value);

        /// <summary>
        /// 获取偏好
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        int Get(string key, int defaultValue);

        /// <summary>
        /// 设置偏好
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void Set(string key, string value);

        /// <summary>
        /// 获取偏好
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        string Get(string key, string defaultValue);
    }
}