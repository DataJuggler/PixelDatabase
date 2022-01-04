

#region using statements

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using DataJuggler.PixelDatabase.Enumerations;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class QueryBuilder
    /// <summary>
    /// This class helps you programmatically create queries.
    /// QueryBuilder will grow over time, as I don't need all
    /// the features of PixelDatabase yet.
    /// </summary>
    public class QueryBuilder
    {
        
        #region Private Variables
        private Color color;
        private Range xRange;
        private Range yRange;
        private List<PixelCriteria> criteria;
        private ActionTypeEnum actionType;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'QueryBuilder' object.
        /// </summary>
        public QueryBuilder(ActionTypeEnum actionType)
        {
            // Store the arg
            ActionType = actionType;

            // Create a new collection of 'PixelCriteria' objects.
            Criteria = new List<PixelCriteria>();
        }
        #endregion
        
        #region Methods
            
            #region AddCriteria(PixelCriteria criteria)
            /// <summary>
            /// Add Criteria
            /// </summary>
            public void AddCriteria(PixelCriteria criteria)
            {
                // Add this item
                Criteria.Add(criteria);
            }
            #endregion
            
            #region ToString()
            /// <summary>
            /// method returns the String
            /// </summary>
            public override string ToString()
            {
                // Create the string builder
                StringBuilder sb = new StringBuilder();
                
                // get the return value
                string query = sb.ToString();
                
                if (ActionType == ActionTypeEnum.Update)
                {   
                    // Add the word update and a new line
                    sb.Append("Update");
                    sb.Append(Environment.NewLine);
                }

                if (Color != Color.Empty)
                {  
                    // To be safe, always using RGB
                    sb.Append("Set Color ");
                    sb.Append(Color.R);
                    // Add space
                    sb.Append(' ');
                    sb.Append(Color.G);
                    // Add space
                    sb.Append(' ');
                    sb.Append(Color.B);
                    sb.Append(Environment.NewLine);
                }
                
                // return value
                return sb.ToString();
            }
            #endregion
            
        #endregion
        
        #region Properties
            
            #region ActionType
            /// <summary>
            /// This property gets or sets the value for 'ActionType'.
            /// </summary>
            public ActionTypeEnum ActionType
            {
                get { return actionType; }
                set { actionType = value; }
            }
            #endregion
            
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
            
            #region Criteria
            /// <summary>
            /// This property gets or sets the value for 'Criteria'.
            /// </summary>
            public List<PixelCriteria> Criteria
            {
                get { return criteria; }
                set { criteria = value; }
            }
            #endregion
            
            #region XRange
            /// <summary>
            /// This property gets or sets the value for 'XRange'.
            /// </summary>
            public Range XRange
            {
                get { return xRange; }
                set { xRange = value; }
            }
            #endregion
            
            #region YRange
            /// <summary>
            /// This property gets or sets the value for 'YRange'.
            /// </summary>
            public Range YRange
            {
                get { return yRange; }
                set { yRange = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
