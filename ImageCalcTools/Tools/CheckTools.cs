using System;

namespace ImageCalcTools.Tools;

public static class CheckTools
{
    /// <summary>
    ///     必须大于0
    /// </summary>
    /// <param name="value"></param>
    /// <param name="name">
    ///     参数名
    /// </param>
    /// <exception cref="ArgumentException"></exception>
    public static void MustGreaterThanZero(ulong value, string name)
    {
        if (value <= 0)
            throw new ArgumentException($"{name}必须大于0");
    }

    /// <summary>
    ///     必须大于等于
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <param name="name1">
    ///     参数名1
    /// </param>
    /// <param name="name2">
    ///     参数名2
    /// </param>
    /// <exception cref="ArgumentException"></exception>
    public static void MustGreaterThanOrEqual(ulong value1, ulong value2, string name1, string name2)
    {
        if (value1 < value2)
            throw new ArgumentException($"{name1}必须大于等于{name2}");
    }

    /// <summary>
    ///     必须大于
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <param name="name1">
    ///     参数名1
    /// </param>
    /// <param name="name2">
    ///     参数名2
    /// </param>
    /// <exception cref="ArgumentException"></exception>
    public static void MustGreaterThan(ulong value1, ulong value2, string name1, string name2)
    {
        if (value1 <= value2)
            throw new ArgumentException($"{name1}必须大于{name2}");
    }
}