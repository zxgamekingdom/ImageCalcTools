using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using ImageCalcTools.SlidingWindowCrop;

namespace ImageCalcTools.Test.SlidingWindowCrop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class UnitTest_InputCropParameters
{
    [Fact]
    public void Test_Ctor()
    {
        const int testCount = 10000;
        {
            var enumerable = 生成异常的参数(testCount);
            foreach (var (w, h, cw, ch, ow, oh) in enumerable)
                Assert.Throws<ArgumentException>(() => new InputSlidingWindowCropParameters(w, h, cw, ch, ow, oh));
        }
        {
            var enumerable = 生成正常参数(testCount);
            foreach (var (w, h, cw, ch, ow, oh) in enumerable)
            {
                var i = new InputSlidingWindowCropParameters(w, h, cw, ch, ow, oh);
                Assert.Equal(w, i.Width);
                Assert.Equal(h, i.Height);
                Assert.Equal(cw, i.CropWidth);
                Assert.Equal(ch, i.CropHeight);
                Assert.Equal(ow, i.OverlapWidth);
                Assert.Equal(oh, i.OverlapHeight);
                //w>=1
                Assert.True(i.Width >= 1);
                //h>=1
                Assert.True(i.Height >= 1);
                //cw>=1
                Assert.True(i.CropWidth >= 1);
                //ch>=1
                Assert.True(i.CropHeight >= 1);
                //ow>=0
                Assert.True(i.OverlapWidth >= 0);
                //oh>=0
                Assert.True(i.OverlapHeight >= 0);
                //cw<=w
                Assert.True(i.CropWidth <= i.Width);
                //ch<=h
                Assert.True(i.CropHeight <= i.Height);
                //ow<cw
                Assert.True(i.OverlapWidth < i.CropWidth);
                //oh<ch
                Assert.True(i.OverlapHeight < i.CropHeight);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (ulong w, ulong h, ulong cw, ulong ch, ulong ow, ulong oh)[] 生成正常参数(ulong testCount)
    {
        var random = Random.Shared;
        var list = new List<(ulong w, ulong h, ulong cw, ulong ch, ulong ow, ulong oh)>();
        for (ulong index = 0; index < testCount; index++)
        {
            var w = random.Next(2, int.MaxValue);
            var h = random.Next(2, int.MaxValue);
            var cw = random.Next(1, w + 1);
            var ch = random.Next(1, h + 1);
            var ow = random.Next(0, cw);
            var oh = random.Next(0, ch);
            list.Add(((ulong)w, (ulong)h, (ulong)cw, (ulong)ch, (ulong)ow, (ulong)oh));
        }

        return list.ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (ulong w, ulong h, ulong cw, ulong ch, ulong ow, ulong oh)[] 生成异常的参数(ulong testCount)
    {
        var random = Random.Shared;
        var list = new List<(ulong w, ulong h, ulong cw, ulong ch, ulong ow, ulong oh)>();
        const int spanLength = 6;
        Span<ulong> span = stackalloc ulong[spanLength];
        for (ulong index = 0; index < testCount; index++)
        {
            for (var i = 0; i < spanLength; i++)
                span[i] = (ulong)random.Next(int.MaxValue);

            //ints0-3随机一个为0
            span[random.Next(4)] = 0;
            list.Add((span[0], span[1], span[2], span[3], span[4], span[5]));
        }

        return list.ToArray();
    }
}