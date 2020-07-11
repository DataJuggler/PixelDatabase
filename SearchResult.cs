

#region using statements

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class SearchResult
    /// <summary>
    /// This class is used to return the point of a Bitmap sub image inside a larger image.
    /// The final result is the confidence, or match percentage of the result. This can be
    /// used to guage the accuracy of this result.
    /// </summary>
    public class SearchResult
    {
        
        #region Private Variables
        private Point point;        
        private double confidence;
        private long score;
        #endregion

        #region Properties
            
            #region Confidence
            /// <summary>
            /// This property gets or sets the value for 'Confidence'.
            /// </summary>
            public double Confidence
            {
                get { return confidence; }
                set { confidence = value; }
            }
            #endregion
            
            #region Point
            /// <summary>
            /// This property gets or sets the value for 'Point'.
            /// </summary>
            public Point Point
            {
                get { return point; }
                set { point = value; }
            }
            #endregion
            
            #region Score
            /// <summary>
            /// This property gets or sets the value for 'Score'.
            /// </summary>
            public long Score
            {
                get { return score; }
                set { score = value; }
            }
            #endregion
            
        #endregion

        
    }
    #endregion

}
