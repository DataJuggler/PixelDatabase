

#region using statements

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class QueryResponse
    /// <summary>
    /// This class is used to return a response from some updates.
    /// </summary>
    public class QueryResponse
    {
        
        #region Private Variables
        private bool success;
        private int pixelsUpdated;
        private Exception error;
        #endregion
        
        #region Events
            
        #endregion
        
        #region Methods
            
        #endregion
        
        #region Properties
            
            #region Error
            /// <summary>
            /// This property gets or sets the value for 'Error'.
            /// </summary>
            public Exception Error
            {
                get { return error; }
                set { error = value; }
            }
            #endregion
            
            #region HasError
            /// <summary>
            /// This property returns true if this object has an 'Error'.
            /// </summary>
            public bool HasError
            {
                get
                {
                    // initial value
                    bool hasError = (Error != null);

                    // return value
                    return hasError;
                }
            }
            #endregion
            
            #region PixelsUpdated
            /// <summary>
            /// This property gets or sets the value for 'PixelsUpdated'.
            /// </summary>
            public int PixelsUpdated
            {
                get { return pixelsUpdated; }
                set { pixelsUpdated = value; }
            }
            #endregion
            
            #region Success
            /// <summary>
            /// This property gets or sets the value for 'Success'.
            /// </summary>
            public bool Success
            {
                get { return success; }
                set { success = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
