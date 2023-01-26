using System;

namespace ImageCalcTools.EqualRatioResize;

public static class EqualRatioResizeTools
{
    /// <summary>
    ///     计算等比缩放参数
    /// </summary>
    /// <param name="inputParameters"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static OutputEqualRatioResizeParameters Calc(InputEqualRatioResizeParameters inputParameters)
    {
        var w = inputParameters.Width;
        var h = inputParameters.Height;
        var type = inputParameters.ResizeType;
        var t = inputParameters.TargetValue;
        var ratio = type switch
        {
            InputEqualRatioResizeParameters.Type.Width => (decimal)t / w,
            InputEqualRatioResizeParameters.Type.Height => (decimal)t / h,
            _ => throw new ArgumentOutOfRangeException()
        };
        var targetWidth = (ulong)(w * ratio);
        var targetHeight = (ulong)(h * ratio);
        return new OutputEqualRatioResizeParameters
        {
            Width = w,
            Height = h,
            TargetWidth = targetWidth,
            TargetHeight = targetHeight,
            Ratio = ratio
        };
    }
}