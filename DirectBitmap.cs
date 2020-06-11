

#region using statements

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class DirectBitmap
    /// <summary>
    /// This class is used as a faster alternative to GetPixel and SetPixel
    /// </summary>
    public class DirectBitmap : IDisposable
    {

        #region Private Variables
        
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a 'DirectBitmap' object.
        /// </summary>
        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new Int32[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }
        #endregion
        
        #region Methods
            
            #region Dispose()
            /// <summary>
            /// method Dispose
            /// </summary>
            public void Dispose()
            {
                if (Disposed) return;
                Disposed = true;
                Bitmap.Dispose();
                BitsHandle.Free();
            }
            #endregion
            
            #region GetPixel(int x, int y)
            /// <summary>
            /// method Get Pixel
            /// </summary>
            public Color GetPixel(int x, int y)
            {
                int index = x + (y * Width);
                int col = Bits[index];
                Color result = Color.FromArgb(col);
                
                return result;
            }
            #endregion
            
            #region SetPixel(int x, int y, Color color)
            /// <summary>
            /// method Set Pixel
            /// </summary>
            public void SetPixel(int x, int y, Color color)
            {
                int index = x + (y * Width);
                int col = color.ToArgb();
                
                Bits[index] = col;
            }
            #endregion

        #endregion
        
        #region Properties
            
            #region Bitmap
            /// <summary>
            /// method [Enter Method Description]
            /// </summary>
            public Bitmap Bitmap { get; private set; }
            #endregion
            
            #region Bits
            /// <summary>
            /// method [Enter Method Description]
            /// </summary>
            public Int32[] Bits { get; private set; }
            #endregion
            
            #region BitsHandle
            /// <summary>
            /// This is a ptr to the garbage collector
            /// </summary>
            protected GCHandle BitsHandle { get; private set; }
            #endregion
            
            #region Disposed
            /// <summary>
            /// method [Enter Method Description]
            /// </summary>
            public bool Disposed { get; private set; }
            #endregion
            
            #region Height
            /// <summary>
            /// method [Enter Method Description]
            /// </summary>
            public int Height { get; private set; }
            #endregion
            
            #region Width
            /// <summary>
            /// method [Enter Method Description]
            /// </summary>
            public int Width { get; private set; }
            #endregion
            
        #endregion
        
    }
    #endregion

}
