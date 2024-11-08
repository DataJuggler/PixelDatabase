﻿

#region using statements

using DataJuggler.UltimateHelper;
using System.Collections.Generic;
using System.Drawing;
using DataJuggler.PixelDatabase.Enumerations;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class PixelQuery
    /// <summary>
    /// This class contains a set of one or more PixelCriterias.
    /// </summary>
    public class PixelQuery
    {

        #region Private Variables
        private ActionTypeEnum actionType;
        private List<PixelCriteria> criteria;
        private Directions directions;
        private int red;
        private int green;
        private int blue;
        private int alpha;
        private Color color;
        private bool colorSet;
        private bool adjustColor;
        private bool swapColors;
        private Gradient gradient;
        private bool setColor;
        private bool scatter;
        private double scatterPercent;
        private int scatterModX;
        private int scatterModY;
        private SwapTypeEnum swapType;
        private int adjustment;
        private Mask mask;
        private bool setMaskColor;
        private string setMaskcolorName;
        private RGBColor colorToAdjust;
        private RGBColor assignToColor;
        private string queryText;
        private int pixelsUpdated;
        private bool normalize;
        private bool applyGrayscale;
        private GrayScaleFormulaEnum grayScaleFormula;
        private int min;
        private int max;
        private int step;
        private Color normalizeColor;
        private DataJuggler.RandomShuffler.LargeNumberShuffler shuffler;
        private SplitImageSettings splitImageSettings;
        private Clump clump;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a PixelQuery object
        /// </summary>
        public PixelQuery()
        {
            // Create a new instance of a 'Directions' object.
            this.Directions = new Directions();
        }
        #endregion

        #region Methods

            #region FindXCriteria()
            /// <summary>
            /// This method returns the X Criteria, if any.
            /// </summary>
            public PixelCriteria FindXCriteria()
            {
                // initial value
                PixelCriteria criteria = null;

                // local
                int index = -1;

                // if the value for HasCriteria is true
                if (HasCriteria)
                {
                    // Iterate the collection of PixelCriteria objects
                    foreach (PixelCriteria pixelCriteria in Criteria)
                    {
                        // Increment the value for index
                        index++;

                        // if this Criteria.PixelType equals X
                        if (pixelCriteria.PixelType == PixelTypeEnum.X)
                        {
                            // set the return value
                            criteria = pixelCriteria;

                            // Set the Index
                            criteria.Index = index;

                            // break out of the loop
                            break;
                        }
                    }
                }
                
                // return value
                return criteria;
            }
            #endregion

            #region FindYCriteria()
            /// <summary>
            /// This method returns the Y Criteria, if any.
            /// </summary>
            public PixelCriteria FindYCriteria()
            {
                // initial value
                PixelCriteria criteria = null;

                // local
                int index = -1;

                // if the value for HasCriteria is true
                if (HasCriteria)
                {
                    // Iterate the collection of PiYelCriteria objects
                    foreach (PixelCriteria pixelCriteria in Criteria)
                    {
                        // Increment the value for index
                        index++;

                        // if this Criteria.PiYelType equals Y
                        if (pixelCriteria.PixelType == PixelTypeEnum.Y)
                        {
                            // set the return value
                            criteria = pixelCriteria;

                            // Set the index
                            criteria.Index = index;

                            // break out of the loop
                            break;
                        }
                    }
                }
                
                // return value
                return criteria;
            }
            #endregion
            
            #region RemoveCriteria(int index)
            /// <summary>
            /// This method Remove Criteria
            /// </summary>
            public void RemoveCriteria(int index)
            {
                // if the index exists and is in range
                if ((index >= 0) && (index < Criteria.Count))
                {
                    // Remove this criteria
                    Criteria.RemoveAt(index); 

                    // if there are one or more criteria range items left
                    if (ListHelper.HasOneOrMoreItems(Criteria))
                    {
                        // reset
                        index = -1;

                        // iterate the Criteria
                        foreach (PixelCriteria tempCriteria in Criteria)
                        {
                            // Increment the value for index
                            index++;

                            // Set the new index
                            tempCriteria.Index = index;
                        }
                    }
                }
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
            
            #region AdjustColor
            /// <summary>
            /// This property gets or sets the value for 'AdjustColor'.
            /// </summary>
            public bool AdjustColor
            {
                get { return adjustColor; }
                set { adjustColor = value; }
            }
            #endregion
            
            #region Adjustment
            /// <summary>
            /// This property gets or sets the value for 'Adjustment'.
            /// </summary>
            public int Adjustment
            {
                get { return adjustment; }
                set { adjustment = value; }
            }
            #endregion
            
            #region Alpha
            /// <summary>
            /// This property gets or sets the value for 'Alpha'.
            /// </summary>
            public int Alpha
            {
                get { return alpha; }
                set { alpha = value; }
            }
        #endregion

            #region ApplyGrayscale
            /// <summary>
            /// This property gets or sets the value for 'ApplyGrayscale'.
            /// </summary>
            public bool ApplyGrayscale
            {
                get { return applyGrayscale; }
                set { applyGrayscale = value; }
            }
            #endregion

            #region AssignToColor
            /// <summary>
            /// This property gets or sets the value for 'AssignToColor'.
            /// </summary>
            public RGBColor AssignToColor
            {
                get { return assignToColor; }
                set { assignToColor = value; }
            }
            #endregion
            
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
            
            #region Clump
            /// <summary>
            /// This property gets or sets the value for 'Clump'.
            /// </summary>
            public Clump Clump
            {
                get { return clump; }
                set { clump = value; }
            }
            #endregion
            
            #region Color
            /// <summary>
            /// This property gets or sets the value for 'Color'.
            /// </summary>
            public Color Color
            {
                get { return color; }
                set 
                {
                    // set the value
                    color = value;

                    // Set the value for the property 'ColorSet' to true
                    colorSet = true;
                }
            }
            #endregion
            
            #region ColorSet
            /// <summary>
            /// This property gets or sets the value for 'ColorSet'.
            /// </summary>
            public bool ColorSet
            {
                get { return colorSet; }
                set { colorSet = value; }
            }
            #endregion
            
            #region ColorToAdjust
            /// <summary>
            /// This property gets or sets the value for 'ColorToAdjust'.
            /// </summary>
            public RGBColor ColorToAdjust
            {
                get { return colorToAdjust; }
                set { colorToAdjust = value; }
            }
            #endregion

            #region ContainsLastUpdateCriteria
            /// <summary>
            /// This read only returns true if this PixelQuery contains a Criteria involving LastUpdate
            /// </summary>
            /// <returns></returns>
            public bool ContainsLastUpdateCriteria
            {
                get
                {
                    // initial value
                    bool containsLastUpdate = false;

                    // if the value for HasCriteria is true
                    if (HasCriteria)
                    {
                        // Iterate the collection of PixelCriteria objects
                        foreach (PixelCriteria criteria in Criteria)
                        {
                            // if this Critieria.PixelType equals LastUpdate
                            if (criteria.PixelType == PixelTypeEnum.LastUpdate)
                            {
                                // set the return value to true
                                containsLastUpdate = true;

                                // break out of the loop
                                break;
                            }
                        }
                    }

                    // return value
                    return containsLastUpdate;
                }
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
            
            #region Directions
            /// <summary>
            /// This property gets or sets the value for 'Directions'.
            /// </summary>
            public Directions Directions
            {
                get { return directions; }
                set { directions = value; }
            }
            #endregion
            
            #region Gradient
            /// <summary>
            /// This property gets or sets the value for 'Gradient'.
            /// </summary>
            public Gradient Gradient
            {
                get { return gradient; }
                set { gradient = value; }
            }
            #endregion

            #region GrayScaleFormula
            /// <summary>
            /// This property gets or sets the value for 'GrayScaleFormula'.
            /// </summary>
            public GrayScaleFormulaEnum GrayScaleFormula
            {
                get { return grayScaleFormula; }
                set { grayScaleFormula = value; }
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
            
            #region HasAssignToColor
            /// <summary>
            /// This property returns true if this object has an 'AssignToColor'.
            /// </summary>
            public bool HasAssignToColor
            {
                get
                {
                    // initial value
                    bool hasAssignToColor = (this.AssignToColor != RGBColor.NotSet);
                    
                    // return value
                    return hasAssignToColor;
                }
            }
            #endregion
            
            #region HasClump
            /// <summary>
            /// This property returns true if this object has a 'Clump'.
            /// </summary>
            public bool HasClump
            {
                get
                {
                    // initial value
                    bool hasClump = (this.Clump != null);
                    
                    // return value
                    return hasClump;
                }
            }
            #endregion
            
            #region HasCriteria
            /// <summary>
            /// This property returns true if this object has a 'Criteria'.
            /// </summary>
            public bool HasCriteria
            {
                get
                {
                    // initial value
                    bool hasCriteria = (this.Criteria != null);
                    
                    // return value
                    return hasCriteria;
                }
            }
            #endregion
            
            #region HasDirections
            /// <summary>
            /// This property returns true if this object has a 'Directions'.
            /// </summary>
            public bool HasDirections
            {
                get
                {
                    // initial value
                    bool hasDirections = (this.Directions != null);
                    
                    // return value
                    return hasDirections;
                }
            }
            #endregion
            
            #region HasGradient
            /// <summary>
            /// This property returns true if this object has a 'Gradient'.
            /// </summary>
            public bool HasGradient
            {
                get
                {
                    // initial value
                    bool hasGradient = (this.Gradient != null);
                    
                    // return value
                    return hasGradient;
                }
            }
            #endregion
            
            #region HasMask
            /// <summary>
            /// This property returns true if this object has a 'Mask'.
            /// </summary>
            public bool HasMask
            {
                get
                {
                    // initial value
                    bool hasMask = (this.Mask != null);
                    
                    // return value
                    return hasMask;
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
            
            #region HasNormalizeColor
            /// <summary>
            /// This property returns true if this object has a 'NormalizeColor'.
            /// </summary>
            public bool HasNormalizeColor
            {
                get
                {
                    // initial value
                    bool hasNormalizeColor = (this.NormalizeColor != Color.Empty);
                    
                    // return value
                    return hasNormalizeColor;
                }
            }
            #endregion
            
            #region HasPixelsUpdated
            /// <summary>
            /// This property returns true if this object has a 'PixelsUpdated'.
            /// </summary>
            public bool HasPixelsUpdated
            {
                get
                {
                    // initial value
                    bool hasPixelsUpdated = (this.PixelsUpdated > 0);
                    
                    // return value
                    return hasPixelsUpdated;
                }
            }
            #endregion
            
            #region HasShuffler
            /// <summary>
            /// This property returns true if this object has a 'Shuffler'.
            /// </summary>
            public bool HasShuffler
            {
                get
                {
                    // initial value
                    bool hasShuffler = (this.Shuffler != null);
                    
                    // return value
                    return hasShuffler;
                }
            }
            #endregion
            
            #region HasSplitImageSettings
            /// <summary>
            /// This property returns true if this object has a 'SplitImageSettings'.
            /// </summary>
            public bool HasSplitImageSettings
            {
                get
                {
                    // initial value
                    bool hasSplitImageSettings = (this.SplitImageSettings != null);
                    
                    // return value
                    return hasSplitImageSettings;
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
            
            #region IsValid
            /// <summary>
            /// This read only property returns the value for 'IsValid'.
            /// </summary>
            public bool IsValid
            {
                get
                {
                    // initial value
                    bool isValid = (ActionType != ActionTypeEnum.Unknown);

                    // if we not have any criteria
                    if ((isValid) && (!ListHelper.HasOneOrMoreItems(Criteria)))
                    {
                        // if Alpha is not part of this query
                        if ((!queryText.ToLower().Contains("alpha")) && (ActionType != ActionTypeEnum.ShowPixels))
                        {
                            // create default pixel criteria
                            PixelCriteria pixelCriteria = new PixelCriteria();

                            // Set the Properties on the criteria
                            pixelCriteria.CriteriaText = "Alpha > 0";
                            pixelCriteria.ComparisonType = ComparisonTypeEnum.GreaterThan;
                            pixelCriteria.PixelType = PixelTypeEnum.Alpha;
                            pixelCriteria.MinValue = 1;

                            // Create Default Criteria
                            Criteria = new List<PixelCriteria>();
                            Criteria.Add(pixelCriteria);
                        }
                    }

                    // adding a test for UpdateQueries
                
                    // if currently valid
                    if ((isValid) && (actionType == ActionTypeEnum.Update))
                    {  
                        if (HasGradient)
                        {
                        }
                        else
                        {
                            // if any of the values are out of range
                            if ((red < 0) || (green < 0) || (blue < 0) || (alpha < 0) || (red > 255) || (green > 255) || (blue > 255) || (alpha > 255))
                            {
                                // set to false
                                isValid = false;
                            }
                        }
                    }

                    // if this is a Hide From query
                    if (this.ActionType == ActionTypeEnum.HideFrom)
                    {
                        // if the Directions object exists and the Directions object is not Empty
                        isValid = ((HasDirections) && (!this.Directions.Empty));

                        // if the value for isValid is true
                        if (isValid)
                        {  
                            // if the Color has been set
                            isValid = this.ColorSet;
                        }
                    }
                    
                    // return value
                    return isValid;
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
                    bool isValid = (Normalize && HasMin && HasMax && HasStep);

                    // return value
                    return isValid;
                }
            }
            #endregion

            #region Mask
            /// <summary>
            /// This property gets or sets the value for 'Mask'.
            /// </summary>
            public Mask Mask
            {
                get { return mask; }
                set { mask = value; }
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
            
            #region NormalizeColor
            /// <summary>
            /// This property gets or sets the value for 'NormalizeColor'.
            /// </summary>
            public Color NormalizeColor
            {
                get { return normalizeColor; }
                set { normalizeColor = value; }
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
            
            #region QueryText
            /// <summary>
            /// This property gets or sets the value for 'QueryText'.
            /// </summary>
            public string QueryText
            {
                get { return queryText; }
                set { queryText = value; }
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
            
            #region ScatterModX
            /// <summary>
            /// This property gets or sets the value for 'ScatterModX'.
            /// </summary>
            public int ScatterModX
            {
                get { return scatterModX; }
                set { scatterModX = value; }
            }
            #endregion
            
            #region ScatterModY
            /// <summary>
            /// This property gets or sets the value for 'ScatterModY'.
            /// </summary>
            public int ScatterModY
            {
                get { return scatterModY; }
                set { scatterModY = value; }
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
            
            #region SetColor
            /// <summary>
            /// This property gets or sets the value for 'SetColor'.
            /// </summary>
            public bool SetColor
            {
                get { return setColor; }
                set { setColor = value; }
            }
            #endregion
            
            #region SetMaskColor
            /// <summary>
            /// This property gets or sets the value for 'SetMaskColor'.
            /// </summary>
            public bool SetMaskColor
            {
                get { return setMaskColor; }
                set { setMaskColor = value; }
            }
            #endregion
            
            #region SetMaskcolorName
            /// <summary>
            /// This property gets or sets the value for 'SetMaskcolorName'.
            /// </summary>
            public string SetMaskcolorName
            {
                get { return setMaskcolorName; }
                set { setMaskcolorName = value; }
            }
            #endregion
            
            #region LargeNumberShuffler
            /// <summary>
            /// This property gets or sets the value for 'Shuffler'.
            /// </summary>
            public DataJuggler.RandomShuffler.LargeNumberShuffler Shuffler
            {
                get { return shuffler; }
                set { shuffler = value; }
            }
            #endregion
            
            #region SplitImageSettings
            /// <summary>
            /// This property gets or sets the value for 'SplitImageSettings'.
            /// </summary>
            public SplitImageSettings SplitImageSettings
            {
                get { return splitImageSettings; }
                set { splitImageSettings = value; }
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
            
            #region SwapColors
            /// <summary>
            /// This property gets or sets the value for 'SwapColors'.
            /// </summary>
            public bool SwapColors
            {
                get { return swapColors; }
                set { swapColors = value; }
            }
            #endregion
            
            #region SwapType
            /// <summary>
            /// This property gets or sets the value for 'SwapType'.
            /// </summary>
            public SwapTypeEnum SwapType
            {
                get { return swapType; }
                set { swapType = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
