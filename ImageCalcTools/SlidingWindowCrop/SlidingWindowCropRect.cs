using ImageCalcTools.Tools;

namespace ImageCalcTools.SlidingWindowCrop;

public class SlidingWindowCropRect
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
    public SlidingWindowCropRect(ulong index, ulong rowIndex, ulong columnIndex, decimal topLeftRow,
        decimal topLeftColumn, ulong width, ulong height)
    {
        TopLeftRow = topLeftRow;
        TopLeftColumn = topLeftColumn;
        Width = width;
        Height = height;
        RowIndex = rowIndex;
        ColumnIndex = columnIndex;
        Index = index;
        //Width >0
        CheckTools.MustGreaterThanZero(Width, nameof(Width));
        //Height >0
        CheckTools.MustGreaterThanZero(Height, nameof(Height));
        //TopLeftRow >=0
        CheckTools.MustGreaterThanOrEqualZero(TopLeftRow, nameof(TopLeftRow));
        //TopLeftColumn >=0
        CheckTools.MustGreaterThanOrEqualZero(TopLeftColumn, nameof(TopLeftColumn));
        //RowIndex >=0
        CheckTools.MustGreaterThanOrEqualZero(RowIndex, nameof(RowIndex));
        //ColumnIndex >=0
        CheckTools.MustGreaterThanOrEqualZero(ColumnIndex, nameof(ColumnIndex));
    }

    /// <summary>
    ///     左上角行号
    /// </summary>
    public decimal TopLeftRow { get; }

    /// <summary>
    ///     左上角列号
    /// </summary>
    public decimal TopLeftColumn { get; }

    /// <summary>
    ///     右下角行号
    /// </summary>
    public decimal BottomRightRow => TopLeftRow + Height;

    /// <summary>
    ///     右下角列号
    /// </summary>
    public decimal BottomRightColumn => TopLeftColumn + Width;

    /// <summary>
    ///     中心行号
    /// </summary>
    public decimal CenterRow => (BottomRightRow + TopLeftRow) / 2m;

    /// <summary>
    ///     中心列号
    /// </summary>
    public decimal CenterColumn => (BottomRightColumn + TopLeftColumn) / 2m;

    /// <summary>
    ///     宽度
    /// </summary>
    public ulong Width { get; }

    /// <summary>
    ///     高度
    /// </summary>
    public ulong Height { get; }

    /// <summary>
    ///     索引
    /// </summary>
    public ulong Index { get; }

    /// <summary>
    ///     行索引
    /// </summary>
    public ulong RowIndex { get; }

    /// <summary>
    ///     列索引
    /// </summary>
    public ulong ColumnIndex { get; }
}