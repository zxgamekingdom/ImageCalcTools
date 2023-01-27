using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using ImageCalcTools.SlidingWindowCrop;

namespace ImageCalcTools.Test.SlidingWindowCrop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
public class UnitTest_SlidingWindowCropRect
{
    //Test_Ctor
    [Fact]
    public void Test_Ctor()
    {
        const ulong testCount = 10000;
        {
            foreach (var (index, rowIndex, columnIndex, topLeftRow, topLeftColumn, width, height) in 生成异常参数(testCount))
                Assert.Throws<ArgumentException>(() =>
                    new SlidingWindowCropRect(index, rowIndex, columnIndex, topLeftRow, topLeftColumn, width, height));
        }
        {
            foreach (var (index, rowIndex, columnIndex, topLeftRow, topLeftColumn, width, height) in 生成正常参数(testCount))
            {
                var i = new SlidingWindowCropRect(index, rowIndex, columnIndex, topLeftRow, topLeftColumn, width,
                    height);
                //i.Index>=0
                Assert.True(i.Index >= 0);
                //i.TopLeftRow>=0
                Assert.True(i.TopLeftRow >= 0);
                //i.TopLeftColumn>=0
                Assert.True(i.TopLeftColumn >= 0);
                //i.Width>=1
                Assert.True(i.Width >= 1);
                //i.Height>=1
                Assert.True(i.Height >= 1);
                //i.TopLeftRow+i.Height==i.BottomRightRow
                Assert.True(i.TopLeftRow + i.Height == i.BottomRightRow);
                //i.TopLeftColumn+i.Width==i.BottomRightColumn
                Assert.True(i.TopLeftColumn + i.Width == i.BottomRightColumn);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static (ulong index, ulong rowIndex, ulong columnIndex, ulong topLeftRow, ulong topLeftColumn, ulong width,
        ulong height)[] 生成异常参数(ulong testCount)
    {
        var rnd = Random.Shared;
        var list =
            new List<(ulong index, ulong rowIndex, ulong columnIndex, ulong topLeftRow, ulong topLeftColumn, ulong width
                , ulong height)>();
        const int spanLength = 7;
        Span<ulong> span = stackalloc ulong[spanLength];
        for (ulong index = 0; index < testCount; index++)
        for (var i = 0; i < spanLength; i++)
        {
            span[i] = (ulong)rnd.Next(int.MaxValue);
            //span[5-6]随机一个为0
            span[rnd.Next(5, 7)] = 0;
            list.Add((span[0], span[1], span[2], span[3], span[4], span[5], span[6]));
        }

        return list.ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static (ulong index, ulong rowIndex, ulong columnIndex, ulong topLeftRow, ulong topLeftColumn, ulong width,
        ulong height)[] 生成正常参数(ulong testCount)
    {
        var rnd = Random.Shared;
        var list =
            new List<(ulong index, ulong rowIndex, ulong columnIndex, ulong topLeftRow, ulong topLeftColumn, ulong width
                , ulong height)>();
        for (ulong index = 0; index < testCount; index++)
        {
            var i = (ulong)rnd.Next(int.MaxValue);
            var rowIndex = (ulong)rnd.Next(int.MaxValue);
            var columnIndex = (ulong)rnd.Next(int.MaxValue);
            var topLeftRow = (ulong)rnd.Next(int.MaxValue);
            var topLeftColumn = (ulong)rnd.Next(int.MaxValue);
            var width = (ulong)rnd.Next(1, int.MaxValue);
            var height = (ulong)rnd.Next(1, int.MaxValue);
            list.Add((i, rowIndex, columnIndex, topLeftRow, topLeftColumn, width, height));
        }

        return list.ToArray();
    }
}