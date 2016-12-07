using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GalleryProjects
{
  public  class BitmapHelpers
    {
        public  static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image 
            float height = options.OutHeight;
            float width = options.OutWidth;
            double inSampleSize = 1D;

            if (height > reqHeight || width > reqWidth)
            {
                int halfHeight = (int)(height / 2);
                int halfWidth = (int)(width / 2);

                // Calculate a inSampleSize that is a power of 2 - the decoder will use a value that is a power of two anyway. 
                while ((halfHeight / inSampleSize) > reqHeight && (halfWidth / inSampleSize) > reqWidth)
                {
                    inSampleSize *= 2;
                }
            }

            return (int)inSampleSize;
        }

        public static async void CreateBitmap(ImageView imageView, byte[] imageData)
        {
            try
            {
                if (imageData != null)
                {
                    var bm = await BitmapFactory.DecodeByteArrayAsync(imageData, 0, imageData.Length);
                    if (bm != null)
                    {
                        imageView.SetImageBitmap(bm);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Bitmap creation failed: " + e);
            }
        }
    }
}