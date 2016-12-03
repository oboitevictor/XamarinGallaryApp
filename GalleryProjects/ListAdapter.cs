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
using GallarySharedProject;
using Java.Lang;

namespace GalleryProjects
{
    public class ListAdapter : BaseAdapter
    {
        private List<GalleryItem> _items;
        private Activity _context;

        public ListAdapter(Activity context) : base()
        {
            _context = context;
            _items = new List<GalleryItem>();
            foreach(var galleryitem in ImageHandler.GetFiles(_context))
            {
                _items.Add(galleryitem);
            }
        }
        public override int Count
        {
            get
            {
                return _items.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public GalleryItem GetItemByPosition(int position)
        {
            return _items[position];
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available 

            if (view == null)
            {
                // otherwise create a new one 
                view = _context.LayoutInflater.Inflate(Resource.Layout.CustomCell, null);
            }
            // set image 
            var imageView = view.FindViewById<ImageView>(Resource.Id.image);
            BitmapHelpers.CreateBitmap(imageView, _items[position].ImageData);

            // set labels 
            var titleTextView = view.FindViewById<TextView>(Resource.Id.title);
            titleTextView.Text = _items[position].Title;
            var dateTextView = view.FindViewById<TextView>(Resource.Id.date);
            dateTextView.Text = _items[position].Date;

            return view;
        }
        private async void createBitmap(ImageView imageView, byte[] imageData)
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
            catch (Java.Lang.Exception e)
            {
                Console.WriteLine("Bitmap creation failed: " + e);
            }
        }
    }
}