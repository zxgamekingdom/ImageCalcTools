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
            for (var i = 0; i < testCount; i++)
            {
                var ints = 生成异常的参数();
                if (ints.Length != 6 && ints.Any(x => x <= 0) is false)
                    throw new Exception("生成异常的参数错误");
                Assert.Throws<ArgumentException>(() => new InputSlidingWindowCropParameters(ints[0], ints[1], ints[2],
                    ints[3], ints[4], ints[5]));
            }
        }
        {
            for (var i = 0; i < testCount; i++)
            {
                var ints = 生成正常参数();
                if ((ints is [var w, var h, var cw, var ch, var ow, var oh] &&
                        w >= cw &&
                        h >= ch &&
                        cw > ow &&
                        ch > oh &&
                        w > 0 &&
                        h > 0) is false)
                    throw new Exception("生成正常参数错误");
                _ = new InputSlidingWindowCropParameters(ints[0], ints[1], ints[2], ints[3], ints[4], ints[5]);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong[] 生成正常参数()
    {
        var random = Random.Shared;
        var w = random.Next(2, int.MaxValue);
        var h = random.Next(2, int.MaxValue);
        var cw = random.Next(1, w + 1);
        var ch = random.Next(1, h + 1);
        var ow = random.Next(0, cw);
        var oh = random.Next(0, ch);
        var ints = new[] { w, h, cw, ch, ow, oh };
        return ints.Select(i => (ulong)i).ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong[] 生成异常的参数()
    {
        var random = Random.Shared;
        var ints = new ulong[6];
        for (var i = 0; i < ints.Length; i++)
            ints[i] = (ulong)random.Next(int.MaxValue);

        //ints0-3随机一个为0
        ints[random.Next(4)] = 0;
        return ints;
    }
}