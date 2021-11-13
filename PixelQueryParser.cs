

#region using statements

using DataJuggler.UltimateHelper;
using DataJuggler.UltimateHelper.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DataJuggler.PixelDatabase.Enumerations;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class PixelQueryParser
    /// <summary>
    /// This class is used to parse querries into PixelQuery objects
    /// </summary>
    public class PixelQueryParser
    {
        
        #region Methods

            #region AssignColor(string assignToColorText)
            /// <summary>
            /// This method is used to return Red, Green or Blue
            /// if either of those are passed in.
            /// </summary>
            /// <param name="assignToColorText">Only Red, Green or Blue may be passed in</param>
            public static RGBColor AssignColor(string assignToColorText)
            {
                // initial value
                RGBColor assignColor = RGBColor.NotSet;

                // If the assignToColorText string exists
                if (TextHelper.Exists(assignToColorText))
                {
                    // Determine the action by the assignToColorText
                    switch (assignToColorText)
                    {
                        case "red":

                            // Set the return value
                            assignColor = RGBColor.Red;

                            // required
                            break;

                        case "green":

                             // Set the return value
                            assignColor = RGBColor.Green;

                            // required
                            break;

                        case "blue":

                             // Set the return value
                            assignColor = RGBColor.Blue;

                            // required
                            break;
                    }
                }
                
                // return value
                return assignColor;
            }
            #endregion
            
            #region CheckForGraident(List<TextLine> lines)
            /// <summary>
            /// This method returns the For Graident
            /// </summary>
            public static Gradient CheckForGraident(List<TextLine> lines)
            {
                // initial value
                Gradient gradient = null;

                // create the expectation to control which line should be parsed
                NextLineExpectationEnum expectation = NextLineExpectationEnum.Unknown;

                // Get the words
                List<Word> words = null;
                char[] delimiter = new char[] { ' ' };

                if (ListHelper.HasOneOrMoreItems(lines))
                {
                     // Iterate the collection of TextLine objects
                    foreach (TextLine line in lines)
                    {
                        if (expectation == NextLineExpectationEnum.Unknown)
                        {
                            // if the line starts with Normalize                        
                            if (line.Text.ToLower().StartsWith("create gradient"))
                            {
                                // Create an emptyGradient
                                gradient = new Gradient();

                                // change the expectation
                                expectation = NextLineExpectationEnum.SetColor1;
                            }
                        }
                        else if (expectation == NextLineExpectationEnum.SetColor1)
                        {
                            // Get the words of this line
                            words = TextHelper.GetWords(line.Text, delimiter);

                            // Create the color from these words
                            gradient.Color1 = ParseGradientColor(words);

                            // change the expectation
                            expectation = NextLineExpectationEnum.SetColor2;
                        }
                        else if (expectation == NextLineExpectationEnum.SetColor2)
                        {
                            // Get the words of this line
                            words = TextHelper.GetWords(line.Text, delimiter);

                            // Create the color from these words
                            gradient.Color2 = ParseGradientColor(words);
                        }
                    }
                }
                
                // return value
                return gradient;
            }
            #endregion

            #region CheckForApplyGrayScale(List<TextLine> lines, ref GrayScaleFormulaEnum grayScaleFormula)
            /// <summary>
            /// This method returns true if the second line is Set Grayscale
            /// </summary>
            public static bool CheckForApplyGrayScale(List<TextLine> lines, ref GrayScaleFormulaEnum grayScaleFormula)
            {
                // initial value
                bool applyGrayscale = false;

                // if there are two lines
                if (ListHelper.HasXOrMoreItems(lines, 2))
                {
                    // if this is the line
                    if (lines[1].Text.ToLower().StartsWith("set grayscale"))
                    {  
                        // set to true
                        applyGrayscale = true;

                        // if red
                        if (lines[1].Text.ToLower().Contains("red"))
                        {
                            // set to red
                            grayScaleFormula = GrayScaleFormulaEnum.TakeRed;
                        }
                        else if (lines[1].Text.ToLower().Contains("green"))
                        {
                            // set to green
                            grayScaleFormula = GrayScaleFormulaEnum.TakeGreen;
                        }
                        else if (lines[1].Text.ToLower().Contains("blue"))
                        {
                            // set to green
                            grayScaleFormula = GrayScaleFormulaEnum.TakeBlue;
                        }
                        else if (lines[1].Text.ToLower().Contains("min"))
                        {
                            // set to green
                            grayScaleFormula = GrayScaleFormulaEnum.TakeMin;
                        }
                        else if (lines[1].Text.ToLower().Contains("max"))
                        {
                            // set to green
                            grayScaleFormula = GrayScaleFormulaEnum.TakeMax;
                        }
                        else if (lines[1].Text.ToLower().Contains("mean"))
                        {
                            // set to green
                            grayScaleFormula = GrayScaleFormulaEnum.TakeMean;
                        }
                        else
                        {
                            // set to average
                            grayScaleFormula = GrayScaleFormulaEnum.TakeAverage;
                        }
                    }
                }
                
                // return value
                return applyGrayscale;
            }
            #endregion
            
            #region CheckForNormalize(List<TextLine> lines)
            /// <summary>
            /// This method returns the For Normalize
            /// </summary>
            public static NormalizeSettings CheckForNormalize(List<TextLine> lines)
            {
                // initial value
                NormalizeSettings normalizeSettings = new NormalizeSettings();
    
                // If the lines collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(lines))
                {
                    // Iterate the collection of TextLine objects
                    foreach (TextLine line in lines)
                    {
                        // if the line starts with Normalize                        
                        if (line.Text.ToLower().StartsWith("normalize "))
                        {
                            // get the words
                            List<Word> words = TextHelper.GetWords(line.Text);

                            // if there are exactly 4 or more words
                            if (ListHelper.HasXOrMoreItems(words, 4))
                            {
                                // Set the value for the property 'Normalize' to true
                                normalizeSettings.Normalize = true;

                                // Set the Min
                                normalizeSettings.Min = NumericHelper.ParseInteger(words[1].Text, 0, -1);

                                // Set the Max
                                normalizeSettings.Max = NumericHelper.ParseInteger(words[2].Text, 0, -1);

                                // Set the Step
                                normalizeSettings.Step = NumericHelper.ParseInteger(words[3].Text, 0, -1);

                                // if there are 5 or more words
                                if (ListHelper.HasXOrMoreItems(words, 5))
                                {
                                    // Attempt to parse the color
                                    normalizeSettings.Color = ParseColor(words[4].Text);
                                }

                                // !f not valid
                                if (!normalizeSettings.IsValidNormalizeQuery)
                                {
                                    // do not Normalize
                                    normalizeSettings.Normalize = false;
                                }
                            }
                        }
                        else
                        {
                            // Add this line
                            normalizeSettings.Lines.Add(line);
                        }
                    }
                }
                
                // return value
                return normalizeSettings;
            }
            #endregion
            
            #region CheckForScatter(List<TextLine> lines)
            /// <summary>
            /// This method returns the For Scatter
            /// </summary>
            public static ScatterSettings CheckForScatter(List<TextLine> lines)
            {
                // initial value
                ScatterSettings settings = new ScatterSettings();

                // locals
                char[] delimiters = new char[]{ ' ' };
                double scatterValue = 0;

                try
                {
                    // If the lines collection exists and has one or more items
                    if (ListHelper.HasOneOrMoreItems(lines))
                    {
                        // Iterate the collection of TextLine objects
                        foreach (TextLine line in lines)
                        {
                            // if this is a scatter line
                            if (line.Text.ToLower().StartsWith("scatter"))
                            {
                                // Get the words
                                List<Word> words = TextHelper.GetWords(line.Text, delimiters);

                                // if there are two or more words
                                if (words.Count >= 2)
                                {
                                    // Parse the scatter value
                                    scatterValue = NumericHelper.ParseDouble(words[1].Text, 0, -1);

                                    // if the scatter value is in range
                                    if ((scatterValue >= .0001) && (scatterValue <= 99.9999))
                                    {
                                        // create the se
                                        settings.Scatter = true;
                                        settings.ScatterPercent = scatterValue;
                                    }
                                }

                                // if there are 3 or more words
                                if (words.Count >= 3)
                                {  
                                    // Parse ModX
                                    settings.ModX = NumericHelper.ParseInteger(words[2].Text, 0, -1);
                                }

                                // if there are 4 words
                                if (words.Count == 4)
                                {
                                    // Parse ModY
                                    settings.ModY = NumericHelper.ParseInteger(words[3].Text, 0, -1);
                                }
                            }
                            else
                            {
                                // Add this line
                                settings.TextLines.Add(line);
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("CheckForScatter", "PixelQueryParser", error);
                }
                
                // return value
                return settings;
            }
            #endregion
            
            #region CheckForSplitImage(List<TextLine> lines)
            /// <summary>
            /// This method returns the For Split Image
            /// </summary>
            public static SplitImageSettings CheckForSplitImage(List<TextLine> lines)
            {
                // initial value
                SplitImageSettings splitImageSettings = null;

                // If the lines collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(lines))
                {
                    // Iterate the collection of TextLine objects
                    foreach (TextLine line in lines)
                    {
                        // if splitimage exists in this line
                        if (line.Text.ToLower().Contains("splitimage"))
                        {
                            // Create a new instance of a 'SplitImageSettings' object.
                            splitImageSettings = new SplitImageSettings();

                            // create the delimiter
                            char[] delimiter = new char[] { ' ' };

                            // Get the words
                            List<Word> words = TextHelper.GetWords(line.Text, delimiter);

                            // If the words collection exists and has one or more items
                            if (ListHelper.HasOneOrMoreItems(words))
                            {
                                // Iterate the collection of Word objects
                                foreach (Word word in words)
                                {
                                    // if this is the split image
                                    if (word.Text.ToLower() == "splitimage")
                                    {
                                        // do nothiing
                                    }
                                    else if (word.Text.ToLower() == "takeleft")
                                    {
                                        // Set to TakeLeft
                                        splitImageSettings.Direction = SplitImageDirectionEnum.TakeLeft;
                                    }
                                    else if (word.Text.ToLower() == "takeright")
                                    {
                                        // Set to TakeLeft
                                        splitImageSettings.Direction = SplitImageDirectionEnum.TakeRight;
                                    }
                                    else
                                    {
                                        // get the value for X
                                        int x = NumericHelper.ParseInteger(word.Text, 0, -1);

                                        // If the value for x is greater than zero
                                        if (x > 0)
                                        {
                                            // Set the value for X
                                            splitImageSettings.SplitX = x; 
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                
                // return value
                return splitImageSettings;
            }
            #endregion
            
            #region CreatePixelCriteria(string text, ActionTypeEnum actionType, int lineNumber, PixelCriteria existingCriteria = null)
            /// <summary>
            /// This method returns the Pixel Criteria
            /// </summary>
            public static PixelCriteria CreatePixelCriteria(string text, ActionTypeEnum actionType, int lineNumber, PixelCriteria existingCriteria = null)
            {
                // initial value
                PixelCriteria pixelCriteria = null;

                // only use a space as a delimiter character
                char[] delimiterChars = { ' '};

                // If the text string exists
                if (TextHelper.Exists(text))
                {
                    // Set the BackColor
                    if (actionType == ActionTypeEnum.SetBackColor)
                    {
                        // Create a new instance of a 'PixelCriteria' object.
                        pixelCriteria = new PixelCriteria();

                        // Get the words
                        List<Word> words = TextHelper.GetWords(text, delimiterChars);

                        // If the words collection exists and has one or more items
                        if (ListHelper.HasOneOrMoreItems(words))
                        {
                            // if there are 3 words
                            if (words.Count == 3)
                            {
                                // if the third word is remove
                                if (words[2].Text.ToLower() == "remove")
                                {
                                    // Set the value for the property 'RemoveBackColor' to true
                                    pixelCriteria.RemoveBackColor = true;
                                }
                                else
                                {
                                    // Set the BackColor
                                    pixelCriteria.BackColor = ParseColor(words[2].Text);  
                                }
                            }
                            else if (words.Count == 5)
                            {
                                // set the value for Red, Green & Blue
                                int red = NumericHelper.ParseInteger(words[2].Text, -1, -1);
                                int green = NumericHelper.ParseInteger(words[3].Text, -1, -1);
                                int blue = NumericHelper.ParseInteger(words[4].Text, -1, -1);

                                // if all the RGB values are set
                                if ((red >= 0) && (green >= 0) && (blue >= 0))
                                {
                                    // Set the BackColor
                                    pixelCriteria.BackColor = Color.FromArgb(red, green, blue);
                                }
                            }
                        }
                    }
                    // if this is a draw line
                    else if ((actionType == ActionTypeEnum.DrawTransparentLine) || (actionType == ActionTypeEnum.DrawLine))
                    {
                        // if the existingCriteria 
                        if (NullHelper.IsNull(existingCriteria))
                        {
                            // Create a new instance of a 'PixelCriteria' object.
                            pixelCriteria = new PixelCriteria();
                        }
                        else
                        {
                            // use the existing criteria so more properties can be set on it
                            pixelCriteria = existingCriteria;
                        }

                        // Set to DrawLine
                        pixelCriteria.PixelType = PixelTypeEnum.DrawLine;

                        // if this is the first line
                        if (lineNumber == 1)
                        {
                            // Get the words
                            List<Word> words = TextHelper.GetWords(text, delimiterChars);

                            // If the words collection exists and has three or more items
                            if (ListHelper.HasXOrMoreItems(words, 3))
                            {
                                // Get the lastWord
                                Word lastWord = words[2];

                                // Set the thickness
                                pixelCriteria.Thickness = NumericHelper.ParseInteger(lastWord.Text, -1000, -1001);
                            }
                        }
                        else if (lineNumber == 3)
                        {
                            // Set the RepeatType and the repeating attributes
                            pixelCriteria.RepeatType = SetRepeatType(text);

                            // if the repeat type was found
                            if (pixelCriteria.RepeatType != RepeatTypeEnum.NoRepeat)
                            {
                                // get the text after the repeat 
                                string repeatText = GetRepeatText(text, pixelCriteria.RepeatType);

                                // get the words
                                List<Word> words = TextHelper.GetWords(repeatText);

                                // if there are exactly two words
                                if ((ListHelper.HasOneOrMoreItems(words)) && (words.Count == 2))
                                {  
                                    // set the repititions
                                    pixelCriteria.Repititions = NumericHelper.ParseInteger(words[0].Text, 0, -1);

                                    // set the Distance
                                    pixelCriteria.Distance = NumericHelper.ParseInteger(words[1].Text, 0, -1);
                                }
                            }
                        }
                    }
                    else if (!text.StartsWith("where"))
                    {
                        // Create a new instance of a 'PixelCriteria' object.
                        pixelCriteria = new PixelCriteria();

                        // if this text contains bluegreen
                        if ((text.StartsWith("x ")) || (text.Contains(" x ")))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.X;
                        }
                        else if ((text.StartsWith("y ")) || (text.Contains(" y ")))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.Y;
                        }
                        else if (text.Contains("bluegreendiff"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.BlueGreenDifference;
                        }
                        else if (text.Contains("bluemindiff"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.BlueMinDifference;
                        }
                        else if (text.Contains("bluemaxdiff"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.BlueMaxDifference;
                        }
                        else if (text.Contains("blueaveragediff"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.BlueAverageDifference;
                        }
                        else if (text.Contains("greenaveragediff"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.GreenAverageDifference;
                        }
                        else if (text.Contains("greenmindiff"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.GreenMinDifference;
                        }
                        else if (text.Contains("greenmaxdiff"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.GreenMaxDifference;
                        }
                        else if (text.Contains("redaveragediff"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.RedAverageDifference;
                        }
                        else if (text.Contains("redmindiff"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.RedMinDifference;
                        }
                        else if (text.Contains("redmaxdiff"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.RedMaxDifference;
                        }
                        else if (text.Contains("blueaveragediff"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.BlueAverageDifference;
                        }
                        else if (text.Contains("bluegreen"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.BlueGreen;
                        }
                        else if (text.Contains("bluereddiff"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.BlueRedDifference;
                        }
                        else if (text.Contains("bluered"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.BlueRed;
                        }
                        else if (text.Contains("greenreddiff"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.GreenRedDifference;
                        }
                        else if (text.Contains("greenred"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.GreenRed;
                        }
                        else if (text.Contains("minmaxdiff"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.MinMaxDifference;
                        }
                        // if this text contains min
                        else if (text.Contains("min"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.Min;
                        }
                        else if (text.Contains("max"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.Max;
                        }
                        else if (text.Contains("average"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.Average;
                        }
                        else if (text.Contains("red"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.Red;
                        }
                        else if (text.Contains("green"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.Green;
                        }
                        else if (text.Contains("blue"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.Blue;
                        }
                        else if (text.Contains("total"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.Total;
                        }
                        else if (text.Contains("alpha"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.Alpha;
                        }
                        else if (text.Contains("pixels in lastupdate"))
                        {
                            // Set the PixelType
                            pixelCriteria.PixelType = PixelTypeEnum.LastUpdate;
                        }
                    }
                }
                
                // return value
                return pixelCriteria;
            }
            #endregion
            
            #region GetLinesAfterFirstLine(List<TextLine> sourceLines)
            /// <summary>
            /// This method returns a list of Lines After First Line
            /// </summary>
            public static List<TextLine> GetLinesAfterFirstLine(List<TextLine> sourceLines)
            {
                // initial value
                List<TextLine> lines = null;

                // local 
                int count = 0;

                // if there are two or more lines
                if (ListHelper.HasXOrMoreItems(sourceLines, 2))
                {
                    // create the return value
                    lines = new List<TextLine>();

                    // Iterate the collection of TextLine objects
                    foreach (TextLine line in sourceLines)
                    {
                        // Increment the value for count
                        count++;

                        // if not the first line
                        if (count > 1)
                        {
                            // add this line
                            lines.Add(line);
                        }
                    }
                }

                // return value
                return lines;
            }
            #endregion
            
            #region GetLinesAfterWhere(List<TextLine> lines)
            /// <summary>
            /// This method returns a list of Lines After Where
            /// </summary>
            public static List<TextLine> GetLinesAfterWhere(List<TextLine> lines)
            {
                // initial value
                List<TextLine> returnLines = new List<TextLine>();

                // local
                bool whereFound = false;

                // If the lines collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(lines))
                {
                    // Iterate the collection of TextLine objects
                    foreach (TextLine line in lines)
                    {
                        // if where was found
                        if (whereFound)
                        {
                             // Add this line
                            returnLines.Add(line);   
                        }
                        else
                        {
                            // if this line is the Where line, skip it for now, later I may get rid of that requirement
                            if (line.Text.ToLower().Contains("where"))
                            {
                                // where has been found
                                whereFound = true;
                            }                        
                        }
                    }
                }

                // return value
                return returnLines;
            }
            #endregion
            
            #region GetRepeatText(string text, RepeatTypeEnum repeatType)
            /// <summary>
            /// This method returns the text after Repeat Left, Repeat Right, etc. 
            /// </summary>
            public static string GetRepeatText(string text, RepeatTypeEnum repeatType)
            {
                // initial value
                string repeatText = "";

                // local
                string lineStart = "repeat ";

                // If the strings text and repeatType both exist
                if ((TextHelper.Exists(text)) && (repeatType != RepeatTypeEnum.NoRepeat))
                {
                    // set the lineStart text
                    lineStart += repeatType.ToString().ToLower();

                    // if the text was found
                    if (text.Contains(lineStart))
                    {
                        // get the repeatText
                        repeatText = text.Substring(lineStart.Length).Trim();
                    }
                }
                
                // return value
                return repeatText;
            }
            #endregion
            
            #region ParseActionType(string queryText, ref PixelQuery pixelQuery)
            /// <summary>
            /// This method returns the Action Type
            /// </summary>
            public static ActionTypeEnum ParseActionType(string queryText, ref PixelQuery pixelQuery)
            {
                // initial value
                ActionTypeEnum actionType = ActionTypeEnum.Unknown;

                // If the queryText string exists
                if (TextHelper.Exists(queryText))
                {  
                    // determine the action type
                    if (queryText.Contains("show"))
                    {
                        // set the actionType to ShowPixels
                        actionType = ActionTypeEnum.ShowPixels;
                    }
                    else if (queryText.Contains("hide from"))
                    {
                        // set the actionType to ShowPixels
                        actionType = ActionTypeEnum.HideFrom;
                    }
                    else if (queryText.Contains("hide"))
                    {
                        // set the actionType to HidePixels
                        actionType = ActionTypeEnum.HidePixels;
                    }
                    else if (queryText.Contains("draw line"))
                    {
                        // get the textLines
                        List<TextLine> textLines = TextHelper.GetTextLines(queryText);

                        // If the textLines collection exists and has one or more items
                        if (ListHelper.HasOneOrMoreItems(textLines))
                        {
                            // get the topLine
                            TextLine topLine = textLines[0];

                            // get the words
                            List<Word> words = TextHelper.GetWords(topLine.Text);

                            if (words.Count == 3)
                            {
                                // set the actionType to DrawLine
                                actionType = ActionTypeEnum.DrawTransparentLine;
                            }
                            if (words.Count == 4)
                            {
                                // Set the Color
                                pixelQuery.Color = Color.FromName(words[3].Text);

                                // set the actionType to DrawLine
                                actionType = ActionTypeEnum.DrawLine;
                            }
                            if (words.Count == 6)
                            {
                                // get the red green blue values
                                int red = NumericHelper.ParseInteger(words[3].Text, -1, -1);
                                int green = NumericHelper.ParseInteger(words[4].Text, -1, -1);
                                int blue = NumericHelper.ParseInteger(words[5].Text, -1, -1);

                                // if in range
                                if ((red > 0) && (red <= 255) && (green > 0) && (green <= 255) && (blue > 0) && (blue <= 255))
                                {
                                    // Set the color
                                    pixelQuery.Color = Color.FromArgb(red, green, blue);
                                }

                                // set the actionType to DrawLine
                                actionType = ActionTypeEnum.DrawLine;
                            }
                        }
                    }
                    else if (queryText.Contains("set backcolor"))
                    {
                        // set the actionType to DrawLine
                        actionType = ActionTypeEnum.SetBackColor;
                    }
                    else if (queryText.Contains("update"))
                    {
                        // Set to Update
                        actionType = ActionTypeEnum.Update;
                    }
                }
                                
                // return value
                return actionType;
            }
            #endregion
            
            #region ParseColor(string colorName)
            /// <summary>
            /// This method returns the Color
            /// </summary>
            public static Color ParseColor(string colorName)
            {
                // initial value
                Color color = Color.Empty;
                
                // If the colorName string exists
                if (TextHelper.Exists(colorName))
                {
                    try
                    {
                        // Set the color
                        color = Color.FromName(colorName);
                    }
                    catch (Exception error)
                    {
                        // for debugging only
                        DebugHelper.WriteDebugError("ParseColor", "PixelQueryParser", error);
                    }
                }

                // return value
                return color;
            }
            #endregion

            #region ParseColor(int red, int green, int blue)
            /// <summary>
            /// This method returns the Color
            /// </summary>
            public static Color ParseColor(int red, int green, int blue)
            {  
                // Ensure the values are in range
                red = NumericHelper.EnsureInRange(red, 0, 255);
                green = NumericHelper.EnsureInRange(green, 0, 255);
                blue = NumericHelper.EnsureInRange(blue, 0, 255);
                    
                // Set the color
                Color color = Color.FromArgb(red, green, blue);

                // return value
                return color;
            }
            #endregion
            
            #region ParseCriteria(List<TextLine> lines, ActionTypeEnum actionType)
            /// <summary>
            /// This method returns a list of Criteria
            /// </summary>
            public static List<PixelCriteria> ParseCriteria(List<TextLine> lines, ActionTypeEnum actionType)
            {
                // initial value
                List<PixelCriteria> criteriaList = null;

                // locals
                int count = 0;
                string text = "";
                PixelCriteria pixelCriteria = null;

                // If the lines collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(lines))
                {
                    // Create a new collection of 'PixelCriteria' objects.
                    criteriaList = new List<PixelCriteria>();

                    // parse the text lines
                    foreach (TextLine textLine in lines)
                    {
                        // Increment the value for count
                        count++;
                            
                        // Get the text
                        text = textLine.Text.ToLower();

                        // verify the line has text in case the user hit enter at the end of the line
                        if (TextHelper.Exists(text))
                        {  
                            // if this is DrawLine
                            if ((actionType == ActionTypeEnum.DrawTransparentLine) || (actionType == ActionTypeEnum.DrawLine))
                            {
                                // if this is the first line
                                if (count == 1)
                                {
                                    // Create the pixelCriteria based upon this text
                                    pixelCriteria = CreatePixelCriteria(text, actionType, count);

                                    // verify the pixelCriteria exists before adding
                                    if (pixelCriteria != null)
                                    {
                                        // Add this criteria
                                        criteriaList.Add(pixelCriteria);
                                    }
                                }
                                else if (count == 2)
                                {
                                    // Set the ComparisonText
                                    SetComparisonType(text, ref pixelCriteria);
                                }
                                else
                                {
                                    // Create the pixelCriteria based upon this text
                                    pixelCriteria = CreatePixelCriteria(text, actionType, count, pixelCriteria);
                                }
                            }
                            else
                            {  
                                // Create the pixelCriteria based upon this text
                                pixelCriteria = CreatePixelCriteria(text, actionType, count);

                                // verify the pixelCriteria exists before adding
                                if (pixelCriteria != null)
                                {
                                    // Set the ComparisonText
                                    SetComparisonType(text, ref pixelCriteria);

                                    // Add this criteria
                                    criteriaList.Add(pixelCriteria);
                                }
                            }
                        }
                    }

                    // Update 10.6.2021: If there are not any Alpha settings in the query,
                    // then the query gets updated to append alpha > 0
                    // if there is not any alpha criteria, we need to add one for Alpha > 0
                    if (!ListHelper.HasOneOrMoreItems(criteriaList))
                    {
                        // create default pixel criteria
                        pixelCriteria = new PixelCriteria();

                        // Set the Properties on the criteria
                        pixelCriteria.ComparisonType = ComparisonTypeEnum.GreaterThan;
                        pixelCriteria.PixelType = PixelTypeEnum.Alpha;
                        pixelCriteria.MinValue = 1;

                         // Create Default Criteria
                        criteriaList = new List<PixelCriteria>();
                        criteriaList.Add(pixelCriteria);
                    } 
                    else
                    {
                        // if alpha is set
                        bool hasAlpha = false;

                        // this is the new code 10.6.2021:
                        foreach (PixelCriteria criteria in criteriaList)
                        {
                            // if this query is a PixelType of Alpha
                            if (criteria.PixelType == PixelTypeEnum.Alpha)
                            {
                                // set to true
                                hasAlpha = true;

                                // break out of loop
                                break;
                            }
                        }

                        // if the value for hasAlpha is false
                        if (!hasAlpha)
                        {
                            // create default pixel criteria
                            pixelCriteria = new PixelCriteria();

                            // Set the Properties on the criteria
                            pixelCriteria.ComparisonType = ComparisonTypeEnum.GreaterThan;
                            pixelCriteria.PixelType = PixelTypeEnum.Alpha;
                            pixelCriteria.MinValue = 1;

                             // Add this criteria
                            criteriaList.Add(pixelCriteria);
                        }
                    }
                }
                
                // return value
                return criteriaList;
            }
            #endregion
            
            #region ParseDirections(string directionsText)
            /// <summary>
            /// This method parses the Directions given. 
            /// </summary>
            public static Directions ParseDirections(string directionsText)
            {
                // initial value
                Directions directions = null;

                // If the directionsText string exists
                if (TextHelper.Exists(directionsText))
                {
                    // Create a new instance of a 'Directions' object.
                    directions = new Directions();

                    // only use a space as a delimiter character
                    char[] delimiterChars = { ' ' };

                    // if the words exists
                    List<Word> words = TextHelper.GetWords(directionsText, delimiterChars);

                    // If the words collection exists and has one or more items
                    if (ListHelper.HasOneOrMoreItems(words))
                    {
                        // Iterate the collection of Word objects
                        foreach (Word word in words)
                        {
                            switch (word.Text.ToLower())
                            {
                                case "left":
                                    
                                    // update the directions object
                                    directions.Left = true;
                                
                                    // required
                                    break;

                                case "right":

                                    // set to Right
                                    directions.Right  = true;
                                
                                    // required
                                    break;

                                case "top":

                                    // set to Top
                                    directions.Top = true;
                                
                                    // required
                                    break;

                                case "bottom":

                                    // Set to Bottom
                                    directions.Bottom = true;
                                
                                    // required
                                    break;

                                case "all":

                                    // set all directions to true
                                    directions.Left = true;
                                    directions.Top = true;
                                    directions.Bottom = true;
                                    directions.Right = true;
                                
                                    // required
                                    break;
                            }
                        }
                    }
                }
                
                // return value
                return directions;
            }
            #endregion

            #region ParseGradientColor(List<Word> words)
            /// <summary>
            /// This method returns the Color. This is used by Gradient, but could
            /// be used by anything. The syntax expected is:
            /// Set Color1 Red Green Blue X Y
            /// Or
            /// Set Color2 Orange X Y
            /// </summary>
            public static PixelInformation ParseGradientColor(List<Word> words)
            {
                // initial value
                PixelInformation pixel = new PixelInformation();

                // locals
                int x = 0;
                int y = 0;
                Color color = Color.Empty;
                
                // If the words collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(words))
                {
                    // If there are exactly 7 words
                    if (words.Count == 7)
                    {
                        // set the value for Red, Green & Blue
                        int red = NumericHelper.ParseInteger(words[2].Text, -1, -1);
                        int green = NumericHelper.ParseInteger(words[3].Text, -1, -1);
                        int blue = NumericHelper.ParseInteger(words[4].Text, -1, -1);
                        x = NumericHelper.ParseInteger(words[5].Text, -1, -1);
                        y = NumericHelper.ParseInteger(words[6].Text, -1, -1);

                        // convert the integers to a List
                        List<int> numbers = new List<int>();
                        numbers.Add(red);
                        numbers.Add(green);
                        numbers.Add(blue);

                        // if all the values are in range
                        if ((NumericHelper.IsInRange(numbers, 0, 255)) && (x >= 0) && (y >= 0))
                        {
                            // Create the color
                            color = Color.FromArgb(red, green, blue);

                            // Create the PixelInformation object
                            pixel = new PixelInformation(x, y, color);
                        }
                    }
                    // if there are exactly 5 words
                    else if (words.Count == 5)
                    {
                        // set the x and y values
                        x = NumericHelper.ParseInteger(words[3].Text, -1, -1);
                        y = NumericHelper.ParseInteger(words[4].Text, -1, -1);

                        // Parse out the color text
                        color = ParseColor(words[2].Text);

                        // if valid
                        if  ((!color.IsEmpty) && (x >= 0) && (y >= 0))
                        {
                            // Create a new instance of a 'PixelInformation' object.
                            pixel = new PixelInformation(x, y, color);
                        }
                    }
                }

                // return value
                return pixel;
            }
            #endregion
            
            #region ParseNumbers(string text, ref int number1, ref int number2)
            /// <summary>
            /// This method Parse Numbers
            /// </summary>
            public static void ParseNumbers(string text, ref int number1, ref int number2)
            {
                // locals
                int count = 0;

                // only use a space as a delimiter character
                char[] delimiterChars = { ' '};

                // if the words exists
                List<Word> words = TextHelper.GetWords(text, delimiterChars);

                // If the words collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(words))
                {
                    // Iterate the collection of Word objects
                    foreach (Word word in words)
                    {
                        // if this word is a number
                        if (NumericHelper.IsNumeric(word.Text))
                        {
                            // Increment the value for count
                            count++;  

                            // if this is the firstNumber
                            if (count == 1)
                            {
                                // set number1
                                number1 = NumericHelper.ParseInteger(word.Text, -1000, -1001);
                            }
                            else if (count == 2)
                            {
                                // set number2
                                number2 = NumericHelper.ParseInteger(word.Text, -1000, -1001);

                                // we are done
                                break;
                            }
                        }
                    }
                }
            }
            #endregion

            #region ParseNumbers(string text, ref int number1, ref int number2, ref int number3, ref int number4)
            /// <summary>
            /// This method Parse Numbers
            /// </summary>
            public static void ParseNumbers(string text, ref int number1, ref int number2, ref int number3, ref int number4)
            {
                // locals
                int count = 0;

                // only use a space as a delimiter character
                char[] delimiterChars = { ' ' };

                // if the words exists
                List<Word> words = TextHelper.GetWords(text, delimiterChars);

                // If the words collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(words))
                {
                    // Iterate the collection of Word objects
                    foreach (Word word in words)
                    {
                        // if this word is a number
                        if (NumericHelper.IsNumeric(word.Text))
                        {
                            // Increment the value for count
                            count++;  

                            // if this is the firstNumber
                            if (count == 1)
                            {
                                // set number1
                                number1 = NumericHelper.ParseInteger(word.Text, -1000, -1001);
                            }
                            else if (count == 2)
                            {
                                // set number2
                                number2 = NumericHelper.ParseInteger(word.Text, -1000, -1001);
                            }
                            else if (count == 3)
                            {
                                // set number3
                                number3 = NumericHelper.ParseInteger(word.Text, -1000, -1001);
                            }
                            else if (count == 4)
                            {
                                // set number4
                                number4 = NumericHelper.ParseInteger(word.Text, -1000, -1001);
                            }                            
                        }
                    }
                }
            }
            #endregion
            
            #region ParsePixelQuery(string queryText)
            /// <summary>
            /// This class is used to parse querries from the queryText given
            /// </summary>
            public static PixelQuery ParsePixelQuery(string queryText)
            {
                // initial value
                PixelQuery pixelQuery = new PixelQuery();

                // store the text
                pixelQuery.QueryText = queryText;

                // locals
                string criteriaText = String.Empty;
                string updateText = String.Empty;

                // If the queryText string exists
                if (TextHelper.Exists(queryText))
                {
                    // if the NewLine is not found
                    if (!queryText.Contains(Environment.NewLine))
                    {
                        // The parsing on lines isn't working, this is a good hack till
                        // I rewrite the parser to be more robust someday
                        queryText = queryText.Replace("\n", Environment.NewLine);
                    }

                    // just in case, fix for the hack
                    queryText = queryText.Replace("\r\r", "\r");

                    // get the lowercase version of the text
                    queryText = queryText.ToLower().Trim();

                    // Get the text lines
                    List<TextLine> lines = TextHelper.GetTextLines(queryText);

                    // parse the ActionType (Show Pixels, Hide Pixels, Draw Line, Update)
                    pixelQuery.ActionType = ParseActionType(queryText, ref pixelQuery);

                    // if we are doing an update query, we have to modify this a little
                    if (pixelQuery.ActionType == ActionTypeEnum.Update)
                    {
                        // for an update query, the second line sets the color values to update to
                        // and the third line starts the where clause.

                        // New feature 7.19.2020: 
                        // A Scatter query is created like this:
                        // Update
                        // Set Color Red
                        // Scatter 30
                        // Where
                        // Rest of where criteria goes here

                        // Set the updateText
                        updateText = SetUpdateText(lines);

                        // The way scatter works is the lines are tested to contains a Scatter line before the Where clause.
                        // If a Scatter line exists and has a valid double value between 0.0001 and 99.9999, scatter returns true
                        //  and the line for the scatter is removed, so the rest of the code funcations as it is.
                        ScatterSettings settings = CheckForScatter(lines);

                        // If the value for the property settings.Scatter is true
                        if (settings.Scatter)
                        {
                            // Set the ScatterPercent
                            pixelQuery.Scatter = true;
                            pixelQuery.ScatterPercent = settings.ScatterPercent;
                            pixelQuery.ScatterModX = settings.ModX;
                            pixelQuery.ScatterModY = settings.ModY;
                            lines = settings.TextLines;

                            // Create the RandomShuffler
                            pixelQuery.Shuffler = new RandomShuffler.LargeNumberShuffler(4, 1, 9999, RandomShuffler.Enumerations.NumberOutOfRangeOptionEnum.ReturnModulus);
                        }
                        
                        // Check to see if a Normalize query is present
                        NormalizeSettings normalizeSettings = CheckForNormalize(lines);

                        // If the value for the property normalizeSettings.Normalize is true
                        if (normalizeSettings.Normalize)
                        {
                            pixelQuery.Normalize = true;
                            pixelQuery.Min = normalizeSettings.Min;
                            pixelQuery.Max = normalizeSettings.Max;
                            pixelQuery.Step = normalizeSettings.Step;
                            pixelQuery.NormalizeColor = normalizeSettings.Color;
                            lines = normalizeSettings.Lines;
                        }

                        // Check if the SplitImage is set, and if yes take the properties for Direction and SplitX
                        SplitImageSettings splitImageSettings = CheckForSplitImage(lines);

                        // If the splitImageSettings object exists
                        if (NullHelper.Exists(splitImageSettings))
                        {
                            // Set the settings
                            pixelQuery.SplitImageSettings = splitImageSettings;
                        }

                        // Check for Gradient
                        pixelQuery.Gradient = CheckForGraident(lines);

                        // get the formula
                        GrayScaleFormulaEnum grayScaleFormula = GrayScaleFormulaEnum.TakeAverage;

                        // Check for ApplyGrayScale
                        pixelQuery.ApplyGrayscale = CheckForApplyGrayScale(lines, ref grayScaleFormula);

                        // if ApplyGrayScale is true
                        if (pixelQuery.ApplyGrayscale)
                        {
                            // Set the GrayScaleFormula
                            pixelQuery.GrayScaleFormula = grayScaleFormula;
                        }

                        // Get the lines after the where clause
                        lines = GetLinesAfterWhere(lines);

                        // If the lines collection exists and has one or more items
                        if (ListHelper.HasOneOrMoreItems(lines))
                        {
                            // Parse the Criteria (this is the matching part)
                            pixelQuery.Criteria = ParseCriteria(lines, pixelQuery.ActionType);
                        }

                        // If the updateText string exists
                        if (TextHelper.Exists(updateText))
                        {
                            // Set the update values
                            SetUpdateParameters(updateText, ref pixelQuery);
                        }

                    }
                    else if (pixelQuery.ActionType == ActionTypeEnum.HideFrom)
                    {
                        // hide from (Left, Right, Up, Down, All)
                        // Get the first line
                        if (ListHelper.HasOneOrMoreItems(lines))
                        {
                            // Get the text
                            string hideFromText = lines[0].Text;

                            // set the directionsText
                            string directionsText = hideFromText.Substring(10).Trim();

                            // Parse the directions 
                            pixelQuery.Directions = ParseDirections(directionsText);
                        }
                    }
                    else if ((pixelQuery.ActionType == ActionTypeEnum.HidePixels) || (pixelQuery.ActionType == ActionTypeEnum.ShowPixels))
                    {
                        // if there are two or more lines
                        if (ListHelper.HasXOrMoreItems(lines, 2))
                        {
                            // This is a Hide or Show Query
                            // and the second line starts the where clause.
                            lines = GetLinesAfterFirstLine(lines);

                            // Parse the Criteria (this is the matching part)
                            pixelQuery.Criteria = ParseCriteria(lines, pixelQuery.ActionType);
                        }
                    }
                }

                // return value
                return pixelQuery;
            }
            #endregion
            
            #region SetColorToAdjust()
            /// <summary>
            /// This method returns the Color To Adjust
            /// </summary>
            public static RGBColor SetColorToAdjust(string colorWord)
            {
                // initial value
                RGBColor colorToAdjust = RGBColor.NotSet;
                
                // If the colorWord string exists
                if (TextHelper.Exists(colorWord))
                {
                    // determine the action by the text of the word                    
                    switch(colorWord.ToLower())
                    {
                        case "red":

                            // set the value
                            colorToAdjust = RGBColor.Red;

                            // required
                            break;

                        case "green":

                            // set the value
                            colorToAdjust = RGBColor.Green;

                            // required
                            break;

                        case "blue":

                            // set the value
                            colorToAdjust = RGBColor.Blue;

                            // required
                            break;

                        case "greenred":

                            // set the value
                            colorToAdjust = RGBColor.GreenRed;

                            // required
                            break;

                        case "bluered":

                            // set the value
                            colorToAdjust = RGBColor.BlueRed;

                            // required
                            break;

                         case "bluegreen":

                            // set the value
                            colorToAdjust = RGBColor.BlueGreen;

                            // required
                            break;

                        case "all":

                            // set the value
                            colorToAdjust = RGBColor.All;

                            // required
                            break;
                    }
                }

                // return value
                return colorToAdjust;
            }
            #endregion
            
            #region SetComparisonType(string text, ref PixelCriteria pixelCriteria)
            /// <summary>
            /// This method returns the Comparison Type
            /// </summary>
            public static ComparisonTypeEnum SetComparisonType(string text, ref PixelCriteria pixelCriteria)
            {
                // initial value
                ComparisonTypeEnum comparisonType = ComparisonTypeEnum.Unknown;

                // locals
                int number1 = -1000;
                int number2 = -1000;
                int number3 = -1000;
                int number4 = -1000;
                
                // if the text exists
                if ((TextHelper.Exists(text)) && (NullHelper.Exists(pixelCriteria)))
                {
                    // drawing is different for DrawLine
                    if (pixelCriteria.PixelType == PixelTypeEnum.DrawLine)
                    {
                        // Parse out the numbers from the text
                        ParseNumbers(text, ref number1, ref number2, ref number3, ref number4);

                        // Set the Start and End Point
                        pixelCriteria.StartPoint = new Point(number1, number2);
                        pixelCriteria.EndPoint = new Point(number3, number4);
                    }
                    else
                    {
                        // Parse out the numbers from the text
                        ParseNumbers(text, ref number1, ref number2);

                        // if the text contains less than
                        if (text.Contains("<"))
                        {
                            // Set to Less Than
                            comparisonType = ComparisonTypeEnum.LessThan;

                            // Set the MaxValue
                            pixelCriteria.MaxValue = number1;
                        }
                        // if the text contains between
                        else if (text.Contains("between"))
                        {
                            // Set to Between
                            comparisonType = ComparisonTypeEnum.Between;

                            // Set the MinValue
                            pixelCriteria.MinValue = number1;

                            // Set the MaxValue
                            pixelCriteria.MaxValue = number2;
                        }
                        // if the text contains greater than
                        else if (text.Contains(">"))
                        {
                            // Set to Greater Than
                            comparisonType = ComparisonTypeEnum.GreaterThan;

                            // Set the MinValue
                            pixelCriteria.MinValue = number1;
                        }
                        else if (text.Contains("="))
                        {
                            // Set to Equals
                            comparisonType = ComparisonTypeEnum.Equals;

                            // Set the TargetValue
                            pixelCriteria.TargetValue = number1;
                        }
                        else if (text.Contains("pixels in lastupdate"))
                        {
                            // Set the ComparisonType
                            comparisonType = ComparisonTypeEnum.In;
                        }
                    }

                    // set the comparisonType on the object passed in
                    pixelCriteria.ComparisonType = comparisonType;
                }
                
                // return value
                return comparisonType;
            }
            #endregion

            #region SetMaskAction(string verb)
            /// <summary>
            /// This method returns the Mask Action
            /// </summary>
            public static MaskActionEnum SetMaskAction(string verb)
            {
                // initial value
                MaskActionEnum maskAction = MaskActionEnum.NoAction;

                switch (verb)
                {
                    case "clear":

                        // Clear 1 mask
                        maskAction = MaskActionEnum.Clear;

                        // required
                        break;

                    case "clearall":

                        // Clear 1 mask
                        maskAction = MaskActionEnum.ClearAll;

                        // required
                        break;

                    case "add":

                        // Clear 1 mask
                        maskAction = MaskActionEnum.Add;

                        // required
                        break;

                    case "replace":

                        // Clear 1 mask
                        maskAction = MaskActionEnum.Replace;

                        // required
                        break;
                }
                
                // return value
                return maskAction;
            }
            #endregion
            
            #region SetRepeatType(string text)
            /// <summary>
            /// This method returns the Repeat Type
            /// </summary>
            public static RepeatTypeEnum SetRepeatType(string text)
            {
                // initial value
                RepeatTypeEnum repeatType = RepeatTypeEnum.NoRepeat;

                // determine the repeat type by the text
                if (text.Contains("repeat left"))
                {
                    // Set the RepeatType
                    repeatType = RepeatTypeEnum.Left;
                }
                else if (text.Contains("repeat up"))
                {
                    // Set the RepeatType
                    repeatType = RepeatTypeEnum.Up;
                }
                else if (text.Contains("repeat right"))
                {
                    // Set the RepeatType
                    repeatType = RepeatTypeEnum.Right;
                }
                else if (text.Contains("repeat down"))
                {
                    // Set the RepeatType
                    repeatType = RepeatTypeEnum.Down;
                }

                // return value
                return repeatType;
            }
            #endregion
            
            #region SetSwapType(string sourceColorWord, string targetColorWord)
            /// <summary>
            /// This method returns the Swap Type
            /// </summary>
            public static SwapTypeEnum SetSwapType(string sourceColorWord, string targetColorWord)
            {
                // initial value
                SwapTypeEnum swapType = SwapTypeEnum.Unknown;

                // If the strings sourceColorWord and targetColorWord both exist
                if (TextHelper.Exists(sourceColorWord, targetColorWord))
                {
                    // if thel first word is red
                    if (TextHelper.IsEqual(sourceColorWord, "red"))
                    {
                        // if the second word is green
                        if (TextHelper.IsEqual(targetColorWord, "green"))
                        {
                            // Swap red and green
                            swapType = SwapTypeEnum.RedToGreen;
                        }
                        // if the second word is green
                        else if (TextHelper.IsEqual(targetColorWord, "blue"))
                        {
                            // Swap red and blue
                            swapType = SwapTypeEnum.RedToBlue;
                        }
                    }
                    // if the first word is green
                    else if (TextHelper.IsEqual(sourceColorWord, "green"))
                    {
                        // if the second word is red
                        if (TextHelper.IsEqual(targetColorWord, "red"))
                        {
                            // Swap green and red
                            swapType = SwapTypeEnum.RedToGreen;
                        }
                        // if the second word is blue
                        else if (TextHelper.IsEqual(targetColorWord, "blue"))
                        {
                            // Swap green and blue
                            swapType = SwapTypeEnum.BlueToGreen;
                        }
                    }
                    // if the first word is blue
                    else if (TextHelper.IsEqual(sourceColorWord, "blue"))
                    {
                        // if the second word is red
                        if (TextHelper.IsEqual(targetColorWord, "red"))
                        {
                            // Swap blue and red
                            swapType = SwapTypeEnum.RedToBlue;
                        }
                        // if the second word is blue
                        else if (TextHelper.IsEqual(targetColorWord, "green"))
                        {
                            // Swap blue and green
                            swapType = SwapTypeEnum.BlueToGreen;
                        }
                    }
                }
                
                // return value
                return swapType;
            }
            #endregion
            
            #region SetUpdateParameters(string updateText, ref PixelQuery pixelQuery)
            /// <summary>
            /// This method Set Update Parameters
            /// </summary>
            public static void SetUpdateParameters(string updateText, ref PixelQuery pixelQuery)
            {
                // if the updateText exists and the pixelQuery object exists                
                if ((TextHelper.Exists(updateText)) && (NullHelper.Exists(pixelQuery)))
                {
                    // if the updateText starts with the word set (else this is not a proper BQL Update query).
                    if (updateText.StartsWith("set"))    
                    {
                        // only use a space as a delimiter character
                        char[] delimiterChars = { ' ' };

                        // Get a list of words from this text
                        List<Word> words = TextHelper.GetWords(updateText, delimiterChars);

                        // If the words collection exists and has one or more items
                        if (ListHelper.HasOneOrMoreItems(words))
                        {
                            // if there are six words. 
                            // Example: Set Color 100 150 200 40 - (Red = 100, Green = 150, Blue = 200, Alpha = 40)
                            if (words.Count == 6)
                            {  
                                // Set to red
                                pixelQuery.Red = NumericHelper.ParseInteger(words[2].Text, -1, -2);
                                pixelQuery.Green = NumericHelper.ParseInteger(words[3].Text, -1, -2);
                                pixelQuery.Blue = NumericHelper.ParseInteger(words[4].Text, -1, -2);
                                pixelQuery.Alpha = NumericHelper.ParseInteger(words[5].Text, -1, -2);

                                // verify everything is valid
                                if ((pixelQuery.Red >= 0) && (pixelQuery.Green >= 0) && (pixelQuery.Red >= 0) && (pixelQuery.Alpha >= 0))
                                {
                                    // SetColor is true
                                    pixelQuery.SetColor = true;

                                    // Set the Color
                                    pixelQuery.Color = Color.FromArgb(pixelQuery.Alpha, pixelQuery.Red, pixelQuery.Green, pixelQuery.Blue);
                                }
                            }
                            // if there are 5 words
                            // Example: Set Color 100 150 200 - (Red = 100, Green = 150, Blue = 200, Alpha = 255 default value)
                            else if (words.Count == 5)
                            {
                                // Set to red
                                pixelQuery.Red = NumericHelper.ParseInteger(words[2].Text, -1, -2);
                                pixelQuery.Green = NumericHelper.ParseInteger(words[3].Text, -1, -2);
                                pixelQuery.Blue = NumericHelper.ParseInteger(words[4].Text, -1, -2);
                                pixelQuery.Alpha = 255;

                                // verify everything is valid
                                if ((pixelQuery.Red >= 0) && (pixelQuery.Green >= 0) && (pixelQuery.Red >= 0) && (pixelQuery.Alpha >= 0))
                                {
                                    // SetColor is true
                                    pixelQuery.SetColor = true;

                                    // Set the Color
                                    pixelQuery.Color = Color.FromArgb(pixelQuery.Alpha, pixelQuery.Red, pixelQuery.Green, pixelQuery.Blue);
                                }
                            }
                            // If there are 4 words
                            // Example: Set Adjust Red 25 (every pixel gets 25 more red)
                            else if (words.Count == 4)
                            {
                                // Adjustment, Swap, Or Mask
                                string secondWord = words[1].Text;

                                // if the second word is adjust
                                if (TextHelper.IsEqual(secondWord, "adjust"))
                                {
                                    // AdjustColor is true
                                    pixelQuery.AdjustColor = true;

                                    // get the text of the third word
                                    string colorWord = words[2].Text;

                                    // set the Color to Adjust
                                    pixelQuery.ColorToAdjust = SetColorToAdjust(colorWord);

                                    // Attempt to set the 4th word to the AssignToColor
                                    pixelQuery.AssignToColor = AssignColor(words[3].Text);

                                    // if there is not an AssignToColor
                                    if (!pixelQuery.HasAssignToColor)
                                    {
                                        // Set the Adjustment value only if we do not have an AssignToColor
                                        pixelQuery.Adjustment = NumericHelper.ParseInteger(words[3].Text, -1000, -1001);
                                    }
                                }
                                // if the second word is swap
                                else if (TextHelper.IsEqual(secondWord, "swap"))
                                {
                                    // get the thrid and fourth word
                                    string sourceColorWord = words[2].Text;
                                    string targetColorWord = words[3].Text;

                                    // We are swapping colors
                                    pixelQuery.SwapColors = true;

                                    // Set the SwapType
                                    pixelQuery.SwapType = SetSwapType(sourceColorWord, targetColorWord);
                                }
                                 // if the second word is mask
                                else if (TextHelper.IsEqual(secondWord, "mask"))
                                {
                                    // get the third and fourth word
                                    string verb = words[2].Text;
                                    string name = words[3].Text;

                                    // We are swapping colors
                                    pixelQuery.Mask = new Mask(name);
                                    
                                    // Set the Mask.Action property
                                    pixelQuery.Mask.Action = SetMaskAction(verb);
                                }
                            }
                            // if there are 3 words
                            // Example: Set Color Orchid (the color must be a named color)
                            else if (words.Count == 3)
                            {
                                // SetColor is true
                                pixelQuery.SetColor = true;

                                // Set the Color
                                pixelQuery.Color = Color.FromName(words[2].Text);
                                pixelQuery.Alpha = 255;
                            }
                        }
                    }
                }
            }
            #endregion
            
            #region SetUpdateText(List<TextLine> lines)
            /// <summary>
            /// This method returns the Update Text
            /// </summary>
            public static string SetUpdateText(List<TextLine> lines)
            {
                // initial value
                string updateText = "";

                // if there are one or more lines
                if (ListHelper.HasOneOrMoreItems(lines))
                {
                    // Iterate the collection of TextLine objects
                    foreach (TextLine line in lines)
                    {
                        // if this is the Set line
                        if (line.Text.ToLower().StartsWith("set "))
                        {
                            // set the return value
                            updateText = line.Text;

                            // break out of the loop
                            break;
                        }
                    }
                }
                
                // return value
                return updateText;
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
