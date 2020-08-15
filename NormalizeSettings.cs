

#region using statements

using System;
using System.Collections.Generic;
using System.Text;
using DataJuggler.UltimateHelper.Core;
using DataJuggler.UltimateHelper.Core.Objects;
using System.Drawing;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class NormalizeSettings
    /// <summary>
    /// This class is used to return the values that comprise a valid Normalize query.
    /// These values include the world of a line before the Where clause must
    /// start with the world Normalize.
    /// </summary>
    public class NormalizeSettings
    {
        
        #region Private Variables
        private List<TextLine> lines;
        private int max;
        private int min;
        private bool normalize;
        private int step;
        private Color color;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a NormalizeSettings object
        /// </summary>
        public NormalizeSettings()
        {
            // Create a new collection of 'TextLine' objects.
            Lines = new List<TextLine>();
        }
        #endregion

        #region Properties
            
            #region Color
            /// <summary>
            /// This property gets or sets the value for 'Color'.
            /// </summary>
            public Color Color
            {
                get { return color; }
                set { color = value; }
            }
            #endregion
            
            #region HasColor
            /// <summary>
            /// This property returns true if this object has a 'Color'.
            /// </summary>
            public bool HasColor
            {
                get
                {
                    // initial value
                    bool hasColor = (this.Color != Color.Empty);
                    
                    // return value
                    return hasColor;
                }
            }
            #endregion
            
            #region HasMax
            /// <summary>
            /// This property returns true if the 'Max' is set.
            /// </summary>
            public bool HasMax
            {
                get
                {
                    // initial value
                    bool hasMax = (this.Max > 0);
                    
                    // return value
                    return hasMax;
                }
            }
            #endregion
            
            #region HasMin
            /// <summary>
            /// This property returns true if the 'Min' is set.
            /// </summary>
            public bool HasMin
            {
                get
                {
                    // initial value
                    bool hasMin = (this.Min > 0);
                    
                    // return value
                    return hasMin;
                }
            }
            #endregion
            
            #region HasStep
            /// <summary>
            /// This property returns true if the 'Step' is set.
            /// </summary>
            public bool HasStep
            {
                get
                {
                    // initial value
                    bool hasStep = (this.Step > 0);
                    
                    // return value
                    return hasStep;
                }
            }
            #endregion

            #region IsValidNormalizeQuery
            /// <summary>
            /// This read only property returns true if a Normalize is true, and the Min, Max and Step are set.
            /// This query can be extrememely slow on large images
            /// </summary>
            public bool IsValidNormalizeQuery
            {
                get
                {
                    // initial value
                    bool isValid = (Normalize && HasMin && HasMax && HasStep && HasColor);

                    // return value
                    return isValid;
                }
            }
            #endregion
            
            #region Lines
            /// <summary>
            /// This property gets or sets the value for 'Lines'.
            /// </summary>
            public List<TextLine> Lines
            {
                get { return lines; }
                set { lines = value; }
            }
            #endregion
            
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
            
            #region Normalize
            /// <summary>
            /// This property gets or sets the value for 'Normalize'.
            /// </summary>
            public bool Normalize
            {
                get { return normalize; }
                set { normalize = value; }
            }
            #endregion
            
            #region Step
            /// <summary>
            /// This property gets or sets the value for 'Step'.
            /// </summary>
            public int Step
            {
                get { return step; }
                set { step = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
