

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
            
        #endregion

    }
    #endregion

}
