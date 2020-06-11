

#region using statements

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class IndexRange
    /// <summary>
    /// This class is used in conjunction with the LastUpdate to keep track of the pixels that were updated
    /// in the last update. The IndexRange helps to reduce the size.
    /// </summary>
    public class IndexRange
    {
        
        #region Private Variables
        private int start;
        private int end;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of an IndexRange object
        /// </summary>
        /// <param name="start"></param>
        public IndexRange(int start)
        {
            // Set the
            Start = start;
            End = start;
        }
        #endregion

        #region Methods

            #region AddIfContinuous(int index)
            /// <summary>
            /// This method returns the Continuous
            /// </summary>
            public bool AddIfContinuous(int index)
            {
                // initial value
                bool isContinuous = ((index - end) == 1);

                // if the value for isContinuous is true
                if (isContinuous)
                {
                    // set the new end
                    end = index;
                }
                
                // return value
                return isContinuous;
            }
            #endregion
            
            #region IsPixelIncluded(int index)
            /// <summary>
            /// This method returns the Pixel Included
            /// </summary>
            public bool IsPixelIncluded(int index)
            {
                // initial value
                bool isPixelIncluded = ((index >= start) && (index <= end));

                // return value
                return isPixelIncluded;
            }
        #endregion

            #region ToString()
            /// <summary>
            /// method returns the String
            /// </summary>
            public override string ToString()
            {
                // return some useful information (to a human) for debugging
                return "Start: " + Start + " End: " + End;
            }
            #endregion
            
        #endregion

        #region Properties

            #region End
            /// <summary>
            /// This property gets or sets the value for 'End'.
            /// </summary>
            public int End
            {
                get { return end; }
                set { end = value; }
            }
            #endregion
            
            #region Start
            /// <summary>
            /// This property gets or sets the value for 'Start'.
            /// </summary>
            public int Start
            {
                get { return start; }
                set { start = value; }
            }
            #endregion

            #region TotalPixels
            /// <summary>
            /// This read only property returns the total number of pixels affected
            /// </summary>
            public int TotalPixels
            {
                get
                {
                    // initial value
                    int total = End - Start + 1;

                    // return value
                    return total;
                }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
