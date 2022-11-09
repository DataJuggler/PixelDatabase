

#region using statements

using DataJuggler.RandomShuffler;
using DataJuggler.UltimateHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using DataJuggler.PixelDatabase.Enumerations;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Versioning;
using System.Xaml.Permissions;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class PixelDatabase
    /// <summary>
    /// This class represents a collection of PixelInformation objects
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class PixelDatabase : IDisposable
    {

        #region Private Variables
        private DirectBitmap directBitmap;
        private MaskManager maskManager;
        private LastUpdate lastUpdate;
        private bool abort;
        private Color lineColor;
        private bool lineColorSet;
        private List<Layer> layers;
        private PixelQuery pixelQuery;
        private string resetPath;
        private string undoPath;        
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a PixelDatabase object
        /// </summary>
        public PixelDatabase()
        {
            // Perform initializations for this object
            Init();            
        }
        #endregion

        #region Methods

            #region AdjustColor(Color previousColor, PixelQuery pixelQuery)
            /// <summary>
            /// This method returns the Adjusted Color
            /// </summary>
            public static Color AdjustColor(Color previousColor, PixelQuery pixelQuery)
            {
                // initial value
                Color color = previousColor;

                // locals
                int adjustedValue = 0;
                int adjustedValue2 = 0;
                int adjustedValue3 = 0;
                
                // If the pixelQuery object exists
                if (NullHelper.Exists(pixelQuery))
                {
                    switch (pixelQuery.ColorToAdjust)
                    {
                        case RGBColor.Red:

                            // if there is an AssignToColor
                            if (pixelQuery.HasAssignToColor)
                            {
                                // Set Red Equals Blue
                                if (pixelQuery.AssignToColor == RGBColor.Blue)
                                {
                                    // Create the new color
                                    color = Color.FromArgb(previousColor.B, previousColor.G, previousColor.B);
                                }
                                else if (pixelQuery.AssignToColor == RGBColor.Green)
                                {
                                    // Create the new color
                                    color = Color.FromArgb(previousColor.G, previousColor.G, previousColor.B);
                                }
                            }
                            else
                            {
                                // get the adjust color (guarunteed to be in range)
                                adjustedValue = AdjustValue(previousColor.R, pixelQuery.Adjustment);

                                // Create the adjusted color
                                color = Color.FromArgb(adjustedValue, previousColor.G, previousColor.B);
                            }

                            // required
                            break;

                        case RGBColor.Green:

                            // if there is an AssignToColor
                            if (pixelQuery.HasAssignToColor)
                            {
                                // Set Red Equals Blue
                                if (pixelQuery.AssignToColor == RGBColor.Blue)
                                {
                                    // Create the new color
                                    color = Color.FromArgb(previousColor.R, previousColor.B, previousColor.B);
                                }
                                else if (pixelQuery.AssignToColor == RGBColor.Red)
                                {
                                    // Create the new color
                                    color = Color.FromArgb(previousColor.R, previousColor.R, previousColor.B);
                                }
                            }
                            else
                            {
                                // get the adjust color (guarunteed to be in range)
                                adjustedValue = AdjustValue(previousColor.G, pixelQuery.Adjustment);

                                // Create the adjusted color
                                color = Color.FromArgb(previousColor.R, adjustedValue, previousColor.B);
                            }

                            // required
                            break;

                        case RGBColor.Blue:

                            // if there is an AssignToColor
                            if (pixelQuery.HasAssignToColor)
                            {
                                // Set Red Equals Blue
                                if (pixelQuery.AssignToColor == RGBColor.Green)
                                {
                                    // Create the new color
                                    color = Color.FromArgb(previousColor.R, previousColor.G, previousColor.G);
                                }
                                else if (pixelQuery.AssignToColor == RGBColor.Red)
                                {
                                    // Create the new color
                                    color = Color.FromArgb(previousColor.R, previousColor.G, previousColor.R);
                                }
                            }
                            else
                            {
                                // get the adjust color (guarunteed to be in range)
                                adjustedValue = AdjustValue(previousColor.B, pixelQuery.Adjustment);

                                // Create the adjusted color
                                color = Color.FromArgb(previousColor.R, previousColor.G, adjustedValue);
                            }

                            // required
                            break;

                        case RGBColor.GreenRed:

                            // get the adjust color (guarunteed to be in range)
                            adjustedValue = AdjustValue(previousColor.R, pixelQuery.Adjustment);
                            adjustedValue2 = AdjustValue(previousColor.G, pixelQuery.Adjustment);
                                                
                            // Create the adjusted color
                            color = Color.FromArgb(adjustedValue, adjustedValue2, previousColor.B);

                            // required
                            break;

                        case RGBColor.BlueRed:

                            // get the adjust color (guarunteed to be in range)
                            adjustedValue = AdjustValue(previousColor.R, pixelQuery.Adjustment);
                            adjustedValue2 = AdjustValue(previousColor.B, pixelQuery.Adjustment);
                                                
                            // Create the adjusted color
                            color = Color.FromArgb(adjustedValue, previousColor.G , adjustedValue2);

                            // required
                            break;

                        case RGBColor.BlueGreen:

                            // get the adjust color (guarunteed to be in range)
                            adjustedValue = AdjustValue(previousColor.G, pixelQuery.Adjustment);
                            adjustedValue2 = AdjustValue(previousColor.B, pixelQuery.Adjustment);
                                                
                            // Create the adjusted color
                            color = Color.FromArgb(previousColor.R, adjustedValue , adjustedValue2);

                            // required
                            break;

                        case RGBColor.All:

                            // get the adjust color (guarunteed to be in range)
                            adjustedValue = AdjustValue(previousColor.R, pixelQuery.Adjustment);
                            adjustedValue2 = AdjustValue(previousColor.G, pixelQuery.Adjustment);
                            adjustedValue3 = AdjustValue(previousColor.B, pixelQuery.Adjustment);

                            // Create the adjusted color
                            color = Color.FromArgb(adjustedValue, adjustedValue2, adjustedValue3);

                            // required
                            break;

                    } 
                }
                
                // return value
                return color;
            }
            #endregion

            #region AdjustColorValue(int value, int adjustment
            /// <summary>
            /// This method returns the new integer value after the adjustment.
            /// Regardless of what is passed in, the value will return 0
            /// </summary>
            public static int AdjustColorValue(int value, int adjustment)
            {
                // initial value
                int adjustedColorValue = value + adjustment;

                // if less than zero
                if (adjustedColorValue < 0)
                {
                    // set to zero
                    adjustedColorValue = 0;
                }

                // if higher than 255
                if (adjustedColorValue > 255)
                {
                    // set to max
                    adjustedColorValue = 255;
                }
                
                // return value
                return adjustedColorValue;
            }
            #endregion

            #region AdjustValue(int originalValue, int adjustment)
            /// <summary>
            /// This method returns the Value
            /// </summary>
            public static int AdjustValue(int originalValue, int adjustment)
            {
                // Set the return value (adjustment may be negative)
                int adjustValue = originalValue + adjustment;

                // if too low
                if (adjustValue < 0)
                {
                    // cannot be lower than zero
                    adjustValue = 0;
                }

                 // if too high
                if (adjustValue > 255)
                {
                    // cannot be higher than 255
                    adjustValue = 255;
                }
                
                // return value
                return adjustValue;
            }
            #endregion
            
            #region ApplyCriteria(PixelQuery pixelQuery, StatusUpdate status)
            /// <summary>
            /// This method applies the changes in the PixelQuery to the DirectBitmap.
            /// </summary>
            public int ApplyCriteria(PixelQuery pixelQuery, StatusUpdate status)
            {
                // initial value
                int pixelsUpdated = 0;

                // local
                bool shouldThisPixelBeUpdated = false;
                Color color = Color.Empty;
                Color newColor = Color.Empty;
                int count = 0;
                bool addToLastUpdate = true;
                int startIndex = -1;
                int endIndex = -1;
                List<PixelCriteria> criteria = null;
                
                // if the pixels exist and the pixelQuery exists and is valid
                if ((NullHelper.Exists(pixelQuery)) && (pixelQuery.IsValid) && (HasDirectBitmap))
                {
                    // Set the criteria
                    criteria = pixelQuery.Criteria;

                    // calculate the range of pixels in the DirectBitmap to iterate
                    QueryRange range = CreateQueryRange();

                    // if the PixelQuery does not contain a LastUpdate criteria item
                    if ((pixelQuery.ContainsLastUpdateCriteria) && (HasLastUpdate) && (LastUpdate.Available))
                    {
                        // Get the values for Start and End
                        startIndex = LastUpdate.GetMinimum();

                        // Get the max index
                        endIndex = LastUpdate.GetMaximum();

                         // Set to false
                        addToLastUpdate = false;
                    }
                    else if ((pixelQuery.ContainsLastUpdateCriteria) && ((!HasLastUpdate) || (!LastUpdate.Available)))
                    {
                        // if the Status object exists
                        if (NullHelper.Exists(status))
                        {
                            // Show a message
                            status("Last Update Is Not Available", 0);
                        }
                    }
                    else
                    {
                        // Create a LastUpdate object
                        LastUpdate = new LastUpdate();
                    }

                    // If the value for the property pixelQuery.HasSplitImageSettings is true
                    if (pixelQuery.HasSplitImageSettings)
                    {
                        // Perform the split image
                        pixelsUpdated = SplitImage(pixelQuery.SplitImageSettings, status);
                    }
                    else if (pixelQuery.HasGradient)
                    {
                        // Apply the Gradient
                        pixelsUpdated = ApplyGradient(pixelQuery.Gradient);
                    }
                    else
                    {
                        // This is for all paths except for SplitImage and Create Gradient, which are special
                        // as there is no reason to get the current color for the pixels updated.

                        // iterate the y pixels
                        for (int y = range.StartY; y <= range.EndY; y++)
                        {
                            // iterate the x pixels
                            for (int x = range.StartX; x <= range.EndX; x++)
                            {
                                // Increment the value for count
                                count++;

                                // Reset
                                newColor = Color.Empty;

                                // set the color
                                color = DirectBitmap.GetPixel(x, y);

                                // Create a new PixelInformation object
                                PixelInformation pixel = new PixelInformation(x, y, color);

                                // set the Index
                                pixel.Index = x + (y * DirectBitmap.Width);

                                // Should this pixel be updated (in such situations like Scatter and Normalize, this actually updates)
                                shouldThisPixelBeUpdated = ShouldBeUpdated(pixel, pixelQuery, ref pixelsUpdated, range, count, status);

                                // if the value for shouldThisPixelBeUpdated is true
                                if (shouldThisPixelBeUpdated)
                                {
                                    // apply the pixel
                                    newColor = ApplyPixel(color, pixelQuery);

                                    // Set Pixel Color
                                    SetPixelColor(x, y, newColor, addToLastUpdate, pixel.Index);

                                    // increase pixelsUpdated
                                    pixelsUpdated++;   
                                }
                            
                                // Update the graph
                                HandleGraph(count, pixelsUpdated, status, range);
                            }
                        }
                    }

                    // Set the value for PixelsUpdated
                    pixelQuery.PixelsUpdated = pixelsUpdated;                   
                }
                
                // return value
                return pixelsUpdated;
            }
            #endregion
                        
            #region ApplyGradient(Gradient gradient)
            /// <summary>
            /// This method Apply Gradient
            /// </summary>
            public int ApplyGradient(Gradient gradient)
            {
                // initial value
                int pixelsUpdated = 0;

                try
                {
                    // Create the gradient
                    gradient.ImageHeight = Height;
                    gradient.ImageWidth = Width;

                    

                    // iterate the pixels
                    for(int x = 0; x < this.Width; x++)
                    {
                        for (int y = 0; y < this.Height; y++)
                        {
                            // Get the pixel at this x, y
                            PixelInformation pixel = gradient.GetPixelInfo(x, y);

                            // Set this color
                            DirectBitmap.SetPixel(x, y, pixel.Color);

                            // Increment the value for pixelsUpdated
                            pixelsUpdated++;
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only for now
                    DebugHelper.WriteDebugError("ApplyGradient", "PixelDatabase", error);
                }

                // return value
                return pixelsUpdated;
            }
            #endregion
            
            #region ApplyPixel(Color color, PixelQuery pixelQuery)
            /// <summary>
            /// This method expects you to have called ShouldPixelBeUpdated first.
            /// This method determines the new color for the color passed in and the criteria.
            /// </summary>
            public static Color ApplyPixel(Color color, PixelQuery pixelQuery)
            {
                // initial value
                Color newColor = color;

                // if SwapColors
                if (pixelQuery.HasSplitImageSettings)
                {
                    
                }
                else if (pixelQuery.ApplyGrayscale)
                {
                    int red = 0;
                    int green = 0;
                    int blue = 0;

                    if (pixelQuery.GrayScaleFormula == GrayScaleFormulaEnum.TakeBlue)
                    {
                        // set the values
                        red = color.B;
                        green = color.B;
                        blue = color.B;
                    }
                    else if (pixelQuery.GrayScaleFormula == GrayScaleFormulaEnum.TakeGreen)
                    {
                        // set the values
                        red = color.G;
                        green = color.G;
                        blue = color.G;
                    }
                    else if (pixelQuery.GrayScaleFormula == GrayScaleFormulaEnum.TakeRed)
                    {
                        // set the values
                        red = color.R;
                        green = color.R;
                        blue = color.R;
                    }
                    else if (pixelQuery.GrayScaleFormula == GrayScaleFormulaEnum.TakeMin)
                    {
                        // Get the min color, which is the lowest color

                        // Kind of violates the only one entry point and one exit point of a method,
                        // but in this case speed is needed.
                        return GetMinColor(color);
                    }
                    else if (pixelQuery.GrayScaleFormula == GrayScaleFormulaEnum.TakeMax)
                    {
                         // Get the max color, which is the highest color

                        // Kind of violates the only one entry point and one exit point of a method,
                        // but in this case speed is needed.
                        return GetMaxColor(color);
                    }
                    else if (pixelQuery.GrayScaleFormula == GrayScaleFormulaEnum.TakeMean)
                    {
                        // Get the mean color, which is the middle color

                        // Kind of violates the only one entry point and one exit point of a method,
                        // but in this case speed is needed.
                        return GetMeanColor(color);
                    }
                    else
                    {
                        // average
                        int average = (int) (color.R + color.G + color.B) / 3;

                        red = average;
                        green = average;
                        blue = average;
                    }

                    // create the newColor
                    newColor = Color.FromArgb(color.A, red, green, blue);
                }
                else if (pixelQuery.SwapColors)
                {
                    // Set color2
                    newColor = SwapColor(color, pixelQuery);
                }
                else if (pixelQuery.AdjustColor)
                {
                    newColor = AdjustColor(color, pixelQuery);
                }
                else if (pixelQuery.SetColor)
                {
                    // Set the color from here
                    newColor = pixelQuery.Color;
                }
                else if (pixelQuery.ActionType == ActionTypeEnum.HidePixels)
                {
                    // set to transparent
                    newColor = Color.FromArgb(0, color);
                }
                else if (pixelQuery.ActionType == ActionTypeEnum.ShowPixels)
                {
                    // set to visible
                    newColor = Color.FromArgb(255, color);
                }
                
                // return value
                return newColor;
            }
            #endregion
            
            #region ApplyQuery(string queryText)
            /// <summary>
            /// This method parses and applies the queryText passed in.
            /// </summary>
            /// <param name="queryText"></param>
            public PixelQuery ApplyQuery(string queryText, StatusUpdate status)
            {
                // locals
                int alpha = 0;               
                int pixelsUpdated = 0;
                
                // if the queryText exists
                if (TextHelper.Exists(queryText))
                {
                   // Parse the PixelQuery
                   pixelQuery = PixelQueryParser.ParsePixelQuery(queryText);

                    // if this is a valid query
                    if (pixelQuery.IsValid)
                    {  
                        // Set the alpha value based upon the ActionType
                        alpha = SetAlpha(pixelQuery.ActionType, pixelQuery);

                        // if SetBackColor is the action type, and there is exactly 1 Criteria object
                        if ((pixelQuery.ActionType == ActionTypeEnum.SetBackColor) && (pixelQuery.HasCriteria) && (pixelQuery.Criteria.Count == 1))
                        {
                            // this is TransparencyMaker specific so far

                            // If RemoveBackColor is true
                            if (pixelQuery.Criteria[0].RemoveBackColor)
                            {
                                // remove the back color
                                status("RemoveBackColor", 0);
                            }
                            else
                            {
                                // Send a message
                                string message = "SetBackColor" + "|" + pixelQuery.Criteria[0].BackColor.Name;

                                // Set the back color
                                status(message, 0);
                            }
                        }
                        if (pixelQuery.ActionType == ActionTypeEnum.HideFrom)
                        {
                            // Handle hiding pixels from the selected directions until 
                            // the match color is found
                            HandleHideFrom(pixelQuery);
                        }
                        else if (pixelQuery.ActionType == ActionTypeEnum.Update)
                        {
                            // if Clear All
                            if ((HasMaskManager) && (pixelQuery.HasMask) && (pixelQuery.Mask.HasAction) && (pixelQuery.Mask.Action == MaskActionEnum.ClearAll))
                            {
                                // Recreate the MaskManager
                                this.MaskManager = new MaskManager();
                            }
                            
                            // Find the pixels that match the Criteria given
                            pixelsUpdated = ApplyCriteria(pixelQuery, status);

                            // if there are one or more pixels
                            if (pixelsUpdated < 1)
                            {
                                // show a message  
                                status("No pixels could be found matching your search criteria", 0);
                            }
                        }
                        // if we are drawing
                        else if ((pixelQuery.ActionType >= ActionTypeEnum.DrawTransparentLine) || (pixelQuery.ActionType == ActionTypeEnum.DrawLine))
                        {
                            // Handle drawing a line
                            HandleDrawLine(pixelQuery, status);
                        }
                        else
                        {
                            // Hide or Show

                            // Find the pixels that match the Criteria given
                            pixelsUpdated = ApplyCriteria(pixelQuery, status);
                        }
                    }
                }

                // return value
                return pixelQuery;
            }
            #endregion

            #region ApplyPixels(List<PixelInformation> pixels, PixelQuery pixelQuery, StatusUpdate status)
            /// <summary>
            /// This method Apply Pixels
            /// </summary>
            public void ApplyPixels(List<PixelInformation> pixels, PixelQuery pixelQuery, StatusUpdate status)
            {
                // locals
                Color previousColor = Color.Empty;
                Color color = Color.Empty;
                Guid historyId = Guid.NewGuid();
                int count = 0;
                
                // Update the pixels
                foreach (PixelInformation pixel in pixels)
                {
                    // if this pixel is not under a mask. Masked pixels don't get updated (are not supposed to is a better way to put it).
                    if (!pixel.IsMask)
                    {
                        // get the prevoiusColor
                        previousColor = this.DirectBitmap.GetPixel(pixel.X, pixel.Y);

                        // Get the color
                        color = pixel.Color;

                        // if this pixel is not part of any active masks
                        if ((pixelQuery.AdjustColor) || (pixelQuery.SwapColors) || (pixelQuery.SetColor))
                        {
                            // if adjust color is true
                            if (pixelQuery.AdjustColor)
                            {
                                // Adjust the color
                                color = AdjustColor(previousColor, pixelQuery);
                            }
                            else if (pixelQuery.SwapColors)
                            {
                                // Swap two colors
                                color = SwapColor(previousColor, pixelQuery);
                            }
                            else if (pixelQuery.SetColor)
                            {
                                // Set the color from here
                                color = pixelQuery.Color;
                            }

                            // Increment the value for count
                            count++;

                            // refresh every 500,000 in case this is a long query
                            if (count % 500000 == 0)
                            {
                                // if abort is true
                                if (Abort)
                                {
                                    // Show the user a message
                                    status("Operation Aborted.", 0);

                                    // break out of the loop
                                    break;
                                }
                                else
                                {
                                    // Update the pixels affected by the query
                                    status("Updated " + String.Format("{0:n0}", count) + " of " +  String.Format("{0:n0}", pixels.Count), count);    
                                }
                            }
                        }

                        // Set the pixel
                        this.DirectBitmap.SetPixel(pixel.X, pixel.Y, color);
                    }
                }

                // Update the pixels affected by the query
                status("Updated " + String.Format("{0:n0}", count) + " pixels.", count);

                // Reset
                Abort = false;
            }
            #endregion

            #region CheckMatchAlpha(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match Alpha
            /// </summary>
            public static bool CheckMatchAlpha(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.Alpha >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.Alpha >= criteria.MinValue) && (pixel.Alpha <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.Alpha <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.Alpha == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchAverage(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match Average
            /// </summary>
            public static bool CheckMatchAverage(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.Average >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.Average >= criteria.MinValue) && (pixel.Average <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.Average <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.Average == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchBlue(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match Blue
            /// </summary>
            public static bool CheckMatchBlue(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.Blue >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.Blue >= criteria.MinValue) && (pixel.Blue <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.Blue <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.Blue == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchBlueAverageDifference(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match BlueAverageDifference
            /// </summary>
            public static bool CheckMatchBlueAverageDifference(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.BlueAverageDifference >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.BlueAverageDifference >= criteria.MinValue) && (pixel.BlueAverageDifference <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.BlueAverageDifference <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.BlueAverageDifference == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchBlueGreen(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match BlueGreen
            /// </summary>
            public static bool CheckMatchBlueGreen(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.BlueGreen >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.BlueGreen >= criteria.MinValue) && (pixel.BlueGreen <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.BlueGreen <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.BlueGreen == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchBlueGreenDifference(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match BlueGreenDifference
            /// </summary>
            public static bool CheckMatchBlueGreenDifference(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.BlueGreenDifference >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.BlueGreenDifference >= criteria.MinValue) && (pixel.BlueGreenDifference <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.BlueGreenDifference <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.BlueGreenDifference == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchBlueMinDifference(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match BlueMinDifference
            /// </summary>
            public static bool CheckMatchBlueMinDifference(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.BlueMinDifference >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.BlueMinDifference >= criteria.MinValue) && (pixel.BlueMinDifference <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.BlueMinDifference <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.BlueMinDifference == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion
            
            #region CheckMatchBlueMaxDifference(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match BlueMaxDifference
            /// </summary>
            public static bool CheckMatchBlueMaxDifference(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.BlueMaxDifference >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.BlueMaxDifference >= criteria.MinValue) && (pixel.BlueMaxDifference <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.BlueMaxDifference <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.BlueMaxDifference == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchBlueRed(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match BlueRed
            /// </summary>
            public static bool CheckMatchBlueRed(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.BlueRed >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.BlueRed >= criteria.MinValue) && (pixel.BlueRed <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.BlueRed <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.BlueRed == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchBlueRedDifference(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match BlueRedDifference
            /// </summary>
            public static bool CheckMatchBlueRedDifference(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.BlueRedDifference >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.BlueRedDifference >= criteria.MinValue) && (pixel.BlueRedDifference <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.BlueRedDifference <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.BlueRedDifference == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchGreen(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match Green
            /// </summary>
            public static bool CheckMatchGreen(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.Green >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.Green >= criteria.MinValue) && (pixel.Green <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.Green <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.Green == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchGreenAverageDifference(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match GreenAverageDifference
            /// </summary>
            public static bool CheckMatchGreenAverageDifference(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.GreenAverageDifference >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.GreenAverageDifference >= criteria.MinValue) && (pixel.GreenAverageDifference <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.GreenAverageDifference <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.GreenAverageDifference == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchGreenMinDifference(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match GreenMinDifference
            /// </summary>
            public static bool CheckMatchGreenMinDifference(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.GreenMinDifference >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.GreenMinDifference >= criteria.MinValue) && (pixel.GreenMinDifference <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.GreenMinDifference <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.GreenMinDifference == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion
            
            #region CheckMatchGreenMaxDifference(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match GreenMaxDifference
            /// </summary>
            public static bool CheckMatchGreenMaxDifference(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.GreenMaxDifference >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.GreenMaxDifference >= criteria.MinValue) && (pixel.GreenMaxDifference <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.GreenMaxDifference <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.GreenMaxDifference == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchGreenRed(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match GreenRed
            /// </summary>
            public static bool CheckMatchGreenRed(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.GreenRed >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.GreenRed >= criteria.MinValue) && (pixel.GreenRed <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.GreenRed <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.GreenRed == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchGreenRedDifference(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match GreenRedDifference
            /// </summary>
            public static bool CheckMatchGreenRedDifference(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.GreenRedDifference >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.GreenRedDifference >= criteria.MinValue) && (pixel.GreenRedDifference <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.GreenRedDifference <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.GreenRedDifference == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchMax(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match Max
            /// </summary>
            public static bool CheckMatchMax(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.Max >= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.Max >= criteria.MaxValue) && (pixel.Max <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.Max <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.Max == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchMin(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match Min
            /// </summary>
            public static bool CheckMatchMin(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.Min >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.Min >= criteria.MinValue) && (pixel.Min <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.Min <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.Min == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchMinMaxDifference(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match MinMaxDifference
            /// </summary>
            public static bool CheckMatchMinMaxDifference(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.MinMaxDifference >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.MinMaxDifference >= criteria.MinValue) && (pixel.MinMaxDifference <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.MinMaxDifference <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.MinMaxDifference == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchRed(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match Red
            /// </summary>
            public static bool CheckMatchRed(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.Red >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.Red >= criteria.MinValue) && (pixel.Red <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.Red <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.Red == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchRedAverageDifference(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match RedAverageDifference
            /// </summary>
            public static bool CheckMatchRedAverageDifference(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.RedAverageDifference >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.RedAverageDifference >= criteria.MinValue) && (pixel.RedAverageDifference <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.RedAverageDifference <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.RedAverageDifference == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchRedMinDifference(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match RedMinDifference
            /// </summary>
            public static bool CheckMatchRedMinDifference(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.RedMinDifference >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.RedMinDifference >= criteria.MinValue) && (pixel.RedMinDifference <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.RedMinDifference <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.RedMinDifference == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion
            
            #region CheckMatchRedMaxDifference(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match RedMaxDifference
            /// </summary>
            public static bool CheckMatchRedMaxDifference(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.RedMaxDifference >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.RedMaxDifference >= criteria.MinValue) && (pixel.RedMaxDifference <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.RedMaxDifference <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.RedMaxDifference == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CheckMatchTotal(PixelInformation pixel, PixelCriteria criteria)
            /// <summary>
            /// This method returns the Match Total
            /// </summary>
            public static bool CheckMatchTotal(PixelInformation pixel, PixelCriteria criteria)
            {
                // initial value
                bool match = false;
               
                // if a Greater Than
                if (criteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                {
                    // Set the return value
                    match = (pixel.Total >= criteria.MinValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Between)
                {
                    // Set the return value
                    match = ((pixel.Total >= criteria.MinValue) && (pixel.Total <= criteria.MaxValue));
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.LessThan)
                {
                    // Set the return value
                    match = (pixel.Total <= criteria.MaxValue);
                }
                else if (criteria.ComparisonType == ComparisonTypeEnum.Equals)
                {
                    // Set the return value
                    match = (pixel.Total == criteria.TargetValue);
                }

                // return value
                return match;
            }
            #endregion

            #region CopySubImage(PixelDatabase subImage, Point start)
            /// <summary>
            /// This method Copies the pixels from the Sub Image into the current database
            /// </summary>
            public void CopySubImage(PixelDatabase subImage, Point start)
            {
                // locals
                PixelInformation pixel = null;
                int localX = 0;
                int localY = 0;

                // If the subImage object exists
                if (NullHelper.Exists(subImage))
                {
                    try
                    {
                        // iterate the bounds of the subImage
                        for (int x = 0; x < subImage.Width; x++)
                        {
                            for (int y = 0; y < subImage.Height; y++)
                            {
                                // Get this pixel
                                pixel = subImage.GetPixel(x, y);

                                // If the pixel object exists
                                if (NullHelper.Exists(pixel))
                                {
                                    // now convert the subImage pixel to this pixel x and y
                                    localX = start.X + x;
                                    localY = start.Y + y;

                                    // if the localX and localY is in range
                                    if ((NumericHelper.IsInRange(localX, 0, Width - 1)) && (NumericHelper.IsInRange(localY, 0, Height - 1)))
                                    {
                                        // Now update the pixel
                                        SetPixelColor(localX, localY, pixel.Color, false, 0);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception error)
                    {
                        // For debugging for now
                        DebugHelper.WriteDebugError("CopySubImage", "PixelDatabase.cs", error);
                    }
                }
            }
            #endregion
            
            #region CreateQueryRange()
            /// <summary>
            /// This method returns the Query Range
            /// </summary>
            public QueryRange CreateQueryRange()
            {
                // initial value
                QueryRange range = new QueryRange();

                // if the value for HasDirectBitmap is true
                if (HasDirectBitmap)
                {
                    // initial values
                    range.StartX = 0;
                    range.EndX = DirectBitmap.Width -1;
                    range.StartY = 0;
                    range.EndY = DirectBitmap.Height -1;

                    // First Pass, we must test if there are any xCriteria
                    PixelCriteria xCriteria = pixelQuery.FindXCriteria();
                    PixelCriteria yCriteria = pixelQuery.FindYCriteria();

                    // If the xCriteria object exists
                    if (NullHelper.Exists(xCriteria))
                    {
                        // Set the x values for the range
                        range = SetRangeX(range, xCriteria);

                        // Remove this item and renumber the indexes
                        pixelQuery.RemoveCriteria(xCriteria.Index);
                    }

                    // If the yCriteria object exists
                    if (NullHelper.Exists(yCriteria))
                    {
                        // Set the y values for the range
                        range = SetRangeY(range, yCriteria);

                        // Remove this item and renumber the indexes
                        pixelQuery.RemoveCriteria(yCriteria.Index);
                    }
                }
                
                // return value
                return range;
            }
            #endregion

            #region CreateSubImage(Point topLeft, Rectangle size)
            /// <summary>
            /// This method returns a Sub Image
            /// </summary>
            public Bitmap CreateSubImage(Point topLeft, Rectangle size)
            {
                // initial value
                Bitmap subImage = null;

                try
                {
                    // for debugging only
                    int originalX = topLeft.X;
                    int originalY = topLeft.Y;

                    // locals
                    int endX = 0;
                    int endY = 0;
                    int a = 0;
                    int b = 0;
                    PixelInformation pixel = null;

                    // if the DirectBitmap.Bitmap exists
                    if ((HasDirectBitmap) && (DirectBitmap.Bitmap != null))
                    {
                        // get a reference to the source image
                        Bitmap source = DirectBitmap.Bitmap;

                        // if if the sub image is not bigger than the source
                        if ((size.Width <= source.Width) && (size.Height <= source.Height))
                        {
                            // if the X is further right than the full width, the x is moved
                            if (topLeft.X > (source.Width - topLeft.X))
                            {
                                // reset the Width
                                int left = source.Width - size.Width;
                                size = new Rectangle(left, size.Y, size.Width, size.Height);
                            }

                            // if the Y is further right than the full width, the width is shortened
                            if (topLeft.Y > (source.Height - topLeft.Y))
                            {
                                int top = source.Height - size.Height;
                                size = new Rectangle(size.X, top, size.Width, size.Height);
                            }

                            // set the endX value
                            endX = topLeft.X + size.Width;
                            endY = topLeft.Y + size.Height;

                            // ensure in range
                            endX = NumericHelper.EnsureInRange(endX, 0, this.Width -1);
                            endY = NumericHelper.EnsureInRange(endY, 0, this.Height -1);

                            // create a subimage
                            subImage = new Bitmap(size.Width, size.Height);

                            // Code To Lockbits
                            BitmapData bitmapData = subImage.LockBits(new Rectangle(0, 0, size.Width, size.Height), ImageLockMode.ReadWrite, source.PixelFormat);
                            IntPtr pointer = bitmapData.Scan0;
                            int imageSize = Math.Abs(bitmapData.Stride) * size.Height;
                            byte[] pixels = new byte[imageSize];
                            Marshal.Copy(pointer, pixels, 0, imageSize);

                            // Unlock the bits
                            subImage.UnlockBits(bitmapData);

                            // End Code To Lockbits

                            // iterate the x pixels
                            for(int x = topLeft.X; x < endX; x++)
                            {
                                // reset b every x position
                                b = 0;

                                // iterate the y pixels
                                for (int y = topLeft.Y; y < endY; y++)
                                {
                                    // get the pixel
                                    pixel = GetPixel(x, y);

                                    // Set the Pixel
                                    subImage.SetPixel(a, b, pixel.Color);    
                            
                                    // Increment the value for b
                                    b++;
                                }
                            
                                // Increment the value for a
                                a++;   
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // For debugging only for now
                    DebugHelper.WriteDebugError("CreateSubImage", "PixelDatabase", error);
                }
                
                // return value
                return subImage;
            }
            #endregion
            
            #region Dispose()
            /// <summary>
            /// method Dispose
            /// </summary>
            public void Dispose()
            {
                if (HasDirectBitmap)
                {
                    // Dispose of the child object
                    DirectBitmap.Dispose();

                    // recommend by VS
                    GC.SuppressFinalize(this);
                }
            }
            #endregion
            
            #region DoesPixelMatchThisCriteria(PixelInformation pixel, PixelCriteria pixelCriteria)
            /// <summary>
            /// This method returns the Pixel Match This Criteria
            /// </summary>
            public bool DoesPixelMatchThisCriteria(PixelInformation pixel, PixelCriteria pixelCriteria)
            {
                // initial value
                bool doesMatch = false;

                // determine the action by the pixelType
                switch (pixelCriteria.PixelType)
                {
                    case PixelTypeEnum.Alpha:

                        // check the Alpha channel
                        doesMatch = CheckMatchAlpha(pixel, pixelCriteria);

                        // required
                        break;

                     case PixelTypeEnum.Average:

                        // check the Average
                        doesMatch = CheckMatchAverage(pixel, pixelCriteria);

                        // required
                        break;

                    case PixelTypeEnum.Blue:

                        // check Blue
                        doesMatch = CheckMatchBlue(pixel, pixelCriteria);

                        // required
                        break;

                    case PixelTypeEnum.BlueAverageDifference:

                        // check the BlueAverageDifference
                        doesMatch = CheckMatchBlueAverageDifference(pixel, pixelCriteria);

                        // required
                        break;

                    case PixelTypeEnum.BlueGreen:

                        // check BlueGreen
                        doesMatch = CheckMatchBlueGreen(pixel, pixelCriteria);

                        // required
                        break;

                    case PixelTypeEnum.BlueGreenDifference:

                        // check the BlueGreenDifference
                        doesMatch = CheckMatchBlueGreenDifference(pixel, pixelCriteria);

                        // required
                        break;

                     case PixelTypeEnum.BlueMaxDifference:

                        // check the BlueMaxDifference
                        doesMatch = CheckMatchBlueMaxDifference(pixel, pixelCriteria);

                        // required
                        break;

                    case PixelTypeEnum.BlueMinDifference:

                        // check the BlueMinDifference
                        doesMatch = CheckMatchBlueMinDifference(pixel, pixelCriteria);

                        // required
                        break;

                    case PixelTypeEnum.BlueRed:

                        // check BlueRed
                        doesMatch = CheckMatchBlueRed(pixel, pixelCriteria);

                        // required
                        break;

                     case PixelTypeEnum.BlueRedDifference:

                        // check the BlueRedDifference
                        doesMatch = CheckMatchBlueRedDifference(pixel, pixelCriteria);

                        // required
                        break;

                     case PixelTypeEnum.Green:

                        // check the Green
                        doesMatch = CheckMatchGreen(pixel, pixelCriteria);

                        // required
                        break;

                    case PixelTypeEnum.GreenAverageDifference:

                        // check the GreenAverageDifference
                        doesMatch = CheckMatchGreenAverageDifference(pixel, pixelCriteria);

                        // required
                        break;

                     case PixelTypeEnum.GreenMaxDifference:

                        // check the GreenMaxDifference
                        doesMatch = CheckMatchGreenMaxDifference(pixel, pixelCriteria);

                        // required
                        break;

                     case PixelTypeEnum.GreenMinDifference:

                        // check the GreenMinDifference
                        doesMatch = CheckMatchGreenMinDifference(pixel, pixelCriteria);

                        // required
                        break;

                    case PixelTypeEnum.GreenRed:

                        // check the GreenRed
                        doesMatch = CheckMatchGreenRed(pixel, pixelCriteria);

                        // required
                        break;

                     case PixelTypeEnum.GreenRedDifference:

                        // check the GreenRedDifference
                        doesMatch = CheckMatchGreenRedDifference(pixel, pixelCriteria);

                        // required
                        break;

                    case PixelTypeEnum.LastUpdate:

                        // check the Alpha channel
                        // doesMatch = ((HasLastUpdate) && (LastUpdate.Available) && (LastUpdate.IsPixelIncluded(pixel.Index)));

                        // there is no reason to check until the minimum
                        if ((HasLastUpdate) && (LastUpdate.Available) && (pixel.Index >= LastUpdate.Minimum) && (pixel.Index <= LastUpdate.Maximum))
                        {
                            // Check
                            doesMatch = LastUpdate.IsPixelIncluded(pixel.Index);
                        }

                        // required
                        break;

                    case PixelTypeEnum.Min:

                        // check the Min
                        doesMatch = CheckMatchMin(pixel, pixelCriteria);

                        // required
                        break;

                     case PixelTypeEnum.Max:

                        // check the Max
                        doesMatch = CheckMatchMax(pixel, pixelCriteria);

                        // required
                        break;

                    case PixelTypeEnum.MinMaxDifference:

                        // check the MinMaxDifference
                        doesMatch = CheckMatchMinMaxDifference(pixel, pixelCriteria);

                        // required
                        break;

                     case PixelTypeEnum.Red:

                        // check Red
                        doesMatch = CheckMatchRed(pixel, pixelCriteria);

                        // required
                        break;

                    case PixelTypeEnum.RedAverageDifference:

                        // check the RedAverageDifference
                        doesMatch = CheckMatchRedAverageDifference(pixel, pixelCriteria);

                        // required
                        break;

                    case PixelTypeEnum.RedMaxDifference:

                        // check the RedMaxDifference
                        doesMatch = CheckMatchRedMaxDifference(pixel, pixelCriteria);

                        // required
                        break;

                    case PixelTypeEnum.RedMinDifference:

                        // check the RedMinDifference
                        doesMatch = CheckMatchRedMinDifference(pixel, pixelCriteria);

                        // required
                        break;

                     case PixelTypeEnum.Total:

                        // check the Total
                        doesMatch = CheckMatchTotal(pixel, pixelCriteria);

                        // required
                        break;
                }

                // return value
                return doesMatch;
            }
            #endregion

            #region DrawLine(PixelCriteria pixelCriteria, int alpha, Bitmap bitmap, StatusUpdate status, Graphics graphics, bool replaceColors = true, Color? colorToUse = null)
            /// <summary>
            /// This method Draws a Line based upon the pixelCriteria
            /// </summary>
            public void DrawLine(PixelCriteria pixelCriteria, int alpha, Bitmap bitmap, StatusUpdate status, Graphics graphics, bool replaceColors = true, Color? colorToUse = null)
            {
                // locals
                bool useColor = false;
                Color color = Color.Empty;

                // This now draws lines in color, or lines in a line color not in your image and then replaces that color.

                // This is a little cumberson, but what this is doing is drawing a line with a replacement color 
                // (a color not in the source image). Next the pixels that match the replacement color are 
                // replaced with a transparent pixel. This has to be done since Pen object does not support full opacity.

                // Create a pen
                Pen pen;

                // if the LineColor has not been set
                if (!this.LineColorSet)
                {
                    // Find a color not in the image
                    this.LineColor = SetLineColor();
                }

                // create the pen to use the LineColor
                pen =  new Pen(LineColor, pixelCriteria.Thickness);

                // if null
                if (NullHelper.Exists(colorToUse))
                {
                    // we are using the color here
                    useColor = true;

                    // local
                    color = (Color) colorToUse;

                    // color
                    pen = new Pen(color, pixelCriteria.Thickness);

                    // no need
                    replaceColors = false;
                }
                
                // Draw the line in LineColor
                graphics.DrawLine(pen, pixelCriteria.StartPoint, pixelCriteria.EndPoint);
                
                // if replaceColors is true
                if (replaceColors)
                {
                    // Reload the PixelDatabase
                    PixelDatabase pixelDatabase = PixelDatabaseLoader.LoadPixelDatabase(bitmap, null);

                    // Now get the pixels that are equal to the lineColor
                    List<PixelInformation> pixels = null;
                    
                    // Get the pixels in the LineColor (it is supposed to be unique)
                    pixels = pixelDatabase.GetPixels(LineColor);

                    // if one or more pixels were founds
                    if (ListHelper.HasOneOrMoreItems(pixels))
                    {
                        // local
                        int count = 0;

                        // iterate the pixels
                        foreach (PixelInformation pixel in pixels)
                        {
                            // get the count
                            count++;

                            // If the status object exists
                            if (NullHelper.Exists(status))
                            {
                                // update every 1,000
                                if (count % 1000 == 0)
                                {
                                    // set a message
                                    string message = "Draw Line has completed  " + String.Format("{0:n0}", count) + " of " + String.Format("{0:n0}", pixels.Count) + ".";

                                    // send updated message
                                    status(message, pixels.Count);
                                }
                            }

                            // attempt to find the source pixel
                            PixelInformation source = GetPixel(pixel.X, pixel.Y);

                            // if the source pixel exists
                            if (NullHelper.Exists(source))
                            {
                                // if we are not using a color, then we need a transparent color
                                if (!useColor)
                                {
                                    // Create a transparent color
                                    color = Color.FromArgb(alpha, source.Color);
                                }

                                // Set the pixels
                                pixel.Color = color;
                               
                                // Set the color
                                this.DirectBitmap.SetPixel(pixel.X, pixel.Y, color);

                                // Update the color of the source
                                source.Color = color;
                            }
                        }

                        // if the status exists
                        if (NullHelper.Exists(status))
                        {
                            // set a message
                            string message = "Draw Line completed with " + String.Format("{0:n0}", pixels.Count) + " updated.";

                            // send updated message
                            status(message, pixels.Count);
                        }
                    }
                }
            }
            #endregion
            
            #region DrawRectangle(Point point1, Point point2, Color color, int thickness)
            /// <summary>
            /// This method Draw Rectangle
            /// </summary>
            /// <param name="point1">The top left corner of the rectangle</param>
            /// <param name="point2">The bottom right</param>
            public void DrawRectangle(Point point1, Point point2, Color color, int thickness)
            {
                // locals
                int a = 0;
                int b = 0;
                int line = 0;
                
                try
                {
                    if ((point1 != Point.Empty) && (point2 != Point.Empty))
                    {
                        // draw top line
                        for (int x = point1.X; x < point2.X; x++)
                        {
                            for (int t = 0; t < thickness; t++)
                            {
                                // set the value for a & b
                                a = x;
                                b = point1.Y + t;    

                                // if in range
                                if (b < Height)
                                {
                                    // Draw the line
                                    DirectBitmap.SetPixel(a, b, color);  
                                }
                            }
                        }

                        // Increment the value for line
                        line++;

                        // draw left line

                        // draw left line
                        for (int y = point1.Y; y < point2.Y; y++)
                        {
                            for (int t = 0; t < thickness; t++)
                            {
                                // set the value for a & b
                                a = point1.X + t;
                                b = y;

                                // if in range
                                if (a < Width)
                                {
                                    // Draw the line
                                    DirectBitmap.SetPixel(a, b, color);  
                                }
                            }
                        }

                         // Increment the value for line
                        line++;

                        // draw bottom line
                        for (int x = point1.X; x < point2.X; x++)
                        {
                            for (int t = 0; t < thickness; t++)
                            {
                                // set the value for a & b
                                a = x;
                                b = point2.Y - t;

                                // if in range
                                if (b < Height)
                                {
                                    // Draw the line
                                    DirectBitmap.SetPixel(a, b, color);  
                                }
                            }
                        }

                         // Increment the value for line
                        line++;

                        // draw right line
                        for (int y = point1.Y; y < point2.Y; y++)
                        {
                            for (int t = 0; t < thickness; t++)
                            {
                                // set the value for a & b
                                a = point2.X - t;
                                b = y;

                                if (a < Width)
                                {
                                    // Draw the line
                                    DirectBitmap.SetPixel(a, b, color);  
                                }
                            }
                        }
                    }

                     // Increment the value for line
                    line++;
                }
                catch (Exception error)
                {
                    // for debugging only for now
                    DebugHelper.WriteDebugError("DrawRectangle", "PixelDatabase", error);
                }
            }
            #endregion
            
            #region DrawRepeatingLines(PixelCriteria pixelCriteria, int alpha, Bitmap bitmap, StatusUpdate status, Graphics graphics, Color? colorToUse = null)
            /// <summary>, 
            /// This method Draw Repeating Lines
            /// </summary>
            public void DrawRepeatingLines(PixelCriteria pixelCriteria, int alpha, Bitmap bitmap, StatusUpdate status, Graphics graphics, Color? colorToUse = null)
            {
                // verify all objects exist                
                if (NullHelper.Exists(pixelCriteria, bitmap, graphics))
                {
                    // iterate the Reps
                    for (int x = 0; x < pixelCriteria.Repititions; x++)
                    {
                        // the first line does not have to be adjusted
                        if (x > 0)
                        {
                            // Move the Start and End Points before the next line is drawn
                            pixelCriteria = MoveLine(pixelCriteria);
                        }

                        // if this is the lastPixel
                        if (x == (pixelCriteria.Repititions - 1))
                        {
                            // Draw the line and replace the LineColor with a transparency. This only has to be done once.
                            DrawLine(pixelCriteria, alpha, bitmap, status, graphics, true, colorToUse);
                        }
                        else
                        {
                            // Draw the line, but do not replace the colors
                            DrawLine(pixelCriteria, alpha, bitmap, status, graphics, false, colorToUse);
                        }
                    }
                }
            }
            #endregion

            #region DrawText(string text, Font font, Point location, StringAlignment textAlignment, StringAlignment lineAlignment, Brush brush)
            /// <summary>
            /// Draw Text
            /// </summary>
            public void DrawText(string text, Font font, Point location, StringAlignment textAlignment, StringAlignment lineAlignment, Brush brush)
            {
                // Get a reference to the internal Bitmap
                Bitmap bitmap = DirectBitmap.Bitmap;

                // Create graphic object that will draw onto the bitmap
                Graphics g = Graphics.FromImage(bitmap);

                // ------------------------------------------
                // Ensure the best possible quality rendering
                // ------------------------------------------
                // The smoothing mode specifies whether lines, curves, and the edges of filled areas use smoothing (also called antialiasing). 
                // One exception is that path gradient brushes do not obey the smoothing mode. 
                // Areas filled using a PathGradientBrush are rendered the same way (aliased) regardless of the SmoothingMode property.
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // The interpolation mode determines how intermediate values between two endpoints are calculated.
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Use this property to specify either higher quality, slower rendering, or lower quality, faster rendering of the contents of this Graphics object.
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // This one is important
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                // Create string formatting options (used for alignment)
                StringFormat format = new StringFormat()
                {
                    Alignment = textAlignment,
                    LineAlignment = lineAlignment                    
                };

                // Draw the text onto the image
                g.DrawString(text, font, brush, location.X, location.Y, format);

                // Flush all graphics changes to the bitmap
                g.Flush();
            }
            #endregion

            #region DrawText(string text, Font font, Point location, StringAlignment textAlignment, StringAlignment lineAlignment, Brush brush)
            /// <summary>
            /// Draw Text
            /// </summary>
            public void DrawText(string text, Font font, Point location, StringAlignment textAlignment, StringAlignment lineAlignment)
            {
                // call the override
                DrawText(text, font, location, textAlignment, lineAlignment, Brushes.Black);
            }
            #endregion

            #region DrawText(string text, Font font, Point location)
            /// <summary>
            /// Draw Text using the default values of StringAlignment.Near, StringAlignment.Center and Brushes.Black
            /// </summary>
            public void DrawText(string text, Font font, Point location)
            {
                // call the override
                DrawText(text, font, location, StringAlignment.Near, StringAlignment.Center, Brushes.Black);
            }
            #endregion
            
            #region FindModifiedPixels(PixelDatabase compareDatabase, StatusUpdate statusUpdate, int startX = 0, int endX = 0, int startY = 0, int endY = 0)
            /// <summary>
            /// This method returns a list of Modified Pixels
            /// </summary>
            public List<PixelInformation> FindModifiedPixels(PixelDatabase compareDatabase, StatusUpdate statusUpdate, int startX = 0, int endX = 0, int startY = 0, int endY = 0)
            {
                // initial value
                List<PixelInformation> modifiedPixels = null;

                // verify the compareDatabase exists
                if (NullHelper.Exists(compareDatabase))
                {
                    // if the dimensions are the same
                    if ((this.Width == compareDatabase.Width) && (this.Height == compareDatabase.Height))
                    {
                        // Create a new collection of 'PixelInformation' objects.
                        modifiedPixels = new List<PixelInformation>();
                                                
                        // Get the total search area
                        int searchWidth = endX - startX;
                        int searchHeight = endY - startY;
                        double totalSearchArea = searchWidth * searchHeight;
                        int searched = 0;

                        // If the statusUpdate object exists
                        if (NullHelper.Exists(statusUpdate))
                        {
                            // notify the caller to setup the graph
                            statusUpdate("SetupGraph: totalSearchArea", (int) totalSearchArea);
                        }

                        // iterate
                        for (int x = startX; x < endX; x++)
                        {
                            // iterate the y pixels
                            for (int y = startY; y < endY; y++)
                            {
                                // Get this color in both databases
                                Color color = this.GetColor(x, y);
                                Color color2 = compareDatabase.GetColor(x, y);

                                // if this pixel has been modifiied
                                if (!IsSameColor(color, color2))
                                {
                                    // Create this pixel
                                    PixelInformation pixel = new PixelInformation(x, y, color2);

                                    // Add this pixel (from the image being changed)
                                    modifiedPixels.Add(pixel);
                                }

                                // Increment the value for searched
                                searched++;

                                // Send an update status message every 1,000
                                if ((searched % 1000 == 0) && (NullHelper.Exists(statusUpdate)))
                                {
                                    // Notify the caller
                                    statusUpdate("UpdateGraph: Searched", searched);
                                }
                            }
                        }

                        // Notify the caller
                        statusUpdate("Modified Pixels Found: " + String.Format("{0:n0}", modifiedPixels.Count), modifiedPixels.Count);
                    }
                    else
                    {
                        // Show a message
                        throw new Exception("The two images are not equal in size");
                    }
                }
                else
                {
                    // Show a message
                    throw new Exception("Both databases most exist to use this feature.");
                }
                
                // return value
                return modifiedPixels;
            }
            #endregion

            #region GetBlankVerticalLines(PixelDatabase pixelDatabase, int total, BackgroundObjectDetectionTypeEnum objectDetectionType)
            /// <summary>
            /// returns a list of Blank Horizontal (y values) Lines. By blank, it means objects that are less than or greater than the total, depending on the objectDetectionType
            /// </summary>
            public static List<int> GetBlankVerticalLines(PixelDatabase pixelDatabase, int total, BackgroundObjectDetectionTypeEnum objectDetectionType)
            {
                // initial value
                List<int> blankVerticalLines = new List<int>();

                // local
                bool lineIsBlank = false;

                // If the pixelDatabase object exists
                if (NullHelper.Exists(pixelDatabase))
                {
                    // iterate the x pixels
                    for(int x = 0; x < pixelDatabase.Width; x++)
                    {
                        // reset
                        lineIsBlank = true;
                        
                        // iterate the lines
                        for(int y = 0; y < pixelDatabase.Height; y++)
                        {
                            // get this pixel
                            PixelInformation pixel = pixelDatabase.GetPixel(x, y);

                            // if the object must be brighter than the background
                            if (objectDetectionType == BackgroundObjectDetectionTypeEnum.ObjectTotalMustBeMoreThan)
                            {
                                // the pixel total is less than total, than this line is not blank
                                if (pixel.Total > total)
                                {
                                    // not blank
                                    lineIsBlank = false;

                                    // exit inner loop
                                    break;
                                }
                            }
                            else
                            {
                                // default, if Pixel total ismore than total, line is not blank
                                if (pixel.Total < total)
                                {
                                    // not blank
                                    lineIsBlank = false;

                                    // exit inner loop
                                    break;
                                }
                            }
                        }

                        // if the value for lineIsBlank is true
                        if (lineIsBlank)
                        {
                            // Add this x
                            blankVerticalLines.Add(x);
                        }
                    }
                }
                
                // return value
                return blankVerticalLines;
            }
            #endregion
            
            #region GetBlankHorizontalLines(PixelDatabase pixelDatabase, int total, BackgroundObjectDetectionTypeEnum objectDetectionType)
            /// <summary>
            /// returns a list of Blank rows (x values) Lines
            /// </summary>
            public static List<int> GetBlankHorizontalLines(PixelDatabase pixelDatabase, int total, BackgroundObjectDetectionTypeEnum objectDetectionType)
            {
                // initial value
                List<int> blankHorizontalLines = new List<int>();

                // local
                bool lineIsBlank = false;

                // If the pixelDatabase object exists
                if (NullHelper.Exists(pixelDatabase))
                {
                    // iterate the y pixels
                    for(int y = 0; y < pixelDatabase.Height; y++)
                    {
                        // reset
                        lineIsBlank = true;
                        
                        // iterate the columns
                        for(int x = 0; x < pixelDatabase.Width; x++)
                        {
                            // get this pixel
                            PixelInformation pixel = pixelDatabase.GetPixel(x, y);

                            // if the object must be brighter than the background
                            if (objectDetectionType == BackgroundObjectDetectionTypeEnum.ObjectTotalMustBeMoreThan)
                            {
                                // the pixel total is less than total, than this line is not blank
                                if (pixel.Total > total)
                                {
                                    // not blank
                                    lineIsBlank = false;

                                    // exit inner loop
                                    break;
                                }
                            }
                            else
                            {
                                // default, if Pixel total ismore than total, line is not blank
                                if (pixel.Total < total)
                                {
                                    // not blank
                                    lineIsBlank = false;

                                    // exit inner loop
                                    break;
                                }
                            }
                        }

                        // if the value for lineIsBlank is true
                        if (lineIsBlank)
                        {
                            // Add this x
                            blankHorizontalLines.Add(y);
                        }
                    }
                }
                
                // return value
                return blankHorizontalLines;
            }
            #endregion
            
            #region GetColor(int x, int y)
            /// <summary>
            /// This method returns the Color at the x, y given.
            /// </summary>
            public Color GetColor(int x, int y)
            {
                // initial value
                Color color = Color.Empty;

                try
                {
                    // get the color
                    color = DirectBitmap.GetPixel(x, y);
                }
                catch (Exception error)
                {
                    // Set the error
                    DebugHelper.WriteDebugError("GetColor", "PixelDatabase.cs", error);
                }

                // return value
                return color;
            }
            #endregion

            #region GetHighestWhiteY(PixelDatabase pixelDatabase)
            /// <summary>
            /// returns the Highest White Y
            /// </summary>
            public static int GetHighestWhiteY(PixelDatabase pixelDatabase)
            {
                // initial value
                int highestWhiteY = -1;

                // If the pixelDatabase object exists
                if (NullHelper.Exists(pixelDatabase))
                {
                    // iterate the x by 10's
                    for(int x = 0; x < pixelDatabase.Width; x += 10)
                    {
                        for (int y = 0; y < pixelDatabase.Height; y++)
                        {
                            // get this pixel
                            PixelInformation pixel = pixelDatabase.GetPixel(x , y);

                            // if white
                            if ((pixel.Total >= 765) && (y > highestWhiteY))
                            {
                                // get the new lowestY
                                highestWhiteY = y;
                            }
                        }
                    }
                }
                
                // return value
                return highestWhiteY;
            }
            #endregion

            #region GetHighestWhiteX(PixelDatabase pixelDatabase)
            /// <summary>
            /// returns the Highest White X
            /// </summary>
            public static int GetHighestWhiteX(PixelDatabase pixelDatabase)
            {
                // initial value
                int highestWhiteX = -1;

                // If the pixelDatabase object exists
                if (NullHelper.Exists(pixelDatabase))
                {
                    // iterate the x by 10's
                    for(int y = 0; y < pixelDatabase.Height; y += 10)
                    {
                        for (int x = 0; x < pixelDatabase.Width; x++)
                        {
                            // get this pixel
                            PixelInformation pixel = pixelDatabase.GetPixel(x , y);

                            // if white
                            if ((pixel.Total >= 765) && (x > highestWhiteX))
                            {
                                // get the new lowestX
                                highestWhiteX = x;
                            }
                        }
                    }
                }
                
                // return value
                return highestWhiteX;
            }
            #endregion

            #region GetLowestWhiteY(PixelDatabase pixelDatabase)
            /// <summary>
            /// returns the Lowest White Y
            /// </summary>
            public static int GetLowestWhiteY(PixelDatabase pixelDatabase)
            {
                // initial value
                int lowestWhiteY = 999999;

                // If the pixelDatabase object exists
                if (NullHelper.Exists(pixelDatabase))
                {
                    // iterate the x by 10's
                    for(int x = 0; x < pixelDatabase.Width; x += 10)
                    {
                        for (int y = 0; y < pixelDatabase.Height; y++)
                        {
                            // get this pixel
                            PixelInformation pixel = pixelDatabase.GetPixel(x , y);

                            // if white
                            if ((pixel.Total >= 765) && (y < lowestWhiteY))
                            {
                                // get the new lowestY
                                lowestWhiteY = y;
                            }
                        }
                    }
                }
                
                // return value
                return lowestWhiteY;
            }
            #endregion

            #region GetLowestWhiteX(PixelDatabase pixelDatabase)
            /// <summary>
            /// returns the Lowest White X
            /// </summary>
            public static int GetLowestWhiteX(PixelDatabase pixelDatabase)
            {
                // initial value
                int lowestWhiteX = 999999;

                // If the pixelDatabase object exists
                if (NullHelper.Exists(pixelDatabase))
                {
                    // iterate the x by 10's
                    for(int y = 0; y < pixelDatabase.Height; y += 10)
                    {
                        for (int x = 0; x < pixelDatabase.Height; x++)
                        {
                            // get this pixel
                            PixelInformation pixel = pixelDatabase.GetPixel(x , y);

                            // if white
                            if ((pixel.Total >= 765) && (x < lowestWhiteX))
                            {
                                // get the new lowestX
                                lowestWhiteX = x;
                            }
                        }
                    }
                }
                
                // return value
                return lowestWhiteX;
            }
            #endregion
            
            #region GetMaxColor(Color color)
            /// <summary>
            /// This method returns the lowest valued color.
            /// </summary>
            /// <param name="color"></param>
            /// <returns></returns>
            public static Color GetMaxColor(Color color)
            {
                // create the 3 color values
                ColorValue red = new ColorValue(PrimaryColorEnum.Red, color.R, "R");
                ColorValue green = new ColorValue(PrimaryColorEnum.Green, color.G, "G");
                ColorValue blue = new ColorValue(PrimaryColorEnum.Blue, color.B, "B");
                List<ColorValue> colors = new List<ColorValue>();
                colors.Add(red);
                colors.Add(green);
                colors.Add(blue);

                // sort the colors by value and then alpha
                colors = colors.OrderBy(x => x.Value).ThenBy(x => x.Alpha).ToList();

                // get the meanValue
                ColorValue maxValue = colors[2];

                // create the meanColor
                int value = maxValue.Value;
                Color maxColor = Color.FromArgb(value, value, value);

                // return value
                return maxColor;
            }
            #endregion

            #region GetMinColor(Color color)
            /// <summary>
            /// This method returns the lowest valued color.
            /// </summary>
            /// <param name="color"></param>
            /// <returns></returns>
            public static Color GetMinColor(Color color)
            {
                // create the 3 color values
                ColorValue red = new ColorValue(PrimaryColorEnum.Red, color.R, "R");
                ColorValue green = new ColorValue(PrimaryColorEnum.Green, color.G, "G");
                ColorValue blue = new ColorValue(PrimaryColorEnum.Blue, color.B, "B");
                List<ColorValue> colors = new List<ColorValue>();
                colors.Add(red);
                colors.Add(green);
                colors.Add(blue);

                // sort the colors by value and then alpha
                colors = colors.OrderBy(x => x.Value).ThenBy(x => x.Alpha).ToList();

                // get the meanValue
                ColorValue minValue = colors[0];

                // create the meanColor
                int value = minValue.Value;
                Color minColor = Color.FromArgb(value, value, value);

                // return value
                return minColor;
            }
            #endregion

            #region GetMeanColor(Color color)
            /// <summary>
            /// This method returns the middle valued color.
            /// </summary>
            /// <param name="color"></param>
            /// <returns></returns>
            public static Color GetMeanColor(Color color)
            {
                // create the 3 color values
                ColorValue red = new ColorValue(PrimaryColorEnum.Red, color.R, "R");
                ColorValue green = new ColorValue(PrimaryColorEnum.Green, color.G, "G");
                ColorValue blue = new ColorValue(PrimaryColorEnum.Blue, color.B, "B");
                List<ColorValue> colors = new List<ColorValue>();
                colors.Add(red);
                colors.Add(green);
                colors.Add(blue);

                // sort the colors by value and then alpha
                colors = colors.OrderBy(x => x.Value).ThenBy(x => x.Alpha).ToList();

                // get the meanValue
                ColorValue meanValue = colors[1];

                // create the meanColor
                int value = meanValue.Value;
                Color meanColor = Color.FromArgb(value, value, value);

                // return value
                return meanColor;
            }
            #endregion
            
            #region DetectObjects(PixelDatabase pixelDatabase, int total, BackgroundObjectDetectionTypeEnum objectDetectionType)
            /// <summary>
            /// method returns the Objects found in an image. 
            /// </summary>
            public static List<Rectangle> DetectObjects(PixelDatabase pixelDatabase, int total, BackgroundObjectDetectionTypeEnum objectDetectionType)
            {
                // initial value
                List<Rectangle> rectangles  = new List<Rectangle>();

                // local
                Rectangle rectangle = new Rectangle();
                List<int> blankHorizontalLines = null;
                List<int> blankVerticalLines = null;
                string query = "";

                // If the pixelDatabase object exists
                // Avoid 
                if (NullHelper.Exists(pixelDatabase))
                {
                    // First step is to get the blank lines
                    // by blank, this means anything that counts as the background
                    blankHorizontalLines = GetBlankHorizontalLines(pixelDatabase, total, objectDetectionType);
                    blankVerticalLines = GetBlankVerticalLines(pixelDatabase, total, objectDetectionType);

                    // if the lines exist
                    if (ListHelper.HasOneOrMoreItems(blankVerticalLines))
                    {
                        // iterate the x rows
                        for(int x = 0; x < blankVerticalLines.Count; x++)
                        {
                            // create an update Query
                            query = "Update" + Environment.NewLine + "Set Color Firebrick" + Environment.NewLine + "Where" + Environment.NewLine + "X = " + blankVerticalLines[x];

                            // update 
                            pixelDatabase.ApplyQuery(query, null);
                        }
                    }

                    // if the lines exist
                    if (ListHelper.HasOneOrMoreItems(blankHorizontalLines))
                    {
                        // iterate the y axix
                        for(int y = 0; y < blankHorizontalLines.Count; y++)
                        {
                            // create an update Query
                            query = "Update" + Environment.NewLine + "Set Color Firebrick" + Environment.NewLine + "Where" + Environment.NewLine + "Y = " + blankHorizontalLines[y];

                            // update 
                            pixelDatabase.ApplyQuery(query, null);
                        }
                    }

                    // Now, this query will make everything white, that isn't Firebrick                    
                    query = "Update" + Environment.NewLine + "Set Color White" + Environment.NewLine + "Where" + Environment.NewLine + "MinMaxDiff < 140";

                    // update 
                    pixelDatabase.ApplyQuery(query, null);

                    // by now we should have an image with white boxes
                    int lowestWhiteY = GetLowestWhiteY(pixelDatabase);
                    int lowestWhiteX = GetLowestWhiteX(pixelDatabase);
                    int highestWhiteY = GetHighestWhiteY(pixelDatabase);
                    int highestWhiteX = GetHighestWhiteX(pixelDatabase);

                    // at this point we know the rectangle that contains everything

                    // set the length
                    int length = highestWhiteY - lowestWhiteY;

                    // create a rectangle
                    rectangle = new Rectangle();
                    rectangle.X = lowestWhiteX;
                    rectangle.Y = lowestWhiteY;
                    rectangle.Height = length;
                    rectangle.Width = 1;
                    
                    // local
                    bool inRectangle = true;

                    // set the index
                    int index = 0;
                    
                    // iterate along the horizontal axis
                    for (int x = lowestWhiteX; x < highestWhiteX; x++)
                    {
                        // get this pixel
                        PixelInformation pixel = pixelDatabase.GetPixel(x, lowestWhiteY);

                        // if the value for inRectangle is true
                        if (inRectangle)
                        {  
                            // if not white
                            if (pixel.Total < 765)
                            {
                                // add this item
                                rectangles.Add(rectangle);

                                // no longer in a rectangle
                                inRectangle = false;
                            }
                            else
                            {
                                // increase the width of the recetangle
                                rectangle.Width++;
                            }
                        }
                        else
                        {
                            // if white
                            if (pixel.Total == 765)
                            {
                                // Increment the value for index
                                index++;

                                // We started a new rectangle
                                inRectangle = true;

                                // create a rectangle
                                rectangle = new Rectangle();

                                // Set the width
                                rectangle.X = x;
                                rectangle.Y = lowestWhiteY;
                                rectangle.Width = 1;
                                rectangle.Height = length;
                            }
                        }
                    }

                    // add the last item
                    rectangles.Add(rectangle);

                    // no longer in a rectangle
                    inRectangle = false;
                }

                // return value
                return rectangles;
            }
            #endregion
            
            #region GetPixel(int x, int y)
            /// <summary>
            /// This method returns the Pixel
            /// </summary>
            public PixelInformation GetPixel(int x, int y)
            {
                // initial value
                PixelInformation pixel = null;

                try
                {
                     // get the color
                    Color color = DirectBitmap.GetPixel(x, y);

                    // Create a new instance of a 'PixelInformation' object.
                    pixel = new PixelInformation();

                    // Set the color
                    pixel.Color = color;
                    pixel.X = x;
                    pixel.Y = y;
                }
                catch (Exception error)
                {
                    // Set the error
                    DebugHelper.WriteDebugError("GetPixel", "PixelDatabase.cs", error);
                }

                // return value
                return pixel;
            }
            #endregion

            #region GetPixels(int x, int y, int height, int width, int max)
            /// <summary>
            /// This method returns a list of Pixels, either all pixels if maxPixels = 0up to the count of pixels
            /// <paramref name="max">This parameter is the maximum pixels to return. If max == 0, all matches are returned </paramref>
            /// </summary>
            public List<PixelInformation> GetPixels(int x, int y, int height, int width, int max = 0)
            {
                // initial value
                List<PixelInformation> pixels = null;

                // local
                int tempCount = 0;
                bool done = false;    

                // if the value for HasDirectBitmap is true
                if (HasDirectBitmap)
                {
                    // Create a new collection of 'PixelInformation' objects.
                    pixels = new List<PixelInformation>();

                    // iterate the x pixels
                    for (int a = x; a < width;a++)
                    {
                        // if the value for done is true
                        if (done)
                        {
                            // exit
                            break;
                        }

                        // iterate the y pixels
                        for (int b = y; b < height;b++)
                        {  
                            // Increment the value for tempCount
                            tempCount++;

                            // get the color at this coordinate
                            Color tempColor = this.DirectBitmap.Bitmap.GetPixel(a, b);
   
                            // Create a new instance of a 'PixelInformation' object.
                            PixelInformation pixel = new PixelInformation();

                            // set the properties
                            pixel.X = a;
                            pixel.Y = b;
                            pixel.Color = tempColor;

                            // Add this item
                            pixels.Add(pixel);

                            // if we have reached the count
                            if ((max > 0) && (tempCount >= max))
                            { 
                                // we have to exit the outer loop
                                done = true;

                                // break out of the loop
                                break;
                            }
                        }
                    }
                }
                
                // return value
                return pixels;
            }
            #endregion

            #region GetPixels(int max = 0)
            /// <summary>
            /// This method returns a list of Pixels, either all pixels if max = 0, else up to the count of max
            /// <paramref name="max">This parameter is the maximum pixels to return. If max == 0, all matches are returned </paramref>
            /// </summary>
            public List<PixelInformation> GetPixels(int max = 0)
            {
                // initial value
                List<PixelInformation> pixels = GetPixels(0, 0, this.Height, this.Width, max);
                
                // return value
                return pixels;
            }
            #endregion
            
            #region GetPixels(Color color)
            /// <summary>
            /// This method returns a list of Pixels
            /// </summary>
            public List<PixelInformation> GetPixels(Color color)
            {
                // initial value
                List<PixelInformation> pixels = null;

                // if the value for HasDirectBitmap is true
                if (HasDirectBitmap)
                {
                    // Create a new collection of 'PixelInformation' objects.
                    pixels = new List<PixelInformation>();

                    // iterate the x pixels
                    for (int x = 0; x < this.DirectBitmap.Bitmap.Width;x++)
                    {
                        // iterate the y pixels
                        for (int y = 0; y < this.DirectBitmap.Bitmap.Height;y++)
                        {  
                            // get the color at this coordinate
                            Color tempColor = this.DirectBitmap.Bitmap.GetPixel(x, y);

                            // if this is the color being sought. Change to compare by values, not color because names #ffffff does not equal white, example
                            if ((tempColor.R == color.R) && (tempColor.G == color.G) && (tempColor.B == color.B) && (tempColor.A == color.A))
                            {
                                // Create a new instance of a 'PixelInformation' object.
                                PixelInformation pixel = new PixelInformation();

                                // set the properties
                                pixel.X = x;
                                pixel.Y = y;
                                pixel.Color = color;

                                // Add this item
                                pixels.Add(pixel);
                            }
                        }
                    }
                }
                
                // return value
                return pixels;
            }
            #endregion
    
            #region HandleDrawLine(PixelQuery pixelQuery, StatusUpdate status)
            /// <summary>
            /// This method Handle Draw Line
            /// </summary>
            public PixelQuery HandleDrawLine(PixelQuery pixelQuery,  StatusUpdate status)
            {
                 // locals
                int alpha = 0;
                
                // if there are one or more criteria items
                if (ListHelper.HasOneOrMoreItems(pixelQuery.Criteria))
                {
                    // get the first criteria
                    PixelCriteria criteria = pixelQuery.Criteria[0];

                    // initial value
                    Color? colorToUse = null;

                    // if draw line
                    if (pixelQuery.ActionType == ActionTypeEnum.DrawLine)
                    {
                        // set the color
                        colorToUse = pixelQuery.Color;
                    }

                    // Create a graphics object
                    Graphics graphics = Graphics.FromImage(this.DirectBitmap.Bitmap);

                    // if we are drawing a single line
                    if (criteria.RepeatType == RepeatTypeEnum.NoRepeat)
                    {
                        // Draw the line
                        DrawLine(criteria, alpha, this.DirectBitmap.Bitmap, status, graphics, true, colorToUse);
                    }
                    else
                    {
                        // Draw repeating lines
                        DrawRepeatingLines(criteria, alpha, this.DirectBitmap.Bitmap, status, graphics, colorToUse);
                    }

                    // load again
                    PixelDatabase pixelDatabase = PixelDatabaseLoader.LoadPixelDatabase(this.DirectBitmap.Bitmap, status);

                    // If the pixelDatabase object exists
                    if (NullHelper.Exists(pixelDatabase))
                    {
                        // Replace out the DirectBitmap
                        this.DirectBitmap = pixelDatabase.DirectBitmap;
                    }
                }

                // return value
                return pixelQuery;
            }
            #endregion
            
            #region HandleGraph(int count, int pixelsUpdated, StatusUpdate status, QueryRange range)
            /// <summary>
            /// This method Handle Graph
            /// </summary>
            public static void HandleGraph(int count, int pixelsUpdated, StatusUpdate status, QueryRange range)
            {
                // refresh every 500,000 in case this is a long query
                if (count % 500000 == 0)
                {  
                    // set the message
                    string message = "Updated " + String.Format("{0:n0}", pixelsUpdated) + " of " +  String.Format("{0:n0}", range.Size);

                    // if the status object exists
                    if (NullHelper.Exists(status))
                    {
                        // notify the caller
                        status(message, count);
                    }                    
                }
            }
            #endregion
            
            #region HandleHideFrom(PixelQuery pixelQuery)
            /// <summary>
            /// This method Handle Hide From
            /// </summary>
            public static void HandleHideFrom(PixelQuery pixelQuery)
            {
                // If the pixelQuery object exists
                if (NullHelper.Exists(pixelQuery))
                {
                    // breakpoint only
                    pixelQuery = null;
                }
            }
            #endregion

            #region HandleNormalization(PixelInformation pixel, PixelQuery pixelQuery, ref int pixelsUpdated, QueryRange range)
            /// <summary>
            /// This method Handle Normalization
            /// </summary>
            public void HandleNormalization(PixelInformation pixel, PixelQuery pixelQuery, ref int pixelsUpdated, QueryRange range)
            {
                // if there is a NormalizeColor set, then only 1 channel is updated
                if (pixelQuery.HasNormalizeColor)
                {
                    // if color red is only pixel updated
                    if (pixelQuery.NormalizeColor == Color.Red)
                    {
                        // Handle only the Red pixels
                        HandleNormalizationRed(pixel, pixelQuery, ref pixelsUpdated, range);
                    }    
                    else if (pixelQuery.NormalizeColor == Color.Green)
                    {
                        // Handle only the Green pixels
                        HandleNormalizationGreen(pixel, pixelQuery, ref pixelsUpdated, range);
                    }    
                    else if (pixelQuery.NormalizeColor == Color.Blue)
                    {
                        // Handle only the Blue pixels
                        HandleNormalizationBlue(pixel, pixelQuery, ref pixelsUpdated);
                    }    
                }
            }
            #endregion

            #region HandleNormalizationBlue(PixelInformation pixel, PixelQuery pixelQuery, ref int pixelsUpdated)
            /// <summary>
            /// This method Handle Blue Normalization
            /// </summary>
            public void HandleNormalizationBlue(PixelInformation pixel, PixelQuery pixelQuery, ref int pixelsUpdated)
            {
                // locals
                int blue = 0;
                Color newColor = Color.Empty;
                int adjustment = 0;
                NormalizeDirectionEnum direction = NormalizeDirectionEnum.Unknown;

                // first check if below min
                if ((pixel.Blue < pixelQuery.Min) || (pixel.Blue > pixelQuery.Max))
                {
                    // Determine the direction, meaning will be increasing or decreasing for this pixel
                    direction = SetDirection(pixel, pixelQuery);

                    do
                    {
                        // if this pixel is below the min
                        if (direction == NormalizeDirectionEnum.Increasing)
                        {
                            // set the adjustment
                            adjustment += pixelQuery.Step;

                            // if we have gone above the max value
                            if ((pixel.Blue + adjustment) > pixelQuery.Min)
                            {
                                // break out of the loop
                                break;
                            }
                        }
                        else if (direction == NormalizeDirectionEnum.Decreasing)
                        {
                            // set the adjustment
                            adjustment -= pixelQuery.Step;

                            // This may look strange to Add a negative number, but the adjustment needs to be a negative value
                            if ((pixel.Blue + adjustment) < pixelQuery.Max)
                            {
                                // break out of the loop
                                break;
                            }
                        }

                    } while (true);

                    // get the new values for Blue, green and blue
                    blue = AdjustColorValue(pixel.Blue, adjustment);
                    
                    // Get the new color
                    newColor = Color.FromArgb(pixel.Alpha, pixel.Red, pixel.Green, blue);

                    // Set the new PixelColor
                    SetPixelColor(pixel.X, pixel.Y, newColor, false, pixel.Index);

                    // Increment the value for pixelsUpdated
                    pixelsUpdated++;
                }
            }
            #endregion

            #region HandleNormalizationGreen(PixelInformation pixel, PixelQuery pixelQuery, ref int pixelsUpdated, QueryRange range)
            /// <summary>
            /// This method Handle Green Normalization
            /// </summary>
            public void HandleNormalizationGreen(PixelInformation pixel, PixelQuery pixelQuery, ref int pixelsUpdated, QueryRange range)
            {
                // locals
                int green = 0;
                Color newColor = Color.Empty;
                int adjustment = 0;
                NormalizeDirectionEnum direction = NormalizeDirectionEnum.Unknown;

                // first check if below min
                if ((pixel.Green < pixelQuery.Min) || (pixel.Green > pixelQuery.Max))
                {
                    // Determine the direction, meaning will be increasing or decreasing for this pixel
                    direction = SetDirection(pixel, pixelQuery);

                    do
                    {
                        // if this pixel is below the min
                        if (direction == NormalizeDirectionEnum.Increasing)
                        {
                            // set the adjustment
                            adjustment += pixelQuery.Step;

                            // if we have gone above the max value
                            if ((pixel.Green + adjustment) > pixelQuery.Min)
                            {
                                // break out of the loop
                                break;
                            }
                        }
                        else if (direction == NormalizeDirectionEnum.Decreasing)
                        {
                            // set the adjustment
                            adjustment -= pixelQuery.Step;

                            // This may look strange to Add a negative number, but the adjustment needs to be a negative value
                            if ((pixel.Green + adjustment) < pixelQuery.Max)
                            {
                                // break out of the loop
                                break;
                            }
                        }

                    } while (true);

                    // get the new values for Green, green and blue
                    green = AdjustColorValue(pixel.Green, adjustment);
                    
                    // Get the new color
                    newColor = Color.FromArgb(pixel.Alpha, pixel.Red, green, pixel.Blue);

                    // Set the new PixelColor
                    SetPixelColor(pixel.X, pixel.Y, newColor, false, pixel.Index);

                    // Increment the value for pixelsUpdated
                    pixelsUpdated++;
                }
            }
            #endregion
            
            #region HandleNormalizationRed(PixelInformation pixel, PixelQuery pixelQuery, ref int pixelsUpdated, QueryRange range)
            /// <summary>
            /// This method Handle Red Normalization
            /// </summary>
            public void HandleNormalizationRed(PixelInformation pixel, PixelQuery pixelQuery, ref int pixelsUpdated, QueryRange range)
            {
                // locals
                int red = 0;
                Color newColor = Color.Empty;
                int adjustment = 0;
                NormalizeDirectionEnum direction = NormalizeDirectionEnum.Unknown;

                // first check if below min
                if ((pixel.Red < pixelQuery.Min) || (pixel.Red > pixelQuery.Max))
                {
                    // Determine the direction, meaning will be increasing or decreasing for this pixel
                    direction = SetDirection(pixel, pixelQuery);

                    do
                    {
                        // if this pixel is below the min
                        if (direction == NormalizeDirectionEnum.Increasing)
                        {
                            // set the adjustment
                            adjustment += pixelQuery.Step;

                            // if we have gone above the max value
                            if ((pixel.Red + adjustment) > pixelQuery.Min)
                            {
                                // break out of the loop
                                break;
                            }
                        }
                        else if (direction == NormalizeDirectionEnum.Decreasing)
                        {
                            // set the adjustment
                            adjustment -= pixelQuery.Step;

                            // This may look strange to Add a negative number, but the adjustment needs to be a negative value
                            if ((pixel.Red + adjustment) < pixelQuery.Max)
                            {
                                // break out of the loop
                                break;
                            }
                        }

                    } while (true);

                    // get the new values for red, green and blue
                    red = AdjustColorValue(pixel.Red, adjustment);
                    
                    // Get the new color
                    newColor = Color.FromArgb(pixel.Alpha, red, pixel.Green, pixel.Blue);

                    // Set the new PixelColor
                    SetPixelColor(pixel.X, pixel.Y, newColor, false, pixel.Index);

                    // Increment the value for pixelsUpdated
                    pixelsUpdated++;
                }
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method  This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Create the Layers
                this.Layers = new List<Layer>();

                // create a layer for Background
                Layer layer = new Layer();
                layer.Name = "Background";
                layer.Visible = true;
                layer.Selected = true;

                // Add this layer
                Layers.Add(layer);
            }
            #endregion
            
            #region IsSameColor(Color color, Color color2)
            /// <summary>
            /// This method returns the Same Color
            /// </summary>
            public static bool IsSameColor(Color color, Color color2)
            {
                // Set the return value to true if all 4 ARGB values match
                bool isSameColor = ((color.R == color2.R) && (color.G == color2.G) && (color.B == color2.B) && (color.A == color2.A));
                
                // return value
                return isSameColor;
            }
            #endregion
            
            #region MoveLine(PixelCriteria pixelCriteria)
            /// <summary>
            /// This method returns the Line
            /// </summary>
            public static PixelCriteria MoveLine(PixelCriteria pixelCriteria)
            {
                // ensure the PixelCriteria exists
                if (NullHelper.Exists(pixelCriteria))
                {
                    // determine the action by the repeat type                    
                    switch (pixelCriteria.RepeatType)
                    {
                        case RepeatTypeEnum.Left:

                            // Adjust the StartPoint
                            pixelCriteria.StartPoint = new Point(pixelCriteria.StartPoint.X - pixelCriteria.Distance, pixelCriteria.StartPoint.Y);

                            // Adjust the EndPoint
                            pixelCriteria.EndPoint = new Point(pixelCriteria.EndPoint.X - pixelCriteria.Distance, pixelCriteria.EndPoint.Y);

                            // required
                            break;

                        case RepeatTypeEnum.Down:

                            // Adjust the StartPoint
                            pixelCriteria.StartPoint = new Point(pixelCriteria.StartPoint.X, pixelCriteria.StartPoint.Y + pixelCriteria.Distance);

                            // Adjust the EndPoint
                            pixelCriteria.EndPoint = new Point(pixelCriteria.EndPoint.X, pixelCriteria.EndPoint.Y + pixelCriteria.Distance);

                            // required
                            break;

                        case RepeatTypeEnum.Up:

                            // Adjust the StartPoint
                            pixelCriteria.StartPoint = new Point(pixelCriteria.StartPoint.X, pixelCriteria.StartPoint.Y - pixelCriteria.Distance);

                            // Adjust the EndPoint
                            pixelCriteria.EndPoint = new Point(pixelCriteria.EndPoint.X, pixelCriteria.EndPoint.Y - pixelCriteria.Distance);

                            // required
                            break;

                        case RepeatTypeEnum.Right:

                            // Adjust the StartPoint
                            pixelCriteria.StartPoint = new Point(pixelCriteria.StartPoint.X + pixelCriteria.Distance, pixelCriteria.StartPoint.Y);

                            // Adjust the EndPoint
                            pixelCriteria.EndPoint = new Point(pixelCriteria.EndPoint.X + pixelCriteria.Distance, pixelCriteria.EndPoint.Y);

                            // required
                            break;
                    }
                }
                
                // return value
                return pixelCriteria;
            }
            #endregion

            #region ResizeImage(System.Drawing.Image imgToResize, Size size)  
            /// <summary>
            /// This method will resize the image. Credit for this is given to Ish Bandhu and this C# Corner article
            /// https://www.c-sharpcorner.com/UploadFile/ishbandhu2009/resize-an-image-in-C-Sharp/
            /// </summary>
            /// <param name="imgToResize"></param>
            /// <param name="size"></param>
            /// <returns></returns>
            public static System.Drawing.Image ResizeImage(System.Drawing.Image imgToResize, Size size)  
            {  
                //Get the image current width  
                int sourceWidth = imgToResize.Width;  
                //Get the image current height  
                int sourceHeight = imgToResize.Height;  
                float nPercent = 0;  
                float nPercentW = 0;  
                float nPercentH = 0;  
                //Calulate  width with new desired size  
                nPercentW = ((float)size.Width / (float)sourceWidth);  
                //Calculate height with new desired size  
                nPercentH = ((float)size.Height / (float)sourceHeight);  
                if (nPercentH < nPercentW)  
                    nPercent = nPercentH;  
                else  
                 nPercent = nPercentW;  

                 //New Width  and Height
                 int destWidth = (int)(sourceWidth * nPercent);                   
                 int destHeight = (int)(sourceHeight * nPercent);  

                 // Create a new bitmap
                 Bitmap bitmap = new Bitmap(destWidth, destHeight);  
                 Graphics g = Graphics.FromImage((System.Drawing.Image)bitmap);  
                 g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;  
                 // Draw image with new width and height  
                 g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);  
                 g.Dispose();  

                 // return value
                 return (System.Drawing.Image)bitmap;  
            }
            #endregion

            #region SaveAs(string path)
            /// <summary>
            /// This method returns the As
            /// </summary>
            public bool SaveAs(string path)
            {
                // initial value
                bool saved = false;

                try
                {
                    // Save
                    DirectBitmap.Bitmap.SetResolution(300, 300);
                    DirectBitmap.Bitmap.Save(path);

                    // set the return value
                    saved = File.Exists(path);
                }
                catch (Exception error)
                {
                    // Write Debug Error
                    DebugHelper.WriteDebugError("SaveAs", "PixelDatabase.cs", error);
                }
                
                // return value
                return saved;
            }
            #endregion
            
            #region ScorePixel(PixelInformation source, PixelInformation target)
            /// <summary>
            /// This method returns the Pixel
            /// </summary>
            public static SearchResult ScorePixel(PixelInformation source, PixelInformation target)
            {
                // initial value
                SearchResult result = new SearchResult();
  
                // if botht the tempDB and the pixel exist                
                if (NullHelper.Exists(source, target))
                {
                    // Score Red, Green, Blue and Alpha
                    result.Score += Math.Abs(source.Red - target.Red);
                    result.Score += Math.Abs(source.Green - target.Green);
                    result.Score += Math.Abs(source.Blue - target.Blue);
                    result.Score += Math.Abs(source.Alpha - target.Alpha);
                }

                // return value
                return result;
            }
            #endregion
            
            #region ScorePixels(List<PixelInformation> source, List<PixelInformation> target)
            /// <summary>
            /// This method returns the Pixels
            /// </summary>
            public static SearchResult ScorePixels(List<PixelInformation> source, List<PixelInformation> target)
            {
                // initial value
                SearchResult result = new SearchResult();

                // verify both lists exists                
                if (ListHelper.HasOneOrMoreItems(source, target))
                {
                    // if the same number of items do not exist
                    if (source.Count != target.Count)
                    {
                        // Set the Score to 1 billion -2
                        result.Score = 9999999998;
                    }
                    else
                    {
                        // iterate the count
                        for (int x = 0; x < source.Count; x++)
                        {
                            // get the tempResult
                            SearchResult tempResult = ScorePixel(source[x], target[x]);

                            // Add this score
                            result.Score += tempResult.Score;
                        }
                    }
                }
                else
                {
                    // Set the Score to 1 billion -1
                    result.Score = 999999999;
                }
                
                // return value
                return result;
            }
            #endregion
            
            #region SearchForSubImage(Bitmap bitmap, int searchDepth = 10)
            /// <summary>
            /// This method returns the For Sub Image
            /// </summary>
            public SearchResult SearchForSubImage(Bitmap bitmap, int searchDepth = 10)
            {
                // initial value
                SearchResult result = null;

                // locals
                PixelDatabase tempDB = null;
                PixelInformation lowestPixel = null;
                PixelInformation tempPixel = null;
                Rectangle rectangle;
                List<PixelInformation> searchPixels = null;
                List<PixelInformation> targetPixels = null;
                bool done = false;
            
                try
                {
                    // load the tempDB
                    tempDB = PixelDatabaseLoader.LoadPixelDatabase(bitmap, null);

                    // If the tempDB object exists
                    if (NullHelper.Exists(tempDB))
                    {
                        // Create a new instance of a 'Rectangle' object.
                        rectangle = new Rectangle();
                        rectangle.Width = tempDB.Width;
                        rectangle.Height = tempDB.Height;

                        // get the searchPixels
                        searchPixels = tempDB.GetPixels(0, 0, rectangle.Height, rectangle.Width);
    
                        // search all pixels in this database
                        for (int x = 0; x < Width; x++)
                        {  
                            // break out
                            if (done)
                            { 
                                // break out of this loop
                                break;
                            }

                            for (int y = 0; y < Height; y++)
                            {
                                // Get the pixel at this location
                                tempPixel = GetPixel(x, y);

                                // if the tempPixel exists
                                if (NullHelper.Exists(tempPixel))
                                {
                                    // get the targetPixels
                                    targetPixels = GetPixels(x, y, rectangle.Height, rectangle.Width);

                                    // Score these pixels
                                    SearchResult score = ScorePixels(searchPixels, targetPixels);

                                    // Score this pixel
                                    tempPixel.Score = score.Score;
    
                                    // if the lowestPixel has not been set yet
                                    if (NullHelper.IsNull(lowestPixel))
                                    {
                                        // set the lowestPixel
                                        lowestPixel = tempPixel;
                                    }
                                    else if (tempPixel.Score < lowestPixel.Score)
                                    {
                                        // Set the new lowestPixel
                                        lowestPixel = tempPixel;

                                        // break out
                                        if (tempPixel.Score == 0)
                                        { 
                                            // set done to true
                                            done = true;

                                            // break out of the loop
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // if the lowetPixel exists
                    if (NullHelper.Exists(lowestPixel))
                    {
                        // Create a new instance of a 'SearchResult' object.
                        result = new SearchResult();

                        // set the result
                        result.Point = new Point(lowestPixel.X, lowestPixel.Y);

                        // Set the Score
                        result.Score = lowestPixel.Score;
                    }
                }
                catch (Exception error)
                {
                    // For debugging only
                    DebugHelper.WriteDebugError("SearchForSubImage", "PixelDatabase.cs", error);
                }
                
                // return value
                return result;
            }
            #endregion
            
            #region SetAlpha(ActionTypeEnum actionType, PixelQuery pixelQuery = null)
            /// <summary>
            /// This method returns the Alpha
            /// </summary>
            public static int SetAlpha(ActionTypeEnum actionType, PixelQuery pixelQuery = null)
            {
                // initial value
                int alpha = 0;

                // if ShowPixels than this changes
               if (actionType == ActionTypeEnum.ShowPixels)
                {
                    // set the alpha to 255
                    alpha = 255;
                }
                else if (actionType == ActionTypeEnum.Update)
                {
                    // If the pixelQuery object exists
                    if (NullHelper.Exists(pixelQuery))
                    {
                        // set the return value
                        alpha = pixelQuery.Alpha;
                    }
                    else
                    {
                        // Set to 255
                        alpha = 255;
                    }
                }
                
                // return value
                return alpha;
            }
            #endregion

            #region SetDirection(PixelInformation pixel, PixelQuery pixelQuery)
            /// <summary>
            /// This method returns the Direction
            /// </summary>
            public static NormalizeDirectionEnum SetDirection(PixelInformation pixel, PixelQuery pixelQuery)
            {
                // initial value
                NormalizeDirectionEnum direction = NormalizeDirectionEnum.Unknown;

                // If the pixelQuery object exists
                if (NullHelper.Exists(pixel, pixelQuery))
                {
                    // if the NormalizeColor is red
                    if (pixelQuery.NormalizeColor == Color.Red)
                    {
                        // if this Green is below the min
                        if (pixel.Red < pixelQuery.Min)
                        {
                            // this is a decreasing query
                            direction = NormalizeDirectionEnum.Increasing;
                        }
                        else if (pixel.Red > pixelQuery.Max)
                        {
                            // this is a decreasing query
                            direction = NormalizeDirectionEnum.Decreasing;
                        }
                    }
                    // if Green
                    else  if (pixelQuery.NormalizeColor == Color.Green)
                    {
                        // if this Green is below the min
                        if (pixel.Green < pixelQuery.Min)
                        {
                            // this is a decreasing query
                            direction = NormalizeDirectionEnum.Increasing;
                        }
                        else if (pixel.Green > pixelQuery.Max)
                        {
                            // this is a decreasing query
                            direction = NormalizeDirectionEnum.Decreasing;
                        }
                    }
                    else if (pixelQuery.NormalizeColor == Color.Blue)
                    {
                        // if this pixel is below the min
                        if (pixel.Blue < pixelQuery.Min)
                        {
                            // this is a decreasing query
                            direction = NormalizeDirectionEnum.Increasing;
                        }
                        else if (pixel.Blue > pixelQuery.Max)
                        {
                            // this is a decreasing query
                            direction = NormalizeDirectionEnum.Decreasing;
                        }
                    }
                    else
                    {
                        // defaults to adjusting Total

                        // if this pixel is below the min
                        if (pixel.Total < pixelQuery.Min)
                        {
                            // this is a decreasing query
                            direction = NormalizeDirectionEnum.Increasing;
                        }
                        else if (pixel.Total > pixelQuery.Max)
                        {
                            // this is a decreasing query
                            direction = NormalizeDirectionEnum.Decreasing;
                        }
                    }
                }
                
                // return value
                return direction;
            }
            #endregion
            
            #region SetLineColor()
            /// <summary>
            /// This method returns the Line Color
            /// </summary>
            public Color SetLineColor()
            {
                // initial value
                Color lineColor = Color.Transparent;

                // locals
                List<PixelInformation> pixels = null;
                
                // iterate red up to 255
                for (int red = 0; red < 255; red++)
                {
                    // iterate green up to 255
                    for (int green = 0; green < 255; green++)
                    {
                        // iterate blue up to 255
                        for (int blue = 0; blue < 255; blue++)
                        {
                            Color color = Color.FromArgb(red, green, blue);

                            // attempt to get a list of pixels with this value
                            pixels = this.GetPixels(color);

                            // if no pixels were found matching this color combination
                            if (!ListHelper.HasOneOrMoreItems(pixels))
                            {
                                // set the return value
                                lineColor = Color.FromArgb(red, green, blue);

                                // Set to true
                                LineColorSet = true;

                                // break out of the loop
                                break;
                            }
                        }

                        // if the line color has been set
                        if (LineColorSet)
                        {
                                // break out of this loop also
                                break;
                        }
                    }

                    // if the line color has been set
                    if (LineColorSet)
                    {
                        // break out of this loop also
                        break;
                    }
                }
                
                // return value
                return lineColor;
            }
            #endregion
            
            #region SetPixelColor(int x, int y, Color color, bool addToLastUpdate, int pixelIndex)
            /// <summary>
            /// This method Set Pixel Color
            /// </summary>
            public void SetPixelColor(int x, int y, Color color, bool addToLastUpdate, int pixelIndex)
            {
                try
                {  
                    // Set the newColor
                    DirectBitmap.SetPixel(x, y, color);

                    // Only add this pixel if it is still available
                    if ((HasLastUpdate) && (LastUpdate.Available) && (addToLastUpdate))
                    {
                        // Add this index to the LastUpdate
                        LastUpdate.AddIndex(pixelIndex);                                 
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("SetPixelColor", "PixelDatabase.cs", error);
                }
            }
            #endregion
            
            #region SetRangeX(QueryRange range, PixelCriteria xCriteria
            /// <summary>
            /// This method returns the Range X
            /// </summary>
            public static QueryRange SetRangeX(QueryRange range, PixelCriteria xCriteria)
            {
                // if the range and xCriteria objects both exist
                if (NullHelper.Exists(range, xCriteria))
                {
                    if (xCriteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                    {
                        range.StartX = xCriteria.MinValue;
                    }
                    else if (xCriteria.ComparisonType == ComparisonTypeEnum.Between)
                    {
                        // Set the MinValue
                        range.StartX = xCriteria.MinValue;
                        range.EndX = xCriteria.MaxValue;
                    }
                    else if (xCriteria.ComparisonType == ComparisonTypeEnum.LessThan)
                    {
                        range.EndX = xCriteria.MaxValue;
                    }
                    else if (xCriteria.ComparisonType == ComparisonTypeEnum.Equals)
                    {
                        // This is only for 1 x value
                        range.StartX = xCriteria.TargetValue;
                        range.EndX = xCriteria.TargetValue;
                    }
                }

                // return value
                return range;
            }
            #endregion

            #region SetRangeY(QueryRange range, PixelCriteria yCriteria
            /// <summary>
            /// This method sets the range values for the Y axis
            /// </summary>
            public static QueryRange SetRangeY(QueryRange range, PixelCriteria yCriteria)
            {
                // if the range and yCriteria objects both eyist
                if (NullHelper.Exists(range, yCriteria))
                {
                    if (yCriteria.ComparisonType == ComparisonTypeEnum.GreaterThan)
                    {
                        range.StartY = yCriteria.MinValue;
                    }
                    else if (yCriteria.ComparisonType == ComparisonTypeEnum.Between)
                    {
                        // Set the MinValue
                        range.StartY = yCriteria.MinValue;
                        range.EndY = yCriteria.MaxValue;
                    }
                    else if (yCriteria.ComparisonType == ComparisonTypeEnum.LessThan)
                    {
                        range.EndY = yCriteria.MaxValue;
                    }
                    else if (yCriteria.ComparisonType == ComparisonTypeEnum.Equals)
                    {
                        // This is only for 1 y value
                        range.StartY = yCriteria.TargetValue;
                        range.EndY = yCriteria.TargetValue;
                    }
                }

                // return value
                return range;
            }
            #endregion

            #region ShouldBeUpdated(PixelInformation pixel, PixelQuery pixelQuery, ref int pixelsUpdated, QueryRange range, int count, StatusUpdate status)
            /// <summary>
            /// This method returns the If Pixel Should Be Updated
            /// </summary>
            public bool ShouldBeUpdated(PixelInformation pixel, PixelQuery pixelQuery, ref int pixelsUpdated, QueryRange range, int count, StatusUpdate status)
            {
                // initial value
                bool shouldPixelBeUpdated = false;
            
                try
                {
                    // locals
                    int expectedCount = 0;
                    int actualCount = 0;
                    bool pixelMatchesThisCriteria = false;
                    List<PixelCriteria> criteriaList = null;
                    
                    // If the pixelQuery object exists
                    if (NullHelper.Exists(pixelQuery))
                    {
                        // set the value for criteriaList
                        criteriaList = pixelQuery.Criteria;
                
                        // Set the expected count
                        expectedCount = criteriaList.Count;
   
                        // Iterate the collection of PixelCriteria objects
                        foreach (PixelCriteria criteria in criteriaList)
                        {
                            // Check if the pixel matches this criteria
                            pixelMatchesThisCriteria = DoesPixelMatchThisCriteria(pixel, criteria);

                            // if the value for pixelMatchesThisCriteria is true
                            if (pixelMatchesThisCriteria)
                            {
                                // Increment the value for actualTrueCount
                                actualCount++;
                            }
                            else
                            {
                                // break out of the loop
                                break;
                            }
                        }

                        // set to true if the expected count matches the actual count
                        shouldPixelBeUpdated = (expectedCount == actualCount);

                        // if the value for shouldPixelBeUpdated is true
                        if ((shouldPixelBeUpdated) && (pixelQuery.Scatter) && (pixelQuery.HasShuffler))
                        {
                            // get a value
                            int rawRandomValue = 0;
                            double randomValue = 0;
                        
                            // get the next value. Times .01 because the shuffler is set to 1 to 10,000, but the values are 0.01 to 99.99
                            rawRandomValue = pixelQuery.Shuffler.PullNumber();
    
                            // lower the value
                            randomValue = (rawRandomValue * .01);
                            
                            // if the randomValue is higher than the ScatterPercent
                            shouldPixelBeUpdated = (randomValue <= pixelQuery.ScatterPercent);
                        }
                        else if ((shouldPixelBeUpdated) && (pixelQuery.Normalize))
                        {
                            // Handle the Normalizaiton code
                            HandleNormalization(pixel, pixelQuery, ref pixelsUpdated, range);

                            // Handle the graph
                            HandleGraph(count, pixelsUpdated, status, range);    
                        
                            // this pixel was already updated
                            shouldPixelBeUpdated = false;
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("ShouldBeUpdated", "PixelDatabase", error);
                }

                // return value
                return shouldPixelBeUpdated;
            }
            #endregion
            
            #region SplitImage(SplitImageSettings splitImageSettings, StatusUpdate status)
            /// <summary>
            /// This method Splits Image
            /// </summary>
            public int SplitImage(SplitImageSettings splitImageSettings, StatusUpdate status)
            {
                // local
                int pixelsUpdated = 0;
                Color color = Color.Empty;
                Color newColor = Color.Empty;
                int inverseX = 0;
                int splitX = 0;
                int x = 0;
                int y = 0;
                
                try
                {
                    // If the splitImageSettings object exists
                    if (NullHelper.Exists(splitImageSettings))
                    {
                        // If SplitX is not set
                        if ((splitImageSettings.SplitX == 0) && (this.Width > 2))
                        {
                            // Set SplitX to half way
                            splitImageSettings.SplitX = this.Width / 2;
                        }

                        // get a local copy for shorter typing
                        splitX = splitImageSettings.SplitX;

                        // Create a new instance of a 'QueryRange' object.
                        QueryRange range = new QueryRange();

                        // y is the same for both directions
                        range.StartY = 0;
                        range.EndY = this.directBitmap.Height -1;

                        if (splitImageSettings.Direction == SplitImageDirectionEnum.TakeRight)
                        {   
                            range.StartX = 0;
                            range.EndX = splitX - 1;                            
                        }
                        else
                        {
                            range.StartX = splitX + 1;
                            range.EndX = range.StartX + splitImageSettings.SplitX - 1;
                        }

                        // iterate the y pixels
                        for (y = range.StartY; y <= range.EndY; y++)
                        {
                            // Update the Graph every y
                            HandleGraph(pixelsUpdated, pixelsUpdated, status, range);

                            // iterate the x pixels
                            for (x = range.StartX; x <= range.EndX; x++)
                            {
                                // Increment the value for count
                                pixelsUpdated++;

                                // we must get the color from the inverse of this pixel.
                                // Example: In a 256 x 256 iimage, SplitX is 128.
                                // Pixel at 129, 0 comes from 127, 0.
                                // Pixel at 130, 0 comes from 126, 0.
                                // down to
                                // Pixel at 256, 256 comes from 0, 256.

                                if (splitImageSettings.Direction == SplitImageDirectionEnum.TakeRight)
                                {
                                   inverseX = splitX + splitX - x + 1;
                                }
                                else
                                {  
                                    // get the inverse amount
                                    inverseX = splitX - (x - splitX);
                                }

                                // Safeguard
                                if ((inverseX >= 0) && (inverseX < this.Width -1) && (x < this.Width))
                                {
                                    try
                                    {
                                        // set the color
                                        color = directBitmap.GetPixel(inverseX, y);

                                        // Now apply the color to the new pixel
                                        this.DirectBitmap.SetPixel(x, y, color);
                                    }
                                    catch (Exception error)
                                    {
                                        // trap the error
                                        DebugHelper.WriteDebugError("SplitImage 2", "PixelDatabase", error);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("SplitImage", "PixelDatabase", error);
                }

                // return value
                return pixelsUpdated;
            }
            #endregion
            
            #region SwapColor(Color previousColor, PixelQuery pixelQuery)
            /// <summary>
            /// This method swaps one color with another
            /// </summary>
            public static Color SwapColor(Color previousColor, PixelQuery pixelQuery)
            {
                // initial value
                Color color = previousColor;

                // If the pixelQuery object exists
                if (NullHelper.Exists(pixelQuery))                
                {
                    switch (pixelQuery.SwapType)
                    {
                        case SwapTypeEnum.BlueToGreen:

                            // create the new color
                            color = Color.FromArgb(previousColor.A, previousColor.R, previousColor.B, previousColor.G);

                            // required
                            break;

                        case SwapTypeEnum.RedToBlue:

                            // create the new color
                            color = Color.FromArgb(previousColor.A, previousColor.B, previousColor.G, previousColor.R);

                            // required
                            break;

                        case SwapTypeEnum.RedToGreen:

                            // create the new color
                            color = Color.FromArgb(previousColor.A, previousColor.G, previousColor.R, previousColor.B);

                            // required
                            break;
                    }
                }
                
                // return value
                return color;
            }
        #endregion

        #endregion

        #region Properties

            #region Abort
            /// <summary>
            /// This property gets or sets the value for 'Abort'.
            /// </summary>
            public bool Abort
            {
                get { return abort; }
                set { abort = value; }
            }
            #endregion
            
            #region DirectBitmap
            /// <summary>
            /// This property gets or sets the value for 'DirectBitmap'.
            /// </summary>
            public DirectBitmap DirectBitmap
            {
                get { return directBitmap; }
                set { directBitmap = value; }
            }
            #endregion
            
            #region HasDirectBitmap
            /// <summary>
            /// This property returns true if this object has a 'DirectBitmap'.
            /// </summary>
            public bool HasDirectBitmap
            {
                get
                {
                    // initial value
                    bool hasDirectBitmap = (this.DirectBitmap != null);
                    
                    // return value
                    return hasDirectBitmap;
                }
            }
            #endregion
            
            #region HasLastUpdate
            /// <summary>
            /// This property returns true if this object has a 'LastUpdate'.
            /// </summary>
            public bool HasLastUpdate
            {
                get
                {
                    // initial value
                    bool hasLastUpdate = (this.LastUpdate != null);
                    
                    // return value
                    return hasLastUpdate;
                }
            }
            #endregion
            
            #region HasLayers
            /// <summary>
            /// This property returns true if this object has a 'Layers'.
            /// </summary>
            public bool HasLayers
            {
                get
                {
                    // initial value
                    bool hasLayers = (this.Layers != null);
                    
                    // return value
                    return hasLayers;
                }
            }
            #endregion
            
            #region HasMaskManager
            /// <summary>
            /// This property returns true if this object has a 'MaskManager'.
            /// </summary>
            public bool HasMaskManager
            {
                get
                {
                    // initial value
                    bool hasMaskManager = (this.MaskManager != null);
                    
                    // return value
                    return hasMaskManager;
                }
            }
            #endregion
            
            #region HasPixelQuery
            /// <summary>
            /// This property returns true if this object has a 'PixelQuery'.
            /// </summary>
            public bool HasPixelQuery
            {
                get
                {
                    // initial value
                    bool hasPixelQuery = (this.PixelQuery != null);
                    
                    // return value
                    return hasPixelQuery;
                }
            }
            #endregion
            
            #region HasResetPath
            /// <summary>
            /// This property returns true if the 'ResetPath' exists.
            /// </summary>
            public bool HasResetPath
            {
                get
                {
                    // initial value
                    bool hasResetPath = (!String.IsNullOrEmpty(this.ResetPath));
                    
                    // return value
                    return hasResetPath;
                }
            }
            #endregion
            
            #region HasUndoPath
            /// <summary>
            /// This property returns true if the 'UndoPath' exists.
            /// </summary>
            public bool HasUndoPath
            {
                get
                {
                    // initial value
                    bool hasUndoPath = (!String.IsNullOrEmpty(this.UndoPath));
                    
                    // return value
                    return hasUndoPath;
                }
            }
            #endregion

            #region Height
            /// <summary>
            /// This read only property returns the Height of the DirectBitmap.Bitmap.
            /// </summary>
            public int Height
            {
                get
                {
                    // initial value
                    int height = 0;

                    // if the DirectBitmap.Bitmap exists
                    if ((HasDirectBitmap) && (DirectBitmap.Bitmap != null))
                    {   
                        // set the return value
                        height = DirectBitmap.Bitmap.Height;
                    }

                    // return value
                    return height;
                }                
            }
            #endregion
            
            #region LastUpdate
            /// <summary>
            /// This property gets or sets the value for 'LastUpdate'.
            /// </summary>
            public LastUpdate LastUpdate
            {
                get { return lastUpdate; }
                set { lastUpdate = value; }
            }
            #endregion
            
            #region Layers
            /// <summary>
            /// This property gets or sets the value for 'Layers'.
            /// </summary>
            public List<Layer> Layers
            {
                get { return layers; }
                set { layers = value; }
            }
            #endregion
            
            #region LineColor
            /// <summary>
            /// This property gets or sets the value for 'LineColor'.
            /// </summary>
            public Color LineColor
            {
                get { return lineColor; }
                set { lineColor = value; }
            }
            #endregion
            
            #region LineColorSet
            /// <summary>
            /// This property gets or sets the value for 'LineColorSet'.
            /// </summary>
            public bool LineColorSet
            {
                get { return lineColorSet; }
                set { lineColorSet = value; }
            }
            #endregion
            
            #region MaskManager
            /// <summary>
            /// This property gets or sets the value for 'MaskManager'.
            /// </summary>
            public MaskManager MaskManager
            {
                get { return maskManager; }
                set { maskManager = value; }
            }
            #endregion
            
            #region PixelQuery
            /// <summary>
            /// This property gets or sets the value for 'PixelQuery'.
            /// </summary>
            public PixelQuery PixelQuery
            {
                get { return pixelQuery; }
                set { pixelQuery = value; }
            }
            #endregion
            
            #region ResetPath
            /// <summary>
            /// This property gets or sets the value for 'ResetPath'.
            /// </summary>
            public string ResetPath
            {
                get { return resetPath; }
                set { resetPath = value; }
            }
            #endregion
            
            #region UndoPath
            /// <summary>
            /// This property gets or sets the value for 'UndoPath'.
            /// </summary>
            public string UndoPath
            {
                get { return undoPath; }
                set { undoPath = value; }
            }
            #endregion

            #region Width
            /// <summary>
            /// This read only property returns the Width of the DirectBitmap.Bitmap.
            /// </summary>
            public int Width
            {
                get
                {
                    // initial value
                    int width = 0;

                    // if the DirectBitmap.Bitmap exists
                    if ((HasDirectBitmap) && (DirectBitmap.Bitmap != null))
                    {   
                        // set the return value
                        width = DirectBitmap.Bitmap.Width;
                    }

                    // return value
                    return width;
                }                
            }
            #endregion
            
        #endregion

    }
    #endregion

}