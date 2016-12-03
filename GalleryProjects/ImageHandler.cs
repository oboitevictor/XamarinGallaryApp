using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GallarySharedProject;

namespace GalleryProjects
{
    public class ImageHandler
    {
        public static IEnumerable<GalleryItem> GetFiles(Context context)
        {
            ContentResolver cr = context.ContentResolver;

            string[] columns = new string[]
            {
                MediaStore.Images.ImageColumns.Id,
                MediaStore.Images.ImageColumns.Title,
                MediaStore.Images.ImageColumns.Data,
                MediaStore.Images.ImageColumns.DateAdded,
                MediaStore.Images.ImageColumns.MimeType,
                MediaStore.Images.ImageColumns.Size,
            };

            var cursor = cr.Query(MediaStore.Images.Media.ExternalContentUri, columns, null, null, null);

            int columnIndex = cursor.GetColumnIndex(columns[2]);

            int index = 0;

            // create max 100 items 
            while (cursor.MoveToNext() && index < 100)
            {
                index++;

                var url = cursor.GetString(columnIndex);

                var imageData = createCompressedImageDataFromBitmap(url);

                yield return new GalleryItem()
                {
                    Title = cursor.GetString(1),
                    Date = cursor.GetString(3),
                    ImageData = imageData,
                    ImageUri = url,
                };
            }
        }

        private static Byte[] createCompressedImageDataFromBitmap(string url)
        {
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InJustDecodeBounds = true;
            BitmapFactory.DecodeFile(url, options);
            options.InSampleSize = BitmapHelpers.CalculateInSampleSize(options, 1600, 1200);
            options.InJustDecodeBounds = false;

            Bitmap bm = BitmapFactory.DecodeFile(url, options);

            var stream = new MemoryStream();
            bm.Compress(Bitmap.CompressFormat.Jpeg, 80, stream);
            return stream.ToArray();
        }
    }
}