using System;
using ImageCalcTools.Tools;

namespace ImageCalcTools.SlidingWindowCrop;

/// <summary>
///     输入裁切参数
/// </summary>
public class InputSlidingWindowCropParameters
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
    public InputSlidingWindowCropParameters(ulong width, ulong height, ulong cropWidth, ulong cropHeight,
        ulong overlapWidth, ulong overlapHeight)
    {
        验证参数(width, height, cropWidth, cropHeight, overlapWidth, overlapHeight);
        Height = height;
        Width = width;
        CropWidth = cropWidth;
        CropHeight = cropHeight;
        OverlapWidth = overlapWidth;
        OverlapHeight = overlapHeight;
    }

    /// <summary>
    ///     图片宽
    /// </summary>
    public ulong Width { get; }

    /// <summary>
    ///     图片高
    /// </summary>
    public ulong Height { get; }

    /// <summary>
    ///     每块小图片的宽度
    /// </summary>
    public ulong CropWidth { get; }

    /// <summary>
    ///     每块小图片的高度
    /// </summary>
    public ulong CropHeight { get; }

    /// <summary>
    ///     每块小图片重复的宽度
    /// </summary>
    public ulong OverlapWidth { get; }

    /// <summary>
    ///     每块小图片重复的高度
    /// </summary>
    public ulong OverlapHeight { get; }

    private static void 验证参数(ulong width, ulong height, ulong cropWidth, ulong cropHeight, ulong overlapWidth,
        ulong overlapHeight)
    {
        CheckTools.MustGreaterThanZero(width, nameof(width));
        CheckTools.MustGreaterThanZero(height, nameof(height));
        CheckTools.MustGreaterThanZero(cropWidth, nameof(cropWidth));
        CheckTools.MustGreaterThanZero(cropHeight, nameof(cropHeight));
        //width must>=cropWidth
        CheckTools.MustGreaterThanOrEqual(width, cropWidth, nameof(width), nameof(cropWidth));
        //height must>=cropHeight
        CheckTools.MustGreaterThanOrEqual(height, cropHeight, nameof(height), nameof(cropHeight));
        //must cropWidth>overlapWidth
        CheckTools.MustGreaterThan(cropWidth, overlapWidth, nameof(cropWidth), nameof(overlapWidth));
        //must cropHeight>overlapHeight
        CheckTools.MustGreaterThan(cropHeight, overlapHeight, nameof(cropHeight), nameof(overlapHeight));
    }

    private static void 必须大于0(ulong num, string name)
    {
        if (num <= 0)
            throw new ArgumentException($"{name}必须大于0");
    }
}