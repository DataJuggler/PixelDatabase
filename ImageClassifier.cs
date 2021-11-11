

#region using statements

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataJuggler.UltimateHelper;
using DataJuggler.PixelDatabase.Enumerations;
using System.Runtime.Versioning;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class ImageClassifier
    /// <summary>
    /// This class is used to classify pixeldatabases by colors.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class ImageClassifier
    {  
       
        #region Methods
            
            #region Classify(PixelDatabase pixelDatabase, int fastSortFactor = 0)
            /// <summary>
            /// Classify the current image that was passed in
            /// </summary>
            /// <param name="pixelDatabase"></param>
            /// <returns></returns>
            public static ImageClassification Classify(PixelDatabase pixelDatabase, int fastSortFactor = 0)
            {
                // Create a new instance of an 'ImageClassification' object.
                ImageClassification classification = new ImageClassification();

                // locals
                long totalRed = 0;
                long totalGreen = 0;
                long totalBlue = 0;
                PixelInformation pixel = null;
                int totalPixels = 0;
                int pixelNumber = 0;
                int pixelsInspected = 0;
                bool inspectPixel = true;

                // If the pixelDatabase object exists
                if (NullHelper.Exists(pixelDatabase))
                {
                    // Set the totalPixels
                    totalPixels = pixelDatabase.Width * pixelDatabase.Height;

                    // iterate each pixel
                    for (int x = 0; x < pixelDatabase.Width; x++)
                    {
                        for (int y = 0; y < pixelDatabase.Height; y++)
                        {
                            // Increment the value for pixelNumber
                            pixelNumber++;

                            // If the value for fastSortFactor is greater than zero
                            if (fastSortFactor > 0)
                            {
                                // inspect this pixel every time we get a zero for a remainder.
                                // Higher fastSpeedFactor is faster.
                                inspectPixel = ((pixelNumber % fastSortFactor) == 0);
                            }

                            // if the value for inspectPixel is true
                            if (inspectPixel)
                            {
                                // Get the current pixel
                                pixel = pixelDatabase.GetPixel(x, y);

                                // increment the values
                                totalRed += pixel.Red;
                                totalGreen += pixel.Green;
                                totalBlue += pixel.Blue;

                                // Increment the value for pixelsInspected
                                pixelsInspected++;
                            }
                        }
                    }

                    // if the totalRed is set
                    if (totalRed > 0)
                    {
                        // set the averageRed
                        classification.AverageRed = totalRed / pixelsInspected;
                    }
                    else
                    {
                        // None
                        classification.AverageRed = 0;
                    }

                    // if the totalGreen is set
                    if (totalGreen > 0)
                    {
                        // set the averageGreen
                        classification.AverageGreen = totalGreen / pixelsInspected;
                    }
                    else
                    {
                        // None
                        classification.AverageGreen = 0;
                    }

                    // if the totalBlue is set
                    if (totalBlue > 0)
                    {
                        // set the averageBlue
                        classification.AverageBlue = totalBlue / pixelsInspected;
                    }
                    else
                    {
                        // None
                        classification.AverageBlue = 0;
                    }

                    // if Red is highest
                    if ((totalRed > totalBlue) && (totalRed > totalGreen))
                    {
                        // Set to Red
                        classification.PrimaryColor = Enumerations.PrimaryColorEnum.Red;

                        // if Green is next highest
                        if (totalGreen > totalBlue)
                        {
                            // Set to Green
                            classification.SecondaryColor = PrimaryColorEnum.Green;
                            classification.MinorityColor = PrimaryColorEnum.Blue;
                        }
                        else if (totalBlue > totalGreen)
                        {
                            // Set to Blue
                            classification.SecondaryColor = PrimaryColorEnum.Blue;
                            classification.MinorityColor = PrimaryColorEnum.Green;
                        }
                    }

                    // if Green is highest
                    if ((totalGreen > totalRed) && (totalGreen > totalBlue))
                    {
                        // Set to Green
                        classification.PrimaryColor = Enumerations.PrimaryColorEnum.Green;

                        // if Red is next highest
                        if (totalRed > totalBlue)
                        {
                            // Set to Red
                            classification.SecondaryColor = PrimaryColorEnum.Red;
                            classification.MinorityColor = PrimaryColorEnum.Blue;
                        }
                        else if (totalBlue > totalRed)
                        {
                            // Set to Blue
                            classification.SecondaryColor = PrimaryColorEnum.Blue;
                            classification.MinorityColor = PrimaryColorEnum.Red;
                        }
                    }

                    // if Blue is highest
                    if ((totalBlue > totalRed) && (totalBlue > totalGreen))
                    {
                        // Set to Green
                        classification.PrimaryColor = Enumerations.PrimaryColorEnum.Blue;

                        // if totalRed is next highest
                        if (totalRed > totalGreen)
                        {
                            // Set to Blue
                            classification.SecondaryColor = PrimaryColorEnum.Red;
                            classification.MinorityColor = PrimaryColorEnum.Green;
                        }
                        else if (totalGreen > totalRed)
                        {
                            // Set to Blue
                            classification.SecondaryColor = PrimaryColorEnum.Green;
                            classification.MinorityColor = PrimaryColorEnum.Red;
                        }
                    }

                    // Just a call to set the colors in case of ties from above
                    ClassifyColors(classification);
                }

                // return value
                return classification;
            }
            #endregion

            #region ClassifyColors(ImageClassification imageClassification)
            /// <summary>
            /// This method returns the Colors
            /// </summary>
            public static void ClassifyColors(ImageClassification image)
            {
                // If the image object exists
                if (NullHelper.Exists(image))
                {
                      // if Red is Primary
                    if (image.PrimaryColor == PrimaryColorEnum.Red)
                    { 
                        // if Green is the next color
                        if (image.SecondaryColor == PrimaryColorEnum.NotSet)
                        {
                            // no second color, so go alphabetical (usually from a tie)
                            image.SecondaryColor = PrimaryColorEnum.Blue;
                            image.MinorityColor = PrimaryColorEnum.Green;
                        }
                    }
                    else if (image.PrimaryColor == PrimaryColorEnum.Blue)
                    {
                        // if Green is the next color
                        if (image.SecondaryColor == PrimaryColorEnum.NotSet)
                        {
                            // no second color, so go alphabetical (usually from a tie)
                            image.SecondaryColor = PrimaryColorEnum.Green;
                            image.MinorityColor = PrimaryColorEnum.Red;
                        }
                    }
                    else if (image.PrimaryColor == PrimaryColorEnum.Green)
                    {
                         // if Green is the next color
                        if (image.SecondaryColor == PrimaryColorEnum.NotSet)
                        {
                            // no second color, so go alphabetical (usually from a tie)
                            image.SecondaryColor = PrimaryColorEnum.Blue;
                            image.MinorityColor = PrimaryColorEnum.Red;
                        }
                    }
                    else
                    {
                        // we have to check for ties here for certain cases that are not alphabetical

                        // here, testing if blue and red exactly tie, and green is less than average blue (not a 3 way tie)
                        if ((image.AverageBlue == image.AverageRed) && (image.AverageGreen < image.AverageBlue))
                        {
                            // no Primary Color, so go alphabetical (usually from a tie)
                            image.PrimaryColor = PrimaryColorEnum.Blue;
                            image.MinorityColor = PrimaryColorEnum.Red;
                            image.SecondaryColor = PrimaryColorEnum.Green;
                        }
                        // here, testing if green and red exactly tie, and blue is less than Green (not a 3 way tie)
                        else if ((image.AverageGreen == image.AverageRed) && (image.AverageBlue < image.AverageGreen))
                        {
                            // no Primary Color, so go alphabetical (usually from a tie)
                            image.PrimaryColor = PrimaryColorEnum.Green;
                            image.MinorityColor = PrimaryColorEnum.Red;
                            image.SecondaryColor = PrimaryColorEnum.Blue;
                        }
                        else if ((image.AverageBlue == image.AverageGreen) && (image.AverageRed < image.AverageBlue))
                        {
                            // no Primary Color, so go alphabetical (usually from a tie)
                            image.PrimaryColor = PrimaryColorEnum.Blue;
                            image.MinorityColor = PrimaryColorEnum.Green;
                            image.SecondaryColor = PrimaryColorEnum.Red;
                        }
                        else
                        {
                            // Alphabetical if nothing else to go by
                            image.PrimaryColor = PrimaryColorEnum.Blue;
                            image.SecondaryColor = PrimaryColorEnum.Green;
                            image.MinorityColor = PrimaryColorEnum.Red;
                        }
                    }
                }
            }
            #endregion
            
            #region ConvertChar(char c)
            /// <summary>
            /// This method returns the Char
            /// </summary>
            private static string ConvertChar(char c)
            {
                // initial value
                string convertedChar = "";

                switch (c)
                {
                    case '0':

                        // set the return value
                        convertedChar = "a";

                        // required
                        break;

                     case '1':

                        convertedChar = "b";

                        // required
                        break;

                     case '2':

                        convertedChar = "c";

                        // required
                        break;

                    case '3':

                        // set the return value
                        convertedChar = "d";

                        // required
                        break;

                     case '4':

                        convertedChar = "e";

                        // required
                        break;

                     case '5':

                        convertedChar = "f";

                        // required
                        break;

                    case '6':

                        // set the return value
                        convertedChar = "g";

                        // required
                        break;

                     case '7':

                        convertedChar = "h";

                        // required
                        break;

                     case '8':

                        convertedChar = "i";

                        // required
                        break;

                     case '9':

                        convertedChar = "j";

                        // required
                        break;
                }
                
                // return value
                return convertedChar;
            }
            #endregion
            
            #region GetColorName(PrimaryColorEnum color)
            /// <summary>
            /// This method returns the Color Name, if none is found Blue is returned for alphabetical reasons
            /// </summary>
            public static string GetColorName(PrimaryColorEnum color)
            {
                // initial value
                string colorName = "";

                // Set the colorName
                if (color == PrimaryColorEnum.Red)
                {
                    // set to red
                    colorName = "red";
                }
                else if (color == PrimaryColorEnum.Green)
                {
                    // set to red
                    colorName = "green";
                }
                else 
                {
                    // set to blue
                    colorName = "blue";
                }
                
                // return value
                return colorName;
            }
            #endregion
            
            #region GetFiles(string sourceDirectory, List<string> extensions
            /// <summary>
            /// This method returns a list of Files
            /// </summary>
            public static List<string> GetFiles(string sourceDirectory, List<string> extensions)
            {
                // initial value
                List<string> files = new List<string>();

                // locals
                List<string> tempFiles = null;

                // if there are extensions listed
                if (ListHelper.HasOneOrMoreItems(extensions))
                {
                    // Iterate the collection of string objects
                    foreach (string extension in extensions)
                    {
                        // Get the tempFiles
                        tempFiles = FileHelper.GetFiles(sourceDirectory, extension);

                        // If the tempFiles collection exists and has one or more items
                        if (ListHelper.HasOneOrMoreItems(tempFiles))
                        {
                            // append each file
                            foreach (string file in tempFiles)
                            {
                                // Add this file
                                files.Add(file);
                            }
                        }
                    }
                }
                
                // return value
                return files;
            }
            #endregion

            #region GetMinorityValue(ImageClassification image)
            /// <summary>
            /// This method returns the Minority Value, which is the average of the MinorityColor
            /// </summary>
            private static int GetMinorityValue(ImageClassification image)
            {
                // initial value
                int minorityValue = 0;
                
                // If the image object exists
                if (NullHelper.Exists(image))
                {
                    // if Red is Minority
                    if (image.MinorityColor == PrimaryColorEnum.Red)
                    {
                        // set the return value
                        minorityValue = (int) image.AverageRed;    
                    }
                    // if Green is Minority
                    else if (image.MinorityColor == PrimaryColorEnum.Green)
                    {
                        // set the return value
                        minorityValue = (int) image.AverageGreen; 
                    }
                    // else
                    else
                    {
                        // Use Blue
                        minorityValue = (int) image.AverageBlue;
                    }
                }
        
                // return value
                return minorityValue;
            }
            #endregion
            
            #region GetPrimaryValue(ImageClassification image)
            /// <summary>
            /// This method returns the Primary Value, which is the average of the PrimaryColor
            /// </summary>
            private static int GetPrimaryValue(ImageClassification image)
            {
                // initial value
                int primaryValue = 0;
                
                // If the image object exists
                if (NullHelper.Exists(image))
                {
                    // if Red is Primary
                    if (image.PrimaryColor == PrimaryColorEnum.Red)
                    {
                        // set the return value
                        primaryValue = (int) image.AverageRed;    
                    }
                    // if Green is Primary
                    else if (image.PrimaryColor == PrimaryColorEnum.Green)
                    {
                        // set the return value
                        primaryValue = (int) image.AverageGreen; 
                    }
                    // else
                    else
                    {
                        // Use Blue
                        primaryValue = (int) image.AverageBlue;
                    }}
        
                // return value
                return primaryValue;
            }
            #endregion

            #region GetSecondaryValue(ImageClassification image)
            /// <summary>
            /// This method returns the Secondary Value, which is the average of the SecondaryColor
            /// </summary>
            private static int GetSecondaryValue(ImageClassification image)
            {
                // initial value
                int secondaryValue = 0;
                
                // If the image object exists
                if (NullHelper.Exists(image))
                {
                    // if Red is Secondary
                    if (image.SecondaryColor == PrimaryColorEnum.Red)
                    {
                        // set the return value
                        secondaryValue = (int) image.AverageRed;    
                    }
                    // if Green is Secondary
                    else if (image.SecondaryColor == PrimaryColorEnum.Green)
                    {
                        // set the return value
                        secondaryValue = (int) image.AverageGreen; 
                    }
                    // else
                    else
                    {
                        // Use Blue
                        secondaryValue = (int) image.AverageBlue;
                    }
                }
        
                // return value
                return secondaryValue;
            }
            #endregion
            
            #region GetValueAsText(int value)
            /// <summary>
            /// This method returns the Value As Text
            /// </summary>
            public static string GetValueAsText(int value)
            {
                // initial value
                string valueAsText = "";

                // local
                string temp = "";

                // 
                if (value > 99)
                {
                    // get a 3 digit number
                    temp = value.ToString();
                }
                else if (value > 9)
                {
                    // add a to a two digit number
                    temp = "0" + value.ToString();
                }
                else
                {
                    // add aa to a two digit number
                    temp = "00" + value.ToString();
                }

                // Create a new instance of a 'StringBuilder' object.
                StringBuilder sb = new StringBuilder();

                // now convert each number to a character
                for (int x = 0; x < 3; x++)
                {
                    // Get the character at this array
                    char c = temp[x];

                    // now convert the numeric character to a letter
                    string character = ConvertChar(c);

                    // add this character
                    sb.Append(character);
                }

                // Get the value as Text
                valueAsText = sb.ToString();
                
                // return value
                return valueAsText;
            }
            #endregion
            
            #region SetNewName(ImageClassification image, string outputDirectory)
            /// <summary>
            /// This method returns the New Name
            /// </summary>
            private static string SetNewName(ImageClassification image, string outputDirectory)
            {
                // initial value
                string newName = "";

                // Create a new instance of a 'FileInfo' object.
                FileInfo fileInfo = new FileInfo(image.FileName);

                // Create a new instance of a 'StringBuilder' object.
                StringBuilder sb = new StringBuilder();

                // get the names
                string primary = GetColorName(image.PrimaryColor);
                int valuePrimary = GetPrimaryValue(image);
                string secondary = GetColorName(image.SecondaryColor);
                int ValueSecondary = GetSecondaryValue(image);
                string minority = GetColorName(image.MinorityColor);
                int valueMinority = GetMinorityValue(image);
                string value1 = GetValueAsText(valuePrimary);
                string value2 = GetValueAsText(ValueSecondary);
                string value3 = GetValueAsText(valueMinority);

                // Now build the string
                sb.Append(primary);
                sb.Append(secondary);
                sb.Append(minority);
                sb.Append(value1);
                sb.Append(value2);
                sb.Append(value3);
                sb.Append(fileInfo.Extension);

                // Add the new name
                newName = Path.Combine(outputDirectory, sb.ToString());

                // Return the fileName with a partialGuid
                newName = FileHelper.CreateFileNameWithPartialGuid(newName, 12);

                // return value
                return newName;
            }
            #endregion
            
            #region Sort(string sourceDirectory, string outputDirectory, List<string> extensions, StatusUpdate statusUpdate, int fastSortFactor = 0)
            /// <summary>
            /// This method sorts the images in the source directory, if the extension is in the extensions list
            /// </summary>
            /// <param name="sourceDirectory"></param>
            /// <param name="outputDirectory"></param>
            /// <param name="extensions"></param>
            public static void Sort(string sourceDirectory, string outputDirectory, List<string> extensions, StatusUpdate statusUpdate, int fastSortFactor = 0)
            {
                // locals
                List<string> files = GetFiles(sourceDirectory, extensions);

                // locals
                List<ImageClassification> images = new List<ImageClassification>();
                int count = 0;

                // if the files exist
                if (ListHelper.HasOneOrMoreItems(files))
                {
                    // if statusUpdate exists
                    if (NullHelper.Exists(statusUpdate))
                    {
                        // Callback
                        statusUpdate("Total Files", files.Count);
                    }

                    // Iterate the collection of string objects
                    foreach (string fileName in files)
                    {
                        // Increment the value for count
                        count++;

                        // get the path
                        string path = System.IO.Path.Combine(sourceDirectory, fileName);

                        // load the pixelDatabase
                        PixelDatabase pixelDatabase = PixelDatabaseLoader.LoadPixelDatabase(path, null);

                        // Classify this image
                        ImageClassification classification = ImageClassifier.Classify(pixelDatabase, fastSortFactor);
                        classification.FileName = path;

                        // Set the classification
                        images.Add(classification);

                        // sent an update message every 10
                        if ((count % 10 == 0) && (NullHelper.Exists(statusUpdate)))
                        {
                            // update status
                            statusUpdate("Status: Analyzing...", count);
                        }
                    }
    
                    // update status
                    statusUpdate("Status: Finished Analyzing. Setting new file names", count);

                    // Now set the primary, secondary and minority colors so the image can be sorted
                    foreach (ImageClassification image in images)
                    {
                        // Set the new fileName based on the Classification values
                        image.NewName = SetNewName(image, outputDirectory);                        
                    }

                    // Now sort the images by the new name
                    images = images.OrderBy(x => x.NewName).ToList();

                     // if statusUpdate exists
                    if (NullHelper.Exists(statusUpdate))
                    {
                        // Callback
                        statusUpdate("Sorting Files...", files.Count);
                        statusUpdate("Total", files.Count);
                    }

                    // reset
                    count = 0;

                    // Now copy the images to the destination directory
                    foreach (ImageClassification image in images)
                    {
                        // Increment the value for count
                        count++;

                         // sent an update message every 10
                        if ((count % 10 == 0) && (NullHelper.Exists(statusUpdate)))
                        {
                            // update status
                            statusUpdate("Status: Copying Files...", count);
                        }

                        // Copy the file
                        File.Copy(image.FileName, image.NewName);
                    }


                    // If the StatusUpdate exists
                    if (NullHelper.Exists(statusUpdate))
                    {
                        // update status
                        statusUpdate("Status: Finished. Total files sorted and copied: ", count);
                    }
                }
            }
            #endregion

            #region Sort(string sourceDirectory, string outputDirectory, string extension, StatusUpdate statusUpdate, int fastSortFactor = 0)
            /// <summary>
            /// This method sorts the images in the source directory, if the extension is in the extensions list
            /// </summary>
            /// <param name="sourceDirectory"></param>
            /// <param name="outputDirectory"></param>
            /// <param name="extensions"></param>
            public static void Sort(string sourceDirectory, string outputDirectory, string extension, StatusUpdate statusUpdate, int fastSortFactor = 0)
            {
                // if there are extensions listed
                if (TextHelper.Exists(extension))
                {
                    // Create the list to call
                    List<string> extensions = new List<string>();

                    // Add this item
                    extensions.Add(extension);

                    // Call Sort override
                    Sort(sourceDirectory, outputDirectory, extensions, statusUpdate, fastSortFactor);
                }
            }
            #endregion

            #region Sort(string sourceDirectory, string outputDirectory, StatusUpdate statusUpdate, int fastSortFactor = 0)
            /// <summary>
            /// This method sorts the images in the source directory, if the extension is in the extensions list
            /// </summary>
            /// <param name="sourceDirectory"></param>
            /// <param name="outputDirectory"></param>
            /// <param name="extensions"></param>
            public static void Sort(string sourceDirectory, string outputDirectory, StatusUpdate statusUpdate, int fastSortFactor = 0)
            {  
                // Create the list to call
                List<string> extensions = new List<string>();

                // Add default images
                extensions.Add(".jpg");
                extensions.Add(".png");

                // Call Sort override
                Sort(sourceDirectory, outputDirectory, extensions, statusUpdate, fastSortFactor);
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
