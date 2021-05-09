using Xamarin.Forms;

namespace Mathe.Services
{
    /// <summary>
    /// 导航上下文
    /// </summary>
    public class NavigationContext
    {
        /// <summary>
        /// 导航参数属性。
        /// </summary>
        public static readonly BindableProperty NavigationParameterProperty =
            BindableProperty.CreateAttached("NavigationParameter",
                typeof(object), typeof(NavigationContext), null,
                BindingMode.OneWayToSource);

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="value">值</param>
        public static void SetParameter(BindableObject page, object value) =>
            page.SetValue(NavigationParameterProperty, value);
    }
}