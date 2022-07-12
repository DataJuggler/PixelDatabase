
namespace DataJuggler.PixelDatabase.Enumerations
{

    #region ActionTypeEnum : int
    /// <summary>
    /// Does this PixelQuery Show or Hide pixels?
    /// </summary>
    public enum ActionTypeEnum : int
    {
        Unknown = 0,
        HidePixels = 1,
        ShowPixels = 2,
        DrawLine = 3,
        DrawTransparentLine = 4,
        SetBackColor = 5,
        Update = 6,
        HideFrom = 7,
        Undo = 8
    }
    #endregion

    #region BackgroundObjectDetectionTypeEnum
    /// <summary>
    /// This enum is used by the GetRectangle methods
    /// </summary>
    public enum BackgroundObjectDetectionTypeEnum : int
    {
        ObjectTotalMustBeLessThan = 0,
        ObjectTotalMustBeMoreThan = 1
    }
    #endregion

    #region ComparisonTypeEnum : int
    /// <summary>
    /// This is the type of comparison a PixelCriteria will set
    /// </summary>
    public enum ComparisonTypeEnum : int
    {
        NoComparison = -1,
        Unknown = 0,
        LessThan = -1,
        GreaterThan = 1,
        Between = 2,
        Equals = 3,
        In = 4
    }
    #endregion    

    #region DirectionEnum : int
    /// <summary>
    /// This enum is used by the DrawRiver method
    /// </summary>
    public enum DirectionEnum : int
    {
        Random = 0,
        LeftToRight = 1,
        TopToBottom = 2,
        RightToLeft = 3,
        BottomToTop = 4
    }
    #endregion

    #region GrayScaleFormulaEnum : int
    /// <summary>
    /// This enum is used to determine if a GrayScale should set a pixel to
    /// the average of Red, Green or Blue or use the value of Red, Green or Blue.
    /// </summary>
    public enum GrayScaleFormulaEnum : int
    {
        TakeAverage = 0,
        TakeRed = 1,
        TakeGreen = 2,
        TakeBlue = 3,
        TakeMin = 4,
        TakeMax = 5,
        TakeMean = 6
    }
    #endregion

    #region MaskActionEnum
    /// <summary>
    /// MaskActionEnum is used by PixelQuery to turn masks on or off.
    /// Any pixel with Mask = true will not be affected during an update.
    /// </summary>
    public enum MaskActionEnum : int
    {
        ClearAll = -2,
        Clear = -1,
        NoAction = 0,
        Add = 1,
        Replace = 2
    }
    #endregion

    #region NextLineExpectationEnum
    /// <summary>
    /// NextLineExpectationEnum is used by PixelQueryParser
    /// to parse Gradients.
    /// </summary>
    public enum NextLineExpectationEnum : int
    {
        Unknown = 0,
        SetColor1 = 1,
        SetColor2 = 2
    }
    #endregion

    #region NormalizeDirectionEnum
    /// <summary>
    /// This enum is used to determine if a pixel is above or below the Min Max difference
    /// </summary>
    public enum NormalizeDirectionEnum : int
    {
        Unknown = 0,
        Decreasing = -1,
        Increasing = 1
    }
    #endregion

    #region PixelTypeEnum : int
    /// <summary>
    /// This is the type of pixel a PixelCriteria will apply to.
    /// </summary>
    public enum PixelTypeEnum : int
    {
        Unknown = 0,
        Alpha = 1,
        Average = 2,
        Blue = 3,
        BlueAverageDifference = 4,
        BlueGreen = 5,
        BlueGreenDifference = 6,
        BlueMaxDifference = 7,
        BlueMinDifference = 8,
        BlueRed = 9,
        BlueRedDifference = 10,
        DrawLine = 11,
        Green = 12,
        GreenAverageDifference = 13,
        GreenMaxDifference = 14,
        GreenMinDifference = 15,
        GreenRed = 16,
        GreenRedDifference = 17,
        LastUpdate = 18,
        Min = 19,
        Max = 20,
        MinMaxDifference = 21,
        Red = 22,
        RedAverageDifference = 23,
        RedMaxDifference = 24,
        RedMinDifference = 25,
        Total = 26,
        X = 27,
        Y = 28
    }
    #endregion

    #region RepeatTypeEnum : int
    /// <summary>
    /// This is the type of repeat that is used when drawing a line
    /// </summary>
    public enum RepeatTypeEnum : int
    {
        NoRepeat = 0,
        Down = 1,
        Left = 2,
        Right = 3,
        Up = 4
    }
    #endregion

    #region RGBColor : int
    /// <summary>
    /// This enumeration is used to select Red, Green or Blue or combinations of such
    /// </summary>
    public enum RGBColor : int
    {
        NotSet = 0,
        Red = 1,
        Green = 2,
        Blue = 3,
        GreenRed = 4,
        BlueRed = 5,
        BlueGreen = 6,
        All = 7
    }
    #endregion

    #region PrimaryColorEnum : int
    /// <summary>
    /// This enumeration is used to select Red, Green or Blue.
    /// </summary>
    public enum PrimaryColorEnum : int
    {
        NotSet = 0,
        Red = 1,
        Green = 2,
        Blue = 3
    }
    #endregion

    #region SplitImageDirectionEnum
    /// <summary>
    /// This is used to set which side needs to be the one to copy from..
    /// </summary>
    public enum SplitImageDirectionEnum : int
    {
        TakeLeft = 0,
        TakeRight = 1
    }
    #endregion

    #region SwapTypeEnum : int
    /// <summary>
    /// The SwapType is used to designate which colors
    /// are being swapped when the SwapColors is true.
    /// </summary>
    public enum SwapTypeEnum : int
    {
        Unknown = 0,
        RedToGreen = 1,
        RedToBlue = 2,
        BlueToGreen = 3
    }
    #endregion

}
