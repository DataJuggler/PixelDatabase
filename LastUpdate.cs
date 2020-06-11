

#region using statements

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class LastUpdate
    /// <summary>
    /// This class is used to attempt to keep track of the pixels updated in the last update.
    /// If the count of Index Range's gets too high (1,000 or 10,000 or maybe 100,000 ?), this will 
    /// get too large to keep track of so it becomes unavailable in that case.
    /// </summary>
    public class LastUpdate
    {
        
        #region Private Variables
        private List<IndexRange> indexRangeList;
        private IndexRange currentIndexRange;
        private bool available;
        private int minimum;
        private int maximum;
        private const int MaxListSize = 10000;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a LastUpdate object
        /// </summary>
        public LastUpdate()
        {
            // Create a new collection of 'IndexRange' objects.
            IndexRangeList = new List<IndexRange>();

            // Default to true until Max has been exceeded
            Available = true;
        }
        #endregion

        #region Methods

            #region AddIndex(int index)
            /// <summary>
            /// This method Add Index
            /// </summary>
            public bool AddIndex(int index)
            {
                // initial value
                bool added = false;

                // if the value for HasCurrentIndexRange is true
                if ((HasCurrentIndexRange) && (CurrentIndexRange.AddIfContinuous(index)))
                {
                    // nothing to do here, this item was just added
                    added = true;
                }
                else
                {
                    // as long as we haven't exceeded the max list size
                    if (IndexRangeList.Count < MaxListSize)
                    {
                        // Create a new range
                        CurrentIndexRange = new IndexRange(index);

                        // Add this item
                        this.IndexRangeList.Add(CurrentIndexRange);

                        // has been added
                        added = true;
                    }
                    else
                    {
                        // already set, just making it obvious
                        added = false;

                        // not available to do a LastUpdate query
                        Available = false;
                    }
                }

                // return the index as long as the Max
                return added;
            }
            #endregion
            
            #region GetMaximum()
            /// <summary>
            /// This method returns the index of the last pixel that should be included.
            /// </summary>
            /// <returns></returns>
            public int GetMaximum()
            {
                // not found
                int end = -1;

                // if Available
                if (Available)
                {
                    // if there are one or more items
                    if ((HasIndexRangeList) && (IndexRangeList.Count > 0))
                    {
                        // set the return value
                        end = IndexRangeList[IndexRangeList.Count -1].End;

                        // set the value
                        Maximum = end;
                    }
                }

                // return value
                return end;
            }
            #endregion

            #region GetMinimum()
            /// <summary>
            /// This method returns the first pixel that should be included.
            /// </summary>
            /// <returns></returns>
            public int GetMinimum()
            {
                // not found
                int start = -1;

                // if Available
                if (Available)
                {
                    // if there are one or more items
                    if ((HasIndexRangeList) && (IndexRangeList.Count > 0))
                    {
                        // set the return value
                        start = IndexRangeList[0].Start;

                        // Set this value
                        Minimum = start;
                    }
                }

                // return value
                return start;
            }
            #endregion

            #region IsPixelIncluded(int index)
            /// <summary>
            /// This method returns the Pixel Included
            /// </summary>
            public bool IsPixelIncluded(int index)
            {
                // initial value
                bool isPixelIncluded = false;
                
                // if the Index range list exists
                if (HasIndexRangeList)
                {
                    // Iterate the collection of IndexRange objects
                    foreach (IndexRange range in IndexRangeList)
                    {
                        // if this pixel is included in this range
                        if (range.IsPixelIncluded(index))
                        {
                            // set the return value
                            isPixelIncluded = true;

                            // break out of the loop
                            break;
                        }
                    }
                }

                // return value
                return isPixelIncluded;
            }
            #endregion
            
        #endregion

        #region Properties

            #region Available
            /// <summary>
            /// This property gets or sets the value for 'Available'.
            /// </summary>
            public bool Available
            {
                get { return available; }
                set { available = value; }
            }
            #endregion
            
            #region CurrentIndexRange
            /// <summary>
            /// This property gets or sets the value for 'CurrentIndexRange'.
            /// </summary>
            public IndexRange CurrentIndexRange
            {
                get { return currentIndexRange; }
                set { currentIndexRange = value; }
            }
            #endregion
            
            #region HasCurrentIndexRange
            /// <summary>
            /// This property returns true if this object has a 'CurrentIndexRange'.
            /// </summary>
            public bool HasCurrentIndexRange
            {
                get
                {
                    // initial value
                    bool hasCurrentIndexRange = (this.CurrentIndexRange != null);
                    
                    // return value
                    return hasCurrentIndexRange;
                }
            }
            #endregion
            
            #region HasIndexRangeList
            /// <summary>
            /// This property returns true if this object has an 'IndexRangeList'.
            /// </summary>
            public bool HasIndexRangeList
            {
                get
                {
                    // initial value
                    bool hasIndexRangeList = (this.IndexRangeList != null);
                    
                    // return value
                    return hasIndexRangeList;
                }
            }
            #endregion
            
            #region IndexRangeList
            /// <summary>
            /// This property gets or sets the value for 'IndexRangeList'.
            /// </summary>
            public List<IndexRange> IndexRangeList
            {
                get { return indexRangeList; }
                set { indexRangeList = value; }
            }
            #endregion

            #region Maximum
            /// <summary>
            /// This property gets or sets the value for 'Maximum'.
            /// </summary>
            public int Maximum
            {
                get { return maximum; }
                set { maximum = value; }
            }
            #endregion
            
            #region Minimum
            /// <summary>
            /// This property gets or sets the value for 'Minimum'.
            /// </summary>
            public int Minimum
            {
                get { return minimum; }
                set { minimum = value; }
            }
            #endregion
            
            #region TotalPixels
            /// <summary>
            /// This read only property returns the total number of pixels updated in the LastUpdate
            /// </summary>
            public int TotalPixels
            {
                get
                {
                    // initilal value
                    int totalPixels = 0;

                    // if the value for HasIndexRangeList is true
                    if (HasIndexRangeList)
                    {
                        // Set the Sum of total pixels (mainly for debugging for now)
                        totalPixels = IndexRangeList.Sum(x => x.TotalPixels);
                    }

                    // return value
                    return totalPixels;
                }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
