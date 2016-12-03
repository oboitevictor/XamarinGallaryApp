using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GalleryProjects
{
    [Activity(Label = "PhotoActivity", Icon = "@drawable/icon")]
    public class PhotoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            // Set our view from the "main" layout resource 
            SetContentView(Resource.Layout.photo_view);

            var imageData = Intent.GetByteArrayExtra("ImageData");
            var title = Intent.GetStringExtra("Title") ?? string.Empty;
            var date = Intent.GetStringExtra("Date") ?? string.Empty;

            // set image 
            var imageView = FindViewById<ImageView>(Resource.Id.image_photo);
            BitmapHelpers.CreateBitmap(imageView, imageData);

            // set labels 
            var titleTextView = FindViewById<TextView>(Resource.Id.title_photo);
            titleTextView.Text = title;
            var dateTextView = FindViewById<TextView>(Resource.Id.date_photo);
            dateTextView.Text = date;
        }
    }
}