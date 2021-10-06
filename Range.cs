

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class Range
    /// <summary>
    /// This query is used to keep track of min and max values
    /// </summary>
    public class Range
    {
        
        #region Private Variables
        private int min;
        private int max;
        #endregion
        
        #region Properties
        
            #region Max
            /// <summary>
            /// This property gets or sets the value for 'Max'.
            /// </summary>
            public int Max
            {
                get { return max; }
                set { max = value; }
            }
            #endregion
            
            #region Min
            /// <summary>
            /// This property gets or sets the value for 'Min'.
            /// </summary>
            public int Min
            {
                get { return min; }
                set { min = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
