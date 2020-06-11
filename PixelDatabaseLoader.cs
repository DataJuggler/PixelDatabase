

#region using statements

using DataJuggler.UltimateHelper.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

#endregion

namespace DataJuggler.PixelDatabase
{

    #region class PixelDatabaseLoader
    /// <summary>
    /// This class is used to load PixelDatabases and their DirectBitmaps
    /// </summary>
    public class PixelDatabaseLoader
    {

        #region Methods
            
            #region LoadPixelDatabase(Image original, StatusUpdate updateCallback)
            /// <summary>
            /// This method is used to load a PixelDatabase and its DirectBitmap object.
            /// </summary>
            /// <param name="bitmap"></param>
            /// <returns></returns>
            public static PixelDatabase LoadPixelDatabase(Image original, StatusUpdate updateCallback)
            {
                // initial valule
                PixelDatabase pixelDatabase = null;

                try
                {
                    // convert to a bitmap
                    Bitmap bitmap = (Bitmap) original;

                    // Load the PixelDatabase
                    pixelDatabase = LoadPixelDatabase(bitmap, updateCallback);
                }
                catch (Exception error)
                {
                    // write to console for now
                    DebugHelper.WriteDebugError("LoadPixelDatabase", "PixelDatabaseLoader", error);
                }
                finally
                {
                    
                }

                // return value
                return pixelDatabase;
            }
            #endregion

            #region LoadPixelDatabase(string imagePath, StatusUpdate updateCallback)
            /// <summary>
            /// This method is used to load a PixelDatabase and its DirectBitmap object from an imagePath
            /// </summary>
            /// <param name="imagePath">The path to the image</param>
            /// <param name="updateCallback">The delegate to call during load for status.</param>
            /// <param name="copyInPlaceForResetImage">If true, a copy of the image will be created, and the ResetPath and UndoPath will be set to this new filename.</param>
            /// <returns></returns>
            public static PixelDatabase LoadPixelDatabase(string imagePath, StatusUpdate updateCallback)
            {
                // initial valule
                PixelDatabase pixelDatabase = null;

                try
                {
                    // if we have an imagePath
                    if ((TextHelper.Exists(imagePath)) && (File.Exists(imagePath)))
                    {
                        // create the Bitmap
                        using (Bitmap bitmap = (Bitmap) Bitmap.FromFile(imagePath))
                        {
                            // load the pixelDatabase
                            pixelDatabase = LoadPixelDatabase(bitmap, updateCallback);                            
                        }
                    }   
                }
                catch (Exception error)
                {
                    // write to console for now
                    DebugHelper.WriteDebugError("LoadPixelDatabase", "PixelDatabaseLoader", error);
                }
                finally
                {
                    
                }

                // return value
                return pixelDatabase;
            }
            #endregion

            #region LoadPixelDatabase(Bitmap original, StatusUpdate updateCallback)
            /// <summary>
            /// This method is used to load a PixelDatabase and its DirectBitmap object.
            /// </summary>
            /// <param name="bitmap"></param>
            /// <returns></returns>
            public static PixelDatabase LoadPixelDatabase(Bitmap original, StatusUpdate updateCallback)
            {
                // initial valule
                PixelDatabase pixelDatabase = null;

                // locals
                int max = 0;
                
                try
                {
                    // if we have an image
                    if (NullHelper.Exists(original))
                    {
                        // create a new bitmap
                        using (Bitmap source = new Bitmap(original))
                        {
                             // Create a new instance of a 'PixelDatabase' object.
                            pixelDatabase = new PixelDatabase();

                            // Create a DirectBitmap
                            pixelDatabase.DirectBitmap = new DirectBitmap(source.Width, source.Height);

                            // Code To Lockbits
                            BitmapData bitmapData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadWrite, source.PixelFormat);
                            IntPtr pointer = bitmapData.Scan0;
                            int size = Math.Abs(bitmapData.Stride) * source.Height;
                            byte[] pixels = new byte[size];
                            Marshal.Copy(pointer, pixels, 0, size);

                            // End Code To Lockbits

                            // Marshal.Copy(pixels,0,pointer, size);
                            source.UnlockBits(bitmapData);

                            // locals
                            Color color = Color.FromArgb(0, 0, 0);
                            int red = 0;
                            int green = 0;
                            int blue = 0;
                            int alpha = 0;

                            // variables to hold height and width
                            int width = source.Width;
                            int height = source.Height;
                            int x = -1;
                            int y = 0;
                            
                            // if the UpdateCallback exists
                            if (NullHelper.Exists(updateCallback))
                            {
                                // Set the value for max
                                max = height * width;    

                                // Set the graph max
                                updateCallback("SetGraphMax", max);
                            }

                            // Iterating the pixel array, every 4th byte is a new pixel, much faster than GetPixel
                            for (int a = 0; a < pixels.Length; a = a + 4)
                            {
                                // increment the value for x
                                x++;

                                // every new column
                                if (x >= width)
                                {
                                    // reset x
                                    x = 0;

                                    // Increment the value for y
                                    y++;
                                }      

                                // get the values for r, g, and blue
                                blue = pixels[a];
                                green = pixels[a + 1];
                                red = pixels[a + 2];
                                alpha = pixels[a + 3];
                    
                                // create a color
                                color = Color.FromArgb(alpha, red, green, blue);

                                // Set the pixel at this spot
                                pixelDatabase.DirectBitmap.SetPixel(x, y, color);
                            }
                        }
                        
                        // Create the MaskManager 
                        pixelDatabase.MaskManager = new MaskManager();
                    }
                }
                catch (Exception error)
                {
                    // write to console for now
                    DebugHelper.WriteDebugError("LoadPixelDatabase", "PixelDatabaseLoader", error);
                }

                // return value
                return pixelDatabase;
            }
            #endregion

        #endregion

    }
    #endregion

}
