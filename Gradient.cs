﻿

#region using statements

using System.Drawing;
using System;
using DataJuggler.UltimateHelper;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class Gradient
    /// <summary>
    /// This class is used to create gradients based on Two Points (for now)
    /// </summary>
    public class Gradient
    {
        
        #region Private Variables
        private PixelInformation color1;
        private PixelInformation color2;
        private int imageHeight;
        private int imageWidth;
        #endregion

        #region Constructors

            #region Default Constructor
            /// <summary>
            /// Create an empty gradient
            /// </summary>
            public Gradient()
            {
                
            }
            #endregion

            #region Constructor(PixelInformation color1, PixelInformation color2, int imageHeight, int imageWidth)
            /// <summary>
            /// Create a new instance of a Gradient object
            /// </summary>
            /// <param name="color1"></param>
            /// <param name="color2"></param>
            /// <param name="imageHeight"></param>
            /// <param name="imageWidth"></param>
            public Gradient(PixelInformation color1, PixelInformation color2, int imageHeight, int imageWidth)
            {
                // Store the args
                Color1 = color1;
                Color2 = color2;
                ImageHeight = imageHeight;
                ImageWidth = imageWidth;
            }
            #endregion

        #endregion

        #region Methods

            #region CalculateBlue(int x, int y)
            /// <summary>
            /// This method returns the Blue
            /// </summary>
            public int CalculateBlue(int x, int y)
            {
                // initial value
                int blue = 0;

                try
                {
                    // We need the percentage of each color
                    int xDiff1 = Math.Abs(Color1.X - x);
                    int xDiff2 = Math.Abs(Color2.X - x);
                    int yDiff1 = Math.Abs(Color1.Y - y);
                    int yDiff2 = Math.Abs(Color2.Y - y);
                    double diff1 = xDiff1 + yDiff1;
                    double diff2 = xDiff2 + yDiff2;
                    double totalDiff = diff1 + diff2;
                    double bluePercent1 = 0;
                    double bluePercent2 = 0;
                    
                    // if the totalDiff is set (avoids division by zero error
                    if (totalDiff > 0)
                    {
                        // Set the value for bluePercent1
                        bluePercent1 = 100 / totalDiff * diff2;

                        // Ensure in range (should never be out)

                        // make sure bluePercent1 is at least zero
                        bluePercent1 = NumericHelper.EnsureInRange(bluePercent1, 0, 100);

                        // turn into a percent
                        bluePercent1 = bluePercent1 * .01;

                        // The second number is opposite the first
                        bluePercent2 = 1 - bluePercent1;

                        // Get the value for blue1 and blue2
                        double blue1 = Color1.Blue * bluePercent1;
                        double blue2 = Color2.Blue * bluePercent2;

                        // Set the value for blue
                        blue = (int) (blue1 + blue2);

                        // Ensure the value is in range
                        blue = NumericHelper.EnsureInRange(blue, 0, 255);
                    }
                }
                catch (Exception error)
                {
                    // For debugging only for now
                    string err = error.ToString();
                }

                // return value
                return blue;
            }
            #endregion

            #region CalculateGreen(int x, int y)
            /// <summary>
            /// This method returns the Green
            /// </summary>
            public int CalculateGreen(int x, int y)
            {
                // initial value
                int green = 0;

                try
                {
                    // We need the percentage of each color
                    int xDiff1 = Math.Abs(Color1.X - x);
                    int xDiff2 = Math.Abs(Color2.X - x);
                    int yDiff1 = Math.Abs(Color1.Y - y);
                    int yDiff2 = Math.Abs(Color2.Y - y);
                    double diff1 = xDiff1 + yDiff1;
                    double diff2 = xDiff2 + yDiff2;
                    double totalDiff = diff1 + diff2;
                    double greenPercent1 = 0;
                    double greenPercent2 = 0;
                    
                    // if the totalDiff is set (avoids division by zero error
                    if (totalDiff > 0)
                    {
                        // Set the value for greenPercent1
                        greenPercent1 = 100 / totalDiff * diff2;

                        // Ensure in range (should never be out)

                        // make sure greenPercent1 is at least zero
                        greenPercent1 = NumericHelper.EnsureInRange(greenPercent1, 0, 100);

                        // Turn into a percent
                        greenPercent1 = greenPercent1 * .01;

                        // The second number is opposite the first
                        greenPercent2 = 1 - greenPercent1;

                        // Get the value for green1 and green2
                        double green1 = Color1.Green * greenPercent1;
                        double green2 = Color2.Green * greenPercent2;

                        // Set the value for green
                        green = (int) (green1 + green2);

                        // Ensure the value is in range
                        green = NumericHelper.EnsureInRange(green, 0, 255);
                    }
                }
                catch (Exception error)
                {
                    // For debugging only for now
                    string err = error.ToString();
                }

                // return value
                return green;
            }
            #endregion
            
            #region CalculateRed(int x, int y)
            /// <summary>
            /// This method returns the Red
            /// </summary>
            public int CalculateRed(int x, int y)
            {
                // initial value
                int red = 0;

                try
                {
                    // We need the percentage of each color
                    int xDiff1 = Math.Abs(Color1.X - x);
                    int xDiff2 = Math.Abs(Color2.X - x);
                    int yDiff1 = Math.Abs(Color1.Y - y);
                    int yDiff2 = Math.Abs(Color2.Y - y);
                    double diff1 = xDiff1 + yDiff1;
                    double diff2 = xDiff2 + yDiff2;
                    double totalDiff = diff1 + diff2;
                    double redPercent1 = 0;
                    double redPercent2 = 0;
                    
                    // if the totalDiff is set (avoids division by zero error
                    if (totalDiff > 0)
                    {
                        // Set the value for redPercent1
                        redPercent1 = 100 / totalDiff * diff2;

                        // Ensure in range (should never be out)

                        // make sure redPercent1 is at least zero
                        redPercent1 = NumericHelper.EnsureInRange(redPercent1, 0, 100);

                        // Set the redPercent1
                        redPercent1 = redPercent1 * .01;

                        // The second number is opposite the first
                        redPercent2 = 1 - redPercent1;

                        // Get the value for red1 and red2
                        double red1 = Color1.Red * redPercent1;
                        double red2 = Color2.Red * redPercent2;

                        // Set the value for red
                        red = (int) (red1 + red2);

                        // Ensure the value is in range
                        red = NumericHelper.EnsureInRange(red, 0, 255);
                    }
                }
                catch (Exception error)
                {
                    // For debugging only for now
                    string err = error.ToString();
                }

                // return value
                return red;
            }
            #endregion
            
            #region GetPixelInfo(int x, int y)
            /// <summary>
            /// This method returns the Pixel Info
            /// </summary>
            public PixelInformation GetPixelInfo(int x, int y)
            {
                // Calculate the red value
                int red = CalculateRed(x, y);
                int green = CalculateGreen(x, y);
                int blue = CalculateBlue(x, y);

                // Create the new color
                Color color = Color.FromArgb(red, green, blue);
                PixelInformation pixelInfo = new PixelInformation(x, y, color);

                // return value
                return pixelInfo;
            }
            #endregion
            
        #endregion

        #region Properties

        #region Color1
        /// <summary>
        /// This property gets or sets the value for 'Color1'.
        /// </summary>
        public PixelInformation Color1
            {
                get { return color1; }
                set { color1 = value; }
            }
            #endregion
            
            #region Color2
            /// <summary>
            /// This property gets or sets the value for 'Color2'.
            /// </summary>
            public PixelInformation Color2
            {
                get { return color2; }
                set { color2 = value; }
            }
            #endregion
            
            #region ImageHeight
            /// <summary>
            /// This property gets or sets the value for 'ImageHeight'.
            /// </summary>
            public int ImageHeight
            {
                get { return imageHeight; }
                set { imageHeight = value; }
            }
            #endregion
            
            #region ImageWidth
            /// <summary>
            /// This property gets or sets the value for 'ImageWidth'.
            /// </summary>
            public int ImageWidth
            {
                get { return imageWidth; }
                set { imageWidth = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
