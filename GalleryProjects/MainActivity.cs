using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace GalleryProjects
{
    [Activity(Label = "GalleryProjects", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private ListAdapter _adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            _adapter = new ListAdapter(this);

            var listView = FindViewById<ListView>(Resource.Id.listView);
            listView.Adapter = _adapter;

            listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                var galleryItem = _adapter.GetItemByPosition(e.Position);
                var photoActivity = new Intent(this, typeof(PhotoActivity));
                photoActivity.PutExtra("ImageData", galleryItem.ImageData);
                photoActivity.PutExtra("Title", galleryItem.Title);
                photoActivity.PutExtra("Date", galleryItem.Date);
                StartActivity(photoActivity);
            };
        }
    }
}

