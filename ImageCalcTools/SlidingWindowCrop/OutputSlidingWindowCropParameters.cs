﻿namespace ImageCalcTools.SlidingWindowCrop;

/// <summary>
///     计算得到的裁切输出参数
/// </summary>
public class OutputSlidingWindowCropParameters
{
    internal OutputSlidingWindowCropParameters()
    {
    }

    /// <summary>
    ///     <inheritdoc cref="InputSlidingWindowCropParameters.Width" />
    /// </summary>
    public ulong Width { get; internal set; }

    /// <summary>
    ///     <inheritdoc cref="InputSlidingWindowCropParameters.Height" />
    /// </summary>
    public ulong Height { get; internal set; }

    /// <summary>
    ///     <inheritdoc cref="InputSlidingWindowCropParameters.CropWidth" />
    /// </summary>
    public ulong CropWidth { get; internal set; }

    /// <summary>
    ///     <inheritdoc cref="InputSlidingWindowCropParameters.CropHeight" />
    /// </summary>
    public ulong CropHeight { get; internal set; }

    /// <summary>
    ///     <inheritdoc cref="InputSlidingWindowCropParameters.OverlapWidth" />
    /// </summary>

    public decimal OverlapWidth { get; internal set; }

    /// <summary>
    ///     <inheritdoc cref="InputSlidingWindowCropParameters.OverlapHeight" />
    /// </summary>
    public decimal OverlapHeight { get; internal set; }

    /// <summary>
    ///     横向裁切次数
    /// </summary>
    public ulong HorizontalCropCount => HorizontalBlockCount - 1;

    /// <summary>
    ///     纵向裁切次数
    /// </summary>
    public ulong VerticalCropCount => VerticalBlockCount - 1;

    /// <summary>
    ///     横向块数量
    /// </summary>
    public ulong HorizontalBlockCount { get; internal set; }

    /// <summary>
    ///     纵向块数量
    /// </summary>
    public ulong VerticalBlockCount { get; internal set; }

    /// <summary>
    ///     水平滑动的距离
    /// </summary>
    public decimal HorizontalSlideDistance => HorizontalCropCount == 0 ? 0 : CropWidth - OverlapWidth;

    /// <summary>
    ///     垂直滑动的距离
    /// </summary>
    public decimal VerticalSlideDistance => VerticalCropCount == 0 ? 0 : CropHeight - OverlapHeight;
}