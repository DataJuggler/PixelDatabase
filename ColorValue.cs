

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

    #region class ColorValue
    /// <summary>
    /// This class contains a primary color and its value,
    /// this making it easy to find the Mean value.
    /// </summary>
    public class ColorValue
    {
        
        #region Private Variables
        private PrimaryColorEnum color;
        private int _value;
        private string alpha;
        #endregion

        #region ColorValue(PrimaryColorEnum color, int value, string alpha)
        /// <summary>
        /// Create a new instance of a ColorValue object
        /// </summary>
        /// <param name="primaryColor"></param>
        /// <param name="value"></param>
        /// <param name="alpha"></param>
        public ColorValue(PrimaryColorEnum color, int value, string alpha)
        {
            // store
            Color = color;
            Value = value;
            Alpha = alpha;
        }
        #endregion

        #region Properties

            #region Alpha
            /// <summary>
            /// This property gets or sets the value for 'Alpha'.
            /// </summary>
            public string Alpha
            {
                get { return alpha; }
                set { alpha = value; }
            }
            #endregion
            
            #region Color
            /// <summary>
            /// This property gets or sets the value for 'Color'.
            /// </summary>
            public PrimaryColorEnum Color
            {
                get { return color; }
                set { color = value; }
            }
            #endregion
            
            #region Value
            /// <summary>
            /// This property gets or sets the value for 'Value'.
            /// </summary>
            public int Value
            {
                get { return _value; }
                set { _value = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
