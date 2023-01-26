using System.Diagnostics.CodeAnalysis;
using ImageCalcTools.EqualRatioResize;

namespace ImageCalcTools.Test.EqualRatioResize;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class UnitTest_EqualRatioResizeTools
{
    //Test_Calc
    [Fact]
    public void Test_Calc()
    {
        const ulong testCount = 10000;
        {
            foreach (var (w, h, t, type) in UnitTest_InputEqualRatioResizeParameters.生成正常参数(testCount))
            {
                var i = new InputEqualRatioResizeParameters(w, h, t, type);
                var o = EqualRatioResizeTools.Calc(i);
                Assert.Equal(i.Height, o.Height);
                Assert.Equal(i.Width, o.Width);
                Assert.True(o.TargetHeight >= 1);
                Assert.True(o.TargetWidth >= 1);
                //0<Ratio
                Assert.True(o.Ratio > 0);
                //TargetWidth/Width=TargetHeight/Height
                Assert.Equal(o.TargetWidth / (double)o.Width, o.TargetHeight / (double)o.Height, 3);
                //TargetWidth==Width*Ratio
                Assert.Equal(o.TargetWidth, (ulong)(o.Width * o.Ratio));
                //TargetHeight==Height*Ratio
                Assert.Equal(o.TargetHeight, (ulong)(o.Height * o.Ratio));
                //TargetValue-(TargetWidth or TargetHeight)<=1
                Assert.True(Math.Abs((decimal)(i.TargetValue - o.TargetWidth)) <= 1 ||
                    Math.Abs((decimal)(i.TargetValue - o.TargetHeight)) <= 1);
            }
        }
    }
}