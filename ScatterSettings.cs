

#region using statements

using System;
using System.Collections.Generic;
using System.Text;
using DataJuggler.UltimateHelper;
using DataJuggler.UltimateHelper.Objects;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class ScatterSettings
    /// <summary>
    /// This class is used to set the value for Scatter true or false, plus the ScatterPercent if true.
    /// Also the TextLines are returned without Scatter, so the rest of the parsing stays the same.
    /// </summary>
    public class ScatterSettings
    {
        
        #region Private Variables
        private bool scatter;
        private double scatterPercent;
        private int modX;
        private int modY;
        private List<TextLine> textLines;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'ScatterSettings' object.
        /// </summary>
        public ScatterSettings()
        {
            // Create a new collection of 'TextLine' objects.
            TextLines = new List<TextLine>();
        }
        #endregion
        
        #region Properties
            
            #region HasTextLines
            /// <summary>
            /// This property returns true if this object has a 'TextLines'.
            /// </summary>
            public bool HasTextLines
            {
                get
                {
                    // initial value
                    bool hasTextLines = (this.TextLines != null);
                    
                    // return value
                    return hasTextLines;
                }
            }
            #endregion
            
            #region ModX
            /// <summary>
            /// This property gets or sets the value for 'ModX'.
            /// </summary>
            public int ModX
            {
                get { return modX; }
                set { modX = value; }
            }
            #endregion
            
            #region ModY
            /// <summary>
            /// This property gets or sets the value for 'ModY'.
            /// </summary>
            public int ModY
            {
                get { return modY; }
                set { modY = value; }
            }
            #endregion
            
            #region Scatter
            /// <summary>
            /// This property gets or sets the value for 'Scatter'.
            /// </summary>
            public bool Scatter
            {
                get { return scatter; }
                set { scatter = value; }
            }
            #endregion
            
            #region ScatterPercent
            /// <summary>
            /// This property gets or sets the value for 'ScatterPercent'.
            /// </summary>
            public double ScatterPercent
            {
                get { return scatterPercent; }
                set { scatterPercent = value; }
            }
            #endregion
            
            #region TextLines
            /// <summary>
            /// This property gets or sets the value for 'TextLines'.
            /// </summary>
            public List<TextLine> TextLines
            {
                get { return textLines; }
                set { textLines = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
