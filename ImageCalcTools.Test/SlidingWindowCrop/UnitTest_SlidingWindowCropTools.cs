using System.Diagnostics.CodeAnalysis;
using ImageCalcTools.SlidingWindowCrop;

namespace ImageCalcTools.Test.SlidingWindowCrop;

/// <summary>
///     UnitTest SlidingWindowCropTools
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
public class UnitTest_SlidingWindowCropTools
{
    [Fact]
    public void Test_Calc()
    {
        const ulong testCount = 10000;
        {
            foreach (var (w, h, cw, ch, _, _) in UnitTest_InputCropParameters.生成正常参数(testCount))
            {
                var i = new InputSlidingWindowCropParameters(w, h, cw, ch, 0, 0);
                var o = SlidingWindowCropTools.Calc(i);
                //i.Width==o.Width
                Assert.Equal(i.Width, o.Width);
                //i.Height==o.Height
                Assert.Equal(i.Height, o.Height);
                //i.CropWidth==o.CropWidth
                Assert.Equal(i.CropWidth, o.CropWidth);
                //i.CropHeight==o.CropHeight
                Assert.Equal(i.CropHeight, o.CropHeight);
                //i.OverlapWidth<=o.OverlapWidth
                Assert.True(i.OverlapWidth <= o.OverlapWidth);
                //i.OverlapHeight<=o.OverlapHeight
                Assert.True(i.OverlapHeight <= o.OverlapHeight);
                //o.HorizontalBlockCount>=1
                Assert.True(o.HorizontalBlockCount >= 1);
                //o.VerticalBlockCount>=1
                Assert.True(o.VerticalBlockCount >= 1);
                //o.HorizontalCropCount>=0
                Assert.True(o.HorizontalCropCount >= 0);
                //o.VerticalCropCount>=0
                Assert.True(o.VerticalCropCount >= 0);
                //o.HorizontalCropCount==o.HorizontalBlockCount-1
                Assert.Equal(o.HorizontalCropCount, o.HorizontalBlockCount - 1);
                //o.VerticalCropCount==o.VerticalBlockCount-1
                Assert.Equal(o.VerticalCropCount, o.VerticalBlockCount - 1);
                if (o.HorizontalBlockCount > 2)
                    //(o.CropWidth*(o.HorizontalBlockCount-1)-o.Width)/(o.HorizontalBlockCount-1-1)<=o.OverlapWidth
                    Assert.True(((decimal)o.CropWidth * (o.HorizontalBlockCount - 1) - o.Width) /
                        (o.HorizontalBlockCount - 1 - 1) <=
                        i.OverlapWidth);
                if (o.VerticalCropCount > 2)
                    //(o.CropHeight*(o.VerticalBlockCount-1)-o.Height)/(o.VerticalBlockCount-1-1)<=o.OverlapHeight
                    Assert.True(((decimal)o.CropHeight * (o.VerticalBlockCount - 1) - o.Height) /
                        (o.VerticalBlockCount - 1 - 1) <=
                        i.OverlapHeight);

                //(o.CropWidth*(o.HorizontalBlockCount+1)-o.Width)/(o.HorizontalBlockCount+1-1)>=o.OverlapWidth
                Assert.True(((decimal)o.CropWidth * o.HorizontalBlockCount + 1 - o.Width) /
                    (o.HorizontalBlockCount + 1 - 1) >=
                    i.OverlapWidth);
                //(o.CropHeight*(o.VerticalBlockCount+1)-o.Height)/(o.VerticalBlockCount+1-1)>=o.OverlapHeight
                Assert.True(((decimal)o.CropHeight * o.VerticalBlockCount + 1 - o.Height) /
                    (o.VerticalBlockCount + 1 - 1) >=
                    i.OverlapHeight);
            }
        }
        {
            foreach (var (w, h, _, _, _, _) in UnitTest_InputCropParameters.生成正常参数(testCount))
            {
                var i = new InputSlidingWindowCropParameters(w, h, w, h, 0, 0);
                var o = SlidingWindowCropTools.Calc(i);
                //o.HorizontalBlockCount = 1;
                Assert.Equal((ulong)1, o.HorizontalBlockCount);
                //o.VerticalBlockCount = 1;
                Assert.Equal((ulong)1, o.VerticalBlockCount);
                //o.HorizontalCropCount = 0;
                Assert.Equal((ulong)0, o.HorizontalCropCount);
                //o.VerticalCropCount = 0;
                Assert.Equal((ulong)0, o.VerticalCropCount);
                //o.HorizontalSlideDistance = 0;
                Assert.Equal(0, o.HorizontalSlideDistance);
                //o.VerticalSlideDistance = 0;
                Assert.Equal(0, o.VerticalSlideDistance);
                //o.OverlapWidth = 0;
                Assert.Equal(0, o.OverlapWidth);
                //o.OverlapHeight = 0;
                Assert.Equal(0, o.OverlapHeight);
                //o.CropWidth = i.CropWidth;
                Assert.Equal(i.CropWidth, o.CropWidth);
                //o.CropHeight = i.CropHeight;
                Assert.Equal(i.CropHeight, o.CropHeight);
                //o.Width = i.Width;
                Assert.Equal(i.Width, o.Width);
                //o.Height = i.Height;
                Assert.Equal(i.Height, o.Height);
            }
        }
        {
            foreach (var (w, h, cw, ch, ow, oh) in UnitTest_InputCropParameters.生成正常参数(testCount))
            {
                var i = new InputSlidingWindowCropParameters(w, h, cw, ch, ow, oh);
                var o = SlidingWindowCropTools.Calc(i);
                Assert.True(o.VerticalBlockCount >= i.Height / i.CropHeight);
                Assert.True(o.HorizontalBlockCount >= i.Width / i.CropWidth);
                Assert.True(o.HorizontalBlockCount * o.CropWidth >= o.Width);
                Assert.True(o.VerticalBlockCount * o.CropHeight >= o.Height);
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

    //Test_GenSlidingWindowCropRects
    [Fact]
    public void Test_GenSlidingWindowCropRects()
    {
        const ulong testCount = 100;
        foreach (var (w, h, cw, ch, ow, oh) in UnitTest_InputCropParameters.生成正常参数(testCount))
        {
            var input = new InputSlidingWindowCropParameters(w, h, cw, ch, ow, oh);
            var output = SlidingWindowCropTools.Calc(input);
            var rects = SlidingWindowCropTools.GenSlidingWindowCropRects(output);
            //rects.Count == output.HorizontalBlockCount * output.VerticalBlockCount
            Assert.Equal(output.HorizontalBlockCount * output.VerticalBlockCount, (ulong)rects.Length);
            //rects.All(x => x.TopLeftRow >= 0)
            Assert.True(rects.All(x => x.TopLeftRow >= 0));
            //rects.All(x => x.TopLeftColumn >= 0)
            Assert.True(rects.All(x => x.TopLeftColumn >= 0));
            //rects.All(x => x.BottomRightRow >= 0)
            Assert.True(rects.All(x => x.BottomRightRow >= 0));
            //rects.All(x => x.BottomRightColumn >= 0)
            Assert.True(rects.All(x => x.BottomRightColumn >= 0));
            //rects.All(x => x.TopLeftRow <= x.BottomRightRow)
            Assert.True(rects.All(x => x.TopLeftRow <= x.BottomRightRow));
            //rects.All(x => x.TopLeftColumn <= x.BottomRightColumn)
            Assert.True(rects.All(x => x.TopLeftColumn <= x.BottomRightColumn));
            //rects.All(x => x.Width == output.CropWidth)
            Assert.True(rects.All(x => x.Width == output.CropWidth));
            //rects.All(x => x.Height == output.CropHeight)
            Assert.True(rects.All(x => x.Height == output.CropHeight));
            //Math.Abs(i.Height - rects.Max(x => x.BottomRightRow))<=1
            Assert.True(Math.Abs(input.Height - rects.Max(x => x.BottomRightRow)) <= 1);
            //Math.Abs(i.Width - rects.Max(x => x.BottomRightColumn))<=1
            Assert.True(Math.Abs(input.Width - rects.Max(x => x.BottomRightColumn)) <= 1);
        }
    }
}