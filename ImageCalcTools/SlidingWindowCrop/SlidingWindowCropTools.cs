using System.Collections.Generic;

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
            decimal width = inputParameters.Width;
            decimal height = inputParameters.Height;
            decimal cropWidth = inputParameters.CropWidth;
            decimal cropHeight = inputParameters.CropHeight;
            decimal overlapWidth = inputParameters.OverlapWidth;
            decimal overlapHeight = inputParameters.OverlapHeight;
            //w=cw*hbc-ow*(hbc-1)
            //h=ch*vbc-oh*(vbc-1)
            //横向块数
            var hbc = -(width - overlapWidth) / (overlapWidth - cropWidth);
            //纵向块数
            var vbc = -(height - overlapHeight) / (overlapHeight - cropHeight);
            //横向块数向上取整
            var hCc = (ulong)decimal.Ceiling(hbc);
            //纵向块数向上取整
            var vCc = (ulong)decimal.Ceiling(vbc);
            //横向重叠部分
            var ow = hCc == 1 ? 0 : (cropWidth * hCc - width) / (hCc - 1);
            //纵向重叠部分
            var oh = vCc == 1 ? 0 : (cropHeight * vCc - height) / (vCc - 1);
            return new OutputSlidingWindowCropParameters
            {
                Height = (ulong)height,
                Width = (ulong)width,
                CropHeight = (ulong)cropHeight,
                CropWidth = (ulong)cropWidth,
                OverlapHeight = oh,
                OverlapWidth = ow,
                HorizontalBlockCount = hCc,
                VerticalBlockCount = vCc
            };
        }
    }

    public static SlidingWindowCropRect[] GenSlidingWindowCropRects(OutputSlidingWindowCropParameters parameters)
    {
        var cw = parameters.CropWidth;
        var ch = parameters.CropHeight;
        var hc = parameters.HorizontalBlockCount;
        var vc = parameters.VerticalBlockCount;
        var hsd = parameters.HorizontalSlideDistance;
        var vsd = parameters.VerticalSlideDistance;
        var total = hc * vc;
        var rectList = new List<SlidingWindowCropRect>((int)total);
        for (ulong v = 0; v < vc; v++)
        for (ulong h = 0; h < hc; h++)
        {
            var row = v * vsd;
            var col = h * hsd;
            var index = v * hc + h;
            var rect = new SlidingWindowCropRect(index, v, h, row, col, cw, ch);
            rectList.Add(rect);
        }

        return rectList.ToArray();
    }
}