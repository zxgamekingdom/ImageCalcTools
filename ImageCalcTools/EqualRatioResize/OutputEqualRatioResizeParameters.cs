namespace ImageCalcTools.EqualRatioResize;

/// <summary>
///     输出等比缩放参数
/// </summary>
public class OutputEqualRatioResizeParameters
{
    internal OutputEqualRatioResizeParameters()
    {
    }

    /// <summary>
    ///     源图像宽度
    /// </summary>
    public ulong Width { get; internal set; }

    /// <summary>
    ///     源图像高度
    /// </summary>
    public ulong Height { get; internal set; }

    /// <summary>
    ///     目标图像宽度
    /// </summary>
    public ulong TargetWidth { get; internal set; }

    /// <summary>
    ///     目标图像高度
    /// </summary>
    public ulong TargetHeight { get; internal set; }

    /// <summary>
    ///    缩放比例
    /// <remarks>
    ///缩放后的值 = 原值 * 比例
    /// </remarks>
    /// </summary>
    public decimal Ratio { get; internal set; }
}