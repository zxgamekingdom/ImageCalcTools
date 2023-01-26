using ImageCalcTools.Tools;

namespace ImageCalcTools.EqualRatioResize;

/// <summary>
///     输入同比缩放参数
/// </summary>
public class InputEqualRatioResizeParameters
{
    /// <summary>
    ///     缩放类型
    /// </summary>
    public enum Type
    {
        Width = 0, Height = 1
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
    public InputEqualRatioResizeParameters(ulong width, ulong height, Type resizeType, ulong targetValue)
    {
        //width>0
        CheckTools.MustGreaterThanZero(width, nameof(width));
        //height>0
        CheckTools.MustGreaterThanZero(height, nameof(height));
        //targetValue>0
        CheckTools.MustGreaterThanZero(targetValue, nameof(targetValue));
        Width = width;
        Height = height;
        ResizeType = resizeType;
        TargetValue = targetValue;
    }

    /// <summary>
    ///     当前图像的宽度
    /// </summary>
    public ulong Width { get; }

    /// <summary>
    ///     当前图像的高度
    /// </summary>
    public ulong Height { get; }

    /// <summary>
    ///     缩放类型
    /// </summary>
    public Type ResizeType { get; }

    /// <summary>
    ///     目标值
    /// </summary>
    public ulong TargetValue { get; }
}