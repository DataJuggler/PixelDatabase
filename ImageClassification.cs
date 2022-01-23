

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataJuggler.PixelDatabase.Enumerations;
using DataJuggler.PixelDatabase;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class ImageClassifier
    /// <summary>
    /// This class is used to score an image's value to make it easier to sort
    /// </summary>
    public class ImageClassification
    {
        
        #region Private Variables
        private double averageRed;
        private double averageBlue;
        private double averageGreen;
        private PrimaryColorEnum primaryColor;
        private PrimaryColorEnum secondaryColor;
        private PrimaryColorEnum minorityColor;
        private int minRed;
        private int minGreen;
        private int minBlue;
        private int maxRed;
        private int maxGreen;
        private int maxBlue;
        private string fileName;
        private string newName;
        #endregion

        #region Properties

            #region AverageBlue
            /// <summary>
            /// This property gets or sets the value for 'AverageBlue'.
            /// </summary>
            public double AverageBlue
            {
                get { return averageBlue; }
                set { averageBlue = value; }
            }
            #endregion
            
            #region AverageGreen
            /// <summary>
            /// This property gets or sets the value for 'AverageGreen'.
            /// </summary>
            public double AverageGreen
            {
                get { return averageGreen; }
                set { averageGreen = value; }
            }
            #endregion
            
            #region AverageRed
            /// <summary>
            /// This property gets or sets the value for 'AverageRed'.
            /// </summary>
            public double AverageRed
            {
                get { return averageRed; }
                set { averageRed = value; }
            }
            #endregion
            
            #region FileName
            /// <summary>
            /// This property gets or sets the value for 'FileName'.
            /// </summary>
            public string FileName
            {
                get { return fileName; }
                set { fileName = value; }
            }
            #endregion

            #region MaxAverage
            /// <summary>
            /// This property returns the average Maximum
            /// </summary>
            public int MaxAverage
            {
                get
                {
                    // set the return value
                    int maxAverage = (int) MaxTotal / 3;
                    
                    // return value
                    return maxAverage;
                }
            }
            #endregion
            
            #region MaxBlue
            /// <summary>
            /// This property gets or sets the value for 'MaxBlue'.
            /// </summary>
            public int MaxBlue
            {
                get { return maxBlue; }
                set { maxBlue = value; }
            }
            #endregion
            
            #region MaxGreen
            /// <summary>
            /// This property gets or sets the value for 'MaxGreen'.
            /// </summary>
            public int MaxGreen
            {
                get { return maxGreen; }
                set { maxGreen = value; }
            }
            #endregion
            
            #region MaxRed
            /// <summary>
            /// This property gets or sets the value for 'MaxRed'.
            /// </summary>
            public int MaxRed
            {
                get { return maxRed; }
                set { maxRed = value; }
            }
            #endregion

            #region MaxTotal
            /// <summary>
            /// This property gets or sets the value for 'MaxTotal'.
            /// </summary>
            public int MaxTotal
            {
                get
                {
                    // set the return value
                    int MaxTotal = MaxRed + MaxGreen + MaxBlue;
                    
                    // return value
                    return MaxTotal;
                }
            }
            #endregion

            #region MinAverage
            /// <summary>
            /// This property returns the average minimum
            /// </summary>
            public int MinAverage
            {
                get
                {
                    // set the return value
                    int minAverage = (int) MinTotal / 3;
                    
                    // return value
                    return minAverage;
                }
            }
            #endregion
            
            #region MinBlue
            /// <summary>
            /// This property gets or sets the value for 'MinBlue'.
            /// </summary>
            public int MinBlue
            {
                get { return minBlue; }
                set { minBlue = value; }
            }
            #endregion

            #region MinMaxAverageDifference
            /// <summary>
            /// This read only property returns MaxAverage minus MinAverage
            /// </summary>
            public int MinMaxAverageDifference
            {
                get
                {
                    // initial value
                    int minMaxAverageDifference = MaxAverage - MinAverage;

                    // return value
                    return minMaxAverageDifference = MaxAverage - MinAverage;
                }
            }
            #endregion
            
            #region MinGreen
            /// <summary>
            /// This property gets or sets the value for 'MinGreen'.
            /// </summary>
            public int MinGreen
            {
                get { return minGreen; }
                set { minGreen = value; }
            }
            #endregion
            
            #region MinorityColor
            /// <summary>
            /// This property gets or sets the value for 'MinorityColor'.
            /// </summary>
            public PrimaryColorEnum MinorityColor
            {
                get { return minorityColor; }
                set { minorityColor = value; }
            }
            #endregion
            
            #region MinRed
            /// <summary>
            /// This property gets or sets the value for 'MinRed'.
            /// </summary>
            public int MinRed
            {
                get { return minRed; }
                set { minRed = value; }
            }
            #endregion
            
            #region MinTotal
            /// <summary>
            /// This read only property returns the value for 'MinTotal'.
            /// </summary>
            public int MinTotal
            {
                get
                {
                    // set the return value
                    int minTotal = MinRed + MinGreen + MinBlue;
                    
                    // return value
                    return minTotal;
                }
            }
            #endregion
            
            #region NewName
            /// <summary>
            /// This property gets or sets the value for 'NewName'.
            /// </summary>
            public string NewName
            {
                get { return newName; }
                set { newName = value; }
            }
            #endregion
            
            #region PrimaryColor
            /// <summary>
            /// This property gets or sets the value for 'PrimaryColor'.
            /// </summary>
            public PrimaryColorEnum PrimaryColor
            {
                get { return primaryColor; }
                set { primaryColor = value; }
            }
            #endregion
            
            #region PrimaryColorName
            /// <summary>
            /// This read only property returns the Primary Color Name
            /// </summary>
            public string PrimaryColorName
            {
                get
                {
                    // initial value
                    string name = "Blue";
                    
                    if (PrimaryColor == PrimaryColorEnum.Red)
                    {
                        // set the return value
                        name = "Red";
                    }
                    else if (PrimaryColor == PrimaryColorEnum.Green)
                    {
                        // set the return value
                        name = "Green";
                    }
                    
                    // return value
                    return name;
                }
            }
            #endregion
            
            #region SecondaryColor
            /// <summary>
            /// This property gets or sets the value for 'SecondaryColor'.
            /// </summary>
            public PrimaryColorEnum SecondaryColor
            {
                get { return secondaryColor; }
                set { secondaryColor = value; }
            }
            #endregion

            #region SecondaryColorName
            /// <summary>
            /// This read only property returns the Secondary Color Name
            /// </summary>
            public string SecondaryColorName
            {
                get
                {
                    // initial value
                    string name = "Blue";
                    
                    if (SecondaryColor == PrimaryColorEnum.Red)
                    {
                        // set the return value
                        name = "Red";
                    }
                    else if (SecondaryColor == PrimaryColorEnum.Green)
                    {
                        // set the return value
                        name = "Green";
                    }
                    
                    // return value
                    return name;
                }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
