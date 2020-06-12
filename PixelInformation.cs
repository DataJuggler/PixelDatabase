

#region using statements

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class PixelInformation
    /// <summary>
    /// This class is used to contain information about a pixel, and 
    /// </summary>
    public class PixelInformation
    {

        #region Private Variables
        private int index;
        private Color color;
        private int x;
        private int y;
        private bool isMask;
        private Guid updateId;
        #endregion

        #region Constructors

            #region DefaultConstructor
            /// <summary>
            /// Create a new instance of a PixelInformationObject
            /// </summary>
            public PixelInformation()
            { 
                // Set the values
                Color = System.Drawing.Color.Empty;
                X = 0;
                Y = 0;
            }
            #endregion

            #region Parameterized Constructor(int x, int y, Color color)
            /// <summary>
            /// Create a new instance of a PixelInformationObject
            /// </summary>
            public PixelInformation(int x, int y, Color color)
            { 
                // store the args
                Color = color;
                X = x;
                Y = y;
            }
            #endregion

        #endregion

        #region Methods

            #region ToString()
            /// <summary>
            /// method returns the String
            /// </summary>
            public override string ToString()
            {
                // Create a new instance of a 'StringBuilder' object.
                StringBuilder sb = new StringBuilder();

                // Append the string
                sb.Append("R:");
                sb.Append(Red);
                sb.Append("G:");
                sb.Append(Green);
                sb.Append("B:");
                sb.Append(Blue);
                sb.Append("T:");
                sb.Append(Total);
                
                // set the return value
                string toString = sb.ToString();

                // return value
                return toString;
            }
            #endregion
            
        #endregion

        #region Properties

            #region Alpha
            /// <summary>
            /// This property gets or sets the value for 'Alpha'.
            /// </summary>
            public int Alpha
            {
                get 
                { 
                    // initial value
                    int alpha = Color.A;

                    // return value
                    return alpha; 
                }
            }
            #endregion

            #region Average
            /// <summary>
            /// This read only property returns the value for 'Average'.
            /// </summary>
            public int Average
            {
                get
                {
                    // inititial value
                    int average = (Color.R + Color.G + Color.B) / 3;
                    
                    // return value
                    return average;
                }
            }
            #endregion
            
            #region Blue
            /// <summary>
            /// This property gets or sets the value for 'Blue'.
            /// </summary>
            public int Blue
            {
                get 
                { 
                    // initial value
                    int blue = Color.B;

                    // return value
                    return blue; 
                }
            }
            #endregion

            #region BlueAverageDifference
            /// <summary>
            /// This read only property returns the value for BlueAverageDifference'.
            /// This value is calculated by taking the Average and subtracting the value for Blue.
            /// </summary>
            public int BlueAverageDifference
            {
                get
                {
                    // initial value
                    int blueAverageDifference = Average - Blue;
                    
                    // return value
                    return blueAverageDifference;
                }
            }
            #endregion

            #region BlueMaxDifference
            /// <summary>
            /// This read only property returns the value for 'BlueMaxDifference'.
            /// This value is calculated by taking the Max and subtracting the value for Blue.
            /// </summary>
            public int BlueMaxDifference
            {
                get
                {
                    // initial value
                    int blueMaxDifference = Max - Blue;
                    
                    // return value
                    return blueMaxDifference;
                }
            }
            #endregion

            #region BlueMinDifference
            /// <summary>
            /// This read only property returns the value for 'BlueMinDifference'.
            /// This value is calculated by taking the value for Blue and subtracting the value for Min.
            /// </summary>
            public int BlueMinDifference
            {
                get
                {
                    // initial value
                    int blueMinDifference = Blue - Min;
                    
                    // return value
                    return blueMinDifference;
                }
            }
            #endregion
            
            #region BlueGreen
            /// <summary>
            /// This read only property returns the value for 'BlueGreen'.
            /// </summary>
            public int BlueGreen
            {
                get
                {
                    // initial value
                    int blueGreen = Blue + Green;
                    
                    // return value
                    return blueGreen;
                }
            }
            #endregion

            #region BlueGreenDifference
            /// <summary>
            /// This read only property returns the value for 'BlueGreenDifference'.
            /// This value is calculated by taking the value of Blue - Green.
            /// </summary>
            public int BlueGreenDifference
            {
                get
                {
                    // initial value
                    int blueGreenDifference = Blue - Green;
                    
                    // return value
                    return blueGreenDifference;
                }
            }
            #endregion

            #region BlueRed
            /// <summary>
            /// This read only property returns the value for 'BlueRed'.
            /// </summary>
            public int BlueRed
            {
                get
                {
                    // initial value
                    int blueRed = Blue + Red;
                    
                    // return value
                    return blueRed;
                }
            }
            #endregion

            #region BlueRedDifference
            /// <summary>
            /// This read only property returns the value for 'BlueRedDifference'.
            /// This value is calculated by taking the value of Blue - Red.
            /// </summary>
            public int BlueRedDifference
            {
                get
                {
                    // initial value
                    int blueRedDifference = Blue - Red;
                    
                    // return value
                    return blueRedDifference;
                }
            }
            #endregion
            
            #region Color
            /// <summary>
            /// This property gets or sets the value for 'Color'.
            /// </summary>
            public Color Color
            {
                get { return color; }
                set { color = value; }
            }
            #endregion
            
            #region Green
            /// <summary>
            /// This property gets or sets the value for 'Green'.
            /// </summary>
            public int Green
            {
                get 
                { 
                    // initial value
                    int green = Color.G;

                    // return value
                    return green;
                }
            }
            #endregion

            #region GreenAverageDifference
            /// <summary>
            /// This read only property returns the value for 'GreenAverageDifference'.
            /// This value is calculated by taking the Average and subtracting the value for Green.
            /// </summary>
            public int GreenAverageDifference
            {
                get
                {
                    // initial value
                    int greenAverageDifference = Average - Green;
                    
                    // return value
                    return greenAverageDifference;
                }
            }
            #endregion

            #region GreenMaxDifference
            /// <summary>
            /// This read only property returns the value for 'GreenMaxDifference'.
            /// This value is calculated by taking the Max and subtracting the value for Green.
            /// </summary>
            public int GreenMaxDifference
            {
                get
                {
                    // initial value
                    int greenMaxDifference = Max - Green;
                    
                    // return value
                    return greenMaxDifference;
                }
            }
            #endregion

            #region GreenMinDifference
            /// <summary>
            /// This read only property returns the value for 'GreenMinDifference'.
            /// This value is calculated by taking the Green and subtracting the value for Min.
            /// </summary>
            public int GreenMinDifference
            {
                get
                {
                    // initial value
                    int greenMinDifference = Green - Min;
                    
                    // return value
                    return greenMinDifference;
                }
            }
            #endregion

            #region GreenRed
            /// <summary>
            /// This read only property returns the value for 'GreenRed'.
            /// </summary>
            public int GreenRed
            {
                get
                {
                    // initial value
                    int greenRed = Green + Red;
                    
                    // return value
                    return greenRed;
                }
            }
            #endregion

            #region GreenRedDifference
            /// <summary>
            /// This read only property returns the value for 'GreenRedDifference'.
            /// This value is calculated by taking the value of Green - Red.
            /// </summary>
            public int GreenRedDifference
            {
                get
                {
                    // initial value
                    int greenRedDifference = Green - Red;
                    
                    // return value
                    return greenRedDifference;
                }
            }
            #endregion

            #region Index
            /// <summary>
            /// This property gets or sets the value for 'Index'.
            /// </summary>
            public int Index
            {
                get { return index; }
                set { index = value; }
            }
            #endregion

            #region IsMask
            /// <summary>
            /// This property gets or sets the value for 'IsMask'.
            /// </summary>
            public bool IsMask
            {
                get { return isMask; }
                set { isMask = value; }
            }
            #endregion
            
            #region Max
            /// <summary>
            /// This read only property returns the Maximum color for a Pixel.
            /// Example: Red 200 Green 105 Blue 70 - Max = 200
            /// </summary>
            public int Max
            {
                get
                {
                    // set the value for Max
                    int max = Red;

                    // If Green is greater than Max
                    if (Green > max)
                    {
                        // set the new max
                        max = Green;
                    }

                    // If Blue is greater than Max
                    if (Blue > max)
                    {
                        // set the new max
                        max = Blue;
                    }

                    // return value
                    return max;
                }
            }
            #endregion

            #region MinMaxDifference
            /// <summary>
            /// This read only property returns Max - Min.
            /// </summary>
            public int MinMaxDifference
            {
                get
                {   
                    // initial value
                    int maxMinDifference = Max - Min;

                    // return value
                    return maxMinDifference;
                }
            }
            #endregion

            #region Min
            /// <summary>
            /// This read only property returns the Minimum color for a Pixel.
            /// Example: Red 200 Green 105 Blue 70 - Min = 70
            /// </summary>
            public int Min
            {
                get
                {
                    // set the value for min
                    int min = Red;

                    // If Green is less than min
                    if (Green < min)
                    {
                        // set the new min
                        min = Green;
                    }

                    // If Blue is less than min
                    if (Blue < min)
                    {
                        // set the new min
                        min = Blue;
                    }

                    // return value
                    return min;
                }
            }
            #endregion
            
            #region Red
            /// <summary>
            /// This property gets or sets the value for 'Red'.
            /// </summary>
            public int Red
            {
                get
                {
                    // initial value
                    int red = this.Color.R;

                    // return value
                    return red;
                }
            }
            #endregion
            
            #region RedAverageDifference
            /// <summary>
            /// This read only property returns the value for 'RedAverageDifference'.
            /// This value is calculated by taking the Average and subtracting the value for Red.
            /// </summary>
            public int RedAverageDifference
            {
                get
                {
                    // initial value
                    int redAverageDifference = Average - Red;
                    
                    // return value
                    return redAverageDifference;
                }
            }
            #endregion

            #region RedMaxDifference
            /// <summary>
            /// This read only property returns the value for 'RedMaxDifference'.
            /// This value is calculated by taking the Max and subtracting the value for Red.
            /// </summary>
            public int RedMaxDifference
            {
                get
                {
                    // initial value
                    int redMaxDifference = Max - Red;
                    
                    // return value
                    return redMaxDifference;
                }
            }
            #endregion

            #region RedMinDifference
            /// <summary>
            /// This read only property returns the value for 'RedMinDifference'.
            /// This value is calculated by taking the Red and subtracting the value for Min.
            /// </summary>
            public int RedMinDifference
            {
                get
                {
                    // initial value
                    int redMinDifference = Red - Min;
                    
                    // return value
                    return redMinDifference;
                }
            }
            #endregion
            
            #region Total
            /// <summary>
            /// This read only property returns the value for 'Total'.
            /// </summary>
            public int Total
            {
                get
                {
                    // initial value
                    int total = Red + Green + Blue;
                    
                    // return value
                    return total;
                }
            }
            #endregion
            
            #region X
            /// <summary>
            /// This property gets or sets the value for 'X'.
            /// </summary>
            public int X
            {
                get { return x; }
                set { x = value; }
            }
            #endregion
            
            #region Y
            /// <summary>
            /// This property gets or sets the value for 'Y'.
            /// </summary>
            public int Y
            {
                get { return y; }
                set { y = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
