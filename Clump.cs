using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DataJuggler.PixelDatabase
{

    /// <summary>
    /// This class is used to apply a query, but it only applies a query if the 
    /// adjacent pixels match the clump criteria.
    /// </summary>
    public class Clump
    {
        #region Private Variables
        private int red;
        private int green;
        private int blue;
        private PixelQuery pixelQuery;
        #endregion

        #region Properties

            #region Blue
            /// <summary>
            /// This property gets or sets the value for 'Blue'.
            /// </summary>
            public int Blue
            {
                get { return blue; }
                set { blue = value; }
            }
            #endregion
            
            #region Color
            /// <summary>
            /// This read only property returns the value of Color from the object Color.
            /// </summary>
            public Color Color
            {
                
                get
                {
                    // initial value
                    Color color = Color.FromArgb(Red, Green, Blue);
                    
                    // return value
                    return color;
                }
            }
            #endregion
            
            #region Green
            /// <summary>
            /// This property gets or sets the value for 'Green'.
            /// </summary>
            public int Green
            {
                get { return green; }
                set { green = value; }
            }
            #endregion
            
            #region HasPixelQuery
            /// <summary>
            /// This property returns true if this object has a 'PixelQuery'.
            /// </summary>
            public bool HasPixelQuery
            {
                get
                {
                    // initial value
                    bool hasPixelQuery = (this.PixelQuery != null);
                    
                    // return value
                    return hasPixelQuery;
                }
            }
            #endregion
            
            #region PixelQuery
            /// <summary>
            /// This property gets or sets the value for 'PixelQuery'.
            /// </summary>
            public PixelQuery PixelQuery
            {
                get { return pixelQuery; }
                set { pixelQuery = value; }
            }
            #endregion
            
            #region Red
            /// <summary>
            /// This property gets or sets the value for 'Red'.
            /// </summary>
            public int Red
            {
                get { return red; }
                set { red = value; }
            }
            #endregion
            
        #endregion

    }
}
