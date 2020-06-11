

#region using statements

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class QueryRange
    /// <summary>
    /// This class is used to determine which parts of an image are affected by a query.
    /// By setting the StartX, EndX, StartY and EndY, the DirectBitmap can be iterated
    /// to apply the remaining criteria at a much faster rate.
    /// </summary>
    public class QueryRange
    {
        
        #region Private Variables
        private int endX;
        private int endY;
        private int startX;
        private int startY;
        #endregion

        #region Properties
            
            #region EndX
            /// <summary>
            /// This property gets or sets the value for 'EndX'.
            /// </summary>
            public int EndX
            {
                get { return endX; }
                set { endX = value; }
            }
            #endregion
            
            #region EndY
            /// <summary>
            /// This property gets or sets the value for 'EndY'.
            /// </summary>
            public int EndY
            {
                get { return endY; }
                set { endY = value; }
            }
            #endregion

            #region Size
            /// <summary>
            /// This read only property returns the Size of this object.
            /// This value calculated by multiplying ((EndX -1) - StartX) * ((EndY -1) - StartY);
            /// </summary>
            public int Size
            {
                get
                {
                    // initial value
                    int width = EndX - StartX -1;
                    int height = EndY - StartY -1;
                    int size = width * height;

                    // return value
                    return size;
                }
            }
            #endregion
            
            #region StartX
            /// <summary>
            /// This property gets or sets the value for 'StartX'.
            /// </summary>
            public int StartX
            {
                get { return startX; }
                set { startX = value; }
            }
            #endregion
            
            #region StartY
            /// <summary>
            /// This property gets or sets the value for 'StartY'.
            /// </summary>
            public int StartY
            {
                get { return startY; }
                set { startY = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
