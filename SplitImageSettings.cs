

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataJuggler.PixelDatabase.Enumerations;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class SplitImageSettings
    /// <summary>
    /// method [Enter Method Description]
    /// </summary>
    public class SplitImageSettings
    {
        
        #region Private Variables        
        private SplitImageDirectionEnum direction;
        private int splitX;
        #endregion

        #region Properties
            
            #region Direcction
            /// <summary>
            /// This property gets or sets the value for 'Direcction'.
            /// </summary>
            public SplitImageDirectionEnum Direction
            {
                get { return direction; }
                set { direction = value; }
            }
            #endregion
            
            #region SplitX
            /// <summary>
            /// This property gets or sets the value for 'SplitX'.
            /// </summary>
            public int SplitX
            {
                get { return splitX; }
                set { splitX = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
