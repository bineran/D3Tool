
namespace DMTools
{
    /// <summary>
    /// 按钮点击类型
    /// </summary>
    public enum KeyClickType
    {
        /// <summary>
        /// 不做操作.
        /// </summary>
        不做操作 = 0,
        /// <summary>
        /// 每隔几秒按
        /// </summary>
        点击 = 10,
        /// <summary>
        /// 按住
        /// </summary>
        按下 = 20,
        /// <summary>
        /// 按住
        /// </summary>
        弹起 = 30,
        /// <summary>
        /// 颜色匹配按键
        /// </summary>
        颜色匹配点击 = 40,
        /// <summary>
        /// 颜色不匹配按键
        /// </summary>
        颜色不匹配点击 = 50,
        /// <summary>
        /// 图片找到点击
        /// </summary>
        图片找到点击 = 60,
        /// <summary>
        /// 图片未找到点击
        /// </summary>
        图片未找到点击 = 70,

    }
}
