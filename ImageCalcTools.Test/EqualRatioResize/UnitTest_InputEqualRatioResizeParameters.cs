using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using ImageCalcTools.EqualRatioResize;

namespace ImageCalcTools.Test.EqualRatioResize;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class UnitTest_InputEqualRatioResizeParameters
{
    //Test_Ctor
    [Fact]
    public void Test_Ctor()
    {
        const int testCount = 10000;
        {
            foreach (var (w, h, type, t) in 生成异常参数(testCount))
                Assert.Throws<ArgumentException>(() => new InputEqualRatioResizeParameters(w, h, type, t));
        }
        {
            foreach (var (w, h, type, t) in 生成正常参数(testCount))
            {
                var i = new InputEqualRatioResizeParameters(w, h, type, t);
                Assert.Equal(w, i.Width);
                Assert.Equal(h, i.Height);
                Assert.Equal(type, i.ResizeType);
                Assert.Equal(t, i.TargetValue);
                //w>=1
                Assert.True(i.Width >= 1);
                //h>=1
                Assert.True(i.Height >= 1);
                //t>=1
                Assert.True(i.TargetValue >= 1);
                //type only 0 or 1
                Assert.True(i.ResizeType is InputEqualRatioResizeParameters.Type.Width
                    or InputEqualRatioResizeParameters.Type.Height);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static (ulong w, ulong h, InputEqualRatioResizeParameters.Type type, ulong t)[] 生成异常参数(ulong testCount)
    {
        var list = new List<(ulong w, ulong h, InputEqualRatioResizeParameters.Type type, ulong t)>();
        var random = Random.Shared;
        const int spanLength = 3;
        Span<ulong> span = stackalloc ulong[spanLength];
        for (ulong i = 0; i < testCount; i++)
        {
            for (var index = 0; index < spanLength; index++)
                span[index] = (ulong)random.Next(int.MaxValue);
            var t = (InputEqualRatioResizeParameters.Type)random.Next(0, 2);
            //span[0-2]随机一个为0
            span[random.Next(3)] = 0;
            list.Add((span[0], span[1], t, span[2]));
        }

        return list.ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (ulong w, ulong h, InputEqualRatioResizeParameters.Type type, ulong t)[] 生成正常参数(ulong testCount)
    {
        var list = new List<(ulong w, ulong h, InputEqualRatioResizeParameters.Type type, ulong t)>();
        var random = Random.Shared;
        for (ulong i = 0; i < testCount; i++)
        {
            var w = (ulong)random.Next(1, int.MaxValue);
            var h = (ulong)random.Next(1, int.MaxValue);
            var t = (InputEqualRatioResizeParameters.Type)random.Next(0, 2);
            var tV = (ulong)random.Next(1, int.MaxValue);
            list.Add((w, h, t, tV));
        }

        return list.ToArray();
    }
}