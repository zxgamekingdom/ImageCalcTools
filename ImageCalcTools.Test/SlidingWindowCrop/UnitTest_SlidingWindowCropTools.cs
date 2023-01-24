using System.Diagnostics.CodeAnalysis;
using ImageCalcTools.SlidingWindowCrop;

namespace ImageCalcTools.Test.SlidingWindowCrop;

/// <summary>
///     UnitTest SlidingWindowCropTools
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class UnitTest_SlidingWindowCropTools
{
    [Fact]
    public void Test_Calc()
    {
        const int testCount = 10000;
        for (var index = 0; index < testCount; index++)
        {
            var ints = UnitTest_InputCropParameters.生成正常参数();
            var i = new InputSlidingWindowCropParameters(ints[0], ints[1], ints[2], ints[3], 0, 0);
            var o = SlidingWindowCropTools.Calc(i);
            Assert.Equal(i.Height, o.Height);
            Assert.Equal(i.Width, o.Width);
            Assert.Equal(i.CropHeight, o.CropHeight);
            Assert.Equal(i.CropWidth, o.CropWidth);
            Assert.True(i.OverlapHeight <= o.OverlapHeight);
            Assert.True(i.OverlapWidth <= o.OverlapWidth);
            Assert.True(o.OverlapHeight <= o.CropHeight);
            Assert.True(o.OverlapWidth <= o.CropWidth);
            Assert.Equal(o.HorizontalBlockCount, o.HorizontalCropCount + 1);
            Assert.Equal(o.VerticalBlockCount, o.VerticalCropCount + 1);
            Assert.Equal((ulong)0, i.OverlapHeight);
            Assert.Equal((ulong)0, i.OverlapWidth);
            Assert.Equal(o.HorizontalSlideDistance, o.CropWidth - o.OverlapWidth);
            Assert.Equal(o.VerticalSlideDistance, o.CropHeight - o.OverlapHeight);
        }

        for (var index = 0; index < testCount; index++)
        {
            var ints = UnitTest_InputCropParameters.生成正常参数();
            var i = new InputSlidingWindowCropParameters(ints[0], ints[1], ints[2], ints[3], ints[4], ints[5]);
            var o = SlidingWindowCropTools.Calc(i);
            Assert.True(o.VerticalCropCount >= i.Height / i.CropHeight);
            Assert.True(o.HorizontalCropCount >= i.Width / i.CropWidth);
            Assert.True(o.HorizontalCropCount * o.CropWidth >= o.Width);
            Assert.True(o.VerticalCropCount * o.CropHeight >= o.Height);
            Assert.True(o.OverlapWidth <= o.CropWidth);
            Assert.True(o.OverlapHeight <= o.CropHeight);
            Assert.Equal(o.HorizontalBlockCount, o.HorizontalCropCount + 1);
            Assert.Equal(o.VerticalBlockCount, o.VerticalCropCount + 1);
            Assert.Equal(i.Height, o.Height);
            Assert.Equal(i.Width, o.Width);
            Assert.Equal(i.CropHeight, o.CropHeight);
            Assert.Equal(i.CropWidth, o.CropWidth);
            Assert.True(o.OverlapHeight >= i.OverlapHeight);
            Assert.True(o.OverlapWidth >= i.OverlapWidth);
            Assert.Equal(o.HorizontalSlideDistance, o.CropWidth - o.OverlapWidth);
            Assert.Equal(o.VerticalSlideDistance, o.CropHeight - o.OverlapHeight);
        }
    }
}