using System;
namespace DorllyService.Common.Enums
{
    /// <summary>
    /// 可用状态
    /// </summary>
    public enum StateStatus
    {
        启用 = 1,
        禁用 = 0
    }

    /// <summary>
    /// 输入类型
    /// </summary>
    public enum InputType
    {
        文本 = 1,
        单选 = 2,
        多选 = 3
    }

    /// <summary>
    /// 服务属性类型
    /// </summary>
    public enum PropertyType
    {
        公共属性 = 1,
        分类属性 = 2
    }
}
