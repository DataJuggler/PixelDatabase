

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class Layer
    /// <summary>
    /// This class is used to display additional layers on
    /// top of the BackgroundImage.
    /// </summary>
    public class Layer
    {
        
        #region Private Variables
        private List<PixelInformation> pixels;
        private int displayOrder;
        private bool visible;
        private string name;
        private bool selected;
        private int index;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'Layer' object.
        /// </summary>
        public Layer()
        {
            // Create the Pixels
            Pixels = new List<PixelInformation>();
        }
        #endregion
        
        #region Properties
            
            #region DisplayOrder
            /// <summary>
            /// This property gets or sets the value for 'DisplayOrder'.
            /// </summary>
            public int DisplayOrder
            {
                get { return displayOrder; }
                set { displayOrder = value; }
            }
            #endregion
            
            #region Index
            /// <summary>
            /// This property gets or sets the value for 'Index'.
            /// </summary>
            public int Index
            {
                get { return index; }
                set { index = value; }
            }
            #endregion
            
            #region Name
            /// <summary>
            /// This property gets or sets the value for 'Name'.
            /// </summary>
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            #endregion
            
            #region Pixels
            /// <summary>
            /// This property gets or sets the value for 'Pixels'.
            /// </summary>
            public List<PixelInformation> Pixels
            {
                get { return pixels; }
                set { pixels = value; }
            }
            #endregion
            
            #region Selected
            /// <summary>
            /// This property gets or sets the value for 'Selected'.
            /// </summary>
            public bool Selected
            {
                get { return selected; }
                set { selected = value; }
            }
            #endregion
            
            #region Visible
            /// <summary>
            /// This property gets or sets the value for 'Visible'.
            /// </summary>
            public bool Visible
            {
                get { return visible; }
                set { visible = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
