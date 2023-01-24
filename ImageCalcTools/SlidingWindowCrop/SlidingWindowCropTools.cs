using System;

namespace ImageCalcTools.SlidingWindowCrop;

public static class SlidingWindowCropTools
{
    /// <summary>
    ///     计算裁切参数
    /// </summary>
    /// <remarks>
    ///     优先保证裁切尺寸,其次保证重叠部分尺寸
    ///     <para></para>
    ///     如果裁切次数不是整数,向上取整
    ///     <para></para>
    ///     如果重叠部分小于指定的重叠部分,裁切次数+1
    ///     <para></para>
    ///     即使设置了重叠部分尺寸为0,如果裁切次数不是整数,也会向上取整,导致重叠部分不为0
    /// </remarks>
    /// <param name="inputParameters"></param>
    /// <returns></returns>
    public static OutputSlidingWindowCropParameters Calc(InputSlidingWindowCropParameters inputParameters)
    {
        checked
        {
            var w = inputParameters.Width;
            var h = inputParameters.Height;
            var cw = inputParameters.CropWidth;
            var ch = inputParameters.CropHeight;
            var ow = inputParameters.OverlapWidth;
            var oh = inputParameters.OverlapHeight;
            var output =
                new OutputSlidingWindowCropParameters { Width = w, Height = h, CropWidth = cw, CropHeight = ch };
            var horizontalCropCount = w / (decimal)cw;
            var verticalCropCount = h / (decimal)ch;
            //切割次数向上取整
            horizontalCropCount = Math.Ceiling(horizontalCropCount);
            verticalCropCount = Math.Ceiling(verticalCropCount);
            CALC_HORIZONTAL:
            //计算重叠部分尺寸
            var horizontalOverlap = (horizontalCropCount * cw - w) / horizontalCropCount;
            var verticalOverlap = (verticalCropCount * ch - h) / verticalCropCount;
            if (horizontalOverlap >= ow)
            {
            }
            else
            {
                horizontalCropCount++;
                goto CALC_HORIZONTAL;
            }

            if (verticalOverlap >= oh)
            {
            }
            else
            {
                verticalCropCount++;
                goto CALC_HORIZONTAL;
            }

            output.HorizontalCropCount = (ulong)horizontalCropCount;
            output.VerticalCropCount = (ulong)verticalCropCount;
            output.OverlapWidth = horizontalOverlap;
            output.OverlapHeight = verticalOverlap;
            return output;
        }
    }
}